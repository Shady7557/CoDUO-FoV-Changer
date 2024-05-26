using CurtLog;
using DirectoryExtensions;
using serverside_dcim.Misc;
using ShadyPool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//This wasn't written by me. I found it on stackoverflow and nobody seems to have claimed "ownership" of it. I've increasingly made modifications to it.
namespace ProcessExtensions
{
    public class ProcessExtension
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(ProcessAccessFlags access, bool inheritHandle, int procId);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        private static readonly HashSet<Process> _elevatedProcesses = new HashSet<Process>(); //this is a cache.
        private static readonly HashSet<int> _elevatedPids = new HashSet<int>(); //sometimes I hate windows, truly. don't ask why this is necessary. fix it yourself & let me know.

        private static readonly Dictionary<int, TimeCachedValue<bool>> _isElevated = new Dictionary<int, TimeCachedValue<bool>>();

        /// <summary>
        /// Checks if given process is still alive
        /// </summary>
        /// <param name="processId">process id</param>
        /// <returns>true if process is alive, false if process is not alive or process is elevated and inaccessible</returns>
        public static bool IsProcessAlive(int processId)
        {
            var h = OpenProcess(ProcessAccessFlags.QueryInformation, true, processId);

            if (h == IntPtr.Zero) //this will be zero if the process was elevated.
                return false;
            

            var b = GetExitCodeProcess(h, out uint code);
            CloseHandle(h);

            if (b)
                b = code == 259 /* STILL_ACTIVE  */;

            return b;
        }

        /*/This part below was added by myself (Shady)/*/
        public static string GetFileNameFromProcessName(string processName)
        {
            if (string.IsNullOrEmpty(processName))
                throw new ArgumentNullException(nameof(processName));

            var procs = Process.GetProcessesByName(processName);
            if (procs == null || procs.Length < 1)
                return string.Empty;


            var activeProcesses = Pool.GetList<Process>();
            try
            {
                for (int i = 0; i < procs.Length; i++)
                {
                    var p = procs[i];
                    if (!(p?.HasExited ?? true))
                        activeProcesses.Add(p);

                }

                return GetFileNameFromProcess(activeProcesses.OrderByDescending(p => p.StartTime).FirstOrDefault());
            }
            finally { Pool.FreeList(ref activeProcesses); }
        }

        public static string GetFileNameFromProcess(Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));
            if (process.HasExited)
                throw new InvalidOperationException(nameof(process));


            var sb = Pool.Get<StringBuilder>();
            try
            {

                var fileName = DirectoryExtension.GetMainModuleFileNameNoAlloc(process, ref sb);

                var logTxt = sb.Clear().Append(nameof(GetFileNameFromProcess)).Append(" returned: ").Append(fileName).ToString();
                Console.WriteLine(logTxt);
                Log.WriteLine(logTxt);

                return fileName;
            }
            finally { Pool.Free(ref sb); }
        }

        public static bool IsAnyProcessRunning(string processName)
        {
            if (string.IsNullOrEmpty(processName))
                throw new ArgumentNullException(nameof(processName));

            var procs = Process.GetProcessesByName(processName);
            if (procs == null || procs.Length < 1)
                return false;


            for (int i = 0; i < procs.Length; i++)
            {
                try
                {
                    var p = procs[i];
                    if (p != null && IsProcessAlive(p.Id))
                        return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }
            }

            return false;
        }

        public static bool IsProcessElevated(Process process, bool skipCache = false)
        {
            if (process == null)
                return false;

         

            if (!_isElevated.TryGetValue(process.Id, out var cachedValue))
                _isElevated[process.Id] = new TimeCachedValue<bool>()
                {
                    UpdateValue = new Func<bool>(() =>
                    {
                        //cache so we aren't always throwing exceptions (they are slow!)
                        if (_elevatedProcesses.Contains(process))
                            return true;


                        if (_elevatedPids.Contains(process.Id))
                            return true;

                        try
                        {
                            if (process?.Modules != null)
                                return false;
                        }
                        catch (Win32Exception win32ex) when (win32ex.NativeErrorCode == 5) //5 == access denied
                        {
                            try
                            {
                                _elevatedProcesses.Add(process);
                                _elevatedPids.Add(process.Id);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                Log.WriteLine(ex.ToString());
                            }

                            return true;
                        }
                        catch (Win32Exception win32ex) when (win32ex.NativeErrorCode == 299) //299 = partial memory read/write - presumably 5 would've been thrown before this exception could be thrown if elevation was an issue.
                        {
                            // do literally nothing - we can do nothing about this exception & it clogs up the log.
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLine(ex.ToString());
                            Console.WriteLine(ex.ToString());
                            throw ex;
                        }

                        return false;
                    }),
                    RefreshCooldown = 10
                };


            return _isElevated[process.Id].Get(skipCache);
        }

        public static bool IsAnyCoDProcessRunning()
        {
            foreach (var name in PathInfos.GAME_PROCESS_NAMES)
                if (IsAnyProcessRunning(name))
                    return true;

            return false;
        }

        public static bool IsCoDMPProcess(Process proc)
        {
            if (proc == null)
                throw new ArgumentNullException(nameof(proc));

            var procName = proc?.ProcessName?.ToLower();

            if (string.IsNullOrWhiteSpace(procName))
                return false;

            switch (procName)
            {
                case "coduomp":
                case "codmp":
                case "mohaa":
                    return true;
                default: return false;
            }
        }

    }
}

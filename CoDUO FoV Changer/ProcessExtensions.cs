using CurtLog;
using DirectoryExtensions;
using ShadyPool;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//This wasn't written by me. I found it on stackoverflow and nobody seems to have claimed "ownership" of it. I did make some slight modifications
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


        /// <summary>
        /// Checks if given process is still alive
        /// </summary>
        /// <param name="processId">process id</param>
        /// <returns>true if process is alive, false if not</returns>
        public static bool IsProcessAlive(int processId)
        {
            var h = OpenProcess(ProcessAccessFlags.QueryInformation, true, processId);

            if (h == IntPtr.Zero) 
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
    }
}

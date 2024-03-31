using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Linq;
using CurtLog;
//This wasn't written by me (Shady), and quite frankly I can't remember who did but I'm pretty sure it's fairly generic at this point
//I have made some significant changes to this, rewriting some of the code and deleting some
namespace ReadWriteMemory
{
    public class ProcessMemory
    {
        // Fields
        public int BaseAddress { get; protected set; }
        public Process Process { get; protected set; }
        public ProcessModule ProcessModule { get; protected set; }
        public int processHandle { get; protected set; }
        public string ProcessName { get; protected set; }
        public int ProcessPID { get; protected set; }

        // Methods
        public ProcessMemory(string pProcessName) { ProcessName = !string.IsNullOrEmpty(pProcessName) ? pProcessName.Replace(".exe", string.Empty) : string.Empty; }

        public ProcessMemory(int pPid) { ProcessPID = pPid; }


        public bool CheckProcess()
        {
            if (ProcessPID > 0 && ProcessExtensions.ProcessExtension.IsProcessAlive(ProcessPID))
                return true;

            if (string.IsNullOrWhiteSpace(ProcessName))
                return false;

            var procs = Process.GetProcesses();

            for (int i = 0; i < procs.Length; i++)
            {
                var proc = procs[i];

                if (proc.ProcessName.Equals(ProcessName, StringComparison.OrdinalIgnoreCase) && ProcessExtensions.ProcessExtension.IsProcessAlive(proc.Id))
                    return true;
            }

            return false;
        }

        public bool StartProcess()
        {
            Process proc = null;

            var procs = Process.GetProcesses();
            for (int i = 0; i < procs.Length; i++)
            {
                var p = procs[i];
                if ((ProcessPID != 0 && p?.Id == ProcessPID) || p.ProcessName.Equals(ProcessName, StringComparison.OrdinalIgnoreCase))
                {
                    proc = p;
                    break;
                }
            }

            if (proc == null || proc.Id == 0)
                return false;

            Process = proc;

            if (string.IsNullOrEmpty(ProcessName)) ProcessName = proc.ProcessName;

            if (ProcessPID == 0)
                ProcessPID = proc.Id;

            processHandle = OpenProcess(2035711, false, proc.Id);

            return processHandle != 0;
        }

        public int DllImageAddress(string dllname)
        {
            if (string.IsNullOrWhiteSpace(dllname))
                throw new ArgumentNullException(nameof(dllname));

            try
            {
                if (Process == null || RequiresElevation())
                {
                    var msg = nameof(DllImageAddress) + " Process null or requires elevation.";

                    Log.WriteLine(msg);
                    Console.WriteLine(msg);

                    return 0;
                }

                Process tempProc;

                try { tempProc = Process.GetProcessById(Process.Id) ?? Process; } //hacky workaround for modules not being detected
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    Console.WriteLine(ex.ToString());
                    tempProc = Process;
                }

                if (tempProc == null || !ProcessExtensions.ProcessExtension.IsProcessAlive(tempProc.Id))
                    return 0;
                

                IntPtr? addr = null;

                var modules = tempProc?.Modules?.Cast<ProcessModule>() ?? null;

                if (modules == null || !modules.Any())
                {
                    var msg = nameof(DllImageAddress) + " modules null/empty!";
                    Log.WriteLine(msg);
                    Console.WriteLine(msg);
                    return 0;
                }

                foreach (var module in modules)
                {
                    try
                    {
                        if (module?.ModuleName == dllname)
                        {
                            addr = module.BaseAddress;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLine(ex.ToString());
                        Console.WriteLine(ex.ToString());
                    }
                }

                return (addr == null) ? 0 : (int)addr;
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            return 0;
        }

        public bool RequiresElevation()
        {
            if (Process == null)
                return false;

            try
            {
                if (Process?.Modules != null)
                    return false;
            }
            catch (System.ComponentModel.Win32Exception win32ex) when (win32ex.NativeErrorCode == 5) { return true; }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
                throw ex;
            }

            return false;
        }

        public int ImageAddress()
        {
            BaseAddress = 0;
            ProcessModule = Process.MainModule;
            BaseAddress = (int)ProcessModule.BaseAddress;
            return BaseAddress;
        }

        public int ImageAddress(int pOffset)
        {
            BaseAddress = 0;
            ProcessModule = Process.MainModule;
            BaseAddress = (int)ProcessModule.BaseAddress;
            return pOffset + BaseAddress;
        }
        #region Reading
        public byte ReadByte(int pOffset)
        {
            byte[] buffer = new byte[1];
            ReadProcessMemory(processHandle, pOffset, buffer, 1, 0);
            return buffer[0];
        }

        public byte[] ReadMem(int pOffset, int pSize)
        {
            byte[] buffer = new byte[pSize];
            ReadProcessMemory(processHandle, pOffset, buffer, pSize, 0);
            return buffer;
        }

        public byte[] ReadMem(int pOffset, int pSize, bool AddToImageAddress)
        {
            byte[] buffer = new byte[pSize];
            int lpBaseAddress = AddToImageAddress ? ImageAddress(pOffset) : pOffset;
            ReadProcessMemory(processHandle, lpBaseAddress, buffer, pSize, 0);
            return buffer;
        }
        #endregion
        #region Writing
        public void WriteByte(int pOffset, byte pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes));
        public void WriteByte(bool AddToImageAddress, int pOffset, byte pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes), AddToImageAddress);
        public void WriteByte(string Module, int pOffset, byte pBytes) => WriteMem(DllImageAddress(Module) + pOffset, BitConverter.GetBytes(pBytes));


        public void WriteDouble(int pOffset, double pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes));
        public void WriteDouble(bool AddToImageAddress, int pOffset, double pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes), AddToImageAddress);
        public void WriteDouble(string Module, int pOffset, double pBytes) => WriteMem(DllImageAddress(Module) + pOffset, BitConverter.GetBytes(pBytes));

        public void WriteFloat(int pOffset, float pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes));
        public void WriteFloat(bool AddToImageAddress, int pOffset, float pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes), AddToImageAddress);
        public void WriteFloat(string Module, int pOffset, float pBytes) => WriteMem(DllImageAddress(Module) + pOffset, BitConverter.GetBytes(pBytes));

        public void WriteInt(int pOffset, int pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes));
        public void WriteInt(int pOffset, int pBytes, int nsize = 0)
        {
            if (nsize == 0) nsize = BitConverter.GetBytes(pBytes).Length;
            WriteMem(pOffset, BitConverter.GetBytes(pBytes), nsize);
        }
        public void WriteInt(bool AddToImageAddress, int pOffset, int pBytes) => WriteMem(pOffset, BitConverter.GetBytes(pBytes), AddToImageAddress);
        public void WriteInt(string Module, int pOffset, int pBytes) => WriteMem(DllImageAddress(Module) + pOffset, BitConverter.GetBytes(pBytes));


        public void WriteMem(int pOffset, byte[] pBytes) => WriteProcessMemory(processHandle, pOffset, pBytes, pBytes.Length, 0);
        public void WriteMem(int pOffset, byte[] pBytes, bool AddToImageAddress) => WriteProcessMemory(processHandle, AddToImageAddress ? ImageAddress(pOffset) : pOffset, pBytes, pBytes.Length, 0);
        public void WriteMem(int pOffset, byte[] pBytes, int nsize) => WriteProcessMemory(processHandle, pOffset, pBytes, nsize, 0);
        #endregion
        #region Imports
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtectEx(int hProcess, int lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(int hObject);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern int FindWindowByCaption(int ZeroOnly, string lpWindowName);
        #endregion


        // Nested Types
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 2035711,
            CreateThread = 2,
            DupHandle = 64,
            QueryInformation = 1024,
            SetInformation = 512,
            Synchronize = 1048576,
            Terminate = 1,
            VMOperation = 8,
            VMRead = 16,
            VMWrite = 32
        }
    }
}
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Text;
using System.Collections;
using System.Linq;
//This wasn't written by me (Shady), and quite frankly I can't remember who did but I'm pretty sure it's fairly generic at this point
//I have made changes to this, rewriting some of the code and deleting some
namespace ReadWriteMemory
{
    internal class ProcessMemory
    {
        // Fields
        public int BaseAddress { get; protected set; }
        public Process _Process { get; protected set; }
        public ProcessModule ProcessModule { get; protected set; }
        private const uint PAGE_EXECUTE = 16;
        private const uint PAGE_EXECUTE_READ = 32;
        private const uint PAGE_EXECUTE_READWRITE = 64;
        private const uint PAGE_EXECUTE_WRITECOPY = 128;
        private const uint PAGE_GUARD = 256;
        private const uint PAGE_NOACCESS = 1;
        private const uint PAGE_NOCACHE = 512;
        private const uint PAGE_READONLY = 2;
        private const uint PAGE_READWRITE = 4;
        private const uint PAGE_WRITECOPY = 8;
        private const uint PROCESS_ALL_ACCESS = 2035711;
        public int processHandle { get; protected set; }
        public string ProcessName { get; protected set; }
        public int ProcessPID { get; protected set; }

        // Methods
        public ProcessMemory(string pProcessName) { ProcessName = !string.IsNullOrEmpty(pProcessName) ? pProcessName.Replace(".exe", "") : string.Empty; }
        public ProcessMemory(int pPid) { ProcessPID = pPid; }

        public bool CheckProcess() { return (Process.GetProcessesByName(ProcessName).Length > 0 || Process.GetProcessById(ProcessPID) != null); }

        public bool StartProcess()
        {
            var proc = (ProcessPID != 0) ? Process.GetProcessById(ProcessPID) : !string.IsNullOrEmpty(ProcessName) ? Process.GetProcessesByName(ProcessName)?.FirstOrDefault() ?? null : null;
            if (proc == null || proc.Id == 0) return false;
            _Process = proc;
            processHandle = OpenProcess(2035711, false, proc.Id);
            return (processHandle != 0);
        }

        public int DllImageAddress(string dllname)
        {
            if (string.IsNullOrEmpty(dllname)) return -1;
            ProcessModuleCollection modules = _Process.Modules;
            return (int)(modules?.Cast<ProcessModule>()?.Where(p => p?.ModuleName == dllname)?.FirstOrDefault()?.BaseAddress ?? null);
        }

        public int ImageAddress()
        {
            BaseAddress = 0;
            ProcessModule = _Process.MainModule;
            BaseAddress = (int)ProcessModule.BaseAddress;
            return BaseAddress;
        }

        public int ImageAddress(int pOffset)
        {
            BaseAddress = 0;
            ProcessModule = _Process.MainModule;
            BaseAddress = (int)ProcessModule.BaseAddress;
            return (pOffset + BaseAddress);
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
        public void WriteByte(int pOffset, byte pBytes) => WriteMem(pOffset, BitConverter.GetBytes((short)pBytes));
        public void WriteByte(bool AddToImageAddress, int pOffset, byte pBytes) => WriteMem(pOffset, BitConverter.GetBytes((short)pBytes), AddToImageAddress);
        public void WriteByte(string Module, int pOffset, byte pBytes) => WriteMem(DllImageAddress(Module) + pOffset, BitConverter.GetBytes((short)pBytes));
        

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
        public void WriteMem(int pOffset, byte[] pBytes, bool AddToImageAddress) => WriteProcessMemory(processHandle, (AddToImageAddress ? ImageAddress(pOffset) : pOffset), pBytes, pBytes.Length, 0);
        public void WriteMem(int pOffset, byte[] pBytes, int nsize) => WriteProcessMemory(processHandle, pOffset, pBytes, nsize, 0);
        #endregion
        #region Imports
        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(int hProcess, int lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
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
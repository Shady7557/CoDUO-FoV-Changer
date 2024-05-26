using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoDUO_FoV_Changer
{
    public enum DllInjectionResult
    {
        DllNotFound,
        GameProcessNotFound,
        InjectionFailed,
        Success
    }

    public sealed class Injector
    {
        static readonly IntPtr INTPTR_ZERO = (IntPtr)0;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        static Injector _instance;

        public static Injector GetInstance() { return _instance ?? (_instance = new Injector()); }

        Injector() { }

        public DllInjectionResult Inject(int processId, string dllPath)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ArgumentNullException(nameof(dllPath));

            if (!File.Exists(dllPath))
                return DllInjectionResult.DllNotFound;

            return Inject((uint)processId, dllPath) ? DllInjectionResult.Success : DllInjectionResult.InjectionFailed;

        }

        private bool Inject(uint processId, string dllPath)
        {
            if (processId == 0 || string.IsNullOrWhiteSpace(dllPath)) 
                return false;

            var hndProc = OpenProcess((0x2 | 0x8 | 0x10 | 0x20 | 0x400), 1, processId);

            if (hndProc == INTPTR_ZERO) 
                return false;


            IntPtr lpLLAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (lpLLAddress == INTPTR_ZERO) 
                return false;


            IntPtr lpAddress = VirtualAllocEx(hndProc, (IntPtr)null, (IntPtr)dllPath.Length, (0x1000 | 0x2000), 0X40);

            if (lpAddress == INTPTR_ZERO) 
                return false;


            byte[] bytes = Encoding.ASCII.GetBytes(dllPath);

            if (WriteProcessMemory(hndProc, lpAddress, bytes, (uint)bytes.Length, 0) == 0) 
                return false;


            if (CreateRemoteThread(hndProc, (IntPtr)null, INTPTR_ZERO, lpLLAddress, lpAddress, 0, (IntPtr)null) == INTPTR_ZERO) 
                return false;

            CloseHandle(hndProc);

            return true;
        }
    }
}

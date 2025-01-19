using CurtLog;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoDUO_FoV_Changer
{
    // Used for Steam Overlay injection / to make the Steam Overlay work.

    public enum DllInjectionResult
    {
        DllNotFound,
        GameProcessNotFound,
        InjectionFailed,
        Success
    }

    public sealed class Injector
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        private static readonly IntPtr _intPtrZero = (IntPtr)0;
        private static Injector _instance;

        public static Injector Instance => _instance ?? (_instance = new Injector());
      
        private Injector() { }

        public async Task<DllInjectionResult> Inject(int processId, string dllPath, int msDelay = 0)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ArgumentNullException(nameof(dllPath));

            if (!File.Exists(dllPath))
                return DllInjectionResult.DllNotFound;

            if (msDelay > 0)
                await Task.Delay(msDelay);

            try 
            {
                return Inject((uint)processId, dllPath) ? DllInjectionResult.Success : DllInjectionResult.InjectionFailed; 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());

                return DllInjectionResult.InjectionFailed;
            }
        }

        private bool Inject(uint processId, string dllPath)
        {
            if (processId == 0 || string.IsNullOrWhiteSpace(dllPath)) 
                return false;

            var hndProc = OpenProcess(0x2 | 0x8 | 0x10 | 0x20 | 0x400, 1, processId);

            if (hndProc == _intPtrZero) 
                return false;


            var lpLLAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

            if (lpLLAddress == _intPtrZero) 
                return false;


            var lpAddress = VirtualAllocEx(hndProc, (IntPtr)null, (IntPtr)dllPath.Length, 0x1000 | 0x2000, 0X40);

            if (lpAddress == _intPtrZero) 
                return false;


            var bytes = Encoding.ASCII.GetBytes(dllPath);

            if (WriteProcessMemory(hndProc, lpAddress, bytes, (uint)bytes.Length, 0) == 0) 
                return false;


            if (CreateRemoteThread(hndProc, (IntPtr)null, _intPtrZero, lpLLAddress, lpAddress, 0, (IntPtr)null) == _intPtrZero) 
                return false;

            CloseHandle(hndProc);

            return true;
        }
    }
}

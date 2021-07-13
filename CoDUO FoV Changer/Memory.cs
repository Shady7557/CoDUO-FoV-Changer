﻿using ReadWriteMemory;
using System;
using System.Text;

namespace CoDUO_FoV_Changer
{
    public class Memory
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public ProcessMemory ProcMemory { get; }

        public Memory(string processName)
        {
            if (string.IsNullOrEmpty(processName)) throw new ArgumentNullException();
            ProcMemory = new ProcessMemory(processName);
            ProcMemory.StartProcess();
        }

        public Memory(int processID)
        {
            ProcMemory = new ProcessMemory(processID);
            ProcMemory.StartProcess();
        }

        public bool IsRunning() { return ProcMemory?.CheckProcess() ?? false; }

        public override string ToString()
        {
            var proc = ProcMemory?.Process;
            if (proc == null)
            {
                Console.WriteLine("proc is null on tostring()!!");
                return base.ToString();
            }

            return _stringBuilder.Append(proc.ProcessName).Append(" (").Append(proc.Id).Append(")").ToString();
        }

        #region Reading
        public int GetIntPointerAddress(IntPtr baseAddress, int offset) { return !IsRunning() ? 0 : IntPtr.Add((IntPtr)Convert.ToInt32(BitConverter.ToInt32(ProcMemory.ReadMem(baseAddress.ToInt32(), 4), 0).ToString("X"), 16), offset).ToInt32(); }
        public int GetIntPointerAddress(int baseAddress, int offset) { return GetIntPointerAddress((IntPtr)baseAddress, offset); }
        public int ReadIntAddress(IntPtr baseAddress, int offset, int pSize = 4, int startIndex = 0) { return ReadIntAddress(baseAddress.ToInt32(), offset, pSize, startIndex); }
        public int ReadIntAddress(int baseAddress, int offset, int pSize = 4, int startIndex = 0) { return !IsRunning() ? 0 : BitConverter.ToInt32(ProcMemory.ReadMem(GetIntPointerAddress(baseAddress, offset), pSize), startIndex); }
        #endregion
    }
}

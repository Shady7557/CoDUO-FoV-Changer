using System;
using System.Diagnostics;
using TimerExtensions;

namespace SessionHandling
{
    public class SessionHandler
    {
        private DateTime LastSessionTime = DateTime.MinValue;
        private DateTime StartSessionTime = DateTime.MinValue;
        public TimeSpan GetSessionTime() { return LastSessionTime - StartSessionTime; }

        public bool IsGameRunning()
        {
            var procs = Process.GetProcesses();
            for(int i = 0; i < procs.Length; i++)
            {
                var procName = procs[i]?.ProcessName ?? string.Empty;
                if (procName == "CoDUOMP" || procName == "CoDMP" || procName == "mohaa") return true;
            }
            return false;
        }

        public SessionHandler()
        {
            var procAct = new Action(() =>
            {
                var now = DateTime.UtcNow;
                if (IsGameRunning())
                {
                    if (StartSessionTime <= DateTime.MinValue) StartSessionTime = now;
                    LastSessionTime = now;
                }
            });
            TimerEx.Every(1f, procAct);
        }
        
    }
}

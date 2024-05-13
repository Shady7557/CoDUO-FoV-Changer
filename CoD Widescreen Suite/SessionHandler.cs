﻿using System;
using System.Diagnostics;
using TimerExtensions;

namespace SessionHandling
{
    public class SessionHandler
    {
        private DateTime _lastSessionTime = DateTime.MinValue;
        private DateTime _startSessionTime = DateTime.MinValue;
        public TimeSpan GetSessionTime() { return _lastSessionTime - _startSessionTime; }

        public bool IsGameRunning()
        {
            var procs = Process.GetProcesses();
            for (int i = 0; i < procs.Length; i++)
                if (ProcessExtensions.ProcessExtension.IsCoDMPProcess(procs[i]))
                    return true;
            
            return false;
        }

        public SessionHandler()
        {
            var procAct = new Action(() =>
            {
                if (!IsGameRunning())
                    return;

                var now = DateTime.UtcNow;

                if (_startSessionTime <= DateTime.MinValue)
                    _startSessionTime = now;

                _lastSessionTime = now;
            });

            procAct.Invoke();
            TimerEx.Every(1f, procAct);
        }

    }
}
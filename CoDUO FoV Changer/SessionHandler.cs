using System;
using TimerExtensions;

namespace SessionHandling
{
    public class SessionHandler
    {
        private DateTime _lastSessionTime = DateTime.MinValue;
        private DateTime _startSessionTime = DateTime.MinValue;
        public TimeSpan GetSessionTime() => _lastSessionTime - _startSessionTime;


        public SessionHandler()
        {
            var procAct = new Action(() =>
            {
                if (!ProcessExtensions.ProcessExtension.IsAnyCoDProcessRunning())
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

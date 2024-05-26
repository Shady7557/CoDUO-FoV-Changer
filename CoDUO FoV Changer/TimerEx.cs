using System;
using System.Collections.Generic;
using System.Threading;

// beautiful mess
namespace TimerExtensions
{
    public static class TimerEx
    {
        // HashSet to keep track of all timers - this was seemingly necessary to keep them from being GC'd/otherwise not firing off.
        private static readonly HashSet<Timer> _timers = new HashSet<Timer>();
        public static Timer Once(int seconds, Action action)
        {
            Timer t = null;

            t = new Timer(p =>
            {
                try { action.Invoke(); }
                finally { _timers?.Remove(t); }

            }, null, seconds * 1000, Timeout.Infinite);

            _timers.Add(t);

            return t;
        }
        public static Timer Once(float seconds, Action action) => Once((int)seconds, action);
        public static Timer Once(TimeSpan time, Action action)
        {
            if (time <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(time));

            return Once((int)time.TotalSeconds, action);
        }
        public static RepeatingTimer Every(float seconds, Action action) => new RepeatingTimer(seconds, action);

        public class RepeatingTimer
        {
            private long _currentLoops = 0;

            private Timer _timer = null;

            private Action _timerAction;

            private Action _loopAction;

            private readonly Action _originalAction;

            public float Interval { get; set; } = 0f;
            public long Loops { get; set; } = 0;

            public Action TimerAction
            {
                get => _timerAction;
                set
                {
                    _timer = null;

                    _timerAction = value;

                    if (_timerAction == null)
                        return;

                    if (Interval <= 0) 
                        return;

                    _loopAction = new Action(() =>
                    {
                        _timer = null;

                        if (TimerAction == null) 
                            return;

                        TimerAction.Invoke();

                        if (Loops > 0 && _currentLoops >= Loops) 
                            return;

                        _timer = Once(Interval, _loopAction);

                        _currentLoops++;
                    });

                    _timer = Once(Interval, _loopAction);

                }
            }

            public void Stop()
            {
                _timer?.Dispose();
                _timer = null;
                TimerAction = null;
            }

            public void Resume()
            {
                TimerAction = _originalAction;
            }

            public RepeatingTimer() { }
            public RepeatingTimer(float interval, Action action)
            {
                if (interval <= 0) 
                    throw new ArgumentOutOfRangeException(nameof(interval));

                Interval = interval;

                TimerAction = _originalAction = action ?? throw new ArgumentNullException(nameof(action));
            }
        }
    }
}
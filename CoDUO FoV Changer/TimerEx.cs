using System;
using System.Threading;

//beautiful mess
namespace TimerExtensions
{
    public static class TimerEx
    {
        public static Timer Once(float seconds, Action action) { return new Timer(p => action.Invoke(), null, (int)seconds * 1000, Timeout.Infinite); }
        public static RepeatingTimer Every(float seconds, Action action) => new RepeatingTimer(seconds, action);
        public class RepeatingTimer
        {
            private int _currentLoops = 0;
            private Timer _timer = null;
            private Action _timerAction;

            public float Interval { get; set; } = 0f;
            public int Loops { get; set; } = 0;
          
            public Action TimerAction
            {
                get { return _timerAction; }
                set
                {
                    _timer = null;

                    _timerAction = value;

                    if (Interval <= 0) return;

                    Action loopAct = null;
                    loopAct = new Action(() =>
                    {
                        _timer = null;

                        if (TimerAction == null) return;

                        TimerAction.Invoke();

                        if (Loops > 0 && _currentLoops >= Loops) return;

                        _timer = Once(Interval, loopAct);

                        _currentLoops++;
                    });

                    _timer = Once(Interval, loopAct);

                }
            }
            public RepeatingTimer() { }
            public RepeatingTimer(float interval, Action action)
            {
                if (interval <= 0) throw new ArgumentOutOfRangeException(nameof(interval));
                Interval = interval;
                TimerAction = action ?? throw new ArgumentNullException(nameof(action));
            }
        }
    }
}
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
            public float Interval { get; set; } = 0f;
            public int Loops { get; set; } = 0;
            private int _currentLoops = 0;
            private Timer timer = null;
            private Action _timerAction;
            public Action TimerAction
            {
                get { return _timerAction; }
                set
                {
                    if (timer != null) timer = null;
                    _timerAction = value;
                    if (Interval > 0)
                    {
                        Action loopAct = null;
                        loopAct = new Action(() =>
                        {
                            if (timer != null) timer = null;
                            if (TimerAction == null) return;
                            TimerAction.Invoke();
                            if (Loops > 0 && _currentLoops >= Loops) return;
                            timer = Once(Interval, loopAct);
                            _currentLoops++;
                        });
                        timer = Once(Interval, loopAct);
                    }
                   
                }
            }
            public RepeatingTimer() { }
            public RepeatingTimer(float interval, Action action)
            {
                Interval = interval;
                TimerAction = action;
            }
        }
    }
}
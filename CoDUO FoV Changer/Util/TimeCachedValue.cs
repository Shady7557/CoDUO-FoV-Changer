using System;

namespace serverside_dcim.Misc
{

    public class TimeCachedValue<T>
    {
        public float RefreshCooldown { get; set; }

        public Func<T> UpdateValue { get; set; }

        private T _cachedValue;

        private DateTime _lastRun;

        private bool _hasRun;

        private bool _forceNextRun;

        private DateTime _lastForce;
        private TimeSpan _cacheInvalidationTime = TimeSpan.Zero;

        public T Get(bool force)
        {
            var cooldown = DateTime.UtcNow - _lastRun;

            if ((float)cooldown.TotalSeconds < RefreshCooldown && !force && _hasRun && !_forceNextRun)
                return _cachedValue;


            _hasRun = true;

            if (_cacheInvalidationTime == TimeSpan.Zero || DateTime.UtcNow - _lastForce >= _cacheInvalidationTime)
            {
                _forceNextRun = false;
                _cacheInvalidationTime = TimeSpan.Zero;
            }

            _lastRun = DateTime.UtcNow;

            return _cachedValue = UpdateValue != null ? UpdateValue() : default;
        }

        public void ForceNextRun()
        {
            _forceNextRun = true;
            _lastForce = DateTime.UtcNow;
        }
        public void ForceNextRun(TimeSpan durationToForce)
        {
            _forceNextRun = true;
            _lastForce = DateTime.UtcNow;
            _cacheInvalidationTime = durationToForce;
        }
    }
}

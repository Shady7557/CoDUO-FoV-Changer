// Based on .NET internal code
// Example: https://source.dot.net/#System.Net.Primitives/src/libraries/Common/src/System/Text/StringBuilderCache.cs,a6dbe82674916ac0
using System;
using System.Text;

namespace CoDUO_FoV_Changer.Util
{
    public static class StringBuilderCache
    {
        internal const int MaxBuilderSize = 360;
        private const int DefaultCapacity = 16; // == StringBuilder.DefaultCapacity

        [ThreadStatic]
        private static StringBuilder t_cachedInstance = null;

        /// <summary>Get a StringBuilder for the specified capacity.</summary>
        /// <remarks>If a StringBuilder of an appropriate size is cached, it will be returned and the cache emptied.</remarks>
        public static StringBuilder Acquire(int capacity = DefaultCapacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            if (capacity <= MaxBuilderSize)
            {
                var sb = t_cachedInstance;

                if (sb != null && capacity <= sb.Capacity)
                {
                    t_cachedInstance = null;

                    sb.Clear();

                    return sb;
                }

            }

            return new StringBuilder(capacity);
        }

        /// <summary>Place the specified builder in the cache if it is not too big.</summary>
        public static void Release(StringBuilder sb)
        {
            if (sb is null)
                throw new ArgumentNullException(nameof(sb));

            if (sb.Capacity <= MaxBuilderSize)
                t_cachedInstance = sb;
            
        }

        /// <summary>ToString() the stringbuilder, Release it to the cache, and return the resulting string.</summary>
        public static string GetStringAndRelease(StringBuilder sb)
        {
            if (sb is null)
                throw new ArgumentNullException(nameof(sb));

            var result = sb.ToString();
            Release(sb);
            return result;
        }
    }
}

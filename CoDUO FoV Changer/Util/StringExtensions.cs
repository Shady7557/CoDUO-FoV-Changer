using System;

namespace StringExtension
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return source.IndexOf(value, comparisonType) >= 0;
        }
    }
}

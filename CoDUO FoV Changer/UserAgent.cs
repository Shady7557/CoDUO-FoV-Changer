using CoDUO_FoV_Changer.Util;
using StringExtension;
using System;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal class UserAgent
    {
        private const string APP_NAME_DASH = "CoDUO-FoV-Changer";
        private const string BASE_USER_AGENT = APP_NAME_DASH + "/{version} ({os}; {architecture})";
        private static string _userAgent = string.Empty;
        public static string UserAgentString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_userAgent))
                    _userAgent = StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(56)
                    .Clear()
                    .Append(BASE_USER_AGENT)
                    .Replace("{version}", Application.ProductVersion)
                    .Replace("{os}", Environment.OSVersion.ToString().Replace(" ", "-"))
                    .Replace("{architecture}", Environment.Is64BitOperatingSystem ? "x64" : "x86"));


                return _userAgent;
            }
        }

        public static bool ContainsAppName(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str));

            return str.Contains(APP_NAME_DASH, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ContainsUserAgent(HttpHeaderValueCollection<ProductInfoHeaderValue> headers)
        {
            if (headers is null)
                throw new ArgumentNullException(nameof(headers));

            foreach (var header in headers)
                if (ContainsAppName((header?.Product?.Name ?? string.Empty)))
                    return true;
            

            return false;
        }

    }
}

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CoDUO_FoV_Changer
{
    internal class ServerUtil
    {
        private static readonly Regex _caratRegex = new Regex(@"\^+(\d{1,2})", RegexOptions.Compiled); //new Regex(@"\^+(\d)", RegexOptions.Compiled); //new Regex(@"\^\d{1}(?:\^\d{1})?", RegexOptions.Compiled);

        public static string GetIpAndPort(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            var sb = StringBuilderCache.Acquire(21);

            sb
                .Append(server.Ip)
                .Append(":")
                .Append(server.Port);

            return StringBuilderCache.GetStringAndRelease(sb);

        }

        public static GameConfig.GameType GetGameType(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            switch (server.Game)
            {
                case "cod":
                    return GameConfig.GameType.CoDMP;
                    case "coduo":
                    return GameConfig.GameType.CoDUOMP;
                default:
                    return (GameConfig.GameType)(-1);
            }
        }
        
        public static string GetPlayersString(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            var sb = StringBuilderCache.Acquire(7);

            sb
                .Append(server.Clients)
                .Append("/")
                .Append(server.MaxClients);

            return StringBuilderCache.GetStringAndRelease(sb);
        }

        public static string GetFilteredHostname(string hostname)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                return hostname;


            return _caratRegex.Replace(hostname, string.Empty);
        }

        public static string FilterCaratColors(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            return _caratRegex.Replace(str, string.Empty);
        }

        public static string GetPrettyMapName(string mapName)
        {
            if (string.IsNullOrWhiteSpace(mapName))
                return mapName;

            // Yes, we filter twice. Sometimes people use carats like ^^22.

            mapName = FilterCaratColors(FilterCaratColors(mapName));

            var sb = StringBuilderCache.Acquire(mapName.Length);

            sb
                .Append(mapName)
                .Replace("mp_", string.Empty);

            if (sb.Length != sb.Replace("uo_", string.Empty).Length)
                sb.Append(" (UO)");

            return FirstUpper(StringBuilderCache.GetStringAndRelease(sb));
        }

        private static string FirstUpper(string original)
        {
            if (string.IsNullOrWhiteSpace(original))
                return string.Empty;

            var array = original.ToCharArray();
            array[0] = char.ToUpper(array[0], CultureInfo.CurrentCulture);
            return new string(array);
        }

    }
}

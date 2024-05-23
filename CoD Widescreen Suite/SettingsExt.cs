using System;

namespace CoD_Widescreen_Suite
{
    internal class SettingsExt
    {
        public static bool IsFavoriteServer(string ipAndPort)
        {
            if (string.IsNullOrWhiteSpace(ipAndPort))
                throw new ArgumentNullException(nameof(ipAndPort));

            return Settings.Instance?.FavoriteServers?.Contains(ipAndPort) ?? false;

        }

        public static bool IsFavoriteServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            return IsFavoriteServer(ServerUtil.GetIpAndPort(server));
        }

        public static bool AddFavoriteServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            return AddFavoriteServer(ServerUtil.GetIpAndPort(server));
        }

        public static bool RemoveFavoriteServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            return RemoveFavoriteServer(ServerUtil.GetIpAndPort(server));
        }

        public static bool AddFavoriteServer(string ipAndPort)
        {
            if (string.IsNullOrWhiteSpace(ipAndPort))
                throw new ArgumentNullException(nameof(ipAndPort));

            if (IsFavoriteServer(ipAndPort))
                return false;

            Settings.Instance.FavoriteServers.Add(ipAndPort);

            return true;
        }

        public static bool RemoveFavoriteServer(string ipAndPort)
        {
            if (string.IsNullOrWhiteSpace(ipAndPort))
                throw new ArgumentNullException(nameof(ipAndPort));

            return Settings.Instance.FavoriteServers.Remove(ipAndPort);
        }

    }
}

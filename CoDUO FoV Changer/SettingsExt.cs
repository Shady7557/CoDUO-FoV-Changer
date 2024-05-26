using System;

namespace CoDUO_FoV_Changer
{
    internal class SettingsExt
    {

        public static FavoriteServer GetFavoriteServer(string ip, int port)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentNullException(nameof(ip));

            if (port < 0)
                throw new ArgumentOutOfRangeException(nameof(port));

            if (Settings.Instance?.FavoriteServers is null)
                return null;

            for (int i = 0; i < Settings.Instance.FavoriteServers.Count; i++)
            {
                var server = Settings.Instance.FavoriteServers[i];
                if (server.IpAddress.Equals(ip, StringComparison.OrdinalIgnoreCase) && server.Port == port)
                    return server;
            }
            
            return null;
        }

        public static FavoriteServer GetFavoriteServer(string ipAndPort)
        {
            if (string.IsNullOrWhiteSpace(ipAndPort))
                throw new ArgumentNullException(nameof(ipAndPort));

            if (Settings.Instance?.FavoriteServers is null)
                return null;

            for (int i = 0; i < Settings.Instance.FavoriteServers.Count; i++)
            {
                var server = Settings.Instance.FavoriteServers[i];
                if (server.IpAndPort.Equals(ipAndPort, StringComparison.OrdinalIgnoreCase))
                    return server;
            }

            return null;
        }

        public static bool IsFavoriteServer(string ipAndPort)
        {
            if (string.IsNullOrWhiteSpace(ipAndPort))
                throw new ArgumentNullException(nameof(ipAndPort));

            var servers = Settings.Instance?.FavoriteServers;
            if (servers is null)
                return false;

            for (int i = 0; i < servers.Count; i++)
            {
                if (servers[i].IpAndPort.Equals(ipAndPort, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public static bool IsFavoriteServer(string ip, int port)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentNullException(nameof(ip));

            if (port < 0)
                throw new ArgumentOutOfRangeException(nameof(port));

            var servers = Settings.Instance?.FavoriteServers;
            if (servers is null)
                return false;

            for (int i = 0; i < servers.Count; i++)
            {
                var server = servers[i];
                if (server.IpAddress.Equals(ip, StringComparison.OrdinalIgnoreCase) && server.Port == port)
                    return true;
            }

            return false;
        }   

        public static bool IsFavoriteServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            return IsFavoriteServer(ServerUtil.GetIpAndPort(server));
        }


        public static bool AddFavoriteServer(FavoriteServer favoriteServer)
        {
            if (favoriteServer is null)
                throw new ArgumentNullException(nameof(favoriteServer));

            if (IsFavoriteServer(favoriteServer.IpAddress, favoriteServer.Port))
                return false;

            Settings.Instance.FavoriteServers.Add(favoriteServer);
            return true;
        }

        public static bool AddFavoriteServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            if (IsFavoriteServer(server))
                return true;

            var newFavorite = new FavoriteServer(server.Ip, server.Port, ServerUtil.GetFilteredHostname(server.Hostname), ServerUtil.GetGameType(server));

            return AddFavoriteServer(newFavorite);
        }

        public static bool RemoveFavoriteServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            return RemoveFavoriteServer(server.Ip, server.Port);
        }

        public static bool AddFavoriteServer(string ipAddress, int port)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentNullException(nameof(ipAddress));

            if (port < 0)
                throw new ArgumentOutOfRangeException(nameof(port));

            if (IsFavoriteServer(ipAddress, port))
                return false;

            Settings.Instance.FavoriteServers.Add(new FavoriteServer(ipAddress, port));

            return true;
        }

        public static bool RemoveFavoriteServer(string ipAndPort)
        {
            if (string.IsNullOrWhiteSpace(ipAndPort))
                throw new ArgumentNullException(nameof(ipAndPort));

            var favorite = GetFavoriteServer(ipAndPort);

            return favorite != null && Settings.Instance.FavoriteServers.Remove(favorite);
        }

        public static bool RemoveFavoriteServer(string ip, int port)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentNullException(nameof(ip));

            if (port < 0)
                throw new ArgumentOutOfRangeException(nameof(port));

            var favorite = GetFavoriteServer(ip, port);

            return favorite != null && Settings.Instance.FavoriteServers.Remove(favorite);
        }

        public static bool RemoveFavoriteServer(FavoriteServer favoriteServer)
        {
            if (favoriteServer is null)
                throw new ArgumentNullException(nameof(favoriteServer));

            return Settings.Instance.FavoriteServers.Remove(favoriteServer);
        }

    }
}

﻿using CoDUO_FoV_Changer.Util;
using Newtonsoft.Json;
namespace CoDUO_FoV_Changer
{
    internal class FavoriteServer
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public string Hostname { get; set; } = string.Empty;
        public GameConfig.GameType GameType { get; set; }

        [JsonIgnore]
        public string IpAndPort => 
                     StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(21)
                    .Append(IpAddress)
                    .Append(":")
                    .Append(Port));
            
        

        public FavoriteServer() { }
        public FavoriteServer(string ipAddress, int port)
        {
            IpAddress = ipAddress;
            Port = port;
        }

        public FavoriteServer(string ipAddress, int port, string hostname, GameConfig.GameType gameType)
        {
            IpAddress = ipAddress;
            Port = port;
            Hostname = hostname;
            GameType = gameType;
        }

    }
}

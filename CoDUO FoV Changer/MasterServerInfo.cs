using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoDUO_FoV_Changer
{
    public class MasterServerInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("time_retrieved")]
        public long TimeRetrieved { get; set; }

        [JsonProperty("masterlist_updated")]
        public long MasterlistUpdated { get; set; }

        [JsonProperty("servers")]
        public List<Server> Servers { get; set; }

        [JsonProperty("error")]
        public int Error { get; set; }
    }

    public class ServerInfo
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("time_retrieved")]
        public long TimeRetrieved { get; set; }

        [JsonProperty("masterlist_updated")]
        public long MasterlistUpdated { get; set; }

        [JsonProperty("serverinfo")]
        public Server Server { get; set; }

        [JsonProperty("playerinfo")]
        public List<PlayerInfo> PlayerInfo { get; set; }

        [JsonProperty("error")]
        public int Error { get; set; }
    }

    public class SingleServer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("added")]
        public string Added { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("teamspeak")]
        public string Teamspeak { get; set; }

        [JsonProperty("discord")]
        public string Discord { get; set; }

        [JsonProperty("telegram")]
        public string Telegram { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("gameversion")]
        public string GameVersion { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("clients")]
        public int Clients { get; set; }

        [JsonProperty("bots")]
        public int Bots { get; set; }

        [JsonProperty("g_gametype")]
        public string GameType { get; set; }

        [JsonProperty("mapname")]
        public string MapName { get; set; }

        [JsonProperty("shortversion")]
        public string ShortVersion { get; set; }

        [JsonProperty("protocol")]
        public int Protocol { get; set; }

        [JsonProperty("sv_hostname")]
        public string Hostname { get; set; }

        [JsonProperty("sv_maxclients")]
        public int MaxClients { get; set; }

        [JsonProperty("sv_maxping")]
        public int MaxPing { get; set; }

        [JsonProperty("sv_pure")]
        public int Pure { get; set; }

        [JsonProperty("pswrd")]
        public int Password { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("hidden")]
        public int Hidden { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("country_isocode")]
        public string CountryIsoCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }
    }

    public class Server
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("added")]
        public string Added { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("teamspeak")]
        public string Teamspeak { get; set; }

        [JsonProperty("discord")]
        public string Discord { get; set; }

        [JsonProperty("telegram")]
        public string Telegram { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("gameversion")]
        public string GameVersion { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("clients")]
        public int Clients { get; set; }

        [JsonProperty("bots")]
        public int Bots { get; set; }

        [JsonProperty("g_gametype")]
        public string GameType { get; set; }

        [JsonProperty("mapname")]
        public string MapName { get; set; }

        [JsonProperty("shortversion")]
        public string ShortVersion { get; set; }

        [JsonProperty("protocol")]
        public int Protocol { get; set; }

        [JsonProperty("sv_hostname")]
        public string Hostname { get; set; }

        [JsonProperty("sv_maxclients")]
        public int MaxClients { get; set; }

        [JsonProperty("sv_maxping")]
        public int MaxPing { get; set; }

        [JsonProperty("sv_pure")]
        public int Pure { get; set; }

        [JsonProperty("pswrd")]
        public int Password { get; set; }

        [JsonProperty("playerinfo")]
        public List<PlayerInfo> PlayerInfo { get; set; }

        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("hidden")]
        public int Hidden { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("country_isocode")]
        public string CountryIsoCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }
    }

    public class PlayerInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("ping")]
        public int Ping { get; set; }

    }

    public class PlayersLastSeen
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("players")]
        public int Players { get; set; }
    }
}

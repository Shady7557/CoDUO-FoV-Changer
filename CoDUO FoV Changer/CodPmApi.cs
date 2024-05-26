using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoDUO_FoV_Changer
{
    internal class CodPmApi
    {
        private const string API_COD_PM_URL = "https://api.cod.pm";
        private const string API_COD_PM_MASTERLIST = API_COD_PM_URL + "/masterlist/{master}/{version}";
        private const string API_COD_PM_SNAPSHOT = API_COD_PM_URL + "/snapshot/{ip}/{port}";


        private static readonly HttpClient _httpClient = new HttpClient();


        public static async Task<MasterServerInfo> GetMasterList(string master, string version)
        {
            if (string.IsNullOrWhiteSpace(master))
                throw new ArgumentNullException(nameof(master));

            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));

            var response = await _httpClient.GetAsync(API_COD_PM_MASTERLIST.Replace("{master}", master).Replace("{version}", version));
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response?.Content?.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonResponse))
                throw new Exception(nameof(jsonResponse) + " was null");

            var serverInfo = JsonConvert.DeserializeObject<MasterServerInfo>(jsonResponse);

            return serverInfo;
        }

        public static async Task<ServerInfo> GetServer(string ip, int port)
        {
            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentNullException(nameof(ip));

            if (port < 0)
                throw new ArgumentOutOfRangeException(nameof(port));


            var response = await _httpClient.GetAsync(API_COD_PM_SNAPSHOT.Replace("{ip}", ip).Replace("{port}", port.ToString()));
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response?.Content?.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonResponse))
                throw new Exception(nameof(jsonResponse) + " was null");

            var server = JsonConvert.DeserializeObject<ServerInfo>(jsonResponse);

            Console.WriteLine(JsonConvert.SerializeObject(server, Formatting.Indented));

            return server;
        }

       


    }
}

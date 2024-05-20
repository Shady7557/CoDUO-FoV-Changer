using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoD_Widescreen_Suite
{
    internal class CodPmApi
    {
        private const string API_COD_PM_URL = "https://api.cod.pm";
        private const string API_COD_PM_MASTERLIST = API_COD_PM_URL + "/masterlist/{master}/{version}";
        private const string API_COD_PM_SNAPSHOT = API_COD_PM_URL + "/snapshot/{ip}/{port}";

        private static readonly Regex _caratRegex = new Regex("\\^+(\\d)", RegexOptions.Compiled);

        private static readonly HttpClient _httpClient = new HttpClient();


        public static async Task<ServerInfo> GetMasterList(string master, string version)
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

            var serverInfo = JsonConvert.DeserializeObject<ServerInfo>(jsonResponse);

            return serverInfo;
        }

        public static string GetFilteredHostname(string hostname)
        {
            if (string.IsNullOrWhiteSpace(hostname))
                return hostname;


            return _caratRegex.Replace(hostname, string.Empty);
        }

        public static string GetPrettyMapName(string mapName)
        {
            if (string.IsNullOrWhiteSpace(mapName))
                return mapName;

            // use SB later on:

            // Needs to handle maps like mp_uo_carentan so that it becomes Carentan (UO).
            // Differentiated from mp_carentan which would just be Carentan.



            return FirstUpper(_caratRegex.Replace(mapName.Replace("mp_", string.Empty), string.Empty));
        }

        private static string FirstUpper(string original)
        {
            if (string.IsNullOrEmpty(original)) return string.Empty;
            var array = original.ToCharArray();
            array[0] = char.ToUpper(array[0], CultureInfo.CurrentCulture);
            return new string(array);
        }


    }
}

using CoDUO_FoV_Changer.Util;
using CurtLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoDUO_FoV_Changer
{
    internal class CodMapImage
    {
       

        private const string COD_PM_STOCK_MAP_IMAGE_URL = "https://cod.pm/mp_maps/cod1+coduo/stock/{map}.png";
        private const string COD_PM_CUSTOM_MAP_IMAGE_URL = "https://cod.pm/mp_maps/cod1+coduo/custom/{map}.png";

        private static readonly Dictionary<string, Image> _mapNameToImage = new Dictionary<string, Image>();
        private static readonly HashSet<string> _mapsWithoutImages = new HashSet<string>();

        private static readonly string _noImgCachePath = Path.Combine(PathInfos.CachePath, "noimgcache.json");

        private static readonly HashSet<string> _stockMapNames = new HashSet<string>()
{
    "mp_arnhem",
    "mp_berlin",
    "mp_bocage",
    "mp_brecourt",
    "mp_carentan",
    "mp_uo_carentan",
    "mp_cassino",
    "mp_chateau",
    "mp_dawnville",
    "mp_uo_dawnville",
    "mp_depot",
    "mp_uo_depot",
    "mp_foy",
    "mp_harbor",
    "mp_uo_harbor",
    "mp_hurtgen",
    "mp_uo_hurtgen",
    "mp_italy",
    "mp_kharkov",
    "mp_kursk",
    "mp_neuville",
    "mp_pavlov",
    "mp_peaks",
    "mp_ponyri",
    "mp_powcamp",
    "mp_uo_powcamp",
    "mp_railyard",
    "mp_rhinevalley",
    "mp_rocket",
    "mp_ship",
    "mp_sicily",
    "mp_stalingrad",
    "mp_streets",
    "mp_tigertown",
    "mp_uo_stanjel"
};
        private static readonly HttpClient _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(5) };


        // A class for serializing/deserializing into JSON.
        // This will be stored in the cache directory, it will have
        // A map name and timestamp. If it hasn't been at least a day since the last attempt to grab the image for this map,
        // We won't check it.
        private class NoImageCache
        {
            public Dictionary<string, DateTime> LastMapAttempt { get; set; } = new Dictionary<string, DateTime>();

            public NoImageCache() { }

            public DateTime GetLastAttempt(string mapName) => LastMapAttempt.TryGetValue(mapName, out var dt) ? dt : DateTime.MinValue;

            public bool ExistsAndIsUnexpired(string mapName) => LastMapAttempt.TryGetValue(mapName, out var dt) && (DateTime.UtcNow - dt).TotalDays < 1;

            public void SetLastAttempt(string mapName) => LastMapAttempt[mapName] = DateTime.UtcNow;
        }


        private static NoImageCache NoImgCache { get; set; }

        private static void LoadNoImgCache()
        {

            if (!File.Exists(_noImgCachePath))
            {
                NoImgCache = new NoImageCache();
                return;
            }

            try
            {
                var txt = File.ReadAllText(_noImgCachePath);

                if (string.IsNullOrWhiteSpace(txt))
                {
                    NoImgCache = new NoImageCache();
                    return;
                }

                NoImgCache = JsonConvert.DeserializeObject<NoImageCache>(txt);
            }
            catch(Exception ex) 
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            if (NoImgCache is null)
                NoImgCache = new NoImageCache();

        }

        public static void SaveNoImgCacheToDisk()
        {
            if (NoImgCache is null)
                return;

            // Ensure cache directory exists.
            new DirectoryInfo(Path.GetDirectoryName(_noImgCachePath)).Create();

            File.WriteAllText(_noImgCachePath, JsonConvert.SerializeObject(NoImgCache));
        }

        public static async Task<Image> GetImageAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            try
            {
                // Set the User-Agent header if necessary

                var hasHeader = UserAgent.ContainsUserAgent(_httpClient.DefaultRequestHeaders.UserAgent);

                if (!hasHeader)
                    _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent.UserAgentString);

                var getRequest = await _httpClient.GetAsync(url);

                if (!getRequest.IsSuccessStatusCode)
                    return null;

                // Download the image data
                var imageData = await getRequest.Content.ReadAsByteArrayAsync();

                // Convert the byte array to an Image
                using (var ms = new MemoryStream(imageData))
                    return Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public static string GetMapImageURL(string mapName, bool stock = true)
        {
            if (string.IsNullOrWhiteSpace(mapName))
                return string.Empty;

            var baseUrl = stock ? COD_PM_STOCK_MAP_IMAGE_URL : COD_PM_CUSTOM_MAP_IMAGE_URL;

            var sb = StringBuilderCache.Acquire(72);
            sb
                .Append(baseUrl)
                .Replace("{map}", mapName.ToLower());

            return StringBuilderCache.GetStringAndRelease(sb);
        }

        public static async Task<Image> GetMapImage(string mapName)
        {
            if (string.IsNullOrWhiteSpace(mapName))
                throw new ArgumentNullException(nameof(mapName));

            mapName = mapName.ToLower();

            if (_mapsWithoutImages.Contains(mapName))
                return null;

            if (NoImgCache is null)
                LoadNoImgCache();


            if (NoImgCache.ExistsAndIsUnexpired(mapName))
            {
                _mapsWithoutImages.Add(mapName);
                return null;
            }

            // First, try to read the image from memory:

            if (_mapNameToImage.TryGetValue(mapName, out var image))
                return image;

            // Now, check if it's cached on the disk:

            var imgPath = Path.Combine(PathInfos.CachePath, StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(mapName.Length + 4).Append(mapName).Append(".png")));

            var fileExistedAndValid = false;

            if (File.Exists(imgPath))
            {
                // Read file and parse it into an image, then store it in memory:

                try
                {
                    using (var fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                        image = Image.FromStream(fs);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }

                fileExistedAndValid = image != null;
            }

            if (image is null)
            {
                // File did not exist on disk or was not a valid image file, so now we'll attempt to download it.


                var filteredMapName = StringBuilderCache.GetStringAndRelease(
                    StringBuilderCache.Acquire(mapName.Length)
                    .Append(mapName)
                    .Replace("_uo", string.Empty)
                    .Replace("uo_", "mp_")
                    .Replace("ctf_", "mp_")
                    .Replace("_ctf", string.Empty));

                var isStock = _stockMapNames.Contains(mapName) || _stockMapNames.Contains(filteredMapName);

                image = await GetImageAsync(GetMapImageURL(filteredMapName, isStock))
                    ?? await GetImageAsync(GetMapImageURL(mapName, isStock));

                // Was "stock", but image is still null. Try again without isStock true.
                if (image is null && isStock)
                {
                      image = await GetImageAsync(GetMapImageURL(mapName, false))
                        ?? await GetImageAsync(GetMapImageURL(filteredMapName, false));
                }
                  
            }


            if (image != null)
            {
                _mapNameToImage[mapName] = image;

                // Save the image to disk:

                if (!fileExistedAndValid)
                    try 
                    {

                        byte[] imageBytes;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Png);
                            imageBytes = ms.ToArray();
                        }

                        // Ensure cache directory exists.
                        new DirectoryInfo(Path.GetDirectoryName(imgPath)).Create();

                        // Write byte array to file
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                            fs.Write(imageBytes, 0, imageBytes.Length);
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Log.WriteLine(ex.ToString());
                    }
                

                return image;
            }

            // Couldn't find an image. Add it to the HashSet so we know not to check again.

            _mapsWithoutImages.Add(mapName);
            NoImgCache.SetLastAttempt(mapName);

            return null;
        }

    }
}

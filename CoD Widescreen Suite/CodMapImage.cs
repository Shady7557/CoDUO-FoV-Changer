using CurtLog;
using Newtonsoft.Json;
using ShadyPool;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoD_Widescreen_Suite
{
    internal class CodMapImage
    {
        private const string COD_PM_STOCK_MAP_IMAGE_URL = "https://cod.pm/mp_maps/cod1+coduo/stock/{map}.png";
        private const string COD_PM_CUSTOM_MAP_IMAGE_URL = "https://cod.pm/mp_maps/cod1+coduo/custom/{map}.png";
        private static readonly Dictionary<string, Image> _mapNameToImage = new Dictionary<string, Image>();
        private static readonly HashSet<string> _mapsWithoutImages = new HashSet<string>();

        private static readonly string _noImgCachePath = Path.Combine(PathInfos.CachePath, "noimgcache.json");

        // A class for serializing/deserializing into JSON.
        // This will be stored in the cache directory, it will have
        // A map name and timestamp. If it hasn't been at least a day since the last attempt to grab the image for this map,
        // We won't check it.
        private class NoImageCache
        {

            public Dictionary<string, DateTime> LastMapAttempt { get; set; } = new Dictionary<string, DateTime>();

            public NoImageCache() { }

            public DateTime GetLastAttempt(string mapName)
            {
                if (LastMapAttempt.TryGetValue(mapName, out var dt))
                    return dt;

                return DateTime.MinValue;
            }

            public bool ExistsAndIsUnexpired(string mapName)
            {
                return LastMapAttempt.TryGetValue(mapName, out var dt) && (DateTime.UtcNow - dt).TotalDays < 1;
            }

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

            var txt = File.ReadAllText(_noImgCachePath);

            if (string.IsNullOrWhiteSpace(txt))
            {
                NoImgCache = new NoImageCache();
                return;
            }

            try { NoImgCache = JsonConvert.DeserializeObject<NoImageCache>(txt); }
            catch(Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            if (NoImgCache is null)
                NoImgCache = new NoImageCache();

           // if (!File.Exists())
        }

        public static void SaveNoImgCacheToDisk() => File.WriteAllText(_noImgCachePath, JsonConvert.SerializeObject(NoImgCache, Formatting.Indented));
        

        public static async Task<Image> GetImageAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            var sb = Pool.Get<StringBuilder>();

            try
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        // Set the User-Agent header

                        client.DefaultRequestHeaders.UserAgent.ParseAdd(sb
                            .Clear()
                            .Append("CoDUO FoV Changer/")
                            .Append(Application.ProductVersion)
                            .ToString());
                        

                        // Download the image data
                        var imageData = await client.GetByteArrayAsync(url);

                        // Convert the byte array to an Image
                        using (var ms = new MemoryStream(imageData))
                            return Image.FromStream(ms);
                        
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    Console.WriteLine(ex.ToString());
                }
            }
            finally { Pool.Free(ref sb); }

            return null;
        }

        public static string GetMapImageURL(string mapName, bool stock = true) => (stock ? COD_PM_STOCK_MAP_IMAGE_URL : COD_PM_CUSTOM_MAP_IMAGE_URL).Replace("{map}", mapName.ToLower());
        

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
                return null;

            // First, try to read the image from memory:

            if (_mapNameToImage.TryGetValue(mapName, out var image))
                return image;

            // Now, check if it's cached on the disk:

            var imgPath = Path.Combine(PathInfos.CachePath, mapName + ".png");

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

                image = await GetImageAsync(GetMapImageURL(mapName))
                     ?? await GetImageAsync(GetMapImageURL(mapName.Replace("uo_", string.Empty)))
                     ?? await GetImageAsync(GetMapImageURL(mapName, false));

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

                        // Write byte array to file
                        new DirectoryInfo(Path.GetDirectoryName(imgPath)).Create();
                        using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(imageBytes, 0, imageBytes.Length);
                        }

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

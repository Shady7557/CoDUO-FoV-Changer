using CurtLog;
using ShadyPool;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CoD_Widescreen_Suite
{
    internal class GameConfig
    {

        public static Regex ConfigRegex { get; private set; } = new Regex(@"(?=\+)", RegexOptions.Compiled);

        // This can perhaps be moved elsewhere if it is useful for another part of the codebase at some point:

        public enum GameType
        {
            CoDSP,
            CoDMP,
            CoDUOSP,
            CoDUOMP
        }

        public static string GetConfigFileName(GameType gameType)
        {
            string configName;

            switch (gameType)
            {
                case GameType.CoDSP:
                    configName = "config.cfg";
                    break;
                case GameType.CoDMP:
                    configName = "config_mp.cfg";
                    break;
                case GameType.CoDUOSP:
                    configName = "uoconfig.cfg";
                    break;
                case GameType.CoDUOMP:
                    configName = "uoconfig_mp.cfg";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameType), gameType, null);
            }

            return configName;
        }

        public static string GetConfigDirectory(GameType gameType) => (gameType == GameType.CoDSP || gameType == GameType.CoDMP) ? "main" : "uo";
        
        public static string GetConfigPath(GameType gameType) => Path.Combine(Settings.Instance.InstallPath, GetConfigDirectory(gameType), GetConfigFileName(gameType));

        /// <summary>
        /// Reads config file from disk and returns as string.
        /// </summary>
        /// <param name="vCod"></param>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        public static string GetGameConfig(GameType gameType)
        {
            var cfgPath = GetConfigPath(gameType);

            var cfgFileInfo = new FileInfo(cfgPath);
            if (!cfgFileInfo.Exists)
            {
                Log.WriteLine("could not find cfgPath: " + cfgPath);
                throw new FileNotFoundException(cfgPath);
            }

            var sizeInBytes = cfgFileInfo.Length;

            if (sizeInBytes > 1024000)
            {
                Log.WriteLine("cfg file is too large to modify (bytes): " + sizeInBytes);
                throw new IOException($"{cfgPath} is too large ({sizeInBytes})");
            }

            return File.ReadAllText(cfgPath);
        }

        public static string[] GetGameConfigByLines(GameType gameType)
        {
            var cfgPath = GetConfigPath(gameType);

            var cfgFileInfo = new FileInfo(cfgPath);
            if (!cfgFileInfo.Exists)
            {
                Log.WriteLine("could not find cfgPath: " + cfgPath);
                throw new FileNotFoundException(cfgPath);
            }

            var sizeInBytes = cfgFileInfo.Length;

            if (sizeInBytes > 1024000)
            {
                Log.WriteLine("cfg file is too large to modify (bytes): " + sizeInBytes);
                throw new IOException($"{cfgPath} is too large ({sizeInBytes})");
            }

            return File.ReadAllLines(cfgPath);
        }

        /// <summary>
        /// Writes config file to disk.
        /// </summary>
        /// <param name="cfgContents"></param>
        /// <param name="vCod"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static void WriteGameConfig(string cfgContents, GameType gameType)
        {
            var installPath = Settings.Instance.InstallPath;

            if (!Directory.Exists(installPath))
                throw new DirectoryNotFoundException(installPath);

            File.WriteAllText(GetConfigPath(gameType), cfgContents);
        }

        /// <summary>
        /// Updates a specific value inside the game config.
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="configValue"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateConfigValue(string configKey, string configValue, GameType gameType)
        {
            if (string.IsNullOrWhiteSpace(configKey))
                throw new ArgumentNullException(nameof(configKey));

            if (string.IsNullOrWhiteSpace(configValue))
                throw new ArgumentNullException(nameof(configValue));

            var cfgLines = GetGameConfigByLines(gameType);

            var sb = Pool.Get<StringBuilder>();
            try
            {
                if (!configValue.StartsWith("\"") && !configValue.EndsWith("\""))
                    configValue = sb.Clear().Append("\"").Append(configValue).Append("\"").ToString();

                var toMatch = sb.Clear().Append("seta ").Append(configKey).ToString();

                sb.Clear();

                for (int i = 0; i < cfgLines.Length; i++)
                {
                    var line = cfgLines[i];

                    if (line.StartsWith(toMatch))
                        sb
                            .Append(toMatch)
                            .Append(" ")
                            .Append(configValue)
                            .Append(Environment.NewLine);
                    else sb
                            .Append(line)
                            .Append(Environment.NewLine);
                }

                if (sb.Length > 1)
                    sb.Length--; // trim newline.

                WriteGameConfig(sb.ToString(), gameType);

            }
            finally { Pool.Free(ref sb); }

        }

        /// <summary>
        /// Writes launch parameters to config file.
        /// </summary>
        /// <param name="paramString"></param>
        /// <param name="gameType"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ApplyLaunchParametersToConfig(string paramString, GameType gameType)
        {
            if (string.IsNullOrWhiteSpace(paramString))
                throw new ArgumentNullException(nameof(paramString));

            var cfgReadText = GetGameConfig(gameType);

            if (string.IsNullOrWhiteSpace(cfgReadText))
                throw new ArgumentNullException(nameof(cfgReadText));


            var sb = Pool.Get<StringBuilder>();

            try
            {
                sb.Clear().Append(cfgReadText);

                var argsStr = paramString;

                var splitArguments = ConfigRegex.Split(argsStr);

                for (int i = 0; i < splitArguments.Length; i++)
                    sb.Append(splitArguments[i]).Replace("+set", "seta").Append(Environment.NewLine);


                if (splitArguments.Length > 0)
                    sb.Length--;

                try { WriteGameConfig(sb.ToString(), gameType); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }

            }
            finally { Pool.Free(ref sb); }
        }

    }
}

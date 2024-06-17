using CurtLog;
using ShadyPool;
using StringExtension;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace CoDUO_FoV_Changer
{
    internal class GameConfig
    {

        public static Regex ConfigRegex { get; } = new Regex(@"(?=\+)", RegexOptions.Compiled);

        // This can perhaps be moved elsewhere if it is useful for another part of the codebase at some point:

        public enum GameType
        {
            CoDSP,
            CoDMP,
            CoDUOSP,
            CoDUOMP
        }

        public static bool ConfigFileExists(GameType gameType) => File.Exists(GetConfigPath(gameType));

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
        
        public static string GetConfigPath(GameType gameType) => Path.Combine(Settings.Instance.BaseGamePath, GetConfigDirectory(gameType), GetConfigFileName(gameType));

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
        public static void WriteGameConfig(string installPath, string cfgContents, GameType gameType)
        {
            if (string.IsNullOrWhiteSpace(installPath))
                throw new ArgumentNullException(nameof(installPath));

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

                WriteGameConfig(Settings.Instance.BaseGamePath, sb.ToString(), gameType);

            }
            finally { Pool.Free(ref sb); }

        }

        /// <summary>
        /// Grabs a specific value from the game config.
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="gameType"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetConfigValue(string configKey, GameType gameType)
        {
            if (string.IsNullOrWhiteSpace(configKey))
                throw new ArgumentNullException(nameof(configKey));

            var cfgLines = GetGameConfigByLines(gameType);

            var sb = Pool.Get<StringBuilder>();
            try
            {
                var toMatch = sb.Clear().Append("seta ").Append(configKey).ToString();

                for (int i = 0; i < cfgLines.Length; i++)
                {
                    var line = cfgLines[i];

                    if (!line.StartsWith(toMatch))
                        continue;

                    var split = line.Split(new string[] { "cg_fov " }, StringSplitOptions.RemoveEmptyEntries);

                    if (split.Length > 1)
                    {
                        var value = split[1];
                        return sb.Clear().Append(value).Replace("\"", string.Empty).ToString();
                    }
                }

            }
            finally { Pool.Free(ref sb); }

            return string.Empty;
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

                try { WriteGameConfig(Settings.Instance.BaseGamePath, sb.ToString(), gameType); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }

            }
            finally { Pool.Free(ref sb); }
        }

        public static GameType GetGameTypeFromProcess(Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));              

            var gameType = (GameType)(-1);

            // Switch process name, check proc names, set gameType:
            
            switch (process.ProcessName)
            {
                case "CoDMP":
                    gameType = GameType.CoDMP;
                    break;
                    case "CoDSP":
                    gameType = GameType.CoDSP;
                    break;
                    case "CoDUOMP":
                    gameType = GameType.CoDUOMP;
                    break;
                    case "CoDUOSP":
                    gameType = GameType.CoDUOSP;
                    break;
            }

            // Couldn't determine the type of game from the process name, so it's likely mohaa.exe.
            // Now, we must try and read modules.
            if (gameType == (GameType)(-1))
            {
                // This is REALLY bad. Don't do it this way. This is temporary.
                // We need to change where the selected memory is stored. That's the only bad part.

                var memory = ExtendedForm.GetInstance<MainForm>().MemorySelection;

                if (memory != null)
                {
                    var isUOMP = memory.ProcMemory?.DllImageAddress(MemoryAddresses.UO_UI_MP_DLL) != 0 || memory.ProcMemory?.DllImageAddress(MemoryAddresses.UO_CGAME_MP_DLL) != 0;
                    var isUOSP = memory.ProcMemory?.DllImageAddress(MemoryAddresses.UO_UI_DLL) != 0 && !isUOMP;

                    var isCoDMP = memory.ProcMemory?.DllImageAddress(MemoryAddresses.COD_UI_MP_DLL) != 0 || memory.ProcMemory?.DllImageAddress(MemoryAddresses.COD_CGAME_MP_DLL) != 0;
                    var isCoDSP = memory.ProcMemory?.DllImageAddress(MemoryAddresses.COD_UI_DLL) != 0 && !isCoDMP;


                    if (isUOMP)
                        gameType = GameType.CoDUOMP;
                    else if (isUOSP)
                        gameType = GameType.CoDUOSP;
                    else if (isCoDMP)
                        gameType = GameType.CoDMP;
                    else if (isCoDSP)
                        gameType = GameType.CoDSP;
                }

            }

            return gameType;

        }

        public static bool ShouldUseSteam(string exePath)
        {
            if (string.IsNullOrWhiteSpace(exePath))
                throw new ArgumentNullException(nameof(exePath));

            return exePath.Contains("steamapps", StringComparison.OrdinalIgnoreCase);
        }

    }
}

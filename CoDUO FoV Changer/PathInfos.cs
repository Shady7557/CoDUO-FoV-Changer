using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class PathInfos
{
    public const string CODMP_PROCESS_NAME = "CoDMP";
    public const string CODSP_PROCESS_NAME = "CoDSP";
    public const string CODUOMP_PROCESS_NAME = "CoDUOMP";
    public const string CODUOSP_PROCESS_NAME = "CoDUOSP";
    public const string MOHAA_PROCESS_NAME = "mohaa";

    private static readonly StringBuilder _stringBuilder = new StringBuilder();

    public static string AppData => Environment.GetEnvironmentVariable("appdata");
    public static string Temp => Environment.GetEnvironmentVariable("temp");
    public static string AppTemp => Path.Combine(Temp, "CoDUO FoV Changer");
    public static string AppDataFoV => Path.Combine(AppData, "CoDUO FoV Changer");
    public static string LogsPath => Path.Combine(AppDataFoV, "Logs");
    public static string SettingsPath => Path.Combine(AppDataFoV, "settings.json");
    public static string CachePath => Path.Combine(AppTemp, "cache");

    public static HashSet<string> GAME_PROCESS_NAMES = new HashSet<string>(5) { CODSP_PROCESS_NAME, CODMP_PROCESS_NAME, CODUOSP_PROCESS_NAME, CODUOMP_PROCESS_NAME, MOHAA_PROCESS_NAME };

    public static HashSet<string> GetProcessNamesWithDotExe()
    {
        var names = new HashSet<string>(GAME_PROCESS_NAMES.Count);

        foreach(var name in GAME_PROCESS_NAMES)
        {
            names.Add(_stringBuilder.
                Clear()
                .Append(name)
                .Append(".exe")
                .ToString());
        }

        return names;
    }

}


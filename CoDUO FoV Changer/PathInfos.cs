using System;
using System.Collections.Generic;
using System.IO;

public class PathInfos
{
    public static string AppData => Environment.GetEnvironmentVariable("appdata");
    public static string Temp => Environment.GetEnvironmentVariable("temp");
    public static string AppTemp => Path.Combine(Temp, "CoDUO FoV Changer");
    public static string AppDataFoV => Path.Combine(AppData, "CoDUO FoV Changer");
    public static string LogsPath => Path.Combine(AppDataFoV, "Logs");
    public static string SettingsPath => Path.Combine(AppDataFoV, "settings.json");
    public static string CachePath => Path.Combine(AppTemp, "cache");


    public const string VCOD_PROCESS_NAME = "CoDMP";
    public const string CODUO_PROCESS_NAME = "CoDUOMP";
    public const string MOHAA_PROCESS_NAME = "mohaa";

    public static HashSet<string> GAME_PROCESS_NAMES = new HashSet<string>(3) { VCOD_PROCESS_NAME, CODUO_PROCESS_NAME, MOHAA_PROCESS_NAME };
}


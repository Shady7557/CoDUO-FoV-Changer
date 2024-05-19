using System;
using System.Collections.Generic;

public class PathInfos
{
    public static string AppData => Environment.GetEnvironmentVariable("appdata");
    public static string Temp => Environment.GetEnvironmentVariable("temp");
    public static string AppDataFoV => AppData + @"\CoD Widescreen Suite";
    public static string LogsPath => AppDataFoV + @"\Logs";
    public static string SettingsPath => AppDataFoV + @"\settings.json";


    public const string VCOD_PROCESS_NAME = "CoDMP";
    public const string CODUO_PROCESS_NAME = "CoDUOMP";
    public const string MOHAA_PROCESS_NAME = "mohaa";

    public static HashSet<string> GAME_PROCESS_NAMES = new HashSet<string>(3) { VCOD_PROCESS_NAME, CODUO_PROCESS_NAME, MOHAA_PROCESS_NAME };
}


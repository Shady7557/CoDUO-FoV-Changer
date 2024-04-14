using System;
using System.Collections.Generic;

public class PathInfos
{
    public static readonly string AppData = Environment.GetEnvironmentVariable("appdata");
    public static readonly string Temp = Environment.GetEnvironmentVariable("temp");
    public static readonly string AppDataFoV = AppData + @"\CoD Widescreen Suite";
    public static readonly string LogsPath = AppDataFoV + @"\Logs";
    public static readonly string SettingsPath = AppDataFoV + @"\settings.xml";

    public const string VCOD_PROCESS_NAME = "CoDMP";
    public const string CODUO_PROCESS_NAME = "CoDUOMP";
    public const string MOHAA_PROCESS_NAME = "mohaa";

    public static HashSet<string> GAME_PROCESS_NAMES = new HashSet<string>(3) { VCOD_PROCESS_NAME, CODUO_PROCESS_NAME, MOHAA_PROCESS_NAME };
}


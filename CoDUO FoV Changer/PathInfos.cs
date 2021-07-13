using System;

public class PathInfos
{
    public static readonly string AppData = Environment.GetEnvironmentVariable("appdata");
    public static readonly string Temp = Environment.GetEnvironmentVariable("temp");
    public static readonly string AppDataFoV = AppData + @"\CoDUO FoV Changer";
    public static readonly string LogsPath = AppDataFoV + @"\Logs";
    public static readonly string SettingsPath = AppDataFoV + @"\settings.xml";
}


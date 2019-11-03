using System;

public class PathInfos
{
    public static readonly string Appdata = Environment.GetEnvironmentVariable("appdata") + @"\";
    public static readonly string Temp = Environment.GetEnvironmentVariable("temp") + @"\";
    public static readonly string AppdataFoV = Appdata + "CoDUO FoV Changer";
    public static readonly string LogsPath = AppdataFoV + @"\Logs";
    public static readonly string SettingsPath = AppdataFoV + @"\settings.xml";
}


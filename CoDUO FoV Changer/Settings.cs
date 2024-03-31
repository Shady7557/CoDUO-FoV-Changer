using System;
using System.IO;

namespace CoDUO_FoV_Changer
{
    [Serializable]
    public sealed class Settings
    {
        [NonSerialized]
        private static readonly Settings instance;
        [NonSerialized]
        private static readonly string settingsFile;
        [NonSerialized]
        private bool settingsChanged;

        private string gamePath;
        private string gameExe;
        private decimal fov;
        private string cmdline;
        private bool minimize;
        private string lastlog;
        private bool trackTime;
        private double gameTime;
        private string hotkeyUp;
        private string hotkeyDown;
        private string hotkeyModifier;
        private string hotkeyToggleFog;
        private string hotkeyFogModifier;

        static Settings()
        {
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CoDUO FoV Changer";
            settingsFile = applicationDataDirectory + @"\settings.xml";
            if (!Directory.Exists(applicationDataDirectory)) Directory.CreateDirectory(applicationDataDirectory);
            if (File.Exists(settingsFile)) instance = DatabaseFile.Read<Settings>(settingsFile);
            else
            {
                instance = new Settings();
                DatabaseFile.Write(instance, settingsFile);
            }
        }

        public Settings()
        {
            HasChanged = false;
            gamePath = string.Empty;
            gameExe = string.Empty;
            fov = 80;
            cmdline = string.Empty;
            minimize = true;
            lastlog = string.Empty;
            trackTime = true;
            gameTime = 0;
            hotkeyUp = 107.ToString();
            hotkeyDown = 109.ToString();
            hotkeyModifier = 17.ToString();
            hotkeyFogModifier = 18.ToString();
            hotkeyToggleFog = 70.ToString();
        }




        public string InstallPathExe { get { return gamePath + @"\" + gameExe; } }

        public string InstallPath
        {
            get { return gamePath; }
            set { HasChanged = gamePath != value; gamePath = value; }
        }

        public string ExeName
        {
            get { return gameExe; }
            set { HasChanged = gameExe != value; gameExe = value; }
        }

        public string CommandLine
        {
            get { return cmdline; }
            set { HasChanged = cmdline != value; cmdline = value; }
        }

        public string LastLogFile
        {
            get { return lastlog; }
            set { HasChanged = lastlog != value; lastlog = value; }
        }

        public decimal FoV
        {
            get { return fov; }
            set { HasChanged = fov != value; fov = value; }
        }

        public double GameTime
        {
            get { return gameTime; }
            set { HasChanged = gameTime != value; gameTime = value; }
        }

        public bool MinimizeToTray
        {
            get { return minimize; }
            set { HasChanged = minimize != value; minimize = value; }
        }

        public bool TrackGameTime
        {
            get { return trackTime; }
            set { HasChanged = trackTime != value; trackTime = value; }
        }

        public string HotKeyUp
        {
            get { return hotkeyUp; }
            set { HasChanged = hotkeyUp != value; hotkeyUp = value; }
        }

        public string HotKeyDown
        {
            get { return hotkeyDown; }
            set { HasChanged = hotkeyDown != value; hotkeyDown = value; }
        }

        public string HotKeyModifier
        {
            get { return hotkeyModifier; }
            set { HasChanged = hotkeyModifier != value; hotkeyModifier = value; }
        }

        public string HotKeyFogModifier
        {
            get { return hotkeyFogModifier; }
            set { HasChanged = hotkeyFogModifier != value; hotkeyFogModifier = value; }
        }

        public string HotKeyFog
        {
            get { return hotkeyToggleFog; }
            set { HasChanged = hotkeyToggleFog != value; hotkeyToggleFog = value; }
        }

        public bool HasChanged
        {
            get { return settingsChanged; }
            set { settingsChanged = value; }
        }

        public static Settings Instance { get { return instance; } }
    }
}
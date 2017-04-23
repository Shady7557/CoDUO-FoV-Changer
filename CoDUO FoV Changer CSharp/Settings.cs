using System;
using System.IO;

namespace CoDUO_FoV_Changer_CSharp
{
    [Serializable]
    public sealed class Settings
    {
        [NonSerialized]
        private static readonly Settings instance;
        [NonSerialized]
        private static string settingsFile;
        [NonSerialized]
        private bool settingsChanged;

        private string gamePath;
        private decimal fov;
        private string cmdline;
        private bool minimize;
        private string lastlog;
        private bool fog;
        private bool trackTime;
        private int gameTime;
        private bool disableUpdateTimer;
        private string hotkeyUp;
        private string hotkeyDown;
        private string hotkeyModifier;
        private string hotkeyToggleFog;
        private string hotkeyFogModifier;

        static Settings()
        {
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CoDUO FoV Changer";
            settingsFile = applicationDataDirectory + @"\settings.xml";
            if (!Directory.Exists(applicationDataDirectory))
            {
                Directory.CreateDirectory(applicationDataDirectory);
            }
            if (File.Exists(settingsFile))
            {
                instance = DatabaseFile.Read<Settings>(settingsFile);
            }
            else
            {
                    instance = new Settings();
                    DatabaseFile.Write(instance, settingsFile);
            }
        }

        public Settings()
        {
            settingsChanged = false;
            gamePath = string.Empty;
            fov = 80;
            cmdline = string.Empty;
            minimize = true;
            lastlog = string.Empty;
            fog = true;
            trackTime = true;
            gameTime = 0;
            disableUpdateTimer = false;
            hotkeyUp = (107).ToString();
            hotkeyDown = (109).ToString();
            hotkeyModifier = (17).ToString();
            hotkeyFogModifier = (18).ToString();
            hotkeyToggleFog = (70).ToString();
        }

   
        public string InstallPath
        {
            get { return gamePath; }
            set
            {
                settingsChanged = true;
                gamePath = value;
            }
        }

        public string CommandLine
        {
            get { return cmdline; }
            set
            {
                settingsChanged = true;
                cmdline = value;
            }
        }

        public string LastLogFile
        {
            get { return lastlog; }
            set
            {
                settingsChanged = true;
                lastlog = value;
            }
        }

        public decimal FoV
        {
            get { return fov; }
            set
            {
                settingsChanged = true;
                fov = value;
            }
        }

        public int GameTime
        {
            get { return gameTime; }
            set
            {
                settingsChanged = true;
                gameTime = value;
            }
        }

        public bool MinimizeToTray
        {
            get { return minimize; }
            set
            {
                settingsChanged = true;
                minimize = value;
            }
        }

        public bool Fog
        {
            get { return fog; }
            set
            {
                settingsChanged = true;
                fog = value;
            }
        }

        public bool TrackGameTime
        {
            get { return trackTime; }
            set
            {
                settingsChanged = true;
                trackTime = value;
            }
        }

        public bool DisableUpdateTimer
        {
            get { return disableUpdateTimer; }
            set
            {
                settingsChanged = true;
                disableUpdateTimer = value;
            }
        }

        public string HotKeyUp
        {
            get { return hotkeyUp; }
            set
            {
                settingsChanged = true;
                hotkeyUp = value;
            }
        }

        public string HotKeyDown
        {
            get { return hotkeyDown; }
            set
            {
                settingsChanged = true;
                hotkeyDown = value;
            }
        }

        public string HotKeyModifier
        {
            get { return hotkeyModifier; }
            set
            {
                settingsChanged = true;
                hotkeyModifier = value;
            }
        }

        public string HotKeyFogModifier
        {
            get { return hotkeyFogModifier; }
            set
            {
                settingsChanged = true;
                hotkeyFogModifier = value;
            }
        }


        public string HotKeyFog
        {
            get { return hotkeyToggleFog; }
            set
            {
                settingsChanged = true;
                hotkeyToggleFog = value;
            }
        }

        public bool HasChanged
        {
            get { return settingsChanged; }
            set { settingsChanged = value; }
        }

  

        public static Settings Instance
        {
            get { return instance; }
        }
    }
}

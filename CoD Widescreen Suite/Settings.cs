using CurtLog;
using ShadyPool;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoD_Widescreen_Suite
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

        static Settings()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var oldAppDirectory = Path.Combine(appData, "CoDUO FoV Changer");
            var applicationDataDirectory = Path.Combine(appData, "CoD Widescreen Suite");

            try
            {
                if (Directory.Exists(oldAppDirectory) && !Directory.Exists(applicationDataDirectory))
                {
                    Log.WriteLine("Moving old settings directory to new location.");
                    Directory.Move(oldAppDirectory, applicationDataDirectory);
                    Log.WriteLine("Moved old settings directory to new location.");
                }
            }
            catch(Exception ex)
            {
                try
                {
                    Log.WriteLine($"Failed to move old settings directory to new directory{Environment.NewLine}Old directory: {oldAppDirectory} to new -> {applicationDataDirectory}{Environment.NewLine}{ex}");
                }
                catch (Exception ex2) { MessageBox.Show(ex2.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
           

            settingsFile = Path.Combine(applicationDataDirectory, "settings.xml");

            try
            {
                if (!Directory.Exists(applicationDataDirectory))
                    Directory.CreateDirectory(applicationDataDirectory);
            }
            catch(Exception ex)
            {
                try { Log.WriteLine($"Failed to create {nameof(applicationDataDirectory)} ({applicationDataDirectory}){Environment.NewLine}{ex}"); }
                catch(Exception ex2)
                {
                    MessageBox.Show(ex2.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          

            if (!File.Exists(settingsFile))
            {
                instance = new Settings();
                DatabaseFile.Write(instance, settingsFile);

                return;
            }
            else
            {
                var readFile = File.ReadAllText(settingsFile);

                var sb = Pool.Get<StringBuilder>();
                try 
                {
                    var oldRead = readFile;

                    var newRead = sb.Clear().Append(readFile).Replace("CoDUO_FoV_Changer", "CoD_Widescreen_Suite").ToString();
                    
                    if (oldRead != newRead)
                        File.WriteAllText(settingsFile, newRead);
                }
                finally { Pool.Free(ref sb); }

                instance = DatabaseFile.Read<Settings>(settingsFile);
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
            hotkeyUp = "107";
            hotkeyDown = "109";
            hotkeyModifier = "17";
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

        public bool HasChanged
        {
            get { return settingsChanged; }
            set { settingsChanged = value; }
        }

        public static Settings Instance { get { return instance; } }
    }
}
﻿using CurtLog;
using Newtonsoft.Json;
using ShadyPool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    internal class Settings
    {
        // This is a JSON class that is the same as OldSettings.cs, but updated to use JSON:

        [JsonIgnore]
        private static readonly Settings instance;
        [JsonIgnore]
        private static readonly string settingsFile;

        [JsonIgnore]
        public bool HasChanged { get; set; } = false;

        // Not serialized:
        private bool FinishedInitialLoad { get; set; } = false;

        private void SetConfigField<T>(ref T field, T value)
        {
            if (!Equals(field, value))
            {
                field = value;

                if (FinishedInitialLoad)
                    HasChanged = true;
            }
        }

        private string _baseGamePath;
        public string BaseGamePath
        {
            get { return _baseGamePath; }
            set { SetConfigField(ref _baseGamePath, value); }
        }

        private List<string> _gameExes;
        public List<string> GameExes
        {
            get { return _gameExes; }
            set { SetConfigField(ref _gameExes, value); }
        }

        private string _selectedExecutable;
        /// <summary>
        /// The executable that a user has selected to launch the game. This should not be set directly; it will be controlled programatically by the program based on the selected GameExe from the GameExes list.
        /// </summary>
        public string SelectedExecutable
        {
            get { return _selectedExecutable; }
            set { SetConfigField(ref _selectedExecutable, value); }
        }

        /// <summary>
        /// The selected favorite server that will be connected to when the game is started through the app. This should not be set directly; it will be controlled programatically by the program based on the selected FavoriteServer from the FavoriteServers list.
        /// </summary>
        private string _selectedFavoriteServer;
        public string SelectedFavoriteServer
        {
            get { return _selectedFavoriteServer; }
            set { SetConfigField(ref _selectedFavoriteServer, value); }
        }

        private decimal _fov;
        public decimal FoV
        {
            get { return _fov; }
            set { SetConfigField(ref _fov, value); }
        }

        private string _cmdLine;
        public string CommandLine
        {
            get { return _cmdLine; }
            set { SetConfigField(ref _cmdLine, value); }
        }

        private bool _minimize;
        public bool MinimizeToTray
        {
            get { return _minimize; }
            set { SetConfigField(ref _minimize, value); }
        }

        private string _lastLog;
        public string LastLogFile
        {
            get { return _lastLog; }
            set { SetConfigField(ref _lastLog, value); }
        }

        private bool _trackTime;
        public bool TrackGameTime
        {
            get { return _trackTime; }
            set { SetConfigField(ref _trackTime, value); }
        }

        private double _gameTime;
        public double GameTime
        {
            get { return _gameTime; }
            set { SetConfigField(ref _gameTime, value); }
        }

        private string _hotkeyUp;
        public string HotKeyUp
        {
            get { return _hotkeyUp; }
            set { SetConfigField(ref _hotkeyUp, value); }
        }

        private string _hotkeyDown;
        public string HotKeyDown
        {
            get { return _hotkeyDown; }
            set { SetConfigField(ref _hotkeyDown, value); }
        }

        private string _hotkeyModifier;
        public string HotKeyModifier
        {
            get { return _hotkeyModifier; }
            set { SetConfigField(ref _hotkeyModifier, value); }
        }

        private bool _launchWhenSelectedExeChanged;
        public bool LaunchWhenSelectedExeChanged
        {
            get { return _launchWhenSelectedExeChanged; }
            set { SetConfigField(ref _launchWhenSelectedExeChanged, value); }
        }

        private int _serverListGameIndex;
        public int ServerListGameIndex
        {
            get { return _serverListGameIndex; }
            set { SetConfigField(ref _serverListGameIndex, value); }
        }

        private int _serverListMaxPing;
        public int ServerListMaxPing
        {
            get { return _serverListMaxPing; }
            set { SetConfigField(ref _serverListMaxPing, value); }
        }

        private bool _serverListHideNoPing;
        public bool ServerListHideNoPing
        {
            get { return _serverListHideNoPing; }
            set { SetConfigField(ref _serverListHideNoPing, value); }
        }

        private bool _serverListHideEmpty;
        public bool ServerListHideEmpty
        {
            get { return _serverListHideEmpty; }
            set { SetConfigField(ref _serverListHideEmpty, value); }
        }

        private bool _serverListFilterPlayerNames;
        public bool ServerListFilterPlayerNames
        {
            get { return _serverListFilterPlayerNames; }
            set { SetConfigField(ref _serverListFilterPlayerNames, value); }
        }

        private bool _serverListFilterBots;
        public bool ServerListFilterBots
        {
            get { return _serverListFilterBots; }
            set { SetConfigField(ref _serverListFilterBots, value); }
        }

        private bool _serverListFilterBotServers;
        public bool ServerListFilterBotServers
        {
            get { return _serverListFilterBotServers; }
            set { SetConfigField(ref _serverListFilterBotServers, value); }
        }

        private bool _serverListShowFavorites;
        public bool ServerListShowFavorites
        {
            get { return _serverListShowFavorites; }
            set { SetConfigField(ref _serverListShowFavorites, value); }
        }

        private int _serverListAutoRefreshSetting;
        public int ServerListAutoRefreshSetting
        {
            get => _serverListAutoRefreshSetting;
            set => SetConfigField(ref _serverListAutoRefreshSetting, value);    
        }

        private bool _useDesktopRes;
        public bool UseDesktopRes
        {
            get { return _useDesktopRes; }
            set { SetConfigField(ref _useDesktopRes, value); }
        }

        private List<FavoriteServer> _favoriteServers;
        public List<FavoriteServer> FavoriteServers
        {
            get { return _favoriteServers; }
            set { SetConfigField(ref _favoriteServers, value); }
        }

        private bool _useSteamOverlay;
        public bool UseSteamOverlay
        {
            get { return _useSteamOverlay; }
            set { SetConfigField(ref _useSteamOverlay, value); }
        }

        private string _selectedCultureCode;
        public string SelectedCultureCode
        {
            get { return _selectedCultureCode; }
            set { SetConfigField(ref _selectedCultureCode, value); }
        }

        public string SelectedExecutablePath => Path.Combine(_baseGamePath, SelectedExecutable);


        static Settings()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var applicationDataDirectory = Path.Combine(appData, "CoDUO FoV Changer");

            settingsFile = Path.Combine(applicationDataDirectory, "settings.json");

            var oldSettingsFile = Path.Combine(applicationDataDirectory, "settings.xml");

            try
            {
                if (!Directory.Exists(applicationDataDirectory))
                    Directory.CreateDirectory(applicationDataDirectory);
            }
            catch (Exception ex)
            {
                try { Log.WriteLine($"Failed to create {nameof(applicationDataDirectory)} ({applicationDataDirectory}){Environment.NewLine}{ex}"); }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            if (!File.Exists(settingsFile) && !File.Exists(oldSettingsFile))
            {
                instance = new Settings();
                var settingsSerialized = JsonConvert.SerializeObject(instance, Formatting.Indented);
                
                File.WriteAllText(settingsFile, settingsSerialized);

                return;
            }
            else if (File.Exists(oldSettingsFile))
            {
                var readFile = File.ReadAllText(oldSettingsFile);

                var sb = Pool.Get<StringBuilder>();
                try
                {
                    var oldRead = readFile;

                    var newRead = sb.Clear()
                        .Append(readFile)
                        .Replace("Settings", "OldSettings")
                        .ToString();

                    if (oldRead != newRead)
                        File.WriteAllText(oldSettingsFile, newRead);
                }
                finally { Pool.Free(ref sb); }

                var oldSettings = DatabaseFile.Read<OldSettings>(oldSettingsFile);

                // Set all the properties of this new Settings.cs to be the same as the oldSettings - we're transitioning from XML to JSON:

                instance = new Settings
                {
                    BaseGamePath = oldSettings.InstallPath,
                    CommandLine = oldSettings.CommandLine,
                    SelectedExecutable = oldSettings.ExeName,
                    FoV = oldSettings.FoV,
                    GameTime = oldSettings.GameTime,
                    HotKeyDown = oldSettings.HotKeyDown,
                    HotKeyUp = oldSettings.HotKeyUp,
                    HotKeyModifier = oldSettings.HotKeyModifier,
                    LastLogFile = oldSettings.LastLogFile,
                    MinimizeToTray = oldSettings.MinimizeToTray,
                    TrackGameTime = oldSettings.TrackGameTime,
                    FinishedInitialLoad = true // not part of the old settings
                };

                // No need to write the changes to the disk for the new settings - that will be done automatically when the app closes.

                var replaced = oldSettingsFile.Replace(".xml", ".old");
                if (!File.Exists(replaced))
                    File.Move(oldSettingsFile, oldSettingsFile.Replace(".xml", ".old"));
            }
            else if (File.Exists(settingsFile))
            {
                instance = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(settingsFile));
                instance.FinishedInitialLoad = true;
            }


        }

        public Settings()
        {
            HasChanged = false;
            _baseGamePath = string.Empty;
            _selectedExecutable = string.Empty;
            _fov = 80;
            _cmdLine = string.Empty;
            _minimize = true;
            _lastLog = string.Empty;
            _trackTime = true;
            _gameTime = 0;
            _hotkeyUp = "107";
            _hotkeyDown = "109";
            _hotkeyModifier = "17";
            _launchWhenSelectedExeChanged = false;
            _gameExes = new List<string>();
            _favoriteServers = new List<FavoriteServer>();
            _useSteamOverlay = true;
        }

        public static Settings Instance { get { return instance; } }

        public static void SaveInstanceToDisk()
        {
            var settingsSerialized = JsonConvert.SerializeObject(instance, Formatting.Indented);

            File.WriteAllText(settingsFile, settingsSerialized);
        }

    }
}

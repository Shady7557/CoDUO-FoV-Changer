using CoDUO_FoV_Changer.Util;
using CurtLog;
using Microsoft.Win32;
using StringExtension;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerExtensions;

namespace CoDUO_FoV_Changer
{
    public partial class GameFixesForm : ExtendedForm
    {

        private const string APP_COMPAT_FLAGS_PATH = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers";
        private const string DPI_FIX_REGISTRY_VALUE = "~ DISABLEDXMAXIMIZEDWINDOWEDMODE PERPROCESSSYSTEMDPIFORCEOFF HIGHDPIAWARE";

        private const string PCGAMINGWIKI_URL = "https://www.pcgamingwiki.com/wiki/Call_of_Duty";

        public bool IsClosingOrClosed
        {
            get;
            private set;
        }

        public float GetCurrentFov()
        {
            return GetInstance<MainForm>()?.CurrentFoV ?? -1;
        }

        public GameFixesForm()
        {
            InitializeComponent();
        }

        private async void applyCfgBtn_Click(object sender, EventArgs e)
        {
            // Get current Field of View from MainForm
            // and then write to the single player configs
            // for both CoD and UO (only if config file already exists).

            // Get the current Field of View from MainForm
            var fov = GetCurrentFov();

            // Write desired FOV to cg_fov in both game configs.

            await Task.Run(() =>
            {
                if (GameConfig.ConfigFileExists(GameConfig.GameType.CoDSP))
                    GameConfig.UpdateConfigValue("cg_fov", fov.ToString(), GameConfig.GameType.CoDSP);

                if (GameConfig.ConfigFileExists(GameConfig.GameType.CoDUOSP))
                    GameConfig.UpdateConfigValue("cg_fov", fov.ToString(), GameConfig.GameType.CoDUOSP);
            });

            // Update FOV labels to reflect changes.
            UpdateFovLabels();
            ShowChangesCheckBoxAndLabel(TimeSpan.FromSeconds(2));
        }

        private void SingleplayerForm_Load(object sender, EventArgs e)
        {
            
            UpdateFovLabels();
            HideChangesCheckBoxAndLabel();
        }

        private System.Threading.Timer _changesTimer = null;

        private void ShowChangesCheckBoxAndLabel(TimeSpan hideIn = default)
        {
            if (IsClosingOrClosed)
                return;

            BeginInvoke((MethodInvoker)delegate
            {

                appliedChangesLbl.Visible = true;
                greenCheckPb.Visible = true;
            });

            _changesTimer?.Dispose();
            _changesTimer = null;


            if (hideIn.TotalSeconds > 0)
                _changesTimer = TimerEx.Once(hideIn, () => HideChangesCheckBoxAndLabel());
        }

        private void HideChangesCheckBoxAndLabel()
        {
            if (IsClosingOrClosed)
                return;

            Console.WriteLine(nameof(HideChangesCheckBoxAndLabel));

            BeginInvoke((MethodInvoker)delegate
            {
                appliedChangesLbl.Visible = false;
                greenCheckPb.Visible = false;
            });
        }

        private void UpdateFovLabels()
        {
            if (IsClosingOrClosed)
                return;

            var desiredCodTxt = string.Empty; //GameConfig.GetConfigValue("cg_fov", GameConfig.GameType.CoDSP);

            var desiredUoTxt = string.Empty;

            if (GameConfig.ConfigFileExists(GameConfig.GameType.CoDSP))
                desiredCodTxt = GameConfig.GetConfigValue("cg_fov", GameConfig.GameType.CoDSP);

            if (GameConfig.ConfigFileExists(GameConfig.GameType.CoDUOSP))
                desiredUoTxt = GameConfig.GetConfigValue("cg_fov", GameConfig.GameType.CoDUOSP);

            if (string.IsNullOrWhiteSpace(desiredCodTxt))
                desiredCodTxt = $"CoDSP config ({GameConfig.GetConfigFileName(GameConfig.GameType.CoDSP)}) not found";

            if (string.IsNullOrWhiteSpace(desiredUoTxt))
                desiredUoTxt = $"CoDUOSP config ({GameConfig.GetConfigFileName(GameConfig.GameType.CoDUOSP)}) not found";

            BeginInvoke((MethodInvoker)delegate 
            {

                codFovLbl.Text = desiredCodTxt;
                uoFovLabel.Text = desiredUoTxt;
            });
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        // TODO: Merge into one button; show undo text if changes already applied.
        private void dpiFixButton_Click(object sender, EventArgs e)
        {
            var baseGamePath = Settings.Instance.BaseGamePath;
            var exes = PathInfos.GetProcessNamesWithDotExe();

            foreach (var exe in exes)
            {
                try
                {
                    var exePath = Path.Combine(baseGamePath, exe);

                    if (!File.Exists(exePath))
                        continue;
                    // TODO: Parse existing value & do not remove/modify existing value (ones we didn't apply).

                    var existingRegistryValue = Registry.GetValue(APP_COMPAT_FLAGS_PATH, exePath, string.Empty)?.ToString() ?? string.Empty;

                    if (existingRegistryValue.Contains(DPI_FIX_REGISTRY_VALUE, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var hasTilde = existingRegistryValue.StartsWith("~", StringComparison.OrdinalIgnoreCase);
                    var newVal = StringBuilderCache.Acquire(existingRegistryValue.Length + DPI_FIX_REGISTRY_VALUE.Length + 2)
                        .Append(existingRegistryValue)
                        .Append(!hasTilde ? "~" : string.Empty)
                        .Append(" ")
                        .Append(DPI_FIX_REGISTRY_VALUE)
                        .ToString();

                    Registry.SetValue(APP_COMPAT_FLAGS_PATH, exePath, DPI_FIX_REGISTRY_VALUE, RegistryValueKind.String);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }

            }

            ShowChangesCheckBoxAndLabel(TimeSpan.FromSeconds(2));            
        }

        private void undoDpiButton_Click(object sender, EventArgs e)
        {
            var baseGamePath = Settings.Instance.BaseGamePath;
            var exes = PathInfos.GetProcessNamesWithDotExe();

            foreach (var exe in exes)
            {
                try
                {
                    var exePath = Path.Combine(baseGamePath, exe);

                    if (!File.Exists(exePath))
                        continue;

                    var existingValue = Registry.GetValue(APP_COMPAT_FLAGS_PATH, exePath, string.Empty)?.ToString() ?? string.Empty;

                    var newVal = string.Empty;

                    if (!string.IsNullOrWhiteSpace(existingValue))
                        newVal = existingValue.Replace(DPI_FIX_REGISTRY_VALUE, string.Empty);


                    Registry.SetValue(APP_COMPAT_FLAGS_PATH, newVal, string.Empty, RegistryValueKind.String);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }

            }

            ShowChangesCheckBoxAndLabel(TimeSpan.FromSeconds(2));
        }

        private void GameFixesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosingOrClosed = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(PCGAMINGWIKI_URL);
        
    }
}

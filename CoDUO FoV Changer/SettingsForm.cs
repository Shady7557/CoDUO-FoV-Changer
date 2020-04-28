using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using BitmapExtension;
using CurtLog;

namespace CoDUO_FoV_Changer
{
    public partial class SettingsForm : Form
    {
        private string keyLabelText = string.Empty;
        private readonly string RegistryPath = PathScanner.RegistryPath;
        private readonly string GameVersion = MainForm.Instance.GameVersion;
        private readonly Settings settings = Settings.Instance;

        private string _cdKeyUO = string.Empty;
        private string CDKeyUO
        {
            get
            {
                if (string.IsNullOrEmpty(_cdKeyUO)) _cdKeyUO = Registry.GetValue(RegistryPath, "key", string.Empty)?.ToString() ?? string.Empty;
                return _cdKeyUO;
            }
        }

        private string _cdKey = string.Empty;
        private string CDKey
        {
            get
            {
                if (string.IsNullOrEmpty(_cdKey)) _cdKey = Registry.GetValue(RegistryPath.Replace("United Offensive", string.Empty).TrimEnd(' '), "key", string.Empty)?.ToString() ?? string.Empty;
                return _cdKey;
            }
        }


        public static SettingsForm Instance = null;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        } //makes the loading look less shitty


        public SettingsForm()
        {
            Instance = this;
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AppVersLabel.Text = "App. Version: " + Application.ProductVersion;
            GameVersLabel.Text = "Game Version: " + (!string.IsNullOrEmpty(GameVersion) ? GameVersion : "Unknown");

            Location = new Point(MainForm.Instance.Location.X - 250, MainForm.Instance.Location.Y - 60);
            ButtonBrowseGameFiles.Enabled = !string.IsNullOrEmpty(settings.InstallPath) && Directory.Exists(settings.InstallPath);
            CDKeyLabel.Text = "CD-Key: Hidden";

            var vcKey = CDKey;
            keyLabelText = "CD-Key: " + CDKeyUO + (!string.IsNullOrEmpty(vcKey) ? (Environment.NewLine + "VCoD Key: " + vcKey) : string.Empty);
            if (!Program.IsElevated) hotKeysButton.Image = BitmapHelper.ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));
        }

        #region Buttons
        private void CloseSettingsButton_Click(object sender, EventArgs e) => Close();

        private void RestartAppButton_Click(object sender, EventArgs e) => Application.Restart();

        private void ButtonBrowseGameFiles_Click(object sender, EventArgs e)
        {
            try { Process.Start(settings.InstallPath); }
            catch(Exception ex)
            {
                Log.WriteLine("An error happened while trying to browse local game files:");
                Log.WriteLine(ex.ToString());
            }
        }

        private void ButtonSettingsAdvanced_Click(object sender, EventArgs e)
        {
            if (AdvancedSettings.Instance != null && !AdvancedSettings.Instance.IsDisposed && !AdvancedSettings.Instance.Disposing) AdvancedSettings.Instance.BringToFront();
            else new AdvancedSettings().Show();
        }

        private void hotKeysButton_Click(object sender, EventArgs e)
        {
            if (Hotkeys.Instance != null && !Hotkeys.Instance.IsDisposed && !Hotkeys.Instance.Disposing) Hotkeys.Instance.BringToFront();
            else new Hotkeys().Show();
        }
        #endregion

        private void CDKeyLabel_MouseDown(object sender, MouseEventArgs e) => CDKeyLabel.Text = !CDKeyLabel.Text.Contains("Hidden") ? "CD-Key: Hidden" : keyLabelText;
    }
}

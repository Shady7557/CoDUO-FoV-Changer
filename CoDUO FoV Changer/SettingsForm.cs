using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace CoDUO_FoV_Changer
{
    public partial class SettingsForm : Form
    {
        string CDKey = string.Empty;
        string RegistryPath = MainForm.GetRegistryPath();
        string GameVersion = string.Empty;
        Settings settings = Settings.Instance;
        public SettingsForm() => InitializeComponent();

        void WriteLog(string message) => MainForm.WriteLog(message);

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AppVersLabel.Text = "App. Version: " + Application.ProductVersion;
            GameVersion = GetGameVersion();
            GameVersLabel.Text = "Game Version: " + ((!string.IsNullOrEmpty(GameVersion)) ? GameVersion : "Unknown");

            this.Location = new Point(MainForm.location.X - 250, MainForm.location.Y - 60);
            ButtonBrowseGameFiles.Enabled = (!string.IsNullOrEmpty(settings.InstallPath) && Directory.Exists(settings.InstallPath));
            CDKeyLabel.Text = "CD-Key: " + (CDKey = GetCDKey());
            if (!MainForm.isElevated) hotKeysButton.Image = ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));
        }

        #region Buttons
        private void CloseSettingsButton_Click(object sender, EventArgs e) => Close();

        private void RestartAppButton_Click(object sender, EventArgs e) => Application.Restart();

        private void ButtonBrowseGameFiles_Click(object sender, EventArgs e)
        {
            try { Process.Start(settings.InstallPath); }
            catch(Exception ex)
            {
                WriteLog("An error happened while trying to browse local game files:");
                WriteLog(ex.ToString());
            }
        }

        private void ButtonSettingsAdvanced_Click(object sender, EventArgs e) => new AdvancedSettings().Show();

        private void hotKeysButton_Click(object sender, EventArgs e) => new Hotkeys().Show();
        #endregion

        private void CDKeyLabel_MouseDown(object sender, MouseEventArgs e) => CDKeyLabel.Text = "CD-Key: " + ((CDKeyLabel.Text.Contains(CDKey) && !string.IsNullOrEmpty(CDKey)) ? "Hidden" : CDKey);

        #region Util
        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            var b = new Bitmap(size.Width, size.Height);
            using (var g = Graphics.FromImage((Image)b))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
            }
            return b;
        }


        private string GetCDKey() { return Registry.GetValue(RegistryPath, "key", string.Empty)?.ToString() ?? string.Empty; }
        private string GetGameVersion() { return Registry.GetValue(RegistryPath, "Version", string.Empty)?.ToString() ?? string.Empty; }
        #endregion

    }
}

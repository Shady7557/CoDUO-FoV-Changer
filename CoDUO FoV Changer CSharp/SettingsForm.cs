using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CoDUO_FoV_Changer_CSharp
{
    public partial class SettingsForm : Form
    {
        string CDKey = string.Empty;
        string RegistryPath = MainForm.RegistryPath;
        string GameVersion = string.Empty;
        Settings settings = Settings.Instance;
        public SettingsForm()
        {
            InitializeComponent();
        }

        void WriteLog(string message) => MainForm.WriteLog(message);

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AppVersLabel.Text = AppVersLabel.Text.Replace("0000", Application.ProductVersion);
            GameVersion = GetGameVersion();

            if (!string.IsNullOrEmpty(GameVersion)) GameVersLabel.Text = GameVersLabel.Text.Replace("0000", GameVersion);
            else GameVersLabel.Text = GameVersLabel.Text.Replace("0000", "Unknown");

            this.Location = new Point(MainForm.location.X - 250, MainForm.location.Y - 60);
            if (string.IsNullOrEmpty(settings.InstallPath) || !Directory.Exists(settings.InstallPath)) ButtonBrowseGameFiles.Enabled = false;
            CDKeyLabel.Text = "CD-Key: " + (CDKey = GetCDKey());
            if (!MainForm.isElevated) hotKeysButton.Image = ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));

            //hotKeysButton.Visible = MainForm.isDev;
        }

        #region Buttons
        private void CloseSettingsButton_Click(object sender, EventArgs e) => Close();

        private void RestartAppButton_Click(object sender, EventArgs e) => Application.Restart();

        private void ButtonBrowseGameFiles_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(settings.InstallPath);
            }
            catch(Exception ex)
            {
                WriteLog("An error happened while trying to browse local game files:");
                WriteLog(ex.ToString());
            }
        }

        private void ButtonSettingsAdvanced_Click(object sender, EventArgs e) => new AdvancedSettings().Show();

        private void hotKeysButton_Click(object sender, EventArgs e) => new Hotkeys().Show();
        #endregion

        private void CDKeyLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(CDKey) && !CDKeyLabel.Text.Contains(CDKey)) CDKeyLabel.Text = "CD-Key: " + CDKey;
            else CDKeyLabel.Text = "CD-Key: Hidden";
        }

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


        private string GetCDKey()
        {
            return Registry.GetValue(RegistryPath, "key", string.Empty)?.ToString() ?? string.Empty;
        }

        private string GetGameVersion()
        {
            return Registry.GetValue(RegistryPath, "Version", string.Empty)?.ToString() ?? string.Empty;
        }
        #endregion

    }
}

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

        private string GetCDKeyTest()
        {
            var val = string.Empty;

            var regPath = @"HKEY_USERS\" + Program.CurrentUserSID + @"\SOFTWARE\Classes\VirtualStore\MACHINE\SOFTWARE\WOW6432Node\Activision\Call of Duty United Offensive";
            Console.WriteLine("sid: " + Program.CurrentUserSID + Environment.NewLine + regPath);
            val = Registry.GetValue(regPath, "key", string.Empty)?.ToString() ?? string.Empty;

            return val;
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
            Location = new Point(MainForm.Instance.Location.X - 250, MainForm.Instance.Location.Y - 60);

            AppVersLabel.Text = "App. Version: " + Application.ProductVersion;
            GameVersLabel.Text = "Game Version: " + (!string.IsNullOrEmpty(GameVersion) ? GameVersion : "Unknown");


            ButtonBrowseGameFiles.Enabled = !string.IsNullOrEmpty(settings.InstallPath) && Directory.Exists(settings.InstallPath);


            if (!Program.IsElevated)
            {
                hotKeysButton.Image = BitmapHelper.ResizeImage(SystemIcons.Shield.ToBitmap(), new Size(16, 16));
                cdKeyManagerButton.Image = hotKeysButton.Image;
            }
        }

        #region Buttons
        private void CloseSettingsButton_Click(object sender, EventArgs e) => Close();

        private void ButtonBrowseGameFiles_Click(object sender, EventArgs e)
        {
            try { Process.Start(settings.InstallPath); }
            catch (Exception ex)
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
            try
            {
                if (!Program.IsElevated)
                {
                    var currentProc = Process.GetCurrentProcess();
                    var fileNameDir = currentProc?.MainModule?.FileName ?? string.Empty;
                    if (string.IsNullOrEmpty(fileNameDir) || !File.Exists(fileNameDir))
                    {
                        MessageBox.Show("Application path doesn't exist. Cannot start: " + fileNameDir + Environment.NewLine + " Please manually run the program as an Admin if you wish to change your hotkeys.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                    else
                    {
                        var startInfo = new ProcessStartInfo
                        {
                            Verb = "runas",
                            Arguments = "-hotkeys",
                            WorkingDirectory = Application.StartupPath,
                            FileName = fileNameDir
                        };
                        Process.Start(startInfo);
                        Application.Exit();
                    }
                }
                else
                {
                    if (Hotkeys.Instance != null && !Hotkeys.Instance.IsDisposed && !Hotkeys.Instance.Disposing)
                    {
                        Hotkeys.Instance.Show();
                        Hotkeys.Instance.BringToFront();
                        Hotkeys.Instance.Select();
                    }
                    else new Hotkeys().Show();
                }
            }
            catch (System.ComponentModel.Win32Exception win32ex) when (win32ex.NativeErrorCode == 1223)
            {
                Log.WriteLine("User canceled UAC prompt (" + win32ex.Message + " )");
                return;
            }
            catch (Exception ex)
            {
                Log.WriteLine("Failed to start hotkeys form: " + ex.Message);
                Log.WriteLine(ex.ToString());
                MessageBox.Show("Failed to start hot keys form: " + ex.Message + Environment.NewLine + " please refer to the log for more info.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        #endregion

        private void cdKeyManagerButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Program.IsElevated)
                {
                    var currentProc = Process.GetCurrentProcess();
                    var fileNameDir = currentProc?.MainModule?.FileName ?? string.Empty;
                    if (string.IsNullOrEmpty(fileNameDir) || !File.Exists(fileNameDir))
                    {
                        MessageBox.Show("Application path doesn't exist. Cannot start: " + fileNameDir + Environment.NewLine + " Please manually run the program as an Admin if you wish to change your hotkeys.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                    else
                    {
                        var startInfo = new ProcessStartInfo
                        {
                            Verb = "runas",
                            Arguments = "-cdkeymanager",
                            WorkingDirectory = Application.StartupPath,
                            FileName = fileNameDir
                        };
                        Process.Start(startInfo);
                        Application.Exit();
                    }
                }
                else
                {
                    var ins = CDKeyManagerForm.Instance;
                    if (ins != null && !ins.IsDisposed && !ins.Disposing)
                    {
                        ins.Show();
                        ins.BringToFront();
                        ins.Select();
                    }
                    else new CDKeyManagerForm().Show();
                }
            }
            catch (System.ComponentModel.Win32Exception win32ex) when (win32ex.NativeErrorCode == 1223)
            {
                Log.WriteLine("User canceled UAC prompt (" + win32ex.Message + " )");
                return;
            }
            catch (Exception ex)
            {
                Log.WriteLine("Failed to start cd key manager form: " + ex.Message);
                Log.WriteLine(ex.ToString());
                MessageBox.Show("Failed to start cd key manager form: " + ex.Message + Environment.NewLine + " please refer to the log for more info.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}

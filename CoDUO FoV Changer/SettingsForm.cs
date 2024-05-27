using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using CurtLog;
using CoDRegistryExtensions;
using CoDUO_FoV_Changer.Util;

namespace CoDUO_FoV_Changer
{
    public partial class SettingsForm : ExtendedForm
    {
        private readonly Settings settings = Settings.Instance;

        public SettingsForm()
        {
            InitializeComponent();
        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            AppVersLabel.Text = "App. Version: " + Application.ProductVersion + " (" + HotfixVersion.HOTFIX + ")";
            GameVersLabel.Text = "Game Version: " + (!string.IsNullOrEmpty(CodRex.GameVersion) ? CodRex.GameVersion : "Unknown");


            ButtonBrowseGameFiles.Enabled = !string.IsNullOrEmpty(settings.BaseGamePath) && Directory.Exists(settings.BaseGamePath);


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
            try { Process.Start(settings.BaseGamePath); }
            catch (Exception ex)
            {
                Log.WriteLine("An error happened while trying to browse local game files:");
                Log.WriteLine(ex.ToString());
            }
        }

        private void ButtonSettingsAdvanced_Click(object sender, EventArgs e)
        {
            var ins = GetInstance<AdvancedSettingsForm>();

            if (ins != null)
                ins.UnminimizeAndSelect();
            else new AdvancedSettingsForm { Owner = this, AttachToOwner = true }.Show();
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
                            Arguments = "-multi -hotkeys",
                            WorkingDirectory = Application.StartupPath,
                            FileName = fileNameDir
                        };
                        Process.Start(startInfo);
                        Application.Exit();
                    }
                }
                else
                {

                    var ins = GetInstance<HotkeysForm>();
                    if (ins != null) ins.UnminimizeAndSelect();
                    else new HotkeysForm() { Owner = this, AttachToOwner = true}.Show();

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
                            Arguments = "-multi -cdkeymanager",
                            WorkingDirectory = Application.StartupPath,
                            FileName = fileNameDir
                        };
                        Process.Start(startInfo);
                        Application.Exit();
                    }
                }
                else
                {

                    var ins = GetInstance<CDKeyManagerForm>();
                    if (ins != null) ins.UnminimizeAndSelect();
                    else new CDKeyManagerForm { Owner = this, AttachToOwner = true }.Show();
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
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Failed to start cd key manager form: " + ex.Message + Environment.NewLine + " please refer to the log for more info.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}

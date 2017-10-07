using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer_CSharp
{
    public partial class Hotkeys : Form
    {
        private string curKeyName;
        private Keys currentKey;
        private int currentKeyCode;
        Settings settings = Settings.Instance;
        Settings oldSettings = Settings.Instance;
        public Hotkeys() => InitializeComponent();
        
        private void Hotkeys_Load(object sender, EventArgs e)
        {
            try
            {
                if (!MainForm.isElevated)
                {
                    var fileNameDir = Application.StartupPath + @"\" + AppDomain.CurrentDomain.FriendlyName;
                    if (!File.Exists(fileNameDir))
                    {
                        MessageBox.Show("Application has been renamed? Cannot start: " + fileNameDir + Environment.NewLine + " Please manually run the program as an Admin if you wish to change your hotkeys.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                    else
                    {
                        var startInfo = new ProcessStartInfo();
                        startInfo.Verb = "runas";
                        startInfo.Arguments = "-hotkeys";
                        startInfo.WorkingDirectory = Application.StartupPath;
                        startInfo.FileName = fileNameDir;
                        Process.Start(startInfo);
                        Application.Exit();
                    }
                }
            }
            catch(System.ComponentModel.Win32Exception win32ex)
            {
                if (win32ex.NativeErrorCode == 1223)
                {
                    MainForm.WriteLog("User canceled UAC prompt (" + win32ex.Message + " )");
                    Close();
                }
                return; //returns are required to stop the settings code below
            }
            catch(Exception ex)
            {
                MainForm.WriteLog("Failed to start hotkeys form: " + ex.Message);
                MainForm.WriteLog(ex.ToString());
                MessageBox.Show("Failed to start hot keys form: " + ex.Message + Environment.NewLine + " please refer to the log for more info.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return; //returns are required to stop the settings code below
            }
            DatabaseFile.Write<Settings>(settings, MainForm.settingsPath); //save current settings
            settings.HasChanged = false; //force it to not be changed so exit without saving works 'properly'
        }

        private void Hotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            var keyStr = e.KeyData.ToString();
            if (keyStr == "LWin") return;
            if (keyStr.Contains(",")) keyStr = keyStr.Split(',')[1];
            label1.Text = "Key: " + keyStr;
            currentKey = e.KeyData;
            currentKeyCode = e.KeyValue;
            curKeyName = e.KeyData.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(FoVUp.Checked)
            {
                settings.HotKeyUp = curKeyName;
                MessageBox.Show("Set FoV+ hotkey!");
            }
            if (FoVDown.Checked)
            {
                settings.HotKeyDown = curKeyName;
                MessageBox.Show("Set FoV- hotkey!");
            }
            if (FoVModifier.Checked)
            {
                settings.HotKeyModifier = curKeyName;
                MessageBox.Show("Set FoV modifier key!");
            }
            if (FogKey.Checked)
            {
                settings.HotKeyFog = curKeyName;
                MessageBox.Show("Set fog hotkey: " + curKeyName);
            }
            if (FogModifier.Checked)
            {
                settings.HotKeyFogModifier = curKeyName;
                MessageBox.Show("Set fog modifier hotkey!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatabaseFile.Write<Settings>(settings, MainForm.settingsPath);
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var closePrompt = MessageBox.Show("Are you sure you want to close without saving?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (closePrompt == DialogResult.Yes)
            {
                settings.HasChanged = false;
                Close();
            }
        }

        private Keys GetKeyFromString(string keyName)
        {
            if (string.IsNullOrEmpty(keyName)) return 0;
            Keys key;
            if (Enum.TryParse(keyName, out key)) return key;
            return 0;
        }

        private void FogKey_CheckedChanged(object sender, EventArgs e)
        {
            if (FogKey.Checked && !string.IsNullOrEmpty(settings.HotKeyFog)) label1.Text = "Key: " + GetKeyFromString(settings.HotKeyFog) + " (current)";
        }

        private void FoVUp_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVUp.Checked && !string.IsNullOrEmpty(settings.HotKeyUp)) label1.Text = "Key: " + GetKeyFromString(settings.HotKeyUp) + " (current)";
        }

        private void FoVDown_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVDown.Checked && !string.IsNullOrEmpty(settings.HotKeyDown)) label1.Text = "Key: " + GetKeyFromString(settings.HotKeyDown) + " (current)";
        }

        private void FoVModifier_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVModifier.Checked && !string.IsNullOrEmpty(settings.HotKeyModifier)) label1.Text = "Key: " + GetKeyFromString(settings.HotKeyModifier) + " (current)";
        }

        private void FogModifier_CheckedChanged(object sender, EventArgs e)
        {
            if (FogModifier.Checked && !string.IsNullOrEmpty(settings.HotKeyFogModifier)) label1.Text = "Key: " + GetKeyFromString(settings.HotKeyFogModifier) + " (current)";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer_CSharp
{
    public partial class Hotkeys : Form
    {
        string curKeyName;
        System.Windows.Forms.Keys currentKey;
        int currentKeyCode;
        Settings settings = Settings.Instance;
        public Hotkeys()
        {
            InitializeComponent();
        }
        
        private void Hotkeys_Load(object sender, EventArgs e)
        {
            if (MainForm.isDev) return;
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
            catch(Exception ex)
            {
                MainForm.WriteLog("Failed to start hotkeys form: " + ex.Message);
                MainForm.WriteLog(ex.ToString());
                MessageBox.Show("Failed to start hot keys form: " + ex.Message + Environment.NewLine + " please refer to the log for more info.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void Hotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            var keyStr = e.KeyData.ToString();
            if (keyStr == "LWin") return;
            if (keyStr.Contains(",")) keyStr = keyStr.Split(',')[1];
            //     MessageBox.Show(keyStr);
            //  if (IsFKey(e.KeyData))
            //{
            label1.Text = "Key: " + keyStr;
                currentKey = e.KeyData;
                currentKeyCode = e.KeyValue;
                curKeyName = e.KeyData.ToString();
     //       }
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
            if (settings.HasChanged)
            {
                var closePrompt = MessageBox.Show("Settings changed! Are you sure you want to close without saving?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (closePrompt == DialogResult.Yes) Application.Restart();
            }
        }

        Keys GetKeyFromString(string keyName)
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

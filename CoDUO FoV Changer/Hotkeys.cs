using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class Hotkeys : Form
    {
        private string curKeyName;
        private int currentKeyCode;
        private readonly Settings settings = Settings.Instance;

        public static Hotkeys Instance = null;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        } //makes the loading look less shitty

        public Hotkeys()
        {
            Instance = this;
            InitializeComponent();
        }

        private void Hotkeys_Load(object sender, EventArgs e)
        {

            DatabaseFile.Write(settings, PathInfos.SettingsPath); //save current settings
            settings.HasChanged = false; //force it to not be changed so exit without saving works 'properly'

            //transparent label backcolor
            var pos = PointToScreen(label2.Location);
            pos = pictureBox1.PointToClient(pos);
            label2.Parent = pictureBox1;
            label2.Location = pos;
            label2.BackColor = Color.Transparent;
        }

        private void Hotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            var keyStr = e.KeyData.ToString();
            if (keyStr == "LWin") return;
            if (keyStr.Contains(",")) keyStr = keyStr.Split(',')[1];

            currentKeyCode = e.KeyValue;
            curKeyName = GetShortKeyString(e.KeyData);
            label1.Text = "Key: " + curKeyName;
            label2.Text = curKeyName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FoVUp.Checked)
            {
                settings.HotKeyUp = currentKeyCode.ToString();
                MessageBox.Show("Set FoV+ hotkey: " + curKeyName);
            }
            if (FoVDown.Checked)
            {
                settings.HotKeyDown = currentKeyCode.ToString();
                MessageBox.Show("Set FoV- hotkey: " + curKeyName);
            }
            if (FoVModifier.Checked)
            {
                settings.HotKeyModifier = currentKeyCode.ToString();
                MessageBox.Show("Set FoV modifier key: " + curKeyName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatabaseFile.Write(settings, PathInfos.SettingsPath);
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var closePrompt = MessageBox.Show("Are you sure you want to close without saving?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (closePrompt == DialogResult.Yes)
            {
                settings.HasChanged = false;
                //    settings = oldSettings;
                Close();
            }
        }

        private Keys GetKeyFromString(string keyName, bool split = false)
        {
            if (string.IsNullOrEmpty(keyName)) return 0;
            if (Enum.TryParse(keyName, out Keys key)) return key;
            return 0;
        }

        private string GetKeyString(Keys key)
        {
            if (key == 0) return string.Empty;

            var keyStr = key.ToString();
            if (keyStr.Contains(","))
                keyStr = keyStr.Split(',')[1];

            var keySB = new StringBuilder(keyStr);

            keySB.Replace("Oem", string.Empty).Replace("Decimal", "DEC").Replace("comma", ",").Replace("Period", ".").Replace("NumPad", "N").Replace("Minus", "-").Replace("plus", "+");

            return keySB.ToString();
        }

        private string GetShortKeyString(Keys key)
        {
            if (key == 0) return string.Empty;

            var keyStr = key.ToString();

            if (keyStr.Contains(","))
                keyStr = keyStr.Split(',')[1];

            var keySB = new StringBuilder(keyStr);
            keySB.Replace("Oem", string.Empty).Replace("Decimal", "DEC").Replace("comma", ",").Replace("Period", ".").Replace("NumPad", "N").Replace("Minus", "-").Replace("plus", "+");

            if (keySB.Length > 4) keySB = keySB.Remove(3, keySB.Length - 3).Append(".");

            return keySB.ToString();
        }

        private void FoVUp_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVUp.Checked && !string.IsNullOrEmpty(settings.HotKeyUp)) label1.Text = "Key: " + GetKeyString(GetKeyFromString(settings.HotKeyUp)) + " (current)";
        }

        private void FoVDown_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVDown.Checked && !string.IsNullOrEmpty(settings.HotKeyDown)) label1.Text = "Key: " + GetKeyString(GetKeyFromString(settings.HotKeyDown)) + " (current)";
        }

        private void FoVModifier_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVModifier.Checked && !string.IsNullOrEmpty(settings.HotKeyModifier)) label1.Text = "Key: " + GetKeyString(GetKeyFromString(settings.HotKeyModifier)) + " (current)";
        }

        private void Label2_TextChanged(object sender, EventArgs e)
        {
            if (label2.Text.Length > 2) label2.Font = new Font(label2.Font.Name, 9, label2.Font.Bold ? FontStyle.Bold : FontStyle.Regular);
            else label2.Font = new Font(label2.Font.Name, 12, label2.Font.Bold ? FontStyle.Bold : FontStyle.Regular);
        }
    }
}

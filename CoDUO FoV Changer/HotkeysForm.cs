using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class HotkeysForm : ExtendedForm
    {
        private string curKeyName;
        private int currentKeyCode;
        private readonly Settings settings = Settings.Instance;

        public HotkeysForm()
        {
            InitializeComponent();
        }

        private void CalculateAndSetPosition()
        {
            if (Owner == null)
                return;

            var newX = Owner.Left - Width;
            var newY = Owner.Top;

            // Ensure the new position is within screen bounds
            newX = Math.Max(newX, Screen.FromControl(Owner).WorkingArea.Left);
            newY = Math.Max(newY, Screen.FromControl(Owner).WorkingArea.Top);
            newY = Math.Min(newY, Screen.FromControl(Owner).WorkingArea.Bottom - Height);

            Location = new Point(newX, newY);
        }

        private void ParentForm_LocationChanged(object sender, EventArgs e) => CalculateAndSetPosition();

        private void Hotkeys_Load(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                CalculateAndSetPosition();
                Owner.LocationChanged += ParentForm_LocationChanged;
            }

            Settings.SaveInstanceToDisk(); //save current settings
            settings.HasChanged = false; //force it to not be changed so exit without saving works 'properly' (this is all an ugly solution)

            //transparent label backcolor
            var pos = PointToScreen(keyPbLabel.Location);
            pos = pictureBox1.PointToClient(pos);
            keyPbLabel.Parent = pictureBox1;
            keyPbLabel.Location = pos;
            keyPbLabel.BackColor = Color.Transparent;
        }

        private void Hotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            var keyStr = e.KeyData.ToString();
            if (keyStr == "LWin") return;
            if (keyStr.Contains(",")) keyStr = keyStr.Split(',')[1];

            currentKeyCode = e.KeyValue;
            curKeyName = GetShortKeyString(e.KeyData);
            keyLabel.Text = "Key: " + curKeyName;
            keyPbLabel.Text = curKeyName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FoVUpRadioButton.Checked)
            {
                settings.HotKeyUp = currentKeyCode.ToString();
                MessageBox.Show("Set FoV+ hotkey: " + curKeyName);
            }
            if (FoVDownRadioButton.Checked)
            {
                settings.HotKeyDown = currentKeyCode.ToString();
                MessageBox.Show("Set FoV- hotkey: " + curKeyName);
            }
            if (FoVModifierRadioButton.Checked)
            {
                settings.HotKeyModifier = currentKeyCode.ToString();
                MessageBox.Show("Set FoV modifier key: " + curKeyName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.SaveInstanceToDisk();
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
            if (FoVUpRadioButton.Checked && !string.IsNullOrEmpty(settings.HotKeyUp)) keyLabel.Text = "Key: " + GetKeyString(GetKeyFromString(settings.HotKeyUp)) + " (current)";
        }

        private void FoVDown_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVDownRadioButton.Checked && !string.IsNullOrEmpty(settings.HotKeyDown)) keyLabel.Text = "Key: " + GetKeyString(GetKeyFromString(settings.HotKeyDown)) + " (current)";
        }

        private void FoVModifier_CheckedChanged(object sender, EventArgs e)
        {
            if (FoVModifierRadioButton.Checked && !string.IsNullOrEmpty(settings.HotKeyModifier)) keyLabel.Text = "Key: " + GetKeyString(GetKeyFromString(settings.HotKeyModifier)) + " (current)";
        }

        private void Label2_TextChanged(object sender, EventArgs e)
        {
            if (keyPbLabel.Text.Length > 2) keyPbLabel.Font = new Font(keyPbLabel.Font.Name, 9, keyPbLabel.Font.Bold ? FontStyle.Bold : FontStyle.Regular);
            else keyPbLabel.Font = new Font(keyPbLabel.Font.Name, 12, keyPbLabel.Font.Bold ? FontStyle.Bold : FontStyle.Regular);
        }
    }
}

using System;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class AdvancedSettings : Form
    {
        Settings settings = Settings.Instance;

        public static AdvancedSettings Instance = null;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        } //makes the loading look less shitty

        public AdvancedSettings()
        {
            Instance = this;
            InitializeComponent();
        }

        private void AdvancedSettings_Load(object sender, EventArgs e)
        {
            GameTimeCheckbox.Checked = settings.TrackGameTime;
            DisableUpdateTimerCBox.Checked = settings.DisableUpdateTimer;
        }

        private void SaveRestartAppButton_Click(object sender, EventArgs e)
        {
            settings.TrackGameTime = GameTimeCheckbox.Checked;
            settings.DisableUpdateTimer = DisableUpdateTimerCBox.Checked;
            if (settings.HasChanged)
            {
                DatabaseFile.Write(settings, MainForm.settingsPath);
                Application.Restart();
            }
            else Close();
        }

        private void CancelCloseButton_Click(object sender, EventArgs e) => Close();
    }
}

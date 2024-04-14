using System;
using System.Windows.Forms;

namespace CoD_Widescreen_Suite
{
    public partial class AdvancedSettingsForm : ExtendedForm
    {
        private readonly Settings settings = Settings.Instance;

        public AdvancedSettingsForm()
        {
            InitializeComponent();
        }

        private void AdvancedSettings_Load(object sender, EventArgs e)
        {
            GameTimeCheckbox.Checked = settings.TrackGameTime;
        }

        private void SaveRestartAppButton_Click(object sender, EventArgs e)
        {
            settings.TrackGameTime = GameTimeCheckbox.Checked;
            if (settings.HasChanged)
            {
                DatabaseFile.Write(settings, PathInfos.SettingsPath);
                Application.Restart();
            }
            else Close();
        }

        private void CancelCloseButton_Click(object sender, EventArgs e) => Close();
    }
}

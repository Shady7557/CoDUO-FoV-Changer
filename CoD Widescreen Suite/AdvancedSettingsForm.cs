using System;

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
            startIfChangeCheckbox.Checked = settings.LaunchWhenSelectedExeChanged;
        }

        private void SaveRestartAppButton_Click(object sender, EventArgs e)
        {
            settings.TrackGameTime = GameTimeCheckbox.Checked;
            settings.LaunchWhenSelectedExeChanged = startIfChangeCheckbox.Checked;

            if (settings.HasChanged)
                Settings.SaveInstanceToDisk();
            
            Close();
        }

        private void CancelCloseButton_Click(object sender, EventArgs e) => Close();
    }
}

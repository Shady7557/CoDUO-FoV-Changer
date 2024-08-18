using System;

namespace CoDUO_FoV_Changer
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
            useSteamOverlayCheckbox.Checked = settings.UseSteamOverlay;
        }

        private void SaveRestartAppButton_Click(object sender, EventArgs e)
        {
            settings.TrackGameTime = GameTimeCheckbox.Checked;
            settings.LaunchWhenSelectedExeChanged = startIfChangeCheckbox.Checked;
            settings.UseSteamOverlay = useSteamOverlayCheckbox.Checked;

            if (settings.HasChanged)
                Settings.SaveInstanceToDisk();
            
            Close();
        }

        private void CancelCloseButton_Click(object sender, EventArgs e) => Close();
    }
}

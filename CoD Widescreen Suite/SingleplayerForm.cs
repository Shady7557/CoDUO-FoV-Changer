using System;

namespace CoD_Widescreen_Suite
{
    public partial class SingleplayerForm : ExtendedForm
    {

        public float GetCurrentFov()
        {
            return GetInstance<MainForm>().CurrentFoV;
        }

        public SingleplayerForm()
        {
            InitializeComponent();
        }

        private void applyCfgBtn_Click(object sender, EventArgs e)
        {
            // Get current Field of View from MainForm and then write to the single player configs for both CoD and UO (only if config file already exists)

            // Get the current Field of View from MainForm
            var fov = GetCurrentFov();

            GameConfig.UpdateConfigValue("cg_fov", fov.ToString(), GameConfig.GameType.CoDSP);
            GameConfig.UpdateConfigValue("cg_fov", fov.ToString(), GameConfig.GameType.CoDUOSP);

        }
    }
}

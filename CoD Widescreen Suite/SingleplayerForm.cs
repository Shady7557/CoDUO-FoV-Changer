using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerExtensions;

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

        private async void applyCfgBtn_Click(object sender, EventArgs e)
        {            
            // Get current Field of View from MainForm
            // and then write to the single player configs
            // for both CoD and UO (only if config file already exists).

            // Get the current Field of View from MainForm
            var fov = GetCurrentFov();

            // Write desired FOV to cg_fov in both game configs.

            await Task.Run(() =>
            {
                GameConfig.UpdateConfigValue("cg_fov", fov.ToString(), GameConfig.GameType.CoDSP);
                GameConfig.UpdateConfigValue("cg_fov", fov.ToString(), GameConfig.GameType.CoDUOSP);
            });

            // Update FOV labels to reflect changes.
            UpdateFovLabels();
            ShowChangesCheckBoxAndLabel(TimeSpan.FromSeconds(1));
        }

        private void SingleplayerForm_Load(object sender, EventArgs e)
        {
            UpdateFovLabels();
            HideChangesCheckBoxAndLabel();
        }

        private void ShowChangesCheckBoxAndLabel(TimeSpan hideIn = default)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                appliedChangesLbl.Visible = true;
                greenCheckPb.Visible = true;
            });

            if (hideIn.TotalSeconds > 0)
                TimerEx.Once(hideIn, () => HideChangesCheckBoxAndLabel());
        }

        private void HideChangesCheckBoxAndLabel()
        {
            Console.WriteLine(nameof(HideChangesCheckBoxAndLabel));
            BeginInvoke((MethodInvoker)delegate
            {
                appliedChangesLbl.Visible = false;
                greenCheckPb.Visible = false;
            });
        }

        private void UpdateFovLabels()
        {
            BeginInvoke((MethodInvoker)delegate 
            {
                codFovLbl.Text = GameConfig.GetConfigValue("cg_fov", GameConfig.GameType.CoDSP);
                uoFovLabel.Text = GameConfig.GetConfigValue("cg_fov", GameConfig.GameType.CoDUOSP);
            });
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

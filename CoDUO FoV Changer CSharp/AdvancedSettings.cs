using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer_CSharp
{
    public partial class AdvancedSettings : Form
    {
        Settings settings = Settings.Instance;
        public AdvancedSettings()
        {
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
                DatabaseFile.Write<Settings>(settings, MainForm.settingsPath);
                Application.Restart();
            }
            else Close();
        }

        private void CancelCloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

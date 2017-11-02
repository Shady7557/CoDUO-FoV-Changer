using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class ChangelogForm : Form
    {
        public ChangelogForm() => InitializeComponent();

        private void GetChangelog()
        {
            try
            {
                var response = WebRequest.Create("https://drive.google.com/uc?export=download&id=0B0nCag_Hp76za3Y3dW9KYU5kQlE")?.GetResponse() ?? null;
                var respFinal = new StreamReader(response?.GetResponseStream() ?? null)?.ReadToEnd() ?? string.Empty;
                var str = (!string.IsNullOrEmpty(respFinal)) ? respFinal : "Changelog response was empty!";
                textBox1.BeginInvoke((MethodInvoker)delegate () { textBox1.Text = str; });
            }
            catch (Exception ex)
            {
                textBox1.BeginInvoke((MethodInvoker)delegate () { textBox1.Text = "Failed to get changelog: " + ex.Message + " (read log for more info)"; });
                MainForm.WriteLog("Unable to get changelog: " + ex.Message + Environment.NewLine + ex.ToString());
            }
        }

        private void ChangelogForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Getting changelog...";
            Task.Run(() => GetChangelog());
            button1.Select();
        }

        private void button1_Click(object sender, EventArgs e) => Close();
    }
}

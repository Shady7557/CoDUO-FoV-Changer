using CurtLog;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class ChangelogForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        } //makes the loading look less shitty

        public static ChangelogForm Instance;
        public ChangelogForm()
        {
            Instance = this;
            InitializeComponent();
        }

        private void GetChangelog()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://drive.google.com/uc?export=download&id=0B0nCag_Hp76za3Y3dW9KYU5kQlE");
                request.Timeout = 1000;
                using (var stream = request.GetResponse().GetResponseStream())
                {
                    using (var readResponse = new StreamReader(stream))
                    {
                        var responseStr = readResponse.ReadToEnd();
                        var str = !string.IsNullOrEmpty(responseStr) ? responseStr : "Changelog response was empty!";
                        textBox1.BeginInvoke((MethodInvoker)delegate () { textBox1.Text = str; });
                    }
                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout || ex.Status == WebExceptionStatus.NameResolutionFailure)
            {
                Console.WriteLine(ex.ToString());
                textBox1.BeginInvoke((MethodInvoker)delegate () { textBox1.Text = "Failed to get changelog: " + ex.Message + " (read log for more info)"; });
                Log.WriteLine("Unable to get changelog: " + ex.Message + Environment.NewLine + ex.ToString());
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

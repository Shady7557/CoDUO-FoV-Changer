using CoDRegistryExtensions;
using ShadyPool;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class AboutForm : ExtendedForm
    {

        public AboutForm() => InitializeComponent();

        private void AboutForm_Load(object sender, EventArgs e)
        {
            var sb = Pool.Get<StringBuilder>();
            try 
            {
                appVersionLbl.Text = sb.Clear().Append(Application.ProductVersion).Append(" (HF ").Append(HotfixVersion.HOTFIX).Append(")").ToString();

                gameVersionLbl.Text = sb.Clear().Append(string.IsNullOrWhiteSpace(CodRex.GameVersion) ? "Unknown" : CodRex.GameVersion).ToString();
            }
            finally { Pool.Free(ref sb); }
        }

        private void StartBmcLink() => Process.Start("https://buymeacoffee.com/shady757");
        

        private void StartDiscordLink() => Process.Start("https://discord.gg/DUCnZhZ");
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => StartBmcLink();
        private void bmcPicbox_Click(object sender, EventArgs e) => StartBmcLink();
        

        private void bmcPicBox_MouseEnter(object sender, EventArgs e) => Cursor = Cursors.Hand;

        private void bmcPicBox_MouseLeave(object sender, EventArgs e) => Cursor = Cursors.Default;

        private void closeButton_Click(object sender, EventArgs e) => Close();

        private void discordPicBox_Click(object sender, EventArgs e) => StartDiscordLink();

        private void discordPicBox_MouseEnter(object sender, EventArgs e) => Cursor = Cursors.Hand;

        private void discordPicBox_MouseLeave(object sender, EventArgs e) => Cursor = Cursors.Default;

        private void codpmLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://cod.pm/");
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://cod.pm/faq#f42e");
        }
    }
}

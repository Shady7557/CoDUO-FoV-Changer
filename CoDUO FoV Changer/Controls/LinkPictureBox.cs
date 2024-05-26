using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public partial class LinkPictureBox : PictureBox
    {
        //a pb class that changes cursor & has a URL field for clickability

        /// <summary>
        /// The link that will be opened when this control is clicked.
        /// </summary>
        public string UrlResource { get; set; } = string.Empty;

        public LinkPictureBox()
        {
            InitializeComponent();
            Click += OnPictureBoxClicked;
            MouseEnter += LinkPictureBox_MouseEnter;
            MouseLeave += LinkPictureBox_MouseLeave;
        }

        private void LinkPictureBox_MouseLeave(object sender, EventArgs e) => Cursor = Cursors.Default;
        private void LinkPictureBox_MouseEnter(object sender, EventArgs e) => Cursor = Cursors.Hand;

        private void OnPictureBoxClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UrlResource))
                return;

            Process.Start(UrlResource);
        }

    }
}

﻿namespace CoD_Widescreen_Suite
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.gameVersionLbl = new System.Windows.Forms.Label();
            this.appVersionLbl = new System.Windows.Forms.Label();
            this.displayGameVerLbl = new System.Windows.Forms.Label();
            this.displayAppVerLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.aboutTextLabel = new System.Windows.Forms.Label();
            this.bmcLinkLabel = new System.Windows.Forms.LinkLabel();
            this.closeButton = new System.Windows.Forms.Button();
            this.createdByLbl = new System.Windows.Forms.Label();
            this.discordPicBox = new CoD_Widescreen_Suite.LinkPictureBox();
            this.bmcPicBox = new CoD_Widescreen_Suite.LinkPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.discordPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmcPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gameVersionLbl
            // 
            this.gameVersionLbl.AutoSize = true;
            this.gameVersionLbl.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameVersionLbl.Location = new System.Drawing.Point(340, 419);
            this.gameVersionLbl.Name = "gameVersionLbl";
            this.gameVersionLbl.Size = new System.Drawing.Size(37, 13);
            this.gameVersionLbl.TabIndex = 8;
            this.gameVersionLbl.Text = "0.00a";
            // 
            // appVersionLbl
            // 
            this.appVersionLbl.AutoSize = true;
            this.appVersionLbl.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appVersionLbl.Location = new System.Drawing.Point(370, 398);
            this.appVersionLbl.Name = "appVersionLbl";
            this.appVersionLbl.Size = new System.Drawing.Size(79, 13);
            this.appVersionLbl.TabIndex = 7;
            this.appVersionLbl.Text = "0.0.0 (HF 0)";
            // 
            // displayGameVerLbl
            // 
            this.displayGameVerLbl.AutoSize = true;
            this.displayGameVerLbl.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayGameVerLbl.Location = new System.Drawing.Point(246, 419);
            this.displayGameVerLbl.Name = "displayGameVerLbl";
            this.displayGameVerLbl.Size = new System.Drawing.Size(97, 13);
            this.displayGameVerLbl.TabIndex = 6;
            this.displayGameVerLbl.Text = "CoD:UO Version:";
            // 
            // displayAppVerLbl
            // 
            this.displayAppVerLbl.AutoSize = true;
            this.displayAppVerLbl.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayAppVerLbl.Location = new System.Drawing.Point(246, 398);
            this.displayAppVerLbl.Name = "displayAppVerLbl";
            this.displayAppVerLbl.Size = new System.Drawing.Size(127, 13);
            this.displayAppVerLbl.TabIndex = 5;
            this.displayAppVerLbl.Text = "Application Version:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 362);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 3;
            // 
            // aboutTextLabel
            // 
            this.aboutTextLabel.AutoSize = true;
            this.aboutTextLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutTextLabel.Location = new System.Drawing.Point(9, 40);
            this.aboutTextLabel.MaximumSize = new System.Drawing.Size(450, 0);
            this.aboutTextLabel.Name = "aboutTextLabel";
            this.aboutTextLabel.Size = new System.Drawing.Size(445, 260);
            this.aboutTextLabel.TabIndex = 2;
            this.aboutTextLabel.Text = resources.GetString("aboutTextLabel.Text");
            // 
            // bmcLinkLabel
            // 
            this.bmcLinkLabel.AutoSize = true;
            this.bmcLinkLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bmcLinkLabel.LinkColor = System.Drawing.Color.Black;
            this.bmcLinkLabel.Location = new System.Drawing.Point(12, 362);
            this.bmcLinkLabel.Name = "bmcLinkLabel";
            this.bmcLinkLabel.Size = new System.Drawing.Size(336, 28);
            this.bmcLinkLabel.TabIndex = 1;
            this.bmcLinkLabel.TabStop = true;
            this.bmcLinkLabel.Text = "If you wish to support independent developers, \r\nplease consider buying me a coff" +
    "ee!";
            this.bmcLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.DarkGray;
            this.closeButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Black;
            this.closeButton.Location = new System.Drawing.Point(12, 396);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(210, 33);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // createdByLbl
            // 
            this.createdByLbl.AutoSize = true;
            this.createdByLbl.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createdByLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.createdByLbl.Location = new System.Drawing.Point(9, 25);
            this.createdByLbl.Name = "createdByLbl";
            this.createdByLbl.Size = new System.Drawing.Size(280, 15);
            this.createdByLbl.TabIndex = 10;
            this.createdByLbl.Text = "Created with the love of PRISM by Shady";
            // 
            // discordPicBox
            // 
            this.discordPicBox.Image = global::CoD_Widescreen_Suite.Properties.Resources.discord_500x;
            this.discordPicBox.Location = new System.Drawing.Point(243, 303);
            this.discordPicBox.Name = "discordPicBox";
            this.discordPicBox.Size = new System.Drawing.Size(211, 56);
            this.discordPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.discordPicBox.TabIndex = 9;
            this.discordPicBox.TabStop = false;
            this.discordPicBox.UrlResource = "https://discord.gg/DUCnZhZ";
            // 
            // bmcPicBox
            // 
            this.bmcPicBox.Image = global::CoD_Widescreen_Suite.Properties.Resources.bmc_logo_no_background_60x;
            this.bmcPicBox.Location = new System.Drawing.Point(12, 303);
            this.bmcPicBox.Name = "bmcPicBox";
            this.bmcPicBox.Size = new System.Drawing.Size(49, 56);
            this.bmcPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bmcPicBox.TabIndex = 4;
            this.bmcPicBox.TabStop = false;
            this.bmcPicBox.UrlResource = "https://buymeacoffee.com/shady757";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(461, 441);
            this.Controls.Add(this.createdByLbl);
            this.Controls.Add(this.discordPicBox);
            this.Controls.Add(this.gameVersionLbl);
            this.Controls.Add(this.appVersionLbl);
            this.Controls.Add(this.displayGameVerLbl);
            this.Controls.Add(this.displayAppVerLbl);
            this.Controls.Add(this.bmcPicBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.aboutTextLabel);
            this.Controls.Add(this.bmcLinkLabel);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.discordPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bmcPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.LinkLabel bmcLinkLabel;
        private System.Windows.Forms.Label aboutTextLabel;
        private System.Windows.Forms.Label label2;
        private LinkPictureBox bmcPicBox;
        private System.Windows.Forms.Label displayAppVerLbl;
        private System.Windows.Forms.Label displayGameVerLbl;
        private System.Windows.Forms.Label gameVersionLbl;
        private System.Windows.Forms.Label appVersionLbl;
        private LinkPictureBox discordPicBox;
        private System.Windows.Forms.Label createdByLbl;
    }
}
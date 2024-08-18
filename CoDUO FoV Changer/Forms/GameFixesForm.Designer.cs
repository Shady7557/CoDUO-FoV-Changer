namespace CoDUO_FoV_Changer
{
    partial class GameFixesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameFixesForm));
            this.undoDpiButton = new System.Windows.Forms.Button();
            this.dpiFixButton = new System.Windows.Forms.Button();
            this.answerScreenLabel = new System.Windows.Forms.Label();
            this.questionScreenLabel = new System.Windows.Forms.Label();
            this.appliedChangesLbl = new System.Windows.Forms.Label();
            this.greenCheckPb = new System.Windows.Forms.PictureBox();
            this.uoShowFovLabel = new System.Windows.Forms.Label();
            this.vCodShowFovLabel = new System.Windows.Forms.Label();
            this.uoFovLabel = new System.Windows.Forms.Label();
            this.codFovLbl = new System.Windows.Forms.Label();
            this.currentSpFovLabel = new System.Windows.Forms.Label();
            this.applyCfgBtn = new System.Windows.Forms.Button();
            this.answerFovSpLabel = new System.Windows.Forms.Label();
            this.questionFovSpLabel = new System.Windows.Forms.Label();
            this.closeFixesForm = new System.Windows.Forms.Button();
            this.otherSolutionLbl = new System.Windows.Forms.Label();
            this.pcGamingInfoLbl = new System.Windows.Forms.Label();
            this.pcGamingWikiLinkLbl = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.greenCheckPb)).BeginInit();
            this.SuspendLayout();
            // 
            // undoDpiButton
            // 
            this.undoDpiButton.BackColor = System.Drawing.Color.DarkGray;
            this.undoDpiButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.undoDpiButton.ForeColor = System.Drawing.Color.Black;
            this.undoDpiButton.Location = new System.Drawing.Point(438, 272);
            this.undoDpiButton.Name = "undoDpiButton";
            this.undoDpiButton.Size = new System.Drawing.Size(356, 40);
            this.undoDpiButton.TabIndex = 14;
            this.undoDpiButton.Text = "Undo DPI/Scaling fixes";
            this.undoDpiButton.UseVisualStyleBackColor = false;
            this.undoDpiButton.Click += new System.EventHandler(this.undoDpiButton_Click);
            // 
            // dpiFixButton
            // 
            this.dpiFixButton.BackColor = System.Drawing.Color.DarkGray;
            this.dpiFixButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpiFixButton.ForeColor = System.Drawing.Color.Black;
            this.dpiFixButton.Location = new System.Drawing.Point(438, 226);
            this.dpiFixButton.Name = "dpiFixButton";
            this.dpiFixButton.Size = new System.Drawing.Size(356, 40);
            this.dpiFixButton.TabIndex = 13;
            this.dpiFixButton.Text = "Apply DPI/Scaling fixes";
            this.dpiFixButton.UseVisualStyleBackColor = false;
            this.dpiFixButton.Click += new System.EventHandler(this.dpiFixButton_Click);
            // 
            // answerScreenLabel
            // 
            this.answerScreenLabel.AutoSize = true;
            this.answerScreenLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answerScreenLabel.ForeColor = System.Drawing.Color.PeachPuff;
            this.answerScreenLabel.Location = new System.Drawing.Point(435, 178);
            this.answerScreenLabel.Name = "answerScreenLabel";
            this.answerScreenLabel.Size = new System.Drawing.Size(336, 45);
            this.answerScreenLabel.TabIndex = 12;
            this.answerScreenLabel.Text = "A. Windows Scaling! You can manually fix this, \r\nor simply click the button below" +
    " and \r\nit will apply the fix to all game executables.";
            // 
            // questionScreenLabel
            // 
            this.questionScreenLabel.AutoSize = true;
            this.questionScreenLabel.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionScreenLabel.ForeColor = System.Drawing.Color.Lime;
            this.questionScreenLabel.Location = new System.Drawing.Point(435, 154);
            this.questionScreenLabel.Name = "questionScreenLabel";
            this.questionScreenLabel.Size = new System.Drawing.Size(203, 15);
            this.questionScreenLabel.TabIndex = 11;
            this.questionScreenLabel.Text = "Q. Why is my screen cut off?";
            // 
            // appliedChangesLbl
            // 
            this.appliedChangesLbl.AutoSize = true;
            this.appliedChangesLbl.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appliedChangesLbl.Location = new System.Drawing.Point(592, 77);
            this.appliedChangesLbl.Name = "appliedChangesLbl";
            this.appliedChangesLbl.Size = new System.Drawing.Size(202, 24);
            this.appliedChangesLbl.TabIndex = 10;
            this.appliedChangesLbl.Text = "Changes applied!";
            // 
            // greenCheckPb
            // 
            this.greenCheckPb.Image = global::CoDUO_FoV_Changer.Properties.Resources.success_check;
            this.greenCheckPb.Location = new System.Drawing.Point(438, 30);
            this.greenCheckPb.Name = "greenCheckPb";
            this.greenCheckPb.Size = new System.Drawing.Size(155, 121);
            this.greenCheckPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.greenCheckPb.TabIndex = 9;
            this.greenCheckPb.TabStop = false;
            // 
            // uoShowFovLabel
            // 
            this.uoShowFovLabel.AutoSize = true;
            this.uoShowFovLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uoShowFovLabel.Location = new System.Drawing.Point(12, 215);
            this.uoShowFovLabel.Name = "uoShowFovLabel";
            this.uoShowFovLabel.Size = new System.Drawing.Size(28, 15);
            this.uoShowFovLabel.TabIndex = 8;
            this.uoShowFovLabel.Text = "UO:";
            // 
            // vCodShowFovLabel
            // 
            this.vCodShowFovLabel.AutoSize = true;
            this.vCodShowFovLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vCodShowFovLabel.Location = new System.Drawing.Point(12, 200);
            this.vCodShowFovLabel.Name = "vCodShowFovLabel";
            this.vCodShowFovLabel.Size = new System.Drawing.Size(42, 15);
            this.vCodShowFovLabel.TabIndex = 7;
            this.vCodShowFovLabel.Text = "VCoD:";
            // 
            // uoFovLabel
            // 
            this.uoFovLabel.AutoSize = true;
            this.uoFovLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uoFovLabel.Location = new System.Drawing.Point(60, 215);
            this.uoFovLabel.Name = "uoFovLabel";
            this.uoFovLabel.Size = new System.Drawing.Size(84, 15);
            this.uoFovLabel.TabIndex = 6;
            this.uoFovLabel.Text = "{SP_FOV_UO}";
            // 
            // codFovLbl
            // 
            this.codFovLbl.AutoSize = true;
            this.codFovLbl.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codFovLbl.Location = new System.Drawing.Point(60, 200);
            this.codFovLbl.Name = "codFovLbl";
            this.codFovLbl.Size = new System.Drawing.Size(91, 15);
            this.codFovLbl.TabIndex = 5;
            this.codFovLbl.Text = "{SP_FOV_COD}";
            // 
            // currentSpFovLabel
            // 
            this.currentSpFovLabel.AutoSize = true;
            this.currentSpFovLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentSpFovLabel.Location = new System.Drawing.Point(12, 167);
            this.currentSpFovLabel.Name = "currentSpFovLabel";
            this.currentSpFovLabel.Size = new System.Drawing.Size(259, 15);
            this.currentSpFovLabel.TabIndex = 4;
            this.currentSpFovLabel.Text = "Current FOV in single player config:";
            // 
            // applyCfgBtn
            // 
            this.applyCfgBtn.BackColor = System.Drawing.Color.DarkGray;
            this.applyCfgBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyCfgBtn.ForeColor = System.Drawing.Color.Black;
            this.applyCfgBtn.Location = new System.Drawing.Point(12, 246);
            this.applyCfgBtn.Name = "applyCfgBtn";
            this.applyCfgBtn.Size = new System.Drawing.Size(155, 40);
            this.applyCfgBtn.TabIndex = 3;
            this.applyCfgBtn.Text = "Apply FOV to single player config";
            this.applyCfgBtn.UseVisualStyleBackColor = false;
            this.applyCfgBtn.Click += new System.EventHandler(this.applyCfgBtn_Click);
            // 
            // answerFovSpLabel
            // 
            this.answerFovSpLabel.AutoSize = true;
            this.answerFovSpLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answerFovSpLabel.ForeColor = System.Drawing.Color.PeachPuff;
            this.answerFovSpLabel.Location = new System.Drawing.Point(12, 31);
            this.answerFovSpLabel.Name = "answerFovSpLabel";
            this.answerFovSpLabel.Size = new System.Drawing.Size(420, 120);
            this.answerFovSpLabel.TabIndex = 2;
            this.answerFovSpLabel.Text = resources.GetString("answerFovSpLabel.Text");
            // 
            // questionFovSpLabel
            // 
            this.questionFovSpLabel.AutoSize = true;
            this.questionFovSpLabel.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionFovSpLabel.ForeColor = System.Drawing.Color.Lime;
            this.questionFovSpLabel.Location = new System.Drawing.Point(12, 9);
            this.questionFovSpLabel.Name = "questionFovSpLabel";
            this.questionFovSpLabel.Size = new System.Drawing.Size(371, 15);
            this.questionFovSpLabel.TabIndex = 1;
            this.questionFovSpLabel.Text = "Q. How do I change my Field of View in singleplayer?";
            // 
            // closeFixesForm
            // 
            this.closeFixesForm.BackColor = System.Drawing.Color.DarkGray;
            this.closeFixesForm.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeFixesForm.ForeColor = System.Drawing.Color.Black;
            this.closeFixesForm.Location = new System.Drawing.Point(12, 356);
            this.closeFixesForm.Name = "closeFixesForm";
            this.closeFixesForm.Size = new System.Drawing.Size(356, 40);
            this.closeFixesForm.TabIndex = 0;
            this.closeFixesForm.Text = "Close";
            this.closeFixesForm.UseVisualStyleBackColor = false;
            this.closeFixesForm.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // otherSolutionLbl
            // 
            this.otherSolutionLbl.AutoSize = true;
            this.otherSolutionLbl.Font = new System.Drawing.Font("Consolas", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otherSolutionLbl.ForeColor = System.Drawing.Color.Lime;
            this.otherSolutionLbl.Location = new System.Drawing.Point(374, 322);
            this.otherSolutionLbl.Name = "otherSolutionLbl";
            this.otherSolutionLbl.Size = new System.Drawing.Size(119, 15);
            this.otherSolutionLbl.TabIndex = 15;
            this.otherSolutionLbl.Text = "Other Solutions:";
            // 
            // pcGamingInfoLbl
            // 
            this.pcGamingInfoLbl.AutoSize = true;
            this.pcGamingInfoLbl.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pcGamingInfoLbl.ForeColor = System.Drawing.Color.PeachPuff;
            this.pcGamingInfoLbl.Location = new System.Drawing.Point(374, 337);
            this.pcGamingInfoLbl.Name = "pcGamingInfoLbl";
            this.pcGamingInfoLbl.Size = new System.Drawing.Size(462, 45);
            this.pcGamingInfoLbl.TabIndex = 16;
            this.pcGamingInfoLbl.Text = "PCGamingWiki is an excellent resource for many games, \r\nincluding Call of Duty (2" +
    "003) and Call of Duty: United Offensive.\r\nCheck out the article for these games " +
    "here:";
            // 
            // pcGamingWikiLinkLbl
            // 
            this.pcGamingWikiLinkLbl.AutoSize = true;
            this.pcGamingWikiLinkLbl.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pcGamingWikiLinkLbl.Location = new System.Drawing.Point(374, 382);
            this.pcGamingWikiLinkLbl.Name = "pcGamingWikiLinkLbl";
            this.pcGamingWikiLinkLbl.Size = new System.Drawing.Size(196, 14);
            this.pcGamingWikiLinkLbl.TabIndex = 17;
            this.pcGamingWikiLinkLbl.TabStop = true;
            this.pcGamingWikiLinkLbl.Text = "Call of Duty - PCGamingWiki";
            this.pcGamingWikiLinkLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // GameFixesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(836, 408);
            this.Controls.Add(this.pcGamingWikiLinkLbl);
            this.Controls.Add(this.pcGamingInfoLbl);
            this.Controls.Add(this.otherSolutionLbl);
            this.Controls.Add(this.undoDpiButton);
            this.Controls.Add(this.dpiFixButton);
            this.Controls.Add(this.answerScreenLabel);
            this.Controls.Add(this.questionScreenLabel);
            this.Controls.Add(this.appliedChangesLbl);
            this.Controls.Add(this.greenCheckPb);
            this.Controls.Add(this.uoShowFovLabel);
            this.Controls.Add(this.vCodShowFovLabel);
            this.Controls.Add(this.uoFovLabel);
            this.Controls.Add(this.codFovLbl);
            this.Controls.Add(this.currentSpFovLabel);
            this.Controls.Add(this.applyCfgBtn);
            this.Controls.Add(this.answerFovSpLabel);
            this.Controls.Add(this.questionFovSpLabel);
            this.Controls.Add(this.closeFixesForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "GameFixesForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Fixes/Solutions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameFixesForm_FormClosing);
            this.Load += new System.EventHandler(this.SingleplayerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.greenCheckPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeFixesForm;
        private System.Windows.Forms.Label questionFovSpLabel;
        private System.Windows.Forms.Label answerFovSpLabel;
        private System.Windows.Forms.Button applyCfgBtn;
        private System.Windows.Forms.Label currentSpFovLabel;
        private System.Windows.Forms.Label codFovLbl;
        private System.Windows.Forms.Label uoFovLabel;
        private System.Windows.Forms.Label vCodShowFovLabel;
        private System.Windows.Forms.Label uoShowFovLabel;
        private System.Windows.Forms.PictureBox greenCheckPb;
        private System.Windows.Forms.Label appliedChangesLbl;
        private System.Windows.Forms.Label answerScreenLabel;
        private System.Windows.Forms.Label questionScreenLabel;
        private System.Windows.Forms.Button dpiFixButton;
        private System.Windows.Forms.Button undoDpiButton;
        private System.Windows.Forms.Label otherSolutionLbl;
        private System.Windows.Forms.Label pcGamingInfoLbl;
        private System.Windows.Forms.LinkLabel pcGamingWikiLinkLbl;
    }
}
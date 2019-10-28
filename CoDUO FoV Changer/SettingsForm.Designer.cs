namespace CoDUO_FoV_Changer
{
    partial class SettingsForm
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
            this.CDKeyLabel = new System.Windows.Forms.Label();
            this.GameVersLabel = new System.Windows.Forms.Label();
            this.AppVersLabel = new System.Windows.Forms.Label();
            this.InformationLabel = new System.Windows.Forms.Label();
            this.CloseSettingsButton = new System.Windows.Forms.Button();
            this.RestartAppButton = new System.Windows.Forms.Button();
            this.ButtonBrowseGameFiles = new System.Windows.Forms.Button();
            this.ButtonSettingsAdvanced = new System.Windows.Forms.Button();
            this.hotKeysButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CDKeyLabel
            // 
            this.CDKeyLabel.AutoSize = true;
            this.CDKeyLabel.BackColor = System.Drawing.Color.Transparent;
            this.CDKeyLabel.ForeColor = System.Drawing.Color.Transparent;
            this.CDKeyLabel.Location = new System.Drawing.Point(12, 311);
            this.CDKeyLabel.Name = "CDKeyLabel";
            this.CDKeyLabel.Size = new System.Drawing.Size(46, 13);
            this.CDKeyLabel.TabIndex = 56;
            this.CDKeyLabel.Text = "CD-Key:";
            this.CDKeyLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CDKeyLabel_MouseDown);
            // 
            // GameVersLabel
            // 
            this.GameVersLabel.AutoSize = true;
            this.GameVersLabel.BackColor = System.Drawing.Color.Transparent;
            this.GameVersLabel.ForeColor = System.Drawing.Color.Transparent;
            this.GameVersLabel.Location = new System.Drawing.Point(12, 298);
            this.GameVersLabel.Name = "GameVersLabel";
            this.GameVersLabel.Size = new System.Drawing.Size(115, 13);
            this.GameVersLabel.TabIndex = 55;
            this.GameVersLabel.Text = "CoD:UO Version: 0000";
            // 
            // AppVersLabel
            // 
            this.AppVersLabel.AutoSize = true;
            this.AppVersLabel.BackColor = System.Drawing.Color.Transparent;
            this.AppVersLabel.ForeColor = System.Drawing.Color.Transparent;
            this.AppVersLabel.Location = new System.Drawing.Point(12, 285);
            this.AppVersLabel.Name = "AppVersLabel";
            this.AppVersLabel.Size = new System.Drawing.Size(127, 13);
            this.AppVersLabel.TabIndex = 54;
            this.AppVersLabel.Text = "Application Version: 0000";
            // 
            // InformationLabel
            // 
            this.InformationLabel.AutoSize = true;
            this.InformationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationLabel.ForeColor = System.Drawing.Color.Transparent;
            this.InformationLabel.Location = new System.Drawing.Point(12, 270);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(152, 15);
            this.InformationLabel.TabIndex = 52;
            this.InformationLabel.Text = "Additional Information:";
            // 
            // CloseSettingsButton
            // 
            this.CloseSettingsButton.BackColor = System.Drawing.Color.DarkGray;
            this.CloseSettingsButton.ForeColor = System.Drawing.Color.Transparent;
            this.CloseSettingsButton.Location = new System.Drawing.Point(15, 245);
            this.CloseSettingsButton.Name = "CloseSettingsButton";
            this.CloseSettingsButton.Size = new System.Drawing.Size(183, 24);
            this.CloseSettingsButton.TabIndex = 57;
            this.CloseSettingsButton.Text = "Close";
            this.CloseSettingsButton.UseVisualStyleBackColor = false;
            this.CloseSettingsButton.Click += new System.EventHandler(this.CloseSettingsButton_Click);
            // 
            // RestartAppButton
            // 
            this.RestartAppButton.BackColor = System.Drawing.Color.DarkGray;
            this.RestartAppButton.ForeColor = System.Drawing.Color.Transparent;
            this.RestartAppButton.Location = new System.Drawing.Point(15, 216);
            this.RestartAppButton.Name = "RestartAppButton";
            this.RestartAppButton.Size = new System.Drawing.Size(183, 24);
            this.RestartAppButton.TabIndex = 64;
            this.RestartAppButton.Text = "Restart App";
            this.RestartAppButton.UseVisualStyleBackColor = false;
            this.RestartAppButton.Click += new System.EventHandler(this.RestartAppButton_Click);
            // 
            // ButtonBrowseGameFiles
            // 
            this.ButtonBrowseGameFiles.BackColor = System.Drawing.Color.DarkGray;
            this.ButtonBrowseGameFiles.ForeColor = System.Drawing.Color.Transparent;
            this.ButtonBrowseGameFiles.Location = new System.Drawing.Point(15, 8);
            this.ButtonBrowseGameFiles.Name = "ButtonBrowseGameFiles";
            this.ButtonBrowseGameFiles.Size = new System.Drawing.Size(183, 24);
            this.ButtonBrowseGameFiles.TabIndex = 65;
            this.ButtonBrowseGameFiles.Text = "Browse Local Game Files";
            this.ButtonBrowseGameFiles.UseVisualStyleBackColor = false;
            this.ButtonBrowseGameFiles.Click += new System.EventHandler(this.ButtonBrowseGameFiles_Click);
            // 
            // ButtonSettingsAdvanced
            // 
            this.ButtonSettingsAdvanced.BackColor = System.Drawing.Color.DarkGray;
            this.ButtonSettingsAdvanced.ForeColor = System.Drawing.Color.Transparent;
            this.ButtonSettingsAdvanced.Location = new System.Drawing.Point(15, 37);
            this.ButtonSettingsAdvanced.Name = "ButtonSettingsAdvanced";
            this.ButtonSettingsAdvanced.Size = new System.Drawing.Size(183, 24);
            this.ButtonSettingsAdvanced.TabIndex = 66;
            this.ButtonSettingsAdvanced.Text = "Advanced Settings";
            this.ButtonSettingsAdvanced.UseVisualStyleBackColor = false;
            this.ButtonSettingsAdvanced.Click += new System.EventHandler(this.ButtonSettingsAdvanced_Click);
            // 
            // hotKeysButton
            // 
            this.hotKeysButton.BackColor = System.Drawing.Color.DarkGray;
            this.hotKeysButton.ForeColor = System.Drawing.Color.Transparent;
            this.hotKeysButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.hotKeysButton.Location = new System.Drawing.Point(15, 66);
            this.hotKeysButton.Name = "hotKeysButton";
            this.hotKeysButton.Size = new System.Drawing.Size(183, 24);
            this.hotKeysButton.TabIndex = 67;
            this.hotKeysButton.Text = "Hotkeys";
            this.hotKeysButton.UseVisualStyleBackColor = false;
            this.hotKeysButton.Click += new System.EventHandler(this.hotKeysButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(224, 340);
            this.Controls.Add(this.hotKeysButton);
            this.Controls.Add(this.ButtonSettingsAdvanced);
            this.Controls.Add(this.ButtonBrowseGameFiles);
            this.Controls.Add(this.RestartAppButton);
            this.Controls.Add(this.CloseSettingsButton);
            this.Controls.Add(this.CDKeyLabel);
            this.Controls.Add(this.GameVersLabel);
            this.Controls.Add(this.AppVersLabel);
            this.Controls.Add(this.InformationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label CDKeyLabel;
        internal System.Windows.Forms.Label GameVersLabel;
        internal System.Windows.Forms.Label AppVersLabel;
        internal System.Windows.Forms.Label InformationLabel;
        internal System.Windows.Forms.Button CloseSettingsButton;
        internal System.Windows.Forms.Button RestartAppButton;
        internal System.Windows.Forms.Button ButtonBrowseGameFiles;
        internal System.Windows.Forms.Button ButtonSettingsAdvanced;
        internal System.Windows.Forms.Button hotKeysButton;
    }
}
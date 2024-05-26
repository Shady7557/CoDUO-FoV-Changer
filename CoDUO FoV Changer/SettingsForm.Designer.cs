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
            this.GameVersLabel = new System.Windows.Forms.Label();
            this.AppVersLabel = new System.Windows.Forms.Label();
            this.InformationLabel = new System.Windows.Forms.Label();
            this.CloseSettingsButton = new System.Windows.Forms.Button();
            this.ButtonBrowseGameFiles = new System.Windows.Forms.Button();
            this.ButtonSettingsAdvanced = new System.Windows.Forms.Button();
            this.hotKeysButton = new System.Windows.Forms.Button();
            this.cdKeyManagerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameVersLabel
            // 
            this.GameVersLabel.AutoSize = true;
            this.GameVersLabel.BackColor = System.Drawing.Color.Transparent;
            this.GameVersLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameVersLabel.ForeColor = System.Drawing.Color.Black;
            this.GameVersLabel.Location = new System.Drawing.Point(12, 200);
            this.GameVersLabel.Name = "GameVersLabel";
            this.GameVersLabel.Size = new System.Drawing.Size(127, 13);
            this.GameVersLabel.TabIndex = 55;
            this.GameVersLabel.Text = "CoD:UO Version: 0000";
            // 
            // AppVersLabel
            // 
            this.AppVersLabel.AutoSize = true;
            this.AppVersLabel.BackColor = System.Drawing.Color.Transparent;
            this.AppVersLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppVersLabel.ForeColor = System.Drawing.Color.Black;
            this.AppVersLabel.Location = new System.Drawing.Point(12, 187);
            this.AppVersLabel.Name = "AppVersLabel";
            this.AppVersLabel.Size = new System.Drawing.Size(157, 13);
            this.AppVersLabel.TabIndex = 54;
            this.AppVersLabel.Text = "Application Version: 0000";
            // 
            // InformationLabel
            // 
            this.InformationLabel.AutoSize = true;
            this.InformationLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationLabel.ForeColor = System.Drawing.Color.Black;
            this.InformationLabel.Location = new System.Drawing.Point(12, 172);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(168, 14);
            this.InformationLabel.TabIndex = 52;
            this.InformationLabel.Text = "Additional Information:";
            // 
            // CloseSettingsButton
            // 
            this.CloseSettingsButton.BackColor = System.Drawing.Color.DarkGray;
            this.CloseSettingsButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseSettingsButton.ForeColor = System.Drawing.Color.Black;
            this.CloseSettingsButton.Location = new System.Drawing.Point(15, 145);
            this.CloseSettingsButton.Name = "CloseSettingsButton";
            this.CloseSettingsButton.Size = new System.Drawing.Size(183, 24);
            this.CloseSettingsButton.TabIndex = 57;
            this.CloseSettingsButton.Text = "Close";
            this.CloseSettingsButton.UseVisualStyleBackColor = false;
            this.CloseSettingsButton.Click += new System.EventHandler(this.CloseSettingsButton_Click);
            // 
            // ButtonBrowseGameFiles
            // 
            this.ButtonBrowseGameFiles.BackColor = System.Drawing.Color.DarkGray;
            this.ButtonBrowseGameFiles.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonBrowseGameFiles.ForeColor = System.Drawing.Color.Black;
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
            this.ButtonSettingsAdvanced.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSettingsAdvanced.ForeColor = System.Drawing.Color.Black;
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
            this.hotKeysButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotKeysButton.ForeColor = System.Drawing.Color.Black;
            this.hotKeysButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.hotKeysButton.Location = new System.Drawing.Point(15, 66);
            this.hotKeysButton.Name = "hotKeysButton";
            this.hotKeysButton.Size = new System.Drawing.Size(183, 24);
            this.hotKeysButton.TabIndex = 67;
            this.hotKeysButton.Text = "Hotkeys";
            this.hotKeysButton.UseVisualStyleBackColor = false;
            this.hotKeysButton.Click += new System.EventHandler(this.hotKeysButton_Click);
            // 
            // cdKeyManagerButton
            // 
            this.cdKeyManagerButton.BackColor = System.Drawing.Color.DarkGray;
            this.cdKeyManagerButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdKeyManagerButton.ForeColor = System.Drawing.Color.Black;
            this.cdKeyManagerButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cdKeyManagerButton.Location = new System.Drawing.Point(15, 96);
            this.cdKeyManagerButton.Name = "cdKeyManagerButton";
            this.cdKeyManagerButton.Size = new System.Drawing.Size(183, 24);
            this.cdKeyManagerButton.TabIndex = 68;
            this.cdKeyManagerButton.Text = "CD-Key Manager";
            this.cdKeyManagerButton.UseVisualStyleBackColor = false;
            this.cdKeyManagerButton.Click += new System.EventHandler(this.cdKeyManagerButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(217, 216);
            this.Controls.Add(this.cdKeyManagerButton);
            this.Controls.Add(this.hotKeysButton);
            this.Controls.Add(this.ButtonSettingsAdvanced);
            this.Controls.Add(this.ButtonBrowseGameFiles);
            this.Controls.Add(this.CloseSettingsButton);
            this.Controls.Add(this.GameVersLabel);
            this.Controls.Add(this.AppVersLabel);
            this.Controls.Add(this.InformationLabel);
            this.DoubleBuffered = true;
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
        internal System.Windows.Forms.Label GameVersLabel;
        internal System.Windows.Forms.Label AppVersLabel;
        internal System.Windows.Forms.Label InformationLabel;
        internal System.Windows.Forms.Button CloseSettingsButton;
        internal System.Windows.Forms.Button ButtonBrowseGameFiles;
        internal System.Windows.Forms.Button ButtonSettingsAdvanced;
        internal System.Windows.Forms.Button hotKeysButton;
        internal System.Windows.Forms.Button cdKeyManagerButton;
    }
}
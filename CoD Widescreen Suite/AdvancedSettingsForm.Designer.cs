namespace CoD_Widescreen_Suite
{
    partial class AdvancedSettingsForm
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
            this.GameTimeCheckbox = new System.Windows.Forms.CheckBox();
            this.CancelCloseButton = new System.Windows.Forms.Button();
            this.SaveRestartAppButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameTimeCheckbox
            // 
            this.GameTimeCheckbox.AutoSize = true;
            this.GameTimeCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.GameTimeCheckbox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTimeCheckbox.ForeColor = System.Drawing.Color.Black;
            this.GameTimeCheckbox.Location = new System.Drawing.Point(3, 12);
            this.GameTimeCheckbox.Name = "GameTimeCheckbox";
            this.GameTimeCheckbox.Size = new System.Drawing.Size(134, 17);
            this.GameTimeCheckbox.TabIndex = 21;
            this.GameTimeCheckbox.Text = "Track In-Game Time";
            this.GameTimeCheckbox.UseVisualStyleBackColor = false;
            // 
            // CancelCloseButton
            // 
            this.CancelCloseButton.BackColor = System.Drawing.Color.DarkGray;
            this.CancelCloseButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelCloseButton.Location = new System.Drawing.Point(3, 64);
            this.CancelCloseButton.Name = "CancelCloseButton";
            this.CancelCloseButton.Size = new System.Drawing.Size(128, 22);
            this.CancelCloseButton.TabIndex = 20;
            this.CancelCloseButton.Text = "Cancel";
            this.CancelCloseButton.UseVisualStyleBackColor = false;
            this.CancelCloseButton.Click += new System.EventHandler(this.CancelCloseButton_Click);
            // 
            // SaveRestartAppButton
            // 
            this.SaveRestartAppButton.BackColor = System.Drawing.Color.DarkGray;
            this.SaveRestartAppButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveRestartAppButton.Location = new System.Drawing.Point(3, 35);
            this.SaveRestartAppButton.Name = "SaveRestartAppButton";
            this.SaveRestartAppButton.Size = new System.Drawing.Size(128, 23);
            this.SaveRestartAppButton.TabIndex = 18;
            this.SaveRestartAppButton.Text = "Save And Restart";
            this.SaveRestartAppButton.UseVisualStyleBackColor = false;
            this.SaveRestartAppButton.Click += new System.EventHandler(this.SaveRestartAppButton_Click);
            // 
            // AdvancedSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(140, 93);
            this.ControlBox = false;
            this.Controls.Add(this.GameTimeCheckbox);
            this.Controls.Add(this.CancelCloseButton);
            this.Controls.Add(this.SaveRestartAppButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AdvancedSettingsForm";
            this.ShowIcon = false;
            this.Text = "Advanced Settings";
            this.Load += new System.EventHandler(this.AdvancedSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox GameTimeCheckbox;
        internal System.Windows.Forms.Button CancelCloseButton;
        internal System.Windows.Forms.Button SaveRestartAppButton;
    }
}
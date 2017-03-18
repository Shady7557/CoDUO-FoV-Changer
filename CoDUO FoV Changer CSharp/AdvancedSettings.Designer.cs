﻿namespace CoDUO_FoV_Changer_CSharp
{
    partial class AdvancedSettings
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
            this.DisableUpdateTimerCBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // GameTimeCheckbox
            // 
            this.GameTimeCheckbox.AutoSize = true;
            this.GameTimeCheckbox.Location = new System.Drawing.Point(12, 45);
            this.GameTimeCheckbox.Name = "GameTimeCheckbox";
            this.GameTimeCheckbox.Size = new System.Drawing.Size(123, 17);
            this.GameTimeCheckbox.TabIndex = 21;
            this.GameTimeCheckbox.Text = "Track In-Game Time";
            this.GameTimeCheckbox.UseVisualStyleBackColor = true;
            // 
            // CancelCloseButton
            // 
            this.CancelCloseButton.Location = new System.Drawing.Point(12, 120);
            this.CancelCloseButton.Name = "CancelCloseButton";
            this.CancelCloseButton.Size = new System.Drawing.Size(128, 22);
            this.CancelCloseButton.TabIndex = 20;
            this.CancelCloseButton.Text = "Cancel";
            this.CancelCloseButton.UseVisualStyleBackColor = true;
            this.CancelCloseButton.Click += new System.EventHandler(this.CancelCloseButton_Click);
            // 
            // SaveRestartAppButton
            // 
            this.SaveRestartAppButton.Location = new System.Drawing.Point(12, 91);
            this.SaveRestartAppButton.Name = "SaveRestartAppButton";
            this.SaveRestartAppButton.Size = new System.Drawing.Size(128, 23);
            this.SaveRestartAppButton.TabIndex = 18;
            this.SaveRestartAppButton.Text = "Save And Restart";
            this.SaveRestartAppButton.UseVisualStyleBackColor = true;
            this.SaveRestartAppButton.Click += new System.EventHandler(this.SaveRestartAppButton_Click);
            // 
            // DisableUpdateTimerCBox
            // 
            this.DisableUpdateTimerCBox.AutoSize = true;
            this.DisableUpdateTimerCBox.Location = new System.Drawing.Point(12, 22);
            this.DisableUpdateTimerCBox.Name = "DisableUpdateTimerCBox";
            this.DisableUpdateTimerCBox.Size = new System.Drawing.Size(128, 17);
            this.DisableUpdateTimerCBox.TabIndex = 22;
            this.DisableUpdateTimerCBox.Text = "Disable Update Timer";
            this.DisableUpdateTimerCBox.UseVisualStyleBackColor = true;
            // 
            // AdvancedSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(143, 144);
            this.ControlBox = false;
            this.Controls.Add(this.DisableUpdateTimerCBox);
            this.Controls.Add(this.GameTimeCheckbox);
            this.Controls.Add(this.CancelCloseButton);
            this.Controls.Add(this.SaveRestartAppButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AdvancedSettings";
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
        internal System.Windows.Forms.CheckBox DisableUpdateTimerCBox;
    }
}
namespace CoDUO_FoV_Changer
{
    partial class HotkeysForm
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
            this.hotkeyKeyLbl = new System.Windows.Forms.Label();
            this.FoVUpRadioButton = new System.Windows.Forms.RadioButton();
            this.FoVDownRadioButton = new System.Windows.Forms.RadioButton();
            this.FoVModifierRadioButton = new System.Windows.Forms.RadioButton();
            this.applyKeyButton = new System.Windows.Forms.Button();
            this.hotkeySaveExitButton = new System.Windows.Forms.Button();
            this.hotkeySaveWithoutExitButton = new System.Windows.Forms.Button();
            this.keyPbLabel = new System.Windows.Forms.Label();
            this.hotkeyPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.hotkeyPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // hotkeyKeyLbl
            // 
            this.hotkeyKeyLbl.AutoSize = true;
            this.hotkeyKeyLbl.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotkeyKeyLbl.Location = new System.Drawing.Point(8, 50);
            this.hotkeyKeyLbl.Name = "hotkeyKeyLbl";
            this.hotkeyKeyLbl.Size = new System.Drawing.Size(45, 19);
            this.hotkeyKeyLbl.TabIndex = 0;
            this.hotkeyKeyLbl.Text = "Key:";
            // 
            // FoVUpRadioButton
            // 
            this.FoVUpRadioButton.AutoSize = true;
            this.FoVUpRadioButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FoVUpRadioButton.Location = new System.Drawing.Point(12, 105);
            this.FoVUpRadioButton.Name = "FoVUpRadioButton";
            this.FoVUpRadioButton.Size = new System.Drawing.Size(85, 17);
            this.FoVUpRadioButton.TabIndex = 1;
            this.FoVUpRadioButton.TabStop = true;
            this.FoVUpRadioButton.Text = "FoV Up Key";
            this.FoVUpRadioButton.UseVisualStyleBackColor = true;
            this.FoVUpRadioButton.CheckedChanged += new System.EventHandler(this.FoVUp_CheckedChanged);
            // 
            // FoVDownRadioButton
            // 
            this.FoVDownRadioButton.AutoSize = true;
            this.FoVDownRadioButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FoVDownRadioButton.Location = new System.Drawing.Point(12, 128);
            this.FoVDownRadioButton.Name = "FoVDownRadioButton";
            this.FoVDownRadioButton.Size = new System.Drawing.Size(97, 17);
            this.FoVDownRadioButton.TabIndex = 2;
            this.FoVDownRadioButton.TabStop = true;
            this.FoVDownRadioButton.Text = "FoV Down Key";
            this.FoVDownRadioButton.UseVisualStyleBackColor = true;
            this.FoVDownRadioButton.CheckedChanged += new System.EventHandler(this.FoVDown_CheckedChanged);
            // 
            // FoVModifierRadioButton
            // 
            this.FoVModifierRadioButton.AutoSize = true;
            this.FoVModifierRadioButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FoVModifierRadioButton.Location = new System.Drawing.Point(12, 195);
            this.FoVModifierRadioButton.Name = "FoVModifierRadioButton";
            this.FoVModifierRadioButton.Size = new System.Drawing.Size(97, 17);
            this.FoVModifierRadioButton.TabIndex = 4;
            this.FoVModifierRadioButton.TabStop = true;
            this.FoVModifierRadioButton.Text = "FoV Modifier";
            this.FoVModifierRadioButton.UseVisualStyleBackColor = true;
            this.FoVModifierRadioButton.CheckedChanged += new System.EventHandler(this.FoVModifier_CheckedChanged);
            // 
            // applyKeyButton
            // 
            this.applyKeyButton.BackColor = System.Drawing.Color.DarkGray;
            this.applyKeyButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyKeyButton.Location = new System.Drawing.Point(113, 12);
            this.applyKeyButton.Name = "applyKeyButton";
            this.applyKeyButton.Size = new System.Drawing.Size(132, 25);
            this.applyKeyButton.TabIndex = 6;
            this.applyKeyButton.Text = "Apply current key";
            this.applyKeyButton.UseVisualStyleBackColor = false;
            this.applyKeyButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // hotkeySaveExitButton
            // 
            this.hotkeySaveExitButton.BackColor = System.Drawing.Color.DarkGray;
            this.hotkeySaveExitButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotkeySaveExitButton.Location = new System.Drawing.Point(113, 221);
            this.hotkeySaveExitButton.Name = "hotkeySaveExitButton";
            this.hotkeySaveExitButton.Size = new System.Drawing.Size(132, 25);
            this.hotkeySaveExitButton.TabIndex = 7;
            this.hotkeySaveExitButton.Text = "Save and exit";
            this.hotkeySaveExitButton.UseVisualStyleBackColor = false;
            this.hotkeySaveExitButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // hotkeySaveWithoutExitButton
            // 
            this.hotkeySaveWithoutExitButton.BackColor = System.Drawing.Color.DarkGray;
            this.hotkeySaveWithoutExitButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hotkeySaveWithoutExitButton.Location = new System.Drawing.Point(113, 252);
            this.hotkeySaveWithoutExitButton.Name = "hotkeySaveWithoutExitButton";
            this.hotkeySaveWithoutExitButton.Size = new System.Drawing.Size(132, 25);
            this.hotkeySaveWithoutExitButton.TabIndex = 8;
            this.hotkeySaveWithoutExitButton.Text = "Exit without saving";
            this.hotkeySaveWithoutExitButton.UseVisualStyleBackColor = false;
            this.hotkeySaveWithoutExitButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // keyPbLabel
            // 
            this.keyPbLabel.AutoSize = true;
            this.keyPbLabel.BackColor = System.Drawing.Color.Transparent;
            this.keyPbLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyPbLabel.Location = new System.Drawing.Point(177, 82);
            this.keyPbLabel.Name = "keyPbLabel";
            this.keyPbLabel.Size = new System.Drawing.Size(0, 19);
            this.keyPbLabel.TabIndex = 10;
            this.keyPbLabel.TextChanged += new System.EventHandler(this.Label2_TextChanged);
            // 
            // hotkeyPictureBox
            // 
            this.hotkeyPictureBox.Image = global::CoDUO_FoV_Changer.Properties.Resources.empty_key_big_3;
            this.hotkeyPictureBox.Location = new System.Drawing.Point(163, 81);
            this.hotkeyPictureBox.Name = "hotkeyPictureBox";
            this.hotkeyPictureBox.Size = new System.Drawing.Size(64, 64);
            this.hotkeyPictureBox.TabIndex = 9;
            this.hotkeyPictureBox.TabStop = false;
            // 
            // HotkeysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(249, 289);
            this.Controls.Add(this.keyPbLabel);
            this.Controls.Add(this.hotkeyPictureBox);
            this.Controls.Add(this.hotkeySaveWithoutExitButton);
            this.Controls.Add(this.hotkeySaveExitButton);
            this.Controls.Add(this.applyKeyButton);
            this.Controls.Add(this.FoVModifierRadioButton);
            this.Controls.Add(this.FoVDownRadioButton);
            this.Controls.Add(this.FoVUpRadioButton);
            this.Controls.Add(this.hotkeyKeyLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotkeysForm";
            this.ShowIcon = false;
            this.Text = "Hotkeys";
            this.Load += new System.EventHandler(this.Hotkeys_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hotkeys_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.hotkeyPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hotkeyKeyLbl;
        private System.Windows.Forms.RadioButton FoVUpRadioButton;
        private System.Windows.Forms.RadioButton FoVDownRadioButton;
        private System.Windows.Forms.RadioButton FoVModifierRadioButton;
        private System.Windows.Forms.Button applyKeyButton;
        private System.Windows.Forms.Button hotkeySaveExitButton;
        private System.Windows.Forms.Button hotkeySaveWithoutExitButton;
        private System.Windows.Forms.PictureBox hotkeyPictureBox;
        private System.Windows.Forms.Label keyPbLabel;
    }
}
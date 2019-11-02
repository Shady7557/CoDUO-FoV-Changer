namespace CoDUO_FoV_Changer
{
    partial class Hotkeys
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
            this.label1 = new System.Windows.Forms.Label();
            this.FoVUp = new System.Windows.Forms.RadioButton();
            this.FoVDown = new System.Windows.Forms.RadioButton();
            this.FoVModifier = new System.Windows.Forms.RadioButton();
            this.FogKey = new System.Windows.Forms.RadioButton();
            this.FogModifier = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key:";
            // 
            // FoVUp
            // 
            this.FoVUp.AutoSize = true;
            this.FoVUp.Location = new System.Drawing.Point(12, 105);
            this.FoVUp.Name = "FoVUp";
            this.FoVUp.Size = new System.Drawing.Size(82, 17);
            this.FoVUp.TabIndex = 1;
            this.FoVUp.TabStop = true;
            this.FoVUp.Text = "FoV Up Key";
            this.FoVUp.UseVisualStyleBackColor = true;
            this.FoVUp.CheckedChanged += new System.EventHandler(this.FoVUp_CheckedChanged);
            // 
            // FoVDown
            // 
            this.FoVDown.AutoSize = true;
            this.FoVDown.Location = new System.Drawing.Point(12, 128);
            this.FoVDown.Name = "FoVDown";
            this.FoVDown.Size = new System.Drawing.Size(96, 17);
            this.FoVDown.TabIndex = 2;
            this.FoVDown.TabStop = true;
            this.FoVDown.Text = "FoV Down Key";
            this.FoVDown.UseVisualStyleBackColor = true;
            this.FoVDown.CheckedChanged += new System.EventHandler(this.FoVDown_CheckedChanged);
            // 
            // FoVModifier
            // 
            this.FoVModifier.AutoSize = true;
            this.FoVModifier.Location = new System.Drawing.Point(12, 195);
            this.FoVModifier.Name = "FoVModifier";
            this.FoVModifier.Size = new System.Drawing.Size(84, 17);
            this.FoVModifier.TabIndex = 4;
            this.FoVModifier.TabStop = true;
            this.FoVModifier.Text = "FoV Modifier";
            this.FoVModifier.UseVisualStyleBackColor = true;
            this.FoVModifier.CheckedChanged += new System.EventHandler(this.FoVModifier_CheckedChanged);
            // 
            // FogKey
            // 
            this.FogKey.AutoSize = true;
            this.FogKey.Location = new System.Drawing.Point(12, 82);
            this.FogKey.Name = "FogKey";
            this.FogKey.Size = new System.Drawing.Size(64, 17);
            this.FogKey.TabIndex = 3;
            this.FogKey.TabStop = true;
            this.FogKey.Text = "Fog Key";
            this.FogKey.UseVisualStyleBackColor = true;
            this.FogKey.CheckedChanged += new System.EventHandler(this.FogKey_CheckedChanged);
            // 
            // FogModifier
            // 
            this.FogModifier.AutoSize = true;
            this.FogModifier.Location = new System.Drawing.Point(12, 218);
            this.FogModifier.Name = "FogModifier";
            this.FogModifier.Size = new System.Drawing.Size(83, 17);
            this.FogModifier.TabIndex = 5;
            this.FogModifier.TabStop = true;
            this.FogModifier.Text = "Fog Modifier";
            this.FogModifier.UseVisualStyleBackColor = true;
            this.FogModifier.CheckedChanged += new System.EventHandler(this.FogModifier_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGray;
            this.button1.Location = new System.Drawing.Point(128, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "Apply current key";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkGray;
            this.button2.Location = new System.Drawing.Point(128, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 25);
            this.button2.TabIndex = 7;
            this.button2.Text = "Save and exit";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkGray;
            this.button3.Location = new System.Drawing.Point(128, 252);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 25);
            this.button3.TabIndex = 8;
            this.button3.Text = "Exit without saving";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(177, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 10;
            this.label2.TextChanged += new System.EventHandler(this.Label2_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CoDUO_FoV_Changer.Properties.Resources.empty_key_big_3;
            this.pictureBox1.Location = new System.Drawing.Point(163, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Hotkeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(249, 289);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FogModifier);
            this.Controls.Add(this.FoVModifier);
            this.Controls.Add(this.FogKey);
            this.Controls.Add(this.FoVDown);
            this.Controls.Add(this.FoVUp);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Hotkeys";
            this.ShowIcon = false;
            this.Text = "Hotkeys";
            this.Load += new System.EventHandler(this.Hotkeys_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hotkeys_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton FoVUp;
        private System.Windows.Forms.RadioButton FoVDown;
        private System.Windows.Forms.RadioButton FoVModifier;
        private System.Windows.Forms.RadioButton FogKey;
        private System.Windows.Forms.RadioButton FogModifier;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
    }
}
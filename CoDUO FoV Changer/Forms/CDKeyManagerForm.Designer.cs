namespace CoDUO_FoV_Changer
{
    partial class CDKeyManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CDKeyManagerForm));
            this.uoKeyTextBox = new System.Windows.Forms.TextBox();
            this.uoKeyVirtualTextBox = new System.Windows.Forms.TextBox();
            this.codKeyTextBox = new System.Windows.Forms.TextBox();
            this.codKeyVirtualTextBox = new System.Windows.Forms.TextBox();
            this.showPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.uoKeyLabel = new System.Windows.Forms.Label();
            this.uoKeyLabelVS = new System.Windows.Forms.Label();
            this.codKeyLabel = new System.Windows.Forms.Label();
            this.codKeyLabelVS = new System.Windows.Forms.Label();
            this.refreshKeysButton = new System.Windows.Forms.Button();
            this.saveExitButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uoKeyTextBox
            // 
            this.uoKeyTextBox.BackColor = System.Drawing.Color.DarkGray;
            this.uoKeyTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uoKeyTextBox.Location = new System.Drawing.Point(12, 40);
            this.uoKeyTextBox.MaxLength = 24;
            this.uoKeyTextBox.Name = "uoKeyTextBox";
            this.uoKeyTextBox.Size = new System.Drawing.Size(248, 30);
            this.uoKeyTextBox.TabIndex = 0;
            this.uoKeyTextBox.UseSystemPasswordChar = true;
            // 
            // uoKeyVirtualTextBox
            // 
            this.uoKeyVirtualTextBox.BackColor = System.Drawing.Color.DarkGray;
            this.uoKeyVirtualTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uoKeyVirtualTextBox.Location = new System.Drawing.Point(12, 116);
            this.uoKeyVirtualTextBox.MaxLength = 24;
            this.uoKeyVirtualTextBox.Name = "uoKeyVirtualTextBox";
            this.uoKeyVirtualTextBox.Size = new System.Drawing.Size(248, 30);
            this.uoKeyVirtualTextBox.TabIndex = 1;
            this.uoKeyVirtualTextBox.UseSystemPasswordChar = true;
            // 
            // codKeyTextBox
            // 
            this.codKeyTextBox.BackColor = System.Drawing.Color.DarkGray;
            this.codKeyTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codKeyTextBox.Location = new System.Drawing.Point(12, 170);
            this.codKeyTextBox.MaxLength = 24;
            this.codKeyTextBox.Name = "codKeyTextBox";
            this.codKeyTextBox.Size = new System.Drawing.Size(248, 30);
            this.codKeyTextBox.TabIndex = 2;
            this.codKeyTextBox.UseSystemPasswordChar = true;
            // 
            // codKeyVirtualTextBox
            // 
            this.codKeyVirtualTextBox.BackColor = System.Drawing.Color.DarkGray;
            this.codKeyVirtualTextBox.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codKeyVirtualTextBox.Location = new System.Drawing.Point(12, 242);
            this.codKeyVirtualTextBox.MaxLength = 24;
            this.codKeyVirtualTextBox.Name = "codKeyVirtualTextBox";
            this.codKeyVirtualTextBox.Size = new System.Drawing.Size(248, 30);
            this.codKeyVirtualTextBox.TabIndex = 3;
            this.codKeyVirtualTextBox.UseSystemPasswordChar = true;
            // 
            // showPasswordCheckBox
            // 
            this.showPasswordCheckBox.AutoSize = true;
            this.showPasswordCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPasswordCheckBox.Location = new System.Drawing.Point(12, 278);
            this.showPasswordCheckBox.Name = "showPasswordCheckBox";
            this.showPasswordCheckBox.Size = new System.Drawing.Size(98, 17);
            this.showPasswordCheckBox.TabIndex = 4;
            this.showPasswordCheckBox.Text = "Show CD Keys";
            this.showPasswordCheckBox.UseVisualStyleBackColor = true;
            this.showPasswordCheckBox.CheckedChanged += new System.EventHandler(this.ShowPasswordCheckBox_CheckedChanged);
            // 
            // uoKeyLabel
            // 
            this.uoKeyLabel.AutoSize = true;
            this.uoKeyLabel.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uoKeyLabel.Location = new System.Drawing.Point(12, 17);
            this.uoKeyLabel.Name = "uoKeyLabel";
            this.uoKeyLabel.Size = new System.Drawing.Size(312, 18);
            this.uoKeyLabel.TabIndex = 5;
            this.uoKeyLabel.Text = "Call of Duty: United Offensive CD Key:";
            // 
            // uoKeyLabelVS
            // 
            this.uoKeyLabelVS.AutoSize = true;
            this.uoKeyLabelVS.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uoKeyLabelVS.Location = new System.Drawing.Point(12, 77);
            this.uoKeyLabelVS.Name = "uoKeyLabelVS";
            this.uoKeyLabelVS.Size = new System.Drawing.Size(312, 36);
            this.uoKeyLabelVS.TabIndex = 6;
            this.uoKeyLabelVS.Text = "Call of Duty: United Offensive CD Key \r\n(Virtual Store):";
            // 
            // codKeyLabel
            // 
            this.codKeyLabel.AutoSize = true;
            this.codKeyLabel.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codKeyLabel.Location = new System.Drawing.Point(12, 149);
            this.codKeyLabel.Name = "codKeyLabel";
            this.codKeyLabel.Size = new System.Drawing.Size(168, 18);
            this.codKeyLabel.TabIndex = 7;
            this.codKeyLabel.Text = "Call of Duty CD Key:";
            // 
            // codKeyLabelVS
            // 
            this.codKeyLabelVS.AutoSize = true;
            this.codKeyLabelVS.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codKeyLabelVS.Location = new System.Drawing.Point(12, 203);
            this.codKeyLabelVS.Name = "codKeyLabelVS";
            this.codKeyLabelVS.Size = new System.Drawing.Size(168, 36);
            this.codKeyLabelVS.TabIndex = 8;
            this.codKeyLabelVS.Text = "Call of Duty CD Key \r\n(Virtual Store):";
            // 
            // refreshKeysButton
            // 
            this.refreshKeysButton.BackColor = System.Drawing.Color.DarkGray;
            this.refreshKeysButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshKeysButton.ForeColor = System.Drawing.Color.Transparent;
            this.refreshKeysButton.Location = new System.Drawing.Point(12, 298);
            this.refreshKeysButton.Name = "refreshKeysButton";
            this.refreshKeysButton.Size = new System.Drawing.Size(248, 32);
            this.refreshKeysButton.TabIndex = 9;
            this.refreshKeysButton.Text = "Refresh all CD keys";
            this.refreshKeysButton.UseVisualStyleBackColor = false;
            this.refreshKeysButton.Click += new System.EventHandler(this.refreshKeysButton_Click);
            // 
            // saveExitButton
            // 
            this.saveExitButton.BackColor = System.Drawing.Color.DarkGray;
            this.saveExitButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveExitButton.ForeColor = System.Drawing.Color.Transparent;
            this.saveExitButton.Location = new System.Drawing.Point(12, 336);
            this.saveExitButton.Name = "saveExitButton";
            this.saveExitButton.Size = new System.Drawing.Size(248, 32);
            this.saveExitButton.TabIndex = 10;
            this.saveExitButton.Text = "Save and exit";
            this.saveExitButton.UseVisualStyleBackColor = false;
            this.saveExitButton.Click += new System.EventHandler(this.saveExitButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.DarkGray;
            this.exitButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.ForeColor = System.Drawing.Color.Transparent;
            this.exitButton.Location = new System.Drawing.Point(12, 374);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(248, 32);
            this.exitButton.TabIndex = 11;
            this.exitButton.Text = "Close";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 409);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 30);
            this.label1.TabIndex = 12;
            this.label1.Text = "If you\'re unsure what \'Virtual Store\' \r\ndenotes, use the same value for both.";
            // 
            // CDKeyManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(325, 444);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.saveExitButton);
            this.Controls.Add(this.refreshKeysButton);
            this.Controls.Add(this.codKeyLabelVS);
            this.Controls.Add(this.codKeyLabel);
            this.Controls.Add(this.uoKeyLabelVS);
            this.Controls.Add(this.uoKeyLabel);
            this.Controls.Add(this.showPasswordCheckBox);
            this.Controls.Add(this.codKeyVirtualTextBox);
            this.Controls.Add(this.codKeyTextBox);
            this.Controls.Add(this.uoKeyVirtualTextBox);
            this.Controls.Add(this.uoKeyTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CDKeyManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CD Key Manager";
            this.Load += new System.EventHandler(this.CDKeyManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox uoKeyTextBox;
        private System.Windows.Forms.TextBox uoKeyVirtualTextBox;
        private System.Windows.Forms.TextBox codKeyTextBox;
        private System.Windows.Forms.TextBox codKeyVirtualTextBox;
        private System.Windows.Forms.CheckBox showPasswordCheckBox;
        private System.Windows.Forms.Label uoKeyLabel;
        private System.Windows.Forms.Label uoKeyLabelVS;
        private System.Windows.Forms.Label codKeyLabel;
        private System.Windows.Forms.Label codKeyLabelVS;
        private System.Windows.Forms.Button refreshKeysButton;
        private System.Windows.Forms.Button saveExitButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label1;
    }
}
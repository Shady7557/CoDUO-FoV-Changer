namespace CoDUO_FoV_Changer
{
    partial class SingleplayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleplayerForm));
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.applyCfgBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.codFovLbl = new System.Windows.Forms.Label();
            this.uoFovLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.greenCheckPb = new System.Windows.Forms.PictureBox();
            this.appliedChangesLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.greenCheckPb)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.DarkGray;
            this.closeButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Black;
            this.closeButton.Location = new System.Drawing.Point(12, 316);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(437, 33);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Q. How do I change my Field of View in singleplayer?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(420, 120);
            this.label2.TabIndex = 2;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // applyCfgBtn
            // 
            this.applyCfgBtn.BackColor = System.Drawing.Color.DarkGray;
            this.applyCfgBtn.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyCfgBtn.ForeColor = System.Drawing.Color.Black;
            this.applyCfgBtn.Location = new System.Drawing.Point(12, 246);
            this.applyCfgBtn.Name = "applyCfgBtn";
            this.applyCfgBtn.Size = new System.Drawing.Size(229, 31);
            this.applyCfgBtn.TabIndex = 3;
            this.applyCfgBtn.Text = "Apply to single player config";
            this.applyCfgBtn.UseVisualStyleBackColor = false;
            this.applyCfgBtn.Click += new System.EventHandler(this.applyCfgBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current FOV in single player config:";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "VCoD:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "UO:";
            // 
            // greenCheckPb
            // 
            this.greenCheckPb.Image = global::CoDUO_FoV_Changer.Properties.Resources.success_check;
            this.greenCheckPb.Location = new System.Drawing.Point(247, 235);
            this.greenCheckPb.Name = "greenCheckPb";
            this.greenCheckPb.Size = new System.Drawing.Size(35, 51);
            this.greenCheckPb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.greenCheckPb.TabIndex = 9;
            this.greenCheckPb.TabStop = false;
            // 
            // appliedChangesLbl
            // 
            this.appliedChangesLbl.AutoSize = true;
            this.appliedChangesLbl.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appliedChangesLbl.Location = new System.Drawing.Point(296, 251);
            this.appliedChangesLbl.Name = "appliedChangesLbl";
            this.appliedChangesLbl.Size = new System.Drawing.Size(136, 18);
            this.appliedChangesLbl.TabIndex = 10;
            this.appliedChangesLbl.Text = "Changes applied!";
            // 
            // SingleplayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(461, 357);
            this.Controls.Add(this.appliedChangesLbl);
            this.Controls.Add(this.greenCheckPb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.uoFovLabel);
            this.Controls.Add(this.codFovLbl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.applyCfgBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "SingleplayerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.SingleplayerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.greenCheckPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button applyCfgBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label codFovLbl;
        private System.Windows.Forms.Label uoFovLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox greenCheckPb;
        private System.Windows.Forms.Label appliedChangesLbl;
    }
}
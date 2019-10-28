namespace CoDUO_FoV_Changer
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StartGameButton = new System.Windows.Forms.Button();
            this.FoVNumeric = new System.Windows.Forms.NumericUpDown();
            this.FoVLabel = new System.Windows.Forms.Label();
            this.MinimizeCheckBox = new System.Windows.Forms.CheckBox();
            this.FogCheckBox = new System.Windows.Forms.CheckBox();
            this.CheckUpdatesLabel = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.FoVMenuStrip = new System.Windows.Forms.MenuStrip();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ipDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.MinimizeIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.rcStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MemoryTimer = new System.Windows.Forms.Timer(this.components);
            this.LaunchParametersTB = new System.Windows.Forms.TextBox();
            this.LaunchParametersLB = new System.Windows.Forms.Label();
            this.HotKeyHandler = new System.Windows.Forms.Timer(this.components);
            this.DvarsCheckBox = new System.Windows.Forms.CheckBox();
            this.GameTracker = new System.Windows.Forms.Timer(this.components);
            this.CurSessionGT = new System.Windows.Forms.Label();
            this.GameTimeLabel = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.GamePIDBox = new System.Windows.Forms.ComboBox();
            this.CoDPictureBox = new System.Windows.Forms.PictureBox();
            this.ProccessChecker = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ipFDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.FoVNumeric)).BeginInit();
            this.FoVMenuStrip.SuspendLayout();
            this.rcStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoDPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StartGameButton
            // 
            this.StartGameButton.BackColor = System.Drawing.Color.DarkGray;
            this.StartGameButton.ForeColor = System.Drawing.Color.Transparent;
            this.StartGameButton.Location = new System.Drawing.Point(12, 33);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(161, 25);
            this.StartGameButton.TabIndex = 6;
            this.StartGameButton.Text = "Start Game";
            this.StartGameButton.UseVisualStyleBackColor = false;
            this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
            // 
            // FoVNumeric
            // 
            this.FoVNumeric.BackColor = System.Drawing.Color.DarkGray;
            this.FoVNumeric.ForeColor = System.Drawing.Color.Transparent;
            this.FoVNumeric.Location = new System.Drawing.Point(86, 7);
            this.FoVNumeric.Maximum = new decimal(new int[] {
            130,
            0,
            0,
            0});
            this.FoVNumeric.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.FoVNumeric.Name = "FoVNumeric";
            this.FoVNumeric.Size = new System.Drawing.Size(42, 20);
            this.FoVNumeric.TabIndex = 61;
            this.FoVNumeric.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.FoVNumeric.ValueChanged += new System.EventHandler(this.FoVNumeric_ValueChanged);
            // 
            // FoVLabel
            // 
            this.FoVLabel.AutoSize = true;
            this.FoVLabel.ForeColor = System.Drawing.Color.Transparent;
            this.FoVLabel.Location = new System.Drawing.Point(10, 9);
            this.FoVLabel.Name = "FoVLabel";
            this.FoVLabel.Size = new System.Drawing.Size(70, 13);
            this.FoVLabel.TabIndex = 60;
            this.FoVLabel.Text = "Field of View:";
            // 
            // MinimizeCheckBox
            // 
            this.MinimizeCheckBox.AutoSize = true;
            this.MinimizeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.MinimizeCheckBox.ForeColor = System.Drawing.Color.Transparent;
            this.MinimizeCheckBox.Location = new System.Drawing.Point(316, 50);
            this.MinimizeCheckBox.Name = "MinimizeCheckBox";
            this.MinimizeCheckBox.Size = new System.Drawing.Size(98, 17);
            this.MinimizeCheckBox.TabIndex = 65;
            this.MinimizeCheckBox.Text = "Minimize to tray";
            this.MinimizeCheckBox.UseVisualStyleBackColor = false;
            this.MinimizeCheckBox.CheckedChanged += new System.EventHandler(this.MinimizeCheckBox_CheckedChanged);
            // 
            // FogCheckBox
            // 
            this.FogCheckBox.AutoSize = true;
            this.FogCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.FogCheckBox.Checked = true;
            this.FogCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FogCheckBox.ForeColor = System.Drawing.Color.Transparent;
            this.FogCheckBox.Location = new System.Drawing.Point(316, 73);
            this.FogCheckBox.Name = "FogCheckBox";
            this.FogCheckBox.Size = new System.Drawing.Size(44, 17);
            this.FogCheckBox.TabIndex = 64;
            this.FogCheckBox.Text = "Fog";
            this.FogCheckBox.UseVisualStyleBackColor = false;
            this.FogCheckBox.CheckedChanged += new System.EventHandler(this.FogCheckBox_CheckedChanged);
            // 
            // CheckUpdatesLabel
            // 
            this.CheckUpdatesLabel.AutoSize = true;
            this.CheckUpdatesLabel.BackColor = System.Drawing.Color.Transparent;
            this.CheckUpdatesLabel.ForeColor = System.Drawing.Color.Transparent;
            this.CheckUpdatesLabel.Location = new System.Drawing.Point(143, 9);
            this.CheckUpdatesLabel.Name = "CheckUpdatesLabel";
            this.CheckUpdatesLabel.Size = new System.Drawing.Size(117, 13);
            this.CheckUpdatesLabel.TabIndex = 63;
            this.CheckUpdatesLabel.Text = "Checking for updates...";
            this.CheckUpdatesLabel.TextChanged += new System.EventHandler(this.CheckUpdatesLabel_TextChanged);
            this.CheckUpdatesLabel.Click += new System.EventHandler(this.CheckUpdatesLabel_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.DarkGray;
            this.UpdateButton.ForeColor = System.Drawing.Color.Transparent;
            this.UpdateButton.Location = new System.Drawing.Point(179, 33);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(70, 25);
            this.UpdateButton.TabIndex = 62;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Visible = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // FoVMenuStrip
            // 
            this.FoVMenuStrip.BackColor = System.Drawing.Color.DarkGray;
            this.FoVMenuStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FoVMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpToolStripMenuItem,
            this.ToolStripMenuItem1,
            this.ExitToolStripMenuItem});
            this.FoVMenuStrip.Location = new System.Drawing.Point(0, 192);
            this.FoVMenuStrip.Name = "FoVMenuStrip";
            this.FoVMenuStrip.Size = new System.Drawing.Size(419, 24);
            this.FoVMenuStrip.TabIndex = 68;
            this.FoVMenuStrip.Text = "MenuStrip1";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfoToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "Help";
            // 
            // InfoToolStripMenuItem
            // 
            this.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem";
            this.InfoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.InfoToolStripMenuItem.Text = "About";
            this.InfoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsToolStripMenuItem,
            this.ChangelogToolStripMenuItem});
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(46, 20);
            this.ToolStripMenuItem1.Text = "Tools";
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.SettingsToolStripMenuItem.Text = "Settings";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ChangelogToolStripMenuItem
            // 
            this.ChangelogToolStripMenuItem.Name = "ChangelogToolStripMenuItem";
            this.ChangelogToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.ChangelogToolStripMenuItem.Text = "Changelog";
            this.ChangelogToolStripMenuItem.Click += new System.EventHandler(this.ChangelogToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.Red;
            this.StatusLabel.Location = new System.Drawing.Point(-3, 170);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(307, 18);
            this.StatusLabel.TabIndex = 69;
            this.StatusLabel.Text = "Status: not found or failed to write to memory!";
            // 
            // ipDialog
            // 
            this.ipDialog.ShowNewFolderButton = false;
            // 
            // MinimizeIcon
            // 
            this.MinimizeIcon.ContextMenuStrip = this.rcStrip;
            this.MinimizeIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MinimizeIcon.Icon")));
            this.MinimizeIcon.Text = "CoDUO FoV Changer";
            this.MinimizeIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MinimizeIcon_MouseDoubleClick);
            // 
            // rcStrip
            // 
            this.rcStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.fogToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.rcStrip.Name = "rcStrip";
            this.rcStrip.Size = new System.Drawing.Size(104, 92);
            this.rcStrip.Text = "test";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // fogToolStripMenuItem
            // 
            this.fogToolStripMenuItem.Checked = true;
            this.fogToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fogToolStripMenuItem.Name = "fogToolStripMenuItem";
            this.fogToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.fogToolStripMenuItem.Text = "Fog";
            this.fogToolStripMenuItem.CheckedChanged += new System.EventHandler(this.fogToolStripMenuItem_CheckedChanged);
            this.fogToolStripMenuItem.Click += new System.EventHandler(this.fogToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // MemoryTimer
            // 
            this.MemoryTimer.Enabled = true;
            this.MemoryTimer.Interval = 3000;
            this.MemoryTimer.Tick += new System.EventHandler(this.MemoryTimer_Tick);
            // 
            // LaunchParametersTB
            // 
            this.LaunchParametersTB.BackColor = System.Drawing.Color.DarkGray;
            this.LaunchParametersTB.ForeColor = System.Drawing.Color.Transparent;
            this.LaunchParametersTB.Location = new System.Drawing.Point(12, 96);
            this.LaunchParametersTB.Multiline = true;
            this.LaunchParametersTB.Name = "LaunchParametersTB";
            this.LaunchParametersTB.Size = new System.Drawing.Size(270, 20);
            this.LaunchParametersTB.TabIndex = 71;
            this.LaunchParametersTB.TextChanged += new System.EventHandler(this.LaunchParametersTB_TextChanged);
            // 
            // LaunchParametersLB
            // 
            this.LaunchParametersLB.AutoSize = true;
            this.LaunchParametersLB.BackColor = System.Drawing.Color.Transparent;
            this.LaunchParametersLB.ForeColor = System.Drawing.Color.Transparent;
            this.LaunchParametersLB.Location = new System.Drawing.Point(9, 80);
            this.LaunchParametersLB.Name = "LaunchParametersLB";
            this.LaunchParametersLB.Size = new System.Drawing.Size(102, 13);
            this.LaunchParametersLB.TabIndex = 70;
            this.LaunchParametersLB.Text = "Launch Parameters:";
            // 
            // HotKeyHandler
            // 
            this.HotKeyHandler.Enabled = true;
            this.HotKeyHandler.Interval = 30;
            this.HotKeyHandler.Tick += new System.EventHandler(this.HotKeyHandler_Tick);
            // 
            // DvarsCheckBox
            // 
            this.DvarsCheckBox.AutoSize = true;
            this.DvarsCheckBox.ForeColor = System.Drawing.Color.Transparent;
            this.DvarsCheckBox.Location = new System.Drawing.Point(316, 27);
            this.DvarsCheckBox.Name = "DvarsCheckBox";
            this.DvarsCheckBox.Size = new System.Drawing.Size(105, 17);
            this.DvarsCheckBox.TabIndex = 72;
            this.DvarsCheckBox.Text = "Unlock All Dvars";
            this.DvarsCheckBox.UseVisualStyleBackColor = true;
            this.DvarsCheckBox.CheckedChanged += new System.EventHandler(this.DvarsCheckBox_CheckedChanged);
            // 
            // GameTracker
            // 
            this.GameTracker.Enabled = true;
            this.GameTracker.Interval = 1000;
            this.GameTracker.Tick += new System.EventHandler(this.GameTracker_Tick);
            // 
            // CurSessionGT
            // 
            this.CurSessionGT.AutoSize = true;
            this.CurSessionGT.ForeColor = System.Drawing.Color.Transparent;
            this.CurSessionGT.Location = new System.Drawing.Point(147, 74);
            this.CurSessionGT.Name = "CurSessionGT";
            this.CurSessionGT.Size = new System.Drawing.Size(113, 13);
            this.CurSessionGT.TabIndex = 74;
            this.CurSessionGT.Text = "Current Session: None";
            // 
            // GameTimeLabel
            // 
            this.GameTimeLabel.AutoSize = true;
            this.GameTimeLabel.ForeColor = System.Drawing.Color.Transparent;
            this.GameTimeLabel.Location = new System.Drawing.Point(147, 61);
            this.GameTimeLabel.Name = "GameTimeLabel";
            this.GameTimeLabel.Size = new System.Drawing.Size(93, 13);
            this.GameTimeLabel.TabIndex = 73;
            this.GameTimeLabel.Text = "Game Time: None";
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Enabled = true;
            this.UpdateTimer.Interval = 120000;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // GamePIDBox
            // 
            this.GamePIDBox.BackColor = System.Drawing.Color.DarkGray;
            this.GamePIDBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GamePIDBox.ForeColor = System.Drawing.Color.Transparent;
            this.GamePIDBox.FormattingEnabled = true;
            this.GamePIDBox.Location = new System.Drawing.Point(303, 167);
            this.GamePIDBox.Name = "GamePIDBox";
            this.GamePIDBox.Size = new System.Drawing.Size(116, 21);
            this.GamePIDBox.TabIndex = 75;
            this.GamePIDBox.SelectedIndexChanged += new System.EventHandler(this.GamePIDBox_SelectedIndexChanged);
            this.GamePIDBox.VisibleChanged += new System.EventHandler(this.GamePIDBox_VisibleChanged);
            // 
            // CoDPictureBox
            // 
            this.CoDPictureBox.Location = new System.Drawing.Point(360, 96);
            this.CoDPictureBox.Name = "CoDPictureBox";
            this.CoDPictureBox.Size = new System.Drawing.Size(45, 46);
            this.CoDPictureBox.TabIndex = 67;
            this.CoDPictureBox.TabStop = false;
            // 
            // ProccessChecker
            // 
            this.ProccessChecker.Enabled = true;
            this.ProccessChecker.Interval = 1500;
            this.ProccessChecker.Tick += new System.EventHandler(this.ProccessChecker_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(419, 216);
            this.Controls.Add(this.GamePIDBox);
            this.Controls.Add(this.CurSessionGT);
            this.Controls.Add(this.GameTimeLabel);
            this.Controls.Add(this.DvarsCheckBox);
            this.Controls.Add(this.LaunchParametersTB);
            this.Controls.Add(this.LaunchParametersLB);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.FoVMenuStrip);
            this.Controls.Add(this.CoDPictureBox);
            this.Controls.Add(this.MinimizeCheckBox);
            this.Controls.Add(this.FogCheckBox);
            this.Controls.Add(this.CheckUpdatesLabel);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.FoVNumeric);
            this.Controls.Add(this.FoVLabel);
            this.Controls.Add(this.StartGameButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoDUO FoV Changer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.FoVNumeric)).EndInit();
            this.FoVMenuStrip.ResumeLayout(false);
            this.FoVMenuStrip.PerformLayout();
            this.rcStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CoDPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button StartGameButton;
        internal System.Windows.Forms.NumericUpDown FoVNumeric;
        internal System.Windows.Forms.Label FoVLabel;
        internal System.Windows.Forms.CheckBox MinimizeCheckBox;
        internal System.Windows.Forms.CheckBox FogCheckBox;
        internal System.Windows.Forms.Label CheckUpdatesLabel;
        internal System.Windows.Forms.Button UpdateButton;
        internal System.Windows.Forms.PictureBox CoDPictureBox;
        internal System.Windows.Forms.MenuStrip FoVMenuStrip;
        internal System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem InfoToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ChangelogToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.FolderBrowserDialog ipDialog;
        private System.Windows.Forms.NotifyIcon MinimizeIcon;
        internal System.Windows.Forms.Timer MemoryTimer;
        internal System.Windows.Forms.TextBox LaunchParametersTB;
        internal System.Windows.Forms.Label LaunchParametersLB;
        internal System.Windows.Forms.Timer HotKeyHandler;
        internal System.Windows.Forms.CheckBox DvarsCheckBox;
        internal System.Windows.Forms.Timer GameTracker;
        internal System.Windows.Forms.Label CurSessionGT;
        internal System.Windows.Forms.Label GameTimeLabel;
        internal System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.ComboBox GamePIDBox;
        internal System.Windows.Forms.Timer ProccessChecker;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog ipFDialog;
        private System.Windows.Forms.ContextMenuStrip rcStrip;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
    }
}


using Sce.Atf.Controls;
using System.Windows.Forms;

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
            this.StartGameButton = new Sce.Atf.Controls.SplitButton();
            this.startGameStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rcStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.FoVNumeric = new System.Windows.Forms.NumericUpDown();
            this.FoVLabel = new System.Windows.Forms.Label();
            this.MinimizeCheckBox = new System.Windows.Forms.CheckBox();
            this.CheckUpdatesLabel = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.FoVMenuStrip = new System.Windows.Forms.MenuStrip();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleplayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frFRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.serversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ipDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.MinimizeIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.LaunchParametersTB = new System.Windows.Forms.TextBox();
            this.LaunchParametersLB = new System.Windows.Forms.Label();
            this.HotKeyHandler = new System.Windows.Forms.Timer(this.components);
            this.GameTracker = new System.Windows.Forms.Timer(this.components);
            this.SessionLabel = new System.Windows.Forms.Label();
            this.GameTimeLabel = new System.Windows.Forms.Label();
            this.ProccessChecker = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxDesktopRes = new System.Windows.Forms.CheckBox();
            this.ipFDialog = new System.Windows.Forms.OpenFileDialog();
            this.AdminLaunchButton = new System.Windows.Forms.Button();
            this.GameTimeLabelTimer = new System.Windows.Forms.Timer(this.components);
            this.GamePIDBox = new CoDUO_FoV_Changer.ProcessMemoryBox();
            this.CoDPictureBox = new System.Windows.Forms.PictureBox();
            this.SessionTimeLabel = new System.Windows.Forms.Label();
            this.GameTimeCountLabel = new System.Windows.Forms.Label();
            this.rcStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FoVNumeric)).BeginInit();
            this.FoVMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoDPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // StartGameButton
            // 
            this.StartGameButton.BackColor = System.Drawing.Color.DarkGray;
            this.StartGameButton.ContextMenuStrip = this.startGameStrip;
            this.StartGameButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartGameButton.ForeColor = System.Drawing.Color.Black;
            this.StartGameButton.IsShowing = false;
            this.StartGameButton.Location = new System.Drawing.Point(10, 33);
            this.StartGameButton.Name = "StartGameButton";
            this.StartGameButton.Size = new System.Drawing.Size(161, 25);
            this.StartGameButton.TabIndex = 6;
            this.StartGameButton.Text = "Start Game ({LAST_GAME})";
            this.StartGameButton.UseVisualStyleBackColor = false;
            this.StartGameButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartGameButton_MouseDown);
            // 
            // startGameStrip
            // 
            this.startGameStrip.Name = "rcStrip";
            this.startGameStrip.Size = new System.Drawing.Size(61, 4);
            this.startGameStrip.Text = "test";
            // 
            // rcStrip
            // 
            this.rcStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.rcStrip.Name = "rcStrip";
            this.rcStrip.Size = new System.Drawing.Size(104, 70);
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
            // FoVNumeric
            // 
            this.FoVNumeric.BackColor = System.Drawing.Color.DarkGray;
            this.FoVNumeric.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FoVNumeric.ForeColor = System.Drawing.Color.Transparent;
            this.FoVNumeric.Location = new System.Drawing.Point(99, 6);
            this.FoVNumeric.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.FoVNumeric.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.FoVNumeric.Name = "FoVNumeric";
            this.FoVNumeric.Size = new System.Drawing.Size(42, 22);
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
            this.FoVLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FoVLabel.ForeColor = System.Drawing.Color.Black;
            this.FoVLabel.Location = new System.Drawing.Point(7, 9);
            this.FoVLabel.Name = "FoVLabel";
            this.FoVLabel.Size = new System.Drawing.Size(91, 13);
            this.FoVLabel.TabIndex = 60;
            this.FoVLabel.Text = "Field of View:";
            // 
            // MinimizeCheckBox
            // 
            this.MinimizeCheckBox.AutoSize = true;
            this.MinimizeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.MinimizeCheckBox.Checked = true;
            this.MinimizeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MinimizeCheckBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeCheckBox.ForeColor = System.Drawing.Color.Black;
            this.MinimizeCheckBox.Location = new System.Drawing.Point(303, 33);
            this.MinimizeCheckBox.Name = "MinimizeCheckBox";
            this.MinimizeCheckBox.Size = new System.Drawing.Size(122, 17);
            this.MinimizeCheckBox.TabIndex = 65;
            this.MinimizeCheckBox.Text = "Minimize to tray";
            this.toolTip1.SetToolTip(this.MinimizeCheckBox, "Toggle whether or not this app should minimize to the tray");
            this.MinimizeCheckBox.UseVisualStyleBackColor = false;
            this.MinimizeCheckBox.CheckedChanged += new System.EventHandler(this.MinimizeCheckBox_CheckedChanged);
            // 
            // CheckUpdatesLabel
            // 
            this.CheckUpdatesLabel.AutoSize = true;
            this.CheckUpdatesLabel.BackColor = System.Drawing.Color.Transparent;
            this.CheckUpdatesLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckUpdatesLabel.ForeColor = System.Drawing.Color.Black;
            this.CheckUpdatesLabel.Location = new System.Drawing.Point(147, 9);
            this.CheckUpdatesLabel.Name = "CheckUpdatesLabel";
            this.CheckUpdatesLabel.Size = new System.Drawing.Size(145, 13);
            this.CheckUpdatesLabel.TabIndex = 63;
            this.CheckUpdatesLabel.Text = "Checking for updates...";
            this.CheckUpdatesLabel.Click += new System.EventHandler(this.CheckUpdatesLabel_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.Color.DarkGray;
            this.UpdateButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateButton.ForeColor = System.Drawing.Color.Black;
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
            this.FoVMenuStrip.Size = new System.Drawing.Size(420, 24);
            this.FoVMenuStrip.TabIndex = 68;
            this.FoVMenuStrip.Text = "MenuStrip1";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfoToolStripMenuItem,
            this.singleplayerToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "Help";
            // 
            // InfoToolStripMenuItem
            // 
            this.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem";
            this.InfoToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.InfoToolStripMenuItem.Text = "About";
            this.InfoToolStripMenuItem.Click += new System.EventHandler(this.InfoToolStripMenuItem_Click);
            // 
            // singleplayerToolStripMenuItem
            // 
            this.singleplayerToolStripMenuItem.Name = "singleplayerToolStripMenuItem";
            this.singleplayerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.singleplayerToolStripMenuItem.Text = "Game Fixes/Solutions";
            this.singleplayerToolStripMenuItem.Click += new System.EventHandler(this.singleplayerToolStripMenuItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enUSToolStripMenuItem,
            this.frFRToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // enUSToolStripMenuItem
            // 
            this.enUSToolStripMenuItem.CheckOnClick = true;
            this.enUSToolStripMenuItem.Name = "enUSToolStripMenuItem";
            this.enUSToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.enUSToolStripMenuItem.Text = "en-US";
            this.enUSToolStripMenuItem.Click += new System.EventHandler(this.enUSToolStripMenuItem_Click);
            // 
            // frFRToolStripMenuItem
            // 
            this.frFRToolStripMenuItem.Name = "frFRToolStripMenuItem";
            this.frFRToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.frFRToolStripMenuItem.Text = "fr-FR";
            this.frFRToolStripMenuItem.Click += new System.EventHandler(this.frFRToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serversToolStripMenuItem,
            this.SettingsToolStripMenuItem,
            this.ChangelogToolStripMenuItem,
            this.mapArchiveToolStripMenuItem});
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(46, 20);
            this.ToolStripMenuItem1.Text = "Tools";
            // 
            // serversToolStripMenuItem
            // 
            this.serversToolStripMenuItem.Name = "serversToolStripMenuItem";
            this.serversToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.serversToolStripMenuItem.Text = "Servers";
            this.serversToolStripMenuItem.Click += new System.EventHandler(this.serversToolStripMenuItem_Click);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.SettingsToolStripMenuItem.Text = "Settings";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ChangelogToolStripMenuItem
            // 
            this.ChangelogToolStripMenuItem.Name = "ChangelogToolStripMenuItem";
            this.ChangelogToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.ChangelogToolStripMenuItem.Text = "Changelog [GitHub]";
            this.ChangelogToolStripMenuItem.Click += new System.EventHandler(this.ChangelogToolStripMenuItem_Click);
            // 
            // mapArchiveToolStripMenuItem
            // 
            this.mapArchiveToolStripMenuItem.Name = "mapArchiveToolStripMenuItem";
            this.mapArchiveToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.mapArchiveToolStripMenuItem.Text = "Map Archive [GitHub]";
            this.mapArchiveToolStripMenuItem.Click += new System.EventHandler(this.mapArchiveToolStripMenuItem_Click);
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
            this.StatusLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.Orange;
            this.StatusLabel.Location = new System.Drawing.Point(9, 175);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(121, 13);
            this.StatusLabel.TabIndex = 69;
            this.StatusLabel.Text = "Status: Not running";
            // 
            // ipDialog
            // 
            this.ipDialog.ShowNewFolderButton = false;
            // 
            // MinimizeIcon
            // 
            this.MinimizeIcon.ContextMenuStrip = this.rcStrip;
            this.MinimizeIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MinimizeIcon.Icon")));
            this.MinimizeIcon.Text = "Microsoft® Visual Studio®";
            this.MinimizeIcon.BalloonTipClicked += new System.EventHandler(this.MinimizeIcon_BalloonTipClicked);
            this.MinimizeIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MinimizeIcon_MouseClick);
            // 
            // LaunchParametersTB
            // 
            this.LaunchParametersTB.BackColor = System.Drawing.Color.DarkGray;
            this.LaunchParametersTB.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchParametersTB.ForeColor = System.Drawing.Color.Black;
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
            this.LaunchParametersLB.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LaunchParametersLB.ForeColor = System.Drawing.Color.Black;
            this.LaunchParametersLB.Location = new System.Drawing.Point(9, 80);
            this.LaunchParametersLB.Name = "LaunchParametersLB";
            this.LaunchParametersLB.Size = new System.Drawing.Size(115, 13);
            this.LaunchParametersLB.TabIndex = 70;
            this.LaunchParametersLB.Text = "Launch Parameters:";
            // 
            // HotKeyHandler
            // 
            this.HotKeyHandler.Enabled = true;
            this.HotKeyHandler.Interval = 30;
            this.HotKeyHandler.Tick += new System.EventHandler(this.HotKeyHandler_Tick);
            // 
            // GameTracker
            // 
            this.GameTracker.Enabled = true;
            this.GameTracker.Interval = 5000;
            this.GameTracker.Tick += new System.EventHandler(this.GameTracker_Tick);
            // 
            // SessionLabel
            // 
            this.SessionLabel.AutoSize = true;
            this.SessionLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionLabel.ForeColor = System.Drawing.Color.Black;
            this.SessionLabel.Location = new System.Drawing.Point(147, 74);
            this.SessionLabel.Name = "SessionLabel";
            this.SessionLabel.Size = new System.Drawing.Size(85, 13);
            this.SessionLabel.TabIndex = 74;
            this.SessionLabel.Text = "Session Time:";
            // 
            // GameTimeLabel
            // 
            this.GameTimeLabel.AutoSize = true;
            this.GameTimeLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.GameTimeLabel.Location = new System.Drawing.Point(147, 61);
            this.GameTimeLabel.Name = "GameTimeLabel";
            this.GameTimeLabel.Size = new System.Drawing.Size(67, 13);
            this.GameTimeLabel.TabIndex = 73;
            this.GameTimeLabel.Text = "Game Time:";
            // 
            // ProccessChecker
            // 
            this.ProccessChecker.Enabled = true;
            this.ProccessChecker.Interval = 1500;
            this.ProccessChecker.Tick += new System.EventHandler(this.ProccessChecker_Tick);
            // 
            // checkBoxDesktopRes
            // 
            this.checkBoxDesktopRes.AutoSize = true;
            this.checkBoxDesktopRes.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxDesktopRes.Checked = true;
            this.checkBoxDesktopRes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDesktopRes.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDesktopRes.ForeColor = System.Drawing.Color.Black;
            this.checkBoxDesktopRes.Location = new System.Drawing.Point(303, 53);
            this.checkBoxDesktopRes.Name = "checkBoxDesktopRes";
            this.checkBoxDesktopRes.Size = new System.Drawing.Size(98, 30);
            this.checkBoxDesktopRes.TabIndex = 77;
            this.checkBoxDesktopRes.Text = "Use desktop \r\nresolution";
            this.toolTip1.SetToolTip(this.checkBoxDesktopRes, "Toggle whether we should use the desktop resolution when launching the game.\r\nOnl" +
        "y affects game launches via \'Start Game\' button.");
            this.checkBoxDesktopRes.UseVisualStyleBackColor = false;
            // 
            // AdminLaunchButton
            // 
            this.AdminLaunchButton.BackColor = System.Drawing.Color.DarkGray;
            this.AdminLaunchButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdminLaunchButton.ForeColor = System.Drawing.Color.Black;
            this.AdminLaunchButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.AdminLaunchButton.Location = new System.Drawing.Point(10, 151);
            this.AdminLaunchButton.Name = "AdminLaunchButton";
            this.AdminLaunchButton.Size = new System.Drawing.Size(272, 21);
            this.AdminLaunchButton.TabIndex = 76;
            this.AdminLaunchButton.Text = "Launch program as Administrator";
            this.AdminLaunchButton.UseVisualStyleBackColor = false;
            this.AdminLaunchButton.Visible = false;
            this.AdminLaunchButton.Click += new System.EventHandler(this.AdminLaunchButton_Click);
            // 
            // GameTimeLabelTimer
            // 
            this.GameTimeLabelTimer.Enabled = true;
            this.GameTimeLabelTimer.Interval = 1000;
            this.GameTimeLabelTimer.Tick += new System.EventHandler(this.GameTimeLabelTimer_Tick);
            // 
            // GamePIDBox
            // 
            this.GamePIDBox.BackColor = System.Drawing.Color.DarkGray;
            this.GamePIDBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GamePIDBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GamePIDBox.ForeColor = System.Drawing.Color.Black;
            this.GamePIDBox.FormattingEnabled = true;
            this.GamePIDBox.Location = new System.Drawing.Point(303, 167);
            this.GamePIDBox.Name = "GamePIDBox";
            this.GamePIDBox.Size = new System.Drawing.Size(116, 21);
            this.GamePIDBox.TabIndex = 75;
            this.GamePIDBox.SelectedIndexChanged += new System.EventHandler(this.GamePIDBox_SelectedIndexChanged);
            // 
            // CoDPictureBox
            // 
            this.CoDPictureBox.Image = global::CoDUO_FoV_Changer.Properties.Resources.CoD1_UO_icon;
            this.CoDPictureBox.Location = new System.Drawing.Point(360, 96);
            this.CoDPictureBox.Name = "CoDPictureBox";
            this.CoDPictureBox.Size = new System.Drawing.Size(45, 46);
            this.CoDPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CoDPictureBox.TabIndex = 67;
            this.CoDPictureBox.TabStop = false;
            // 
            // SessionTimeLabel
            // 
            this.SessionTimeLabel.AutoSize = true;
            this.SessionTimeLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SessionTimeLabel.ForeColor = System.Drawing.Color.Black;
            this.SessionTimeLabel.Location = new System.Drawing.Point(229, 74);
            this.SessionTimeLabel.Name = "SessionTimeLabel";
            this.SessionTimeLabel.Size = new System.Drawing.Size(31, 13);
            this.SessionTimeLabel.TabIndex = 78;
            this.SessionTimeLabel.Text = "None";
            // 
            // GameTimeCountLabel
            // 
            this.GameTimeCountLabel.AutoSize = true;
            this.GameTimeCountLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameTimeCountLabel.ForeColor = System.Drawing.Color.Black;
            this.GameTimeCountLabel.Location = new System.Drawing.Point(211, 61);
            this.GameTimeCountLabel.Name = "GameTimeCountLabel";
            this.GameTimeCountLabel.Size = new System.Drawing.Size(31, 13);
            this.GameTimeCountLabel.TabIndex = 79;
            this.GameTimeCountLabel.Text = "None";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(420, 216);
            this.Controls.Add(this.GameTimeCountLabel);
            this.Controls.Add(this.SessionTimeLabel);
            this.Controls.Add(this.checkBoxDesktopRes);
            this.Controls.Add(this.AdminLaunchButton);
            this.Controls.Add(this.GamePIDBox);
            this.Controls.Add(this.SessionLabel);
            this.Controls.Add(this.GameTimeLabel);
            this.Controls.Add(this.LaunchParametersTB);
            this.Controls.Add(this.LaunchParametersLB);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.FoVMenuStrip);
            this.Controls.Add(this.CoDPictureBox);
            this.Controls.Add(this.MinimizeCheckBox);
            this.Controls.Add(this.CheckUpdatesLabel);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.FoVNumeric);
            this.Controls.Add(this.FoVLabel);
            this.Controls.Add(this.StartGameButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{Application.ProductName}";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.rcStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FoVNumeric)).EndInit();
            this.FoVMenuStrip.ResumeLayout(false);
            this.FoVMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoDPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal SplitButton StartGameButton;
        internal System.Windows.Forms.NumericUpDown FoVNumeric;
        internal System.Windows.Forms.Label FoVLabel;
        internal System.Windows.Forms.CheckBox MinimizeCheckBox;
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
        internal System.Windows.Forms.TextBox LaunchParametersTB;
        internal System.Windows.Forms.Label LaunchParametersLB;
        internal System.Windows.Forms.Timer HotKeyHandler;
        internal System.Windows.Forms.Timer GameTracker;
        internal System.Windows.Forms.Label SessionLabel;
        internal System.Windows.Forms.Label GameTimeLabel;
        private ProcessMemoryBox GamePIDBox;
        internal System.Windows.Forms.Timer ProccessChecker;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog ipFDialog;
        private System.Windows.Forms.ContextMenuStrip rcStrip;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        internal System.Windows.Forms.Button AdminLaunchButton;
        private System.Windows.Forms.Timer GameTimeLabelTimer;
        private System.Windows.Forms.ToolStripMenuItem singleplayerToolStripMenuItem;
        internal CheckBox checkBoxDesktopRes;
        private ContextMenuStrip startGameStrip;
        private ToolStripMenuItem serversToolStripMenuItem;
        internal Label SessionTimeLabel;
        internal Label GameTimeCountLabel;
        private ToolStripMenuItem mapArchiveToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem enUSToolStripMenuItem;
        private ToolStripMenuItem frFRToolStripMenuItem;
    }
}


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainFoV
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainFoV))
        Me.FoVTextBox = New System.Windows.Forms.TextBox()
        Me.FoVLabel = New System.Windows.Forms.Label()
        Me.FoVTimer = New System.Windows.Forms.Timer(Me.components)
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.StartGameButton = New System.Windows.Forms.Button()
        Me.TextBoxTimer = New System.Windows.Forms.Timer(Me.components)
        Me.debugb = New System.Windows.Forms.Button()
        Me.HotKeyHandler = New System.Windows.Forms.Timer(Me.components)
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.CheckUpdatesLabel = New System.Windows.Forms.Label()
        Me.UpdateCheckTimer = New System.Windows.Forms.Timer(Me.components)
        Me.LaunchParametersLB = New System.Windows.Forms.Label()
        Me.LaunchParametersTB = New System.Windows.Forms.TextBox()
        Me.HackyAppBranchLB = New System.Windows.Forms.Label()
        Me.HackyGameVersLB = New System.Windows.Forms.Label()
        Me.FogCheckBox = New System.Windows.Forms.CheckBox()
        Me.FogTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.FoVFixTimer = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.MinimizeIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.MinimizeCheckBox = New System.Windows.Forms.CheckBox()
        Me.ToolTipHandler = New System.Windows.Forms.ToolTip(Me.components)
        Me.HackyAppVersLB = New System.Windows.Forms.Label()
        Me.ABITWTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CoD1CheckBox = New System.Windows.Forms.CheckBox()
        Me.FileSizeLabel = New System.Windows.Forms.Label()
        Me.FileSizeLabel2 = New System.Windows.Forms.Label()
        Me.ToolTipHandler2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DvarsCheckBox = New System.Windows.Forms.CheckBox()
        Me.CoDPictureBox = New System.Windows.Forms.PictureBox()
        Me.FoVMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangelogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CmdLineTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DvarUnlockerTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.HackyFoVComboBox = New System.Windows.Forms.ComboBox()
        Me.GameTracker = New System.Windows.Forms.Timer(Me.components)
        Me.GameTimeLabel = New System.Windows.Forms.Label()
        Me.GameTimeSaver = New System.Windows.Forms.Timer(Me.components)
        Me.CurSessionGT = New System.Windows.Forms.Label()
        CType(Me.CoDPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FoVMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'FoVTextBox
        '
        Me.FoVTextBox.Location = New System.Drawing.Point(77, 6)
        Me.FoVTextBox.Name = "FoVTextBox"
        Me.FoVTextBox.Size = New System.Drawing.Size(42, 20)
        Me.FoVTextBox.TabIndex = 0
        Me.FoVTextBox.Text = "80"
        '
        'FoVLabel
        '
        Me.FoVLabel.AutoSize = True
        Me.FoVLabel.Location = New System.Drawing.Point(1, 9)
        Me.FoVLabel.Name = "FoVLabel"
        Me.FoVLabel.Size = New System.Drawing.Size(70, 13)
        Me.FoVLabel.TabIndex = 2
        Me.FoVLabel.Text = "Field of View:"
        '
        'FoVTimer
        '
        Me.FoVTimer.Enabled = True
        Me.FoVTimer.Interval = 1500
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = True
        Me.StatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusLabel.ForeColor = System.Drawing.Color.Red
        Me.StatusLabel.Location = New System.Drawing.Point(1, 145)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(307, 18)
        Me.StatusLabel.TabIndex = 3
        Me.StatusLabel.Text = "Status: not found or failed to write to memory!"
        Me.ToolTipHandler.SetToolTip(Me.StatusLabel, "The status of the FoV Changer. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If it is not found or failed to write to memory," &
        " start the game." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If this fails, you probably have com_hunkmegs set above 128." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) &
        "Change it to 128. It CANNOT be higher.")
        '
        'StartGameButton
        '
        Me.StartGameButton.Location = New System.Drawing.Point(4, 32)
        Me.StartGameButton.Name = "StartGameButton"
        Me.StartGameButton.Size = New System.Drawing.Size(161, 25)
        Me.StartGameButton.TabIndex = 5
        Me.StartGameButton.Text = "Start Game"
        Me.StartGameButton.UseVisualStyleBackColor = True
        '
        'TextBoxTimer
        '
        Me.TextBoxTimer.Enabled = True
        Me.TextBoxTimer.Interval = 200
        '
        'debugb
        '
        Me.debugb.Location = New System.Drawing.Point(305, 154)
        Me.debugb.Name = "debugb"
        Me.debugb.Size = New System.Drawing.Size(86, 22)
        Me.debugb.TabIndex = 6
        Me.debugb.Text = "debug"
        Me.debugb.UseVisualStyleBackColor = True
        Me.debugb.Visible = False
        '
        'HotKeyHandler
        '
        Me.HotKeyHandler.Enabled = True
        Me.HotKeyHandler.Interval = 80
        '
        'UpdateButton
        '
        Me.UpdateButton.Location = New System.Drawing.Point(185, 28)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(70, 25)
        Me.UpdateButton.TabIndex = 11
        Me.UpdateButton.Text = "Update"
        Me.ToolTipHandler.SetToolTip(Me.UpdateButton, "Update the program to the latest version.")
        Me.UpdateButton.UseVisualStyleBackColor = True
        Me.UpdateButton.Visible = False
        '
        'CheckUpdatesLabel
        '
        Me.CheckUpdatesLabel.AutoSize = True
        Me.CheckUpdatesLabel.BackColor = System.Drawing.Color.Transparent
        Me.CheckUpdatesLabel.Location = New System.Drawing.Point(125, 9)
        Me.CheckUpdatesLabel.Name = "CheckUpdatesLabel"
        Me.CheckUpdatesLabel.Size = New System.Drawing.Size(117, 13)
        Me.CheckUpdatesLabel.TabIndex = 12
        Me.CheckUpdatesLabel.Text = "Checking for updates..."
        '
        'UpdateCheckTimer
        '
        Me.UpdateCheckTimer.Enabled = True
        Me.UpdateCheckTimer.Interval = 300000
        '
        'LaunchParametersLB
        '
        Me.LaunchParametersLB.AutoSize = True
        Me.LaunchParametersLB.BackColor = System.Drawing.Color.Transparent
        Me.LaunchParametersLB.Location = New System.Drawing.Point(1, 74)
        Me.LaunchParametersLB.Name = "LaunchParametersLB"
        Me.LaunchParametersLB.Size = New System.Drawing.Size(102, 13)
        Me.LaunchParametersLB.TabIndex = 24
        Me.LaunchParametersLB.Text = "Launch Parameters:"
        Me.ToolTipHandler.SetToolTip(Me.LaunchParametersLB, "Start the game with these launch parameters." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Formerly known as ""Command Line""" &
        ")" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'LaunchParametersTB
        '
        Me.LaunchParametersTB.Location = New System.Drawing.Point(4, 90)
        Me.LaunchParametersTB.Multiline = True
        Me.LaunchParametersTB.Name = "LaunchParametersTB"
        Me.LaunchParametersTB.Size = New System.Drawing.Size(270, 20)
        Me.LaunchParametersTB.TabIndex = 25
        Me.ToolTipHandler.SetToolTip(Me.LaunchParametersTB, "Start the game with these launch parameters." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Example:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "+set name ""PlayerName"" +s" &
        "et r_fullscreen 1 +set r_mode -1 +connect 192.168.1.1")
        '
        'HackyAppBranchLB
        '
        Me.HackyAppBranchLB.AutoSize = True
        Me.HackyAppBranchLB.BackColor = System.Drawing.Color.Transparent
        Me.HackyAppBranchLB.Location = New System.Drawing.Point(2, 261)
        Me.HackyAppBranchLB.Name = "HackyAppBranchLB"
        Me.HackyAppBranchLB.Size = New System.Drawing.Size(126, 13)
        Me.HackyAppBranchLB.TabIndex = 26
        Me.HackyAppBranchLB.Text = "Application Branch: 0000"
        '
        'HackyGameVersLB
        '
        Me.HackyGameVersLB.AutoSize = True
        Me.HackyGameVersLB.BackColor = System.Drawing.Color.Transparent
        Me.HackyGameVersLB.Location = New System.Drawing.Point(12, 306)
        Me.HackyGameVersLB.Name = "HackyGameVersLB"
        Me.HackyGameVersLB.Size = New System.Drawing.Size(112, 13)
        Me.HackyGameVersLB.TabIndex = 27
        Me.HackyGameVersLB.Text = "CoDUO Version: 0000"
        '
        'FogCheckBox
        '
        Me.FogCheckBox.AutoSize = True
        Me.FogCheckBox.Location = New System.Drawing.Point(296, 74)
        Me.FogCheckBox.Name = "FogCheckBox"
        Me.FogCheckBox.Size = New System.Drawing.Size(44, 17)
        Me.FogCheckBox.TabIndex = 30
        Me.FogCheckBox.Text = "Fog"
        Me.ToolTipHandler.SetToolTip(Me.FogCheckBox, "If not checked, fog will not be enabled. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "!! WARNING !! You can be detected/kick" &
        "ed/banned by PunkBuster if using this. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "!! Do not use on PB servers. !!")
        Me.FogCheckBox.UseVisualStyleBackColor = True
        '
        'FogTimer
        '
        Me.FogTimer.Interval = 2000
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Select your UO installation path."
        '
        'FoVFixTimer
        '
        Me.FoVFixTimer.Enabled = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MinimizeIcon
        '
        Me.MinimizeIcon.Icon = CType(resources.GetObject("MinimizeIcon.Icon"), System.Drawing.Icon)
        Me.MinimizeIcon.Text = "CoDUO FoV Changer"
        '
        'MinimizeCheckBox
        '
        Me.MinimizeCheckBox.AutoSize = True
        Me.MinimizeCheckBox.Location = New System.Drawing.Point(296, 51)
        Me.MinimizeCheckBox.Name = "MinimizeCheckBox"
        Me.MinimizeCheckBox.Size = New System.Drawing.Size(98, 17)
        Me.MinimizeCheckBox.TabIndex = 45
        Me.MinimizeCheckBox.Text = "Minimize to tray"
        Me.ToolTipHandler.SetToolTip(Me.MinimizeCheckBox, "If checked, the program will go to the tray when minimized.")
        Me.MinimizeCheckBox.UseVisualStyleBackColor = True
        '
        'HackyAppVersLB
        '
        Me.HackyAppVersLB.AutoSize = True
        Me.HackyAppVersLB.BackColor = System.Drawing.Color.Transparent
        Me.HackyAppVersLB.Location = New System.Drawing.Point(12, 293)
        Me.HackyAppVersLB.Name = "HackyAppVersLB"
        Me.HackyAppVersLB.Size = New System.Drawing.Size(127, 13)
        Me.HackyAppVersLB.TabIndex = 48
        Me.HackyAppVersLB.Text = "Application Version: 0000"
        '
        'ABITWTimer
        '
        Me.ABITWTimer.Enabled = True
        Me.ABITWTimer.Interval = 1200
        '
        'CoD1CheckBox
        '
        Me.CoD1CheckBox.AutoSize = True
        Me.CoD1CheckBox.Location = New System.Drawing.Point(296, 28)
        Me.CoD1CheckBox.Name = "CoD1CheckBox"
        Me.CoD1CheckBox.Size = New System.Drawing.Size(78, 17)
        Me.CoD1CheckBox.TabIndex = 49
        Me.CoD1CheckBox.Text = "VCoD v1.5"
        Me.ToolTipHandler.SetToolTip(Me.CoD1CheckBox, "Enable/disable Call of Duty 1 version.")
        Me.CoD1CheckBox.UseVisualStyleBackColor = True
        '
        'FileSizeLabel
        '
        Me.FileSizeLabel.AutoSize = True
        Me.FileSizeLabel.Location = New System.Drawing.Point(232, 163)
        Me.FileSizeLabel.Name = "FileSizeLabel"
        Me.FileSizeLabel.Size = New System.Drawing.Size(49, 13)
        Me.FileSizeLabel.TabIndex = 50
        Me.FileSizeLabel.Text = "File Size:"
        Me.FileSizeLabel.Visible = False
        '
        'FileSizeLabel2
        '
        Me.FileSizeLabel2.AutoSize = True
        Me.FileSizeLabel2.Location = New System.Drawing.Point(278, 163)
        Me.FileSizeLabel2.Name = "FileSizeLabel2"
        Me.FileSizeLabel2.Size = New System.Drawing.Size(31, 13)
        Me.FileSizeLabel2.TabIndex = 51
        Me.FileSizeLabel2.Text = "0000"
        Me.FileSizeLabel2.Visible = False
        '
        'DvarsCheckBox
        '
        Me.DvarsCheckBox.AutoSize = True
        Me.DvarsCheckBox.Location = New System.Drawing.Point(128, 187)
        Me.DvarsCheckBox.Name = "DvarsCheckBox"
        Me.DvarsCheckBox.Size = New System.Drawing.Size(105, 17)
        Me.DvarsCheckBox.TabIndex = 52
        Me.DvarsCheckBox.Text = "Unlock All Dvars"
        Me.DvarsCheckBox.UseVisualStyleBackColor = True
        '
        'CoDPictureBox
        '
        Me.CoDPictureBox.Location = New System.Drawing.Point(346, 90)
        Me.CoDPictureBox.Name = "CoDPictureBox"
        Me.CoDPictureBox.Size = New System.Drawing.Size(45, 46)
        Me.CoDPictureBox.TabIndex = 53
        Me.CoDPictureBox.TabStop = False
        '
        'FoVMenuStrip
        '
        Me.FoVMenuStrip.BackColor = System.Drawing.SystemColors.Control
        Me.FoVMenuStrip.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.FoVMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.FoVMenuStrip.Location = New System.Drawing.Point(0, 165)
        Me.FoVMenuStrip.Name = "FoVMenuStrip"
        Me.FoVMenuStrip.Size = New System.Drawing.Size(399, 24)
        Me.FoVMenuStrip.TabIndex = 54
        Me.FoVMenuStrip.Text = "MenuStrip1"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InfoToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'InfoToolStripMenuItem
        '
        Me.InfoToolStripMenuItem.Name = "InfoToolStripMenuItem"
        Me.InfoToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.InfoToolStripMenuItem.Text = "About"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.ChangelogToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(47, 20)
        Me.ToolStripMenuItem1.Text = "Tools"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ChangelogToolStripMenuItem
        '
        Me.ChangelogToolStripMenuItem.Name = "ChangelogToolStripMenuItem"
        Me.ChangelogToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.ChangelogToolStripMenuItem.Text = "Changelog"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'CmdLineTimer
        '
        Me.CmdLineTimer.Enabled = True
        Me.CmdLineTimer.Interval = 10000
        '
        'DvarUnlockerTimer
        '
        Me.DvarUnlockerTimer.Interval = 2650
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(85, 762)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 55
        '
        'HackyFoVComboBox
        '
        Me.HackyFoVComboBox.FormattingEnabled = True
        Me.HackyFoVComboBox.Location = New System.Drawing.Point(202, 326)
        Me.HackyFoVComboBox.Name = "HackyFoVComboBox"
        Me.HackyFoVComboBox.Size = New System.Drawing.Size(121, 21)
        Me.HackyFoVComboBox.TabIndex = 56
        Me.HackyFoVComboBox.Visible = False
        '
        'GameTracker
        '
        Me.GameTracker.Enabled = True
        Me.GameTracker.Interval = 1000
        '
        'GameTimeLabel
        '
        Me.GameTimeLabel.AutoSize = True
        Me.GameTimeLabel.Location = New System.Drawing.Point(135, 61)
        Me.GameTimeLabel.Name = "GameTimeLabel"
        Me.GameTimeLabel.Size = New System.Drawing.Size(67, 13)
        Me.GameTimeLabel.TabIndex = 57
        Me.GameTimeLabel.Text = "Game Time: "
        Me.ToolTipHandler.SetToolTip(Me.GameTimeLabel, "Your total time spent in-game.")
        '
        'GameTimeSaver
        '
        Me.GameTimeSaver.Enabled = True
        Me.GameTimeSaver.Interval = 5000
        '
        'CurSessionGT
        '
        Me.CurSessionGT.AutoSize = True
        Me.CurSessionGT.Location = New System.Drawing.Point(135, 74)
        Me.CurSessionGT.Name = "CurSessionGT"
        Me.CurSessionGT.Size = New System.Drawing.Size(87, 13)
        Me.CurSessionGT.TabIndex = 58
        Me.CurSessionGT.Text = "Current Session: "
        Me.ToolTipHandler.SetToolTip(Me.CurSessionGT, "Your total time spent in this current session.")
        '
        'MainFoV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(399, 189)
        Me.Controls.Add(Me.CurSessionGT)
        Me.Controls.Add(Me.GameTimeLabel)
        Me.Controls.Add(Me.HackyFoVComboBox)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.FoVMenuStrip)
        Me.Controls.Add(Me.CoDPictureBox)
        Me.Controls.Add(Me.DvarsCheckBox)
        Me.Controls.Add(Me.FileSizeLabel2)
        Me.Controls.Add(Me.FileSizeLabel)
        Me.Controls.Add(Me.CoD1CheckBox)
        Me.Controls.Add(Me.HackyAppVersLB)
        Me.Controls.Add(Me.MinimizeCheckBox)
        Me.Controls.Add(Me.FogCheckBox)
        Me.Controls.Add(Me.HackyGameVersLB)
        Me.Controls.Add(Me.HackyAppBranchLB)
        Me.Controls.Add(Me.LaunchParametersTB)
        Me.Controls.Add(Me.LaunchParametersLB)
        Me.Controls.Add(Me.CheckUpdatesLabel)
        Me.Controls.Add(Me.UpdateButton)
        Me.Controls.Add(Me.debugb)
        Me.Controls.Add(Me.StartGameButton)
        Me.Controls.Add(Me.StatusLabel)
        Me.Controls.Add(Me.FoVLabel)
        Me.Controls.Add(Me.FoVTextBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.FoVMenuStrip
        Me.MaximizeBox = False
        Me.Name = "MainFoV"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CoDUO FoV Changer"
        CType(Me.CoDPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FoVMenuStrip.ResumeLayout(False)
        Me.FoVMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FoVTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FoVLabel As System.Windows.Forms.Label
    Friend WithEvents FoVTimer As System.Windows.Forms.Timer
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents StartGameButton As System.Windows.Forms.Button
    Friend WithEvents TextBoxTimer As System.Windows.Forms.Timer
    Friend WithEvents debugb As System.Windows.Forms.Button
    Friend WithEvents HotKeyHandler As System.Windows.Forms.Timer
    Friend WithEvents UpdateButton As System.Windows.Forms.Button
    Friend WithEvents CheckUpdatesLabel As System.Windows.Forms.Label
    Friend WithEvents UpdateCheckTimer As System.Windows.Forms.Timer
    Friend WithEvents LaunchParametersLB As System.Windows.Forms.Label
    Friend WithEvents LaunchParametersTB As System.Windows.Forms.TextBox
    Friend WithEvents HackyAppBranchLB As System.Windows.Forms.Label
    Friend WithEvents HackyGameVersLB As System.Windows.Forms.Label
    Friend WithEvents FogCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FogTimer As System.Windows.Forms.Timer
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents FoVFixTimer As System.Windows.Forms.Timer
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Private WithEvents MinimizeIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents MinimizeCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTipHandler As System.Windows.Forms.ToolTip
    Friend WithEvents HackyAppVersLB As System.Windows.Forms.Label
    Friend WithEvents ABITWTimer As System.Windows.Forms.Timer
    Friend WithEvents CoD1CheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FileSizeLabel As System.Windows.Forms.Label
    Friend WithEvents FileSizeLabel2 As System.Windows.Forms.Label
    Friend WithEvents ToolTipHandler2 As System.Windows.Forms.ToolTip
    Friend WithEvents DvarsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CoDPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents FoVMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CmdLineTimer As System.Windows.Forms.Timer
    Friend WithEvents DvarUnlockerTimer As System.Windows.Forms.Timer
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents HackyFoVComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ChangelogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GameTracker As Timer
    Friend WithEvents GameTimeLabel As Label
    Friend WithEvents GameTimeSaver As Timer
    Friend WithEvents CurSessionGT As Label
End Class

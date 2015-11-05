﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainFoV
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainFoV))
        Me.FoVTextBox = New System.Windows.Forms.TextBox()
        Me.FoVLabel = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.StartGameButton = New System.Windows.Forms.Button()
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.debugb = New System.Windows.Forms.Button()
        Me.Timer4 = New System.Windows.Forms.Timer(Me.components)
        Me.Button6 = New System.Windows.Forms.Button()
        Me.CheckUpdatesLabel = New System.Windows.Forms.Label()
        Me.Timer6 = New System.Windows.Forms.Timer(Me.components)
        Me.LaunchParametersLB = New System.Windows.Forms.Label()
        Me.LaunchParametersTB = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.FogCheckBox = New System.Windows.Forms.CheckBox()
        Me.ChangeLogButton = New System.Windows.Forms.Button()
        Me.Timer11 = New System.Windows.Forms.Timer(Me.components)
        Me.SettingsButton = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Timer7 = New System.Windows.Forms.Timer(Me.components)
        Me.Button15 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.MinimizeCheckBox = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Timer5 = New System.Windows.Forms.Timer(Me.components)
        Me.CoD1CheckBox = New System.Windows.Forms.CheckBox()
        Me.FileSizeLabel = New System.Windows.Forms.Label()
        Me.FileSizeLabel2 = New System.Windows.Forms.Label()
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DvarsCheckBox = New System.Windows.Forms.CheckBox()
        Me.CoDPictureBox = New System.Windows.Forms.PictureBox()
        Me.FoVMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdvancedSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer9 = New System.Windows.Forms.Timer(Me.components)
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.HackyFoVComboBox = New System.Windows.Forms.ComboBox()
        CType(Me.CoDPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FoVMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'FoVTextBox
        '
        Me.FoVTextBox.Location = New System.Drawing.Point(85, 9)
        Me.FoVTextBox.Name = "FoVTextBox"
        Me.FoVTextBox.Size = New System.Drawing.Size(187, 20)
        Me.FoVTextBox.TabIndex = 0
        Me.FoVTextBox.Text = "80"
        '
        'FoVLabel
        '
        Me.FoVLabel.AutoSize = True
        Me.FoVLabel.Location = New System.Drawing.Point(9, 9)
        Me.FoVLabel.Name = "FoVLabel"
        Me.FoVLabel.Size = New System.Drawing.Size(70, 13)
        Me.FoVLabel.TabIndex = 2
        Me.FoVLabel.Text = "Field of View:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1500
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = True
        Me.StatusLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusLabel.ForeColor = System.Drawing.Color.Red
        Me.StatusLabel.Location = New System.Drawing.Point(1, 139)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(307, 18)
        Me.StatusLabel.TabIndex = 3
        Me.StatusLabel.Text = "Status: not found or failed to write to memory!"
        Me.ToolTip1.SetToolTip(Me.StatusLabel, "The status of the FoV Changer. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If it is not found or failed to write to memory," & _
        " start the game." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If this fails, you probably have com_hunkmegs set above 128." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
        "Change it to 128. It CANNOT be higher.")
        '
        'StartGameButton
        '
        Me.StartGameButton.Location = New System.Drawing.Point(2, 35)
        Me.StartGameButton.Name = "StartGameButton"
        Me.StartGameButton.Size = New System.Drawing.Size(270, 31)
        Me.StartGameButton.TabIndex = 5
        Me.StartGameButton.Text = "Start Game"
        Me.StartGameButton.UseVisualStyleBackColor = True
        '
        'Timer3
        '
        Me.Timer3.Enabled = True
        Me.Timer3.Interval = 200
        '
        'debugb
        '
        Me.debugb.Location = New System.Drawing.Point(315, 142)
        Me.debugb.Name = "debugb"
        Me.debugb.Size = New System.Drawing.Size(86, 22)
        Me.debugb.TabIndex = 6
        Me.debugb.Text = "debug"
        Me.debugb.UseVisualStyleBackColor = True
        Me.debugb.Visible = False
        '
        'Timer4
        '
        Me.Timer4.Enabled = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(281, 25)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(114, 25)
        Me.Button6.TabIndex = 11
        Me.Button6.Text = "Update"
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'CheckUpdatesLabel
        '
        Me.CheckUpdatesLabel.AutoSize = True
        Me.CheckUpdatesLabel.BackColor = System.Drawing.Color.Transparent
        Me.CheckUpdatesLabel.Location = New System.Drawing.Point(278, 9)
        Me.CheckUpdatesLabel.Name = "CheckUpdatesLabel"
        Me.CheckUpdatesLabel.Size = New System.Drawing.Size(117, 13)
        Me.CheckUpdatesLabel.TabIndex = 12
        Me.CheckUpdatesLabel.Text = "Checking for updates..."
        '
        'Timer6
        '
        Me.Timer6.Enabled = True
        Me.Timer6.Interval = 45000
        '
        'LaunchParametersLB
        '
        Me.LaunchParametersLB.AutoSize = True
        Me.LaunchParametersLB.BackColor = System.Drawing.Color.Transparent
        Me.LaunchParametersLB.Location = New System.Drawing.Point(1, 69)
        Me.LaunchParametersLB.Name = "LaunchParametersLB"
        Me.LaunchParametersLB.Size = New System.Drawing.Size(102, 13)
        Me.LaunchParametersLB.TabIndex = 24
        Me.LaunchParametersLB.Text = "Launch Parameters:"
        Me.ToolTip1.SetToolTip(Me.LaunchParametersLB, "Start the game with these launch parameters." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Formerly known as ""Command Line""" & _
        ")" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'LaunchParametersTB
        '
        Me.LaunchParametersTB.Location = New System.Drawing.Point(4, 85)
        Me.LaunchParametersTB.Multiline = True
        Me.LaunchParametersTB.Name = "LaunchParametersTB"
        Me.LaunchParametersTB.Size = New System.Drawing.Size(270, 20)
        Me.LaunchParametersTB.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.LaunchParametersTB, "Start the game with these launch parameters." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Formerly known as ""Command Line""" & _
        ")" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(2, 261)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(126, 13)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Application Branch: 0000"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(12, 306)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(112, 13)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "CoDUO Version: 0000"
        '
        'FogCheckBox
        '
        Me.FogCheckBox.AutoSize = True
        Me.FogCheckBox.Location = New System.Drawing.Point(532, 78)
        Me.FogCheckBox.Name = "FogCheckBox"
        Me.FogCheckBox.Size = New System.Drawing.Size(44, 17)
        Me.FogCheckBox.TabIndex = 30
        Me.FogCheckBox.Text = "Fog"
        Me.FogCheckBox.UseVisualStyleBackColor = True
        '
        'ChangeLogButton
        '
        Me.ChangeLogButton.Location = New System.Drawing.Point(428, 107)
        Me.ChangeLogButton.Name = "ChangeLogButton"
        Me.ChangeLogButton.Size = New System.Drawing.Size(86, 22)
        Me.ChangeLogButton.TabIndex = 31
        Me.ChangeLogButton.Text = "Changelog"
        Me.ChangeLogButton.UseVisualStyleBackColor = True
        '
        'Timer11
        '
        Me.Timer11.Interval = 2000
        '
        'SettingsButton
        '
        Me.SettingsButton.Location = New System.Drawing.Point(520, 107)
        Me.SettingsButton.Name = "SettingsButton"
        Me.SettingsButton.Size = New System.Drawing.Size(86, 22)
        Me.SettingsButton.TabIndex = 36
        Me.SettingsButton.Text = "Settings"
        Me.SettingsButton.UseVisualStyleBackColor = True
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Select your UO installation path."
        '
        'Timer7
        '
        Me.Timer7.Enabled = True
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(304, 252)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(86, 22)
        Me.Button15.TabIndex = 44
        Me.Button15.Text = "Show CD-Key"
        Me.Button15.UseVisualStyleBackColor = True
        Me.Button15.Visible = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "CoDUO FoV Changer"
        '
        'MinimizeCheckBox
        '
        Me.MinimizeCheckBox.AutoSize = True
        Me.MinimizeCheckBox.Location = New System.Drawing.Point(428, 78)
        Me.MinimizeCheckBox.Name = "MinimizeCheckBox"
        Me.MinimizeCheckBox.Size = New System.Drawing.Size(98, 17)
        Me.MinimizeCheckBox.TabIndex = 45
        Me.MinimizeCheckBox.Text = "Minimize to tray"
        Me.MinimizeCheckBox.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(12, 293)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(127, 13)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Application Version: 0000"
        '
        'Timer5
        '
        Me.Timer5.Enabled = True
        Me.Timer5.Interval = 1200
        '
        'CoD1CheckBox
        '
        Me.CoD1CheckBox.AutoSize = True
        Me.CoD1CheckBox.Location = New System.Drawing.Point(428, 55)
        Me.CoD1CheckBox.Name = "CoD1CheckBox"
        Me.CoD1CheckBox.Size = New System.Drawing.Size(71, 17)
        Me.CoD1CheckBox.TabIndex = 49
        Me.CoD1CheckBox.Text = "CoD v1.5"
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
        Me.DvarsCheckBox.Location = New System.Drawing.Point(428, 30)
        Me.DvarsCheckBox.Name = "DvarsCheckBox"
        Me.DvarsCheckBox.Size = New System.Drawing.Size(105, 17)
        Me.DvarsCheckBox.TabIndex = 52
        Me.DvarsCheckBox.Text = "Unlock All Dvars"
        Me.DvarsCheckBox.UseVisualStyleBackColor = True
        '
        'CoDPictureBox
        '
        Me.CoDPictureBox.Location = New System.Drawing.Point(308, 56)
        Me.CoDPictureBox.Name = "CoDPictureBox"
        Me.CoDPictureBox.Size = New System.Drawing.Size(80, 80)
        Me.CoDPictureBox.TabIndex = 53
        Me.CoDPictureBox.TabStop = False
        '
        'FoVMenuStrip
        '
        Me.FoVMenuStrip.BackColor = System.Drawing.SystemColors.Control
        Me.FoVMenuStrip.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.FoVMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.FoVMenuStrip.Location = New System.Drawing.Point(0, 179)
        Me.FoVMenuStrip.Name = "FoVMenuStrip"
        Me.FoVMenuStrip.Size = New System.Drawing.Size(608, 24)
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
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.AdvancedSettingsToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(47, 20)
        Me.ToolStripMenuItem1.Text = "Tools"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'AdvancedSettingsToolStripMenuItem
        '
        Me.AdvancedSettingsToolStripMenuItem.Name = "AdvancedSettingsToolStripMenuItem"
        Me.AdvancedSettingsToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.AdvancedSettingsToolStripMenuItem.Text = "Advanced Settings"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Timer2
        '
        Me.Timer2.Enabled = True
        Me.Timer2.Interval = 10000
        '
        'Timer9
        '
        Me.Timer9.Interval = 2650
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
        Me.HackyFoVComboBox.Location = New System.Drawing.Point(12, 160)
        Me.HackyFoVComboBox.Name = "HackyFoVComboBox"
        Me.HackyFoVComboBox.Size = New System.Drawing.Size(121, 21)
        Me.HackyFoVComboBox.TabIndex = 56
        Me.HackyFoVComboBox.Visible = False
        '
        'MainFoV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(608, 203)
        Me.Controls.Add(Me.HackyFoVComboBox)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.FoVMenuStrip)
        Me.Controls.Add(Me.CoDPictureBox)
        Me.Controls.Add(Me.DvarsCheckBox)
        Me.Controls.Add(Me.FileSizeLabel2)
        Me.Controls.Add(Me.FileSizeLabel)
        Me.Controls.Add(Me.CoD1CheckBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.MinimizeCheckBox)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.SettingsButton)
        Me.Controls.Add(Me.ChangeLogButton)
        Me.Controls.Add(Me.FogCheckBox)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.LaunchParametersTB)
        Me.Controls.Add(Me.LaunchParametersLB)
        Me.Controls.Add(Me.CheckUpdatesLabel)
        Me.Controls.Add(Me.Button6)
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
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents StatusLabel As System.Windows.Forms.Label
    Friend WithEvents StartGameButton As System.Windows.Forms.Button
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents debugb As System.Windows.Forms.Button
    Friend WithEvents Timer4 As System.Windows.Forms.Timer
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents CheckUpdatesLabel As System.Windows.Forms.Label
    Friend WithEvents Timer6 As System.Windows.Forms.Timer
    Friend WithEvents LaunchParametersLB As System.Windows.Forms.Label
    Friend WithEvents LaunchParametersTB As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents FogCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ChangeLogButton As System.Windows.Forms.Button
    Friend WithEvents Timer11 As System.Windows.Forms.Timer
    Friend WithEvents SettingsButton As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Timer7 As System.Windows.Forms.Timer
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Private WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents MinimizeCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Timer5 As System.Windows.Forms.Timer
    Friend WithEvents CoD1CheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents FileSizeLabel As System.Windows.Forms.Label
    Friend WithEvents FileSizeLabel2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents DvarsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CoDPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents FoVMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdvancedSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Timer9 As System.Windows.Forms.Timer
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents HackyFoVComboBox As System.Windows.Forms.ComboBox

End Class

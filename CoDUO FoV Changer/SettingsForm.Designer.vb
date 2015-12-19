<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingsForm
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
        Me.GamePathButton = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SetGamePathDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.CloseSettingsButton = New System.Windows.Forms.Button()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.InformationLabel = New System.Windows.Forms.Label()
        Me.CustomizationLabel = New System.Windows.Forms.Label()
        Me.StyleLabel = New System.Windows.Forms.Label()
        Me.StyleCBox = New System.Windows.Forms.ComboBox()
        Me.AppBranchLabel = New System.Windows.Forms.Label()
        Me.AppVersLabel = New System.Windows.Forms.Label()
        Me.GameVersLabel = New System.Windows.Forms.Label()
        Me.CDKeyLabel = New System.Windows.Forms.Label()
        Me.ButtonSettingsAdvanced = New System.Windows.Forms.Button()
        Me.ToolTipHandler = New System.Windows.Forms.ToolTip(Me.components)
        Me.RestartAppButton = New System.Windows.Forms.Button()
        Me.ButtonBrowseGameFiles = New System.Windows.Forms.Button()
        Me.ButtonSelectGamePID = New System.Windows.Forms.Button()
        Me.SetupKeysButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'GamePathButton
        '
        Me.GamePathButton.Location = New System.Drawing.Point(12, 12)
        Me.GamePathButton.Name = "GamePathButton"
        Me.GamePathButton.Size = New System.Drawing.Size(173, 23)
        Me.GamePathButton.TabIndex = 0
        Me.GamePathButton.Text = "Change Game Path"
        Me.ToolTipHandler.SetToolTip(Me.GamePathButton, "Change your game's install location, as according to the FoV Changer.")
        Me.GamePathButton.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(651, 259)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(149, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Visible = False
        '
        'CloseSettingsButton
        '
        Me.CloseSettingsButton.Location = New System.Drawing.Point(12, 41)
        Me.CloseSettingsButton.Name = "CloseSettingsButton"
        Me.CloseSettingsButton.Size = New System.Drawing.Size(173, 23)
        Me.CloseSettingsButton.TabIndex = 2
        Me.CloseSettingsButton.Text = "Close"
        Me.CloseSettingsButton.UseVisualStyleBackColor = True
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(666, 167)
        Me.TextBox5.MaxLength = 50
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.TextBox5.Size = New System.Drawing.Size(95, 16)
        Me.TextBox5.TabIndex = 10
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(666, 237)
        Me.TextBox6.MaxLength = 50
        Me.TextBox6.Multiline = True
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.ReadOnly = True
        Me.TextBox6.Size = New System.Drawing.Size(95, 16)
        Me.TextBox6.TabIndex = 13
        '
        'InformationLabel
        '
        Me.InformationLabel.AutoSize = True
        Me.InformationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InformationLabel.Location = New System.Drawing.Point(12, 256)
        Me.InformationLabel.Name = "InformationLabel"
        Me.InformationLabel.Size = New System.Drawing.Size(152, 15)
        Me.InformationLabel.TabIndex = 18
        Me.InformationLabel.Text = "Additional Information:"
        '
        'CustomizationLabel
        '
        Me.CustomizationLabel.AutoSize = True
        Me.CustomizationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomizationLabel.Location = New System.Drawing.Point(12, 210)
        Me.CustomizationLabel.Name = "CustomizationLabel"
        Me.CustomizationLabel.Size = New System.Drawing.Size(161, 16)
        Me.CustomizationLabel.TabIndex = 23
        Me.CustomizationLabel.Text = "Customization Options"
        '
        'StyleLabel
        '
        Me.StyleLabel.AutoSize = True
        Me.StyleLabel.Location = New System.Drawing.Point(12, 235)
        Me.StyleLabel.Name = "StyleLabel"
        Me.StyleLabel.Size = New System.Drawing.Size(33, 13)
        Me.StyleLabel.TabIndex = 24
        Me.StyleLabel.Text = "Style:"
        '
        'StyleCBox
        '
        Me.StyleCBox.FormattingEnabled = True
        Me.StyleCBox.Items.AddRange(New Object() {"Default", "Light"})
        Me.StyleCBox.Location = New System.Drawing.Point(50, 232)
        Me.StyleCBox.Name = "StyleCBox"
        Me.StyleCBox.Size = New System.Drawing.Size(114, 21)
        Me.StyleCBox.TabIndex = 25
        '
        'AppBranchLabel
        '
        Me.AppBranchLabel.AutoSize = True
        Me.AppBranchLabel.BackColor = System.Drawing.Color.Transparent
        Me.AppBranchLabel.Location = New System.Drawing.Point(12, 271)
        Me.AppBranchLabel.Name = "AppBranchLabel"
        Me.AppBranchLabel.Size = New System.Drawing.Size(126, 13)
        Me.AppBranchLabel.TabIndex = 27
        Me.AppBranchLabel.Text = "Application Branch: 0000"
        '
        'AppVersLabel
        '
        Me.AppVersLabel.AutoSize = True
        Me.AppVersLabel.BackColor = System.Drawing.Color.Transparent
        Me.AppVersLabel.Location = New System.Drawing.Point(12, 284)
        Me.AppVersLabel.Name = "AppVersLabel"
        Me.AppVersLabel.Size = New System.Drawing.Size(127, 13)
        Me.AppVersLabel.TabIndex = 49
        Me.AppVersLabel.Text = "Application Version: 0000"
        '
        'GameVersLabel
        '
        Me.GameVersLabel.AutoSize = True
        Me.GameVersLabel.BackColor = System.Drawing.Color.Transparent
        Me.GameVersLabel.Location = New System.Drawing.Point(12, 297)
        Me.GameVersLabel.Name = "GameVersLabel"
        Me.GameVersLabel.Size = New System.Drawing.Size(115, 13)
        Me.GameVersLabel.TabIndex = 50
        Me.GameVersLabel.Text = "CoD:UO Version: 0000"
        '
        'CDKeyLabel
        '
        Me.CDKeyLabel.AutoSize = True
        Me.CDKeyLabel.BackColor = System.Drawing.Color.Transparent
        Me.CDKeyLabel.Location = New System.Drawing.Point(12, 310)
        Me.CDKeyLabel.Name = "CDKeyLabel"
        Me.CDKeyLabel.Size = New System.Drawing.Size(46, 13)
        Me.CDKeyLabel.TabIndex = 51
        Me.CDKeyLabel.Text = "CD-Key:"
        '
        'ButtonSettingsAdvanced
        '
        Me.ButtonSettingsAdvanced.Location = New System.Drawing.Point(12, 185)
        Me.ButtonSettingsAdvanced.Name = "ButtonSettingsAdvanced"
        Me.ButtonSettingsAdvanced.Size = New System.Drawing.Size(173, 22)
        Me.ButtonSettingsAdvanced.TabIndex = 59
        Me.ButtonSettingsAdvanced.Text = "Additional Settings"
        Me.ToolTipHandler.SetToolTip(Me.ButtonSettingsAdvanced, "Config file settings, and other more advanced settings.")
        Me.ButtonSettingsAdvanced.UseVisualStyleBackColor = True
        '
        'ToolTipHandler
        '
        '
        'RestartAppButton
        '
        Me.RestartAppButton.Location = New System.Drawing.Point(12, 70)
        Me.RestartAppButton.Name = "RestartAppButton"
        Me.RestartAppButton.Size = New System.Drawing.Size(173, 22)
        Me.RestartAppButton.TabIndex = 63
        Me.RestartAppButton.Text = "Restart App"
        Me.ToolTipHandler.SetToolTip(Me.RestartAppButton, "Restart the program.")
        Me.RestartAppButton.UseVisualStyleBackColor = True
        '
        'ButtonBrowseGameFiles
        '
        Me.ButtonBrowseGameFiles.Location = New System.Drawing.Point(12, 98)
        Me.ButtonBrowseGameFiles.Name = "ButtonBrowseGameFiles"
        Me.ButtonBrowseGameFiles.Size = New System.Drawing.Size(173, 23)
        Me.ButtonBrowseGameFiles.TabIndex = 64
        Me.ButtonBrowseGameFiles.Text = "Browse Local Game Files"
        Me.ToolTipHandler.SetToolTip(Me.ButtonBrowseGameFiles, "Open an explorer window where CoD is installed.")
        Me.ButtonBrowseGameFiles.UseVisualStyleBackColor = True
        '
        'ButtonSelectGamePID
        '
        Me.ButtonSelectGamePID.Location = New System.Drawing.Point(12, 127)
        Me.ButtonSelectGamePID.Name = "ButtonSelectGamePID"
        Me.ButtonSelectGamePID.Size = New System.Drawing.Size(173, 23)
        Me.ButtonSelectGamePID.TabIndex = 65
        Me.ButtonSelectGamePID.Text = "Select Process ID"
        Me.ToolTipHandler.SetToolTip(Me.ButtonSelectGamePID, "Select the process for the FoV changer to write to.")
        Me.ButtonSelectGamePID.UseVisualStyleBackColor = True
        '
        'SetupKeysButton
        '
        Me.SetupKeysButton.Location = New System.Drawing.Point(12, 156)
        Me.SetupKeysButton.Name = "SetupKeysButton"
        Me.SetupKeysButton.Size = New System.Drawing.Size(173, 23)
        Me.SetupKeysButton.TabIndex = 66
        Me.SetupKeysButton.Text = "Setup FoV Hot Keys"
        Me.ToolTipHandler.SetToolTip(Me.SetupKeysButton, "Setup hotkeys to quickly change your fov between presets")
        Me.SetupKeysButton.UseVisualStyleBackColor = True
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(189, 326)
        Me.ControlBox = False
        Me.Controls.Add(Me.SetupKeysButton)
        Me.Controls.Add(Me.ButtonSelectGamePID)
        Me.Controls.Add(Me.ButtonBrowseGameFiles)
        Me.Controls.Add(Me.RestartAppButton)
        Me.Controls.Add(Me.ButtonSettingsAdvanced)
        Me.Controls.Add(Me.CDKeyLabel)
        Me.Controls.Add(Me.GameVersLabel)
        Me.Controls.Add(Me.AppVersLabel)
        Me.Controls.Add(Me.AppBranchLabel)
        Me.Controls.Add(Me.StyleCBox)
        Me.Controls.Add(Me.StyleLabel)
        Me.Controls.Add(Me.CustomizationLabel)
        Me.Controls.Add(Me.InformationLabel)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.CloseSettingsButton)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.GamePathButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "SettingsForm"
        Me.ShowIcon = False
        Me.Text = "Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GamePathButton As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents SetGamePathDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents CloseSettingsButton As System.Windows.Forms.Button
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents InformationLabel As System.Windows.Forms.Label
    Friend WithEvents CustomizationLabel As System.Windows.Forms.Label
    Friend WithEvents StyleLabel As System.Windows.Forms.Label
    Friend WithEvents StyleCBox As System.Windows.Forms.ComboBox
    Friend WithEvents AppBranchLabel As System.Windows.Forms.Label
    Friend WithEvents AppVersLabel As System.Windows.Forms.Label
    Friend WithEvents GameVersLabel As System.Windows.Forms.Label
    Friend WithEvents CDKeyLabel As System.Windows.Forms.Label
    Friend WithEvents ButtonSettingsAdvanced As System.Windows.Forms.Button
    Friend WithEvents ToolTipHandler As System.Windows.Forms.ToolTip
    Friend WithEvents RestartAppButton As System.Windows.Forms.Button
    Friend WithEvents ButtonBrowseGameFiles As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectGamePID As System.Windows.Forms.Button
    Friend WithEvents SetupKeysButton As System.Windows.Forms.Button
End Class

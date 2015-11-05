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
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.CloseSettingsButton = New System.Windows.Forms.Button()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.PictureButton = New System.Windows.Forms.Button()
        Me.InformationLabel = New System.Windows.Forms.Label()
        Me.CustomizationLabel = New System.Windows.Forms.Label()
        Me.StyleLabel = New System.Windows.Forms.Label()
        Me.StyleCBox = New System.Windows.Forms.ComboBox()
        Me.AppBranchLabel = New System.Windows.Forms.Label()
        Me.AppVersLabel = New System.Windows.Forms.Label()
        Me.GameVersLabel = New System.Windows.Forms.Label()
        Me.CDKeyLabel = New System.Windows.Forms.Label()
        Me.ButtonSettingsAdvanced = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RestartAppButton = New System.Windows.Forms.Button()
        Me.ButtonBroweGameFiles = New System.Windows.Forms.Button()
        Me.ButtonSelectGamePID = New System.Windows.Forms.Button()
        Me.IsNet451Label = New System.Windows.Forms.Label()
        Me.ButtonAddFoVCB = New System.Windows.Forms.Button()
        Me.CBBoxFoV = New System.Windows.Forms.ComboBox()
        Me.RemoveFoVCBBox = New System.Windows.Forms.Button()
        Me.FoVHotKeyShowForm = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'GamePathButton
        '
        Me.GamePathButton.Location = New System.Drawing.Point(12, 12)
        Me.GamePathButton.Name = "GamePathButton"
        Me.GamePathButton.Size = New System.Drawing.Size(173, 23)
        Me.GamePathButton.TabIndex = 0
        Me.GamePathButton.Text = "Set Game Path"
        Me.ToolTip1.SetToolTip(Me.GamePathButton, "Change your game's install location, as according to the FoV Changer.")
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
        Me.CloseSettingsButton.Location = New System.Drawing.Point(12, 70)
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
        'PictureButton
        '
        Me.PictureButton.Location = New System.Drawing.Point(12, 41)
        Me.PictureButton.Name = "PictureButton"
        Me.PictureButton.Size = New System.Drawing.Size(173, 23)
        Me.PictureButton.TabIndex = 17
        Me.PictureButton.Text = "Picture Comparisons"
        Me.PictureButton.UseVisualStyleBackColor = True
        '
        'InformationLabel
        '
        Me.InformationLabel.AutoSize = True
        Me.InformationLabel.Location = New System.Drawing.Point(12, 96)
        Me.InformationLabel.Name = "InformationLabel"
        Me.InformationLabel.Size = New System.Drawing.Size(111, 13)
        Me.InformationLabel.TabIndex = 18
        Me.InformationLabel.Text = "Additional Information:"
        '
        'CustomizationLabel
        '
        Me.CustomizationLabel.AutoSize = True
        Me.CustomizationLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomizationLabel.Location = New System.Drawing.Point(12, 183)
        Me.CustomizationLabel.Name = "CustomizationLabel"
        Me.CustomizationLabel.Size = New System.Drawing.Size(161, 16)
        Me.CustomizationLabel.TabIndex = 23
        Me.CustomizationLabel.Text = "Customization Options"
        '
        'StyleLabel
        '
        Me.StyleLabel.AutoSize = True
        Me.StyleLabel.Location = New System.Drawing.Point(9, 205)
        Me.StyleLabel.Name = "StyleLabel"
        Me.StyleLabel.Size = New System.Drawing.Size(33, 13)
        Me.StyleLabel.TabIndex = 24
        Me.StyleLabel.Text = "Style:"
        '
        'StyleCBox
        '
        Me.StyleCBox.FormattingEnabled = True
        Me.StyleCBox.Items.AddRange(New Object() {"Default", "Minimal", "Dark"})
        Me.StyleCBox.Location = New System.Drawing.Point(48, 202)
        Me.StyleCBox.Name = "StyleCBox"
        Me.StyleCBox.Size = New System.Drawing.Size(114, 21)
        Me.StyleCBox.TabIndex = 25
        '
        'AppBranchLabel
        '
        Me.AppBranchLabel.AutoSize = True
        Me.AppBranchLabel.BackColor = System.Drawing.Color.Transparent
        Me.AppBranchLabel.Location = New System.Drawing.Point(12, 109)
        Me.AppBranchLabel.Name = "AppBranchLabel"
        Me.AppBranchLabel.Size = New System.Drawing.Size(126, 13)
        Me.AppBranchLabel.TabIndex = 27
        Me.AppBranchLabel.Text = "Application Branch: 0000"
        '
        'AppVersLabel
        '
        Me.AppVersLabel.AutoSize = True
        Me.AppVersLabel.BackColor = System.Drawing.Color.Transparent
        Me.AppVersLabel.Location = New System.Drawing.Point(12, 122)
        Me.AppVersLabel.Name = "AppVersLabel"
        Me.AppVersLabel.Size = New System.Drawing.Size(127, 13)
        Me.AppVersLabel.TabIndex = 49
        Me.AppVersLabel.Text = "Application Version: 0000"
        '
        'GameVersLabel
        '
        Me.GameVersLabel.AutoSize = True
        Me.GameVersLabel.BackColor = System.Drawing.Color.Transparent
        Me.GameVersLabel.Location = New System.Drawing.Point(12, 135)
        Me.GameVersLabel.Name = "GameVersLabel"
        Me.GameVersLabel.Size = New System.Drawing.Size(115, 13)
        Me.GameVersLabel.TabIndex = 50
        Me.GameVersLabel.Text = "CoD:UO Version: 0000"
        '
        'CDKeyLabel
        '
        Me.CDKeyLabel.AutoSize = True
        Me.CDKeyLabel.BackColor = System.Drawing.Color.Transparent
        Me.CDKeyLabel.Location = New System.Drawing.Point(12, 148)
        Me.CDKeyLabel.Name = "CDKeyLabel"
        Me.CDKeyLabel.Size = New System.Drawing.Size(46, 13)
        Me.CDKeyLabel.TabIndex = 51
        Me.CDKeyLabel.Text = "CD-Key:"
        '
        'ButtonSettingsAdvanced
        '
        Me.ButtonSettingsAdvanced.Location = New System.Drawing.Point(12, 257)
        Me.ButtonSettingsAdvanced.Name = "ButtonSettingsAdvanced"
        Me.ButtonSettingsAdvanced.Size = New System.Drawing.Size(150, 22)
        Me.ButtonSettingsAdvanced.TabIndex = 59
        Me.ButtonSettingsAdvanced.Text = "Advanced Settings"
        Me.ToolTip1.SetToolTip(Me.ButtonSettingsAdvanced, "Config file settings, and other more advanced settings.")
        Me.ButtonSettingsAdvanced.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        '
        'RestartAppButton
        '
        Me.RestartAppButton.Location = New System.Drawing.Point(12, 229)
        Me.RestartAppButton.Name = "RestartAppButton"
        Me.RestartAppButton.Size = New System.Drawing.Size(150, 22)
        Me.RestartAppButton.TabIndex = 63
        Me.RestartAppButton.Text = "Restart App"
        Me.ToolTip1.SetToolTip(Me.RestartAppButton, "Restart the program.")
        Me.RestartAppButton.UseVisualStyleBackColor = True
        '
        'ButtonBroweGameFiles
        '
        Me.ButtonBroweGameFiles.Location = New System.Drawing.Point(12, 285)
        Me.ButtonBroweGameFiles.Name = "ButtonBroweGameFiles"
        Me.ButtonBroweGameFiles.Size = New System.Drawing.Size(150, 23)
        Me.ButtonBroweGameFiles.TabIndex = 64
        Me.ButtonBroweGameFiles.Text = "Browse Local Game Files"
        Me.ToolTip1.SetToolTip(Me.ButtonBroweGameFiles, "Open an explorer window where CoD is installed.")
        Me.ButtonBroweGameFiles.UseVisualStyleBackColor = True
        '
        'ButtonSelectGamePID
        '
        Me.ButtonSelectGamePID.Location = New System.Drawing.Point(12, 314)
        Me.ButtonSelectGamePID.Name = "ButtonSelectGamePID"
        Me.ButtonSelectGamePID.Size = New System.Drawing.Size(150, 23)
        Me.ButtonSelectGamePID.TabIndex = 65
        Me.ButtonSelectGamePID.Text = "Select Process for FoV"
        Me.ToolTip1.SetToolTip(Me.ButtonSelectGamePID, "Select the process for the FoV changer to write to.")
        Me.ButtonSelectGamePID.UseVisualStyleBackColor = True
        '
        'IsNet451Label
        '
        Me.IsNet451Label.AutoSize = True
        Me.IsNet451Label.BackColor = System.Drawing.Color.Transparent
        Me.IsNet451Label.Location = New System.Drawing.Point(12, 161)
        Me.IsNet451Label.Name = "IsNet451Label"
        Me.IsNet451Label.Size = New System.Drawing.Size(85, 13)
        Me.IsNet451Label.TabIndex = 61
        Me.IsNet451Label.Text = "Using .net 4.5.1:"
        '
        'ButtonAddFoVCB
        '
        Me.ButtonAddFoVCB.Location = New System.Drawing.Point(12, 343)
        Me.ButtonAddFoVCB.Name = "ButtonAddFoVCB"
        Me.ButtonAddFoVCB.Size = New System.Drawing.Size(150, 37)
        Me.ButtonAddFoVCB.TabIndex = 66
        Me.ButtonAddFoVCB.Text = "Add current FoV to hotkey ComboBox"
        Me.ButtonAddFoVCB.UseVisualStyleBackColor = True
        '
        'CBBoxFoV
        '
        Me.CBBoxFoV.FormattingEnabled = True
        Me.CBBoxFoV.Location = New System.Drawing.Point(12, 386)
        Me.CBBoxFoV.Name = "CBBoxFoV"
        Me.CBBoxFoV.Size = New System.Drawing.Size(150, 21)
        Me.CBBoxFoV.TabIndex = 67
        '
        'RemoveFoVCBBox
        '
        Me.RemoveFoVCBBox.Location = New System.Drawing.Point(12, 413)
        Me.RemoveFoVCBBox.Name = "RemoveFoVCBBox"
        Me.RemoveFoVCBBox.Size = New System.Drawing.Size(150, 23)
        Me.RemoveFoVCBBox.TabIndex = 68
        Me.RemoveFoVCBBox.Text = "Remove selected FoV"
        Me.RemoveFoVCBBox.UseVisualStyleBackColor = True
        '
        'FoVHotKeyShowForm
        '
        Me.FoVHotKeyShowForm.Location = New System.Drawing.Point(12, 442)
        Me.FoVHotKeyShowForm.Name = "FoVHotKeyShowForm"
        Me.FoVHotKeyShowForm.Size = New System.Drawing.Size(150, 23)
        Me.FoVHotKeyShowForm.TabIndex = 69
        Me.FoVHotKeyShowForm.Text = "Select FoV Hotkey"
        Me.FoVHotKeyShowForm.UseVisualStyleBackColor = True
        '
        'SettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(191, 471)
        Me.ControlBox = False
        Me.Controls.Add(Me.FoVHotKeyShowForm)
        Me.Controls.Add(Me.RemoveFoVCBBox)
        Me.Controls.Add(Me.CBBoxFoV)
        Me.Controls.Add(Me.ButtonAddFoVCB)
        Me.Controls.Add(Me.ButtonSelectGamePID)
        Me.Controls.Add(Me.ButtonBroweGameFiles)
        Me.Controls.Add(Me.RestartAppButton)
        Me.Controls.Add(Me.IsNet451Label)
        Me.Controls.Add(Me.ButtonSettingsAdvanced)
        Me.Controls.Add(Me.CDKeyLabel)
        Me.Controls.Add(Me.GameVersLabel)
        Me.Controls.Add(Me.AppVersLabel)
        Me.Controls.Add(Me.AppBranchLabel)
        Me.Controls.Add(Me.StyleCBox)
        Me.Controls.Add(Me.StyleLabel)
        Me.Controls.Add(Me.CustomizationLabel)
        Me.Controls.Add(Me.InformationLabel)
        Me.Controls.Add(Me.PictureButton)
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
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents CloseSettingsButton As System.Windows.Forms.Button
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents PictureButton As System.Windows.Forms.Button
    Friend WithEvents InformationLabel As System.Windows.Forms.Label
    Friend WithEvents CustomizationLabel As System.Windows.Forms.Label
    Friend WithEvents StyleLabel As System.Windows.Forms.Label
    Friend WithEvents StyleCBox As System.Windows.Forms.ComboBox
    Friend WithEvents AppBranchLabel As System.Windows.Forms.Label
    Friend WithEvents AppVersLabel As System.Windows.Forms.Label
    Friend WithEvents GameVersLabel As System.Windows.Forms.Label
    Friend WithEvents CDKeyLabel As System.Windows.Forms.Label
    Friend WithEvents ButtonSettingsAdvanced As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents IsNet451Label As System.Windows.Forms.Label
    Friend WithEvents RestartAppButton As System.Windows.Forms.Button
    Friend WithEvents ButtonBroweGameFiles As System.Windows.Forms.Button
    Friend WithEvents ButtonSelectGamePID As System.Windows.Forms.Button
    Friend WithEvents ButtonAddFoVCB As System.Windows.Forms.Button
    Friend WithEvents CBBoxFoV As System.Windows.Forms.ComboBox
    Friend WithEvents RemoveFoVCBBox As System.Windows.Forms.Button
    Friend WithEvents FoVHotKeyShowForm As System.Windows.Forms.Button
End Class

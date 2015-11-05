<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdvSettingsForm
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
        Me.SaveRestartAppButton = New System.Windows.Forms.Button()
        Me.FirstRunCheckBox = New System.Windows.Forms.CheckBox()
        Me.DisableUpdateTimerCheck = New System.Windows.Forms.CheckBox()
        Me.CancelButton = New System.Windows.Forms.Button()
        Me.OpenConfigButton = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'SaveRestartAppButton
        '
        Me.SaveRestartAppButton.Location = New System.Drawing.Point(12, 58)
        Me.SaveRestartAppButton.Name = "SaveRestartAppButton"
        Me.SaveRestartAppButton.Size = New System.Drawing.Size(107, 23)
        Me.SaveRestartAppButton.TabIndex = 0
        Me.SaveRestartAppButton.Text = "Save And Restart"
        Me.SaveRestartAppButton.UseVisualStyleBackColor = True
        '
        'FirstRunCheckBox
        '
        Me.FirstRunCheckBox.AutoSize = True
        Me.FirstRunCheckBox.Location = New System.Drawing.Point(12, 12)
        Me.FirstRunCheckBox.Name = "FirstRunCheckBox"
        Me.FirstRunCheckBox.Size = New System.Drawing.Size(68, 17)
        Me.FirstRunCheckBox.TabIndex = 3
        Me.FirstRunCheckBox.Text = "First Run"
        Me.ToolTip1.SetToolTip(Me.FirstRunCheckBox, "Tell the program to act as if the user has never ran" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "this program before.")
        Me.FirstRunCheckBox.UseVisualStyleBackColor = True
        '
        'DisableUpdateTimerCheck
        '
        Me.DisableUpdateTimerCheck.AutoSize = True
        Me.DisableUpdateTimerCheck.Location = New System.Drawing.Point(12, 35)
        Me.DisableUpdateTimerCheck.Name = "DisableUpdateTimerCheck"
        Me.DisableUpdateTimerCheck.Size = New System.Drawing.Size(128, 17)
        Me.DisableUpdateTimerCheck.TabIndex = 4
        Me.DisableUpdateTimerCheck.Text = "Disable Update Timer"
        Me.ToolTip1.SetToolTip(Me.DisableUpdateTimerCheck, "Disables periodically checking for updates every few minutes " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "while the program " & _
        "is running.")
        Me.DisableUpdateTimerCheck.UseVisualStyleBackColor = True
        '
        'CancelButton
        '
        Me.CancelButton.Location = New System.Drawing.Point(12, 116)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(107, 22)
        Me.CancelButton.TabIndex = 11
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'OpenConfigButton
        '
        Me.OpenConfigButton.Location = New System.Drawing.Point(12, 87)
        Me.OpenConfigButton.Name = "OpenConfigButton"
        Me.OpenConfigButton.Size = New System.Drawing.Size(107, 23)
        Me.OpenConfigButton.TabIndex = 12
        Me.OpenConfigButton.Text = "Open Config"
        Me.OpenConfigButton.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(225, 289)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(154, 34)
        Me.Button2.TabIndex = 14
        Me.Button2.Text = "Search for UO (Potentially Lengthy)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(222, 191)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(142, 81)
        Me.TextBox1.TabIndex = 15
        '
        'AdvSettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(135, 144)
        Me.ControlBox = False
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.OpenConfigButton)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.DisableUpdateTimerCheck)
        Me.Controls.Add(Me.FirstRunCheckBox)
        Me.Controls.Add(Me.SaveRestartAppButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "AdvSettingsForm"
        Me.ShowIcon = False
        Me.Text = "Advanced Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SaveRestartAppButton As System.Windows.Forms.Button
    Friend WithEvents FirstRunCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DisableUpdateTimerCheck As System.Windows.Forms.CheckBox
    Friend WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents OpenConfigButton As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

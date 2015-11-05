<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangeHotKeyForm
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
        Me.CloseHotKeyFormButton = New System.Windows.Forms.Button()
        Me.PressAnyKeyLabel = New System.Windows.Forms.Label()
        Me.UpHotKeyLabel = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.KeyComboLabel = New System.Windows.Forms.Label()
        Me.DownHotKeyLabel = New System.Windows.Forms.Label()
        Me.UpHotKeyRB = New System.Windows.Forms.RadioButton()
        Me.DownHotKeyRB = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'CloseHotKeyFormButton
        '
        Me.CloseHotKeyFormButton.Location = New System.Drawing.Point(12, 146)
        Me.CloseHotKeyFormButton.Name = "CloseHotKeyFormButton"
        Me.CloseHotKeyFormButton.Size = New System.Drawing.Size(260, 23)
        Me.CloseHotKeyFormButton.TabIndex = 0
        Me.CloseHotKeyFormButton.Text = "Close"
        Me.CloseHotKeyFormButton.UseVisualStyleBackColor = True
        '
        'PressAnyKeyLabel
        '
        Me.PressAnyKeyLabel.AutoSize = True
        Me.PressAnyKeyLabel.Location = New System.Drawing.Point(9, 9)
        Me.PressAnyKeyLabel.Name = "PressAnyKeyLabel"
        Me.PressAnyKeyLabel.Size = New System.Drawing.Size(224, 13)
        Me.PressAnyKeyLabel.TabIndex = 1
        Me.PressAnyKeyLabel.Text = "Press any key to select it as your FoV hot key."
        '
        'UpHotKeyLabel
        '
        Me.UpHotKeyLabel.AutoSize = True
        Me.UpHotKeyLabel.Location = New System.Drawing.Point(12, 35)
        Me.UpHotKeyLabel.Name = "UpHotKeyLabel"
        Me.UpHotKeyLabel.Size = New System.Drawing.Size(65, 13)
        Me.UpHotKeyLabel.TabIndex = 2
        Me.UpHotKeyLabel.Text = "Up Hot Key:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 130
        '
        'KeyComboLabel
        '
        Me.KeyComboLabel.AutoSize = True
        Me.KeyComboLabel.Location = New System.Drawing.Point(12, 117)
        Me.KeyComboLabel.Name = "KeyComboLabel"
        Me.KeyComboLabel.Size = New System.Drawing.Size(273, 26)
        Me.KeyComboLabel.TabIndex = 3
        Me.KeyComboLabel.Text = "Please note this does not currently support key combos. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(e.g, shift+K)"
        '
        'DownHotKeyLabel
        '
        Me.DownHotKeyLabel.AutoSize = True
        Me.DownHotKeyLabel.Location = New System.Drawing.Point(12, 59)
        Me.DownHotKeyLabel.Name = "DownHotKeyLabel"
        Me.DownHotKeyLabel.Size = New System.Drawing.Size(79, 13)
        Me.DownHotKeyLabel.TabIndex = 4
        Me.DownHotKeyLabel.Text = "Down Hot Key:"
        '
        'UpHotKeyRB
        '
        Me.UpHotKeyRB.AutoSize = True
        Me.UpHotKeyRB.Location = New System.Drawing.Point(12, 97)
        Me.UpHotKeyRB.Name = "UpHotKeyRB"
        Me.UpHotKeyRB.Size = New System.Drawing.Size(80, 17)
        Me.UpHotKeyRB.TabIndex = 5
        Me.UpHotKeyRB.TabStop = True
        Me.UpHotKeyRB.Text = "Up Hot Key"
        Me.UpHotKeyRB.UseVisualStyleBackColor = True
        '
        'DownHotKeyRB
        '
        Me.DownHotKeyRB.AutoSize = True
        Me.DownHotKeyRB.Location = New System.Drawing.Point(178, 97)
        Me.DownHotKeyRB.Name = "DownHotKeyRB"
        Me.DownHotKeyRB.Size = New System.Drawing.Size(94, 17)
        Me.DownHotKeyRB.TabIndex = 6
        Me.DownHotKeyRB.TabStop = True
        Me.DownHotKeyRB.Text = "Down Hot Key"
        Me.DownHotKeyRB.UseVisualStyleBackColor = True
        '
        'ChangeHotKeyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 175)
        Me.Controls.Add(Me.DownHotKeyRB)
        Me.Controls.Add(Me.UpHotKeyRB)
        Me.Controls.Add(Me.DownHotKeyLabel)
        Me.Controls.Add(Me.KeyComboLabel)
        Me.Controls.Add(Me.UpHotKeyLabel)
        Me.Controls.Add(Me.PressAnyKeyLabel)
        Me.Controls.Add(Me.CloseHotKeyFormButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "ChangeHotKeyForm"
        Me.ShowIcon = False
        Me.Text = "Change Hot Key"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CloseHotKeyFormButton As System.Windows.Forms.Button
    Friend WithEvents PressAnyKeyLabel As System.Windows.Forms.Label
    Friend WithEvents UpHotKeyLabel As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents KeyComboLabel As System.Windows.Forms.Label
    Friend WithEvents DownHotKeyLabel As System.Windows.Forms.Label
    Friend WithEvents UpHotKeyRB As System.Windows.Forms.RadioButton
    Friend WithEvents DownHotKeyRB As System.Windows.Forms.RadioButton
End Class

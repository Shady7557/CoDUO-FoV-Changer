<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FoVHotKeyForm
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
        Me.CloseFormButton = New System.Windows.Forms.Button()
        Me.ButtonAddFoVCB = New System.Windows.Forms.Button()
        Me.CBBoxFoV = New System.Windows.Forms.ComboBox()
        Me.RemoveFoVCBBox = New System.Windows.Forms.Button()
        Me.FoVHotKeyShowForm = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CloseFormButton
        '
        Me.CloseFormButton.Location = New System.Drawing.Point(12, 161)
        Me.CloseFormButton.Name = "CloseFormButton"
        Me.CloseFormButton.Size = New System.Drawing.Size(173, 23)
        Me.CloseFormButton.TabIndex = 0
        Me.CloseFormButton.Text = "Close"
        Me.CloseFormButton.UseVisualStyleBackColor = True
        '
        'ButtonAddFoVCB
        '
        Me.ButtonAddFoVCB.Location = New System.Drawing.Point(12, 12)
        Me.ButtonAddFoVCB.Name = "ButtonAddFoVCB"
        Me.ButtonAddFoVCB.Size = New System.Drawing.Size(173, 37)
        Me.ButtonAddFoVCB.TabIndex = 67
        Me.ButtonAddFoVCB.Text = "Add current FoV to hotkey ComboBox"
        Me.ButtonAddFoVCB.UseVisualStyleBackColor = True
        '
        'CBBoxFoV
        '
        Me.CBBoxFoV.FormattingEnabled = True
        Me.CBBoxFoV.Location = New System.Drawing.Point(12, 55)
        Me.CBBoxFoV.Name = "CBBoxFoV"
        Me.CBBoxFoV.Size = New System.Drawing.Size(173, 21)
        Me.CBBoxFoV.TabIndex = 68
        '
        'RemoveFoVCBBox
        '
        Me.RemoveFoVCBBox.Location = New System.Drawing.Point(12, 82)
        Me.RemoveFoVCBBox.Name = "RemoveFoVCBBox"
        Me.RemoveFoVCBBox.Size = New System.Drawing.Size(173, 23)
        Me.RemoveFoVCBBox.TabIndex = 69
        Me.RemoveFoVCBBox.Text = "Remove selected FoV"
        Me.RemoveFoVCBBox.UseVisualStyleBackColor = True
        '
        'FoVHotKeyShowForm
        '
        Me.FoVHotKeyShowForm.Location = New System.Drawing.Point(12, 111)
        Me.FoVHotKeyShowForm.Name = "FoVHotKeyShowForm"
        Me.FoVHotKeyShowForm.Size = New System.Drawing.Size(173, 23)
        Me.FoVHotKeyShowForm.TabIndex = 70
        Me.FoVHotKeyShowForm.Text = "Select FoV Hotkey"
        Me.FoVHotKeyShowForm.UseVisualStyleBackColor = True
        '
        'FoVHotKeyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(193, 196)
        Me.ControlBox = False
        Me.Controls.Add(Me.FoVHotKeyShowForm)
        Me.Controls.Add(Me.RemoveFoVCBBox)
        Me.Controls.Add(Me.CBBoxFoV)
        Me.Controls.Add(Me.ButtonAddFoVCB)
        Me.Controls.Add(Me.CloseFormButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "FoVHotKeyForm"
        Me.ShowIcon = False
        Me.Text = "Setup FoV Hotkey"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CloseFormButton As System.Windows.Forms.Button
    Friend WithEvents ButtonAddFoVCB As System.Windows.Forms.Button
    Friend WithEvents CBBoxFoV As System.Windows.Forms.ComboBox
    Friend WithEvents RemoveFoVCBBox As System.Windows.Forms.Button
    Friend WithEvents FoVHotKeyShowForm As System.Windows.Forms.Button
End Class

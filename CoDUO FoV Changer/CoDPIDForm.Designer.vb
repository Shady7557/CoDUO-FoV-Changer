<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CoDPIDForm
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
        Me.PIDListBox = New System.Windows.Forms.ListBox()
        Me.SelectPIDButton = New System.Windows.Forms.Button()
        Me.ClosePIDListButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'PIDListBox
        '
        Me.PIDListBox.FormattingEnabled = True
        Me.PIDListBox.Location = New System.Drawing.Point(5, 12)
        Me.PIDListBox.Name = "PIDListBox"
        Me.PIDListBox.Size = New System.Drawing.Size(362, 186)
        Me.PIDListBox.TabIndex = 0
        '
        'SelectPIDButton
        '
        Me.SelectPIDButton.Location = New System.Drawing.Point(5, 204)
        Me.SelectPIDButton.Name = "SelectPIDButton"
        Me.SelectPIDButton.Size = New System.Drawing.Size(362, 23)
        Me.SelectPIDButton.TabIndex = 1
        Me.SelectPIDButton.Text = "Select"
        Me.SelectPIDButton.UseVisualStyleBackColor = True
        '
        'ClosePIDListButton
        '
        Me.ClosePIDListButton.Location = New System.Drawing.Point(5, 233)
        Me.ClosePIDListButton.Name = "ClosePIDListButton"
        Me.ClosePIDListButton.Size = New System.Drawing.Size(362, 23)
        Me.ClosePIDListButton.TabIndex = 2
        Me.ClosePIDListButton.Text = "Close"
        Me.ClosePIDListButton.UseVisualStyleBackColor = True
        '
        'CoDPIDForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(371, 262)
        Me.ControlBox = False
        Me.Controls.Add(Me.ClosePIDListButton)
        Me.Controls.Add(Me.SelectPIDButton)
        Me.Controls.Add(Me.PIDListBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "CoDPIDForm"
        Me.ShowIcon = False
        Me.Text = "CoD Process List"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PIDListBox As System.Windows.Forms.ListBox
    Friend WithEvents SelectPIDButton As System.Windows.Forms.Button
    Friend WithEvents ClosePIDListButton As System.Windows.Forms.Button
End Class

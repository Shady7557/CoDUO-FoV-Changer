<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PicturesForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PicturesForm))
        Me.FoVPictureBox = New System.Windows.Forms.PictureBox()
        Me.ButtonPicture110 = New System.Windows.Forms.Button()
        Me.ButtonPicture100 = New System.Windows.Forms.Button()
        Me.ButtonPicture90 = New System.Windows.Forms.Button()
        Me.ButtonPicture80 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ActiveInternetLabel = New System.Windows.Forms.Label()
        CType(Me.FoVPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FoVPictureBox
        '
        Me.FoVPictureBox.Image = CType(resources.GetObject("FoVPictureBox.Image"), System.Drawing.Image)
        Me.FoVPictureBox.Location = New System.Drawing.Point(12, 12)
        Me.FoVPictureBox.Name = "FoVPictureBox"
        Me.FoVPictureBox.Size = New System.Drawing.Size(900, 500)
        Me.FoVPictureBox.TabIndex = 0
        Me.FoVPictureBox.TabStop = False
        '
        'ButtonPicture110
        '
        Me.ButtonPicture110.Location = New System.Drawing.Point(12, 518)
        Me.ButtonPicture110.Name = "ButtonPicture110"
        Me.ButtonPicture110.Size = New System.Drawing.Size(75, 23)
        Me.ButtonPicture110.TabIndex = 1
        Me.ButtonPicture110.Text = "110 FOV"
        Me.ButtonPicture110.UseVisualStyleBackColor = True
        '
        'ButtonPicture100
        '
        Me.ButtonPicture100.Location = New System.Drawing.Point(93, 518)
        Me.ButtonPicture100.Name = "ButtonPicture100"
        Me.ButtonPicture100.Size = New System.Drawing.Size(75, 23)
        Me.ButtonPicture100.TabIndex = 2
        Me.ButtonPicture100.Text = "100 FOV"
        Me.ButtonPicture100.UseVisualStyleBackColor = True
        '
        'ButtonPicture90
        '
        Me.ButtonPicture90.Location = New System.Drawing.Point(174, 518)
        Me.ButtonPicture90.Name = "ButtonPicture90"
        Me.ButtonPicture90.Size = New System.Drawing.Size(75, 23)
        Me.ButtonPicture90.TabIndex = 3
        Me.ButtonPicture90.Text = "90 FOV"
        Me.ButtonPicture90.UseVisualStyleBackColor = True
        '
        'ButtonPicture80
        '
        Me.ButtonPicture80.Location = New System.Drawing.Point(255, 518)
        Me.ButtonPicture80.Name = "ButtonPicture80"
        Me.ButtonPicture80.Size = New System.Drawing.Size(75, 23)
        Me.ButtonPicture80.TabIndex = 4
        Me.ButtonPicture80.Text = "80 FOV"
        Me.ButtonPicture80.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(336, 518)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "Close"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 2750
        '
        'ActiveInternetLabel
        '
        Me.ActiveInternetLabel.AutoSize = True
        Me.ActiveInternetLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ActiveInternetLabel.Location = New System.Drawing.Point(462, 521)
        Me.ActiveInternetLabel.Name = "ActiveInternetLabel"
        Me.ActiveInternetLabel.Size = New System.Drawing.Size(445, 15)
        Me.ActiveInternetLabel.TabIndex = 6
        Me.ActiveInternetLabel.Text = "An active internet connection may be required to load these pictures."
        '
        'PicturesForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(919, 543)
        Me.ControlBox = False
        Me.Controls.Add(Me.ActiveInternetLabel)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.ButtonPicture80)
        Me.Controls.Add(Me.ButtonPicture90)
        Me.Controls.Add(Me.ButtonPicture100)
        Me.Controls.Add(Me.ButtonPicture110)
        Me.Controls.Add(Me.FoVPictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "PicturesForm"
        Me.Text = "Form8"
        CType(Me.FoVPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FoVPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonPicture110 As System.Windows.Forms.Button
    Friend WithEvents ButtonPicture100 As System.Windows.Forms.Button
    Friend WithEvents ButtonPicture90 As System.Windows.Forms.Button
    Friend WithEvents ButtonPicture80 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ActiveInternetLabel As System.Windows.Forms.Label
End Class

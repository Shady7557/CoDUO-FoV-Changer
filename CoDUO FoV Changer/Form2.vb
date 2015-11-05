Imports System
Imports System.Management
Imports System.Runtime.InteropServices

Public Class Form3

    Public userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Public appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(appdata & "CoDUO FoV Changer\settings.ini")
    Dim sleep = ini.ReadValue("Tweaks", "SleepMili")
    Dim installpath = ini.ReadValue("Main", "InstallPath")
    ' Dim appversion = ini.ReadValue("Main", "AppType")
    '  Dim appversion As String = "x86"
    Dim isminimal = ini.ReadValue("Extras", "Style")
    Dim modern As Boolean
    Dim hide2 As Boolean
    Dim reso = ini.ReadValue("Extras", "Resolution")
    Public num1 As Integer = 0
    Public num2 As Integer = 0
    Dim cacheKey As String = ""
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetAsyncKeyState(ByVal vkey As System.Windows.Forms.Keys) As Short
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim shouldContinue As Boolean = False
        Try
            FolderBrowserDialog1.Description = ("Select your game path.")
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                'shouldContinue = True
            Else
                Return
            End If
            TextBox1.Text = FolderBrowserDialog1.SelectedPath


            Dim readvalue As String
            readvalue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "InstallPath", "game registry keys not found")
            If Form1.ostype = "32" Then
                readvalue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "InstallPath", "game registry keys not found")
            End If

            If readvalue = TextBox1.Text Then
                MsgBox("This is already your directory!", MsgBoxStyle.Information)
            Else
                If Form1.ostype = "64" Then
                    My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "InstallPath", TextBox1.Text)
                    MsgBox("Completed!", MsgBoxStyle.Information)
                Else
                    My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "InstallPath", TextBox1.Text)
                    MsgBox("Completed!", MsgBoxStyle.Information)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error setting registry, invalid path?", MsgBoxStyle.Critical)
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Visible = False
        'no need to close this, faster to hide it, plus it keeps the window's position when the user closes it.
    End Sub

    Private Sub showkey()
        Dim keyfind As String
        If Not cacheKey = "" Then
            If Label8.Text.Contains("Hidden") Then
                Label8.Text = "CD-Key: " & cacheKey & "    (Click to hide)"
            Else
                Label8.Text = "CD-Key: Hidden (Click to show)"
            End If
            Return
        End If
        If Form1.ostype = "64" Then
            keyfind = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!")
        Else
            keyfind = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!")
        End If

        If Label8.Text.Contains("Hidden") Then
            Label8.Text = "CD-Key: " & keyfind & "      (Click to hide)"
        Else
            Label8.Text = "CD-Key: Hidden (Click to show)"
        End If


        If Label8.Text = "0" Then
            Label8.Text = "No CD-Key found."
        End If

        If Label8.Text.Contains("Hidden") Then
            Me.Width = 205
        Else
            Me.Width = 215
        End If

        cacheKey = keyfind


    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        Dim splitStrr() As String
        If Form1.fovbox.Contains(",") Then
            splitStrr = Form1.fovbox.Split(",")
            For Each word In splitStrr
                If Not word = "" Then
                    '               MessageBox.Show(word)
                    If Not CInt(word) >= 121 Then
                        ComboBox1.Items.Add(word)
                    Else
                        '   Log.WriteLine(word & " is higher than 120 fov (max) will not add to combobox")
                    End If
                End If
            Next
        End If
        Dim itemlist As Integer = 0
        For Each item In ComboBox1.Items
            itemlist = itemlist + 1
        Next
        If itemlist >= 0 Then
            ComboBox1.SelectedIndex = 0
        End If
        '  Me.CenterToParent()
        'If Not Debugger.IsAttached = True Then
        'RadioButton1.Visible = False
        'RadioButton2.Visible = False
        'RadioButton3.Visible = False
        'Label12.Visible = False
        'Label9.Visible = False
        'ComboBox1.Visible = False
        'Button5.Location = New Point(187, 241)
        'Me.Location = New Point(317, 325)
        'Me.Width = 317
        ' Else
        '   RadioButton1.Checked = True
        '  End If

        If isminimal = "Minimal" Then
            Label10.Visible = True
            Label11.Visible = True
            Label8.Visible = True
            Label7.Visible = True
            ComboBox2.SelectedItem = "Minimal"
        ElseIf isminimal = "Default"
            Label11.Visible = False
            Label10.Visible = False
            Label8.Visible = False
            Label7.Visible = False
            ComboBox2.SelectedItem = "Default"
        ElseIf isminimal = "Dark"
            ComboBox2.SelectedItem = "Dark"
            Me.BackColor = Color.DimGray
            Button1.BackColor = Color.DarkGray
            Button2.BackColor = Color.DarkGray
            Button3.BackColor = Color.DarkGray
            Button4.BackColor = Color.DarkGray
            Button5.BackColor = Color.DarkGray
            Button7.BackColor = Color.DarkGray
            TextBox1.BackColor = Color.DarkGray
            ' TextBox2.BackColor = Color.DarkGray
            ComboBox2.BackColor = Color.DarkGray
            Button8.BackColor = Color.DarkGray
            Button9.BackColor = Color.DarkGray
            Button10.BackColor = Color.DarkGray
        End If
        If Form1.CheckBox3.Checked = True Then
            '   Button6.Enabled = False
            ToolTip1.SetToolTip(Button6, "This only supports CoDUO!")
        End If
        Dim readvalue2 As String = ""
        '    If Form1.ostype = "64" Then
        '        readvalue2 = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Version", 1.51) 'If the registry key is not found, it may report an error.
        '    ElseIf Form1.ostype = "86" Then
        '        readvalue2 = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Version", 1.51) 'If the registry key is not found, it may report an error.
        '    End If

        Label11.Text = Form1.Label11.Text 'Sets the label to read the games version from form1
        Label10.Text = Form1.Label10.Text 'Sets the label's text to contain the application branch.
        Dim testString As String = "Application Version: " & Application.ProductVersion
        Label7.Text = testString
        Dim keyfind As String
        If Form1.ostype = "64" Then
            keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys.
        Else
            keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys.
        End If

        Label8.Text = "CD-Key: " & keyfind & " (Click to hide)"

        If Label8.Text = "No CD-Key here..." Then
            Label8.Text = "CD-Key: Not Found!"
        End If

        If Form1.hidekey = "Yes" Then
            Label8.Text = "CD-Key: Hidden" & " (Click to show)"
        End If

        If My.Computer.FileSystem.FileExists(Application.StartupPath & "\CoDUO FoV Changer.exe.config") Then
            Dim srr As New System.IO.StreamReader(Application.StartupPath & "\CoDUO FoV Changer.exe.config")
            Dim whatami As String
            whatami = srr.ReadToEnd
            srr.Close()
            If whatami.Contains("4.5.1") Then
                Label4.Text = Label4.Text & " True"
            Else
                Label4.Text = Label4.Text & " False"
            End If
        Else
            Label4.Text = Label4.Text & " False"
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        '   Form8.Show()
        If Form8.Visible = False Then
            Form8.Visible = True
            '   Form1.Timer8.Start()
        Else
            Form8.Show()
            '     Form1.Timer8.Start()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedItem = "Minimal" Then
            Form1.Label10.Visible = False
            Form1.Label4.Visible = False
            Form1.Label11.Visible = False
            Label10.Visible = True
            Label11.Visible = True
            Label7.Visible = True
            Label8.Visible = True
            ' Form1.Label2.Location = New Point(0, 201)
            Form1.Height = 220
            ini.WriteValue("Extras", "Style", "Minimal")
        ElseIf ComboBox2.SelectedItem = "Default"
            ini.WriteValue("Extras", "Style", "Default")
            Form1.Label10.Visible = True
            Form1.Label4.Visible = True
            Form1.Label11.Visible = True
            Label10.Visible = False
            Label11.Visible = False
            Label7.Visible = False
            Label8.Visible = False
            Form1.Height = 260
            Form1.BackColor = Form1.DefaultBackColor
            Me.BackColor = DefaultBackColor
            Button1.BackColor = DefaultBackColor
            Button2.BackColor = DefaultBackColor
            Button3.BackColor = DefaultBackColor
            Button4.BackColor = DefaultBackColor
            Button5.BackColor = DefaultBackColor
            Button7.BackColor = DefaultBackColor
            TextBox1.BackColor = DefaultBackColor
            ' TextBox2.BackColor = DefaultBackColor
            ComboBox2.BackColor = DefaultBackColor
            ' Form1.Label2.Location = New Point(0, 162)
        ElseIf ComboBox2.SelectedItem = "Dark"
            Form1.BackColor = Color.DimGray
            Me.BackColor = Color.DimGray
            Button1.BackColor = Color.DarkGray
            Button2.BackColor = Color.DarkGray
            Button3.BackColor = Color.DarkGray
            Button4.BackColor = Color.DarkGray
            Button5.BackColor = Color.DarkGray
            Button7.BackColor = Color.DarkGray
            TextBox1.BackColor = Color.DarkGray
            Button6.BackColor = Color.DarkGray
            '    TextBox2.BackColor = Color.DarkGray
            ComboBox2.BackColor = Color.DarkGray
            Form1.MenuStrip1.BackColor = Color.DarkGray
            Button8.BackColor = Color.DarkGray
            Button9.BackColor = Color.DarkGray
            Button10.BackColor = Color.DarkGray
            ini.WriteValue("Extras", "Style", "Dark")
        End If

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        showkey()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Visible = False
        Form5.Show()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs)

        Try
            ini.WriteValue("Main", "BuildType", "x64")
            Dim myExe As String = Form1.temp & "\CoDUO FoV Changer Updater.exe"
            If Not System.IO.File.Exists(myExe) Then
                System.IO.File.WriteAllBytes(myExe, My.Resources.CoDUO_FoV_Changer_Updater)
                '  log("Creating Updater Application.")
            End If
            Process.Start(myExe)
            '  log("Restarting/Updating")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Unable to create updater application. Error: " & ex.Message, MsgBoxStyle.Critical)
            '  log("Unable to create Updater Application. !! ERROR !! " & ex.Message)
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Application.Restart()
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTip1.Popup

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Process.Start(installpath)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If Form1.CheckBox3.Checked = True Then
            '       Button6.Enabled = False
            ToolTip1.SetToolTip(Button6, "This only supports CoDUO!")
            MessageBox.Show("This only supports CoDUO!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Form6.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Not ComboBox1.Items.Contains(Form1.TextBox1.Text) Then
            ComboBox1.Items.Add(Form1.TextBox1.Text)
            ComboBox1.SelectedItem = Form1.TextBox1.Text
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Not ComboBox1.SelectedIndex < 0 Then
            Dim replace As String
            replace = Form1.fovbox.Replace(ComboBox1.SelectedItem.ToString & ",", "")
            ComboBox1.Items.Remove(ComboBox1.SelectedItem)
            ComboBox1.Text = ""
            ini.WriteValue("Main", "ComboBoxFoV", replace)
        End If
        If Not ComboBox1.Items.Count <= 0 Then
            ComboBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Text = ComboBox1.SelectedItem.ToString
        If Not Form1.ComboBox2.Items.Count <= 0 Then
            Form1.ComboBox2.SelectedIndex = ComboBox1.SelectedIndex
        End If

    End Sub
End Class
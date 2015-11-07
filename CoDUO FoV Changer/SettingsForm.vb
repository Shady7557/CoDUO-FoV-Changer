Imports System
Imports System.Management
Imports System.Runtime.InteropServices

Public Class SettingsForm

    Public userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Public appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(appdata & "CoDUO FoV Changer\settings.ini")
    Dim sleep = ini.ReadValue("Tweaks", "SleepMili")
    Dim installpath = ini.ReadValue("Main", "InstallPath")
    ' Dim appversion = ini.ReadValue("Main", "AppType")
    '  Dim appversion As String = "x86"
    Dim stylet = ini.ReadValue("Extras", "Style")
    Dim modern As Boolean
    Dim hide2 As Boolean
    Dim reso = ini.ReadValue("Extras", "Resolution")
    Public num1 As Integer = 0
    Public num2 As Integer = 0
    Dim cacheKey As String = ""
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetAsyncKeyState(ByVal vkey As System.Windows.Forms.Keys) As Short
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles GamePathButton.Click
        'Dim shouldContinue As Boolean = False
        Try
            SetGamePathDialog.Description = ("Select your game path.")
            If SetGamePathDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                'shouldContinue = True
            Else
                Return
            End If
            TextBox1.Text = SetGamePathDialog.SelectedPath


            Dim readvalue As String
            readvalue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "InstallPath", "game registry keys not found")
            If MainFoV.ostype = "32" Then
                readvalue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "InstallPath", "game registry keys not found")
            End If

            If readvalue = TextBox1.Text Then
                MsgBox("This is already your directory!", MsgBoxStyle.Information)
            Else
                If MainFoV.ostype = "64" Then
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles CloseSettingsButton.Click
        Me.Visible = False
        'no need to close this, faster to hide it, plus it keeps the window's position when the user closes it.
    End Sub

    Private Sub showkey()
        Dim keyfind As String
        If Not cacheKey = "" Then
            If CDKeyLabel.Text.Contains("Hidden") Then
                CDKeyLabel.Text = "CD-Key: " & cacheKey & "    (Click to hide)"
            Else
                CDKeyLabel.Text = "CD-Key: Hidden (Click to show)"
            End If
            Return
        End If
        If MainFoV.ostype = "64" Then
            keyfind = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!")
        Else
            keyfind = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!")
        End If

        If CDKeyLabel.Text.Contains("Hidden") Then
            CDKeyLabel.Text = "CD-Key: " & keyfind & "      (Click to hide)"
        Else
            CDKeyLabel.Text = "CD-Key: Hidden (Click to show)"
        End If


        If CDKeyLabel.Text = "0" Then
            CDKeyLabel.Text = "No CD-Key found."
        End If

        If CDKeyLabel.Text.Contains("Hidden") Then
            Me.Width = 205
        Else
            Me.Width = 215
        End If

        cacheKey = keyfind


    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
   
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

        If stylet = "Minimal" Then
            'AppBranchLabel.Visible = True
            'GameVersLabel.Visible = True
            'CDKeyLabel.Visible = True
            'AppVersLabel.Visible = True
            StyleCBox.SelectedItem = "Minimal"
        ElseIf stylet = "Default" Then
            'GameVersLabel.Visible = False
            'AppBranchLabel.Visible = False
            'CDKeyLabel.Visible = False
            'AppVersLabel.Visible = False
            StyleCBox.SelectedItem = "Default"
        ElseIf stylet = "Dark" Then
            StyleCBox.SelectedItem = "Dark"
            Me.BackColor = Color.DimGray
            GamePathButton.BackColor = Color.DarkGray
            CloseSettingsButton.BackColor = Color.DarkGray
            ButtonBrowseGameFiles.BackColor = Color.DarkGray
            ButtonSettingsAdvanced.BackColor = Color.DarkGray
            RestartAppButton.BackColor = Color.DarkGray
            TextBox1.BackColor = Color.DarkGray
            ' TextBox2.BackColor = Color.DarkGray
            StyleCBox.BackColor = Color.DarkGray
            SetupKeysButton.BackColor = Color.DarkGray
        End If
        If MainFoV.CoD1CheckBox.Checked = True Then
            '   Button6.Enabled = False
            ToolTipHandler.SetToolTip(ButtonSelectGamePID, "This only supports CoDUO!")
        End If
        Dim readvalue2 As String = ""
        '    If Form1.ostype = "64" Then
        '        readvalue2 = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Version", 1.51) 'If the registry key is not found, it may report an error.
        '    ElseIf Form1.ostype = "86" Then
        '        readvalue2 = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Version", 1.51) 'If the registry key is not found, it may report an error.
        '    End If

        GameVersLabel.Text = MainFoV.HackyGameVersLB.Text 'Sets the label to read the games version from form1
        AppBranchLabel.Text = MainFoV.HackyAppBranchLB.Text 'Sets the label's text to contain the application branch.
        Dim testString As String = "Application Version: " & Application.ProductVersion
        AppVersLabel.Text = testString
        Dim keyfind As String
        If MainFoV.ostype = "64" Then
            keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys.
        Else
            keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys.
        End If

        CDKeyLabel.Text = "CD-Key: " & keyfind & " (Click to hide)"

        If CDKeyLabel.Text = "No CD-Key here..." Then
            CDKeyLabel.Text = "CD-Key: Not Found!"
        End If

        If MainFoV.hidekey = "Yes" Then
            CDKeyLabel.Text = "CD-Key: Hidden" & " (Click to show)"
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles StyleCBox.SelectedIndexChanged
        If Not StyleCBox.SelectedItem.ToString = stylet Then
            Dim ask As MsgBoxResult = MessageBox.Show("The program must be restarted to (fully) change your style, would you like to restart it now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If ask = MsgBoxResult.Yes Then
                Application.Restart()
            End If
        End If
        If StyleCBox.SelectedItem = "Minimal" Then
            MainFoV.HackyAppBranchLB.Visible = False
            MainFoV.HackyAppVersLB.Visible = False
            MainFoV.HackyGameVersLB.Visible = False
            'AppBranchLabel.Visible = True
            'GameVersLabel.Visible = True
            'AppVersLabel.Visible = True
            'CDKeyLabel.Visible = True
            ' Form1.Label2.Location = New Point(0, 201)
            MainFoV.Height = 220
            ini.WriteValue("Extras", "Style", "Minimal")
        ElseIf StyleCBox.SelectedItem = "Default" Then
            ini.WriteValue("Extras", "Style", "Default")
            MainFoV.HackyAppBranchLB.Visible = True
            MainFoV.HackyAppVersLB.Visible = True
            MainFoV.HackyGameVersLB.Visible = True
            'AppBranchLabel.Visible = False
            'GameVersLabel.Visible = False
            'AppVersLabel.Visible = False
            'CDKeyLabel.Visible = False
            MainFoV.Height = 249
            MainFoV.BackColor = MainFoV.DefaultBackColor
            Me.BackColor = DefaultBackColor
            GamePathButton.BackColor = DefaultBackColor
            CloseSettingsButton.BackColor = DefaultBackColor
            ButtonBrowseGameFiles.BackColor = DefaultBackColor
            ButtonSettingsAdvanced.BackColor = DefaultBackColor
            RestartAppButton.BackColor = DefaultBackColor
            TextBox1.BackColor = DefaultBackColor
            ' TextBox2.BackColor = DefaultBackColor
            StyleCBox.BackColor = DefaultBackColor
            ' Form1.Label2.Location = New Point(0, 162)
        ElseIf StyleCBox.SelectedItem = "Dark" Then
            MainFoV.BackColor = Color.DimGray
            Me.BackColor = Color.DimGray
            GamePathButton.BackColor = Color.DarkGray
            CloseSettingsButton.BackColor = Color.DarkGray
            ButtonBrowseGameFiles.BackColor = Color.DarkGray
            ButtonSettingsAdvanced.BackColor = Color.DarkGray
            RestartAppButton.BackColor = Color.DarkGray
            TextBox1.BackColor = Color.DarkGray
            ButtonSelectGamePID.BackColor = Color.DarkGray
            '    TextBox2.BackColor = Color.DarkGray
            StyleCBox.BackColor = Color.DarkGray
            MainFoV.FoVMenuStrip.BackColor = Color.DarkGray
            SetupKeysButton.BackColor = Color.DarkGray
            ini.WriteValue("Extras", "Style", "Dark")
        End If

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles CDKeyLabel.Click
        showkey()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ButtonSettingsAdvanced.Click
        Me.Visible = False
        AdvSettingsForm.Show()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs)

        Try
            ini.WriteValue("Main", "BuildType", "x64")
            Dim myExe As String = MainFoV.temp & "\CoDUO FoV Changer Updater.exe"
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

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles RestartAppButton.Click
        Application.Restart()
    End Sub

    Private Sub ToolTip1_Popup(sender As Object, e As PopupEventArgs) Handles ToolTipHandler.Popup

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles ButtonBrowseGameFiles.Click
        Process.Start(installpath)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles ButtonSelectGamePID.Click
        If MainFoV.CoD1CheckBox.Checked = True Then
            '       Button6.Enabled = False
            ToolTipHandler.SetToolTip(ButtonSelectGamePID, "This only supports CoDUO!")
            MessageBox.Show("This only supports CoDUO!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        CoDPIDForm.Show()
    End Sub

    Private Sub SetupKeysButton_Click(sender As Object, e As EventArgs) Handles SetupKeysButton.Click
        FoVHotKeyForm.Show()
    End Sub
End Class
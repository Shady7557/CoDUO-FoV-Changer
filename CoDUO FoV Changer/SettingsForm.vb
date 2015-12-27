Option Strict On
Imports System
Imports System.Management
Imports System.Runtime.InteropServices

Public Class SettingsForm

    Public userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Public appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(appdata & "CoDUO FoV Changer\settings.ini")
    Dim sleep As String = ini.ReadValue("Tweaks", "SleepMili")
    Dim installpath As String = ini.ReadValue("Main", "InstallPath")
    ' Dim appversion = ini.ReadValue("Main", "AppType")
    '  Dim appversion As String = "x86"
    Dim stylet As String = ini.ReadValue("Extras", "Style")
    Dim modern As Boolean
    Dim hide2 As Boolean
    Dim reso As String = ini.ReadValue("Extras", "Resolution")
    Public num1 As Integer = 0
    Public num2 As Integer = 0
    Dim cacheKey As String = ""
    Dim defWidth As Integer = 207
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
            readvalue = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "InstallPath", "game registry keys not found"))
            If MainFoV.ostype = "32" Then
                readvalue = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "InstallPath", "game registry keys not found"))
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
        Try
            Dim keyfind As String
            If Not cacheKey = "" Then
                If CDKeyLabel.Text.Contains("Hidden") Then
                    CDKeyLabel.Text = "CD-Key: " & cacheKey & " (Click to hide)"
                    Width = 215
                Else
                    CDKeyLabel.Text = "CD-Key: Hidden (Click to show)"
                    Width = defWidth
                End If
                Return
            End If

            If MainFoV.ostype = "64" Then
                keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!"))
            Else
                keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!"))
            End If

            If CDKeyLabel.Text.Contains("Hidden") Then
                CDKeyLabel.Text = "CD-Key: " & keyfind & " (Click to hide)"
            Else
                CDKeyLabel.Text = "CD-Key: Hidden (Click to show)"
            End If


            If CDKeyLabel.Text = "0" Then
                CDKeyLabel.Text = "No CD-Key found."
            End If

            If CDKeyLabel.Text.Contains("Hidden") Then
                Width = defWidth
            Else
                Width = 215
            End If

            cacheKey = keyfind
        Catch ex As Exception
            MainFoV.WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("Error: " & ex.Message & Environment.NewLine & "  check the log for more info!", "CoDUO FoV Changer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If stylet = "Default" Then
            StyleCBox.SelectedItem = "Default"
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
        ElseIf stylet = "Light" Then
            StyleCBox.SelectedItem = "Light"
        End If
        Dim readvalue2 As String = ""

        GameVersLabel.Text = MainFoV.HackyGameVersLB.Text 'Sets the label to read the games version from form1
        AppBranchLabel.Text = MainFoV.HackyAppBranchLB.Text 'Sets the label's text to contain the application branch.
        Dim testString As String = "Application Version: " & Application.ProductVersion
        AppVersLabel.Text = testString
        Dim keyfind As String = ""
        Try
            If MainFoV.ostype = "64" Then
                keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys.
            Else
                keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys.
            End If
        Catch ex As Exception
            MainFoV.WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("Error: " & ex.Message & Environment.NewLine & "  check the log for more info!", "CoDUO FoV Changer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

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
            Dim ask As MsgBoxResult = CType(MessageBox.Show("The program must be restarted to (fully) change your style, would you like to restart it now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information), MsgBoxResult)
            If ask = MsgBoxResult.Yes Then
                Application.Restart()
            End If
        End If
        If StyleCBox.SelectedItem.ToString = "Light" Then
            ini.WriteValue("Extras", "Style", "Light")
            MainFoV.HackyAppBranchLB.Visible = True
            MainFoV.HackyAppVersLB.Visible = True
            MainFoV.HackyGameVersLB.Visible = True
            '  MainFoV.Height = 249
            MainFoV.BackColor = MainFoV.DefaultBackColor
            Me.BackColor = DefaultBackColor
            GamePathButton.BackColor = DefaultBackColor
            CloseSettingsButton.BackColor = DefaultBackColor
            ButtonBrowseGameFiles.BackColor = DefaultBackColor
            ButtonSettingsAdvanced.BackColor = DefaultBackColor
            RestartAppButton.BackColor = DefaultBackColor
            TextBox1.BackColor = DefaultBackColor
            StyleCBox.BackColor = DefaultBackColor
            ButtonSelectGamePID.BackColor = DefaultBackColor
            SetupKeysButton.BackColor = DefaultBackColor
        ElseIf StyleCBox.SelectedItem.ToString = "Default" Then
            MainFoV.BackColor = Color.DimGray
            Me.BackColor = Color.DimGray
            GamePathButton.BackColor = Color.DarkGray
            CloseSettingsButton.BackColor = Color.DarkGray
            ButtonBrowseGameFiles.BackColor = Color.DarkGray
            ButtonSettingsAdvanced.BackColor = Color.DarkGray
            RestartAppButton.BackColor = Color.DarkGray
            TextBox1.BackColor = Color.DarkGray
            ButtonSelectGamePID.BackColor = Color.DarkGray
            StyleCBox.BackColor = Color.DarkGray
            MainFoV.FoVMenuStrip.BackColor = Color.DarkGray
            SetupKeysButton.BackColor = Color.DarkGray
            ini.WriteValue("Extras", "Style", "Default")
        End If
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles CDKeyLabel.Click
        showkey()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles ButtonSettingsAdvanced.Click
        Me.Visible = False
        AdtSettingsForm.Show()
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
        CoDPIDForm.Show()
    End Sub

    Private Sub SetupKeysButton_Click(sender As Object, e As EventArgs) Handles SetupKeysButton.Click
        FoVHotKeyForm.Show()
        Me.Hide()
    End Sub
End Class
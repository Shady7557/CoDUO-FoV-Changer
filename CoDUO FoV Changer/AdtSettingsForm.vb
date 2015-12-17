Option Strict On
Public Class AdtSettingsForm
    Public userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Public appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(appdata & "CoDUO FoV Changer\settings.ini")
    Dim firstrunini As String = ini.ReadValue("Extras", "FirstRun")
    Dim disableupdatetimerini As String = ini.ReadValue("Tweaks", "DisableUpdateTimer")
    Dim saveapplocini As String = ini.ReadValue("Extras", "SaveAppLocation")
    Dim TrackGameTime As String = ini.ReadValue("Main", "TrackGameTime")
    Dim whatami As String
    Dim conf As System.Threading.Thread
    Dim search As System.Threading.Thread
    Dim foundUO As Boolean = False
    Dim uoDirS As String
    '   Public iscontext As Boolean = False
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  MessageBox.Show(SettingsForm.StyleCBox.SelectedItem.ToString)
        Try
            If SettingsForm.StyleCBox.SelectedItem.ToString = "Dark" Then
                BackColor = Color.DimGray
                SaveRestartAppButton.BackColor = Color.DarkGray
                OpenConfigButton.BackColor = Color.DarkGray
                CancelCloseButton.BackColor = Color.DarkGray
            End If
            If firstrunini = "Yes" Then
                FirstRunCheckBox.Checked = True
            Else
                FirstRunCheckBox.Checked = False
            End If
            If disableupdatetimerini = "Yes" Then
                DisableUpdateTimerCheck.Checked = True
            Else
                DisableUpdateTimerCheck.Checked = False
            End If
            If saveapplocini.ToLower = "true" Then
                SaveWindowPosCBox.Checked = True
            Else
                SaveWindowPosCBox.Checked = False
            End If
            If TrackGameTime.ToLower = "true" Then
                GameTimeCheckbox.Checked = True
            Else
                GameTimeCheckbox.Checked = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, MainFoV.appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SaveRestartAppButton.Click
        Try
            Dim disableupdatetimer As String = ""
            Dim firstrun As String = ""
            Dim saveapploc As String = ""

            If FirstRunCheckBox.Checked = True Then
                firstrun = "Yes"
            Else
                firstrun = "No"

            End If

            If DisableUpdateTimerCheck.Checked = True Then
                disableupdatetimer = "Yes"
            Else
                disableupdatetimer = "No"
            End If

            If SaveWindowPosCBox.Checked = True Then
                saveapploc = "True"
            Else
                saveapploc = "False"
            End If

            '  ini.WriteValue("HotKeys", "Enabled", hotkeys)
            ini.WriteValue("Tweaks", "DisableUpdateTimer", disableupdatetimer)
            ini.WriteValue("Extras", "FirstRun", firstrun)
            ini.WriteValue("Extras", "SaveAppLocation", saveapploc)

            Application.Restart()
        Catch ex As Exception
            MsgBox("Failed to set config! Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles CancelCloseButton.Click
        If MainFoV.iscontext = False Then
            SettingsForm.Visible = True
            Visible = False
        Else
            Visible = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles OpenConfigButton.Click
        Process.Start(appdata & "CoDUO FoV Changer")
    End Sub
    Private Sub AccessLabel()
        Try
            If InvokeRequired Then
                Invoke(New MethodInvoker(AddressOf AccessLabel))
            Else

                If foundUO = False Then
                    TextBox1.Text = "not found"
                Else
                    TextBox1.Text = uoDirS
                    Dim yesno = MessageBox.Show(uoDirS & " Found! Does this directory look correct? If you press no, we will continue to search.", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                    If yesno = MsgBoxResult.Yes Then
                        MessageBox.Show("HOLY SHIT")
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("A critical error has occured: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If Not Debugger.IsAttached = True Then
            MessageBox.Show("debugger not attached, returning...")
            Return
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs)
        If Not Debugger.IsAttached = True Then
            MessageBox.Show("debugger not attached, returning...")
            Return
        End If
    End Sub

    Private Sub GameTimeCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles GameTimeCheckbox.CheckedChanged
        If GameTimeCheckbox.Checked = True Then
            ini.WriteValue("Main", "TrackGameTime", "True")
            '  MainFoV.trackGameTime = "True"
        Else
            ini.WriteValue("Main", "TrackGameTime", "False")
            '     MainFoV.trackGameTime = "False"
        End If
    End Sub
End Class
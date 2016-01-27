Option Strict On
Imports System.IO
Imports CurtLog

Public Class AdtSettingsForm
    Public Userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Public Appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(Appdata & "CoDUO FoV Changer\settings.ini")
    Dim firstrunini As String = ini.ReadValue("Extras", "FirstRun")
    Dim disableupdatetimerini As String = ini.ReadValue("Tweaks", "DisableUpdateTimer")
    Dim saveapplocini As String = ini.ReadValue("Extras", "SaveAppLocation")
    Dim TrackGameTime As String = ini.ReadValue("Main", "TrackGameTime")
    Dim TimeToKeepLogs As String = ini.ReadValue("Logging", "DaysToKeepLogs")
    Dim whatami As String
    Dim conf As System.Threading.Thread
    Dim search As System.Threading.Thread
    Dim foundUO As Boolean = False
    '   Public iscontext As Boolean = False
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '  MessageBox.Show(SettingsForm.StyleCBox.SelectedItem.ToString)
        Try
            If SettingsForm.StyleCBox.SelectedItem.ToString = "Default" Then
                BackColor = Color.DimGray
                SaveRestartAppButton.BackColor = Color.DarkGray
                OpenConfigButton.BackColor = Color.DarkGray
                CancelCloseButton.BackColor = Color.DarkGray
                ClearCacheButton.BackColor = Color.DarkGray
                DTKLUD.BackColor = Color.DarkGray
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
            If IsNumeric(TimeToKeepLogs) Then
                DTKLUD.Value = CInt(TimeToKeepLogs)
            Else
                ini.WriteValue("Logging", "DaysToKeepLogs", "14")
                DTKLUD.Value = 14
                Log.WriteLine("Time to Keep Logs was not numeric, default value was set to 14.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, MainFoV.appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
            MainFoV.WriteError(ex.Message, ex.StackTrace)
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
            ini.WriteValue("Logging", "DaysToKeepLogs", CStr(DTKLUD.Value))

            Application.Restart()
        Catch ex As Exception
            MsgBox("Failed to set config! Error: " & ex.Message, MsgBoxStyle.Critical)
            MainFoV.WriteError(ex.Message, ex.StackTrace)
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

    Private Sub GameTimeCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles GameTimeCheckbox.CheckedChanged
        If GameTimeCheckbox.Checked = True Then
            ini.WriteValue("Main", "TrackGameTime", "True")
            '  MainFoV.trackGameTime = "True"
        Else
            ini.WriteValue("Main", "TrackGameTime", "False")
            '     MainFoV.trackGameTime = "False"
        End If
    End Sub

    Private Sub ClearCacheButton_Click(sender As Object, e As EventArgs) Handles ClearCacheButton.Click
        Dim ask As MsgBoxResult = CType(MessageBox.Show("The program must be restarted to fully clear the image cache, are you sure you want to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information), MsgBoxResult)
        If ask = MsgBoxResult.Yes Then
            Try
                Dim di As New DirectoryInfo(appdata & "CoDUO FoV Changer\Cache")
                Dim fiArr As FileInfo() = di.GetFiles
                Dim fri As FileInfo
                For Each fri In fiArr
                    If fri.FullName.EndsWith(".cache") Then
                        File.Delete(fri.FullName)
                    End If

                Next
            Catch ex As Exception
                MainFoV.WriteError(ex.Message, ex.StackTrace)
            End Try
        End If
    End Sub
End Class
Public Class AdvSettingsForm
    Public userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Public appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(appdata & "CoDUO FoV Changer\settings.ini")
    Dim firstrunini As String = ini.ReadValue("Extras", "FirstRun")
    Dim disableupdatetimerini As String = ini.ReadValue("Tweaks", "DisableUpdateTimer")
    Dim saveapplocini As String = ini.ReadValue("Extras", "SaveAppLocation")
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
                Me.BackColor = Color.DimGray
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
            Me.Visible = False
        Else
            Me.Visible = False
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles OpenConfigButton.Click
        Process.Start(appdata & "CoDUO FoV Changer")
    End Sub
    'Private Function searchuo4() As Boolean
    '    Dim i As Integer = 0

    '    'Target Directory
    '    Dim directory = "D:\"

    '    'Searches directory and it's subdirectories for all files, which "*" stands for
    '    'Say for example you only want to search for jpeg files... then change "*" to "*.jpg"  
    '    For Each filename As String In IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories)

    '        'The next line of code gets only file extensions from searched directories and subdirectories
    '        Dim fName As String = IO.Path.GetExtension(filename)

    '        If fName = ".suo" Then

    '            'Skips to next iteration of Loop, ignoring files with .suo extension 
    '            Continue For

    '        Else
    '            If fName = ".ini" Then

    '                'Skips to next iteration of Loop, ignoring files with .ini extension
    '                Continue For

    '            Else

    '                'Your code here above count function
    '                'The below counter only displays the final count after all files have been processed
    '                If filename.Contains("CoDUOMP") Then
    '                    MessageBox.Show("success")
    '                    MessageBox.Show(filename)
    '                    uoDirS = filename
    '                    Return True
    '                End If

    '                i = i + 1
    '                ' TextBox1.Text = Convert.ToString(i)


    '            End If
    '        End If
    '    Next
    'End Function
    'Private Function searchuo3() As Boolean
    '    Dim i As Integer = 0

    '    'Target Directory
    '    Dim directory = "C:\"

    '    'Searches directory and it's subdirectories for all files, which "*" stands for
    '    'Say for example you only want to search for jpeg files... then change "*" to "*.jpg"  
    '    For Each filename As String In IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories)

    '        'The next line of code gets only file extensions from searched directories and subdirectories
    '        Dim fName As String = IO.Path.GetExtension(filename)

    '        If fName = ".suo" Then

    '            'Skips to next iteration of Loop, ignoring files with .suo extension 
    '            Continue For

    '        Else
    '            If fName = ".ini" Then

    '                'Skips to next iteration of Loop, ignoring files with .ini extension
    '                Continue For

    '            Else

    '                'Your code here above count function
    '                'The below counter only displays the final count after all files have been processed
    '                If filename.Contains("CoDUOMP") Then
    '                    MessageBox.Show("success")
    '                    MessageBox.Show(filename)
    '                    uoDirS = filename
    '                    Return True
    '                End If

    '                i = i + 1
    '                ' TextBox1.Text = Convert.ToString(i)


    '            End If
    '        End If
    '    Next
    'End Function
    'Private Function searchuo2() As Boolean
    '    Dim i As Integer = 0

    '    'Target Directory
    '    Dim directory = "C:\Program Files"

    '    'Searches directory and it's subdirectories for all files, which "*" stands for
    '    'Say for example you only want to search for jpeg files... then change "*" to "*.jpg"  
    '    For Each filename As String In IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories)

    '        'The next line of code gets only file extensions from searched directories and subdirectories
    '        Dim fName As String = IO.Path.GetExtension(filename)

    '        If fName = ".suo" Then

    '            'Skips to next iteration of Loop, ignoring files with .suo extension 
    '            Continue For

    '        Else
    '            If fName = ".ini" Then

    '                'Skips to next iteration of Loop, ignoring files with .ini extension
    '                Continue For

    '            Else

    '                'Your code here above count function
    '                'The below counter only displays the final count after all files have been processed
    '                If filename.Contains("CoDUOMP") Then
    '                    MessageBox.Show("success")
    '                    MessageBox.Show(filename)
    '                    uoDirS = filename
    '                    Return True
    '                End If

    '                i = i + 1
    '                ' TextBox1.Text = Convert.ToString(i)


    '            End If
    '        End If
    '    Next
    'End Function
    'Private Function searchuo() As Boolean
    '    Dim i As Integer = 0


    '    Dim directory = "C:\Program Files (x86)"
    '    Dim directory2 = "C:\Program Files"
    '    Dim directory3 = Nothing


    '    For Each filename As String In IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories)


    '        Dim fName As String = IO.Path.GetExtension(filename)

    '        If fName = ".a" Then

    '            'test code
    '            Continue For

    '        Else

    '            If filename.Contains("CoDUOMP") And fName.Contains(".exe") Then
    '                MessageBox.Show("success")
    '                MessageBox.Show(filename)
    '                uoDirS = filename
    '                Return True
    '            End If

    '            i = i + 1
    '            ' TextBox1.Text = Convert.ToString(i)


    '        End If
    '    Next
    'End Function
    'Private Sub Checkconnection()
    '    If searchuo() = False Then
    '        foundUO = False
    '    Else
    '        foundUO = True
    '    End If
    '    '      If isathread = True And isDev = True Then
    '    '    MsgBox("THREADED")
    '    'End If
    '    AccessLabel()
    ' End Sub
    Private Sub AccessLabel()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf AccessLabel))
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
        '  search = New Threading.Thread(AddressOf Me.Checkconnection)
        ' search.Start()
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs)
        If Not Debugger.IsAttached = True Then
            MessageBox.Show("debugger not attached, returning...")
            Return
        End If
    End Sub
End Class
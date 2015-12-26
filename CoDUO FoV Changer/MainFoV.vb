Option Strict On
Option Explicit Off
Imports System.Net
Imports System.IO
Imports System.Threading
Imports System.Text
Imports System.Security.Principal
Imports CurtLog

Public Class MainFoV
    Public userpth As String = Environment.GetEnvironmentVariable("userprofile")
    Public temp As String = Environment.GetEnvironmentVariable("temp")
    Public appdata As String = Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(appdata & "CoDUO FoV Changer\settings.ini")
    Dim iniold As New IniFile(appdata & "CoD UO FoV Changer\options.ini")
    Dim fov As String = ini.ReadValue("FoV", "FoV Value")
    Dim hotkey As String = ini.ReadValue("HotKeys", "Enabled")
    Dim manual As String = ini.ReadValue("Manual Set", "Enabled")
    Dim autorun As String = ini.ReadValue("AutoRun", "Enabled")
    Dim cmdline As String = ini.ReadValue("Extras", "cmdline")
    Dim fog As String = ini.ReadValue("Extras", "Fog")
    Dim firstrun As String = ini.ReadValue("Extras", "FirstRun")
    Dim disableupdatetimer As String = ini.ReadValue("Tweaks", "DisableUpdateTimer")
    Dim sleep As String = ini.ReadValue("Tweaks", "Sleep")
    Dim installpath As String = ini.ReadValue("Main", "InstallPath")
    Dim hotfix As String = "6.8"
    Dim hotfixini As String = ini.ReadValue("Main", "Hotfix")
    Dim progvers As String = Application.ProductVersion
    Dim progversini As String = ini.ReadValue("Main", "AppVersion")
    Public hidekey As String = ini.ReadValue("Extras", "HideKey")
    Dim startline As String = ini.ReadValue("Extras", "StartCommandLine")
    Dim fovinterval As String = ini.ReadValue("FoV", "FoVUpdateTime")
    Dim minimizetray As String = ini.ReadValue("Extras", "Minimizetotray")
    Dim lastlogname As String = ini.ReadValue("Logging", "LastLogName")
    Dim isminimal As String = ini.ReadValue("Extras", "Style")
    Dim gamevers As String = ini.ReadValue("Main", "GameVersion")
    Dim lastcboxfov As String = ini.ReadValue("Main", "LastComboBoxFoV")
    Public fovbox As String = ini.ReadValue("Main", "ComboBoxFoV")
    Dim lastwindowposX As String = ini.ReadValue("Extras", "LastWindowPosX")
    Dim lastwindowposY As String = ini.ReadValue("Extras", "LastWindowPosY")
    Public saveapplocation As String = ini.ReadValue("Extras", "SaveAppLocation")
    Dim timetokeeplogs As String = ini.ReadValue("Logging", "DaysToKeepLogs")
    Dim startuptimeaverage As String = ini.ReadValue("Tweaks", "AverageStartupTime")
    Dim iniLocation As String = appdata & "CoDUO FoV Changer\settings.ini"
    Dim oldoptions As String = appdata & "CoD UO FoV Changer\options.ini"
    Dim readvalue2 As String = ""
    Dim exename As String = "CoDUOMP"
    Dim ismohaa As String = ini.ReadValue("Main", "GameExe")
    Dim gameTime As Double = 0
    Dim curSess As Double = 0
    Dim trackGameTime As String = ini.ReadValue("Main", "TrackGameTime")
    Dim gameTimeHack As String = ini.ReadValue("Main", "GameTime")
    Public gameTimeIni As Long = 0
    Public appnamevers As String = Application.ProductName & " (" & Application.ProductVersion & ", HF" & hotfix & ")"
    Public appname As String = Application.ProductName
    Dim lastExe As String
    Public restartNeeded As Boolean = False
    Dim testingaa As String = iniold.ReadValue("FoV", "FoV Value")
    Public minimize As String
    Public updates As Boolean
    Public banned As Boolean
    Public num1fix As Integer = 0
    Public num2fix As Integer = 0
    Public hotkeyup As Integer = 0
    Public hotkeydown As Integer = 0
    Public hotkeycomboup As Integer = 0
    Public hotkeycombodown As Integer = 0
    Dim hasPlayed As Boolean = False
    Dim neg As Boolean
    Dim gamev As String = ini.ReadValue("Main", "Game")
    Dim day As Integer = Date.Now.Day
    Dim niceDay As String
    Dim month As Integer = Date.Now.Month
    Dim monthstring As String
    Dim isathread As Boolean = False
    Dim checkthread As Thread
    Dim servicethread As Thread
    Dim logthread As Thread
    Dim specsthread As Thread
    Dim getmonthT As Thread
    Dim getnDay As Thread
    Dim sendreportl As Thread
    Dim sendreportl2 As Thread
    Dim build As New StringBuilder
    Dim getcl As Thread
    Dim checkElevation As Thread
    Dim pbload As Thread
    Dim getW As Thread
    Dim rand As New Random
    Dim audioEngine As Thread
    Dim logname As String
    Dim restartneededpath As Boolean = False
    Public pid As Integer = 0
    Public newline As String = Environment.NewLine
    Public iscontext As Boolean = False
    'Dim audio As New AudioFile(temp & "\beep.mp3")
    'Dim audion As New AudioFile(temp & "\beepnegative.mp3")
    Dim thread As Thread
    Dim isDev As Boolean
    Dim errorOccured As Boolean = False
    Dim isEmailing As Boolean = False
    Dim notElevated As Boolean = True
    Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
    Dim principal As WindowsPrincipal = New WindowsPrincipal(identity)
    Public ostype As String
    Dim cacheloc As String = appdata & "CoDUO FoV Changer\Cache"
    Dim isElevated As Boolean = principal.IsInRole(WindowsBuiltInRole.Administrator)
    Private WithEvents httpclient As WebClient
    Private WithEvents httpclient2 As WebClient
    Private WithEvents httpclient3 As WebClient
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function GetAsyncKeyState(ByVal vkey As System.Windows.Forms.Keys) As Short
    End Function
    Public Sub WriteError(message As String, stacktrace As String)
        Log.WriteLine("!! ERROR " & message & "  " & stacktrace & " !!")
        errorOccured = True
    End Sub
    Private Function checkupdates() As Boolean
        Try
            checkupdates = False
            If isDev = True Then
                Return True
            End If
            Dim request2 As WebRequest = WebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zczRGeU9CZ3NZc3M")
            Dim response2 As WebResponse = request2.GetResponse()

            Dim sr As StreamReader = New StreamReader(response2.GetResponseStream())


            Dim newestversion2 As String = sr.ReadToEnd()

            If newestversion2.Contains(hotfix) Or hotfix > newestversion2 Then
                Return True
            Else
                Return False
            End If


            Return True

            '  Return False
        Catch ex As Exception
            If Not ex.Message.Contains("Could not establish trust relationship for the SSL/TLS secure channel") Then
                '  MsgBox("Unable to Fetch Updates! " & ex.Message, MsgBoxStyle.Critical)
                WriteError(ex.Message, ex.StackTrace)
            Else
                MsgBox("Unable to Fetch Updates! " & ex.Message & newline & "This error is likely caused by your time being out of sync. (System time)", MsgBoxStyle.Critical)
                WriteError(ex.Message, ex.StackTrace)
            End If
            Return True
            '   Application.Exit()
        End Try

        Return False
    End Function
    Private Sub getChangelog()

        Try
            Dim myExe As String = temp & "\changelog.tmp.txt"
            If File.Exists(myExe) Then
                Dim sr4 As StreamReader = New StreamReader(myExe)
                Dim cache As String = sr4.ReadToEnd
                sr4.Close()
                If Not cache Is Nothing And Not cache = "" Then
                    Process.Start(myExe)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox("Unable to fetch changelog! " & newline & ex.Message, MsgBoxStyle.Critical)
            WriteError(ex.Message, ex.StackTrace)
        End Try


        Try
            Dim request3 As WebRequest = WebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76za3Y3dW9KYU5kQlE")
            Dim response3 As WebResponse = request3.GetResponse()

            Dim sr3 As StreamReader = New StreamReader(response3.GetResponseStream())

            Dim changelog As String = sr3.ReadToEnd()

            '  Dim rn As New Random

            Dim myExe As String = temp & "\changelog.tmp.txt"
            If File.Exists(myExe) Then
                Dim sr4 As StreamReader = New StreamReader(myExe)
                Dim cache As String = sr4.ReadToEnd
                sr4.Close()
                If Not cache Is Nothing And Not cache = "" Then
                    Process.Start(myExe)
                    Exit Sub
                End If
                If changelog = cache Or cache = changelog Then
                    '    Process.Start(myExe)
                Else
                    File.Delete(myExe)
                    myExe = temp & "\changelog.new.tmp.txt"
                    File.WriteAllText(myExe, changelog)
                    Process.Start(myExe)
                    Log.WriteLine("Writing Changelog to location: " & myExe)
                End If
            End If
            If Not File.Exists(myExe) Then
                File.WriteAllText(myExe, changelog)
                Process.Start(myExe)
                Log.WriteLine("Writing Changelog to location: " & myExe)
            End If


        Catch ex As Exception
            MsgBox("Unable to fetch changelog! " & newline & ex.Message, MsgBoxStyle.Critical)
            WriteError(ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub disableUI()
        '   Button3.Enabled = False
        '   debugb.Enabled = False
        '   Button11.Enabled = False
        UpdateButton.Visible = True
        '  TextBox1.ReadOnly = True
        '  Timer1.Stop()
        '  Timer4.Stop()
    End Sub
    Private Sub Checkconnection()
        If checkupdates() = False Then
            updates = False
        Else
            updates = True
        End If
        If isathread = True And isDev = True Then
            '    MsgBox("THREADED")
        End If
        AccessLabel()
    End Sub
    Private Sub AccessLabel()
        Try
            If InvokeRequired Then
                Invoke(New MethodInvoker(AddressOf AccessLabel))
            Else

                If updates = True Then
                    CheckUpdatesLabel.Text = ("No Updates found. Click to check again.")
                Else
                    CheckUpdatesLabel.Text = ("Update Available!")
                    CheckUpdatesLabel.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    disableUI()
                End If
                If isDev = True Then
                    CheckUpdatesLabel.Text = "Developer (debug) mode is active. Updates will not be searched for."
                    UpdateButton.Visible = True
                End If
            End If
        Catch ex As Exception
            MsgBox("A critical error has occured: " & ex.Message, MsgBoxStyle.Critical)
            WriteError(ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub CreateLogFolder()

    End Sub
    Public Enum convTo
        KB = 1
        MB = 2
        GB = 3
        TB = 4
    End Enum

    Public Function BytesTO(lBytes As Long, convertto As convTo) As Double
        BytesTO = lBytes / (1024 ^ convertto)
    End Function
    Private Sub getWav()
        If Not File.Exists(temp & "\beep.mp3") Then
            httpclient = New WebClient
            httpclient2 = New WebClient
            Dim sourceURL = "https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zM0t4X2dvZDFiQm8"
            Dim sourceURL2 = "https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zbUdJWEZWdGZ5Y1k"
            Dim filedir = temp & "\beep.mp3"
            Dim filedir2 = temp & "\beepnegative.mp3"
            Try

                httpclient.DownloadFileAsync(New Uri(sourceURL), (filedir))
                httpclient2.DownloadFileAsync(New Uri(sourceURL2), (filedir2))

            Catch ex As Exception
                WriteError(ex.Message, ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub Cache(sourceurl As String, filename As String)
        If Not File.Exists(cacheloc & "\" & filename) Then
            httpclient = New WebClient
            Try
                httpclient.DownloadFileAsync(New Uri(sourceurl), cacheloc & "\" & filename)
            Catch ex As Exception
                WriteError(ex.Message, ex.StackTrace)
                MessageBox.Show("An error has occured while attempting to download a picture file: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Function corruptCheck(filename As String, bytes As Long) As Long
        Dim infoReader As System.IO.FileInfo =
My.Computer.FileSystem.GetFileInfo(filename)
        Return infoReader.Length
    End Function
    ' Private Function getImgur2()
    'this code isn't yet used, got too confused at 3:40am, will probably work on it later, cause program hanging is annoying
    '  Try
    '          getImgur2 = "False"
    '    Dim request2 As System.Net.WebRequest = System.Net.HttpWebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zczRGeU9CZ3NZc3M")
    '     Dim response2 As System.Net.WebResponse = request2.GetResponse()

    '  Try
    '             Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")

    'Return "True"
    ' Catch ex As Exception
    'Return "False"
    'End Try



    '  Return False
    ' Catch ex As Exception
    'If Not ex.Message.Contains("Could not establish trust relationship for the SSL/TLS secure channel") Then
    '           MsgBox("Unable to download an image! " & ex.Message, MsgBoxStyle.Critical)
    'Else
    '           MsgBox("Unable to download an image! " & ex.Message & newline & "This error is likely caused by your time being out of sync. (System time)", MsgBoxStyle.Critical)
    'End If
    '   Application.Exit()
    'End Try

    'Return "False"
    'End Function

    '  Private Sub getImgur()
    '      If getImgur2() Is "False" Then
    '          updates = False
    '      Else
    '          updates = True
    '      End If
    '      AccessLabel()
    '  End Sub

    Private Sub ChangeFoV()
        Try
            Dim TimerStart As Date = Now
            If Not pid = 0 Then
                Try
                    Dim MyPID As Process = Process.GetProcessById(pid)
                Catch ex As Exception
                    If ex.Message.Contains("Process with an Id") Then
                        pid = 0
                    Else
                        WriteError(ex.Message, ex.StackTrace)

                    End If
                    Return
                End Try
            End If

            If CoD1CheckBox.Checked = False Then
                If pid = 0 Then
                    WriteFloat(exename, &H3052F7C8, CSng(FoVNumeric.Text))
                Else
                    WriteFloatpid(pid, &H3052F7C8, CSng(FoVNumeric.Text))
                End If
            Else
                WriteFloat("CoDMP", &H3029CA28, CSng(FoVNumeric.Text))
            End If
            If pid = 0 Then
                Dim MyP As Process() = Process.GetProcessesByName(exename)
                If MyP.Length = 0 Then
                    StatusLabel.Text = ("Status: not found or failed to write to memory!")
                    If isminimal = "Default" Then
                        StatusLabel.ForeColor = Color.DarkRed
                    Else
                        StatusLabel.ForeColor = Color.Red
                    End If
                    StartGameButton.Enabled = True


                    Exit Sub
                Else
                    StatusLabel.Text = ("Status: UO found and wrote to memory!")
                    If isminimal = "Default" Then
                        StatusLabel.ForeColor = Color.DarkGreen
                    Else
                        StatusLabel.ForeColor = Color.Green
                    End If
                    StartGameButton.Enabled = False
                End If
            End If
            If StatusLabel.Text.Contains("not found or fa") Then
                CurSessionGT.Visible = False
                CurSessionGT.Text = "Current Session: No time played"
            End If
            GetTimeWarning(TimerStart, 70, "ChangeFoV()")
        Catch ex As Exception
            FoVTimer.Stop()
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("An error has occurred: " & ex.Message & " please refer to the log file for more info.", appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)

            '


        End Try
    End Sub
    Private Sub GetRegPath()
        Try
            If ostype = "64" Then
                readvalue2 = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Version", 1.51)) 'If the registry key is not found, it may report an error.
            ElseIf ostype = "86" Then
                readvalue2 = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Version", 1.51)) 'If the registry key is not found, it may report an error.
            End If


            ' If Not readvalue2 Then
            '   readvalue2 = ""
            'End If
            If Not gamevers = readvalue2 Then
                ini.WriteValue("Main", "GameVersion", readvalue2)
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
        End Try
    End Sub
    Private Sub GetIniValues()
        If Not ismohaa = "" And Not ismohaa Is Nothing Then
            exename = ismohaa
        ElseIf ismohaa = "" Or ismohaa Is Nothing Then
            ini.WriteValue("Main", "GameExe", "CoDUOMP")
            exename = "CoDUOMP"
        End If

        If ismohaa.Contains(".exe") Then
            ismohaa = ismohaa.Replace(".exe", "")
            exename = ismohaa
            ini.WriteValue("Main", "GameExe", exename)
        End If
        If exename.Contains(".exe") Then
            exename = exename.Replace(".exe", "")
            ini.WriteValue("Main", "GameExe", exename)
        End If
        If isminimal = "" Or isminimal Is Nothing Then
            ini.WriteValue("Extras", "Style", "Default")
            isminimal = "Default"
        End If
        If saveapplocation = "" Or saveapplocation Is Nothing Then
            ini.WriteValue("Extras", "SaveAppLocation", "True")
            saveapplocation = "True"
        End If
        If Not gameTimeHack = "" And Not gameTimeHack Is Nothing Then
            gameTimeIni = CLng(gameTimeHack)
        End If
        If Not gameTimeIni = Nothing Then
            gameTime = gameTimeIni
        End If
        If isminimal = "Dark" Then
            ini.WriteValue("Extras", "Style", "Default")
            isminimal = "Default"
        End If
        If fog = "" Then
            ini.WriteValue("Extras", "Fog", "Enabled")
        End If
        If timetokeeplogs = "" Or Not IsNumeric(timetokeeplogs) Then
            ini.WriteValue("Logging", "DaysToKeepLogs", "14")
        End If
        If Not fovinterval = "" Then 'Checks the .ini for the user specified timer interval, if one exists
            FoVTimer.Interval = CInt(fovinterval)
        Else
            FoVTimer.Interval = 1500
        End If
        If disableupdatetimer = "" Then
            ini.WriteValue("Tweaks", "DisableUpdateTimer", "No")
        ElseIf disableupdatetimer = "Yes" Then
            UpdateCheckTimer.Stop()
        ElseIf disableupdatetimer = "No" Or disableupdatetimer = "" Or disableupdatetimer Is Nothing Then
            UpdateCheckTimer.Start()
        End If
        If Not ini.ReadValue("Extras", "HotKeyUp") = "" And Not ini.ReadValue("Extras", "HotKeyUp") = Nothing Then
            hotkeyup = CInt(ini.ReadValue("Extras", "HotKeyUp"))
        End If
        If Not ini.ReadValue("Extras", "HotKeyDown") = "" And Not ini.ReadValue("Extras", "HotKeyDown") = Nothing Then
            hotkeydown = CInt(ini.ReadValue("Extras", "HotKeyDown"))
        End If
        If Not ini.ReadValue("Extras", "HotKeyUpCombo") = "" And Not ini.ReadValue("Extras", "HotKeyUpCombo") = Nothing Then
            hotkeycomboup = CInt(ini.ReadValue("Extras", "HotKeyUpCombo"))
        End If
        If Not ini.ReadValue("Extras", "HotKeyDownCombo") = "" And Not ini.ReadValue("Extras", "HotKeyDownCombo") = Nothing Then
            hotkeycombodown = CInt(ini.ReadValue("Extras", "HotKeyDownCombo"))
        End If
    End Sub
    Private Sub OldLogFiles()

        Try
            Dim di As New DirectoryInfo(appdata & "CoDUO FoV Changer\Logs")
            Dim fiArr As FileInfo() = di.GetFiles
            Dim fri As FileInfo
            Dim DaysToKeepLogs As Long = 14
            If Not timetokeeplogs = "" And Not timetokeeplogs = "14" Then
                If IsNumeric(timetokeeplogs) Then
                    DaysToKeepLogs = CLng(timetokeeplogs)
                Else
                    ini.WriteValue("Logging", "DaysToKeepLogs", "14")
                    Log.WriteLine("Time to Keep Logs was not numeric, default value was set to 14.")
                End If
            End If
            For Each fri In fiArr
                If Not fri.FullName = logname Then
                    If fri.FullName.EndsWith(".log") Then
                        Dim dayDiff As Long = DateDiff(DateInterval.Day, fri.LastWriteTime, Now)

                        If dayDiff >= DaysToKeepLogs Then
                            Log.WriteLine("Log: " & fri.ToString & " is " & CStr(dayDiff) & " days old, maximum is 14. - Deleting log.")
                            IO.File.Delete(fri.FullName)
                        End If

                    End If
                End If

            Next
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim TTStart As Date = Now
        CheckForIllegalCrossThreadCalls = True

        If Debugger.IsAttached = True Then
            isDev = True
        Else
            isDev = False
        End If

        If Environment.Is64BitOperatingSystem = True Then
            ostype = "64"
        Else
            ostype = "86"
        End If





        Try 'I'm not really sure this try is even needed, doesn't seem like something that would ever have a fault
            If isElevated = False Then
                MessageBox.Show("Program is not being ran with Administrative privileges, please restart this program with sufficient access.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
                Exit Sub
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("Fatal error occured: " & ex.Message & " this will prevent the program from starting.")
            Application.Exit()
            Exit Sub
        End Try

        Dim inithread As New Thread(AddressOf GetIniValues)
        inithread.Priority = ThreadPriority.AboveNormal
        inithread.IsBackground = True
        inithread.Start()

        If isDev = True Then
            '     
        End If

        Dim nolog As Boolean = False
        For Each arguement As String In My.Application.CommandLineArgs
            If arguement = "-nolog" Then
                nolog = True

            End If

        Next


        If saveapplocation.ToLower = "true" Then
            If Not lastwindowposX = "" And Not lastwindowposX Is Nothing And Not lastwindowposY = "" And Not lastwindowposY Is Nothing Then
                Location = New Point(CInt(lastwindowposX), CInt(lastwindowposY))
            End If
        End If


        Try
            If Not nolog = True Then
                Log.EnableBuffer = False
                logname = appdata & "CoDUO FoV Changer\Logs\CFC_" & DateAndTime.Now.Month & "_" & DateAndTime.Now.Day & "_" & DateAndTime.Now.Hour & "_" & DateAndTime.Now.Minute & "_" & DateAndTime.Now.Second & "_" & DateAndTime.Now.Year & "_" & DateAndTime.Now.Millisecond & ".log"
                Log.AttachLogFile(logname, False, 1)
                '     Log.WriteLine("This is a dev-mode log, it will not be deleted upon next startup.")
                ini.WriteValue("Logging", "LastLogName", logname)
            End If

        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("A fatal error has occured, this will prevent the program from starting: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
            Exit Sub
        End Try




        Try
            If Directory.Exists(appdata & "CoDUO FoV Changer") And Directory.Exists(appdata & "CoDUO FoV Changer\Logs") Then
                'placeholder
            Else
                Directory.CreateDirectory(appdata & "CoDUO FoV Changer")
                Directory.CreateDirectory(appdata & "CoDUO FoV Changer\Logs")
                'Log.WriteLine("Created Log Folder.")
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("A fatal error has occured, this will prevent the program from starting: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
            Exit Sub
        End Try

        If Not cmdline = "" And Not cmdline Is Nothing Then
            LaunchParametersTB.Text = cmdline
        End If



        If trackGameTime = "True" Then
            GameTimeLabel.Visible = True
        ElseIf trackGameTime = "" Or trackGameTime Is Nothing Then
            trackGameTime = "True"
            GameTimeLabel.Visible = True
            ini.WriteValue("Main", "TrackGameTime", "True")
        Else
            GameTracker.Stop()
            GameTimeLabel.Visible = False
        End If



        If isminimal = "Default" Or isminimal = "" Or isminimal Is Nothing Then
            BackColor = Color.DimGray
            FoVNumeric.BackColor = Color.DarkGray
            LaunchParametersTB.BackColor = Color.DarkGray
            UpdateButton.BackColor = Color.DarkGray
            StartGameButton.BackColor = Color.DarkGray
            FoVMenuStrip.BackColor = Color.DarkGray
            If StatusLabel.ForeColor = Color.Red Then
                StatusLabel.ForeColor = Color.DarkRed
            End If
            isminimal = "Default"
        End If

        If month = 12 And Date.Now.Day = 25 Then
            MessageBox.Show("wow christmas & stuff", "pls snow", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If month = 10 And Date.Now.Day = 31 Then
            MessageBox.Show("ooooh spooooooky ghosts and candy", "BOO", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If gamev = "UO" Or gamev Is Nothing Then
            CoD1CheckBox.Checked = False
        ElseIf gamev = "CoD1" Then
            DvarsCheckBox.Text = "Unlock All Dvars (UO only!)"
            DvarsCheckBox.Enabled = False
            DvarsCheckBox.Checked = False
            CoD1CheckBox.Checked = True
        End If

        ''START REFINED INSTALL PATH CODE

        Dim GameRegistry As String = ""
        Try
            If ostype = "64" Then
                My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive")
                GameRegistry = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "InstallPath", "no path found"))
            ElseIf ostype = "86" Then
                My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Activision\Call of Duty United Offensive")
                GameRegistry = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "InstallPath", "no path found"))
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
        End Try

        Dim DoContinue As Boolean = False

        Try

            If GameRegistry.Contains("Call of Duty") Or GameRegistry.Contains("CoD") Then
                If File.Exists(GameRegistry & "\CoDUOMP.exe") Or File.Exists(GameRegistry & "\mohaa.exe") Or File.Exists(GameRegistry & "\CoDMP.exe") Then
                    If installpath = "" Or installpath Is Nothing Then
                        ini.WriteValue("Extras", "FirstRun", "No")
                        ini.WriteValue("Main", "InstallPath", GameRegistry)
                        MessageBox.Show("Auto Detected Install Path as: " & GameRegistry, appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    DoContinue = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("!! ERROR !!" & ex.Message, appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
            WriteError(ex.Message, ex.StackTrace)
        End Try

        Try
            If installpath = "" Or installpath Is Nothing Then 'potential speed improvement if we're not checking the registry each time it starts the program, may accidentally cause errors
                If GameRegistry = "" Or GameRegistry = "no path found" Or GameRegistry Is Nothing And DoContinue = True Then
                    MessageBox.Show("Hello! Since this seems to be your first time using this program, you need to set your install path, because we were unable to detect it automatically!", appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dim SelectedPath As String = ""
                    FolderBrowserDialog1.ShowDialog()
                    SelectedPath = FolderBrowserDialog1.SelectedPath
                    ini.WriteValue("Main", "InstallPath", SelectedPath)
                    ini.WriteValue("Extras", "FirstRun", "No")
                    installpath = SelectedPath
                    firstrun = "No"
                    MessageBox.Show("Install Path set to: " & SelectedPath & ", if you wish to change this later, go to: Tools > Settings > Change Game Path", appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("!!ERROR!! " & ex.Message, appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
            WriteError(ex.Message, ex.StackTrace)
        End Try

        ''END REFINED INSTALL PATH CODE


        checkthread = New Thread(AddressOf Checkconnection)
        checkthread.Priority = ThreadPriority.Highest
        checkthread.IsBackground = True
        checkthread.Start()


        Try
            Dim splitStrr() As String
            If fovbox.Contains(",") Then
                splitStrr = fovbox.Split(CType(",", Char()))
                For Each word As String In splitStrr
                    If Not word = "" And IsNumeric(word) Then
                        If Not CInt(word) >= 121 Then
                            If Not HackyFoVComboBox.Items.Count + 1 >= 13 Then
                                HackyFoVComboBox.Items.Add(word)
                            Else
                                Log.WriteLine("FoV values in hacky fov combo box exceed 12. Not adding item: " & word)
                            End If
                        Else
                            Log.WriteLine(word & " is higher than 120 fov (max), will not add to combobox")
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("Error: " & ex.Message & newline & "  check log for more info!", appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        If Not Directory.Exists(cacheloc) Then
            Directory.CreateDirectory(cacheloc)
        End If

        Dim rn As New Random

        Try
            If gamev = "UO" Or gamev Is Nothing Or gamev = "" Then
                If File.Exists(cacheloc & "\cache5.cache") Then
                    Dim iscorrupt As Double = corruptCheck(cacheloc & "\cache5.cache", 11846)
                    If iscorrupt <= 11845 Or iscorrupt >= 11847 Then
                        CoDPictureBox.Image = My.Resources.Loading
                        CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                        File.Delete(cacheloc & "\cache5.cache")
                        Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                    Else
                        CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache5.cache")
                    End If

                Else
                    CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                    Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                End If
            Else
                If File.Exists(cacheloc & "\cache6.cache") Then
                    Dim iscorrupt As Double = corruptCheck(cacheloc & "\cache6.cache", 8606)
                    If iscorrupt <= 8605 Or iscorrupt >= 8607 Then
                        CoDPictureBox.Image = My.Resources.Loading
                        CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                        File.Delete(cacheloc & "\cache6.cache")
                        Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                    Else
                        CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache6.cache")
                    End If

                Else
                    CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                    Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                End If
            End If
            Dim finalImg As Bitmap
            finalImg = New Bitmap(CoDPictureBox.Image, CoDPictureBox.Width, CoDPictureBox.Height)
            CoDPictureBox.SizeMode = PictureBoxSizeMode.CenterImage
            CoDPictureBox.Image = finalImg
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("An error has occured while attempting to cache files, this could be a result of no write permissions or not having an internet connection: " & ex.Message & newline & " this should not prevent the program from otherwise running normally.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            CoDPictureBox.Image = My.Resources.Loading

        End Try

        If File.Exists(temp & "\CoDUO FoV Changer Updater.exe") Then
            File.Delete(temp & "\CoDUO FoV Changer Updater.exe")
        End If

        If restartNeeded = True Then
            MessageBox.Show("Your .ini file has been moved, it is recommended your restart the program, and as such, most of the program has been disabled until you do so.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            restartNeeded = False
            FoVNumeric.ReadOnly = True
            FoVTimer.Enabled = False
            StartGameButton.Enabled = False
        End If



        Try
            Dim regpaththread As Thread
            regpaththread = New Thread(AddressOf GetRegPath)
            regpaththread.Priority = ThreadPriority.AboveNormal
            regpaththread.IsBackground = True
            regpaththread.Start()

        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)

            '


        End Try

        'safety code
        If Not fov = "80" Or Not fov = "" Then
            If IsNumeric(fov) Then FoVNumeric.Value = CInt(fov)
            If Not IsNumeric(fov) Then FoVNumeric.Value = 80
        Else
                FoVNumeric.Value = My.Settings.FoVFix
        End If

        If minimizetray = "" Then
            ini.WriteValue("Extras", "Minimizetotray", "Yes")
            MinimizeCheckBox.Checked = True
        End If

        If minimizetray = "Yes" Then
            MinimizeCheckBox.Checked = True
        ElseIf minimizetray = "No" Then
            MinimizeCheckBox.Checked = False
        End If




        If fog = "Enabled" Then 'Checks if fog is enabled in the .ini
            FogCheckBox.Checked = True
            WriteInteger(exename, &H98861C, 1)
            FogTimer.Stop()
            Log.WriteLine("Stopping Fog Timer, turning fog on, checking Fog CheckBox.")
        ElseIf fog = "Disabled" Then
            FogCheckBox.Checked = False
            WriteInteger(exename, &H98861C, 0)
            FogTimer.Start()
            Log.WriteLine("Starting Fog Timer, turning fog off, unchecking Fog CheckBox")
        End If

        Try
            If startline.Contains("-launch") Then
                ini.WriteValue("Extras", "StartCommandLine", startline.Replace("-launch", ""))
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)

        End Try



        DvarsCheckBox.Visible = False
        For Each arguement As String In My.Application.CommandLineArgs
            Dim splitStr() As String
            If arguement = ("-unlock") Then
                DvarsCheckBox.Visible = True
            End If
            If arguement.Contains("-fov=") Then
                splitStr = arguement.Split(CType("=", Char()))
                If splitStr(1) = Nothing Or splitStr(1) = "" Then
                    Return
                End If

                If CInt(splitStr(1)) >= 121 Then
                    splitStr(1) = CStr(120)
                End If
                If CInt(splitStr(1)) <= 79 Then
                    splitStr(1) = CStr(80)
                End If
                If splitStr(1).StartsWith(CStr(1)) Or splitStr(1).StartsWith(CStr(2)) Or splitStr(1).StartsWith(CStr(3)) Or splitStr(1).StartsWith(CStr(4)) Or splitStr(1).StartsWith(CStr(5)) Or splitStr(1).StartsWith(CStr(6)) Or splitStr(1).StartsWith(CStr(7)) Or splitStr(1).StartsWith(CStr(8)) Or splitStr(1).StartsWith(CStr(9)) Then
                    FoVNumeric.Text = splitStr(1)
                    Log.WriteLine("Launched fov changer with -fov=" & splitStr(1))
                End If
            End If
            If arguement.Contains("-fog=") Then
                splitStr = arguement.Split(CType("=", Char()))
                If splitStr(1) = Nothing Or splitStr(1) = "" Then
                    '
                End If

                If CInt(splitStr(1)) < 0 Then
                    splitStr(1) = CStr(0)
                End If
                If CInt(splitStr(1)) >= 2 Then
                    splitStr(1) = CStr(1)
                End If
                If splitStr(1).StartsWith("0") Or splitStr(1).StartsWith("1") Then
                    If splitStr(1) = "1" Then
                        FogCheckBox.Checked = True
                        FogTimer.Stop()
                        ini.WriteValue("Extras", "Fog", "Enabled")
                    ElseIf splitStr(1) = "0" Then
                        ini.WriteValue("Extras", "Fog", "Disabled")
                        FogCheckBox.Checked = False
                        FogTimer.Start()
                    End If
                    Log.WriteLine("Launched fov changer with -fog=" & splitStr(1))
                End If
            End If

            If arguement = ("-launch") Then 'Automatically launches the game, probably useful if you're not running a command line and don't know the autorun .ini value exists, one of the codes can be removed.

                Try
                    If startline.Contains("-launch") Then
                        ini.WriteValue("Extras", "StartCommandLine", startline.Replace("-launch", ""))
                    End If
                    Dim StartGameThread As New Thread(AddressOf StartGame)
                    StartGameThread.IsBackground = True
                    StartGameThread.Start()



                Catch ex As Exception
                    WriteError(ex.Message, ex.StackTrace)
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)

                End Try
                '  Button3.Enabled = False
            End If

            If arguement = ("-forceupdate") Then 'Self explantory
                Try
                    Visible = False
                    Me.Hide()
                    Dim updatethread As Thread

                    updatethread = New Thread(AddressOf CreateUpdaterApp)
                    updatethread.Priority = ThreadPriority.AboveNormal
                    updatethread.IsBackground = True
                    updatethread.Start()
                    Log.WriteLine("Restarting.")
                    Thread.Sleep(50)
                    Application.Exit()
                    Return
                Catch ex As Exception
                    WriteError(ex.Message, ex.StackTrace)

                    '


                End Try
            End If




            If arguement.Contains("-unlock=") Then
                splitStr = arguement.Split(CType("=", Char()))
                If splitStr(1) = Nothing Or splitStr(1) = "" Then
                    ' Return
                End If

                If CInt(splitStr(1)) >= 1 Then
                    splitStr(1) = "1"
                    DvarsCheckBox.Visible = True
                    DvarsCheckBox.Checked = True
                    DvarUnlockerTimer.Start()
                End If
                If CInt(splitStr(1)) <= 0 Then
                    splitStr(1) = "0"
                    DvarsCheckBox.Visible = True
                    DvarUnlockerTimer.Stop()
                End If
                Log.WriteLine("Started fov changer with -unlock=" & splitStr(1))
            End If


        Next
        If DvarsCheckBox.Visible = False Then
            DvarsCheckBox.Checked = False
        End If



        Try
            If Not gamevers Is Nothing And Not gamevers = "" Then
                HackyGameVersLB.Text = ("CoD:UO Version: " & gamevers)
                readvalue2 = gamevers 'lazy code
            Else
                HackyGameVersLB.Text = ("CoD:UO Version: " & readvalue2) 'Sets the label to read the games version via registry key above.
            End If
            HackyAppBranchLB.Text = "Application Branch: " & "3.0" 'Sets the label's text to contain the application branch.
            Dim testString As String = "Application Version: " & Application.ProductVersion
            HackyAppVersLB.Text = testString

        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("An error has occured while attempting to read registry and set program labels: " & ex.Message & " this should not prevent the program from functioning normally.")
        End Try



        'Logs stuff
        Log.WriteLine("User is using version: " & ProductVersion & " Hotfix Version: " & hotfix)
        Log.WriteLine("Log folder is: " & appdata & "CoDUO FoV Changer\Logs")


        If Not readvalue2 Is Nothing And Not readvalue2 = "" Then
            If CInt(readvalue2) < 1.51 Then
                FoVTimer.Stop()
                FoVNumeric.ReadOnly = True
                MsgBox("Your Call of Duty Version is not the correct version for this program, the FoV Changer only works on 1.51, if you're sure you have 1.51, then check UO registry, and set it through that.", MsgBoxStyle.Information)
            End If
        End If


        If hidekey = "" Or hidekey = Nothing Then 'Checks if the user want's their CD-key not to be shown on the label.
            '     Button15.Text = ("Show CD-Key")
            'showkey()
            ini.WriteValue("Extras", "HideKey", "Yes")
            hidekey = "Yes"
        End If

        Try
            If Not lastlogname = "" And Not isDev = True Then
                File.Delete(lastlogname)
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
        End Try

        StartGameButton.Select()

        Dim versmod As String = Application.ProductVersion.Substring(0, 3)
        Dim showmsgbox As Boolean = True
        Dim pv1 As Boolean = False

        If Not progversini = "" Then
            If progversini < progvers Then
                If progversini.Substring(0, 3) < progvers.Substring(0, 3) Then
                    showmsgbox = False
                    pv1 = True
                    MessageBox.Show(Application.ProductName & " has been updated from " & progversini & " to " & progvers & " (" & "HF" & hotfix & "), be sure to check the changelog to see what's different!", appname, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    progversini = progvers
                    ini.WriteValue("Main", "AppVersion", progvers)
                    '  Return
                End If
            End If
        End If

        If progversini = "" And Not pv1 = True Then
            progversini = progvers
            ini.WriteValue("Main", "AppVersion", progvers)
            '  If progvers = "" Then
            '   progvers = "Unknown"
            showmsgbox = False
            '   MessageBox.Show(Application.ProductName & " has been updated from Unknown to " & progvers & " (" & "HF" & hotfix & "), be sure to check the changelog to see what's different!", appname, MessageBoxButtons.OK, MessageBoxIcon.Information)
            progversini = progvers
            '     Return
        End If
        ' End If



        If hotfixini = "" Or hotfixini < hotfix Then
            ini.WriteValue("Main", "Hotfix", hotfix)
            If showmsgbox = False Then
                hotfixini = hotfix
                Return
            End If
            If hotfixini = "" Then
                MessageBox.Show(Application.ProductName & " has been updated to HF" & hotfix & ", be sure to check the changelog to see what's different!", appname, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(Application.ProductName & " has been updated from HF" & hotfixini & " to HF" & hotfix & ", be sure to check the changelog to see what's different!", appname, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            hotfixini = hotfix
        End If

        GetGameTimeLabel()

        If HackyFoVComboBox.Items.Count >= 1 Then
            HackyFoVComboBox.SelectedIndex = 0
        End If

        If Not HackyFoVComboBox.Items.Count <= 0 Then
            For Each itemstring In HackyFoVComboBox.Items
                If itemstring.ToString.Contains(lastcboxfov) And Not lastcboxfov = "" And Not lastcboxfov Is Nothing Then
                    HackyFoVComboBox.SelectedItem = lastcboxfov
                End If
            Next
        End If

        Dim OldLogsThread As New Thread(AddressOf OldLogFiles)
        OldLogsThread.IsBackground = True
        OldLogsThread.Start()

        GetTimeWarning(TTStart, 120, "MyBase.Load")

        '    MessageBox.Show(TimeSpent.TotalMilliseconds)
        '   If nolog = True Then MessageBox.Show(TimeSpent.TotalSeconds)


    End Sub
    Private Sub GetTimeWarning(StartTime As Date, MaxMS As Long, Name As String)
        Dim TimeSpent As TimeSpan = Now.Subtract(StartTime)
        If TimeSpent.TotalMilliseconds >= MaxMS Then
            Log.WriteLine(Name & " took: " & TimeSpent.TotalMilliseconds & " (this is too long!)")
        End If
    End Sub

    Private Sub Form1_Close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Dim TTStart As Date = Now

        Log.WriteLine("Program is closing.")
        Try
            WriteFloat(exename, &H3052F7C8, 80)
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)

        End Try

        If Not LaunchParametersTB.Text = "" And Not LaunchParametersTB.Text Is Nothing Then

            ini.WriteValue("Extras", "cmdline", LaunchParametersTB.Text)
        End If

        Try
            'ini.WriteValue("Logging", "LastLogName", Module1.location)
        Catch ex As Exception
            If isDev = True Then
                MsgBox(ex.Message)
            End If
            WriteError(ex.Message, ex.StackTrace)
        End Try

        If File.Exists(oldoptions) Then
            Directory.Delete(appdata & "CoD UO FoV Changer")
            Log.WriteLine("Deleted old options folder")
        End If

        ini.WriteValue("FoV", "FoV Value", FoVNumeric.Text)


        '     MessageBox.Show("what")
        If errorOccured Then
            If Not restartneededpath = True Then
                Log.WriteLine("An error has occurred -- not deleting log file!")
                ini.WriteValue("Logging", "LastLogName", "")
            End If

        End If


        Try
            If isEmailing = False Then
                '   Dim copy = Module1.location & "- copy"

                '  System.IO.File.Delete(copy)
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
        End Try

        If File.Exists(lastlogname & " - copy 1") Then
            File.Delete(lastlogname & " - copy 1")
        End If

        If File.Exists(temp & "\changelog.tmp.txt") Then
            File.Delete(temp & "\changelog.tmp.txt")
            Log.WriteLine("Deleted temporary changelog file")
        End If
        'these are in 2 seperate if statements because they can both exist at once, thus, a elseif would not work.
        If File.Exists(temp & "\changelog.new.tmp.txt") Then
            File.Delete(temp & "\changelog.new.tmp.txt")
            Log.WriteLine("Deleted temporary changelog file")
        End If

        If saveapplocation.ToLower = "true" And Not lastwindowposX = CStr(Location.X) And Not lastwindowposY = CStr(Location.Y) Then
            ini.WriteValue("Extras", "LastWindowPosX", CStr(Location.X))
            ini.WriteValue("Extras", "LastWindowPosY", CStr(Location.Y))
            Log.WriteLine("Saved App Location: " & CStr(Location.X) & " " & CStr(Location.Y))
        End If


        GetTimeWarning(TTStart, 70, "MyBase.FormClosing")

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles FoVTimer.Tick
        ChangeFoV()
    End Sub
    Private Sub StartGame()
        Dim startInfo = New ProcessStartInfo()
        startInfo.WorkingDirectory = installpath
        If Not LaunchParametersTB.Text = "" And Not LaunchParametersTB.Text Is Nothing Then
            startInfo.Arguments = LaunchParametersTB.Text & " +set r_ignorehwgamma 1" & " +set vid_xpos 0 +set vid_ypos 0 +set com_hunkmegs 128 +set win_allowalttab 1"
        End If
        If Not exename = "" And Not exename Is Nothing Then
            startInfo.FileName = installpath & "\" & exename & ".exe"
        Else
            MessageBox.Show("Could not find game's .exe, looked for: " & installpath & "\" & exename & ".exe", appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log.WriteLine("Could not find game's .exe, looked for: " & installpath & "\" & exename & ".exe")
            Return
        End If
        Process.Start(startInfo)
        Log.WriteLine("Started game: " & exename & ".exe, with args: " & startInfo.Arguments)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles StartGameButton.Click
        If CoD1CheckBox.Checked = True Then
            exename = "CoDMP"
        End If


        Try
            If Not CoD1CheckBox.Checked = True Then
                exename = ismohaa
            End If
            Dim StartGameThread As New Thread(AddressOf StartGame)
            StartGameThread.IsBackground = True
            StartGameThread.Start()
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MessageBox.Show("Error: " & ex.Message, appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)



        End Try
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles TextBoxTimer.Tick
        Try
            If FoVNumeric.Value > 120 Then
                FoVNumeric.Value = 120
            End If
        Catch ex As Exception
            TextBoxTimer.Stop()
            WriteError(ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub dbg_Click(sender As Object, e As EventArgs) Handles debugb.Click
        For Each item In HackyFoVComboBox.Items
            MessageBox.Show(item.ToString)
        Next
        Height = (454)
        MsgBox("This is a debug button.", MsgBoxStyle.Information)
        MsgBox(fov)
        MsgBox(fovinterval & " " & FoVTimer.Interval)
        MsgBox(installpath)
        MsgBox(HackyAppBranchLB.Text)
        MsgBox("hotfix: " & hotfix)
        MsgBox("app version: " & Application.ProductVersion)
        UpdateButton.Visible = True
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles HotKeyHandler.Tick
        Dim hotkey3 As Boolean
        Dim hotkey4 As Boolean
        hotkey3 = CBool(GetAsyncKeyState(Keys.Add))
        hotkey4 = CBool(GetAsyncKeyState(Keys.Subtract))
        If hotkey3 = True Then
            If Not CInt(FoVNumeric.Text) + 1 = 121 Then
                FoVNumeric.Text = CStr(CInt(FoVNumeric.Text) + 1)
                neg = False
                ChangeFoV()
            End If
        ElseIf hotkey4 = True Then
            If Not CInt(FoVNumeric.Text) - 1 = 79 Then
                FoVNumeric.Text = CStr(CInt(FoVNumeric.Text) - 1)
                neg = True
                ChangeFoV()
            End If
        End If

        If ChangeHotKeyForm.Visible = True Then Return
        If hotkeyup = 0 Or hotkeyup = Nothing Then Return
        If hotkeydown = 0 Or hotkeydown = Nothing Then Return

        Dim hotkeydownn As Boolean
        Dim hotkeyupp As Boolean
        Dim hotkeycombodownn As Boolean
        Dim hotkeycomboupp As Boolean
        Dim ishotkeydown As Boolean = False
        Dim ishotkeyup As Boolean = False
        hotkeydownn = CBool(GetAsyncKeyState(CType(hotkeydown, Keys)))
        hotkeyupp = CBool(GetAsyncKeyState(CType(hotkeyup, Keys)))

        If Not hotkeycomboup = 0 And Not hotkeycomboup = Nothing Then
            hotkeycomboupp = CBool(GetAsyncKeyState(CType(hotkeycomboup, Keys)))
            ishotkeyup = True
        End If
        If Not hotkeycombodown = 0 And Not hotkeycombodown = Nothing Then
            hotkeycombodownn = CBool(GetAsyncKeyState(CType(hotkeycombodown, Keys)))
            ishotkeydown = True
        End If


        Dim down1 As Integer = 0
        Dim up1 As Integer = 0
        Dim itemlist As Integer = 0
        For Each item In HackyFoVComboBox.Items
            itemlist = itemlist + 1
        Next

        down1 = HackyFoVComboBox.SelectedIndex - 1
        up1 = HackyFoVComboBox.SelectedIndex + 1
        If ishotkeydown = False Then
            If hotkeydownn = True Then


                If Not down1 < 0 Then
                    HackyFoVComboBox.SelectedIndex = down1

                End If
            End If
        ElseIf ishotkeydown = True Then
            If hotkeydownn = True And hotkeycombodownn = True Then


                If Not down1 < 0 Then
                    HackyFoVComboBox.SelectedIndex = down1

                End If
            End If
        End If
        If ishotkeyup = False Then
            If hotkeyupp = True Then

                Try

                    HackyFoVComboBox.SelectedIndex = HackyFoVComboBox.SelectedIndex + 1
                Catch ex As Exception
                    'silent try until I get this figured out
                End Try

            End If
        ElseIf ishotkeyup = True Then
            If hotkeyupp = True And hotkeycomboupp = True Then
                Try

                    HackyFoVComboBox.SelectedIndex = HackyFoVComboBox.SelectedIndex + 1
                Catch ex As Exception
                    'silent try until I get this figured out
                End Try
            End If
        End If


    End Sub
    Private Sub CreateUpdaterApp()
        Try
            Dim myExe As String = temp & "\CoDUO FoV Changer Updater.exe"
            If Not File.Exists(myExe) Then
                File.WriteAllBytes(myExe, My.Resources.CoDUO_FoV_Changer_Updater_CSharp)
                Log.WriteLine("Creating Updater Application at: " & myExe)
            End If
            Process.Start(myExe)
            Log.WriteLine("Restarting/Updating")
            Application.Exit()
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MsgBox("Unable to create updater application. Error: " & ex.Message, MsgBoxStyle.Critical)

        End Try
    End Sub
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles UpdateButton.Click
        Dim updatethread As Thread

        updatethread = New Thread(AddressOf CreateUpdaterApp)
        updatethread.Priority = ThreadPriority.AboveNormal
        updatethread.IsBackground = True
        updatethread.Start()


    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles LaunchParametersTB.TextChanged

        If LaunchParametersTB.TextLength >= 36 Then
            LaunchParametersTB.Height = (50)
        Else
            LaunchParametersTB.Height = (20)
        End If
        If LaunchParametersTB.TextLength >= 139 Then
            LaunchParametersTB.ScrollBars = ScrollBars.Vertical
        Else
            LaunchParametersTB.ScrollBars = ScrollBars.None
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles FogCheckBox.CheckedChanged
        Try
            If FogCheckBox.Checked = True Then
                FogTimer.Stop()
                WriteInteger(exename, &H98861C, 1)
                ini.WriteValue("Extras", "Fog", "Enabled")
                FogTimer.Stop()
                WriteInteger(exename, &H98861C, 1)
            Else
                FogTimer.Start()
                WriteInteger(exename, &H98861C, 0)
                ini.WriteValue("Extras", "Fog", "Disabled")
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
        End Try

    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs)
        Try
            File.Delete(appdata & "CoDUO FoV Changer\settings.ini")
            Log.WriteLine("Reset Config.")
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MsgBox("Unable to reset, you may have already reset it. Error: " & ex.Message, MsgBoxStyle.Information)

            '


        End Try
    End Sub

    Private Sub Timer11_Tick(sender As Object, e As EventArgs) Handles FogTimer.Tick
        Try
            If FogCheckBox.Checked = True Then
                WriteInteger(exename, &H98861C, 1)
            Else
                WriteInteger(exename, &H98861C, 0)
            End If
        Catch ex As Exception
            FogTimer.Stop()
            WriteError(ex.Message, ex.StackTrace)
            MsgBox("Failed to write to memory!" & ex.Message)

            '


        End Try
    End Sub

    Private Sub Button11_Click_1(sender As Object, e As EventArgs)
        SettingsForm.Show()
        Log.WriteLine("Showing Settings Form")
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs)
        Try
            Process.Start("explorer.exe", appdata & "CoDUO FoV Changer\")
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MsgBox(ex.Message, MsgBoxStyle.Critical)

            '


        End Try
    End Sub

    Private Sub Button12_Click_1(sender As Object, e As EventArgs)
        AdtSettingsForm.Show()
    End Sub

    Private Sub Timer7_Tick(sender As Object, e As EventArgs) Handles FoVFixTimer.Tick
        If FoVNumeric.Text = "" Then
            FoVNumeric.Text = CStr(My.Settings.FoVFix) 'FoVFix will never be below 80 or empty.
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs)
        Application.Restart()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles CheckUpdatesLabel.Click
        checkthread = New Thread(AddressOf Checkconnection)
        checkthread.Priority = ThreadPriority.Highest
        checkthread.IsBackground = True
        checkthread.Start()
        If updates = True Then
            MsgBox("No updates found.", MsgBoxStyle.Information)
        Else
            MsgBox("Updates have been found!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, e As EventArgs) Handles Me.Resize

        Try
            If WindowState = FormWindowState.Minimized Then
                If MinimizeCheckBox.Checked = True Then
                    Visible = False
                    MinimizeIcon.Visible = True
                    MinimizeIcon.BalloonTipIcon = ToolTipIcon.Info
                    MinimizeIcon.BalloonTipText = Application.ProductName & " is minimized. Double click to restore full-size."
                    MinimizeIcon.BalloonTipTitle = "Minimized to Tray"
                    MinimizeIcon.ShowBalloonTip(4300, "Minimized to Tray", Application.ProductName & " is minimized. Double click to restore full-size.", ToolTipIcon.Info)
                End If
            End If
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MsgBox(ex.Message, MsgBoxStyle.Critical)

            '


        End Try
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles MinimizeIcon.MouseDoubleClick
        Try
            Visible = True
            WindowState = FormWindowState.Normal
            MinimizeIcon.Visible = False
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)
            MsgBox(ex.Message)

            '


        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles MinimizeCheckBox.CheckedChanged
        If MinimizeCheckBox.Checked = True Then
            ini.WriteValue("Extras", "Minimizetotray", "Yes")
        Else
            ini.WriteValue("Extras", "Minimizetotray", "No")
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Visible = True
        WindowState = FormWindowState.Normal
        MinimizeIcon.Visible = False
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles ABITWTimer.Tick
        If LaunchParametersTB.Text.Contains("wedontneedno") Then
            LaunchParametersLB.Location = New Point(0, 126)
            LaunchParametersTB.Height = 80
            LaunchParametersTB.ScrollBars = ScrollBars.Vertical
            LaunchParametersTB.Text = ""
            Thread.Sleep(200)
            LaunchParametersTB.AppendText("We don't need no education.")
            Thread.Sleep(700)
            LaunchParametersTB.AppendText(newline & "We don't need no thought control.")
            Thread.Sleep(700)
            LaunchParametersTB.AppendText(newline & "No dark sarcasm in the classroom.")
            Thread.Sleep(650)
            LaunchParametersTB.AppendText(newline & "Hey! Teacher, leave them kids alone.")
            Thread.Sleep(700)
            LaunchParametersTB.AppendText(newline & "All in all you're just another brick in the wall.")
            ABITWTimer.Stop()
        End If
        If LaunchParametersTB.Text.Contains("The Endless River") Then
            ABITWTimer.Stop()
            MessageBox.Show("Forever and ever...", "The Endless River", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LaunchParametersTB.Text = LaunchParametersTB.Text.Replace("The Endless River", "")
            ABITWTimer.Start()
        End If

    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CoD1CheckBox.CheckedChanged
        Try
            If CoD1CheckBox.Checked = True Then
                ini.WriteValue("Main", "Game", "CoD1")
                DvarsCheckBox.Text = "Unlock All Dvars (UO only!)"
                DvarsCheckBox.Enabled = False
                DvarsCheckBox.Checked = False
                FogCheckBox.Checked = True
                FogCheckBox.Enabled = False
                FogCheckBox.Text = "Fog (UO only!)"
                Text = "CoDUO FoV Changer (in CoD1 Mode)"
                '  Me.Width = 637
                If File.Exists(cacheloc & "\cache6.cache") Then
                    Dim iscorrupt As Integer = CInt(corruptCheck(cacheloc & "\cache6.cache", 8606))
                    If iscorrupt <= 8605 Or iscorrupt >= 8607 Then
                        CoDPictureBox.Image = My.Resources.Loading
                        CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                        File.Delete(cacheloc & "\cache6.cache")
                        Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                    Else
                        CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache6.cache")
                    End If

                Else
                    CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                    Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                End If
            Else
                ini.WriteValue("Main", "Game", "UO")
                DvarsCheckBox.Text = "Unlock All Dvars"
                DvarsCheckBox.Enabled = True
                FogCheckBox.Enabled = True
                FogCheckBox.Text = "Fog"
                Text = "CoDUO FoV Changer"
                'Me.Width = 624
                If File.Exists(cacheloc & "\cache5.cache") Then
                    Dim iscorrupt As Integer = CInt(corruptCheck(cacheloc & "\cache5.cache", 11846))
                    If iscorrupt <= 11845 Or iscorrupt >= 11847 Then
                        CoDPictureBox.Image = My.Resources.Loading
                        CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                        File.Delete(cacheloc & "\cache5.cache")
                        Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                    Else
                        CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache5.cache")
                    End If

                Else
                    CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                    Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                End If
            End If
            Dim finalImg As Bitmap
            finalImg = New Bitmap(CoDPictureBox.Image, CoDPictureBox.Width, CoDPictureBox.Height)
            CoDPictureBox.SizeMode = PictureBoxSizeMode.CenterImage
            CoDPictureBox.Image = finalImg
        Catch ex As Exception
            MessageBox.Show("An error has occurred while attempting to cache/load image files: " & newline & ex.Message, appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
            WriteError(ex.Message, ex.StackTrace)
        End Try
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles DvarsCheckBox.CheckedChanged
        If DvarsCheckBox.Checked = True Then
            WriteInteger(exename, &H43DD86, 235, nsize:=1)
            WriteInteger(exename, &H43DDA3, 235, nsize:=1)
            WriteInteger(exename, &H43DDC1, 235, nsize:=1)
        Else
            WriteInteger(exename, &H43DD86, 116, nsize:=1)
            WriteInteger(exename, &H43DDA3, 116, nsize:=1)
            WriteInteger(exename, &H43DDC1, 116, nsize:=1)
        End If
    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles UpdateCheckTimer.Tick
        If checkthread.IsAlive = True Then
            checkthread.Abort()
        End If
        checkthread = New Thread(AddressOf Checkconnection)
        checkthread.IsBackground = True
        checkthread.Start()



    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles CoDPictureBox.Click
        If CoD1CheckBox.Checked = True Then
            CoD1CheckBox.Checked = False
        Else
            CoD1CheckBox.Checked = True
        End If
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        SettingsForm.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub AdvancedSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        AdtSettingsForm.Show()
        iscontext = True
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs)
        If checkupdates() = True Then
            ' SettingsButton.Enabled = True
        Else
            '  SettingsButton.Enabled = False
        End If
    End Sub


    Private Sub Timer9_Tick(sender As Object, e As EventArgs) Handles DvarUnlockerTimer.Tick
        If DvarsCheckBox.Checked = True Then
            WriteInteger(exename, &H43DD86, 235, nsize:=1)
            WriteInteger(exename, &H43DDA3, 235, nsize:=1)
            WriteInteger(exename, &H43DDC1, 235, nsize:=1)
        Else
            WriteInteger(exename, &H43DD86, 116, nsize:=1)
            WriteInteger(exename, &H43DDA3, 116, nsize:=1)
            WriteInteger(exename, &H43DDC1, 116, nsize:=1)
        End If
    End Sub

    Private Sub Timer2_Tick_1(sender As Object, e As EventArgs) Handles CmdLineTimer.Tick
        If Not LaunchParametersTB.Text = "" And Not LaunchParametersTB.Text Is Nothing Then

            ini.WriteValue("Extras", "cmdline", LaunchParametersTB.Text)
        End If
    End Sub
    Private Sub InfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoToolStripMenuItem.Click
        MessageBox.Show("Hi, thanks for using my FoV Changer for Call of Duty and Call of Duty United Offensive. This is how to use it properly: " & newline & newline & "1. Start your game and type: r_mode -1 (yes, that's minus 1), r_customwidth " & CStr(My.Computer.Screen.Bounds.Width) & " (your monitor's estimated width), r_customheight " & CStr(My.Computer.Screen.Bounds.Height) & " (your monitor's estimated height)" & newline & newline & "2. Join a server and tab out, or use numpad + and numpad - to adjust your field of view to your liking." & newline & newline & "3. Enjoy playing UO at your monitor's native resolution, with proper Field of View." & newline & newline & "Program developed by:" & newline & "Shady, with the help of CurtDog's logging module, ""CurtLog"".", Application.ProductName & " (" & Application.ProductVersion & ")", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '  MessageBox.Show("Here's some general information on the program: " & Environment.NewLine & Environment.NewLine & "• All config settings and logs are stored in " & appdata & "CoDUO FoV Changer")
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles HackyFoVComboBox.SelectedIndexChanged
        Dim itemlist1 As Integer = 0
        If Not fovbox.Contains(HackyFoVComboBox.SelectedItem.ToString) Then
            If fovbox = "" Then
                ini.WriteValue("Main", "ComboBoxFoV", HackyFoVComboBox.SelectedItem.ToString & ",")
                fovbox = HackyFoVComboBox.SelectedItem.ToString & ","
                Return
            End If

            ini.WriteValue("Main", "ComboBoxFoV", fovbox & HackyFoVComboBox.SelectedItem.ToString & ",")
            fovbox = fovbox & HackyFoVComboBox.SelectedItem.ToString & ","
            FoVNumeric.Text = HackyFoVComboBox.SelectedItem.ToString
        End If
        If CInt(HackyFoVComboBox.SelectedItem) > 120 Then
            Dim replace As String
            replace = fovbox.Replace(HackyFoVComboBox.SelectedItem.ToString & ",", "")
            ini.WriteValue("Main", "ComboBoxFoV", replace)
            HackyFoVComboBox.Items.Remove(HackyFoVComboBox.SelectedItem)
            If Not HackyFoVComboBox.Items.Count <= 0 Then
                HackyFoVComboBox.SelectedIndex = 0
            End If
        End If
        If FoVHotKeyForm.CBBoxFoV.Items.Count >= 1 Then
            FoVHotKeyForm.CBBoxFoV.SelectedIndex = HackyFoVComboBox.SelectedIndex
        End If
        Try
            FoVNumeric.Text = HackyFoVComboBox.SelectedItem.ToString
        Catch ex As Exception
            WriteError(ex.Message, ex.StackTrace)

            Return
        End Try

        ChangeFoV()

        ini.WriteValue("Main", "LastComboBoxFoV", FoVNumeric.Text)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If Not HackyFoVComboBox.Items.Contains(FoVNumeric.Text) Then
            HackyFoVComboBox.Items.Add(FoVNumeric.Text)
            HackyFoVComboBox.SelectedItem = FoVNumeric.Text
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        If Not HackyFoVComboBox.SelectedIndex < 0 Then
            Dim replace As String
            replace = fovbox.Replace(HackyFoVComboBox.SelectedItem.ToString & ",", "")
            HackyFoVComboBox.Items.Remove(HackyFoVComboBox.SelectedItem)
            HackyFoVComboBox.Text = ""
            ini.WriteValue("Main", "ComboBoxFoV", replace)
        End If
        If Not HackyFoVComboBox.Items.Count <= 0 Then
            HackyFoVComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        ChangeHotKeyForm.Show()
    End Sub

    Private Sub ChangelogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangelogToolStripMenuItem.Click
        getcl = New Thread(AddressOf getChangelog)
        getcl.IsBackground = True
        getcl.Start()
    End Sub

    Private Sub GetGameTimeLabel()
        If Not trackGameTime = "True" Then Return
        If gameTime >= 3153600032 Then
            'prevents overflow from timespan, not quite sure the exact max value, but no one is really ever going to play 100 years of Call of Duty
            GameTimeLabel.Text = "Game Time: >= 100 years"
            Return
        End If
        If curSess >= 3153600032 Then
            CurSessionGT.Text = "Current Session: >= 100 years"
            Return
        End If
        If curSess <= 0 Then
            CurSessionGT.Visible = False
            CurSessionGT.Text = "Current Session: No time played"
        End If
        If gameTime <= 0 Then
            GameTimeLabel.Text = "Game Time: No time played"
            Return
        End If

        Dim iSpan As TimeSpan = TimeSpan.FromSeconds(gameTime)
        Dim iSpanCur As TimeSpan = TimeSpan.FromSeconds(curSess)
        Dim TotalMinutes As Double = Math.Floor(iSpan.TotalMinutes)
        Dim CurrentMinutes As Double = Math.Floor(iSpanCur.TotalMinutes)
        Dim TotalHours As Double = Math.Floor(iSpan.TotalHours)

        If gameTime >= 1 And TotalMinutes <= 0 Then
            GameTimeLabel.Text = "Game Time: " & gameTime.ToString() & " seconds"
        End If
        If iSpan.TotalMinutes >= 1 And TotalHours <= 0 And gameTime >= 60 Then
            GameTimeLabel.Text = "Game Time: " & TotalMinutes & " minutes"
        End If
        If TotalHours >= 1 And TotalHours <= 2 Then
            GameTimeLabel.Text = "Game Time: " & TotalHours & " hour"
        End If
        If TotalHours >= 2 Then
            GameTimeLabel.Text = "Game Time: " & TotalHours & " hours"
        End If
        If GameTimeLabel.Text = "Game Time: 0" Then
            GameTimeLabel.Text = "Game Time: No time played"
        End If
        If curSess >= 1 And CurrentMinutes <= 0 Then
            CurSessionGT.Text = "Current Session: " & curSess.ToString() & " seconds"
            CurSessionGT.Visible = True
        End If
        If iSpanCur.TotalMinutes >= 1 And curSess >= 60 Then
            CurSessionGT.Text = "Current Session: " & CurrentMinutes & " minutes"
            CurSessionGT.Visible = True
        End If

    End Sub

    Private Sub GameTracker_Tick(sender As Object, e As EventArgs) Handles GameTracker.Tick
        Try
            Dim MyPID As Process = Process.GetProcessById(pid)
        Catch ex As Exception
            If ex.Message.Contains("Process with an Id") Then
                pid = 0
            Else
                WriteError(ex.Message, ex.StackTrace)

            End If
            Return
        End Try
        GetGameTimeLabel()
        Dim MyP As Process()
        If pid = 0 Then
            If CoD1CheckBox.Checked = True Then
                MyP = Process.GetProcessesByName("CoDMP")
            Else
                MyP = Process.GetProcessesByName(exename)
            End If
            If MyP.Length = 0 Then
                Return
            End If
        End If
        gameTime = gameTime + 1
        curSess = curSess + 1
    End Sub

    Private Sub GameTimeSaver_Tick(sender As Object, e As EventArgs) Handles GameTimeSaver.Tick
        ini.WriteValue("Main", "GameTime", CStr(gameTime))
    End Sub

    Private Sub FoVNumeric_ValueChanged(sender As Object, e As EventArgs) Handles FoVNumeric.ValueChanged
        ChangeFoV()
    End Sub
End Class
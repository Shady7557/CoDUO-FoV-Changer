Option Strict Off
Option Explicit Off

Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports System.Management
Imports Microsoft.VisualBasic.Devices
Imports System.ServiceProcess
Imports System.Threading
Imports System.Net.Sockets
Imports System.Text
Imports System.Text.RegularExpressions
Imports System
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports CurtLog
Imports Microsoft.Win32

Public Class MainFoV
    Public userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Public temp As String = System.Environment.GetEnvironmentVariable("temp")
    Public appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim ini As New IniFile(appdata & "CoDUO FoV Changer\settings.ini")
    Dim iniold As New IniFile(appdata & "CoD UO FoV Changer\options.ini")
    Dim fov As String = ini.ReadValue("FoV", "FoV Value")
    Dim hotkey As String = ini.ReadValue("HotKeys", "Enabled")
    Dim manual As String = ini.ReadValue("Manual Set", "Enabled")
    Dim autorun As String = ini.ReadValue("AutoRun", "Enabled")
    Dim hidden As String = ini.ReadValue("Extras", "Hidden")
    Dim cmdline As String = ini.ReadValue("Extras", "cmdline")
    Dim fog As String = ini.ReadValue("Extras", "Fog")
    Dim firstrun As String = ini.ReadValue("Extras", "FirstRun")
    Dim disableupdatetimer As String = ini.ReadValue("Tweaks", "DisableUpdateTimer")
    Dim sleep As String = ini.ReadValue("Tweaks", "Sleep")
    Dim installpath As String = ini.ReadValue("Main", "InstallPath")
    Dim hotfix As String = "6.3"
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
    Dim iniLocation As String = appdata & "CoDUO FoV Changer\settings.ini"
    Dim oldoptions As String = appdata & "CoD UO FoV Changer\options.ini"
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
    Dim day As Integer = DateTime.Now.Day
    Dim niceDay As String
    Dim month As Integer = DateTime.Now.Month
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
    Dim keyfind As String
    Dim keyfind2 As String
    Dim keyfind3 As String
    Dim rand As New Random
    Dim audioEngine As Thread
    Dim didFS As Boolean = False
    Dim logname As String
    Dim restartneededpath As Boolean = False
    Public pid As Integer = 0
    Public newline As String = Environment.NewLine
    Public iscontext As Boolean = False
    'Dim audio As New AudioFile(temp & "\beep.mp3")
    'Dim audion As New AudioFile(temp & "\beepnegative.mp3")
    Dim thread As System.Threading.Thread
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
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetAsyncKeyState(ByVal vkey As System.Windows.Forms.Keys) As Short
    End Function
    Enum InfoTypes
        VideoCardName
        OperatingSystemName
        ProcessorName
        AmountOfMemory
        VideocardMem
    End Enum
    Private Function checkupdates() As Boolean
        Try
            checkupdates = False
            If isDev = True Then
                Return True
            End If
            Dim request2 As System.Net.WebRequest = System.Net.HttpWebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zczRGeU9CZ3NZc3M")
            Dim response2 As System.Net.WebResponse = request2.GetResponse()

            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response2.GetResponseStream())


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
                MsgBox("Unable to Fetch Updates! " & ex.Message, MsgBoxStyle.Critical)
            Else
                MsgBox("Unable to Fetch Updates! " & ex.Message & newline & "This error is likely caused by your time being out of sync. (System time)", MsgBoxStyle.Critical)
            End If
            Return True
            '   Application.Exit()
        End Try

        Return False
    End Function
    Private Sub getChangelog()

        Try
            Dim myExe As String = temp & "\changelog.tmp.txt"
            If My.Computer.FileSystem.FileExists(myExe) Then
                Dim sr4 As System.IO.StreamReader = New System.IO.StreamReader(myExe)
                Dim cache As String = sr4.ReadToEnd
                sr4.Close()
                If Not cache Is Nothing And Not cache = "" Then
                    Process.Start(myExe)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox("Unable to fetch changelog! " & newline & ex.Message, MsgBoxStyle.Critical)
        End Try


        Try
            Dim request3 As System.Net.WebRequest = System.Net.HttpWebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76za3Y3dW9KYU5kQlE")
            Dim response3 As System.Net.WebResponse = request3.GetResponse()

            Dim sr3 As System.IO.StreamReader = New System.IO.StreamReader(response3.GetResponseStream())

            Dim changelog As String = sr3.ReadToEnd()

            '  Dim rn As New Random

            Dim myExe As String = temp & "\changelog.tmp.txt"
            If My.Computer.FileSystem.FileExists(myExe) Then
                Dim sr4 As System.IO.StreamReader = New System.IO.StreamReader(myExe)
                Dim cache As String = sr4.ReadToEnd
                sr4.Close()
                If Not cache Is Nothing And Not cache = "" Then
                    Process.Start(myExe)
                    Exit Sub
                End If
                If changelog = cache Or cache = changelog Then
                    '    Process.Start(myExe)
                Else
                    System.IO.File.Delete(myExe)
                    myExe = temp & "\changelog.new.tmp.txt"
                    System.IO.File.WriteAllText(myExe, changelog)
                    Process.Start(myExe)
                    Log.WriteLine("Writing Changelog to location: " & myExe)
                End If
            End If
            If Not System.IO.File.Exists(myExe) Then
                System.IO.File.WriteAllText(myExe, changelog)
                Process.Start(myExe)
                Log.WriteLine("Writing Changelog to location: " & myExe)
            End If


        Catch ex As Exception
            MsgBox("Unable to fetch changelog! " & newline & ex.Message, MsgBoxStyle.Critical)
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
    Private Sub systemspecs()
        Try

            Dim strHostName As String

            Dim strIPAddress As String



            strHostName = System.Net.Dns.GetHostName()

            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()


            Dim gfxname As String
            gfxname = (GetInfo(InfoTypes.VideoCardName))

            Dim cpu As String
            cpu = (GetInfo(InfoTypes.ProcessorName))

            Dim ram As String
            ram = (GetInfo(InfoTypes.AmountOfMemory))

            Dim gfxmem As String
            gfxmem = (GetInfo(InfoTypes.VideocardMem))


            Dim smtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            smtpServer.Credentials = New Net.NetworkCredential("fovchangerreports@gmail.com", "FoVchAngeRepoRts")
            'using gmail
            smtpServer.Port = 587
            smtpServer.Host = "smtp.gmail.com"
            smtpServer.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("fovchangerreports@gmail.com")
            mail.To.Add("shady7557@gmail.com")
            mail.To.Add("fovchangerreports@gmail.com")
            mail.Subject = "CoDUO FoV Changer Report"
            mail.Body = " Computer IP: " & strIPAddress & " Computer Info: " & "Available Physical Memory: " & My.Computer.Info.AvailablePhysicalMemory & " Available Virtual Memory: " & My.Computer.Info.AvailableVirtualMemory & " Operating System Info: " & My.Computer.Info.OSFullName & " " & My.Computer.Info.OSPlatform & " " & My.Computer.Info.OSVersion & " Total Physical Memory:" & My.Computer.Info.TotalPhysicalMemory & " System Specs: " & "GFX Card: " & gfxname & " CPU: " & cpu & " RAM: " & ram & " GFX Card Memory: " & gfxmem
            smtpServer.Send(mail)
            Log.WriteLine("Sent E-Mail: " & mail.Body)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub sendreportlog2()
        If Not isDev = True Then
            '          MessageBox.Show("This is disabled because I'm too lazy currently to fix the log code for this part")
            '       Return
        End If
        ' MessageBox.Show("sendreportlog2")

        Try

            Dim strHostName As String

            Dim strIPAddress As String

            strHostName = System.Net.Dns.GetHostName()

            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()


            Dim gfxname As String
            gfxname = (GetInfo(InfoTypes.VideoCardName))

            Dim cpu As String
            cpu = (GetInfo(InfoTypes.ProcessorName))

            Dim ram As String
            ram = (GetInfo(InfoTypes.AmountOfMemory))

            Dim gfxmem As String
            gfxmem = (GetInfo(InfoTypes.VideocardMem))


            Dim smtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            smtpServer.Credentials = New Net.NetworkCredential("fovchangerreports@gmail.com", "FoVchAngeRepoRts")
            'using gmail
            smtpServer.Port = 587
            smtpServer.Host = "smtp.gmail.com"
            smtpServer.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("fovchangerreports@gmail.com")
            mail.To.Add("shady7557@gmail.com")
            mail.To.Add("fovchangerreports@gmail.com")
            mail.Subject = "CoDUO FoV Changer Error Log"
            isEmailing = True
            mail.Body = System.IO.File.ReadAllText(logname)
            MessageBox.Show(mail.Body)
            ' mail.Body = "ERROR: " & dick & Environment.NewLine & "Computer IP: " & strIPAddress & " Computer Info: " & "Available Physical Memory: " & My.Computer.Info.AvailablePhysicalMemory & " Available Virtual Memory: " & My.Computer.Info.AvailableVirtualMemory & " Operating System Info: " & My.Computer.Info.OSFullName & " " & My.Computer.Info.OSPlatform & " " & My.Computer.Info.OSVersion & " Total Physical Memory:" & My.Computer.Info.TotalPhysicalMemory & " System Specs: " & "GFX Card: " & gfxname & " CPU: " & cpu & " RAM: " & ram & " GFX Card Memory: " & gfxmem
            smtpServer.Send(mail)
            isEmailing = False
            '    System.IO.File.Delete(copy)
            Log.WriteLine("Sent E-Mail: " & mail.Body)
        Catch ex As Exception
            '  MsgBox(ex.Message, MsgBoxStyle.Critical)
            Log.WriteLine("!! ERROR !!" & ex.Message & newline & " This shouldn't be treated as an actual error, considering I've only seen it happen whilst restarting after an upgrade, some type of error occurs, it attempts to E-Mail it, restart stops it, error occurs.")
        End Try
    End Sub
    Private Sub sendreportlog(ByVal dick As String)
        Try

            Dim strHostName As String

            Dim strIPAddress As String

            strHostName = System.Net.Dns.GetHostName()

            strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()


            Dim gfxname As String
            gfxname = (GetInfo(InfoTypes.VideoCardName))

            Dim cpu As String
            cpu = (GetInfo(InfoTypes.ProcessorName))

            Dim ram As String
            ram = (GetInfo(InfoTypes.AmountOfMemory))

            Dim gfxmem As String
            gfxmem = (GetInfo(InfoTypes.VideocardMem))


            Dim smtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            smtpServer.Credentials = New Net.NetworkCredential("fovchangerreports@gmail.com", "FoVchAngeRepoRts")
            'using gmail
            smtpServer.Port = 587
            smtpServer.Host = "smtp.gmail.com"
            smtpServer.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("fovchangerreports@gmail.com")
            mail.To.Add("shady7557@gmail.com")
            mail.To.Add("fovchangerreports@gmail.com")
            mail.Subject = "CoDUO FoV Changer Error Log"
            mail.Body = "ERROR: " & dick & newline & "Computer IP: " & strIPAddress & " Computer Info: " & "Available Physical Memory: " & My.Computer.Info.AvailablePhysicalMemory & " Available Virtual Memory: " & My.Computer.Info.AvailableVirtualMemory & " Operating System Info: " & My.Computer.Info.OSFullName & " " & My.Computer.Info.OSPlatform & " " & My.Computer.Info.OSVersion & " Total Physical Memory:" & My.Computer.Info.TotalPhysicalMemory & " System Specs: " & "GFX Card: " & gfxname & " CPU: " & cpu & " RAM: " & ram & " GFX Card Memory: " & gfxmem
            smtpServer.Send(mail)
            MsgBox(mail.Body)
            Log.WriteLine("Sent E-Mail: " & mail.Body)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
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
            If Me.InvokeRequired Then
                Me.Invoke(New MethodInvoker(AddressOf AccessLabel))
            Else

                If updates = True Then
                    CheckUpdatesLabel.Text = ("No Updates found. Click to check again.")
                Else
                    CheckUpdatesLabel.Text = ("Hotfix/Update Available!")
                    CheckUpdatesLabel.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Bold)
                    disableUI()
                End If
                If isDev = True Then
                    CheckUpdatesLabel.Text = "Developer (debug) mode is active. Updates will not be searched for."
                End If
            End If
        Catch ex As Exception
            MsgBox("A critical error has occured: " & ex.Message, MsgBoxStyle.Critical)
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
    Private Sub getNiceDay()

    End Sub
    Private Sub getMonth()
        Dim TimerStart As DateTime
        TimerStart = Now
        If day = 1 Then
            niceDay = "1st"
        ElseIf day = 21 Then
            niceDay = "21st"
        ElseIf day = 31 Then
            niceDay = "31st"
        ElseIf day = 2 Then
            niceDay = "2nd"
        ElseIf day = 22 Then
            niceDay = "22nd"
        ElseIf day = 3 Then
            niceDay = "3rd"
        ElseIf day = 23 Then
            niceDay = "23rd"
        ElseIf day = 4 Then
            niceDay = "4th"
        ElseIf day = 24 Then
            niceDay = "24th"
        ElseIf day = 5 Then
            niceDay = "5th"
        ElseIf day = 25 Then
            niceDay = "25th"
        ElseIf day = 6 Then
            niceDay = "6th"
        ElseIf day = 26 Then
            niceDay = "26th"
        ElseIf day = 7 Then
            niceDay = "7th"
        ElseIf day = 27 Then
            niceDay = "27th"
        ElseIf day = 8 Then
            niceDay = "8th"
        ElseIf day = 9 Then
            niceDay = "9th"
        ElseIf day = 10 Then
            niceDay = "10th"
        ElseIf day = 11 Then
            niceDay = "11th"
        ElseIf day = 12 Then
            niceDay = "12th"
        ElseIf day = 13 Then
            niceDay = "13th"
        ElseIf day = 14 Then
            niceDay = "14th"
        ElseIf day = 15 Then
            niceDay = "15th"
        ElseIf day = 16 Then
            niceDay = "16th"
        ElseIf day = 17 Then
            niceDay = "17th"
        ElseIf day = 18 Then
            niceDay = "18th"
        ElseIf day = 19 Then
            niceDay = "19th"
        ElseIf day = 20 Then
            niceDay = "20th"
        Else
            niceDay = "Unknown"
        End If
        If month = 1 Then
            monthstring = "January "
        ElseIf month = 2 Then
            monthstring = "February "
        ElseIf month = 3 Then
            monthstring = "March "
        ElseIf month = 4 Then
            monthstring = "April "
        ElseIf month = 5 Then
            monthstring = "May "
        ElseIf month = 6 Then
            monthstring = "June "
        ElseIf month = 7 Then
            monthstring = "July "
        ElseIf month = 8 Then
            monthstring = "August "
        ElseIf month = 9 Then
            monthstring = "September "
        ElseIf month = 10 Then
            monthstring = "October "
        ElseIf month = 11 Then
            monthstring = "November "
        ElseIf month = 12 Then
            monthstring = "December "
        Else
            monthstring = "clearly time no longer exists "
        End If
        'start bad code
        'If Module1.location Then
        '  My.Computer.FileSystem.CopyFile(Module1.location, Module1.location & "- copy 1")
        '  Dim cop As String = Module1.location & "- copy 1"
        '  Dim sr As System.IO.StreamReader = New System.IO.StreamReader(cop)
        '  If Not sr.ReadToEnd.Contains(niceDay) Then
        ' Log.WriteLine("                                                                          " & monthstring & niceDay & ", " & DateAndTime.Now.Year)
        '      System.Threading.Thread.Sleep(200)
        '      sr.Close()
        '  End If
        '  System.Threading.Thread.Sleep(1500)
        '  If My.Computer.FileSystem.FileExists(cop) Then
        '      My.Computer.FileSystem.DeleteFile(cop)
        '  End If
        ' End If

        'end bad code

        Dim TimeSpent As System.TimeSpan
        TimeSpent = Now.Subtract(TimerStart)
        ' MsgBox(TimeSpent.TotalSeconds & " seconds spent on this task")
        If TimeSpent.TotalMilliseconds >= 100 Or Debugger.IsAttached = True Then
            Log.WriteLine("program startup took: " & TimeSpent.TotalMilliseconds)
        End If


    End Sub
    Private Sub getWav()
        If Not My.Computer.FileSystem.FileExists(temp & "\beep.mp3") Then
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
                Log.WriteLine("!!ERROR!! " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Cache(sourceurl As String, filename As String)
        If Not My.Computer.FileSystem.FileExists(cacheloc & "\" & filename) Then
            httpclient = New WebClient
            Try
                httpclient.DownloadFileAsync(New Uri(sourceurl), cacheloc & "\" & filename)
            Catch ex As Exception
                MessageBox.Show("An error has occured while attempting to download a picture file: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Function corruptCheck(filename As String, bytes As Long)
        Dim infoReader As System.IO.FileInfo = _
My.Computer.FileSystem.GetFileInfo(filename)
        ' MessageBox.Show(infoReader.Length)
        Return infoReader.Length
    End Function
    Private Function getImgur2()
        'this code isn't yet used, got too confused at 3:40am, will probably work on it later, cause program hanging is annoying
        Try
            getImgur2 = False
            '    Dim request2 As System.Net.WebRequest = System.Net.HttpWebRequest.Create("https://docs.google.com/uc?export=download&id=0B0nCag_Hp76zczRGeU9CZ3NZc3M")
            '     Dim response2 As System.Net.WebResponse = request2.GetResponse()

            Try
                Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")

                Return True
            Catch ex As Exception
                Return False
            End Try



            '  Return False
        Catch ex As Exception
            If Not ex.Message.Contains("Could not establish trust relationship for the SSL/TLS secure channel") Then
                MsgBox("Unable to download an image! " & ex.Message, MsgBoxStyle.Critical)
            Else
                MsgBox("Unable to download an image! " & ex.Message & newline & "This error is likely caused by your time being out of sync. (System time)", MsgBoxStyle.Critical)
            End If
            '   Application.Exit()
        End Try

        Return False
    End Function

    Private Sub getImgur()
        If getImgur2() = False Then
            updates = False
        Else
            updates = True
        End If
        AccessLabel()
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = True

        'Create a variable for start time:
        Dim TimerStart As DateTime
        TimerStart = Now

        If Debugger.IsAttached = True Then
            isDev = True
        Else
            isDev = False
        End If

        Try
            If isElevated = False Then
                MessageBox.Show("Program is not being ran with Administrative privileges, please restart this program with sufficient access.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Fatal error occured: " & ex.Message & " this will prevent the program from starting.")
            Application.Exit()
            Exit Sub
        End Try

        If isDev = True Then
            '     errorOccured = True
        End If

        Dim nolog As Boolean = False
        For Each arguement As String In My.Application.CommandLineArgs
            If arguement = "-nolog" Then
                nolog = True

            End If

        Next

        If isminimal = "" Or isminimal Is Nothing Then
            ini.WriteValue("Extras", "Style", "Default")
        End If

        If saveapplocation.ToLower = "true" Then
            If Not lastwindowposX = "" And Not lastwindowposX Is Nothing And Not lastwindowposY = "" And Not lastwindowposY Is Nothing Then
                Me.Location = New Point(CInt(lastwindowposX), CInt(lastwindowposY))
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
            MessageBox.Show("A fatal error has occured, this will prevent the program from starting: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
            Exit Sub
        End Try




        Try
            If My.Computer.FileSystem.DirectoryExists(appdata & "CoDUO FoV Changer") And My.Computer.FileSystem.DirectoryExists(appdata & "CoDUO FoV Changer\Logs") Then
                'placeholder
            Else
                My.Computer.FileSystem.CreateDirectory(appdata & "CoDUO FoV Changer")
                My.Computer.FileSystem.CreateDirectory(appdata & "CoDUO FoV Changer\Logs")
                'Log.WriteLine("Created Log Folder.")
            End If
        Catch ex As Exception
            MessageBox.Show("A fatal error has occured, this will prevent the program from starting: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Application.Exit()
            Exit Sub
        End Try

        If Not cmdline = "" And Not cmdline Is Nothing Then
            LaunchParametersTB.Text = cmdline
        End If









        getmonthT = New Thread(AddressOf Me.getMonth)
        getmonthT.Priority = ThreadPriority.AboveNormal
        getmonthT.IsBackground = True
        getmonthT.Start()
        If isminimal = "Minimal" Then
            HackyAppBranchLB.Visible = False
            HackyGameVersLB.Visible = False
            HackyAppVersLB.Visible = False
            Me.Height = 220
            'Label2.Location = New Point(0, 162)
        End If

        If isminimal = "Dark" Then
            Me.BackColor = Color.DimGray
            FoVTextBox.BackColor = Color.DarkGray
            LaunchParametersTB.BackColor = Color.DarkGray
            UpdateButton.BackColor = Color.DarkGray
            StartGameButton.BackColor = Color.DarkGray
            ChangeLogButton.BackColor = Color.DarkGray
            SettingsButton.BackColor = Color.DarkGray
            FoVMenuStrip.BackColor = Color.DarkGray
            If StatusLabel.ForeColor = Color.Red Then
                StatusLabel.ForeColor = Color.DarkRed
            End If
        End If

        If month = 12 And DateTime.Now.Day = 25 Then
            MessageBox.Show("wow christmas & stuff", "pls snow", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If month = 10 And DateTime.Now.Day = 31 Then
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


        checkthread = New Thread(AddressOf Me.Checkconnection)
        checkthread.Priority = ThreadPriority.AboveNormal
        checkthread.IsBackground = True
        checkthread.Start()

        '   getW = New Thread(AddressOf Me.getWav)
        '   getW.IsBackground = True
        '   getW.Start()



        Dim splitStrr() As String
        If fovbox.Contains(",") Then
            splitStrr = fovbox.Split(",")
            For Each word In splitStrr
                If Not word = "" Then
                    '               MessageBox.Show(word)
                    If Not CInt(word) >= 121 Then
                        HackyFoVComboBox.Items.Add(word)
                    Else
                        Log.WriteLine(word & " is higher than 120 fov (max) will not add to combobox")
                    End If
                End If
            Next
        End If


        If Not My.Computer.FileSystem.DirectoryExists(cacheloc) Then
            My.Computer.FileSystem.CreateDirectory(cacheloc)
        End If
        Dim rn As New Random

        Try
            If gamev = "UO" Or gamev Is Nothing Or gamev = "" Then
                If My.Computer.FileSystem.FileExists(cacheloc & "\cache5.cache") Then
                    Dim iscorrupt As String = corruptCheck(cacheloc & "\cache5.cache", 11846)
                    If iscorrupt <= 11845 Or iscorrupt >= 11847 Then
                        CoDPictureBox.Image = My.Resources.Loading
                        CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                        My.Computer.FileSystem.DeleteFile(cacheloc & "\cache5.cache")
                        Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                    Else
                        CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache5.cache")
                    End If

                Else
                    CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                    Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                End If
            Else
                If My.Computer.FileSystem.FileExists(cacheloc & "\cache6.cache") Then
                    Dim iscorrupt As String = corruptCheck(cacheloc & "\cache6.cache", 8606)
                    If iscorrupt <= 8605 Or iscorrupt >= 8607 Then
                        CoDPictureBox.Image = My.Resources.Loading
                        CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                        My.Computer.FileSystem.DeleteFile(cacheloc & "\cache6.cache")
                        Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                    Else
                        CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache6.cache")
                    End If

                Else
                    CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                    Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("An error has occured while attempting to cache files, this could be a result of no write permissions or not having an internet connection: " & ex.Message & newline & " this should not prevent the program from otherwise running normally.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log.WriteLine("Unable to retrieve or cache pictures: " & ex.Message)
            CoDPictureBox.Image = My.Resources.Loading
        End Try


        If System.IO.File.Exists(appdata & "CoD UO FoV Changer\options.ini") Then
            Try
                System.IO.File.Delete(iniLocation)
                System.IO.File.Copy(oldoptions, iniLocation)
                '   ini.WriteValue("FoV", "FoV Value", testingaa)
                ' My.Settings.didUpgrade = True
                My.Settings.FoVFix = CInt(testingaa)
                My.Settings.Save()
                ini.WriteValue("FoV", "FoV Value", CStr(My.Settings.FoVFix))
                'Dim restartneeded As Boolean = True
                restartNeeded = True
            Catch ex As Exception

            End Try
        End If

        If My.Computer.FileSystem.FileExists(temp & "\CoDUO FoV Changer Updater.exe") Then
            My.Computer.FileSystem.DeleteFile(temp & "\CoDUO FoV Changer Updater.exe")
        End If

        If restartNeeded = True Then
            MessageBox.Show("Your .ini file has been moved, it is recommended your restart the program, and as such, most of the program has been disabled until you do so.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            restartNeeded = False
            FoVTextBox.ReadOnly = True
            FoVTimer.Enabled = False
            StartGameButton.Enabled = False
            ChangeLogButton.Enabled = False
            SettingsButton.Enabled = False
        End If


        If Environment.Is64BitOperatingSystem = True Then
            ostype = "64"
        Else
            ostype = "86"
        End If

        'safety code
        If Not fov = "80" Or Not fov = "" Then
            FoVTextBox.Text = CStr(fov)
        Else
            FoVTextBox.Text = CStr(My.Settings.FoVFix)
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

        If Not fog = "Disabled" Or Not fog = "Enabled" Then
            ini.WriteValue("Extras", "Fog", "Enabled")
        End If


        If fog = "Enabled" Then 'Checks if fog is enabled in the .ini
            FogCheckBox.Checked = True
            WriteInteger("CoDUOMP", &H9885F0, 1)
            FogTimer.Stop()
            Log.WriteLine("Stopping Fog Timer, turning fog on, checking Fog CheckBox.")
        ElseIf fog = "Disabled" Then
            If Not didFS = True Then
                FogCheckBox.Checked = False
                WriteInteger("CoDUOMP", &H9885F0, 0)
                FogTimer.Start()
                Log.WriteLine("Starting Fog Timer, turning fog off, unchecking Fog CheckBox")
            End If
        End If

        Try
            If startline.Contains("-launch") Then
                ini.WriteValue("Extras", "StartCommandLine", startline.Replace("-launch", ""))
            End If
        Catch ex As Exception
            Log.WriteLine("!! ERROR !! " & ex.Message)
        End Try

        DvarsCheckBox.Visible = False
        For Each arguement As String In My.Application.CommandLineArgs
            Dim splitStr() As String
            If arguement = ("-unlock") Then
                DvarsCheckBox.Visible = True
            End If
            If arguement.Contains("-fov=") Then
                '   If Not arguement.StartsWith("-") Then
                splitStr = arguement.Split("=")
                If splitStr(1) = Nothing Or splitStr(1) = "" Then
                    Return
                End If

                If CInt(splitStr(1)) >= 121 Then
                    splitStr(1) = CInt(120)
                End If
                If CInt(splitStr(1)) <= 79 Then
                    splitStr(1) = CInt(80)
                End If
                If splitStr(1).StartsWith(1) Or splitStr(1).StartsWith(2) Or splitStr(1).StartsWith(3) Or splitStr(1).StartsWith(4) Or splitStr(1).StartsWith(5) Or splitStr(1).StartsWith(6) Or splitStr(1).StartsWith(7) Or splitStr(1).StartsWith(8) Or splitStr(1).StartsWith(9) Then
                    FoVTextBox.Text = splitStr(1)
                    Log.WriteLine("Launched fov changer with -fov=" & splitStr(1))
                End If
                '   MessageBox.Show(splitStr(1))
            End If
            '   If My.Computer.FileSystem.FileExists("C:\users\matt_\cod.dat") Then
            If arguement.Contains("-fog=") Then
                '   If Not arguement.StartsWith("-") Then
                splitStr = arguement.Split("=")
                'MessageBox.Show(splitStr(1))
                If splitStr(1) = Nothing Or splitStr(1) = "" Then
                    Return
                End If

                If CInt(splitStr(1) < 0) Then
                    splitStr(1) = 0
                End If
                If CInt(splitStr(1) >= 2) Then
                    splitStr(1) = 1
                End If
                If splitStr(1).StartsWith(0) Or splitStr(1).StartsWith(1) Then
                    If splitStr(1) = 1 Then
                        FogCheckBox.Checked = True
                        FogTimer.Stop()
                        ini.WriteValue("Extras", "FogMSG", "Ask")
                        ini.WriteValue("Extras", "Fog", "Enabled")
                    ElseIf splitStr(1) = 0 Then
                        didFS = True
                        ini.WriteValue("Extras", "FogMSG", "DoNotAsk")
                        ini.WriteValue("Extras", "Fog", "Disabled")
                        FogCheckBox.Checked = False
                        FogTimer.Start()
                    End If
                    Log.WriteLine("Launched fov changer with -fog=" & splitStr(1))
                End If
                '   MessageBox.Show(splitStr(1))
            End If
            '   End If

            If arguement.Contains("-menustrip=") Then
                '   If Not arguement.StartsWith("-") Then
                splitStr = arguement.Split("=")
                'MessageBox.Show(splitStr(1))
                If splitStr(1) = Nothing Or splitStr(1) = "" Then
                    Return
                End If

                If CInt(splitStr(1) < 0) Then
                    splitStr(1) = 0
                End If
                If CInt(splitStr(1) >= 1) Then
                    Return
                    '      splitStr(1) = 1
                End If
                If splitStr(1).StartsWith(0) Or splitStr(1).StartsWith(1) Then
                    If splitStr(1) = 1 Then
                        '    MenuStrip1.Visible = true
                    ElseIf splitStr(1) = 0 Then
                        FoVMenuStrip.Visible = False
                    End If
                    Log.WriteLine("Launched fov changer with -menustrip=" & splitStr(1))
                End If
                '   MessageBox.Show(splitStr(1))
            End If

            ' MessageBox.Show("is there a return")

            '  End If

            If arguement = ("-launch") Then 'Automatically launches the game, probably useful if you're not running a command line and don't know the autorun .ini value exists, one of the codes can be removed.
                'Button3.PerformClick()

                Try
                    If startline.Contains("-launch") Then
                        ini.WriteValue("Extras", "StartCommandLine", startline.Replace("-launch", ""))
                    End If
                    '   ini.WriteValue("Extras", "StartCommandLine", arguement)
                    Dim startInfo = New ProcessStartInfo()
                    startInfo.WorkingDirectory = installpath
                    If Not LaunchParametersTB.Text = "" And Not LaunchParametersTB Is Nothing Then
                        startInfo.Arguments = LaunchParametersTB.Text & " +set r_ignorehwgamma 1" & " +set vid_xpos 0 +set vid_ypos 0 +set com_hunkmegs 128 +set win_allowalttab 1"
                    Else
                        startInfo.Arguments = "+set r_ignorehwgamma 1" & " +set vid_xpos 0 +set vid_ypos 0 +set com_hunkmegs 128 +set win_allowalttab 1"
                    End If
                    Log.WriteLine("Game arguments are: " & startInfo.Arguments)
                    startInfo.FileName = installpath & "\CoDUOMP.exe"
                    Process.Start(startInfo)
                    Log.WriteLine("Started game with -launch")



                Catch ex As Exception
                    MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
                    errorOccured = True
                    Log.WriteLine("ERROR !!:  " & ex.Message)


                End Try
                '  Button3.Enabled = False
            End If

            If arguement = ("-forceupdate") Then 'Self explantory? Isn't it? Also seems to sometimes crash the program.  --- NOT ANYMORE!
                ' Button6.PerformClick()
                Const quote As String = """"

                Dim test As String
                test = quote & "CoDUO FoV Changer Updater.exe" & quote
                System.Threading.Thread.Sleep(1800) ' lets try to wait for everything to finish first.
                Try
                    Dim myExe As String = temp & "\CoDUO FoV Changer Updater.exe"
                    If Not System.IO.File.Exists(myExe) Then
                        System.IO.File.WriteAllBytes(myExe, My.Resources.CoDUO_FoV_Changer_Updater)
                        Log.WriteLine("Creating Updater Application.")
                    End If
                    Dim shasta As New ProcessStartInfo
                    shasta.CreateNoWindow = True
                    shasta.WindowStyle = ProcessWindowStyle.Hidden
                    shasta.WorkingDirectory = temp
                    shasta.FileName = myExe
                    Process.Start(shasta)
                    Log.WriteLine("Restarting.")
                    System.Threading.Thread.Sleep(700)
                    Application.Exit()
                Catch ex As Exception
                    Log.WriteLine("!! ERROR !! Unable to create Updater Application:")
                    Log.WriteError(ex)
                    errorOccured = True
                    '


                End Try
            End If




            If arguement.Contains("-unlock=") Then
                splitStr = arguement.Split("=")
                If splitStr(1) = Nothing Or splitStr(1) = "" Then
                    Return
                End If

                If CInt(splitStr(1)) >= 1 Then
                    splitStr(1) = CInt(1)
                    DvarsCheckBox.Visible = True
                    DvarsCheckBox.Checked = True
                    DvarUnlockerTimer.Start()
                End If
                If CInt(splitStr(1)) <= 0 Then
                    splitStr(1) = CInt(0)
                    DvarsCheckBox.Visible = True
                    DvarUnlockerTimer.Stop()
                End If
                Log.WriteLine("Started fov changer with -unlock=" & splitStr(1))
            End If


        Next
        If DvarsCheckBox.Visible = False Then
            DvarsCheckBox.Checked = False
        End If


        If Not fovinterval = "" Then 'Checks the .ini for the user specified timer interval, if one exists
            FoVTimer.Interval = CInt(fovinterval)
        Else
            FoVTimer.Interval = 1500
        End If



        Try 'Sets the empty value for the .ini file, and then also disables or enables the timer that automatically checks for updates.
            If disableupdatetimer = "" Then
                ini.WriteValue("Tweaks", "DisableUpdateTimer", "No")
            ElseIf disableupdatetimer = "Yes" Then
                UpdateCheckTimer.Stop()
            ElseIf disableupdatetimer = "No" Or disableupdatetimer = "" Or disableupdatetimer Is Nothing Then
                UpdateCheckTimer.Start()
            End If
        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message)
        End Try

        Dim readvalue As String = ""
        Try
            If ostype = "64" Then
                If installpath = "" Or installpath Is Nothing Then  'potential speed improvement if we're not checking the registry each time it starts the program, may accidentally cause errors
                    '   My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive")
                    readvalue = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "InstallPath", "no path found"))
                End If
            ElseIf ostype = "86" Then
                ' My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Activision\Call of Duty United Offensive")
                If installpath = "" Or installpath Is Nothing Then
                    readvalue = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "InstallPath", "no path found"))
                End If
            End If
            '      If Not CStr(readvalue) Then
            '          readvalue = ""
            '      End If
        Catch ex As Exception
            MsgBox("Registry access denied, please run this program as an Administrator. Or other error: " & ex.Message, MsgBoxStyle.Critical)
            Log.WriteLine("Registry access denied, please run this program as an Administrator. Or other error: " & ex.Message)
            errorOccured = True
            '


        End Try

        Try
            If Not installpath = "" And Not installpath Is Nothing Then
                readvalue = installpath
            End If
        Catch ex As Exception

        End Try


        Dim donotcheck As Boolean
        Dim dorestart As Boolean
        Dim donotcheckk As Boolean = False
        donotcheck = False
        dorestart = False

        If readvalue Is Nothing Or readvalue = "" Then
            donotcheckk = True
            'MessageBox.Show(readvalue & " <--")
            readvalue = ""
        End If


        Try
            If readvalue.Contains("Call of Duty") Or readvalue.Contains("CoD") And Not donotcheckk = True Then
                ini.WriteValue("Extras", "FirstRun", "No")
                ini.WriteValue("Main", "InstallPath", readvalue)
            Else
                donotcheck = True
                dorestart = True
                MsgBox("Hello! We see that this is your first time using this program, and we could also not automatically find your game directory. We highly recommend you set your game path properly if it wasn't already set. We'll only ask you to do this once, that way everytime you update your FoV changer it will always be placed in your UO folder.", MsgBoxStyle.Information)
                Try

                    FolderBrowserDialog1.Description = ("Select your game path.")
                    FolderBrowserDialog1.ShowDialog()
                    Dim uoinstall As String
                    uoinstall = CStr(FolderBrowserDialog1.SelectedPath)


                    If Not readvalue = "" Then
                        If readvalue = uoinstall Or uoinstall = readvalue Then 'Messy and buggy code, needs refined.
                            MsgBox("This is already your directory!", MsgBoxStyle.Information)
                            ini.WriteValue("Main", "InstallPath", readvalue)
                        End If
                    Else
                        If ostype = "64" Then

                            'My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive")
                            My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", RegistryKeyPermissionCheck.ReadWriteSubTree)
                            System.Threading.Thread.Sleep(300)
                            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "InstallPath", uoinstall)
                            readvalue = uoinstall
                            ini.WriteValue("Main", "InstallPath", readvalue)
                            MsgBox("Completed!", MsgBoxStyle.Information)

                        ElseIf ostype = "86" Then
                            My.Computer.Registry.LocalMachine.CreateSubKey("SOFTWARE\Activision\Call of Duty United Offensive")
                            System.Threading.Thread.Sleep(400)
                            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "InstallPath", uoinstall)
                            readvalue = uoinstall
                            ini.WriteValue("Main", "InstallPath", readvalue)
                            MsgBox("Completed!", MsgBoxStyle.Information)

                        End If
                    End If
                    '     specsthread = New Thread(AddressOf Me.systemspecs)
                    '      specsthread.Priority = ThreadPriority.AboveNormal
                    '       specsthread.IsBackground = True
                    ' specsthread.Start()
                Catch ex As Exception
                    errorOccured = True
                    MsgBox("Error setting registry, invalid path? " & ex.Message, MsgBoxStyle.Critical)
                    Log.WriteLine("!! ERROR !! tried to set registry and failed: " & ex.Message)
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show("An error has occurred while attempting to read/write registry: " & ex.Message & newline & "this may prevent the program from functioning normally.")
            errorOccured = True
            Log.WriteLine("!! ERROR !! " & ex.Message)
            '


        End Try


        Try 'Atttempts to check if the set directory contains the UO exe.
            If My.Computer.FileSystem.FileExists(readvalue & "\CoDUOMP.exe") Or donotcheck = True Then
                ini.WriteValue("Extras", "FirstRun", "No")
            Else
                ini.WriteValue("Extras", "FirstRun", "Yes")
                MsgBox("The path you have set does not contain CoDUOMP.exe, please set your path again.", MsgBoxStyle.Information)
                Application.Restart()
            End If
        Catch ex As Exception
            errorOccured = True
            MsgBox("Error setting/checking UO path... " & ex.Message, MsgBoxStyle.Critical)
            Log.WriteLine("!! ERROR !! " & ex.Message)
            '


        End Try

        Dim readvalue2 As String = ""
        Try
            If Not gamevers = "" And Not gamevers Is Nothing Then


                keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys.
                keyfind2 = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "CodKey", "0")) 'Searches for CD-keys.
                keyfind3 = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "0")) 'Searches for CD-keys. Changed to the number 0 because it wont produce errors, and I can just have the label chance if the text is equal to '0'



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
            End If
            If Not gamevers Is Nothing And Not gamevers = "" Then
                HackyGameVersLB.Text = ("CoD:UO Version: " & gamevers)
                readvalue2 = gamevers 'lazy code
            Else
                HackyGameVersLB.Text = ("CoD:UO Version: " & readvalue2) 'Sets the label to read the games version via registry key above.
            End If
            HackyAppBranchLB.Text = "Application Branch: " & "2.2" 'Sets the label's text to contain the application branch.
            Dim testString As String = "Application Version: " & Application.ProductVersion
            HackyAppVersLB.Text = testString

        Catch ex As Exception
            MessageBox.Show("An error has occured while attempting to read registry and set program labels: " & ex.Message & " this should not prevent the program from functioning normally.")
        End Try



        'Logs stuff
        Log.WriteLine("User is using version: " & Me.ProductVersion & " Hotfix Version: " & hotfix)
        Log.WriteLine("Log folder is: " & appdata & "CoDUO FoV Changer\Logs")


        If Not readvalue2 Is Nothing And Not readvalue2 = "" Then
            If CInt(readvalue2) < 1.51 Then
                FoVTimer.Stop()
                FoVTextBox.ReadOnly = True
                MsgBox("Your Call of Duty Version is not the correct version for this program, the FoV Changer only works on 1.51, if you're sure you have 1.51, then check UO registry, and set it through that.", MsgBoxStyle.Information)
            End If
        End If


        If hidekey = "" Or hidekey = Nothing Then 'Checks if the user want's their CD-key not to be shown on the label.
            '     Button15.Text = ("Show CD-Key")
            showkey()
            ini.WriteValue("Extras", "HideKey", "Yes")
            hidekey = "Yes"
        End If

        If dorestart = True Then
            restartneededpath = True
            MsgBox("Restarting to set install path...", MsgBoxStyle.Information)
            Application.Restart()
            Return
        End If

        Try
            If Not lastlogname = "" And Not isDev = True Then
                System.IO.File.Delete(lastlogname)
            End If
        Catch ex As Exception

        End Try

        sendreportl = New Thread(AddressOf Me.sendreportlog)
        sendreportl.IsBackground = True
        sendreportl.Priority = ThreadPriority.AboveNormal
        sendreportl2 = New Thread(AddressOf Me.sendreportlog2)
        sendreportl2.IsBackground = True
        sendreportl2.Priority = ThreadPriority.AboveNormal

        '  If Not My.Computer.FileSystem.FileExists("C:\Users\matt_\cod.dat") Then
        'FogCheckBox.Checked = True
        '    FogCheckBox.Enabled = False
        '  FogCheckBox.Visible = False
        'End If


        StartGameButton.Select()



        Dim TimeSpent As System.TimeSpan
        TimeSpent = Now.Subtract(TimerStart)
        ' MsgBox(TimeSpent.TotalSeconds & " seconds spent on this task")
        If TimeSpent.TotalMilliseconds >= 100 Or Debugger.IsAttached = True Then
            Log.WriteLine("program startup took: " & TimeSpent.TotalMilliseconds)
        End If

        Dim versmod As String = Application.ProductVersion.Substring(0, 3)
        ' MessageBox.Show(versmod)
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
            MessageBox.Show(Application.ProductName & " has been updated from Unknown to " & progvers & " (" & "HF" & hotfix & "), be sure to check the changelog to see what's different!", appname, MessageBoxButtons.OK, MessageBoxIcon.Information)
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

        Dim itemlist As Integer = 0
        For Each item In HackyFoVComboBox.Items
            itemlist = itemlist + 1
        Next
        If itemlist >= 1 Then
            HackyFoVComboBox.SelectedIndex = 0
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

        If Not HackyFoVComboBox.Items.Count <= 0 Then
            For Each itemstring In HackyFoVComboBox.Items
                If itemstring.ToString.Contains(lastcboxfov) And Not lastcboxfov = "" And Not lastcboxfov Is Nothing Then
                    HackyFoVComboBox.SelectedItem = lastcboxfov
                End If
            Next
        End If


        '    MessageBox.Show(TimeSpent.TotalMilliseconds)
        '   If nolog = True Then MessageBox.Show(TimeSpent.TotalSeconds)


    End Sub
    Public Function GetInfo(ByVal InfoType As InfoTypes) As String
        'Create a variable for start time:
        Dim TimerStart As DateTime
        TimerStart = Now
        Dim Info As New ComputerInfo : Dim Value As String = "", vganame As String = "", vgamem As String = "", proc As String = ""
        Dim searcher As New Management.ManagementObjectSearcher( _
            "root\CIMV2", "SELECT * FROM Win32_VideoController")
        Dim searcher1 As New Management.ManagementObjectSearcher( _
            "SELECT * FROM Win32_Processor")
        If InfoType = InfoTypes.OperatingSystemName Then
            Value = Info.OSFullName
        ElseIf InfoType = InfoTypes.ProcessorName Then
            proc = ""
            For Each queryObject As ManagementObject In searcher1.Get
                proc = queryObject.GetPropertyValue("Name").ToString
            Next
            Value = proc
        ElseIf InfoType = InfoTypes.AmountOfMemory Then
            Value = Math.Round((((CDbl(Convert.ToDouble(Val(Info.TotalPhysicalMemory))) / 1024)) / 1024), 2) & " MB"
        ElseIf InfoType = InfoTypes.VideoCardName Then
            vganame = ""
            For Each queryObject As ManagementObject In searcher.Get
                vganame = queryObject.GetPropertyValue("Name").ToString
            Next
            Value = vganame
        ElseIf InfoType = InfoTypes.VideocardMem Then
            vgamem = ""
            For Each queryObject As ManagementObject In searcher.Get
                vgamem = queryObject.GetPropertyValue("AdapterRAM").ToString
            Next
            Value = Math.Round((((CDbl(Convert.ToDouble(Val(vgamem))) / 1024)) / 1024), 2) & " MB"
        End If
        Dim TimeSpent As System.TimeSpan
        TimeSpent = Now.Subtract(TimerStart)
        If TimeSpent.TotalMilliseconds >= 80 Then
            Log.WriteLine("getinfo took: " & TimeSpent.TotalMilliseconds)
        End If
        Return Value
    End Function

    Private Sub Form1_Close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Dim TimerStart As DateTime
        TimerStart = Now
        Try
            WriteFloat("CoDUOMP", &H3052F7C8, 80)
        Catch ex As Exception
            Log.WriteLine("ERROR !!:  " & ex.Message)
            errorOccured = True
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
        End Try

        If System.IO.File.Exists(oldoptions) Then
            My.Computer.FileSystem.DeleteDirectory(appdata & "CoD UO FoV Changer", FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If

        ini.WriteValue("FoV", "FoV Value", FoVTextBox.Text)


        '     MessageBox.Show("what")
        If errorOccured = True Then
            If Not restartneededpath = True Then
                sendreportl2.Start()
                '  MessageBox.Show("attempt to start reportl")
            End If

            '   MessageBox.Show("sendreportl")
            '  ElseIf isDev = True And errorOccured = False Then
        End If


        Try
            If isEmailing = False Then
                '   Dim copy = Module1.location & "- copy"

                '  System.IO.File.Delete(copy)
            End If
        Catch ex As Exception
            'no errors ever occur, or shouldn't, anyway.
            Log.WriteLine("ERROR !!:  " & ex.Message)
        End Try

        If My.Computer.FileSystem.FileExists(lastlogname & " - copy 1") Then
            My.Computer.FileSystem.DeleteFile(lastlogname & " - copy 1")
        End If

        If My.Computer.FileSystem.FileExists(temp & "\changelog.tmp.txt") Then
            File.Delete(temp & "\changelog.tmp.txt")
        End If
        'these are in 2 seperate if statements because they can both exist at once, thus, a elseif would not work.
        If My.Computer.FileSystem.FileExists(temp & "\changelog.new.tmp.txt") Then
            File.Delete(temp & "\changelog.new.tmp.txt")
        End If

        Log.WriteLine("Closing, doing lots of stuff.")
        If My.Computer.FileSystem.DirectoryExists(appdata & "CoDUO FoV Changer\Logs") Then
            '         Log.FlushBuffer()
        End If

        If saveapplocation.ToLower = "true" And Not lastwindowposX = CStr(Me.Location.X) And Not lastwindowposY = CStr(Me.Location.Y) Then
            ini.WriteValue("Extras", "LastWindowPosX", CStr(Me.Location.X))
            ini.WriteValue("Extras", "LastWindowPosY", CStr(Me.Location.Y))
            Log.WriteLine("Saved App Location: " & CStr(Me.Location.X) & " " & CStr(Me.Location.Y))
        End If


        Dim TimeSpent As System.TimeSpan
        TimeSpent = Now.Subtract(TimerStart)
        If TimeSpent.TotalMilliseconds >= 80 Then
            Log.WriteLine("form1 closing took: " & TimeSpent.TotalMilliseconds)
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles FoVTimer.Tick
        Try
            If CoD1CheckBox.Checked = False Then
                If pid = 0 Then
                    WriteFloat("CoDUOMP", &H3052F7C8, FoVTextBox.Text)
                Else
                    WriteFloatpid(pid, &H3052F7C8, FoVTextBox.Text)
                End If
            Else
                WriteFloat("CoDMP", &H3029CA28, FoVTextBox.Text)
            End If
            If pid = 0 Then
                Dim MyP As Process() = Process.GetProcessesByName("CoDUOMP")
                If MyP.Length = 0 Then
                    StatusLabel.Text = ("Status: not found or failed to write to memory!")
                    If isminimal = "Dark" Then
                        StatusLabel.ForeColor = Color.DarkRed
                    Else
                        StatusLabel.ForeColor = Color.Red
                    End If
                    If Not CoD1CheckBox.Checked = True Then
                        StartGameButton.Enabled = True
                    Else
                        StartGameButton.Enabled = False
                    End If

                    Exit Sub
                Else
                    StatusLabel.Text = ("Status: UO found and wrote to memory!")
                    If isminimal = "Dark" Then
                        StatusLabel.ForeColor = Color.DarkGreen
                    Else
                        StatusLabel.ForeColor = Color.Green
                    End If
                    StartGameButton.Enabled = False
                End If
            End If
        Catch ex As Exception
            FoVTimer.Stop()
            '   MsgBox(ex.Message)
            Log.WriteLine("ERROR !!:  " & ex.Message)
            errorOccured = True
            '


        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles StartGameButton.Click
        Try
            Dim startInfo = New ProcessStartInfo()
            startInfo.WorkingDirectory = installpath
            If Not LaunchParametersTB.Text = "" And Not LaunchParametersTB.Text Is Nothing Then
                startInfo.Arguments = LaunchParametersTB.Text & " +set r_ignorehwgamma 1" & " +set vid_xpos 0 +set vid_ypos 0 +set com_hunkmegs 128 +set win_allowalttab 1"
            End If
            startInfo.FileName = installpath & "\CoDUOMP.exe"
            Process.Start(startInfo)
            Log.WriteLine("Started game.")



        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical)
            errorOccured = True
            Log.WriteLine("ERROR !!:  " & ex.Message)


        End Try
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles TextBoxTimer.Tick
        Try
            If FoVTextBox.Text > 120 Then
                FoVTextBox.Text = 120
            End If
        Catch ex As Exception
            TextBoxTimer.Stop()
            Log.WriteLine("ERROR !!:  " & ex.Message)
            errorOccured = True
            Dim timesoccured As String = ""
            If isDev = True Then
                timesoccured = (+1)
            End If
            If Not timesoccured = 3 & isDev = True Then
                TextBoxTimer.Start()
            End If
        End Try
    End Sub

    Private Sub dbg_Click(sender As Object, e As EventArgs) Handles debugb.Click
        '        If Not Debugger.IsAttached = True Then
        ' MessageBox.Show("debugger not attached, returning...")
        ' Return
        ' End If
        For Each item In HackyFoVComboBox.Items
            MessageBox.Show(item.ToString)
        Next
        Me.Height = (454)
        MsgBox("This is a debug button.", MsgBoxStyle.Information)
        MsgBox(fov)
        MsgBox(fovinterval & " " & FoVTimer.Interval)
        MsgBox(installpath)
        MsgBox(HackyAppBranchLB.Text)
        MsgBox("hotfix: " & hotfix)
        MsgBox("app version: " & Application.ProductVersion)
        UpdateButton.Visible = True
    End Sub
    'Private Sub playpos()
    '    If neg = True Then
    '        Me.audio.Play()
    '    Else
    '        Me.audion.Play()
    '    End If
    'End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles HotKeyHandler.Tick
        If hidden = ("Yes") Then
            Try
                Dim hotkey As Boolean
                Dim hotkey2 As Boolean
                hotkey = GetAsyncKeyState(Keys.F2)
                If hotkey = True Then
                    Me.Visible = False
                    Log.WriteLine("Hid Window")
                End If
                hotkey2 = GetAsyncKeyState(Keys.F3)
                If hotkey2 = True Then
                    Me.Visible = True
                    Log.WriteLine("Unhid Window")
                End If
            Catch ex As Exception
                MsgBox("Unable to hide.", MsgBoxStyle.Critical)
                Log.WriteLine("!! ERROR !! Unable to hide Window. " & ex.Message)
            End Try
        End If
        Dim hotkey3 As Boolean
        Dim hotkey4 As Boolean
        hotkey3 = GetAsyncKeyState(Keys.Add)
        hotkey4 = GetAsyncKeyState(Keys.Subtract)
        If hotkey3 = True Then
            If Not FoVTextBox.Text + 1 = 121 Then
                FoVTextBox.Text = FoVTextBox.Text + 1
                neg = False
                'audioEngine = New Thread(AddressOf Me.playpos)
                'audioEngine.IsBackground = True
                'audioEngine.Start()
                'If Not hasPlayed = True Then
                '    audio.Play() 'apparently this fixes it?
                '    hasPlayed = True
                'End If
                If CoD1CheckBox.Checked = False Then
                    If pid = 0 Then
                        WriteFloat("CoDUOMP", &H3052F7C8, FoVTextBox.Text)
                    Else
                        WriteFloatpid(pid, &H3052F7C8, FoVTextBox.Text)
                    End If
                Else
                    WriteFloat("CoDMP", &H3029CA28, FoVTextBox.Text)
                End If
            End If
        ElseIf hotkey4 = True Then
            If Not FoVTextBox.Text - 1 = 79 Then
                FoVTextBox.Text = FoVTextBox.Text - 1
                ' Dim audio As New AudioFile(temp & "\beep.mp3")
                neg = True
                'audioEngine = New Thread(AddressOf Me.playpos)
                'audioEngine.IsBackground = True
                'audioEngine.Start()
                'If Not hasPlayed = True Then
                '    audion.Play() 'apparently this fixes it?
                '    hasPlayed = True
                'End If

                If CoD1CheckBox.Checked = False Then

                    If pid = 0 Then
                        WriteFloat("CoDUOMP", &H3052F7C8, FoVTextBox.Text)
                    Else
                        WriteFloatpid(pid, &H3052F7C8, FoVTextBox.Text)
                    End If
                Else
                    WriteFloat("CoDMP", &H3029CA28, FoVTextBox.Text)
                End If
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
        hotkeydownn = GetAsyncKeyState(hotkeydown)
        hotkeyupp = GetAsyncKeyState(hotkeyup)

        If Not hotkeycomboup = 0 And Not hotkeycomboup = Nothing Then
            hotkeycomboupp = GetAsyncKeyState(hotkeycomboup)
            ishotkeyup = True
        End If
        If Not hotkeycombodown = 0 And Not hotkeycombodown = Nothing Then
            hotkeycombodownn = GetAsyncKeyState(hotkeycombodown)
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
    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles UpdateButton.Click

        Const quote As String = """"

        Dim test As String
        test = quote & "CoDUO FoV Changer Updater.exe" & quote

        Try
            Dim myExe As String = temp & "\CoDUO FoV Changer Updater.exe"
            If Not System.IO.File.Exists(myExe) Then
                System.IO.File.WriteAllBytes(myExe, My.Resources.CoDUO_FoV_Changer_Updater)
                Log.WriteLine("Creating Updater Application.")
            End If
            Process.Start(myExe)
            Log.WriteLine("Restarting/Updating")
            Application.Exit()
        Catch ex As Exception
            MsgBox("Unable to create updater application. Error: " & ex.Message, MsgBoxStyle.Critical)
            Log.WriteLine("Unable to create Updater Application. !! ERROR !! " & ex.Message)
            errorOccured = True
        End Try


    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs)
        Try
            Process.Start(appdata & "CoDUO FoV Changer\settings.ini")
            Log.WriteLine("User opened Config File.")
        Catch ex As Exception
            MsgBox("Config file not found, first time running? Error: " & ex.Message, MsgBoxStyle.Critical)
            Log.WriteLine("!! ERROR !! " & ex.Message)
            errorOccured = True
            '


        End Try
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

        'If Not My.Computer.FileSystem.FileExists("C:\Users\matt_\cod.dat") Then
        '    FogCheckBox.Checked = True
        '    FogCheckBox.Enabled = False
        '    FogCheckBox.Visible = False
        '    WriteInteger("CoDUOMP", &H9885F0, 1)
        '    Return
        'End If


        Dim ask5 As MsgBoxResult

        If didFS = True Then
            FogTimer.Start()
            WriteInteger("CoDUOMP", &H9885F0, 0)
            '   CheckBox1.Checked = False
            ini.WriteValue("Extras", "FogMSG", "DoNotAsk")
            ini.WriteValue("Extras", "Fog", "Disabled")
            Return
        End If

        If FogCheckBox.Checked = True Then
            FogTimer.Stop()
            WriteInteger("CoDUOMP", &H98861C, 1)
        End If
        If FogCheckBox.Checked = False Then
            If didFS = False Then
                ask5 = MessageBox.Show("Please note, you may be banned if you are caught using this, example: making a video and putting it on your clans site, or YouTube, PunkBuster also bans for this, do you want to continue?", "WARNING!", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            End If
            Log.WriteLine("Gave warning about Fog.")
            If ask5 = MsgBoxResult.No And didFS = False Then
                FogTimer.Stop()
                WriteInteger("CoDUOMP", &H98861C, 1)
                ' CheckBox1.Checked = True
                ini.WriteValue("Extras", "FogMSG", "Ask")
                ini.WriteValue("Extras", "Fog", "Enabled")
                Log.WriteLine("User did not continue.")
            ElseIf ask5 = MsgBoxResult.Yes And didFS = False Then
                FogTimer.Start()
                WriteInteger("CoDUOMP", &H98861C, 0)
                '   CheckBox1.Checked = False
                ini.WriteValue("Extras", "FogMSG", "DoNotAsk")
                ini.WriteValue("Extras", "Fog", "Disabled")
                Log.WriteLine("User continued.")
            End If
        End If

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles ChangeLogButton.Click
        getcl = New Thread(AddressOf Me.getChangelog)
        getcl.IsBackground = True
        getcl.Start()
        ChangeLogButton.Enabled = False
        System.Threading.Thread.Sleep(1750)
        ChangeLogButton.Enabled = True
    End Sub

    Private Sub Button9_Click_1(sender As Object, e As EventArgs)
        Try
            My.Computer.FileSystem.DeleteFile(appdata & "CoDUO FoV Changer\settings.ini")
            Log.WriteLine("Reset Config.")
        Catch ex As Exception
            MsgBox("Unable to reset, you may have already reset it. Error: " & ex.Message, MsgBoxStyle.Information)
            Log.WriteLine("!! ERROR !! Unable to reset Config. " & ex.Message)
            errorOccured = True
            '


        End Try
    End Sub

    Private Sub Timer11_Tick(sender As Object, e As EventArgs) Handles FogTimer.Tick
        Try
            If FogCheckBox.Checked = True Then
                WriteInteger("CoDUOMP", &H9885F0, 1)
            Else
                WriteInteger("CoDUOMP", &H9885F0, 0)
            End If
        Catch ex As Exception
            FogTimer.Stop()
            MsgBox("Failed to write to memory!" & ex.Message)
            Log.WriteLine("!! ERROR !! Failed to write to memory." & ex.Message)
            errorOccured = True
            '


        End Try
    End Sub

    Private Sub Button11_Click_1(sender As Object, e As EventArgs) Handles SettingsButton.Click
        SettingsForm.Show()
        Log.WriteLine("Showing Settings Form")
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs)
        Try
            Process.Start("explorer.exe", appdata & "CoDUO FoV Changer\")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Log.WriteLine("!! ERROR !! " & ex.Message)
            errorOccured = True
            '


        End Try
    End Sub

    Private Sub Button12_Click_1(sender As Object, e As EventArgs)
        AdvSettingsForm.Show()
    End Sub

    Private Sub Timer7_Tick(sender As Object, e As EventArgs) Handles FoVFixTimer.Tick
        '     If TextBox4.Height = 50 Then
        '        Label9.Location = New Point(0, 126)
        ' Label8.Location = New Point(0, 138)
        '  Else
        '        Label9.Location = New Point(0, 98)
        '     Label8.Location = New Point(0, 110)
        '  End If
        If FoVTextBox.Text = "" Then
            FoVTextBox.Text = CStr(My.Settings.FoVFix) 'FoVFix will never be below 80 or empty.
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs)
        Process.Start("https://googledrive.com/host/0B0nCag_Hp76zSzJ1MmVzYm1ONEk/coduofovchanger.html")
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs)
        Application.Restart()
    End Sub

    Private Sub showkey()
        keyfind = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!"))
        keyfind2 = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Activision\Call of Duty United Offensive", "CodKey", "CD-Key not found!"))
        keyfind3 = CStr(My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Activision\Call of Duty United Offensive", "Key", "CD-Key not found!"))


    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles CheckUpdatesLabel.Click
        checkthread = New Thread(AddressOf Me.Checkconnection)
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
            If Me.WindowState = FormWindowState.Minimized Then
                If MinimizeCheckBox.Checked = True Then
                    Me.Visible = False
                    MinimizeIcon.Visible = True
                    MinimizeIcon.BalloonTipIcon = ToolTipIcon.Info
                    MinimizeIcon.BalloonTipText = Application.ProductName & " is minimized. Double click to restore full-size."
                    MinimizeIcon.BalloonTipTitle = "Minimized to Tray"
                    MinimizeIcon.ShowBalloonTip(4300, "Minimized to Tray", Application.ProductName & " is minimized. Double click to restore full-size.", ToolTipIcon.Info)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Log.WriteLine("!! ERROR !! " & ex.Message)
            errorOccured = True
            '


        End Try
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles MinimizeIcon.MouseDoubleClick
        Try
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            MinimizeIcon.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
            Log.WriteLine("!! ERROR !! " & ex.Message)
            errorOccured = True
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
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        MinimizeIcon.Visible = False
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles ABITWTimer.Tick
        If LaunchParametersTB.Text.Contains("wedontneedno") Then
            LaunchParametersLB.Location = New Point(0, 126)
            LaunchParametersTB.Height = 80
            LaunchParametersTB.ScrollBars = ScrollBars.Vertical
            LaunchParametersTB.Text = ""
            System.Threading.Thread.Sleep(200)
            LaunchParametersTB.AppendText("We don't need no education.")
            System.Threading.Thread.Sleep(700)
            LaunchParametersTB.AppendText(newline & "We don't need no thought control.")
            System.Threading.Thread.Sleep(700)
            LaunchParametersTB.AppendText(newline & "No dark sarcasm in the classroom.")
            System.Threading.Thread.Sleep(650)
            LaunchParametersTB.AppendText(newline & "Hey! Teacher, leave them kids alone.")
            System.Threading.Thread.Sleep(700)
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

    Private Sub SendButton_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CoD1CheckBox.CheckedChanged
        If CoD1CheckBox.Checked = True Then
            ini.WriteValue("Main", "Game", "CoD1")
            DvarsCheckBox.Text = "Unlock All Dvars (UO only!)"
            StartGameButton.Enabled = False
            StartGameButton.Text = "Start Game (UO only!)"
            DvarsCheckBox.Enabled = False
            DvarsCheckBox.Checked = False
            FogCheckBox.Checked = True
            FogCheckBox.Enabled = False
            FogCheckBox.Text = "Fog (UO only!)"
            Me.Text = "CoDUO FoV Changer (in CoD1 Mode)"
            SettingsForm.ToolTipHandler.SetToolTip(UpdateButton, "Select the process for the FoV changer to write to.")
            '  Me.Width = 637
            If My.Computer.FileSystem.FileExists(cacheloc & "\cache6.cache") Then
                Dim iscorrupt As String = corruptCheck(cacheloc & "\cache6.cache", 8606)
                If iscorrupt <= 8605 Or iscorrupt >= 8607 Then
                    CoDPictureBox.Image = My.Resources.Loading
                    CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                    My.Computer.FileSystem.DeleteFile(cacheloc & "\cache6.cache")
                    Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                Else
                    CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache6.cache")
                End If

            Else
                Try
                    CoDPictureBox.Load("https://i.imgur.com/xhBcQSp.png")
                    Cache("https://i.imgur.com/xhBcQSp.png", "cache6.cache")
                Catch ex As Exception
                    '     MessageBox.Show("An error has occured while attempting to download an image or load a cached image: " & ex.Message)
                End Try
            End If
        Else
            ini.WriteValue("Main", "Game", "UO")
            DvarsCheckBox.Text = "Unlock All Dvars"
            StartGameButton.Enabled = True
            StartGameButton.Text = "Start Game"
            DvarsCheckBox.Enabled = True
            FogCheckBox.Enabled = True
            FogCheckBox.Text = "Fog"
            Me.Text = "CoDUO FoV Changer"
            'Me.Width = 624
            If My.Computer.FileSystem.FileExists(cacheloc & "\cache5.cache") Then
                Dim iscorrupt As String = corruptCheck(cacheloc & "\cache5.cache", 11846)
                If iscorrupt <= 11845 Or iscorrupt >= 11847 Then
                    CoDPictureBox.Image = My.Resources.Loading
                    CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                    My.Computer.FileSystem.DeleteFile(cacheloc & "\cache5.cache")
                    Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                Else
                    CoDPictureBox.Image = Image.FromFile(cacheloc & "\cache5.cache")
                End If

            Else
                Try
                    CoDPictureBox.Load("https://i.imgur.com/2WRGvTd.png")
                    Cache("https://i.imgur.com/2WRGvTd.png", "cache5.cache")
                Catch ex As Exception
                    ' MessageBox.Show("An error has occured while attempting to download an image or load a cached image: " & ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles DvarsCheckBox.CheckedChanged
        'If Not My.Computer.FileSystem.FileExists("C:\Users\matt_\cod.dat") Then
        '    DvarsCheckBox.Checked = False
        '    DvarsCheckBox.Enabled = False
        '    DvarsCheckBox.Visible = False
        '    Return
        'End If
        If DvarsCheckBox.Checked = True Then
            WriteInteger("CoDUOMP", &H43DD86, 235, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDA3, 235, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDC1, 235, nsize:=1)
        Else
            WriteInteger("CoDUOMP", &H43DD86, 116, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDA3, 116, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDC1, 116, nsize:=1)
        End If
    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles UpdateCheckTimer.Tick
        If checkthread.IsAlive = True Then
            checkthread.Abort()
        End If
        checkthread = New Thread(AddressOf Me.Checkconnection)
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
        AdvSettingsForm.Show()
        iscontext = True
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs)
        If checkupdates() = True Then
            SettingsButton.Enabled = True
        Else
            SettingsButton.Enabled = False
        End If
    End Sub


    Private Sub Timer9_Tick(sender As Object, e As EventArgs) Handles DvarUnlockerTimer.Tick
        'If Not My.Computer.FileSystem.FileExists("C:\Users\matt_\cod.dat") Then
        '    DvarsCheckBox.Checked = False
        '    DvarsCheckBox.Enabled = False
        '    DvarsCheckBox.Visible = False
        '    DvarUnlockerTimer.Stop()
        '    Return
        'End If
        If DvarsCheckBox.Checked = True Then
            WriteInteger("CoDUOMP", &H43DD86, 235, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDA3, 235, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDC1, 235, nsize:=1)
        Else
            WriteInteger("CoDUOMP", &H43DD86, 116, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDA3, 116, nsize:=1)
            WriteInteger("CoDUOMP", &H43DDC1, 116, nsize:=1)
        End If
    End Sub

    Private Sub Timer2_Tick_1(sender As Object, e As EventArgs) Handles CmdLineTimer.Tick
        If Not LaunchParametersTB.Text = "" And Not LaunchParametersTB.Text Is Nothing Then

            ini.WriteValue("Extras", "cmdline", LaunchParametersTB.Text)
        End If
    End Sub

    Private Sub HelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.Click

    End Sub
    Private Sub HelpToolStripMenuItem_Hover(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem.MouseHover
        '    HelpToolStripMenuItem.ShowDropDown()
        'not sure why but I had to do this, where as on "Tools", I didn't have to.
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
            FoVTextBox.Text = HackyFoVComboBox.SelectedItem.ToString
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
        '  For Each itemm In Form3.ComboBox2.Items
        '       itemlist1 = itemlist1 + 1
        '    Next
        If FoVHotKeyForm.CBBoxFoV.Items.Count >= 1 Then
            FoVHotKeyForm.CBBoxFoV.SelectedIndex = HackyFoVComboBox.SelectedIndex
        End If
        Try
            FoVTextBox.Text = HackyFoVComboBox.SelectedItem.ToString
        Catch ex As Exception
            Log.WriteLine("!! ERROR !! " & ex.Message)
            Return
        End Try

        Try
            If CoD1CheckBox.Checked = False Then
                If pid = 0 Then
                    WriteFloat("CoDUOMP", &H3052F7C8, FoVTextBox.Text)
                Else
                    WriteFloatpid(pid, &H3052F7C8, FoVTextBox.Text)
                End If
            Else
                WriteFloat("CoDMP", &H3029CA28, FoVTextBox.Text)
            End If
            If pid = 0 Then
                Dim MyP As Process() = Process.GetProcessesByName("CoDUOMP")
                If MyP.Length = 0 Then
                    StatusLabel.Text = ("Status: not found or failed to write to memory!")
                    If isminimal = "Dark" Then
                        StatusLabel.ForeColor = Color.DarkRed
                    Else
                        StatusLabel.ForeColor = Color.Red
                    End If
                    If Not CoD1CheckBox.Checked = True Then
                        StartGameButton.Enabled = True
                    Else
                        StartGameButton.Enabled = False
                    End If

                    Exit Sub
                Else
                    StatusLabel.Text = ("Status: UO found and wrote to memory!")
                    StatusLabel.ForeColor = Color.Green
                    StartGameButton.Enabled = False
                End If
            End If
        Catch ex As Exception
            '   MsgBox(ex.Message)
            Log.WriteLine("ERROR !!:  " & ex.Message)
            errorOccured = True
            '


        End Try

        ini.WriteValue("Main", "LastComboBoxFoV", FoVTextBox.Text)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If Not HackyFoVComboBox.Items.Contains(FoVTextBox.Text) Then
            HackyFoVComboBox.Items.Add(FoVTextBox.Text)
            HackyFoVComboBox.SelectedItem = FoVTextBox.Text
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        '       MessageBox.Show(ComboBox2.SelectedIndex)
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
End Class
Module ReadWritingMemory
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Integer, ByVal dwProcessId As Integer) As Integer

    Private Declare Function WriteProcessMemory1 Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Integer, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function WriteProcessMemory2 Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Single, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Single
    Private Declare Function WriteProcessMemory3 Lib "kernel32" Alias "WriteProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Long, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Long

    Private Declare Function ReadProcessMemory1 Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Integer, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function ReadProcessMemory2 Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Single, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Single
    Private Declare Function ReadProcessMemory3 Lib "kernel32" Alias "ReadProcessMemory" (ByVal hProcess As Integer, ByVal lpBaseAddress As Integer, ByRef lpBuffer As Long, ByVal nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Long

    Const PROCESS_ALL_ACCESS = &H1F0FF

    Public Function WriteDMAFloat(ByVal Process As String, ByVal Address As Integer, ByVal Offsets As Integer(), ByVal Value As Single, ByVal Level As Integer, Optional ByVal nsize As Integer = 4) As Boolean
        Try
            Dim lvl As Integer = Address
            For i As Integer = 1 To Level
                lvl = ReadFloat(Process, lvl, nsize) + Offsets(i - 1)
            Next
            WriteFloat(Process, lvl, Value, nsize)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub WriteNOPs(ByVal ProcessName As String, ByVal Address As Long, ByVal NOPNum As Integer)
        Dim C As Integer
        Dim B As Integer
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        If MyP.Length = 0 Then
            MainFoV.StatusLabel.Text = (ProcessName & " Status: not found or failed to write to memory!")
            MainFoV.StatusLabel.ForeColor = Color.Red
            Exit Sub
        End If
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
        If hProcess = IntPtr.Zero Then
            MessageBox.Show("Failed to open " & ProcessName & "!")
            Exit Sub
        End If

        B = 0
        For C = 1 To NOPNum
            Call WriteProcessMemory1(hProcess, Address + B, &H90, 1, 0&)
            B = B + 1
        Next C
    End Sub

    Public Sub WriteInteger(ByVal ProcessName As String, ByVal Address As Integer, ByVal Value As Integer, Optional ByVal nsize As Integer = 4)
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        If MyP.Length = 0 Then
            'MessageBox.Show(ProcessName & " isn't open!")
            Exit Sub
        End If
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
        If hProcess = IntPtr.Zero Then
            ' MessageBox.Show("Failed to open " & ProcessName & "!")
            Exit Sub
        End If

        Dim hAddress, vBuffer As Integer
        hAddress = Address
        vBuffer = Value
        WriteProcessMemory1(hProcess, hAddress, CInt(vBuffer), nsize, 0)
    End Sub
    Public Sub WriteFloatpid(ByVal ProcessID1 As Integer, ByVal Address As Integer, ByVal Value As Single, Optional ByVal nsize As Integer = 4)
        '  If ProcessName.EndsWith(".exe") Then
        'ProcessName = ProcessName.Replace(".exe", "")
        ' End If
        '   Dim MyP As Process = Process.GetProcessById(ProcessID1)
        '      If MyP.Length = 0 Then
        ' Form1.Label2.Text = (ProcessName & " Status: not found!")
        '       Exit Sub
        '     End If
        Dim processnamee As String = ""
        Try
            For Each p As Process In Process.GetProcesses()
                If p.ProcessName.Contains("CoD") Then
                    '     ListBox1.Items.Add(p.ProcessName & ".exe" & " (" & CStr(p.Id) & ")")
                    If p.Id = ProcessID1 Then
                        processnamee = p.ProcessName & " (" & p.Id & ")"
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Failed to get process list, reason: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, ProcessID1)
        If hProcess = IntPtr.Zero Then
            MessageBox.Show("Failed to open " & processnamee & "!")
            Exit Sub
        End If

        Dim hAddress As Integer
        Dim vBuffer As Single

        hAddress = Address
        vBuffer = Value
        WriteProcessMemory2(hProcess, hAddress, vBuffer, nsize, 0)
    End Sub

    Public Sub WriteFloat(ByVal ProcessName As String, ByVal Address As Integer, ByVal Value As Single, Optional ByVal nsize As Integer = 4)
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        If MyP.Length = 0 Then
            ' Form1.Label2.Text = (ProcessName & " Status: not found!")
            Exit Sub
        End If
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
        If hProcess = IntPtr.Zero Then
            MessageBox.Show("Failed to open " & ProcessName & "!")
            Exit Sub
        End If

        Dim hAddress As Integer
        Dim vBuffer As Single

        hAddress = Address
        vBuffer = Value
        WriteProcessMemory2(hProcess, hAddress, vBuffer, nsize, 0)
    End Sub

    Public Sub WriteLong(ByVal ProcessName As String, ByVal Address As Integer, ByVal Value As Long, Optional ByVal nsize As Integer = 4)
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        If MyP.Length = 0 Then
            MessageBox.Show(ProcessName & " isn't open!")
            Exit Sub
        End If
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
        If hProcess = IntPtr.Zero Then
            MessageBox.Show("Failed to open " & ProcessName & "!")
            Exit Sub
        End If

        Dim hAddress As Integer
        Dim vBuffer As Long

        hAddress = Address
        vBuffer = Value
        WriteProcessMemory3(hProcess, hAddress, vBuffer, nsize, 0)
    End Sub

    Public Function ReadFloat(ByVal ProcessName As String, ByVal Address As Integer, Optional ByVal nsize As Integer = 4) As Single
        If ProcessName.EndsWith(".exe") Then
            ProcessName = ProcessName.Replace(".exe", "")
        End If
        Dim MyP As Process() = Process.GetProcessesByName(ProcessName)
        If MyP.Length = 0 Then
            MessageBox.Show(ProcessName & " isn't open!")
            Return Nothing
            Exit Function
        End If
        Dim hProcess As IntPtr = OpenProcess(PROCESS_ALL_ACCESS, 0, MyP(0).Id)
        If hProcess = IntPtr.Zero Then
            MessageBox.Show("Failed to open " & ProcessName & "!")
            Return Nothing
            Exit Function
        End If

        Dim hAddress As Integer
        Dim vBuffer As Single

        hAddress = Address
        ReadProcessMemory2(hProcess, hAddress, vBuffer, nsize, 0)
        Return vBuffer
    End Function

End Module

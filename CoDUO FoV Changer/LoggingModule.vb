Option Strict On
Module LoggingModule
    Public userpth As String = System.Environment.GetEnvironmentVariable("userprofile")
    Dim rn As New Random
    'Public location As String = userpth & "\AppData\Roaming\CoDUO FoV Changer\Logs\ " & rn.Next(1, 65000)
    Dim sysdrv As String = System.Environment.GetEnvironmentVariable("systemdrive")
    '  Public wlog As New IO.StreamWriter(location, False)

    Public Class IniFile

        Private Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
            (ByVal lpApplicationName As String,
             ByVal lpKeyName As String,
             ByVal lpDefault As String,
             ByVal lpReturnedString As System.Text.StringBuilder,
             ByVal nSize As Integer,
             ByVal lpFileName As String) _
         As Integer

        Private Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" _
            (ByVal lpApplicationName As String,
             ByVal lpKeyName As String,
             ByVal lpString As String,
             ByVal lpFileName As String) _
        As Integer

        Public Property Path As String

        ''' <summary>
        ''' IniFile Constructor
        ''' </summary>
        ''' <param name="IniPath">The path to the INI file.</param>
        Public Sub New(ByVal IniPath As String)
            _Path = IniPath
        End Sub

        ''' <summary>
        ''' Read value from INI file
        ''' </summary>
        ''' <param name="section">The section of the file to look in</param>
        ''' <param name="key">The key in the section to look for</param>
        Public Function ReadValue(ByVal section As String, ByVal key As String) As String
            Dim sb As New System.Text.StringBuilder(255)
            Dim i = GetPrivateProfileString(section, key, "", sb, 255, Path)
            Return sb.ToString()
        End Function

        ''' <summary>
        ''' Write value to INI file
        ''' </summary>
        ''' <param name="section">The section of the file to write in</param>
        ''' <param name="key">The key in the section to write</param>
        ''' <param name="value">The value to write for the key</param>
        Public Sub WriteValue(ByVal section As String, ByVal key As String, ByVal value As String)
            WritePrivateProfileString(section, key, value, Path)
        End Sub

    End Class

End Module

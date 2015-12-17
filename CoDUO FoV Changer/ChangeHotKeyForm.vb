Option Strict On
Public Class ChangeHotKeyForm
    Dim ini As New IniFile(MainFoV.appdata & "CoDUO FoV Changer\settings.ini")
    Dim key As Integer = 0
    Dim key2 As Integer = 0
    Dim keycombo1 As Integer = 0
    Dim keycombo2 As Integer = 0
    Dim keyupini As String = ini.ReadValue("Extras", "HotKeyUp")
    Dim keydownini As String = ini.ReadValue("Extras", "HotKeyDown")
    Dim keyupinistr As String = ini.ReadValue("Extras", "HotKeyUpStr")
    Dim keydowninistr As String = ini.ReadValue("Extras", "HotKeyDownStr")
    Dim keyupcombostr As String = ini.ReadValue("Extras", "HotKeyUpComboStr")
    Dim keydowncombostr As String = ini.ReadValue("Extras", "HotKeyDownComboStr")
    Dim keyupcombo As String = ini.ReadValue("Extras", "HotKeyUpCombo")
    Dim keydowncombo As String = ini.ReadValue("Extras", "HotKeyDownCombo")
    Dim keystring As String
    <System.Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Function GetAsyncKeyState(ByVal vkey As Keys) As Short
    End Function
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SettingsForm.StyleCBox.SelectedItem.ToString = "Dark" Then
            BackColor = Color.DimGray
            CloseHotKeyFormButton.BackColor = Color.DarkGray
            ClearComboKeys.BackColor = Color.DarkGray
            ClearHotKeys.BackColor = Color.DarkGray
        End If
        UpHotKeyRB.Checked = True
        DownHotKeyRB.Checked = False
        If Not keyupini = "" And Not keyupini = Nothing Then
            key = CInt(keyupini)
        End If
        If Not keyupinistr = "" And Not keyupinistr = Nothing Then
            UpHotKeyLabel.Text = "Up Hot Key: " & keyupinistr
            If Not keyupcombostr = "" And Not keyupcombostr = Nothing Then
                UpHotKeyLabel.Text = "Up Hot Key: " & keyupcombostr & " + " & keyupinistr
                '  KeyComboUp.Text = keyupcombostr & " + " & keyupinistr & " (up hot key)"
            End If
        End If
        If Not keydownini = "" And Not keydownini = Nothing Then
            key2 = CInt(keydownini)
        End If
        If Not keydowninistr = "" And Not keydowninistr = Nothing Then
            DownHotKeyLabel.Text = "Down Hot Key: " & keydowninistr
            If Not keydowncombostr = "" And Not keydowncombostr = Nothing Then
                DownHotKeyLabel.Text = "Down Hot Key: " & keydowncombostr & " + " & keydowninistr
                '   KeyComboDown.Text = keydowncombostr & " + " & keydowninistr & " (down hot key)"
            End If
        End If

    End Sub
    Function UppercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToUpper(array(0))

        ' Return new string.
        Return New String(array)
    End Function
    Private Sub Form7_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp

        keystring = e.KeyCode.ToString.ToLower

        If keystring.Contains("lwin") Then
            Return
        End If


        If keystring.Contains("menu") Then
            keystring = keystring.Replace("menu", "ALT")
        End If
        If keystring.Contains("controlkey") Then
            keystring = keystring.Replace("controlkey", "Control")
        End If
        If keystring.Contains("shiftkey") Then
            keystring = keystring.Replace("shiftkey", "Shift")
        End If

        If keystring.Contains("oem") Then
            keystring = keystring.Replace("oem", "")
            keystring = UppercaseFirstLetter(keystring)
        End If

        If keystring = "openbrackets" Then
            keystring = "["
        End If

        If keystring.StartsWith("f1") Then
            keystring = UppercaseFirstLetter(keystring)
        End If

        For i As Integer = 2 To 9
            If keystring.StartsWith("f" & CStr(i)) Then
                keystring = UppercaseFirstLetter(keystring)
            End If
        Next

        If keystring.StartsWith("1") Then
            keystring = Strings.Replace(keystring, "1", ";", 1, 1)
        End If
        If keystring.StartsWith("7") Then
            keystring = Strings.Replace(keystring, "7", "'", 1, 1)
        End If
        If keystring.StartsWith("5") Then
            keystring = Strings.Replace(keystring, "5", "\", 1, 1)
        End If
        If keystring.StartsWith("6") Then
            keystring = Strings.Replace(keystring, "6", "]", 1, 1)
        End If

        If keystring.Contains("capital") Then
            keystring = keystring.Replace("capital", "CapsLock")
        End If
        If keystring.Contains("tab") Then
            keystring = "Tab"
        End If

        If keystring.Contains("brackets") Then
            keystring = keystring.Replace("Openbrackets", "[")
        End If





        '''''seperator'''''


        If UpHotKeyRB.Checked = True Then
            If e.KeyCode.ToString.ToLower = keydowninistr Then Return
            If keyupcombostr = "" Or keyupcombostr Is Nothing Then
                UpHotKeyLabel.Text = "Up Hot Key: " & keystring
                key = e.KeyCode
                ini.WriteValue("Extras", "HotKeyUpStr", keystring)
                keyupinistr = keystring
            Else
                UpHotKeyLabel.Text = "Up Hot Key: " & keyupcombostr & " + " & keystring
                key = e.KeyCode
                ini.WriteValue("Extras", "HotKeyUpStr", keystring)
                keyupinistr = keystring
            End If
        End If
        If DownHotKeyRB.Checked = True Then
            If e.KeyCode.ToString.ToLower = keyupinistr Then Return
            If keydowncombostr = "" Or keydowncombostr Is Nothing Then
                DownHotKeyLabel.Text = "Down Hot Key: " & keystring
                key2 = e.KeyCode
                ini.WriteValue("Extras", "HotKeyDownStr", keystring)
                keydowninistr = keystring
            Else
                DownHotKeyLabel.Text = "Down Hot Key: " & keydowncombostr & " + " & keystring
                key2 = e.KeyCode
                ini.WriteValue("Extras", "HotKeyDownStr", keystring)
                keydowninistr = keystring
            End If
        End If

        If MiscRBUp.Checked = True Then
            If keyupinistr = keystring Then Return
            If Not e.KeyCode = key And Not e.KeyCode.ToString.ToLower = "lwin" And Not e.KeyCode.ToString.ToLower = "rwin" Then
                UpHotKeyLabel.Text = "Up Hot Key: " & keystring & " + " & keyupinistr
                keycombo1 = e.KeyCode
                ini.WriteValue("Extras", "HotKeyUpComboStr", keystring)
                keyupcombostr = keystring
            End If
        End If

        If MiscRBDown.Checked = True Then
            If keydowninistr = keystring Then Return
            If Not e.KeyCode = key2 And Not e.KeyCode.ToString.ToLower = "lwin" And Not e.KeyCode.ToString.ToLower = "rwin" Then
                DownHotKeyLabel.Text = "Down Hot Key: " & keystring & " + " & keydowninistr
                keycombo2 = e.KeyCode
                ini.WriteValue("Extras", "HotKeyDownComboStr", keystring)
                keydowncombostr = keystring
            End If
        End If


        If e.KeyCode.ToString.StartsWith("D") And Not e.KeyCode.ToString = "D" And UpHotKeyRB.Checked = True Then
            UpHotKeyLabel.Text = UpHotKeyLabel.Text.Replace(e.KeyCode.ToString.ToLower, e.KeyCode.ToString.ToLower.Replace("d", ""))
        End If
        If e.KeyCode.ToString.StartsWith("D") And Not e.KeyCode.ToString = "D" And DownHotKeyRB.Checked = True Then
            DownHotKeyLabel.Text = DownHotKeyLabel.Text.Replace(e.KeyCode.ToString.ToLower, e.KeyCode.ToString.ToLower.Replace("d", ""))
        End If
        If UpHotKeyLabel.Text.Contains(" + d") And Not e.KeyCode.ToString = "D" Then
            UpHotKeyLabel.Text = UpHotKeyLabel.Text.Replace(" + d ", " + ")
        End If
        If DownHotKeyLabel.Text.Contains(" + d") And Not e.KeyCode.ToString = "D" Then
            DownHotKeyLabel.Text = DownHotKeyLabel.Text.Replace(" + d ", " + ")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles CloseHotKeyFormButton.Click

        If Not key = 0 Then
            ini.WriteValue("Extras", "HotKeyUp", CStr(key))
            MainFoV.hotkeyup = key
        End If
        If Not key2 = 0 Then
            ini.WriteValue("Extras", "HotKeyDown", CStr(key2))
            MainFoV.hotkeydown = key2
        End If
        If Not keycombo1 = 0 Then
            ini.WriteValue("Extras", "HotKeyUpCombo", CStr(keycombo1))
        End If
        If Not keycombo2 = 0 Then
            ini.WriteValue("Extras", "HotKeyDownCombo", CStr(keycombo2))
        End If
        FoVHotKeyForm.Show()
        Close()
    End Sub

    Private Sub ClearHotKeys_Click(sender As Object, e As EventArgs) Handles ClearHotKeys.Click
        ini.WriteValue("Extras", "HotKeyUp", "")
        ini.WriteValue("Extras", "HotKeyDown", "")
        ini.WriteValue("Extras", "HotKeyUpStr", "")
        ini.WriteValue("Extras", "HotKeyDownStr", "")

        keydowninistr = ""
        keyupinistr = ""
        key = 0
        key2 = 0

        MainFoV.hotkeyup = 0
        MainFoV.hotkeydown = 0

        UpHotKeyLabel.Text = "Up Hot Key: "
        DownHotKeyLabel.Text = "Down Hot Key: "
    End Sub

    Private Sub ClearComboKeys_Click(sender As Object, e As EventArgs) Handles ClearComboKeys.Click
        ini.WriteValue("Extras", "HotKeyUpCombo", "")
        ini.WriteValue("Extras", "HotKeyDownCombo", "")
        ini.WriteValue("Extras", "HotKeyUpComboStr", "")
        ini.WriteValue("Extras", "HotKeyDownComboStr", "")


        keycombo1 = 0
        keycombo2 = 0
        MainFoV.hotkeycomboup = 0
        MainFoV.hotkeycombodown = 0
        If Not UpHotKeyLabel.Text = "Up Hot Key: " Then
            UpHotKeyLabel.Text = UpHotKeyLabel.Text.Replace(keyupcombostr & " + ", "")
        End If
        If Not DownHotKeyLabel.Text = "Down Hot Key: " Then
            DownHotKeyLabel.Text = DownHotKeyLabel.Text.Replace(keydowncombostr & " + ", "")
        End If
        keydowncombostr = ""
        keydowncombo = ""
        keyupcombostr = ""
        keyupcombo = ""
    End Sub
End Class
Public Class ChangeHotKeyForm
    Dim ini As New IniFile(MainFoV.appdata & "CoDUO FoV Changer\settings.ini")
    Dim key = 0
    Dim key2 = 0
    Dim keyupini As String = ini.ReadValue("Extras", "HotKeyUp")
    Dim keydownini As String = ini.ReadValue("Extras", "HotKeyDown")
    Dim keyupinistr As String = ini.ReadValue("Extras", "HotKeyUpStr")
    Dim keydowninistr As String = ini.ReadValue("Extras", "HotKeyDownStr")
    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function GetAsyncKeyState(ByVal vkey As System.Windows.Forms.Keys) As Short
    End Function
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SettingsForm.StyleCBox.SelectedItem.ToString = "Dark" Then
            Me.BackColor = Color.DimGray
            CloseHotKeyFormButton.BackColor = Color.DarkGray
        End If
        UpHotKeyRB.Checked = True
        DownHotKeyRB.Checked = False
        If Not keyupini = "" And Not keyupini = Nothing Then
            key = keyupini
        End If
        If Not keyupinistr = "" And Not keyupinistr = Nothing Then
            UpHotKeyLabel.Text = "Up Hot Key: " & keyupinistr
        End If
        If Not keydownini = "" And Not keydownini = Nothing Then
            key2 = keydownini
        End If
        If Not keydowninistr = "" And Not keydowninistr = Nothing Then
            DownHotKeyLabel.Text = "Down Hot Key: " & keydowninistr
        End If
    End Sub
    Private Sub Form7_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If UpHotKeyRB.Checked = True Then
            UpHotKeyLabel.Text = "Up Hot Key: " & e.KeyCode.ToString.ToLower
            If UpHotKeyLabel.Text.Contains("menu") Then
                UpHotKeyLabel.Text = UpHotKeyLabel.Text.Replace("menu", "ALT")
            End If
            If UpHotKeyLabel.Text.Contains("controlkey") Then
                UpHotKeyLabel.Text = UpHotKeyLabel.Text.Replace("controlkey", "Control")
            End If
            If UpHotKeyLabel.Text.Contains("shiftkey") Then
                UpHotKeyLabel.Text = UpHotKeyLabel.Text.Replace("shiftkey", "Shift")
            End If

            key = e.KeyCode
            ini.WriteValue("Extras", "HotKeyUpStr", e.KeyCode.ToString.ToLower)
        End If
        If DownHotKeyRB.Checked = True Then
            DownHotKeyLabel.Text = "Down Hot Key: " & e.KeyCode.ToString.ToLower
            If DownHotKeyLabel.Text.Contains("menu") Then
                DownHotKeyLabel.Text = DownHotKeyLabel.Text.Replace("menu", "ALT")
            End If
            If DownHotKeyLabel.Text.Contains("controlkey") Then
                DownHotKeyLabel.Text = DownHotKeyLabel.Text.Replace("controlkey", "Control")
            End If
            If DownHotKeyLabel.Text.Contains("shiftkey") Then
                DownHotKeyLabel.Text = DownHotKeyLabel.Text.Replace("shiftkey", "Shift")
            End If

            key2 = e.KeyCode
            ini.WriteValue("Extras", "HotKeyDownStr", e.KeyCode.ToString.ToLower)
        End If
        If UpHotKeyLabel.Text.Contains("d") And Not e.KeyCode.ToString = "D" And Not UpHotKeyLabel.Text.Contains("numpa") Then
            UpHotKeyLabel.Text = "Up Hot Key: " & e.KeyCode.ToString.ToLower.Replace("d", "")
        End If
        If DownHotKeyLabel.Text.Contains("d") And Not e.KeyCode.ToString = "D" And Not DownHotKeyLabel.Text.Contains("numpa") Then
            DownHotKeyLabel.Text = "Up Hot Key: " & e.KeyCode.ToString.ToLower.Replace("d", "")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles CloseHotKeyFormButton.Click
        MainFoV.hotkeyup = key
        MainFoV.hotkeydown = key2
        ini.WriteValue("Extras", "HotKeyUp", key)
        ini.WriteValue("Extras", "HotKeyDown", key2)
        FoVHotKeyForm.Show()
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


    End Sub
End Class
Option Strict On
Public Class FoVHotKeyForm
    Dim ini As New IniFile(MainFoV.appdata & "CoDUO FoV Changer\settings.ini")
    Private Sub ButtonAddFoVCB_Click(sender As Object, e As EventArgs) Handles ButtonAddFoVCB.Click
        Try
            If CBBoxFoV.Items.Count + 1 >= 13 Then
                MessageBox.Show("Unable to add FoV Value because it exceeds the max amount of 12.", MainFoV.appnamevers, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            If Not CBBoxFoV.Items.Contains(MainFoV.FoVNumeric.Text) Then
                CBBoxFoV.Items.Add(MainFoV.FoVNumeric.Text)
                CBBoxFoV.SelectedItem = MainFoV.FoVNumeric.Text
            End If
            If Not MainFoV.HackyFoVComboBox.Items.Contains(MainFoV.FoVNumeric.Text) Then
                MainFoV.HackyFoVComboBox.Items.Add(MainFoV.FoVNumeric.Text)
                MainFoV.HackyFoVComboBox.SelectedItem = CBBoxFoV.SelectedItem
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub CBBoxFoV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBBoxFoV.SelectedIndexChanged
        MainFoV.FoVNumeric.Text = CBBoxFoV.SelectedItem.ToString



    End Sub

    Private Sub RemoveFoVCBBox_Click(sender As Object, e As EventArgs) Handles RemoveFoVCBBox.Click
        If Not CBBoxFoV.SelectedIndex < 0 Then
            Dim replace As String
            replace = MainFoV.fovbox.Replace(CBBoxFoV.SelectedItem.ToString & ",", "")
            CBBoxFoV.Items.Remove(CBBoxFoV.SelectedItem)
            CBBoxFoV.Text = ""
            ini.WriteValue("Main", "ComboBoxFoV", replace)
        End If
        If Not CBBoxFoV.Items.Count <= 0 Then
            CBBoxFoV.SelectedIndex = 0
        End If
    End Sub

    Private Sub FoVHotKeyShowForm_Click(sender As Object, e As EventArgs) Handles FoVHotKeyShowForm.Click
        ChangeHotKeyForm.Show()
        Hide()
    End Sub

    Private Sub CloseFormButton_Click(sender As Object, e As EventArgs) Handles CloseFormButton.Click
        MainFoV.HackyFoVComboBox.SelectedIndex = CBBoxFoV.SelectedIndex
        SettingsForm.Show()
        Close()
    End Sub

    Private Sub FoVHotKeyForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If SettingsForm.StyleCBox.SelectedItem.ToString = "Default" Then
            BackColor = Color.DimGray
            ButtonAddFoVCB.BackColor = Color.DarkGray
            CloseFormButton.BackColor = Color.DarkGray
            FoVHotKeyShowForm.BackColor = Color.DarkGray
            CBBoxFoV.BackColor = Color.DarkGray
            RemoveFoVCBBox.BackColor = Color.DarkGray
        End If
        CBBoxFoV.DropDownStyle = ComboBoxStyle.DropDownList
        Dim splitStrr() As String
        If MainFoV.fovbox.Contains(",") Then
            splitStrr = MainFoV.fovbox.Split(CType(",", Char()))
            For Each word In splitStrr
                If Not word = "" Then
                    '               MessageBox.Show(word)
                    If Not CInt(word) >= 121 Then
                        CBBoxFoV.Items.Add(word)
                    Else
                        '   Log.WriteLine(word & " is higher than 120 fov (max) will not add to combobox")
                    End If
                End If
            Next
        End If
        Dim itemlist As Integer = 0
        For Each item In CBBoxFoV.Items
            itemlist = itemlist + 1
        Next
        If CBBoxFoV.Items.Count >= 1 Then
            For Each item As String In CBBoxFoV.Items
                If item = MainFoV.FoVNumeric.Text Then
                    CBBoxFoV.SelectedItem = MainFoV.FoVNumeric.Text
                    Return
                End If
            Next
        End If
        If itemlist >= 1 Then
            CBBoxFoV.SelectedIndex = 0
        End If
    End Sub
End Class
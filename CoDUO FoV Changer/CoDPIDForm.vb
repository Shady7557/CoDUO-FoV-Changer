Public Class CoDPIDForm

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SelectPIDButton.Click
        If PIDListBox.SelectedIndex < 0 Then
            MessageBox.Show("No process selected!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Dim splitStr() As String
        Dim pid As String
        Dim pidInt As Integer
        pid = PIDListBox.SelectedItem.ToString
        splitStr = pid.Split("(")
        pid = splitStr(1).Replace(")", "")
        pidInt = CInt(pid)
        MainFoV.pid = pidInt
        Hide()
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If SettingsForm.StyleCBox.SelectedItem = "Dark" Then
                BackColor = Color.DimGray
                PIDListBox.BackColor = Color.DarkGray
                SelectPIDButton.BackColor = Color.DarkGray
                ClosePIDListButton.BackColor = Color.DarkGray
            End If
            For Each p As Process In Process.GetProcesses()
                If p.ProcessName.Contains("CoDUOMP") Or p.ProcessName.Contains("CoDMP") Then
                    PIDListBox.Items.Add(p.ProcessName & ".exe" & " (" & CStr(p.Id) & ")")
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Failed to get process list, reason: " & ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        If PIDListBox.Items.Count <= 0 Then
            MessageBox.Show("CoD/UO is not running or the process could not be found! (process must contain or equal ""CoDUOMP"", or ""CoDMP"")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ClosePIDListButton.Click
        Hide()
    End Sub
End Class
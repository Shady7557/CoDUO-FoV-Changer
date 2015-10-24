Imports System.Threading
Imports System.Net


Public Class Form8
    Dim thread As System.Threading.Thread
    Dim pbload As Thread
    Dim imagename As String
    Public appdata As String = System.Environment.GetEnvironmentVariable("appdata") & "\"
    Dim cacheloc As String = appdata & "CoDUO FoV Changer\Cache"
    Private WithEvents httpclient As WebClient
    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pbload = New Thread(AddressOf Me.pbloadd)
        pbload.Priority = ThreadPriority.AboveNormal
        pbload.Start()
        Button1.Enabled = False
        Me.Text = "Picture Comparisons"
        Me.ShowIcon = False
        If Not My.Computer.FileSystem.DirectoryExists(cacheloc) Then
            Label1.Visible = True
            My.Computer.FileSystem.CreateDirectory(cacheloc)
        Else
            Label1.Visible = False
        End If
        If Not My.Computer.FileSystem.FileExists(cacheloc & "\cache1.cache") Then
            Label1.Visible = True
        End If
    End Sub
    Private Sub Form8_Close(sender As Object, e As EventArgs) Handles MyBase.FormClosing

    End Sub
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property
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

    Private Sub pbloadd()
        If Not My.Computer.FileSystem.FileExists(cacheloc & "\cache1.cache") Then
            PictureBox1.Load("https://i.imgur.com/vFffRTd.jpg")
            imagename = "110"
            'start cache
            Cache("https://i.imgur.com/vFffRTd.jpg", "cache1.cache")
            'end cache
        Else
            If My.Computer.FileSystem.FileExists(cacheloc & "\cache1.cache") Then
                Dim iscorrupt As String = corruptCheck(cacheloc & "\cache1.cache", 134941)
                If iscorrupt <= 134940 Or iscorrupt >= 134942 Then
                    PictureBox1.Image = My.Resources.Loading
                    PictureBox1.Load("https://i.imgur.com/vFffRTd.jpg")
                    My.Computer.FileSystem.DeleteFile(cacheloc & "\cache1.cache")
                    Cache("https://i.imgur.com/vFffRTd.jpg", "cache1.cache")
                Else
                    PictureBox1.Image = Image.FromFile(cacheloc & "\cache1.cache")
                End If
            End If
            End If
    End Sub
    Private Function corruptCheck(filename As String, bytes As Long)
        Dim infoReader As System.IO.FileInfo = _
My.Computer.FileSystem.GetFileInfo(filename)
        ' MessageBox.Show(infoReader.Length)
        Return infoReader.Length
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If My.Computer.FileSystem.FileExists(cacheloc & "\cache1.cache") Then
            Dim iscorrupt As String = corruptCheck(cacheloc & "\cache1.cache", 134941)
            If iscorrupt <= 134940 Or iscorrupt >= 134942 Then
                PictureBox1.Image = My.Resources.Loading
                PictureBox1.Load("https://i.imgur.com/vFffRTd.jpg")
                My.Computer.FileSystem.DeleteFile(cacheloc & "\cache1.cache")
                Cache("https://i.imgur.com/vFffRTd.jpg", "cache1.cache")
            Else
                PictureBox1.Image = Image.FromFile(cacheloc & "\cache1.cache")
            End If

        Else
            PictureBox1.Image = My.Resources.Loading
            PictureBox1.Load("https://i.imgur.com/vFffRTd.jpg")
            Cache("https://i.imgur.com/vFffRTd.jpg", "cache1.cache")
            'this should already be cached on startup
        End If
        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        imagename = "110"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If My.Computer.FileSystem.FileExists(cacheloc & "\cache2.cache") Then
            Dim iscorrupt As String = corruptCheck(cacheloc & "\cache2.cache", 130208)
            If iscorrupt <= 130207 Or iscorrupt >= 130209 Then
                PictureBox1.Image = My.Resources.Loading
                PictureBox1.Load("https://i.imgur.com/xPsVnwx.jpg")
                My.Computer.FileSystem.DeleteFile(cacheloc & "\cache2.cache")
                Cache("https://i.imgur.com/xPsVnwx.jpg", "cache2.cache")
            Else
                PictureBox1.Image = Image.FromFile(cacheloc & "\cache2.cache")
            End If
        Else
            PictureBox1.Load("https://i.imgur.com/xPsVnwx.jpg")
            Cache("https://i.imgur.com/xPsVnwx.jpg", "cache2.cache")
        End If
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = True
        imagename = "100"

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If My.Computer.FileSystem.FileExists(cacheloc & "\cache3.cache") Then
            Dim iscorrupt As String = corruptCheck(cacheloc & "\cache3.cache", 127674)
            If iscorrupt <= 127673 Or iscorrupt >= 127675 Then
                PictureBox1.Image = My.Resources.Loading
                PictureBox1.Load("https://i.imgur.com/V2mU2Mh.jpg")
                My.Computer.FileSystem.DeleteFile(cacheloc & "\cache3.cache")
                Cache("https://i.imgur.com/V2mU2Mh.jpg", "cache3.cache")
            Else
                PictureBox1.Image = Image.FromFile(cacheloc & "\cache3.cache")
            End If

        Else
            PictureBox1.Load("https://i.imgur.com/V2mU2Mh.jpg")
            Cache("https://i.imgur.com/V2mU2Mh.jpg", "cache3.cache")
        End If
        Button3.Enabled = False
        Button4.Enabled = True
        Button1.Enabled = True
        Button2.Enabled = True
        imagename = "90"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If My.Computer.FileSystem.FileExists(cacheloc & "\cache4.cache") Then
            Dim iscorrupt As String = corruptCheck(cacheloc & "\cache4.cache", 123852)
            If iscorrupt <= 123851 Or iscorrupt >= 123853 Then
                PictureBox1.Image = My.Resources.Loading
                PictureBox1.Load("https://i.imgur.com/9gpFQma.jpg")
                My.Computer.FileSystem.DeleteFile(cacheloc & "\cache4.cache")
                Cache("https://i.imgur.com/9gpFQma.jpg", "cache4.cache")
            Else
                PictureBox1.Image = Image.FromFile(cacheloc & "\cache4.cache")
            End If

        Else
            PictureBox1.Load("https://i.imgur.com/9gpFQma.jpg")
            Cache("https://i.imgur.com/9gpFQma.jpg", "cache4.cache")
        End If
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = False
        imagename = "80"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Visible = False
        'form isn't shutdown because this causes the pictures to reload, and there's no need.
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        GC.Collect()
    End Sub
End Class
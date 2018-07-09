Imports System.Drawing
Imports System.Drawing.Imaging
Imports System
Imports System.IO
Imports System.Collections

Public Class Form1
    'Barely any comments sorry, this was based on https://www.codeproject.com/Articles/12789/Merging-Images-in-NET
    Dim img As Image
    Dim strWatermark As String
    Dim strFolder As String
    Dim strOut As String
    Dim x As Long
    Dim y As Long

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Select a watermark
        OpenFileDialog1.ShowDialog()
        If (OpenFileDialog1.FileName IsNot "") Then
            strWatermark = OpenFileDialog1.FileName
        End If

        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Select input folder
        FolderBrowserDialog1.ShowDialog()
        If (FolderBrowserDialog1.SelectedPath IsNot "") Then
            strFolder = FolderBrowserDialog1.SelectedPath
        End If

        Button4.Enabled = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'GO!
        If (strWatermark IsNot "" And strFolder IsNot "" And strOut IsNot "") Then
            img = Image.FromFile(strWatermark)

            Dim inputImgs As String() = Directory.GetFiles(strFolder)

            Dim filename As String
            For Each filename In inputImgs
                Dim inImg As Image
                inImg = Image.FromFile(filename)

                Dim g As Graphics
                g = Graphics.FromImage(inImg)

                Dim actX As Long
                Dim actY As Long

                actX = x
                actY = y

                If (CheckBox1.Checked) Then
                    'Actually we do a percentage
                    actX = Math.Round(inImg.Width * (x / 100))
                    actY = Math.Round(inImg.Height * (y / 100))
                End If

                g.DrawImage(img, New Point(actX, actY))

                Dim newOut As String

                newOut = strOut + "\" + Path.GetFileName(filename)

                If (filename.ToUpper.Contains("JPG") Or filename.ToUpper.Contains("JPEG")) Then
                    'JPEG format
                    inImg.Save(newOut, ImageFormat.Jpeg)
                End If

                If (filename.ToUpper.Contains("PNG")) Then
                    'PNG format
                    inImg.Save(newOut, ImageFormat.Png)
                End If

                If (filename.ToUpper.Contains("GIF")) Then
                    'GIF format
                    inImg.Save(newOut, ImageFormat.Gif)
                End If

            Next

            MessageBox.Show("DONE! :D")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'select our output folder
        FolderBrowserDialog1.ShowDialog()

        If (FolderBrowserDialog1.SelectedPath IsNot "") Then
            strOut = FolderBrowserDialog1.SelectedPath
        End If

        Button3.Enabled = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'X
        Try
            x = TextBox1.Text
        Catch ex As Exception
            TextBox1.Text = "0"
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        'Y
        Try
            y = TextBox2.Text
        Catch ex As Exception
            TextBox2.Text = "0"
        End Try

    End Sub
End Class

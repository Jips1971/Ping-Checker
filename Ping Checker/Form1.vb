Imports System.CodeDom
Imports System.Environment
Imports System.IO
Imports System.Timers

Public Class Form1
    Dim SERVER As String
    Dim counter As Integer
    Dim UserDesktop As String
    Dim strFile As String = "C:\TEMP\PingLog_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
    Dim sw As StreamWriter
    REM Dim fs As FileStream = Nothing

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.WhiteSmoke
        Timer1.Interval = 1000

        Button1.Enabled = True
        Button2.Enabled = False



        SERVER = ""
        TextBox1.Text = SERVER
        Label1.Text = "Enter Ping Address Below"
        Label2.Text = ""
        Label3.Text = ""
        Label4.Text = ""
        UserDesktop = Environment.GetFolderPath(SpecialFolder.DesktopDirectory)
        Label5.Text = "Log File Path"
        Label6.Text = "\Desktop" & "\PingLog_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt"
        strFile = (UserDesktop & "\PingLog_" & DateTime.Today.ToString("dd-MMM-yyyy") & ".txt")
        CheckorCreateFile()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False


        If TextBox1.Text = "" Then
            MsgBox("Duh!!  OOps, Fill in the box first")
            Button1.Enabled = True
            StopAll()
        Else
            Button2.Enabled = True
            Timer1.Start()
            SERVER = TextBox1.Text
        End If


    End Sub







    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        counter = counter + 1
        Label3.Text = counter
        Try
            If My.Computer.Network.Ping(SERVER) Then
                Me.BackColor = Color.WhiteSmoke
                Label2.ForeColor = Color.Green
                Label2.Text = ("Ping Successful to " & SERVER)
                Label4.Text = (System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")) & " - Success"
                AppendLog()
            Else
                Label2.Text = ("Ping Failed to " & SERVER)

            End If
        Catch ex As Exception
            Label2.ForeColor = Color.Black
            Me.BackColor = Color.Red
            Label4.Text = (System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")) & " - Failed"
            Label2.Text = ("Failed")
            AppendLog()

        End Try
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2.Enabled = False
        Button1.Enabled = True
        StopAll()
    End Sub


    Private Sub StopAll()
        Timer1.Stop()
        Label3.Text = ""
        Label2.Text = ""
        Me.BackColor = Color.WhiteSmoke
        Label2.ForeColor = Color.Black
        Label4.Text = ""
        counter = 0

    End Sub






    Private Sub CheckorCreateFile()
        If Not System.IO.File.Exists(strFile) Then
            System.IO.File.Create(strFile).Dispose()
        End If

    End Sub

    Private Sub AppendLog()

        sw = File.AppendText(strFile)
        sw.WriteLine(SERVER & " - " & Label4.Text)
        sw.Close()
    End Sub




End Class

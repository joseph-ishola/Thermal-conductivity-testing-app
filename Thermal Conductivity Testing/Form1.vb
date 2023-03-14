Imports System.Threading

Public Class Form1
    Delegate Sub SetTextCallback(ByVal [text] As String) 'Added to prevent threading errors during receiveing of data
    Dim port As String
    Dim RXstring As String
    Dim Q, Conductiv, temp_Hot, temp_Cold, length, Dia, Area, Prev_Time, sum, counter, power, temp_diff As Single

    Dim splitted() As String
    

    Const Res As Single = 136.8

    Const pi As Single = 22 / 7
    Const c As Single = 0.022602
    Dim time


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click




        If (Button2.Text = "Disconnect") And SerialPort1.IsOpen() Then
            SerialPort1.Close()
            Button2.Text = "Connect"
            time = 0
            Prev_Time = 0
            Timer1.Enabled = False
            Label4.Text = "Computed K (W/m K)= " & sum / counter



        ElseIf (Button2.Text = "Connect") And Not (SerialPort1.IsOpen()) Then

            SerialPort1.Open()
            Button2.Text = "Disconnect"

            sum = 0
            counter = 0
        End If




    End Sub

  
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim myPort As Array
        Dim pattern As String
        Dim vid As String
        Dim pid As String
        vid = "0023"
        pid = "002"
        ' Dim writer As System.IO.StreamWriter
        'writer = System.IO.File.AppendText("Thermal Conductivity Data.csv")
        myPort = IO.Ports.SerialPort.GetPortNames



        ComboBox1.Items.AddRange(myPort)

        pattern = String.Format("^VID_{0}.PID_{1}", vid, pid)

        FileOpen(1, "Thermal Conductivity Data.csv", OpenMode.Output)



        port = InputBox("Enter device Port", "Com Port", "COM72")
        SerialPort1.PortName = port



        header()

        'writer.Write("Program to obtain the thermal conductivity of a material" & vbNewLine)
        'writer.Write(vbTab & vbTab & Now() & vbNewLine)


        'Compute the power

        ' writer.Write("Temperature" & "," & " Thermal Conductivity" & "," & "Time" & vbNewLine)
        'Label3.Text = "Power =" & power & "W"



    End Sub
    Public Sub header()

        Me.ListBox1.Items.Add(vbTab & vbTab & vbTab & "Program to obtain the thermal conductivity of a material" & vbNewLine)

        Print(1, "Program to obtain the thermal conductivity of a material" & vbNewLine)

        Me.ListBox1.Items.Add(vbTab & vbTab & vbTab & vbTab & vbTab & Now() & vbNewLine)

        Print(1, vbTab & vbTab & Now() & vbNewLine)

        Me.ListBox1.Items.Add("___________________________________________________________________________________________________________________")
        Me.ListBox1.Items.Add("Hot Temp" & vbTab & "Cold Temp" & vbTab & "Temp. Difference" & vbTab & " Thermal Conductivity (W/m K)" & vbTab & "Time")
        Me.ListBox1.Items.Add("____________________________________________________________________________________________________________________")

        Print(1, "Hot Temp" & "," & "Cold Temp" & "," & "Temp. Difference" & "," & " Thermal Conductivity (W/m K)" & "," & "Time")

    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As System.Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadLine())
    End Sub

    Private Sub ReceivedText(ByVal [text] As String) 'input from ReadExisting
        If Me.ListBox1.InvokeRequired Then
            Dim x As New SetTextCallback(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else

            splitted = Split(text, ";")
            temp_Hot = Val(splitted(0))
            temp_Cold = Val(splitted(1))
            Q = power
            temp_diff = (temp_Hot - temp_Cold)
            Conductiv = ((Q * length) / (Area * (temp_diff))) * c

            sum += Conductiv

            counter += 1

            Me.ListBox1.Items.Add(temp_Hot & vbTab & temp_Cold & vbTab & temp_diff & vbTab & vbTab & vbTab & Conductiv & vbTab & vbTab & time & vbNewLine)  'append text

            Print(1, (temp_Hot & "," & temp_Cold & "," & temp_diff & "," & Conductiv & "," & time & vbNewLine))
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ListBox1.Items.Clear()
        Label4.Text = ""
        header()
        Button2.Enabled = False

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If IsNumeric(TextBox1.Text) And IsNumeric(TextBox2.Text) And IsNumeric(TextBox3.Text) Then
            Button2.Enabled = True
            length = TextBox1.Text
            Dia = TextBox2.Text
            power = (Val(TextBox3.Text) ^ 2) / Res
            Area = pi * ((Dia / 2) ^ 2)
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If IsNumeric(TextBox1.Text) And IsNumeric(TextBox2.Text) And IsNumeric(TextBox3.Text) Then
            Button2.Enabled = True

            length = TextBox1.Text
            Dia = TextBox2.Text
            power = (Val(TextBox3.Text) ^ 2) / Res
            Area = pi * ((Dia / 2) ^ 2)
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If IsNumeric(TextBox1.Text) And IsNumeric(TextBox2.Text) And IsNumeric(TextBox3.Text) Then
            Button2.Enabled = True
            length = TextBox1.Text
            Dia = TextBox2.Text
            power = (Val(TextBox3.Text) ^ 2) / Res
            Area = pi * ((Dia / 2) ^ 2)
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        time += 1
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("This program was designed to compute the thermal conductivity of material....", vbInformation + vbOKOnly, "About:: Thermal conductivity")
    End Sub
End Class

Imports System.Text
Imports MySql.Data.MySqlClient

Public Class teachod
    ' Assuming these controls are already added to your form: Label5, ComboBox1, Label1, DateTimePicker1, Button1

    ' This should be replaced with your actual connection string.
    Dim connectionString As String = " Server=127.0.0.1;Database=kjclab;User Id=root;Password=;"

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        hidecontrols()
    End Sub

    Private Sub btnAddUsers_Click(sender As Object, e As EventArgs) Handles btnAddUsers.Click
        ' Show the controls
        ShowControls()
        ' Populate the ComboBox with lab names
        PopulateComboBoxWithLabNames()
    End Sub

    Private Sub hidecontrols()
        Label5.Visible = False
        ComboBox1.Visible = False
        Label1.Visible = False
        DateTimePicker1.Visible = False
        Button1.Visible = False
        ComboBox2.Visible = False
    End Sub

    Private Sub ShowControls()
        Label5.Visible = True
        ComboBox1.Visible = True
        Label1.Visible = True
        DateTimePicker1.Visible = True
        Button1.Visible = True
        ComboBox2.Visible = True

    End Sub
    Private Sub PopulateComboBoxWithLabNames()
        ComboBox1.Items.Clear()
        Dim labNames As List(Of String) = GetLabNamesFromDatabase()
        For Each labName In labNames
            ComboBox1.Items.Add(labName)
        Next
        If ComboBox1.Items.Count > 0 Then ComboBox1.SelectedIndex = 0 ' Select the first item by default
    End Sub
    Private Function GetLabNamesFromDatabase() As List(Of String)
        Dim labNames As New List(Of String)()
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand("SELECT LabName FROM lab", conn)
                Try
                    conn.Open()
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            labNames.Add(Convert.ToString(reader("LabName")))
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message)
                End Try
            End Using
        End Using
        Return labNames
    End Function
    Private Function GetLabIdByName(labName As String) As Integer
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand("SELECT LabId FROM lab WHERE LabName = @LabName", conn)
                cmd.Parameters.AddWithValue("@LabName", labName)
                Try
                    conn.Open()
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        Return Convert.ToInt32(result)
                    Else
                        Throw New ArgumentException("Lab not found.")
                    End If
                Catch ex As MySqlException
                    MessageBox.Show("MySQL error occurred: " & ex.Message)
                    Return -1 ' Indicate error
                End Try
            End Using
        End Using
    End Function
    Private Sub InsertBookingRequest(labId As Integer, requestDate As Date)
        ' Get the user ID of the currently logged-in user
        Dim userId As String = Form1.LoggedInUserId

        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand("INSERT INTO labbookingrequest (LabId, UserId, RequestDate) VALUES (@LabId, @UserId, @RequestDate)", conn)
                cmd.Parameters.AddWithValue("@LabId", labId)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.Parameters.AddWithValue("@RequestDate", requestDate)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Booking request submitted successfully.")
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim labName As String = ComboBox1.SelectedItem.ToString()
        Dim labId As Integer = GetLabIdByName(labName)

        If labId = -1 Then
            MessageBox.Show("Lab not found.")
            Return
        End If

        ' Remove the userId parameter from the InsertBookingRequest call
        Dim requestDate As Date = DateTimePicker1.Value
        InsertBookingRequest(labId, requestDate)
    End Sub

    Private Sub VmUser_Click(sender As Object, e As EventArgs) Handles VmUser.Click
        ' Get the user ID of the currently logged-in user
        Dim userId As String = Form1.LoggedInUserId

        ' Retrieve lab booking history for the current user
        Dim history As String = GetLabBookingHistory(userId)

        ' Display the history in a message box
        MessageBox.Show(history, "Lab Booking History", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function GetLabBookingHistory(userId As String) As String
        Dim history As New StringBuilder()

        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand("SELECT * FROM labbookingrequest WHERE UserId = @UserId", conn)
                cmd.Parameters.AddWithValue("@UserId", userId)

                Try
                    conn.Open()
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            ' Format the data for display in the message box
                            history.AppendLine($"Booking Request ID: {reader("BookingRequestId")}")
                            history.AppendLine($"Lab ID: {reader("LabId")}")
                            history.AppendLine($"Request Date: {reader("RequestDate")}")
                            history.AppendLine($"Status: {reader("Status")}")
                            history.AppendLine($"Created At: {reader("CreatedAt")}")
                            history.AppendLine()
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message)
                End Try
            End Using
        End Using

        Return history.ToString()
    End Function
End Class

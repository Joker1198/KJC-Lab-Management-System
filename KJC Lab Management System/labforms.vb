Imports MySql.Data.MySqlClient

Public Class labforms
    ' Replace this with your actual connection string.
    Dim connectionString As String = "Server=127.0.0.1;Database=kjclab;User Id=root;Password=;"
    Dim loggedInLabId As Integer = -1 ' Default value indicating no lab is logged in

    Private Sub labforms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load lab data and lab booking requests for the logged-in lab admin
        LoadLabData()
    End Sub


    Private Sub LoadLabData()
        ' Set the logged-in lab's ID based on the logged-in admin's LabAdminId
        SetLoggedInLabId()

        If loggedInLabId <> -1 Then
            ' Load lab details (if needed) based on the logged-in lab admin
            ' Add your logic to load lab details here

            ' Load lab booking requests for the logged-in lab into DataGridView1
            LoadLabBookingRequests()
        Else
            MessageBox.Show("Lab not recognized.")
        End If
    End Sub

    Private Sub SetLoggedInLabId()
        ' Replace this with your logic to get the lab ID based on the logged-in admin's LabAdminId
        ' For now, let's assume a default value of 1
        ' You may need to fetch this value based on your authentication mechanism
        loggedInLabId = 1
    End Sub



    Private Sub LoadLabBookingRequests()
        ' Create a DataTable to hold the lab booking requests
        Dim labBookingRequestsDataTable As New DataTable()

        ' Get lab booking requests for the logged-in lab from the database
        Dim requests As List(Of LabBookingRequest) = GetLabBookingRequests(loggedInLabId)

        If requests.Count > 0 Then
            ' Add columns to the DataTable based on LabBookingRequest properties
            For Each prop As System.Reflection.PropertyInfo In GetType(LabBookingRequest).GetProperties()
                labBookingRequestsDataTable.Columns.Add(prop.Name, prop.PropertyType)
            Next

            ' Populate the DataTable with the lab booking requests
            For Each request As LabBookingRequest In requests
                Dim row As DataRow = labBookingRequestsDataTable.NewRow()
                For Each prop As System.Reflection.PropertyInfo In GetType(LabBookingRequest).GetProperties()
                    row(prop.Name) = prop.GetValue(request)
                Next
                labBookingRequestsDataTable.Rows.Add(row)
            Next
        Else
            MessageBox.Show("No lab booking requests found for the logged-in lab.", "No Requests", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' Bind the DataTable to the DataGridView
        DataGridView1.DataSource = labBookingRequestsDataTable
    End Sub


    Private Function GetLabBookingRequests(labId As Integer) As List(Of LabBookingRequest)
        Dim labBookingRequests As New List(Of LabBookingRequest)()

        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand("SELECT * FROM labbookingrequest WHERE LabId = @LabId", conn)
                cmd.Parameters.AddWithValue("@LabId", labId)

                Try
                    conn.Open()
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            ' Create LabBookingRequest object and populate its properties
                            Dim request As New LabBookingRequest()
                            request.BookingRequestId = Convert.ToInt32(reader("BookingRequestId"))
                            request.LabId = Convert.ToInt32(reader("LabId"))
                            request.UserId = Convert.ToString(reader("UserId"))
                            request.RequestDate = Convert.ToDateTime(reader("RequestDate"))
                            request.Status = Convert.ToString(reader("Status"))
                            request.CreatedAt = Convert.ToDateTime(reader("CreatedAt"))
                            request.Materials = Convert.ToString(reader("Materials"))

                            ' Add the LabBookingRequest object to the list
                            labBookingRequests.Add(request)
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message)
                End Try
            End Using
        End Using

        Return labBookingRequests
    End Function

    ' Class representing LabBookingRequest
    Private Class LabBookingRequest
        Public Property BookingRequestId As Integer
        Public Property LabId As Integer
        Public Property UserId As String
        Public Property RequestDate As Date
        Public Property Status As String
        Public Property CreatedAt As Date
        Public Property Materials As String
    End Class

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Assuming the DataGridView1 is bound to a DataTable named labBookingRequestsDataTable
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected lab booking request ID
            Dim selectedRequestId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("BookingRequestId").Value)

            ' Update the status of the selected lab booking request to "Accepted"
            UpdateBookingRequestStatus(selectedRequestId, "Accepted")

            ' Reload lab booking requests into DataGridView
            LoadLabBookingRequests()
        Else
            MessageBox.Show("Please select a lab booking request to accept.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Assuming the DataGridView1 is bound to a DataTable named labBookingRequestsDataTable
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected lab booking request ID
            Dim selectedRequestId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("BookingRequestId").Value)

            ' Update the status of the selected lab booking request to "Declined"
            UpdateBookingRequestStatus(selectedRequestId, "Declined")

            ' Reload lab booking requests into DataGridView
            LoadLabBookingRequests()
        Else
            MessageBox.Show("Please select a lab booking request to decline.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub UpdateBookingRequestStatus(requestId As Integer, status As String)
        ' Update the status of the lab booking request in the database
        Using conn As New MySqlConnection(connectionString)
            Using cmd As New MySqlCommand("UPDATE labbookingrequest SET Status = @Status WHERE BookingRequestId = @RequestId", conn)
                cmd.Parameters.AddWithValue("@Status", status)
                cmd.Parameters.AddWithValue("@RequestId", requestId)

                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show($"Lab booking request {requestId} has been {status}.", "Request Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

End Class

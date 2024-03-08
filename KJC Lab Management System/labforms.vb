Imports MySql.Data.MySqlClient

Public Class labforms
    ' Replace this with your actual connection string.
    Dim connectionString As String = "Server=127.0.0.1;Database=kjclab;User Id=root;Password=;"
    Dim loggedInLabId As Integer ' Assuming you have a way to store the logged-in lab ID

    Private Sub labforms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load lab booking requests for the logged-in lab into DataGridView1
        LoadLabData()
    End Sub

    Private Sub LoadLabData()
        ' Set the logged-in lab's ID
        SetLoggedInLabId()

        ' Load lab booking requests for the logged-in lab into DataGridView1
        LoadLabBookingRequests()
    End Sub

    Private Sub SetLoggedInLabId()
        ' Assuming you have a way to get the logged-in admin's lab ID, update loggedInLabId accordingly
        ' For now, let's assume a default value of 1
        loggedInLabId = 1
    End Sub

    Private Sub LoadLabBookingRequests()
        ' Clear existing rows and columns in the DataGridView
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()

        If loggedInLabId <> -1 Then
            ' Get lab booking requests for the logged-in lab from the database
            Dim requests As List(Of LabBookingRequest) = GetLabBookingRequests(loggedInLabId)

            ' Add columns to the DataGridView based on LabBookingRequest properties
            For Each prop As System.Reflection.PropertyInfo In GetType(LabBookingRequest).GetProperties()
                DataGridView1.Columns.Add(prop.Name, prop.Name)
            Next

            ' Populate the DataGridView with the lab booking requests
            For Each request As LabBookingRequest In requests
                DataGridView1.Rows.Add(request.BookingRequestId, request.LabId, request.UserId, request.RequestDate, request.Status, request.CreatedAt, request.Materials)
            Next
        Else
            MessageBox.Show("Lab not recognized.")
        End If
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
        ' Reload lab data when Button1 is clicked
        LoadLabData()
    End Sub
End Class

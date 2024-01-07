Imports MySql.Data.MySqlClient

Public Class Form1
    Public Shared connectionString As String = "Server=127.0.0.1;Database=kjclab;User Id=root;Password=;"

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        Try
            Dim role As String = GetUserRole(username, password)

            If role IsNot Nothing Then
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Redirect based on role
                Select Case role.ToLower()
                    Case "admin"
                        ' Redirect admin to admin dashboard
                        MessageBox.Show("Welcome Admin!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Hide()
                        AdminDashboard.Show()


                    Case "teacher"
                        ' Redirect teacher to teacher dashboard
                        MessageBox.Show("Welcome Teacher!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' TODO: Redirect to teacher dashboard form

                    Case "student"
                        ' Redirect student to student dashboard
                        MessageBox.Show("Welcome Student!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' TODO: Redirect to student dashboard form

                    Case Else
                        MessageBox.Show("Unknown role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Select
            Else
                MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetUserRole(username As String, password As String) As String
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT Role FROM login WHERE Username = @Username AND Password = @Password"
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.Parameters.AddWithValue("@Password", password)
                Dim role As Object = cmd.ExecuteScalar()

                ' Check for DBNull.Value before converting to string
                If role IsNot DBNull.Value AndAlso role IsNot Nothing Then
                    Return role.ToString()
                Else
                    Return Nothing
                End If
            End Using
        End Using
    End Function
End Class

Imports MySql.Data.MySqlClient

Public Class Form1
    Public Shared connectionString As String = "Server=127.0.0.1;Database=kjclab;User Id=root;Password=;"

    ' Shared variable to store the logged-in user ID
    Public Shared LoggedInUserId As Integer

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        Try
            Dim roleId As String = GetUserRole(username, password)

            If roleId IsNot Nothing Then
                ' Store the logged-in user ID
                LoggedInUserId = Integer.Parse(username)

                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Redirect based on role
                Select Case roleId.ToLower()
                    Case "1"
                        ' Redirect admin to admin dashboard
                        MessageBox.Show("Welcome Admin!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Hide()
                        AdminDashboard.Show()

                    Case "2"
                        ' Redirect lab admin to lab admin dashboard
                        MessageBox.Show("Welcome Lab Admin!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' TODO: Redirect to lab admin dashboard form

                    Case "3"
                        ' Redirect teacher to teacher dashboard
                        MessageBox.Show("Welcome Teacher!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        teachod.Show()
                        ' TODO: Redirect to teacher dashboard form

                    Case "4"
                        ' Redirect student to student dashboard
                        MessageBox.Show("Welcome Student!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' TODO: Redirect to student dashboard form

                    Case "5"
                        ' Redirect HOD to HOD dashboard
                        MessageBox.Show("Welcome HOD!", "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' TODO: Redirect to HOD dashboard form

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

    Private Function GetUserRole(userId As String, password As String) As String
        Using connection As New MySqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT RoleId FROM userkjc WHERE UserId = @UserId AND Password = @Password"
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@UserId", userId)
                cmd.Parameters.AddWithValue("@Password", password)
                Dim roleId As Object = cmd.ExecuteScalar()

                ' Check for DBNull.Value before converting to string
                If roleId IsNot DBNull.Value AndAlso roleId IsNot Nothing Then
                    Return roleId.ToString()
                Else
                    Return Nothing
                End If
            End Using
        End Using
    End Function
End Class

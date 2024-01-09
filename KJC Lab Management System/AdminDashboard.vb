Imports MySql.Data.MySqlClient

Public Class AdminDashboard
    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load any initial data or setup here
        ' Add roles to the ComboBox
        cmbNewRole.Items.AddRange({"Student", "Teacher", "HOD"})

        ' Hide the controls initially
        txtNewUsername.Visible = False
        txtNewPassword.Visible = False
        cmbNewRole.Visible = False
        btnConfirmAddUser.Visible = False
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        DataGridView1.Visible = False
        btnChangePassword.Visible = False
        btnChangeRole.Visible = False
        btnDeleteUser.Visible = False
    End Sub

    Private Sub btnAddUsers_Click(sender As Object, e As EventArgs) Handles btnAddUsers.Click
        ' Show the controls for adding a new user
        txtNewUsername.Visible = True
        txtNewPassword.Visible = True
        cmbNewRole.Visible = True
        btnConfirmAddUser.Visible = True
        Label1.Visible = True
        Label2.Visible = True
        Label3.Visible = True
        DataGridView1.Visible = False ' Hide DataGridView when adding new users
        btnChangePassword.Visible = False
        btnChangeRole.Visible = False
        btnDeleteUser.Visible = False
    End Sub

    Private Sub btnConfirmAddUser_Click(sender As Object, e As EventArgs) Handles btnConfirmAddUser.Click
        ' Call the method to confirm and add the user
        ConfirmAddUser()
    End Sub

    Private Sub ConfirmAddUser()
        ' Get values from textboxes and ComboBox
        Dim newUsername As String = txtNewUsername.Text
        Dim newPassword As String = txtNewPassword.Text
        Dim newRole As String = cmbNewRole.SelectedItem?.ToString() ' Use ?. to handle possible null

        ' Validate input (you can add more validation as needed)
        If String.IsNullOrEmpty(newUsername) OrElse String.IsNullOrEmpty(newPassword) OrElse String.IsNullOrEmpty(newRole) Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Perform the final steps to add the user to the database
        Try
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()
                Dim insertQuery As String = "INSERT INTO login (Username, Password, Role) VALUES (@Username, @Password, @Role)"
                Using cmd As New MySqlCommand(insertQuery, connection)
                    cmd.Parameters.AddWithValue("@Username", newUsername)
                    cmd.Parameters.AddWithValue("@Password", newPassword)
                    cmd.Parameters.AddWithValue("@Role", newRole)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Hide the controls after adding a new user
            txtNewUsername.Visible = False
            txtNewPassword.Visible = False
            cmbNewRole.Visible = False
            btnConfirmAddUser.Visible = False
            Label1.Visible = False
            Label2.Visible = False
            Label3.Visible = False

            ' Clear textboxes and ComboBox after adding a new user
            txtNewUsername.Clear()
            txtNewPassword.Clear()
            cmbNewRole.SelectedIndex = -1 ' Reset ComboBox selection

            ' Show DataGridView after adding new user
            DataGridView1.Visible = True
            ' Fetch and display updated user data
            LoadUserData()
        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Close the AdminDashboardForm
        Me.Close()
        Form1.Close()
    End Sub

    Private Sub VmUser_Click(sender As Object, e As EventArgs) Handles VmUser.Click
        Try
            ' Display user data in the DataGridView
            txtNewUsername.Visible = False
            txtNewPassword.Visible = False
            cmbNewRole.Visible = False
            btnConfirmAddUser.Visible = False
            Label1.Visible = False
            Label2.Visible = False
            Label3.Visible = False
            DataGridView1.Visible = True
            btnChangePassword.Visible = True
            btnChangeRole.Visible = True
            btnDeleteUser.Visible = True
            LoadUserData()
        Catch ex As Exception
            MessageBox.Show("Error fetching user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUserData()
        Try
            ' Fetch user data from the database
            Dim userData As DataTable = GetUserData()

            ' Display the user data in the DataGridView
            DataGridView1.DataSource = userData
        Catch ex As Exception
            MessageBox.Show("Error fetching user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to delete selected user
    Private Sub DeleteUser()
        If DataGridView1.SelectedRows.Count > 0 Then
            Try
                Dim selectedUsername As String = DataGridView1.SelectedRows(0).Cells("Username").Value.ToString()

                ' Delete user from the database
                Using connection As New MySqlConnection(Form1.connectionString)
                    connection.Open()
                    Dim deleteQuery As String = "DELETE FROM login WHERE Username = @Username"
                    Using cmd As New MySqlCommand(deleteQuery, connection)
                        cmd.Parameters.AddWithValue("@Username", selectedUsername)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                ' Refresh the user data in the DataGridView
                LoadUserData()

                MessageBox.Show($"User '{selectedUsername}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error deleting user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Method to change role of selected user
    Private Sub ChangeUserRole()
        If DataGridView1.SelectedRows.Count > 0 Then
            Try
                Dim selectedUsername As String = DataGridView1.SelectedRows(0).Cells("Username").Value.ToString()

                ' Show a dialog to input the new role
                Dim newRole As String = InputBox($"Enter new role for '{selectedUsername}':", "Change Role", "Student")

                If Not String.IsNullOrEmpty(newRole) Then
                    ' Update the user role in the database
                    Using connection As New MySqlConnection(Form1.connectionString)
                        connection.Open()
                        Dim updateQuery As String = "UPDATE login SET Role = @Role WHERE Username = @Username"
                        Using cmd As New MySqlCommand(updateQuery, connection)
                            cmd.Parameters.AddWithValue("@Role", newRole)
                            cmd.Parameters.AddWithValue("@Username", selectedUsername)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                    ' Refresh the user data in the DataGridView
                    LoadUserData()

                    MessageBox.Show($"Role for '{selectedUsername}' changed to '{newRole}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("Error changing user role: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a user to change the role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Method to change password of selected user
    Private Sub ChangeUserPassword()
        If DataGridView1.SelectedRows.Count > 0 Then
            Try
                Dim selectedUsername As String = DataGridView1.SelectedRows(0).Cells("Username").Value.ToString()

                ' Show a dialog to input the new password
                Dim newPassword As String = InputBox($"Enter new password for '{selectedUsername}':", "Change Password", "")

                If Not String.IsNullOrEmpty(newPassword) Then
                    ' Update the user password in the database
                    Using connection As New MySqlConnection(Form1.connectionString)
                        connection.Open()
                        Dim updateQuery As String = "UPDATE login SET Password = @Password WHERE Username = @Username"
                        Using cmd As New MySqlCommand(updateQuery, connection)
                            cmd.Parameters.AddWithValue("@Password", newPassword)
                            cmd.Parameters.AddWithValue("@Username", selectedUsername)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                    ' Refresh the user data in the DataGridView
                    LoadUserData()

                    MessageBox.Show($"Password for '{selectedUsername}' changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("Error changing user password: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a user to change the password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Button click events for managing users
    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs)
        ' Call the method to delete the selected user
        DeleteUser()
    End Sub

    Private Sub btnChangeRole_Click(sender As Object, e As EventArgs)
        ' Call the method to change the role of the selected user
        ChangeUserRole()
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs)
        ' Call the method to change the password of the selected user
        ChangeUserPassword()
    End Sub

    Private Function GetUserData() As DataTable
        Dim userData As New DataTable()

        Try
            ' Connect to the database
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Query to select user data
                Dim query As String = "SELECT Username, Role FROM login"

                ' Execute the query
                Using cmd As New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(cmd)
                        ' Fill the DataTable with the results
                        adapter.Fill(userData)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Throw ex
        End Try

        Return userData
    End Function

End Class

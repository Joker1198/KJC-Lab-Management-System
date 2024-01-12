Imports MySql.Data.MySqlClient

Public Class AdminDashboard
    Private Sub HideControls(ParamArray controlNames As String())
        For Each controlName As String In controlNames
            Dim control = Controls.Find(controlName, True).FirstOrDefault()
            If control IsNot Nothing Then
                control.Hide()
            End If
        Next
    End Sub

    Private Sub ShowControls(ParamArray controlNames As String())
        For Each controlName As String In controlNames
            Dim control = Controls.Find(controlName, True).FirstOrDefault()
            If control IsNot Nothing Then
                control.Show()
            End If
        Next
    End Sub
    Private Sub AdminDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load any initial data or setup here
        ' Add roles to the ComboBox
        cmbNewRole.Items.AddRange({"Admin", "Lab Admin", "Teacher", "Student", "HOD"})

        ' Hide the controls initially
        HideControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3")
    End Sub

    Private Sub btnAddUsers_Click(sender As Object, e As EventArgs) Handles btnAddUsers.Click
        ' Show the controls for adding a new user
        ShowControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3")
        HideControls("DataGridView1", "btnChangePassword", "btnChangeRole", "btnDeleteUser")
    End Sub

    Private Sub btnConfirmAddUser_Click(sender As Object, e As EventArgs) Handles btnConfirmAddUser.Click
        ' Call the method to confirm and add the user
        ConfirmAddUser()
    End Sub

    Private Sub VmUser_Click(sender As Object, e As EventArgs) Handles VmUser.Click
        Try
            ' Display user data in the DataGridView
            HideControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3")
            ShowControls("DataGridView1", "btnChangePassword", "btnChangeRole", "btnDeleteUser")
            LoadUserData()
        Catch ex As Exception
            MessageBox.Show("Error fetching user data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
        ' Call the method to delete the selected user
        DeleteUser()
    End Sub

    Private Sub btnChangeRole_Click(sender As Object, e As EventArgs) Handles btnChangeRole.Click
        ' Call the method to change the role of the selected user
        ChangeUserRole()
    End Sub

    Private Sub btnChangePassword_Click(sender As Object, e As EventArgs) Handles btnChangePassword.Click
        ' Call the method to change the password of the selected user
        ChangeUserPassword()
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Close the AdminDashboardForm
        Me.Close()
        Form1.Close()
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
                Dim insertQuery As String = "INSERT INTO UserKJC (UserId, UserName, Password, RoleId, RoleName) VALUES (@UserId, @UserName, @Password, @RoleId, @RoleName)"
                Using cmd As New MySqlCommand(insertQuery, connection)
                    cmd.Parameters.AddWithValue("@UserId", Guid.NewGuid().ToString()) ' Generate a unique UserId
                    cmd.Parameters.AddWithValue("@UserName", newUsername)
                    cmd.Parameters.AddWithValue("@Password", newPassword)
                    cmd.Parameters.AddWithValue("@RoleId", GetRoleIdByName(newRole)) ' Get RoleId from RoleName
                    cmd.Parameters.AddWithValue("@RoleName", newRole)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Hide the controls after adding a new user
            HideControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3")

            ' Clear textboxes and ComboBox after adding a new user
            txtNewUsername.Clear()
            txtNewPassword.Clear()
            cmbNewRole.SelectedIndex = -1 ' Reset ComboBox selection

            ' Show DataGridView after adding new user
            ShowControls("DataGridView1", "btnChangePassword", "btnChangeRole", "btnDeleteUser")
            ' Fetch and display updated user data
            LoadUserData()
        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadUserData()
        Try
            ' Fetch user data from the database
            Dim userData As DataTable = GetUserData()

            ' Display the user data in the DataGridView
            DataGridView1.DataSource = userData
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetUserData() As DataTable
        Dim userData As New DataTable()

        Try
            ' Connect to the database
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Query to select user data
                Dim query As String = "SELECT UserId, UserName, RoleName FROM UserKJC"

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

    Private Function GetRoleIdByName(roleName As String) As Integer
        ' Connect to the database
        Using connection As New MySqlConnection(Form1.connectionString)
            connection.Open()

            ' Query to get RoleId by RoleName
            Dim query As String = "SELECT RoleId FROM Role WHERE RoleName = @RoleName"

            ' Execute the query
            Using cmd As New MySqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@RoleName", roleName)
                Dim roleId As Object = cmd.ExecuteScalar()

                ' Check for DBNull.Value before converting to integer
                If roleId IsNot DBNull.Value AndAlso roleId IsNot Nothing Then
                    Return Convert.ToInt32(roleId)
                Else
                    Return -1 ' Return -1 if RoleId is not found
                End If
            End Using
        End Using
    End Function

    Private Sub DeleteUser()
        If DataGridView1.SelectedRows.Count > 0 Then
            Try
                Dim selectedUserId As String = DataGridView1.SelectedRows(0).Cells("UserId").Value.ToString()

                ' Delete user from the database
                Using connection As New MySqlConnection(Form1.connectionString)
                    connection.Open()
                    Dim deleteQuery As String = "DELETE FROM UserKJC WHERE UserId = @UserId"
                    Using cmd As New MySqlCommand(deleteQuery, connection)
                        cmd.Parameters.AddWithValue("@UserId", selectedUserId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                ' Refresh the user data in the DataGridView
                LoadUserData()

                MessageBox.Show($"User '{selectedUserId}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error deleting user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ChangeUserRole()
        If DataGridView1.SelectedRows.Count > 0 Then
            Try
                Dim selectedUserId As String = DataGridView1.SelectedRows(0).Cells("UserId").Value.ToString()

                ' Show a dialog to input the new role
                Dim newRole As String = InputBox($"Enter new role for '{selectedUserId}':", "Change Role", "")

                If Not String.IsNullOrEmpty(newRole) Then
                    ' Update the user role in the database
                    Using connection As New MySqlConnection(Form1.connectionString)
                        connection.Open()
                        Dim updateQuery As String = "UPDATE UserKJC SET RoleId = @RoleId, RoleName = @RoleName WHERE UserId = @UserId"
                        Using cmd As New MySqlCommand(updateQuery, connection)
                            cmd.Parameters.AddWithValue("@RoleId", GetRoleIdByName(newRole)) ' Get RoleId from RoleName
                            cmd.Parameters.AddWithValue("@RoleName", newRole)
                            cmd.Parameters.AddWithValue("@UserId", selectedUserId)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                    ' Refresh the user data in the DataGridView
                    LoadUserData()

                    MessageBox.Show($"Role for '{selectedUserId}' changed to '{newRole}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("Error changing user role: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a user to change the role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ChangeUserPassword()
        If DataGridView1.SelectedRows.Count > 0 Then
            Try
                Dim selectedUserId As String = DataGridView1.SelectedRows(0).Cells("UserId").Value.ToString()

                ' Show a dialog to input the new password
                Dim newPassword As String = InputBox($"Enter new password for '{selectedUserId}':", "Change Password", "")

                If Not String.IsNullOrEmpty(newPassword) Then
                    ' Update the user password in the database
                    Using connection As New MySqlConnection(Form1.connectionString)
                        connection.Open()
                        Dim updateQuery As String = "UPDATE UserKJC SET Password = @Password WHERE UserId = @UserId"
                        Using cmd As New MySqlCommand(updateQuery, connection)
                            cmd.Parameters.AddWithValue("@Password", newPassword)
                            cmd.Parameters.AddWithValue("@UserId", selectedUserId)
                            cmd.ExecuteNonQuery()
                        End Using
                    End Using

                    ' Refresh the user data in the DataGridView
                    LoadUserData()

                    MessageBox.Show($"Password for '{selectedUserId}' changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MessageBox.Show("Error changing user password: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a user to change the password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class

Imports Microsoft.SqlServer
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
        ' Load departments to the ComboBox
        LoadDepartments()

        ' Hide the controls initially
        HideControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3", "UserIDT", "Label4", "cmbcls", "cmbdept", "Label6", "Label5")
        HideControls("DataGridView1", "btnChangePassword", "btnChangeRole", "btnDeleteUser")
    End Sub

    Private Sub btnAddUsers_Click(sender As Object, e As EventArgs) Handles btnAddUsers.Click
        ' Show the controls for adding a new user
        ShowControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3", "UserIDT", "Label4", "cmbcls", "cmbdept", "Label6", "Label5")
        HideControls("DataGridView1", "btnChangePassword", "btnChangeRole", "btnDeleteUser")
    End Sub

    Private Sub btnConfirmAddUser_Click(sender As Object, e As EventArgs) Handles btnConfirmAddUser.Click
        ' Call the method to confirm and add the user
        ConfirmAddUser()
    End Sub

    Private Sub VmUser_Click(sender As Object, e As EventArgs) Handles VmUser.Click
        Try
            ' Display user data in the DataGridView
            HideControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3", "UserIDT", "Label4", "cmbcls", "cmbdept", "Label6", "Label5")
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
        Dim newUserID As String = UserIDT.Text
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
                    cmd.Parameters.AddWithValue("@UserId", newUserID)
                    cmd.Parameters.AddWithValue("@UserName", newUsername)
                    cmd.Parameters.AddWithValue("@Password", newPassword)
                    cmd.Parameters.AddWithValue("@RoleId", GetRoleIdByName(newRole)) ' Get RoleId from RoleName
                    cmd.Parameters.AddWithValue("@RoleName", newRole)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Hide the controls after adding a new user
            HideControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3", "UserIDT", "Label4", "cmbcls", "cmbdept", "Label6", "Label5")

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

    'loading class into cmb class
    Private Function LoadClasses() As List(Of String)
        Dim ClassName As New List(Of String)()

        Try
            ' Connect to the database
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Query to select class names
                Dim query As String = "SELECT ClassName FROM Class"

                ' Execute the query
                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            ' Add class names to the list
                            ClassName.Add(reader("ClassName").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading class names: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return ClassName
    End Function

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

    'This is for loading Dept name into cmb
    Private Function LoadDepartments() As List(Of String)
        Dim DepartmentName As New List(Of String)()
        Try
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Query to select department names
                Dim query As String = "SELECT DepartmentName FROM Department"

                ' Execute the query
                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Clear existing items in the ComboBox
                        cmbdept.Items.Clear()

                        ' Add department names to the ComboBox
                        While reader.Read()
                            cmbdept.Items.Add(reader("DepartmentName").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading departments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub cmbdept_SelectedIndexChanged(sender As Object, e As EventArgs)
        ' Clear existing items in cmbcls

        ' Get the selected department
        Dim selectedDepartment = cmbdept.SelectedItem?.ToString

        ' If a department is selected, load corresponding classes
        If Not String.IsNullOrEmpty(selectedDepartment) Then
            ' Fetch classes for the selected department from the database
        End If
    End Sub
    Public Function GetDepartmentIdByName(departmentName As String) As Integer
        Dim departmentId As Integer = -1 ' Default value if not found

        ' Your database connection string
        Dim connectionString As String = " Server=127.0.0.1;Database=kjclab;User Id=root;Password=;"

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Query to select DepartmentId for the given department name
                Dim query As String = "SELECT DepartmentId FROM Department WHERE DepartmentName = @DepartmentName"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@DepartmentName", departmentName)

                    ' Execute the query
                    Dim result As Object = cmd.ExecuteScalar()

                    ' Check if the result is not DBNull
                    If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                        departmentId = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ' Handle exceptions, log, or display an error message
            Console.WriteLine("Error fetching DepartmentId: " & ex.Message)
        End Try

        Return departmentId
    End Function

    'From here we'll work on lab now
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnaddl.Click
        HideControls("txtNewUsername", "txtNewPassword", "cmbNewRole", "btnConfirmAddUser", "Label1", "Label2", "Label3", "UserIDT", "Label4", "cmbcls", "cmbdept", "Label6", "Label5")
        HideControls("DataGridView1", "btnChangePassword", "btnChangeRole", "btnDeleteUser")
        LoadDepartmentsIntoComboBox(cmbdept1)

        ' Load existing Lab Admins into AdminAsgn ComboBox
        LoadLabAdminsIntoComboBox(AdminAsgn)
    End Sub
    Private Sub ConfirmAddLab()
        ' Get values from controls
        Dim labNameValue As String = Lnae.Text
        Dim selectedDepartment As String = cmbdept1.SelectedItem?.ToString()
        Dim selectedLabAdmin As String = AdminAsgn.SelectedItem?.ToString()

        ' Validate input (you can add more validation as needed)
        If String.IsNullOrEmpty(labNameValue) OrElse String.IsNullOrEmpty(selectedDepartment) OrElse String.IsNullOrEmpty(selectedLabAdmin) Then
            MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Perform the final steps to add the lab to the database
        Try
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Get DepartmentId for the selected department
                Dim departmentId As Integer = GetDepartmentIdByName(selectedDepartment)

                ' Get UserId for the selected Lab Admin
                Dim labAdminUserId As String = GetUserIdByNameAndRole(selectedLabAdmin, "Lab Admin")

                ' Insert the lab into the Lab table
                Dim insertQuery As String = "INSERT INTO Lab (LabName, DepartmentId, LabAdminId) VALUES (@LabName, @DepartmentId, @LabAdminId)"
                Using cmd As New MySqlCommand(insertQuery, connection)
                    cmd.Parameters.AddWithValue("@LabName", labNameValue)
                    cmd.Parameters.AddWithValue("@DepartmentId", departmentId)
                    cmd.Parameters.AddWithValue("@LabAdminId", labAdminUserId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Lab added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Hide the controls after adding a new lab
            HideControls("cmbdept1", "LabName", "AdminAsgn", "cnfmlab")

            ' Clear textboxes and ComboBox after adding a new lab
            cmbdept1.SelectedIndex = -1
            AdminAsgn.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Error adding lab: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub cnfmlab_Click(sender As Object, e As EventArgs) Handles cnfmlab.Click
        ConfirmAddLab()
    End Sub





    Private Sub LoadDepartmentsIntoComboBox(comboBox As ComboBox)
        Try
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Query to select department names
                Dim query As String = "SELECT DepartmentName FROM Department"

                ' Execute the query
                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Clear existing items in the ComboBox
                        comboBox.Items.Clear()

                        ' Add department names to the ComboBox
                        While reader.Read()
                            comboBox.Items.Add(reader("DepartmentName").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading departments: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadLabAdminsIntoComboBox(comboBox As ComboBox)
        Try
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Query to select Lab Admin names
                Dim query As String = "SELECT UserName FROM UserKJC WHERE RoleName = 'Lab Admin'"

                ' Execute the query
                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Clear existing items in the ComboBox
                        comboBox.Items.Clear()

                        ' Add Lab Admin names to the ComboBox
                        While reader.Read()
                            comboBox.Items.Add(reader("UserName").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading Lab Admins: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetUserIdByNameAndRole(userName As String, roleName As String) As String
        Dim userId As String = ""

        Try
            Using connection As New MySqlConnection(Form1.connectionString)
                connection.Open()

                ' Query to select UserId for the given username and role
                Dim query As String = "SELECT UserId FROM UserKJC WHERE UserName = @UserName AND RoleName = @RoleName"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@UserName", userName)
                    cmd.Parameters.AddWithValue("@RoleName", roleName)

                    ' Execute the query
                    Dim result As Object = cmd.ExecuteScalar()

                    ' Check if the result is not DBNull
                    If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                        userId = result.ToString()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching UserId: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return userId
    End Function
End Class
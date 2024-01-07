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
        Catch ex As Exception
            MessageBox.Show("Error adding user: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        ' Close the AdminDashboardForm
        Me.Close()
        Form1.Close()
    End Sub
End Class

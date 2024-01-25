<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdminDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        btnLogout = New Button()
        lblWelcome = New Label()
        btnAddUsers = New Button()
        txtNewUsername = New TextBox()
        txtNewPassword = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        cmbNewRole = New ComboBox()
        Label3 = New Label()
        btnConfirmAddUser = New Button()
        DataGridView1 = New DataGridView()
        VmUser = New Button()
        btnChangePassword = New Button()
        btnChangeRole = New Button()
        Label4 = New Label()
        UserIDT = New TextBox()
        Label6 = New Label()
        cmbdept = New ComboBox()
        btnDeleteUser = New Button()
        Label7 = New Label()
        btnaddl = New Button()
        Button2 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnLogout
        ' 
        btnLogout.Location = New Point(1089, 7)
        btnLogout.Name = "btnLogout"
        btnLogout.Size = New Size(129, 37)
        btnLogout.TabIndex = 0
        btnLogout.Text = "Logout"
        btnLogout.UseVisualStyleBackColor = True
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Location = New Point(305, 189)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(0, 15)
        lblWelcome.TabIndex = 1
        ' 
        ' btnAddUsers
        ' 
        btnAddUsers.Location = New Point(35, 82)
        btnAddUsers.Name = "btnAddUsers"
        btnAddUsers.Size = New Size(90, 59)
        btnAddUsers.TabIndex = 2
        btnAddUsers.Text = "Add Users"
        btnAddUsers.UseVisualStyleBackColor = True
        ' 
        ' txtNewUsername
        ' 
        txtNewUsername.Location = New Point(212, 82)
        txtNewUsername.Name = "txtNewUsername"
        txtNewUsername.PlaceholderText = "College Mail"
        txtNewUsername.Size = New Size(200, 23)
        txtNewUsername.TabIndex = 3
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Location = New Point(212, 111)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.PlaceholderText = "Assign Password"
        txtNewPassword.Size = New Size(200, 23)
        txtNewPassword.TabIndex = 4
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(140, 85)
        Label1.Name = "Label1"
        Label1.Size = New Size(66, 15)
        Label1.TabIndex = 5
        Label1.Text = "Username :"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(143, 114)
        Label2.Name = "Label2"
        Label2.Size = New Size(63, 15)
        Label2.TabIndex = 6
        Label2.Text = "Password :"
        ' 
        ' cmbNewRole
        ' 
        cmbNewRole.FormattingEnabled = True
        cmbNewRole.Location = New Point(212, 227)
        cmbNewRole.Name = "cmbNewRole"
        cmbNewRole.Size = New Size(200, 23)
        cmbNewRole.TabIndex = 7
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(165, 230)
        Label3.Name = "Label3"
        Label3.Size = New Size(41, 15)
        Label3.TabIndex = 8
        Label3.Text = "Roles :"
        ' 
        ' btnConfirmAddUser
        ' 
        btnConfirmAddUser.Location = New Point(230, 281)
        btnConfirmAddUser.Name = "btnConfirmAddUser"
        btnConfirmAddUser.Size = New Size(153, 23)
        btnConfirmAddUser.TabIndex = 9
        btnConfirmAddUser.Text = "Confirm And Add User"
        btnConfirmAddUser.UseVisualStyleBackColor = True
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(768, 63)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(450, 219)
        DataGridView1.TabIndex = 10
        ' 
        ' VmUser
        ' 
        VmUser.Location = New Point(35, 160)
        VmUser.Name = "VmUser"
        VmUser.Size = New Size(90, 59)
        VmUser.TabIndex = 11
        VmUser.Text = "View/Manage Users"
        VmUser.UseVisualStyleBackColor = True
        ' 
        ' btnChangePassword
        ' 
        btnChangePassword.Location = New Point(929, 288)
        btnChangePassword.Name = "btnChangePassword"
        btnChangePassword.Size = New Size(125, 46)
        btnChangePassword.TabIndex = 13
        btnChangePassword.Text = "Change Password"
        btnChangePassword.UseVisualStyleBackColor = True
        ' 
        ' btnChangeRole
        ' 
        btnChangeRole.Location = New Point(768, 288)
        btnChangeRole.Name = "btnChangeRole"
        btnChangeRole.Size = New Size(125, 46)
        btnChangeRole.TabIndex = 14
        btnChangeRole.Text = "Change Role"
        btnChangeRole.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(143, 143)
        Label4.Name = "Label4"
        Label4.Size = New Size(43, 15)
        Label4.TabIndex = 16
        Label4.Text = "User Id"
        ' 
        ' UserIDT
        ' 
        UserIDT.Location = New Point(212, 140)
        UserIDT.Name = "UserIDT"
        UserIDT.PlaceholderText = "Assign User Id"
        UserIDT.Size = New Size(200, 23)
        UserIDT.TabIndex = 15
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(134, 190)
        Label6.Name = "Label6"
        Label6.Size = New Size(76, 15)
        Label6.TabIndex = 20
        Label6.Text = "Department :"
        ' 
        ' cmbdept
        ' 
        cmbdept.FormattingEnabled = True
        cmbdept.Location = New Point(212, 187)
        cmbdept.Name = "cmbdept"
        cmbdept.Size = New Size(200, 23)
        cmbdept.TabIndex = 19
        ' 
        ' btnDeleteUser
        ' 
        btnDeleteUser.Location = New Point(1089, 288)
        btnDeleteUser.Name = "btnDeleteUser"
        btnDeleteUser.Size = New Size(125, 46)
        btnDeleteUser.TabIndex = 21
        btnDeleteUser.Text = "Delete User"
        btnDeleteUser.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Times New Roman", 21.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.Location = New Point(486, 12)
        Label7.Name = "Label7"
        Label7.Size = New Size(239, 32)
        Label7.TabIndex = 22
        Label7.Text = "Admin Dashboard"
        ' 
        ' btnaddl
        ' 
        btnaddl.Location = New Point(35, 245)
        btnaddl.Name = "btnaddl"
        btnaddl.Size = New Size(90, 59)
        btnaddl.TabIndex = 23
        btnaddl.Text = "Add Lab"
        btnaddl.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(35, 325)
        Button2.Name = "Button2"
        Button2.Size = New Size(90, 59)
        Button2.TabIndex = 24
        Button2.Text = "View/Manage Lab"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' AdminDashboard
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1230, 566)
        Controls.Add(Button2)
        Controls.Add(btnaddl)
        Controls.Add(Label7)
        Controls.Add(btnDeleteUser)
        Controls.Add(Label6)
        Controls.Add(cmbdept)
        Controls.Add(Label4)
        Controls.Add(UserIDT)
        Controls.Add(btnChangeRole)
        Controls.Add(btnChangePassword)
        Controls.Add(VmUser)
        Controls.Add(DataGridView1)
        Controls.Add(btnConfirmAddUser)
        Controls.Add(Label3)
        Controls.Add(cmbNewRole)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtNewPassword)
        Controls.Add(txtNewUsername)
        Controls.Add(btnAddUsers)
        Controls.Add(lblWelcome)
        Controls.Add(btnLogout)
        Name = "AdminDashboard"
        Text = "AdminDashboard"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnLogout As Button
    Friend WithEvents lblWelcome As Label
    Friend WithEvents btnAddUsers As Button
    Friend WithEvents txtNewUsername As TextBox
    Friend WithEvents txtNewPassword As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbNewRole As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnConfirmAddUser As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents VmUser As Button
    Friend WithEvents btnChangePassword As Button
    Friend WithEvents btnChangeRole As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents UserIDT As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbdept As ComboBox
    Friend WithEvents btnDeleteUser As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents btnaddl As Button
    Friend WithEvents Button2 As Button
End Class

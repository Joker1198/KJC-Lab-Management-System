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
        SuspendLayout()
        ' 
        ' btnLogout
        ' 
        btnLogout.Location = New Point(700, 26)
        btnLogout.Name = "btnLogout"
        btnLogout.Size = New Size(88, 28)
        btnLogout.TabIndex = 0
        btnLogout.Text = "Logout"
        btnLogout.UseVisualStyleBackColor = True
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Location = New Point(352, 267)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(0, 15)
        lblWelcome.TabIndex = 1
        ' 
        ' btnAddUsers
        ' 
        btnAddUsers.Location = New Point(35, 82)
        btnAddUsers.Name = "btnAddUsers"
        btnAddUsers.Size = New Size(68, 59)
        btnAddUsers.TabIndex = 2
        btnAddUsers.Text = "Add Users"
        btnAddUsers.UseVisualStyleBackColor = True
        ' 
        ' txtNewUsername
        ' 
        txtNewUsername.Location = New Point(588, 207)
        txtNewUsername.Name = "txtNewUsername"
        txtNewUsername.PlaceholderText = "College Mail"
        txtNewUsername.Size = New Size(200, 23)
        txtNewUsername.TabIndex = 3
        ' 
        ' txtNewPassword
        ' 
        txtNewPassword.Location = New Point(588, 259)
        txtNewPassword.Name = "txtNewPassword"
        txtNewPassword.PlaceholderText = "Assign Password"
        txtNewPassword.Size = New Size(200, 23)
        txtNewPassword.TabIndex = 4
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(516, 210)
        Label1.Name = "Label1"
        Label1.Size = New Size(66, 15)
        Label1.TabIndex = 5
        Label1.Text = "Username :"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(519, 267)
        Label2.Name = "Label2"
        Label2.Size = New Size(63, 15)
        Label2.TabIndex = 6
        Label2.Text = "Password :"
        ' 
        ' cmbNewRole
        ' 
        cmbNewRole.FormattingEnabled = True
        cmbNewRole.Location = New Point(588, 309)
        cmbNewRole.Name = "cmbNewRole"
        cmbNewRole.Size = New Size(200, 23)
        cmbNewRole.TabIndex = 7
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(541, 312)
        Label3.Name = "Label3"
        Label3.Size = New Size(41, 15)
        Label3.TabIndex = 8
        Label3.Text = "Roles :"
        ' 
        ' btnConfirmAddUser
        ' 
        btnConfirmAddUser.Location = New Point(610, 365)
        btnConfirmAddUser.Name = "btnConfirmAddUser"
        btnConfirmAddUser.Size = New Size(153, 23)
        btnConfirmAddUser.TabIndex = 9
        btnConfirmAddUser.Text = "Confirm And Add User"
        btnConfirmAddUser.UseVisualStyleBackColor = True
        ' 
        ' AdminDashboard
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
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
End Class

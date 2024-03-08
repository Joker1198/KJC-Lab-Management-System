<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class teachod
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
        VmUser = New Button()
        btnAddUsers = New Button()
        ComboBox1 = New ComboBox()
        DateTimePicker1 = New DateTimePicker()
        Button1 = New Button()
        Label5 = New Label()
        Label1 = New Label()
        ComboBox2 = New ComboBox()
        Label2 = New Label()
        SuspendLayout()
        ' 
        ' VmUser
        ' 
        VmUser.Location = New Point(12, 90)
        VmUser.Name = "VmUser"
        VmUser.Size = New Size(90, 59)
        VmUser.TabIndex = 26
        VmUser.Text = "View Previous Labs"
        VmUser.UseVisualStyleBackColor = True
        ' 
        ' btnAddUsers
        ' 
        btnAddUsers.Location = New Point(12, 12)
        btnAddUsers.Name = "btnAddUsers"
        btnAddUsers.Size = New Size(90, 59)
        btnAddUsers.TabIndex = 25
        btnAddUsers.Text = "Book A Lab"
        btnAddUsers.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(180, 52)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(200, 23)
        ComboBox1.TabIndex = 27
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(180, 107)
        DateTimePicker1.MaxDate = New Date(2024, 12, 31, 0, 0, 0, 0)
        DateTimePicker1.MinDate = New Date(2024, 1, 1, 0, 0, 0, 0)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(109, 23)
        DateTimePicker1.TabIndex = 28
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(225, 194)
        Button1.Name = "Button1"
        Button1.Size = New Size(116, 23)
        Button1.TabIndex = 29
        Button1.Text = "Request Lab"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(180, 34)
        Label5.Name = "Label5"
        Label5.Size = New Size(60, 15)
        Label5.TabIndex = 30
        Label5.Text = "Select Lab"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(180, 89)
        Label1.Name = "Label1"
        Label1.Size = New Size(65, 15)
        Label1.TabIndex = 31
        Label1.Text = "Select Date"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.FormattingEnabled = True
        ComboBox2.Location = New Point(180, 158)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(200, 23)
        ComboBox2.TabIndex = 32
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(180, 140)
        Label2.Name = "Label2"
        Label2.Size = New Size(100, 15)
        Label2.TabIndex = 33
        Label2.Text = "Request Materials"
        ' 
        ' teachod
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(527, 229)
        Controls.Add(Label2)
        Controls.Add(ComboBox2)
        Controls.Add(Label1)
        Controls.Add(Label5)
        Controls.Add(Button1)
        Controls.Add(DateTimePicker1)
        Controls.Add(ComboBox1)
        Controls.Add(VmUser)
        Controls.Add(btnAddUsers)
        Name = "teachod"
        Text = "teachod"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents VmUser As Button
    Friend WithEvents btnAddUsers As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Button1 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label2 As Label
End Class

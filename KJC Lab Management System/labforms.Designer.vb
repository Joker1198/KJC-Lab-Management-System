<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class labforms
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
        DataGridView1 = New DataGridView()
        Button1 = New Button()
        Button2 = New Button()
        TextBox1 = New TextBox()
        Label1 = New Label()
        Button4 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(12, 28)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(744, 187)
        DataGridView1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.Lime
        Button1.Location = New Point(12, 221)
        Button1.Name = "Button1"
        Button1.Size = New Size(101, 37)
        Button1.TabIndex = 2
        Button1.Text = "Approve"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.Red
        Button2.Location = New Point(12, 282)
        Button2.Name = "Button2"
        Button2.Size = New Size(101, 37)
        Button2.TabIndex = 3
        Button2.Text = "Decline"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(279, 250)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(228, 23)
        TextBox1.TabIndex = 5
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(279, 232)
        Label1.Name = "Label1"
        Label1.Size = New Size(85, 15)
        Label1.TabIndex = 7
        Label1.Text = "Material Name"
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(335, 282)
        Button4.Name = "Button4"
        Button4.Size = New Size(101, 37)
        Button4.TabIndex = 9
        Button4.Text = "Add Materials"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' labforms
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(768, 343)
        Controls.Add(Button4)
        Controls.Add(Label1)
        Controls.Add(TextBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(DataGridView1)
        Name = "labforms"
        Text = "labforms"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button4 As Button
End Class

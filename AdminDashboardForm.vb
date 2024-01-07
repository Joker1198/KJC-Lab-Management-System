Imports Microsoft.VisualBasic

Public Class AdminDashboardForm
    ' Your admin dashboard code goes here

    Private Sub AdminDashboardForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ' Ensure the application exits when the admin dashboard is closed
        Application.Exit()
    End Sub
End Class
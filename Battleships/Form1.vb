Public Class frmBattleship
    Private Sub frmBattleship_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblWelcome.Parent = PicBackground
        lblPlayerName.Parent = PicBackground
        btPlay.Parent = PicBackground
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btPlay_Click(sender As Object, e As EventArgs) Handles btPlay.Click
        frmGamePage.Show()

    End Sub

    Private Sub frmBattleship_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBattleship
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
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblPlayerName = New System.Windows.Forms.Label()
        Me.txtPlayerTwo = New System.Windows.Forms.TextBox()
        Me.btPlay = New System.Windows.Forms.Button()
        Me.PicBackground = New System.Windows.Forms.PictureBox()
        CType(Me.PicBackground, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblWelcome
        '
        Me.lblWelcome.AutoSize = True
        Me.lblWelcome.BackColor = System.Drawing.Color.Transparent
        Me.lblWelcome.Font = New System.Drawing.Font("Perpetua", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWelcome.Location = New System.Drawing.Point(-15, 0)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(1516, 147)
        Me.lblWelcome.TabIndex = 7
        Me.lblWelcome.Text = "WELCOME TO BATTLESHIP"
        '
        'lblPlayerName
        '
        Me.lblPlayerName.AutoSize = True
        Me.lblPlayerName.BackColor = System.Drawing.Color.Transparent
        Me.lblPlayerName.Font = New System.Drawing.Font("Perpetua", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlayerName.Location = New System.Drawing.Point(37, 347)
        Me.lblPlayerName.Name = "lblPlayerName"
        Me.lblPlayerName.Size = New System.Drawing.Size(437, 73)
        Me.lblPlayerName.TabIndex = 8
        Me.lblPlayerName.Text = "PLAYER NAME:"
        '
        'txtPlayerTwo
        '
        Me.txtPlayerTwo.Font = New System.Drawing.Font("Perpetua", 25.875!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlayerTwo.Location = New System.Drawing.Point(421, 347)
        Me.txtPlayerTwo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtPlayerTwo.Multiline = True
        Me.txtPlayerTwo.Name = "txtPlayerTwo"
        Me.txtPlayerTwo.Size = New System.Drawing.Size(300, 71)
        Me.txtPlayerTwo.TabIndex = 10
        '
        'btPlay
        '
        Me.btPlay.Font = New System.Drawing.Font("Perpetua", 25.875!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btPlay.Location = New System.Drawing.Point(1171, 594)
        Me.btPlay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btPlay.Name = "btPlay"
        Me.btPlay.Size = New System.Drawing.Size(217, 89)
        Me.btPlay.TabIndex = 12
        Me.btPlay.Text = "PLAY"
        Me.btPlay.UseVisualStyleBackColor = True
        '
        'PicBackground
        '
        Me.PicBackground.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PicBackground.Image = Global.Battleships.My.Resources.Resources.thumb_1920_98673
        Me.PicBackground.Location = New System.Drawing.Point(0, 0)
        Me.PicBackground.Name = "PicBackground"
        Me.PicBackground.Size = New System.Drawing.Size(1438, 733)
        Me.PicBackground.TabIndex = 6
        Me.PicBackground.TabStop = False
        '
        'frmBattleship
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(14.0!, 33.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1438, 733)
        Me.Controls.Add(Me.btPlay)
        Me.Controls.Add(Me.txtPlayerTwo)
        Me.Controls.Add(Me.lblPlayerName)
        Me.Controls.Add(Me.lblWelcome)
        Me.Controls.Add(Me.PicBackground)
        Me.Font = New System.Drawing.Font("Perpetua", 10.875!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmBattleship"
        Me.Text = "Battleship Start Page "
        CType(Me.PicBackground, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PicBackground As PictureBox
    Friend WithEvents lblWelcome As Label
    Friend WithEvents lblPlayerName As Label
    Friend WithEvents txtPlayerTwo As TextBox
    Friend WithEvents btPlay As Button
End Class

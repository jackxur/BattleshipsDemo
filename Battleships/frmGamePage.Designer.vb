<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGamePage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.rndVertical = New System.Windows.Forms.RadioButton()
        Me.rndHorizontal = New System.Windows.Forms.RadioButton()
        Me.btnReady = New System.Windows.Forms.Button()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rndVertical
        '
        Me.rndVertical.AutoSize = True
        Me.rndVertical.BackColor = System.Drawing.Color.Transparent
        Me.rndVertical.Font = New System.Drawing.Font("Perpetua Titling MT", 16.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rndVertical.Location = New System.Drawing.Point(404, 684)
        Me.rndVertical.Margin = New System.Windows.Forms.Padding(2)
        Me.rndVertical.Name = "rndVertical"
        Me.rndVertical.Size = New System.Drawing.Size(134, 30)
        Me.rndVertical.TabIndex = 3
        Me.rndVertical.TabStop = True
        Me.rndVertical.Text = "Vertical"
        Me.rndVertical.UseVisualStyleBackColor = False
        '
        'rndHorizontal
        '
        Me.rndHorizontal.AutoSize = True
        Me.rndHorizontal.BackColor = System.Drawing.Color.Transparent
        Me.rndHorizontal.Font = New System.Drawing.Font("Perpetua Titling MT", 16.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rndHorizontal.Location = New System.Drawing.Point(584, 684)
        Me.rndHorizontal.Margin = New System.Windows.Forms.Padding(2)
        Me.rndHorizontal.Name = "rndHorizontal"
        Me.rndHorizontal.Size = New System.Drawing.Size(179, 30)
        Me.rndHorizontal.TabIndex = 4
        Me.rndHorizontal.TabStop = True
        Me.rndHorizontal.Text = "Horizontal"
        Me.rndHorizontal.UseVisualStyleBackColor = False
        '
        'btnReady
        '
        Me.btnReady.Font = New System.Drawing.Font("Perpetua", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReady.Location = New System.Drawing.Point(942, 75)
        Me.btnReady.Margin = New System.Windows.Forms.Padding(2)
        Me.btnReady.Name = "btnReady"
        Me.btnReady.Size = New System.Drawing.Size(159, 62)
        Me.btnReady.TabIndex = 10
        Me.btnReady.Text = "Start"
        Me.btnReady.UseVisualStyleBackColor = True
        Me.btnReady.Visible = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.Battleships.My.Resources.Resources.smallship_2
        Me.PictureBox5.Location = New System.Drawing.Point(948, 172)
        Me.PictureBox5.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(125, 48)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 9
        Me.PictureBox5.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.Battleships.My.Resources.Resources.AircraftCarrier_2
        Me.PictureBox4.Location = New System.Drawing.Point(948, 328)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(150, 60)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox4.TabIndex = 8
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.Battleships.My.Resources.Resources.battleship_2
        Me.PictureBox3.Location = New System.Drawing.Point(776, 669)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(478, 225)
        Me.PictureBox3.TabIndex = 7
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Battleships.My.Resources.Resources.submarine_2
        Me.PictureBox2.Location = New System.Drawing.Point(948, 224)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(125, 40)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Battleships.My.Resources.Resources.destroyer_2
        Me.PictureBox1.Location = New System.Drawing.Point(948, 268)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(146, 56)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'frmGamePage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1198, 613)
        Me.Controls.Add(Me.btnReady)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.rndHorizontal)
        Me.Controls.Add(Me.rndVertical)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmGamePage"
        Me.Text = "frmGamePage"
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rndVertical As RadioButton
    Friend WithEvents rndHorizontal As RadioButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents btnReady As Button
End Class

Public Class frmGamePage
    'player define
    Private Const Computer = 1
    Private Const Player = 2
    Private Const Level = 2

    'boat define
    Private NumberOfComputerBoats As Integer = 3
    Private NumberOfPlayerBoats As Integer = 3
    Private BoatLengths(2, 4, 5) As Integer


    'game grid
    Private GridSize As Integer=30
    Private Grid_X As Integer=10
    Private Grid_Y As Integer=10
    Private playerButtons(Grid_X, Grid_Y), computerButtons(Grid_X, Grid_Y) As Button
    Private playerGrids(Grid_X, Grid_Y), computerGrids(Grid_X, Grid_Y) As PictureBox
    Private Position_X As Integer = GridSize
    Private Const Position_Y As Integer = 0

    'Color Define
    Private BUTTON_COLOR As Color = Drawing.Color.White
    Private BOAT_COLOR As Color = Drawing.Color.Black
    Private HIT_COLOR As Color = Drawing.Color.FromArgb(255, 192, 192)
    Private WATER_COLOR As Color = Drawing.Color.LightBlue
    Private FILTER_COLOR As Color = Drawing.Color.FromArgb(192, 255, 192)


    Private Const Minimum_Size As Integer = 20

    'boat pic
    Private computerPicBoat(NumberOfComputerBoats), playerPicBoat(NumberOfPlayerBoats) As PictureBox
    'boat define
    Private computerBoats(NumberOfComputerBoats, 8, 2), playerBoats(NumberOfPlayerBoats, 8, 2) As Integer

    Private computerElement(Grid_X + 1, Grid_Y + 1, 4), playerElement(Grid_X + 1, Grid_Y + 1, 4) As Boolean
    Private computerBoatNumber As Integer = 1

    Private computerBoatPart As Integer = 1
    'boat length
    Private computerBoatLength(NumberOfComputerBoats) As Integer
    Private playerBoatLength(NumberOfPlayerBoats) As Integer

    Private Const CanBoat As Integer = 3
    Private Const Restricted As Integer = 4
    Private Const IsBoat As Integer = 1

    Private Const Boat_X As Integer = 1
    Private Const Boat_Y As Integer = 2

    Private btnselected As Button


    Dim total As Integer = 0
    Private Sub frmGamePage_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.W Then
            PictureBox5.Top = PictureBox5.Top - 3
        End If

        If e.KeyCode = Keys.A Then
            PictureBox5.Left = PictureBox5.Left - 3
        End If

        If e.KeyCode = Keys.S Then
            PictureBox5.Top = PictureBox5.Top + 3
        End If

        If e.KeyCode = Keys.D Then
            PictureBox5.Left = PictureBox5.Left - 3
        End If
    End Sub



    Private Sub frmGamePage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BoatLengths(Player, Level, 1) = 2
        BoatLengths(Player, Level, 2) = 2
        BoatLengths(Player, Level, 3) = 3
        BoatLengths(Computer, Level, 1) = 2
        BoatLengths(Computer, Level, 2) = 2
        BoatLengths(Computer, Level, 3) = 3
        Me.WindowState = FormWindowState.Maximized

        ComputerPlay()
        AssignBoatLengths()
        MsgBox("Welcome to Battleship! You are playing head to head on a coordinate grid with the computer; whoever sinks all the opponent's ships is the champion.
You will set up your ships on an empty grid, and once the game starts, you will take turns guessing the locations of your opponent’s ships. 
There are 5 types of ships involved. Begin by selecting 5 consecutive grids.", MsgBoxStyle.OkOnly, "Instructions")

    End Sub
    Private Sub AssignBoatLengths()
        'Computer Boat
        For x = 1 To NumberOfComputerBoats
            computerBoatLength(x) = BoatLengths(Player, Level, x)
        Next

        'Player BOATS
        For x = 1 To NumberOfPlayerBoats
            playerBoatLength(x) = BoatLengths(Computer, Level, x)
        Next
    End Sub
    Private Sub ComputerPlay()
        ' initiates the actions upon the start of the computer's interactive side of the game
        MakeButtons(computerButtons)
        MakeGrids(computerGrids)
    End Sub

    Private Sub MakeButtons(ByVal button(,) As Button)
        Dim gridpoint As Point
        gridpoint.X = Position_X
        gridpoint.Y = Position_Y
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                button(x, y) = New Button
                button(x, y).Height = GridSize
                button(x, y).Width = GridSize
                gridpoint.X = gridpoint.X + GridSize
                button(x, y).Location = gridpoint
                button(x, y).BackColor = BUTTON_COLOR
                button(x, y).BringToFront()
                Me.Controls.Add(button(x, y))
                If button(x, y) Is computerButtons(x, y) Then
                    AddHandler button(x, y).Click, AddressOf ComputerButtons_click
                Else
                    'AddHandler button(x, y).Click, AddressOf PlayerButtons_click
                End If
            Next
            gridpoint.Y = gridpoint.Y + GridSize
            gridpoint.X = Position_X
        Next
    End Sub

    Private Sub MakeGrids(ByVal grid(,) As PictureBox)
        Dim gridpoint As Point
        gridpoint.X = Position_X
        gridpoint.Y = Position_Y
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                grid(x, y) = New PictureBox
                grid(x, y).Height = GridSize - 1
                grid(x, y).Width = GridSize - 1
                gridpoint.X = gridpoint.X + GridSize
                grid(x, y).Location = gridpoint
                grid(x, y).BackColor = WATER_COLOR
                Me.Controls.Add(grid(x, y))
            Next
            gridpoint.Y = gridpoint.Y + GridSize
            gridpoint.X = Position_X
        Next
    End Sub

    Private Sub ComputerButtons_click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnselected = sender
        'buttonshrink.Start()
        'computerBack.Visible = True
        ComputerPickProcedure()
    End Sub

    Private Sub ComputerPickProcedure()
        ComputerPlaceBoats()
        If computerBoatPart < computerBoatLength(computerBoatNumber) Then
            If computerBoatPart = 1 Then
                ComputerFirstFilter(computerBoats(computerBoatNumber, computerBoatPart, Boat_X), computerBoats(computerBoatNumber, computerBoatPart, Boat_Y), computerBoatLength(computerBoatNumber))
            Else
                ComputerSecondFilter(computerBoats(computerBoatNumber, computerBoatPart, Boat_X), computerBoats(computerBoatNumber, computerBoatPart, Boat_Y), computerBoatLength(computerBoatNumber))
            End If
            computerBoatPart = computerBoatPart + 1
        Else
            computerBoatPart = 1
            computerBoatNumber = computerBoatNumber + 1
            If computerBoatNumber <= NumberOfComputerBoats Then
                ComputerRestrict(computerBoatLength(computerBoatNumber))
            End If
        End If
        ComputerDisplay()
    End Sub
    Private Sub ComputerSecondFilter(ByVal intx As Integer, ByVal inty As Integer, ByVal length As Integer)
        For x = 0 To computerBoatPart - 1
            If intx = computerBoats(computerBoatNumber, computerBoatPart - 1, Boat_X) Then
                computerElement(intx, computerBoats(computerBoatNumber, computerBoatPart - x, Boat_Y) - 1, CanBoat) = True
                computerElement(intx, computerBoats(computerBoatNumber, computerBoatPart - x, Boat_Y) + 1, CanBoat) = True
            Else
                computerElement(computerBoats(computerBoatNumber, computerBoatPart - x, Boat_X) - 1, inty, CanBoat) = True
                computerElement(computerBoats(computerBoatNumber, computerBoatPart - x, Boat_X) + 1, inty, CanBoat) = True
            End If
        Next
    End Sub


    Private Sub ComputerFirstFilter(ByVal intx As Integer, ByVal inty As Integer, ByVal length As Integer)
        Const TEMPORARY As Integer = 0

        For x = 1 To length - 1
            If Not intx + x > Grid_X + 1 Then
                If computerElement(intx + x, inty, IsBoat) = True Then
                    For y = 1 To length - x
                        If Not intx - y < 0 Then
                            If computerElement(intx - y, inty, IsBoat) = True Then
                                computerElement(intx + 1, inty, TEMPORARY) = True
                                computerElement(intx - 1, inty, TEMPORARY) = True
                            End If
                        End If
                    Next
                End If
            End If
            If Not inty + x > Grid_Y + 1 Then
                If computerElement(intx, inty + x, IsBoat) = True Then
                    For y = 1 To length - x
                        If Not inty - y < 0 Then
                            If computerElement(intx, inty - y, IsBoat) = True Then
                                computerElement(intx, inty + 1, TEMPORARY) = True
                                computerElement(intx, inty - 1, TEMPORARY) = True
                            End If
                        End If
                    Next
                End If
            End If
        Next
        For x = -1 To 1 Step 2
            If computerElement(intx + x, inty, IsBoat) = False And computerElement(intx + x, inty, TEMPORARY) = False Then
                computerElement(intx + x, inty, CanBoat) = True
            End If
            If computerElement(intx, inty + x, IsBoat) = False And computerElement(intx, inty + x, TEMPORARY) = False Then
                computerElement(intx, inty + x, CanBoat) = True
            End If
        Next
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                computerElement(x, y, TEMPORARY) = False
            Next
        Next
    End Sub
    Private Sub ComputerRestrict(ByVal length As Integer)
        Const PROBABLE As Integer = 0
        For z = 1 To length - 1
            For k = 0 To z
                For y = 1 To Grid_Y
                    For x = 1 To Grid_X - z + 1
                        If computerElement(x + z, y, IsBoat) = True And computerElement(x - 1, y, IsBoat) = True Then
                            computerElement(x + k, y, PROBABLE) = True
                        End If
                    Next
                    For x = z To Grid_X
                        If computerElement(x - z, y, IsBoat) = True And computerElement(x + 1, y, IsBoat) = True Then
                            computerElement(x - k, y, PROBABLE) = True
                        End If
                    Next
                Next
            Next
        Next
        For z = 1 To length - 1
            For k = 0 To z
                For x = 1 To Grid_X
                    For y = 1 To Grid_Y - z + 1
                        If computerElement(x, y + z, IsBoat) = True And computerElement(x, y - 1, IsBoat) = True Then
                            If computerElement(x, y + k, PROBABLE) = True Then
                                computerElement(x, y + k, Restricted) = True
                            End If
                        End If
                    Next
                    For y = z To Grid_Y
                        If computerElement(x, y - z, IsBoat) = True And computerElement(x, y + 1, IsBoat) = True Then
                            If computerElement(x, y - k, PROBABLE) = True Then
                                computerElement(x, y - k, Restricted) = True
                            End If
                        End If
                    Next
                Next
            Next
        Next
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                computerElement(x, y, PROBABLE) = False
            Next
        Next
    End Sub
    Private Sub ComputerPlaceBoats()
        For x = 0 To Grid_X + 1
            computerElement(x, 0, IsBoat) = True
            computerElement(x, Grid_Y + 1, IsBoat) = True
        Next
        For y = 0 To Grid_Y + 1
            computerElement(0, y, IsBoat) = True
            computerElement(Grid_X + 1, y, IsBoat) = True
        Next
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                If computerButtons(x, y) Is btnselected Then
                    computerElement(x, y, IsBoat) = True
                    computerBoats(computerBoatNumber, computerBoatPart, Boat_X) = x
                    computerBoats(computerBoatNumber, computerBoatPart, Boat_Y) = y
                    If computerBoatPart = computerBoatLength(computerBoatNumber) Then
                        PickBoatPic(x, y)
                    End If
                End If
            Next
        Next

    End Sub
    Private Sub PickBoatPic(ByVal intx As Integer, ByVal inty As Integer)
        For x = 1 To NumberOfComputerBoats
            For y = 1 To computerBoatLength(x)
                If computerBoats(x, y, Boat_X) = intx And computerBoats(x, y, Boat_Y) = inty Then
                    Dim gridCounter(2, computerBoatLength(x)) As Integer
                    For z = 1 To computerBoatLength(x)
                        gridCounter(Boat_X, z) = computerBoats(x, z, Boat_X)
                        gridCounter(Boat_Y, z) = computerBoats(x, z, Boat_Y)
                    Next
                    If computerBoatLength(x) > 1 Then
                        If gridCounter(Boat_X, 1) = gridCounter(Boat_X, 2) Then
                            Dim minY As Integer = 100
                            For z = 1 To computerBoatLength(x)
                                If minY > gridCounter(Boat_Y, z) Then
                                    minY = gridCounter(Boat_Y, z)
                                End If
                            Next

                            MakePictureBoxes(computerPicBoat(computerBoatNumber), My.Resources.destroyer_2, computerGrids(gridCounter(Boat_X, 1), minY).Location.X, computerGrids(gridCounter(Boat_X, 1), minY).Location.Y, GridSize, computerBoatLength(x) * GridSize)
                        Else
                            Dim minX As Integer = 100
                            For z = 1 To computerBoatLength(x)
                                If minX > gridCounter(Boat_X, z) Then
                                    minX = gridCounter(Boat_X, z)
                                End If
                            Next

                            MakePictureBoxes(computerPicBoat(computerBoatNumber), My.Resources.smallship, computerGrids(minX, gridCounter(Boat_Y, 1)).Location.X, computerGrids(minX, gridCounter(Boat_Y, 1)).Location.Y, GridSize * computerBoatLength(x), GridSize)
                        End If
                    Else

                        MakePictureBoxes(computerPicBoat(computerBoatNumber), My.Resources.destroyer_2, computerGrids(intx, inty).Location.X, computerGrids(intx, inty).Location.Y, GridSize, GridSize)
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub ComputerDisplay()
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                If computerElement(x, y, CanBoat) = True Then
                    computerButtons(x, y).BackColor = FILTER_COLOR

                    computerButtons(x, y).Enabled = True
                    computerElement(x, y, CanBoat) = False
                Else
                    computerButtons(x, y).Enabled = False
                    computerButtons(x, y).BackColor = BUTTON_COLOR
                End If
                If computerElement(x, y, IsBoat) = True Then
                    computerGrids(x, y).BackColor = BOAT_COLOR
                End If
                If computerElement(x, y, Restricted) = True Then
                    computerButtons(x, y).BackColor = HIT_COLOR
                    computerButtons(x, y).Enabled = False
                    computerElement(x, y, Restricted) = False
                End If
            Next
        Next
        If computerBoatPart = 1 Then
            For x = 1 To Grid_X
                For y = 1 To Grid_Y
                    If computerButtons(x, y).BackColor <> HIT_COLOR Then
                        computerButtons(x, y).Enabled = True
                    End If
                Next
            Next
        End If
        If computerBoatNumber > NumberOfComputerBoats Then
            'computer boat place done
            For x = 1 To Grid_X
                For y = 1 To Grid_Y
                    computerButtons(x, y).Enabled = False
                    computerButtons(x, y).BackColor = WATER_COLOR
                Next
            Next
        End If
    End Sub

    Private Sub MakePictureBoxes(ByRef box As PictureBox, ByVal picture As Image, ByVal locationX As Integer, ByVal locationY As Integer, ByVal height As Integer, ByVal width As Integer)
        box = New PictureBox
        box.Image = picture
        box.SizeMode = PictureBoxSizeMode.StretchImage
        box.Location = New Point(locationX, locationY)
        box.Height = height
        box.Width = width
        box.Visible = True
        Me.Controls.Add(box)
        box.BringToFront()
    End Sub
    Private Sub btnReady_Click(sender As Object, e As EventArgs) Handles btnReady.Click


        'If chkbxA1.Checked And chkbxB1.Checked And chkbxD1.Checked And chkbxC1.Checked And chkbxE1.Checked Then
        'chkbxA1.Visible = True
        'chkbxB1.Visible = True
        'chkbxC1.Visible = True
        'chkbxD1.Visible = True
        'chkbxE1.Visible = True
        'chkbxF1.Visible = False
        'chkbxG1.Visible = False
        'chkbxH1.Visible = False
        'chkbxI1.Visible = False
        'chkbxJ1.Visible = False
        'else
        'chkbxA1.Visible = False
        'chkbxB1.Visible = False
        'chkbxC1.Visible = False
        'chkbxD1.Visible = False
        'chkbxE1.Visible = False
        'chkbxF1.Visible = False
        'chkbxG1.Visible = False
        'chkbxH1.Visible = False
        'chkbxI1.Visible = False
        '    chkbxJ1.Visible = False
        'End If

        ' If PictureBox1 And rndHorizontal.selected Then
        'PictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone)
        ' ElseIf PictureBox1 And rndVertical.selected Then
        'PictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone)
        'End If

    End Sub

    Private Sub btnOpponent_Click(sender As Object, e As EventArgs)
        frmOpponentsPage.Show()
    End Sub





    'If radDeleteShip.Selected = True Then
    'radDeleteShip.Visible = False
    'End If




End Class
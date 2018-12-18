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

    'chose    
    Private Const ComputerChose As Integer = 2
    Private Const PlayerChose As Integer = 2
    Private Const IsHit As Integer = 3

    Private pictureWinner As PictureBox

    Private xTEMP, yTEMP, intExplode As Integer

    '
    Private computerHit As Boolean = False
    Private playerBoatSank As Integer

    Private computerBoatSank As Integer

    Private buttonshrink, dropgrids, timerExplode, displayWinner, tmrTransition As Timer
    Private intTimer As Integer
    ' bonusTimer,
    Private Sub frmGamePage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BoatLengths(Player, Level, 1) = 2
        BoatLengths(Player, Level, 2) = 2
        BoatLengths(Player, Level, 3) = 3
        BoatLengths(Computer, Level, 1) = 2
        BoatLengths(Computer, Level, 2) = 2
        BoatLengths(Computer, Level, 3) = 3
        MakeTimers()
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
        btnReady.Visible = False

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
                    AddHandler button(x, y).Click, AddressOf PlayerButtons_click
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
        buttonshrink.Start()
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
            btnReady.Visible = True
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
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                Me.Controls.Remove(computerButtons(x, y))
            Next
        Next
        For x = 0 To NumberOfComputerBoats
            Me.Controls.Remove(computerPicBoat(x))
        Next
        dropgrids.Start()
        computerBoatNumber = 1
        computerBoatPart = 1
        PlayerPlay()
        btnReady.Visible = False

        'Me.Controls.Remove(computerBack)
        'Me.Controls.Remove(computerNext)
        'Me.Controls.Remove(computerReset)
        'lblScore.Visible = True
        'lblIndicate.Text = "Hit 'Em Down"

    End Sub





    Private Sub MakeTimers()
        tmrTransition = New Timer
        buttonshrink = New Timer
        'colorchange = New Timer
        dropgrids = New Timer
        'bonusTimer = New Timer
        displayWinner = New Timer
        timerExplode = New Timer
        buttonshrink.Interval = 20
        'colorchange.Interval = 20
        dropgrids.Interval = 1
        'bonusTimer.Interval = 100
        displayWinner.Interval = 5
        timerExplode.Interval = 50
        tmrTransition.Interval = 30
        AddHandler buttonshrink.Tick, AddressOf buttonShrink_tick
        AddHandler dropgrids.Tick, AddressOf dropGrids_tick
        'AddHandler bonusTimer.Tick, AddressOf bonusTimer_tick
        AddHandler displayWinner.Tick, AddressOf displayWinner_tick
        AddHandler timerExplode.Tick, AddressOf timerExplode_tick
        AddHandler tmrTransition.Tick, AddressOf tmrTransition_tick
        'AddHandler colorchange.Tick, AddressOf colorChange_tick
    End Sub
    'Private Sub bonusTimer_tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If playerElement(xTEMP, yTEMP, PlayerChose) = False Then
    '        playerButtons(xTEMP, yTEMP).Enabled = False
    '        playerButtons(xTEMP, yTEMP).BackColor = Color.FromArgb(192, 255, 192)
    '    End If
    '    yTEMP = yTEMP + 1
    '    If yTEMP = Grid_Y + 1 Then
    '        yTEMP = 1
    '        xTEMP = xTEMP + 1
    '    End If
    '    If xTEMP > Grid_X Then
    '        bonusTimer.Stop()
    '        winSequence()
    '    End If

    'End Sub
    Private Sub timerExplode_tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim formSize As Size = Me.Size
        Me.AutoSize = False
        Me.Size = formSize

        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                If playerElement(x, y, PlayerChose) = False Then
                    If y > Grid_Y \ 2 Then
                        playerButtons(x, y).BringToFront()
                        playerButtons(x, y).Location = New Point(playerButtons(x, y).Location.X + 20, playerButtons(x, y).Location.Y)
                    Else
                        playerButtons(x, y).BringToFront()
                        playerButtons(x, y).Location = New Point(playerButtons(x, y).Location.X - 20, playerButtons(x, y).Location.Y)
                    End If
                End If
            Next
        Next
        intExplode = intExplode + 1
        If intExplode = 20 Then
            sender.stop()
            For x = 1 To Grid_X
                For y = 1 To Grid_Y
                    Me.Controls.Remove(playerButtons(x, y))
                Next
            Next
            Me.AutoSize = True

        End If

    End Sub
    Private Sub displayWinner_tick(ByVal sender As System.Object, ByVal e As System.EventArgs) '
        If pictureWinner.Height < Grid_X * GridSize Then
            pictureWinner.Height = pictureWinner.Height + 2
        End If
        If pictureWinner.Width < Grid_Y * GridSize Then
            pictureWinner.Width = pictureWinner.Width + 2
        End If
        If pictureWinner.Location.X < Position_X + GridSize Then
            pictureWinner.Location = New Point(pictureWinner.Location.X + 2, pictureWinner.Location.Y)
        End If
        If pictureWinner.Location.Y < Position_Y Then
            pictureWinner.Location = New Point(pictureWinner.Location.X, pictureWinner.Location.Y + 2)
        End If

        If pictureWinner.Height >= Grid_X * GridSize And pictureWinner.Width >= Grid_Y * GridSize And pictureWinner.Location.X >= Position_X + GridSize And pictureWinner.Location.Y >= Position_Y Then
            sender.stop()
        End If
    End Sub

    Private Sub buttonShrink_tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If btnselected.Height > 0 Then
            btnselected.Height = btnselected.Height - 20
            btnselected.Width = btnselected.Width - 20
            btnselected.Left = btnselected.Left + 10
            btnselected.Top = btnselected.Top + 10
        Else
            sender.stop()
        End If
    End Sub
    Private Sub dropGrids_tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Const INT_CHANGE As Integer = 3
        If computerGrids(1, 1).Height > Minimum_Size Then
            For x = 1 To Grid_X
                For y = 1 To Grid_Y
                    computerGrids(x, y).Height = computerGrids(x, y).Height - INT_CHANGE
                    computerGrids(x, y).Width = computerGrids(x, y).Width - INT_CHANGE
                Next
            Next
            For y = 1 To Grid_Y
                computerGrids(1, y).Top = computerGrids(1, y).Top + Grid_X * GridSize / (GridSize - Minimum_Size + 1) * INT_CHANGE
            Next
            For x = 2 To Grid_X
                For y = 1 To Grid_Y
                    computerGrids(x, y).Top = computerGrids(x - 1, y).Top + computerGrids(x, y).Width + 1
                Next
            Next
            For x = 1 To Grid_X
                computerGrids(x, 1).Left = computerGrids(x, 1).Left + Grid_Y / 2 * INT_CHANGE
            Next
            For y = 2 To Grid_Y
                For x = 1 To Grid_X
                    computerGrids(x, y).Left = computerGrids(x, y - 1).Left + computerGrids(x, y).Width + 1
                Next
            Next
        ElseIf computerGrids(1, 1).Top < (Position_Y + (Grid_X + 1) * GridSize) Then
            For x = 1 To Grid_X
                For y = 1 To Grid_Y
                    computerGrids(x, y).Top = computerGrids(x, y).Top + INT_CHANGE
                Next
            Next
        Else
            sender.stop()
        End If
    End Sub

    Private Sub tmrTransition_tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        intTimer = intTimer + 1
        If intTimer = 10 Then
            sender.stop()
            intTimer = 0
            ComputerPickGrid()
        End If
    End Sub


    Private Sub ComputerPickGrid()
        Dim intx, inty As Integer
        If computerHit = True Then
            Do
                ComputerSmartenUp(intx, inty)
            Loop Until computerElement(intx, inty, ComputerChose) = False
        Else
            Do
                intx = RandomPositionX()
                inty = RandomPositionY()
            Loop Until computerElement(intx, inty, ComputerChose) = False
        End If

        computerElement(intx, inty, ComputerChose) = True
        computerGrids(intx, inty).BackColor = BUTTON_COLOR

        If ComputerIsHit(intx, inty) = True Then
            computerHit = True
            playerElement(intx, inty, IsHit) = True
            If ComputerShipSank(intx, inty) = True Then
                computerBoatSank = computerBoatSank + 1
                If computerBoatSank = NumberOfComputerBoats Then
                    'Computer has won
                    ComputerSinkBoat(intx, inty)
                    LoseSequence()
                    'lblIndicate.Text = "You lose"
                    For x = 1 To Grid_X
                        For y = 1 To Grid_Y
                            playerButtons(x, y).Enabled = False
                        Next
                    Next
                Else
                    computerHit = False
                    For x = 1 To NumberOfComputerBoats
                        For y = 1 To computerBoatLength(x)
                            If computerBoats(x, y, Boat_X) = intx And computerBoats(x, y, Boat_Y) = inty Then
                                For z = 1 To computerBoatLength(x)
                                    playerElement(computerBoats(x, z, Boat_X), computerBoats(x, z, Boat_Y), IsHit) = False
                                Next
                            End If
                        Next
                    Next
                    For x = 1 To Grid_X
                        For y = 1 To Grid_Y
                            If playerElement(x, y, IsHit) = True Then
                                computerHit = True
                            End If
                        Next
                    Next
                    'Computer has sank a boat
                    ComputerSinkBoat(intx, inty)
                End If
            Else
                'Computer has hit a boat

                computerGrids(intx, inty).Image = My.Resources.thumb_1920_98673 'Flame
                computerGrids(intx, inty).SizeMode = PictureBoxSizeMode.StretchImage
            End If
            computerGrids(intx, inty).BackColor = HIT_COLOR
        Else
        End If

    End Sub
    Private Sub ComputerSmartenUp(ByRef intx As Integer, ByRef inty As Integer)
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                If playerElement(x, y, IsHit) = True Then
                    intx = x
                    inty = y
                    If ((x + 1) <= Grid_X And playerElement(x + 1, y, IsHit) = True) Or ((x - 1) > 0 And playerElement(x - 1, y, IsHit) = True) Then
                        Dim xStart As Integer = x
                        Dim xEnd As Integer = x
                        While (xStart - 1 > 0)
                            If playerElement(xStart - 1, y, IsHit) = True Then
                                xStart = xStart - 1
                            Else
                                Exit While
                            End If
                        End While
                        While (xEnd + 1 <= Grid_X)
                            If playerElement(xEnd + 1, y, IsHit) = True Then
                                xEnd = xEnd + 1
                            Else
                                Exit While
                            End If
                        End While

                        If (computerElement(xStart - 1, y, ComputerChose) = True Or xStart - 1 <= 0) And (computerElement(xEnd + 1, y, ComputerChose) = True Or xEnd + 1 > Grid_X) Then
                            If RandomDirection() = 1 Then
                                If RandomDirection() = 1 Then
                                    If x + 1 <= Grid_X Then
                                        intx = x + 1
                                        inty = y
                                    Else
                                        intx = x - 1
                                        inty = y
                                    End If
                                Else
                                    If x - 1 > 0 Then
                                        intx = x - 1
                                        inty = y
                                    Else
                                        intx = x + 1
                                        inty = y
                                    End If
                                End If

                            Else
                                If RandomDirection() = 1 Then
                                    If y + 1 <= Grid_Y Then
                                        intx = x
                                        inty = y + 1
                                        Exit For
                                    Else
                                        intx = x
                                        inty = y - 1
                                    End If
                                Else
                                    If y - 1 > 0 Then
                                        intx = x
                                        inty = y - 1
                                    Else
                                        intx = x
                                        inty = y + 1
                                    End If
                                End If
                            End If
                            Exit Sub
                        End If
                        If RandomDirection() = 1 Then
                            If xStart - 1 > 0 Then
                                intx = xStart - 1
                                inty = y
                            End If
                        Else
                            If xStart + 1 <= Grid_X Then
                                intx = xEnd + 1
                                inty = y
                            End If
                        End If
                    ElseIf ((y + 1) <= Grid_Y And playerElement(x, y + 1, IsHit) = True) Or ((y - 1) > 0 And playerElement(x, y - 1, IsHit) = True) Then

                        Dim yStart As Integer = y
                        Dim yEnd As Integer = y
                        While (yStart - 1 > 0)
                            If playerElement(x, yStart - 1, IsHit) = True Then
                                yStart = yStart - 1
                            Else
                                Exit While
                            End If
                        End While
                        While (yEnd + 1 <= Grid_Y)
                            If playerElement(x, yEnd + 1, IsHit) = True Then
                                yEnd = yEnd + 1
                            Else
                                Exit While
                            End If
                        End While

                        If (computerElement(x, yStart - 1, ComputerChose) = True Or yStart - 1 <= 0) And (computerElement(x, yEnd + 1, ComputerChose) = True Or yEnd + 1 > Grid_Y) Then
                            If RandomDirection() = 1 Then
                                If RandomDirection() = 1 Then
                                    If x + 1 <= Grid_X Then
                                        intx = x + 1
                                        inty = y
                                    Else
                                        intx = x - 1
                                        inty = y
                                    End If
                                Else
                                    If x - 1 > 0 Then
                                        intx = x - 1
                                        inty = y
                                    Else
                                        intx = x + 1
                                        inty = y
                                    End If
                                End If

                            Else
                                If RandomDirection() = 1 Then
                                    If y + 1 <= Grid_Y Then
                                        intx = x
                                        inty = y + 1
                                        Exit For
                                    Else
                                        intx = x
                                        inty = y - 1
                                    End If
                                Else
                                    If y - 1 > 0 Then
                                        intx = x
                                        inty = y - 1
                                    Else
                                        intx = x
                                        inty = y + 1
                                    End If
                                End If
                            End If
                            Exit Sub
                        End If

                        If RandomDirection() = 1 Then
                            If yStart - 1 > 0 Then
                                intx = x
                                inty = yStart - 1
                            End If
                        Else
                            If yEnd + 1 <= Grid_Y Then
                                intx = x
                                inty = yEnd + 1
                            End If
                        End If
                    Else
                        If RandomDirection() = 1 Then
                            If RandomDirection() = 1 Then
                                If x + 1 <= Grid_X Then
                                    intx = x + 1
                                    inty = y
                                Else
                                    intx = x - 1
                                    inty = y
                                End If
                            Else
                                If x - 1 > 0 Then
                                    intx = x - 1
                                    inty = y
                                Else
                                    intx = x + 1
                                    inty = y
                                End If
                            End If

                        Else
                            If RandomDirection() = 1 Then
                                If y + 1 <= Grid_Y Then
                                    intx = x
                                    inty = y + 1
                                    Exit For
                                Else
                                    intx = x
                                    inty = y - 1
                                End If
                            Else
                                If y - 1 > 0 Then
                                    intx = x
                                    inty = y - 1
                                Else
                                    intx = x
                                    inty = y + 1
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Next
    End Sub
    Private Function ComputerIsHit(ByVal intx As Integer, ByVal inty As Integer) As Boolean
        If computerElement(intx, inty, IsBoat) = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function ComputerShipSank(ByVal intx As Integer, ByVal inty As Integer) As Boolean
        For x = 1 To NumberOfComputerBoats
            For y = 1 To computerBoatLength(x)
                If computerBoats(x, y, Boat_X) = intx And computerBoats(x, y, Boat_Y) = inty Then
                    For z = 1 To computerBoatLength(x)
                        If computerElement(computerBoats(x, z, Boat_X), computerBoats(x, z, Boat_Y), ComputerChose) = False Then
                            Return False
                        End If
                    Next
                End If
            Next
        Next
        Return True
    End Function
    Private Sub ComputerSinkBoat(ByVal intx As Integer, ByVal inty As Integer)
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
                            MakePictureBoxes(computerPicBoat(computerBoatSank), My.Resources.smallship, computerGrids(gridCounter(Boat_X, 1), minY).Location.X, computerGrids(gridCounter(Boat_X, 1), minY).Location.Y, Minimum_Size - 1, computerBoatLength(x) * (Minimum_Size - 1))
                        Else
                            Dim minX As Integer = 100
                            For z = 1 To computerBoatLength(x)
                                If minX > gridCounter(Boat_X, z) Then
                                    minX = gridCounter(Boat_X, z)
                                End If
                            Next
                            MakePictureBoxes(computerPicBoat(computerBoatSank), My.Resources.smallship_2, computerGrids(minX, gridCounter(Boat_Y, 1)).Location.X, computerGrids(minX, gridCounter(Boat_Y, 1)).Location.Y, (Minimum_Size - 1) * computerBoatLength(x), Minimum_Size - 1)
                        End If
                    Else
                        MakePictureBoxes(computerPicBoat(computerBoatSank), My.Resources.smallship, computerGrids(intx, inty).Location.X, computerGrids(intx, inty).Location.Y, Minimum_Size - 1, Minimum_Size - 1)
                    End If
                End If
            Next
        Next
    End Sub
    Private Sub LoseSequence()
        'If MainMenu.blnBGM = True Then
        '    My.Computer.Audio.Play(My.Resources.Titanic, AudioPlayMode.BackgroundLoop)
        'End If
        'gameover
        MakePictureBoxes(pictureWinner, My.Resources.destroyer_2, Position_X + GridSize, Position_Y, Grid_X * GridSize, Grid_Y * GridSize)
        timerExplode.Start()
        MsgBox("You Lose")
        EndSequence()
    End Sub
    Private Sub winSequence()
        'Winner
        MakePictureBoxes(pictureWinner, My.Resources.smallship, 0, 0, 1, 1)
        displayWinner.Start()
        EndSequence()
        'RecordScore(Score)
    End Sub
    Private Sub EndSequence()
        'PlayerReset.Width = PlayerReset.Width * 2
        'PlayerReset.Text = "Play Again"
        'picWin.BringToFront()
        'PlayerReset.BringToFront()
        'btnMainMenu.Visible = False
        'btnQuit.Visible = False
        'btnInstructions.Visible = False
        ' btnSettings.Visible = False
        'PlayerReset.Location = New Point(Grid_Y / 2 * GridSize, Grid_X * GridSize)
    End Sub

    Private Sub PlayerPlay()
        'subroutine that initiates the actions upon the start of the player's interactive side of the game
        MakeButtons(playerButtons)
        MakeGrids(playerGrids)
        'dynCreateButton(PlayerReset, Position_X + (Grid_Y + 1) * GridSize, GlobalVariables.Position_Y + (Grid_X + 4) * GridSize, 2 * GridSize, GridSize, "Reset")
        PlayerPlaceBoats()
    End Sub
    Private Function RandomPositionX() As Integer
        Return Int((Rnd() * (Grid_X - 1)) + 1)
    End Function
    Private Function RandomPositionY() As Integer
        Return Int((Rnd() * (Grid_Y - 1)) + 1)
    End Function
    Private Function RandomDirection() As Integer
        Return Int(Rnd() * 2 + 1)
    End Function

    Private Sub PlayerPlaceBoats()
        PlayerInitBoats()

        For x = 1 To NumberOfPlayerBoats
            For y = 1 To playerBoatLength(x)
                playerElement(playerBoats(x, y, Boat_X), playerBoats(x, y, Boat_Y), IsBoat) = True
            Next
        Next
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                If playerElement(x, y, IsBoat) = True Then
                    playerGrids(x, y).BackColor = BOAT_COLOR
                End If
            Next
        Next
    End Sub

    Private Sub PlayerInitBoats()
        Dim check As Boolean = True
        Dim direction(NumberOfPlayerBoats) As Integer
        For x = 1 To NumberOfPlayerBoats
            direction(x) = RandomDirection()
        Next
        Do
            check = True
            For x = 1 To NumberOfPlayerBoats
                playerBoats(x, 1, Boat_X) = RandomPositionX()
                playerBoats(x, 1, Boat_Y) = RandomPositionY()
                For y = 2 To playerBoatLength(x)
                    If direction(x) = 1 Then
                        playerBoats(x, y, Boat_Y) = playerBoats(x, 1, Boat_Y)
                        playerBoats(x, y, Boat_X) = playerBoats(x, y - 1, Boat_X) + 1
                        If playerBoats(x, y, Boat_X) > Grid_X Then
                            playerBoats(x, y, Boat_X) = playerBoats(x, 1, Boat_X) - 1
                        End If
                        For z = 1 To y - 1
                            If playerBoats(x, y, Boat_X) = playerBoats(x, y - z, Boat_X) Then
                                playerBoats(x, y, Boat_X) = playerBoats(x, y, Boat_X) - 1
                            End If
                        Next
                    Else
                        playerBoats(x, y, Boat_X) = playerBoats(x, 1, Boat_X)
                        playerBoats(x, y, Boat_Y) = playerBoats(x, y - 1, Boat_Y) + 1
                        If playerBoats(x, y, Boat_Y) > Grid_Y Then
                            playerBoats(x, y, Boat_Y) = playerBoats(x, 1, Boat_Y) - 1
                        End If
                        For z = 1 To y - 1
                            If playerBoats(x, y, Boat_Y) = playerBoats(x, y - z, Boat_Y) Then
                                playerBoats(x, y, Boat_Y) = playerBoats(x, y, Boat_Y) - 1
                            End If
                        Next
                    End If
                Next
            Next
            For x = 1 To NumberOfPlayerBoats
                For y = 1 To playerBoatLength(x)
                    For a = 1 To NumberOfPlayerBoats
                        For b = 1 To playerBoatLength(a)
                            If playerBoats(x, y, Boat_X) = playerBoats(a, b, Boat_X) And playerBoats(x, y, Boat_Y) = playerBoats(a, b, Boat_Y) Then
                                If Not (x = a And y = b) Then
                                    check = False
                                End If
                            End If
                        Next
                    Next
                Next
            Next
        Loop Until check = True
    End Sub


    Private Sub PlayerButtons_click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'handles all the action in the player's interactive grid
        Dim intx, inty As Integer
        btnselected = sender
        buttonshrink.Start()
        For x = 1 To Grid_X
            For y = 1 To Grid_Y
                If playerButtons(x, y) Is sender Then
                    intx = x
                    inty = y
                    playerElement(x, y, PlayerChose) = True
                End If
            Next
        Next
        'upon player is hit, change grid background picture
        If PlayerIsHit(intx, inty) = True Then
            'playerGrids(intx, inty).Image = My.Resources.Flame 'changes image into a little flame:)
            playerGrids(intx, inty).SizeMode = PictureBoxSizeMode.StretchImage
            If PlayerShipSank(intx, inty) = True Then
                PlayerBoatSink(intx, inty)
                playerBoatSank = playerBoatSank + 1
                If playerBoatSank = NumberOfPlayerBoats Then
                    'Player has sank all boats
                    'CountScore(Score, GlobalVariables.SANK, lblScore) 'adds points onto the player's score 
                    'lblIndicate.Text = "SANK!"
                    xTEMP = 1
                    yTEMP = 1
                    'lblIndicate.Text = "Bonus!"
                    'starts the bonus points sequence
                    bonusTimer.Start()
                    'if the background music option is on, play victory song
                    'If MainMenu.blnBGM = True Then
                    '    My.Computer.Audio.Play(My.Resources.BlissfulVictory, AudioPlayMode.BackgroundLoop)
                    'End If
                    For x = 1 To Grid_X
                        For y = 1 To Grid_Y
                            playerButtons(x, y).Enabled = False
                        Next
                    Next
                    Exit Sub
                Else
                    'Player has sank a boat
                    'CountScore(Score, GlobalVariables.SANK, lblScore)
                    'lblIndicate.Text = "SANK!"
                    'PlaySound("myAudio", "BombFalling.mp3")
                End If
            Else
                'Player has hit a boat
                'CountScore(Score, GlobalVariables.HIT, lblScore)
                'lblIndicate.Text = "HIT!"

                'PlaySound("myAudio", "Explosion.wav")
            End If
        Else
            'lblIndicate.Text = "MISS!"
            'PlaySound("myAudio", "Splash.wav")

        End If

        tmrTransition.Start()
    End Sub

    Private Function PlayerIsHit(ByVal intx As Integer, ByVal inty As Integer) As Boolean
        If playerElement(intx, inty, IsBoat) = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function PlayerShipSank(ByVal intx As Integer, ByVal inty As Integer) As Boolean
        For x = 1 To NumberOfPlayerBoats
            For y = 1 To playerBoatLength(x)
                If playerBoats(x, y, Boat_X) = intx And playerBoats(x, y, Boat_Y) = inty Then
                    For z = 1 To playerBoatLength(x)
                        If playerElement(playerBoats(x, z, Boat_X), playerBoats(x, z, Boat_Y), PlayerChose) = False Then
                            Return False
                        End If
                    Next
                End If
            Next
        Next
        Return True
    End Function
    Private Sub PlayerBoatSink(ByVal intx As Integer, ByVal inty As Integer)
        For x = 1 To NumberOfPlayerBoats
            For y = 1 To playerBoatLength(x)
                If playerBoats(x, y, Boat_X) = intx And playerBoats(x, y, Boat_Y) = inty Then
                    Dim gridCounter(2, playerBoatLength(x)) As Integer
                    For z = 1 To playerBoatLength(x)
                        gridCounter(Boat_X, z) = playerBoats(x, z, Boat_X)
                        gridCounter(Boat_Y, z) = playerBoats(x, z, Boat_Y)
                    Next
                    If playerBoatLength(x) > 1 Then
                        If gridCounter(Boat_X, 1) = gridCounter(Boat_X, 2) Then
                            Dim minY As Integer = 100
                            For z = 1 To playerBoatLength(x)
                                If minY > gridCounter(Boat_Y, z) Then
                                    minY = gridCounter(Boat_Y, z)
                                End If
                            Next
                            'Resources.Ship_Wendy_Picked_
                            MakePictureBoxes(playerPicBoat(playerBoatSank), My.Resources.battleship_2, playerGrids(gridCounter(Boat_X, 1), minY).Location.X, playerGrids(gridCounter(Boat_X, 1), minY).Location.Y, GridSize, playerBoatLength(x) * GridSize)
                        Else
                            Dim minX As Integer = 100
                            For z = 1 To playerBoatLength(x)
                                If minX > gridCounter(Boat_X, z) Then
                                    minX = gridCounter(Boat_X, z)
                                End If
                            Next
                            '.BoatY
                            MakePictureBoxes(playerPicBoat(playerBoatSank), My.Resources.destroyer_2, playerGrids(minX, gridCounter(Boat_Y, 1)).Location.X, playerGrids(minX, gridCounter(Boat_Y, 1)).Location.Y, GridSize * playerBoatLength(x), GridSize)
                        End If
                    Else
                        'Ship_Wendy_Picked_
                        MakePictureBoxes(playerPicBoat(playerBoatSank), My.Resources.battleship_2, playerGrids(intx, inty).Location.X, playerGrids(intx, inty).Location.Y, GridSize, GridSize)
                    End If
                End If
            Next
        Next
    End Sub
End Class
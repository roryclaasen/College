﻿Module Module1
    'Skeleton Program code for the AQA COMP1 Summer 2010 examination
    'this code should be used in conjunction with the Preliminary Materials
    'written by the AQA COMP1 Programmer Team
    'developed in the Visual Studio 2008 (Console Mode) programming environment (VB.NET)

    Dim Board(3, 3) As Char
    Dim PlayerOneName As String
    Dim PlayerTwoName As String
    Dim PlayerOneScore As Single
    Dim PlayerTwoScore As Single
    Dim XCoord As Integer
    Dim YCoord As Integer
    Dim NoOfMoves As Integer
    Dim ValidMove As Boolean
    Dim GameHasBeenWon As Boolean
    Dim GameHasBeenDrawn As Boolean
    Dim CurrentSymbol As Char
    Dim StartSymbol As Char
    Dim PlayerOneSymbol As Char
    Dim PlayerTwoSymbol As Char
    Dim Answer As Char

    Sub Main()
        Console.Title = "Skeleton Program for the AQA COMP1 Summer 2010 examination"
        Randomize()
        WriteTitle()
        PlayerOneName = getInput("What is the name of player one?")
        PlayerTwoName = getInput("What is the name of player two?")
        Console.Clear()
        WriteTitle()
        PlayerOneScore = 0
        PlayerTwoScore = 0
        Do 'Choose player one’s symbol
            PlayerOneSymbol = getInput(PlayerOneName & " what symbol do you wish to use, X or O? ")

            Console.WriteLine()
            If Not (PlayerOneSymbol = "X" Or PlayerOneSymbol = "O") Then
                Console.WriteLine("Symbol to play must be uppercase X or O")
                Console.WriteLine()
            End If
        Loop Until PlayerOneSymbol = "X" Or PlayerOneSymbol = "O"
        If PlayerOneSymbol = "X" Then
            PlayerTwoSymbol = "O"
        Else
            PlayerTwoSymbol = "X"
        End If
        StartSymbol = GetWhoStarts()
        Do 'Play a game
            Console.Clear()
            NoOfMoves = 0
            GameHasBeenDrawn = False
            GameHasBeenWon = False
            ClearBoard(Board)
            WriteTitle()
            Console.WriteLine()
            DisplayBoard(Board)
            Console.WriteLine()
            CurrentSymbol = StartSymbol
            Do 'Play until a player wins or the game is drawn
                If CurrentSymbol = PlayerOneSymbol Then
                    Console.WriteLine("It is " & PlayerOneName & "'s turn, you are " & CurrentSymbol)
                Else
                    Console.WriteLine("It is " & PlayerTwoName & "'s turn, you are " & CurrentSymbol)
                End If
                Do 'Get a valid move
                    GetMoveCoordinates(XCoord, YCoord)
                    ValidMove = CheckValidMove(XCoord, YCoord, Board)
                    If Not ValidMove Then Console.WriteLine("Coordinates invalid, please try again")
                Loop Until ValidMove
                Board(XCoord, YCoord) = CurrentSymbol
                Console.Clear()
                WriteTitle()
                Console.WriteLine()
                DisplayBoard(Board)
                Console.WriteLine()
                GameHasBeenWon = CheckXOrOHasWon(Board)
                NoOfMoves = NoOfMoves + 1
                If Not GameHasBeenWon Then
                    If NoOfMoves = 9 Then 'Check if maximum number of allowed moves has been reached
                        GameHasBeenDrawn = True
                    Else
                        If CurrentSymbol = "X" Then
                            CurrentSymbol = "O"
                        Else
                            CurrentSymbol = "X"
                        End If
                    End If
                End If
            Loop Until GameHasBeenWon Or GameHasBeenDrawn
            If GameHasBeenWon Then ' Update scores and display result
                If PlayerOneSymbol = CurrentSymbol Then
                    Console.WriteLine(PlayerOneName & " congratulations you win!")
                    PlayerOneScore = PlayerOneScore + 1
                Else
                    Console.WriteLine(PlayerTwoName & " congratulations you win!")
                    PlayerTwoScore = PlayerTwoScore + 1
                End If
            Else
                Console.WriteLine("A draw this time!")
            End If
            Console.WriteLine()
            Console.WriteLine(PlayerOneName & " your score is: " & PlayerOneScore)
            Console.WriteLine(PlayerTwoName & " your score is: " & PlayerTwoScore)
            Console.WriteLine()
            If StartSymbol = PlayerOneSymbol Then
                StartSymbol = PlayerTwoSymbol
            Else
                StartSymbol = PlayerOneSymbol
            End If
            Console.Write("Another game Y/N?")
            Answer = Console.ReadLine()
        Loop Until Answer = "N" Or Answer = "n"
    End Sub

    Sub WriteTitle()
        Console.WriteLine(Console.Title)
        Console.WriteLine("-------------------------------------------------------------------------------")
    End Sub

    Sub DisplayBoard(ByVal Board(,) As Char)
        Dim Row As Integer
        Dim Column As Integer
        Console.WriteLine("  | 1 2 3 ")
        Console.WriteLine("--+-------")
        For Row = 1 To 3
            Console.Write(Row & " | ")
            For Column = 1 To 3
                Console.Write(Board(Column, Row) & " ")
            Next
            Console.WriteLine()
        Next
    End Sub

    Sub ClearBoard(ByRef Board(,) As Char)
        Dim Row As Integer
        Dim Column As Integer
        For Row = 1 To 3
            For Column = 1 To 3
                Board(Column, Row) = " "
            Next
        Next
    End Sub

    Sub GetMoveCoordinates(ByRef XCoordinate As Integer, ByRef YCoordinate As Integer)
        Dim input As String
        Do
            input = getInput("Enter x coordinate")
        Loop Until (IsNumeric(input))
        XCoordinate = input
        input = vbNullString
        Do
            input = getInput("Enter y coordinate")
        Loop Until (IsNumeric(input))
        YCoordinate = input
        Console.WriteLine()
    End Sub

    Function CheckValidMove(ByVal XCoordinate As Integer, ByVal YCoordinate As Integer, _
ByVal Board(,) As Char)
        Dim ValidMove As Boolean
        ValidMove = True
        'Check x coordinate is valid
        If XCoordinate < 1 Or XCoordinate > 3 Then ValidMove = False
        'Check y coordinate is valid
        If YCoordinate < 1 Or YCoordinate > 3 Then ValidMove = False
        'Check coordinates are empty
        If Board(XCoordinate, YCoordinate) = PlayerOneSymbol Or Board(XCoordinate, YCoordinate) = PlayerTwoSymbol Then ValidMove = False
        CheckValidMove = ValidMove
    End Function

    Function CheckXOrOHasWon(ByVal Board(,) As Char) As Boolean
        Dim Row As Integer
        Dim Column As Integer
        Dim XOrOHasWon As Boolean
        XOrOHasWon = False
        For Column = 1 To 3
            If Board(Column, 1) = Board(Column, 2) And Board(Column, 2) = Board(Column, 3) _
                                    And Board(Column, 2) <> " " Then XOrOHasWon = True
        Next
        For Row = 1 To 3
            If Board(1, Row) = Board(2, Row) And Board(2, Row) = Board(3, Row) _
                            And Board(2, Row) <> " " Then XOrOHasWon = True
        Next
        CheckXOrOHasWon = XOrOHasWon
    End Function

    Function GetWhoStarts() As Char
        Dim RandomNo As Integer
        Dim WhoStarts As Char
        RandomNo = Rnd() * 100
        If RandomNo Mod 2 = 0 Then
            WhoStarts = "X"
        Else
            WhoStarts = "O"
        End If
        GetWhoStarts = WhoStarts
    End Function

    Function getInput(text As String) As String
        Console.WriteLine(text)
        Console.Write("> ")
        Dim input As String = Console.ReadLine()
        If Len(input) > 0 Then Return input
        Return getInput(text)
    End Function
End Module
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System;
using System.IO;
using System.Collections.Generic;

[TestClass]
public class GameStartNewGameTests
{
    private Game game;
    private Level level1;
    private Level level2;
    private Position startPosition;
    private Player testPlayer;

    [TestInitialize]
    public void SetUp()
    {
        level1 = CreateLevel1();
        level2 = CreateLevel2();
        startPosition = new Position(0, 0);
        testPlayer = new Player(startPosition, PieceType.King, new Rules());
        var levels = new List<ILevel> { level1, level2 }; // List of levels
        game = new Game(levels);
    }

    [TestMethod]
    public void TestStartNewGame_SuccessfulStart()
    {
        // Act
        game.StartNewGame(level1);

        // Assert
        Assert.IsNotNull(game.CurrentLevel, "The game board should be initialized.");
        Assert.AreEqual(startPosition, game.CurrentLevel.Player.CurrentPosition, "The player's position should be set to the start position.");
    }

    [TestMethod]
    public void TestStartNewGame_UnsuccessfulStart_GameAlreadyInProgress()
    {
        // Arrange
        game.StartNewGame(level1); // Start the first game

        // Capture the console output
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Act
            game.StartNewGame(level2); // Attempt to start a new game while one is already in progress

            // Assert
            var output = sw.ToString().Trim();
            Assert.AreEqual("Warning: Starting a new game will reset the current game.", output, "The player should receive a warning message indicating that starting a new game will reset the current game.");
        }
    }

    private Level CreateLevel1()
    {
        var board = new Board(8, 8, new Rules());
        var startPosition = new Position(0, 0);
        var endPosition = new Position(7, 7);
        var player = new Player(startPosition, PieceType.King, new Rules());

        // Hard code the pieces for level 1
        board.PlacePiece(new Piece(PieceType.Rook), new Position(0, 1));
        board.PlacePiece(new Piece(PieceType.Bishop), new Position(1, 1));
        // Add more pieces as needed

        return new Level(board, startPosition, endPosition, player);
    }

    private Level CreateLevel2()
    {
        var board = new Board(8, 8, new Rules());
        var startPosition = new Position(0, 0);
        var endPosition = new Position(7, 7);
        var player = new Player(startPosition, PieceType.Knight, new Rules());

        // Hard code the pieces for level 2
        board.PlacePiece(new Piece(PieceType.Rook), new Position(0, 1));
        board.PlacePiece(new Piece(PieceType.Pawn), new Position(1, 1));
        // Add more pieces as needed

        return new Level(board, startPosition, endPosition, player);
    }
}

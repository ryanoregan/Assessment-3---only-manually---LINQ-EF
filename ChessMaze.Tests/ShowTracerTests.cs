using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameTracerTests
{
    private Game game;
    private Level testLevel;
    private Player testPlayer;
    private Board testBoard;
    private Position startPosition;
    private Position validPosition;

    [TestInitialize]
    public void SetUp()
    {
        startPosition = new Position(0, 0);
        validPosition = new Position(0, 1); // Assuming this is a valid move for the piece
        testPlayer = new Player(startPosition, PieceType.Rook, new Rules());
        testBoard = new Board(8, 8, new Rules());
        testLevel = new Level(testBoard, startPosition, new Position(7, 7), testPlayer);
        var levels = new List<ILevel> { testLevel }; // List of levels
        game = new Game(levels);
        game.LoadLevel(testLevel);
    }

    [TestMethod]
    public void TestShowTracer_SuccessfulTracerDisplay()
    {
        // Act
        game.MakeMove(validPosition);
        string tracer = game.ShowTracer();

        // Assert
        Assert.AreEqual($"Tracer: (0, 0) -> (0, 1)", tracer, "The tracer should display the path the player has taken.");
    }

    [TestMethod]
    public void TestShowTracer_UnsuccessfulTracerDisplay_NoMovesMade()
    {
        // Act
        string tracer = game.ShowTracer();

        // Assert
        Assert.AreEqual("No moves have been made.", tracer, "The player should receive a message indicating that no moves have been made.");
    }
}

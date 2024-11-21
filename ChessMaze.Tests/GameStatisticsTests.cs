using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System;
using System.Collections.Generic;

[TestClass]
public class GameStatisticsTests
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
    public void TestDisplayStatistics_SuccessfulDisplay()
    {
        // Act
        game.MakeMove(validPosition);
        string statistics = game.DisplayStatistics();

        // Assert
        Assert.IsTrue(statistics.Contains("Moves Made: 1"), "The statistics should display the number of moves made.");
        Assert.IsTrue(statistics.Contains("Levels Completed: 0"), "The statistics should display the number of levels completed.");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCastException))]
    public void TestDisplayStatistics_ThrowsExceptionForNonStringType()
    {
        // Act
        var statistics = game.DisplayStatistics();

        // This cast should fail and throw an InvalidCastException,
        // which is what we want to test for.
        var castedStatistics = (int)(object)statistics;

        // Assert is not necessary here due to ExpectedException attribute
    }
}

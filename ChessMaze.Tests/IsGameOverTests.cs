using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameLevelCompletionTests
{
    private Game game;
    private Level testLevel;
    private Player testPlayer;
    private Board testBoard;
    private Position startPosition;
    private Position endPosition;
    private Position invalidPosition;

    [TestInitialize]
    public void SetUp()
    {
        startPosition = new Position(6, 7); // One legal move away from the end position
        endPosition = new Position(7, 7); // The designated end position
        invalidPosition = new Position(6, 6); // Moves to a position that is not the end
        testPlayer = new Player(startPosition, PieceType.King, new Rules());
        testBoard = new Board(8, 8, new Rules());
        testLevel = new Level(testBoard, startPosition, endPosition, testPlayer);
        var levels = new List<ILevel> { testLevel }; // List of levels
        game = new Game(levels);
        game.LoadLevel(testLevel);
    }

    [TestMethod]
    public void TestLevelCompletion_SuccessfulCompletion()
    {
        // Act
        bool moveMade = game.MakeMove(endPosition);

        // Assert
        Assert.IsTrue(moveMade, "The piece should move to the end position.");
        Assert.IsTrue(testLevel.IsCompleted, "The level should be marked as completed.");
    }

    [TestMethod]
    public void TestLevelCompletion_UnsuccessfulCompletion_InvalidEndPosition()
    {
        // Act
        bool moveMade = game.MakeMove(invalidPosition);

        // Assert
        Assert.IsFalse(testLevel.IsCompleted, "The level should not be marked as completed.");
    }
}

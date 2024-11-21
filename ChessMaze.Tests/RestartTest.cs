using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameRestartTests
{
    [TestMethod]
    public void RestartLevel_SuccessfulRestart()
    {
        // Arrange
        var board = new Board(8, 8, new Rules());
        var startPosition = new Position(0, 0); // Starting position is (0,0)
        var player = new Player(startPosition, PieceType.King, new Rules());
        var level = new Level(board, startPosition, new Position(7, 7), player); // End position is (7,7)
        var levels = new List<ILevel> { level }; // List of levels
        var game = new Game(levels);
        game.LoadLevel(level);

        // Ensure the newPosition is valid and within the board boundaries
        var newPosition = new Position(0, 1); // A valid next step
        game.MakeMove(newPosition); // Make a move

        // Act
        game.Restart(); // Restart the level

        // Assert
        Assert.AreEqual(startPosition, player.CurrentPosition, "Player should be back at the start position after restart.");
        Assert.AreEqual(0, game.GetMoveCount(), "Move history should be empty after restart.");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void RestartLevel_UnsuccessfulRestart_NoLevelLoaded()
    {
        // Arrange
        var levels = new List<ILevel>(); // Empty list of levels
        var game = new Game(levels);

        // Act
        game.Restart(); // Attempt to restart when no level is loaded
    }
}

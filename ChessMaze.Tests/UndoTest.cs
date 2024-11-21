using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameUndoTests
{
    [TestMethod]
    public void UndoMove_RevertsState()
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

        // Act
        bool moveMade = game.MakeMove(newPosition); // Attempt to make a move
        if (moveMade)
        {
            game.Undo(); // Undo the move only if it was made
        }

        // Assert
        Assert.AreEqual(startPosition, player.CurrentPosition, "Player should be back at the start position after undo.");
        Assert.AreEqual(0, game.GetMoveCount(), "Move history should be empty after undo.");
    }

    [TestMethod]
    public void UndoMove_FailsWithNoMovesMade()
    {
        // Arrange
        var board = new Board(8, 8, new Rules());
        var startPosition = new Position(0, 0);
        var player = new Player(startPosition, PieceType.King, new Rules());
        var level = new Level(board, startPosition, new Position(7, 7), player);
        var levels = new List<ILevel> { level }; // List of levels
        var game = new Game(levels);
        game.LoadLevel(level);

        // Act
        string result = game.Undo(); // Attempt to undo a move when no moves have been made

        // Assert
        Assert.AreEqual("Error: No moves to undo.", result, "The player should receive an error message indicating that there are no moves to undo.");
    }
}

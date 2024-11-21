using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameNextLevelTests
{
    private Game game;
    private Level level1;
    private Level level2;

    [TestInitialize]
    public void SetUp()
    {
        level1 = CreateLevel1();
        level2 = CreateLevel2();
        var levels = new List<ILevel> { level1, level2 }; // List of levels
        game = new Game(levels);
    }

    [TestMethod]
    public void TestNextLevel_SuccessfulTransition()
    {
        // Arrange
        game.StartNewGame(level1); // Start level 1

        // Move player to the end position of level 1
        game.MakeMove(new Position(0, 1)); // Move to (0, 1)
        game.MakeMove(new Position(1, 1)); // Move to (1, 1)
        game.MakeMove(new Position(2, 1)); // Move to (2, 1)
        game.MakeMove(new Position(3, 1)); // Move to (3, 1)
        game.MakeMove(new Position(4, 1)); // Move to (4, 1)
        game.MakeMove(new Position(5, 1)); // Move to (5, 1)
        game.MakeMove(new Position(6, 1)); // Move to (6, 1)
        game.MakeMove(new Position(7, 1)); // Move to (7, 1)
        game.MakeMove(new Position(7, 7)); // Move to end position (7, 7)

        // Act
        var currentLevel = game.CurrentLevel;

        // Assert
        Assert.IsNotNull(currentLevel, "The game should load the next level.");
        Assert.AreEqual(level2.StartPosition, currentLevel.Player.CurrentPosition, "The player's position should be set to the start position of the next level.");
    }

    [TestMethod]
    public void TestNextLevel_UnsuccessfulTransition_LevelNotCompleted()
    {
        // Arrange
        game.StartNewGame(level1); // Start level 1

        // Act
        var currentLevel = game.CurrentLevel;

        // Assert
        Assert.IsNotNull(currentLevel, "The game should not transition to the next level.");
        Assert.AreEqual(level1.StartPosition, currentLevel.Player.CurrentPosition, "The player's position should remain the same.");
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

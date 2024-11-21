using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameMoveTests
{
    private Game game;
    private Level testLevel;
    private Player testPlayer;
    private Board testBoard;
    private Position startPosition;
    private Position validPosition;
    private Position invalidPosition;
    private PieceType initialPieceType;
    private PieceType destinationPieceType;

    [TestInitialize]
    public void SetUp()
    {
        startPosition = new Position(0, 0);
        validPosition = new Position(0, 1); // Assuming this is a valid move for the piece
        invalidPosition = new Position(1, 1); // Assuming this is an invalid move for the piece
        initialPieceType = PieceType.Rook;
        destinationPieceType = PieceType.Bishop; // The piece type at the destination position

        testPlayer = new Player(startPosition, initialPieceType, new Rules());
        testBoard = new Board(8, 8, new Rules());
        testBoard.PlacePiece(new Piece(destinationPieceType), validPosition); // Place a piece at the valid position
        testLevel = new Level(testBoard, startPosition, new Position(7, 7), testPlayer);
        var levels = new List<ILevel> { testLevel }; // List of levels
        game = new Game(levels);
        game.LoadLevel(testLevel);
    }

    [TestMethod]
    public void TestMakeMove_SuccessfulMove()
    {
        // Act
        bool moveMade = game.MakeMove(validPosition);

        // Assert
        Assert.IsTrue(moveMade, "The piece should move to the new position.");
        Assert.AreEqual(validPosition, testPlayer.CurrentPosition, "The player's position should be updated to the new position.");
        Assert.AreEqual(1, game.GetMoveCount(), "The move count should be updated.");
        Assert.AreEqual(destinationPieceType, testPlayer.PieceType, "The player's piece type should change to the type of the piece at the destination position.");
    }

    [TestMethod]
    public void TestMakeMove_UnsuccessfulMove_InvalidChessMove()
    {
        // Act
        bool moveMade = game.MakeMove(invalidPosition);

        // Assert
        Assert.IsFalse(moveMade, "The piece should not move to the invalid position.");
        Assert.AreEqual(startPosition, testPlayer.CurrentPosition, "The player's position should remain the same.");
        Assert.AreEqual(0, game.GetMoveCount(), "The move count should not be updated.");
        Assert.AreEqual(initialPieceType, testPlayer.PieceType, "The player's piece type should remain the same.");
    }
}

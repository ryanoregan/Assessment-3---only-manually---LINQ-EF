using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameHighlightingTests
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
    public void TestShowPossibleMoves_SuccessfulHighlighting()
    {
        // Act
        List<Position> possibleMoves = game.ShowPossibleMoves();

        // Assert
        Assert.IsTrue(possibleMoves.Contains(validPosition), "The possible moves should include the valid position.");
    }

    [TestMethod]
    public void TestShowPossibleMoves_HighlightingWhenBoardIsFull()
    {
        // Arrange
        testBoard = new Board(8, 8, new Rules());
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                testBoard.PlacePiece(new Piece(PieceType.Pawn), new Position(row, col));
            }
        }
        testLevel = new Level(testBoard, startPosition, new Position(7, 7), testPlayer);
        var levels = new List<ILevel> { testLevel }; // List of levels
        game = new Game(levels);
        game.LoadLevel(testLevel);

        // Act
        List<Position> possibleMoves = game.ShowPossibleMoves();

        // Assert
        foreach (var move in possibleMoves)
        {
            if (testPlayer.PieceType == PieceType.Knight)
            {
                Assert.IsTrue(
                    (Math.Abs(move.Row - startPosition.Row) == 2 && Math.Abs(move.Column - startPosition.Column) == 1) ||
                    (Math.Abs(move.Row - startPosition.Row) == 1 && Math.Abs(move.Column - startPosition.Column) == 2),
                    "The Knight should be able to move in an 'L' shape regardless of the board being full."
                );
            }
            else
            {
                Assert.IsTrue(move.Row == startPosition.Row + 1 || move.Column == startPosition.Column + 1, "Each piece should only be able to move one space forward.");
            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System.Collections.Generic;

[TestClass]
public class GameMoveValidationTests
{
    private Game game;
    private Level testLevel;
    private Player testPlayer;
    private Board testBoard;
    private Position startPosition;
    private Position validPosition;
    private Position invalidPosition;

    [TestInitialize]
    public void SetUp()
    {
        startPosition = new Position(0, 0);
        validPosition = new Position(0, 1); // Assuming this is a valid move for the piece
        invalidPosition = new Position(1, 1); // Assuming this is an invalid move for the piece
        testPlayer = new Player(startPosition, PieceType.Rook, new Rules());
        testBoard = new Board(8, 8, new Rules());
        testLevel = new Level(testBoard, startPosition, new Position(7, 7), testPlayer);
        var levels = new List<ILevel> { testLevel }; // List of levels
        game = new Game(levels);
        game.LoadLevel(testLevel);
    }

    [TestMethod]
    public void TestValidateMove_SuccessfulValidation()
    {
        // Act
        bool isValid = game.ValidateMove(validPosition);

        // Assert
        Assert.IsTrue(isValid, "The move should be validated as legal.");
    }

    [TestMethod]
    public void TestValidateMove_UnsuccessfulValidation_InvalidMove()
    {
        // Act
        bool isValid = game.ValidateMove(invalidPosition);

        // Assert
        Assert.IsFalse(isValid, "The move should be rejected as illegal.");
    }
}

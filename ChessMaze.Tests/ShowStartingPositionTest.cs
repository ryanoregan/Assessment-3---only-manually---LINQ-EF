using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;

[TestClass]
public class ShowStartPositionTests
{
    private Level level;
    private Board board;
    private Position startPosition;
    private Position endPosition;
    private Player player;

    [TestInitialize]
    public void SetUp()
    {
        board = new Board(8, 8, new Rules());
        startPosition = new Position(0, 0);
        endPosition = new Position(7, 7);
        player = new Player(startPosition, PieceType.King, new Rules());
        level = new Level(board, startPosition, endPosition, player);
    }

    [TestMethod]
    public void TestShowStartingPosition_SuccessfulDisplay()
    {
        // Act
        var result = level.GetStartPosition();

        // Assert
        Assert.AreEqual((startPosition.Row, startPosition.Column), result, "The starting position should be correctly displayed.");
    }

    [TestMethod]
    public void TestShowStartingPosition_UnsuccessfulDisplay_NoPieceOnBoard()
    {
        // Arrange
        player = null; // Simulate no player on the board
        level = new Level(board, startPosition, endPosition, player);

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>(() => level.ShowStartPosition(), "The player should receive an error message indicating that their piece is not currently on the board.");
    }

}

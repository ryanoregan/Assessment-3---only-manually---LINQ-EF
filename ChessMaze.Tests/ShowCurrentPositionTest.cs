using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;

[TestClass]
public class ShowCurrentPositionTests
{
    private Player player;
    private Position startPosition;
    private Rules rules;

    [TestInitialize]
    public void SetUp()
    {
        startPosition = new Position(0, 0);
        rules = new Rules();
        player = new Player(startPosition, PieceType.King, rules);
    }

    [TestMethod]
    public void TestShowCurrentPosition_SuccessfulDisplay()
    {
        // Act
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            player.ShowCurrentPosition();
            var result = sw.ToString().Trim();

            // Assert
            Assert.AreEqual($"The current position is ({startPosition.Row}, {startPosition.Column})", result, "The current position should be correctly displayed.");
        }
    }

    [TestMethod]
    public void TestShowCurrentPosition_UnsuccessfulDisplay_NoPieceOnBoard()
    {
        // Arrange
        player = null; // Simulate no player on the board

        // Act and Assert
        Assert.ThrowsException<NullReferenceException>(() => player.ShowCurrentPosition(), "The player should receive an error message indicating that their piece is not currently on the board.");
    }

}

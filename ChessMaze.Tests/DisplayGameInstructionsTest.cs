using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessMaze;
using System;
using System.Collections.Generic;

// Testing Feature Display game instructions

[TestClass]
public class GameInstructionsTests
{
    private Game game;

    [TestInitialize]
    public void SetUp()
    {
        var levels = new List<ILevel>(); // Empty list of levels
        game = new Game(levels);
    }

    [TestMethod]
    public void TestDisplayInstructions_ReturnsString()
    {
        // Act
        var instructions = game.DisplayInstructions();

        // Assert
        Assert.IsInstanceOfType(instructions, typeof(string), "DisplayInstructions should return a string.");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidCastException))]
    public void TestDisplayInstructions_ThrowsExceptionForNonStringType()
    {
        // Act
        var instructions = game.DisplayInstructions();

        // This cast should fail and throw an InvalidCastException,
        // which is what we want to test for.
        var castedInstructions = (int)(object)instructions;

        // Assert is not necessary here due to ExpectedException attribute
    }
}

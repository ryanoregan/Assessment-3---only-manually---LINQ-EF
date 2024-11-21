namespace ChessMaze;
/// <summary>
/// Represents a level in the Chess Maze game.
/// </summary>
public interface ILevel
{
    /// <summary>
    /// Gets the game board for this level.
    /// </summary>
    IBoard Board { get; }

    /// <summary>
    /// Gets the start position for this level.
    /// </summary>
    IPosition StartPosition { get; }

    /// <summary>
    /// Gets the end position for this level.
    /// </summary>
    IPosition EndPosition { get; }

    /// <summary>
    /// Gets the player for this level.
    /// </summary>
    IPlayer Player { get; }

    /// <summary>
    /// Determines if the level is completed.
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// Displays the start position for this level.
    /// </summary>
    void ShowStartPosition();


    /// <summary>
    /// Method to get the start position.
    /// </summary>
    /// <returns>exact row and column of the start position.</returns>
    (int Row, int Column) GetStartPosition();

    /// <summary>
    /// Displays the end position for this level.
    /// </summary>
    void ShowEndPosition();

    /// <summary>
    /// Method to get the end position.
    /// </summary>
    /// <returns>exact row and column of the end position.</returns>
    (int Row, int Column) GetEndPosition();
}
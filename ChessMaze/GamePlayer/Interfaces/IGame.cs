namespace ChessMaze;
/// <summary>
/// Represents a game session in the Chess Maze game.
/// </summary>
public interface IGame
{
    /// <summary>
    /// Gets the current level being played.
    /// </summary>
    ILevel? CurrentLevel { get; }

    /// <summary>
    /// Determines if the game is over.
    /// </summary>
    bool IsGameOver {get;}

    /// <summary>
    /// Loads a specified level into the game.
    /// </summary>
    /// <param name="level">The level to load.</param>
    void LoadLevel(ILevel level);

    /// <summary>
    /// Attempts to make a move to a new position.
    /// </summary>
    /// <param name="newPosition">The new position to move to.</param>
    /// <returns>True if the move is successful, otherwise false.</returns>
    bool MakeMove(IPosition newPosition);


    /// <summary>
    /// Attempts to validate a move to a new position.
    /// </summary>
    /// <param name="newPosition"></param>
    /// <returns>True if the move is valid, otherwise false.</returns>
    bool ValidateMove(IPosition newPosition);

    /// <summary>
    /// Gets the count of moves made in the current game.
    /// </summary>
    /// <returns>The number of moves made.</returns>
    int GetMoveCount();

    /// <summary>
    /// Undoes the last move made in the game.
    /// </summary>
    string? Undo();

    /// <summary>
    /// Method to start a new game.
    /// </summary>
    void StartNewGame(ILevel level);

    /// <summary>
    /// Restarts the current game level.
    /// </summary>
    void Restart();

    /// <summary>
    /// Method to show the tracer of where the user has moved.
    /// </summary>
    /// <returns>The history of moves from the move stack.</returns>
    string ShowTracer();

    /// <summary>
    /// Method to show possible moves for the current player.
    /// </summary>
    /// <returns>List of Positions that would be valid moves.</returns>
    List<Position> ShowPossibleMoves();

    /// <summary>
    /// Method to display game statistics.
    /// </summary>
    /// <returns>Moves made, time taken, levels completed.</returns>
    string DisplayStatistics();

    /// <summary>
    /// Method to display the game instructions.
    /// </summary>
    /// <returns>Game Instructions.</returns>
    string DisplayInstructions();

}

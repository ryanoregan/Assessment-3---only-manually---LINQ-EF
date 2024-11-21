namespace ChessMaze
{
    public class Level : ILevel  // The Level class implements the ILevel interface
    {
        public IBoard Board { get; }  // Property for the game board
        public IPosition StartPosition { get; }  // Property for the start position of the level
        public IPosition EndPosition { get; }  // Property for the end position of the level
        public IPlayer Player { get; }  // Property for the player

        // Constructor to initialize a new Level instance with specified board, start position, end position, and player
        public Level(IBoard board, IPosition startPosition, IPosition endPosition, IPlayer player)
        {
            Board = board;
            StartPosition = startPosition;
            EndPosition = endPosition;
            Player = player;
        }

        // Property to check if the level is completed
        public bool IsCompleted
        {
            get
            {
                // A level is completed when the player's current position is the end position
                return Player.CurrentPosition.Equals(EndPosition);
            }
        }

        // Method to show the start position
    public void ShowStartPosition()
        {
            if (Player == null)
            {
                throw new InvalidOperationException("Error: Player's starting position is not available.");
            }

            Console.WriteLine($"The start position is ({StartPosition.Row}, {StartPosition.Column})");
        }

        // Method to get the start position
        public (int Row, int Column) GetStartPosition()
        {
            return (StartPosition.Row, StartPosition.Column);
        }


        // Method to show the end position
    public void ShowEndPosition()
        {
            if (Player == null)
            {
                throw new InvalidOperationException("Error: Player's piece is not on the board.");
            }

            Console.WriteLine($"The end position is ({EndPosition.Row}, {EndPosition.Column})");
        }

        public (int Row, int Column) GetEndPosition()
        {
            return (EndPosition.Row, EndPosition.Column);
        }
    }
}

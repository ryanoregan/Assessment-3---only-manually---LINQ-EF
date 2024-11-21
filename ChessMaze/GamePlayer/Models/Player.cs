using System.Diagnostics;

namespace ChessMaze
{
    public class Player : IPlayer  // The Player class implements the IPlayer interface
    {
        public IPosition CurrentPosition { get; set; }  // Property for the player's current position on the board
        public PieceType PieceType { get; set; }  // Add a PieceType property to the Player class
        private Rules rules;  // Instance of the Rules class to determine valid moves
        private List<IPosition> moveHistory;

        // Constructor to initialize a new Player instance with specified start position and rules
        public Player(IPosition startPosition, PieceType pieceType, Rules rules)  // Add a PieceType parameter to the constructor
        {
            CurrentPosition = startPosition;  // Set the player's current position
            PieceType = pieceType;  // Initialize the PieceType property
            this.rules = rules;  // Set the rules for the game
            moveHistory = new List<IPosition> { startPosition };
        }

        public bool CanMove(IPosition newPosition, IBoard board)
        {
            // Get the piece at the current position on the board
            IPiece? currentPiece = board.GetPiece(CurrentPosition);
            if (currentPiece == null)
            {
                return false;
            }

            // Get valid moves for the current piece type
            var validMoves = Rules.GetValidMoves(currentPiece, CurrentPosition, board);

            bool canMove = validMoves.Any(pos => pos.Row == newPosition.Row && pos.Column == newPosition.Column);

            return canMove;
        }




        // Method to move the player to a new position
        public void Move(IPosition newPosition, IBoard board)
        {
            // Get the piece at the new position
            IPiece? destinationPiece = board.GetPiece(newPosition);

            // Update the player's current position to the new position
            CurrentPosition = newPosition;
            moveHistory.Add(newPosition);

            // If there was a piece at the new position, update the player's PieceType
            if (destinationPiece != null)
            {
                PieceType = destinationPiece.Type;
            }
        }

        // Method to show the player's current position
        public void ShowCurrentPosition()
            {
                if (CurrentPosition == null)
                {
                    throw new InvalidOperationException("Error: Player's piece is not on the board.");
                }

                Console.WriteLine($"The current position is ({CurrentPosition.Row}, {CurrentPosition.Column})");
            }

        // Method to show the player's movement trace
        public string ShowTracer()
            {
                if (moveHistory.Count == 1)
                {
                    return "No moves have been made.";
                }

                var tracer = "Tracer: ";
                for (int i = 0; i < moveHistory.Count - 1; i++)
                {
                    tracer += $"({moveHistory[i].Row}, {moveHistory[i].Column}) -> ";
                }
                var lastMove = moveHistory[moveHistory.Count - 1];
                tracer += $"({lastMove.Row}, {lastMove.Column})";

                return tracer;
            }
    }
}

namespace ChessMaze
{
    public class Board : IBoard  // The Board class implements the IBoard interface
    {
        public int Rows { get; }  // Property for the number of rows on the board
        public int Columns { get; }  // Property for the number of columns on the board
        public IPiece?[,] Cells { get; private set; }
        // 2D array representing the board, each cell can hold a Piece
        private readonly Rules rules;  // Instance of the Rules class to determine valid moves

        // Constructor to initialize a new Board instance with specified rows, columns, and rules
        public Board(int rows, int columns, Rules rules)
        {
            Rows = rows;
            Columns = columns;
            Cells = new Piece[rows, columns];  // Initialize the Cells array
            this.rules = rules;  // Set the rules for the game
        }

        // Method to get the Piece at a given Position on the board
        public IPiece? GetPiece(IPosition position)
        {
            return Cells[position.Row, position.Column];
        }

        // Method to place a Piece at a specified Position on the board
        public void PlacePiece(IPiece piece, IPosition position)
        {
            Cells[position.Row, position.Column] = piece;
        }

        // Method to remove the Piece from a specified Position on the board
        public void RemovePiece(IPosition position)
        {
            Cells[position.Row, position.Column] = null;
        }

        // Method to move a Piece from one Position to another on the board
        public void MovePiece(IPosition from, IPosition to)
        {
            IPiece? piece = GetPiece(from);  // Get the Piece at the 'from' Position
            if (piece == null)
            {
            throw new InvalidOperationException("No piece found at the  given position.");
            }
            RemovePiece(from);  // Remove the Piece from the 'from' Position
            PlacePiece(piece, to);  // Place the Piece at the 'to' Position
        }

        // Method to check if a given Position is valid (i.e., within the bounds of the board)
        public bool IsValidPosition(IPosition position)
        {
            return position.Row >= 0 && position.Row < Rows &&
                   position.Column >= 0 && position.Column < Columns;
        }

        // Method to show possible moves for the current player
        public List<Position> ShowPossibleMoves(IPlayer player)
        {
            IPiece? piece = GetPiece(player.CurrentPosition);  // Get the Piece at the player's current position
            if (piece == null)
            {
            throw new InvalidOperationException("No piece found at the  given position.");
            }
            return Rules.GetValidMoves(piece, player.CurrentPosition, this);
              // Get the valid moves for the Piece
        }

        // Method to check if a move from one Position to another is legal according to the game's rules
        public bool IsMoveLegal(IPosition from, IPosition to)
        {
            IPiece? piece = GetPiece(from);  // Get the Piece at the 'from' Position
            if (piece == null)
            {
            throw new InvalidOperationException("No piece found at the  given position.");
            }
            List<Position> validMoves = Rules.GetValidMoves(piece, from, this);  // Get the valid moves for the Piece
            return validMoves.Contains(to);  // Check if the 'to' Position is in the list of valid moves
        }
    }
}

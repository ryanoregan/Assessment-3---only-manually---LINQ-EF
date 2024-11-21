namespace ChessMaze
{
    public class Piece : IPiece  // The Piece class implements the IPiece interface
    {
        public PieceType Type { get; }  // Property for the type of the piece

        // Constructor to initialize a new Piece instance with specified type
        public Piece(PieceType type)
        {
            Type = type;  // Set the type of the piece
        }
    }
}

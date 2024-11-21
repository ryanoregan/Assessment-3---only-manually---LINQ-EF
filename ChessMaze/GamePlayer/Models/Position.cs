namespace ChessMaze
{
    public class Position : IPosition  // The Position class implements the IPosition interface
    {
        public int Row { get; set; }  // Change to read-write for deserialization
        public int Column { get; set; }  // Change to read-write for deserialization

        // Parameterless constructor required for deserialization
        public Position() { }

        // Constructor to initialize a new Position instance with specified row and column
        public Position(int row, int column)
        {
            if (row < 0 || column < 0)
            {
                throw new ArgumentException("Row and Column must be non-negative.");
            }

            Row = row;
            Column = column;
        }

        // Override Equals to compare Position objects based on Row and Column
        public override bool Equals(object? obj)
        {
            if (obj is Position other)
            {
                return Row == other.Row && Column == other.Column;
            }
            return false;
        }

        // Override GetHashCode to generate a hash code based on Row and Column
        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public override string ToString()
        {
            return $"({Row}, {Column})";
        }
    }
}

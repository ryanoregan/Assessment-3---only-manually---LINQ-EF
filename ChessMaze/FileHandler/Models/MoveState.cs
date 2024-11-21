using ChessMaze;

public class MoveState
{
    public Position From { get; set; }
    public Position To { get; set; }

    public MoveState(Position from, Position to)
    {
        From = from;
        To = to;
    }

    // Parameterless constructor for deserialization
    public MoveState() { }
}

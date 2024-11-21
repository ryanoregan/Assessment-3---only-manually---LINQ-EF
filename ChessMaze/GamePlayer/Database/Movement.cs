namespace ChessMaze;

public class Movement
{
    public int MovementId { get; set; }
    public int PlayerId { get; set; }
    public string PieceMoved { get; set; }
    public string FromPosition { get; set; }
    public string ToPosition { get; set; }
    public DateTime Timestamp { get; set; }
}

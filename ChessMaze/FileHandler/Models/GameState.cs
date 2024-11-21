using System.Collections.Generic;

namespace ChessMaze.FileHandler
{
    public class GameState
    {
        public List<PieceState> Board { get; set; } = new List<PieceState>();
        public List<MoveState> MoveHistory { get; set; } = new List<MoveState>();
        public int CurrentLevel { get; set; }
    }
}

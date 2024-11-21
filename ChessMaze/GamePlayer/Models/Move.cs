namespace ChessMaze
{
    public class Move
    {
        public IPosition From { get; private set; }
        public IPosition To { get; private set; }

        public Move(IPosition from, IPosition to)
        {
            From = from;
            To = to;
        }
    }
}

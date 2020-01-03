namespace board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsQty { get; protected set; }
        public Board Brd { get; protected set; }

        public Piece(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            Brd = board;
            MovementsQty = 0;
        }
    }
}

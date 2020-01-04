namespace board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementsQty { get; protected set; }
        public Board Brd { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Brd = board;
            MovementsQty = 0;
        }

        public void MovementsQtyIncrement()
        {
            MovementsQty++;
        }
    }
}

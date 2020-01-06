namespace board
{
    abstract class Piece
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

        public bool IsTherePossibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < Brd.Rows; i++)
            {
                for (int j = 0; j < Brd.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMovements()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}

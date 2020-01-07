using board;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Board brd, Color color) : base(color, brd)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Brd.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Brd.Rows, Brd.Columns];
            Position pos = new Position(0, 0);

            //North-East
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(Position.Row - 1, Position.Column + 1);
            }

            //North-West
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(Position.Row - 1, Position.Column - 1);
            }

            //South-East
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(Position.Row + 1, Position.Column + 1);
            }

            //South-West
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(Position.Row + 1, Position.Column - 1);
            }

            return mat;
        }
    }
}

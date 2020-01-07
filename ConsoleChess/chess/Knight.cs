using board;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Board brd, Color color) : base(color, brd)
        {
        }

        public override string ToString()
        {
            return "N";
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

            pos.SetValues(Position.Row - 1, Position.Column - 2);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row - 2, Position.Column - 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row - 2, Position.Column + 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row - 1, Position.Column + 2);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 2, Position.Column + 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 2, Position.Column - 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetValues(Position.Row + 1, Position.Column - 2);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }
    }
}

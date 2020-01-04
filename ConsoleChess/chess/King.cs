using board;

namespace chess
{
    class King : Piece
    {
        public King(Board brd, Color color) : base(color, brd)
        {
        }

        public override string ToString()
        {
            return "K";
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

            //North
            pos.SetValues(Position.Row - 1, Position.Column);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //North-East
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //East
            pos.SetValues(Position.Row, Position.Column + 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //South-East
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //South
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //South-West
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //West
            pos.SetValues(Position.Row, Position.Column - 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //North-West
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            if (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }
    }
}

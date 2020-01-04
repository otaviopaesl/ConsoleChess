using board;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board brd, Color color) : base(color, brd)
        {
        }

        public override string ToString()
        {
            return "T";
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
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row -= 1;
            }

            //South
            pos.SetValues(Position.Row + 1, Position.Column);
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Row += 1;
            }

            //East
            pos.SetValues(Position.Row, Position.Column + 1);
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column += 1;
            }

            //West
            pos.SetValues(Position.Row, Position.Column - 1);
            while (Brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Brd.Piece(pos) != null && Brd.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column -= 1;
            }

            return mat;
        }
    }
}

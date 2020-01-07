using board;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Board brd, Color color) : base(color, brd)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool IsThereEnemy(Position pos)
        {
            Piece p = Brd.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Brd.Piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Brd.Rows, Brd.Columns];
            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Row - 1, Position.Column);
                if (Brd.IsValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row - 2, Position.Column);
                if (Brd.IsValidPosition(pos) && Free(pos) && MovementsQty == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row - 1, Position.Column - 1);
                if (Brd.IsValidPosition(pos) && IsThereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row - 1, Position.Column + 1);
                if (Brd.IsValidPosition(pos) && IsThereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }

            else
            {
                pos.SetValues(Position.Row + 1, Position.Column);
                if (Brd.IsValidPosition(pos) && Free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row + 2, Position.Column);
                if (Brd.IsValidPosition(pos) && Free(pos) && MovementsQty == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row + 1, Position.Column - 1);
                if (Brd.IsValidPosition(pos) && IsThereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.SetValues(Position.Row + 1, Position.Column + 1);
                if (Brd.IsValidPosition(pos) && IsThereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }

            return mat;
        }
    }
}

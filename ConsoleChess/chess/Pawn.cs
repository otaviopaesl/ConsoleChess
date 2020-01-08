using board;

namespace chess
{
    class Pawn : Piece

    {
        private ChessMatch Match;

        public Pawn(Board brd, Color color, ChessMatch match) : base(color, brd)
        {
            Match = match;
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
                Position p2 = new Position(Position.Row - 1, Position.Column);
                if (Brd.IsValidPosition(p2) && Free(p2) && Brd.IsValidPosition(pos) && Free(pos) && MovementsQty == 0)
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

                //#Special Play: En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Brd.IsValidPosition(left) && IsThereEnemy(left) && Brd.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Brd.IsValidPosition(right) && IsThereEnemy(right) && Brd.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
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
                Position p2 = new Position(Position.Row + 1, Position.Column);
                if (Brd.IsValidPosition(p2) && Free(p2) && Brd.IsValidPosition(pos) && Free(pos) && MovementsQty == 0)
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

                //#Special Play: En Passant
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Brd.IsValidPosition(left) && IsThereEnemy(left) && Brd.Piece(left) == Match.VulnerableEnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Brd.IsValidPosition(right) && IsThereEnemy(right) && Brd.Piece(right) == Match.VulnerableEnPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}

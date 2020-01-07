using board;

namespace chess
{
    class King : Piece
    {
        private ChessMatch Match;

        public King(Board brd, Color color, ChessMatch match) : base(color, brd)
        {
            Match = match;
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

        private bool CastlingTest(Position pos)
        {
            Piece p = Brd.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.MovementsQty == 0;
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

            //#Special Play: Castling
            if (MovementsQty == 0 && !Match.Check)
            {
                //Castling Short
                Position posR1 = new Position(Position.Row, Position.Column + 3);
                if (CastlingTest(posR1))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Brd.Piece(p1) == null && Brd.Piece(p2) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }

                //Castling Long
                Position posR2 = new Position(Position.Row, Position.Column -4);
                if (CastlingTest(posR2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Brd.Piece(p1) == null && Brd.Piece(p2) == null && Brd.Piece(p3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }




            return mat;
        }
    }
}

using System.Collections.Generic;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board Brd { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Brd = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            VulnerableEnPassant = null;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            MountBoard();
        }

        public Piece RunMovement(Position origin, Position destination)
        {
            Piece P = Brd.RemovePiece(origin);
            P.IncreaseMovementsQty();
            Piece CapturedPiece = Brd.RemovePiece(destination);
            Brd.PutPiece(P, destination);
            if (CapturedPiece != null)
            {
                Captured.Add(CapturedPiece);
            }
            
            //#Special Play: Castling short
            if (P is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Row, origin.Column + 3);
                Position destinationR = new Position(origin.Row, origin.Column + 1);
                Piece R = Brd.RemovePiece(originR);
                R.IncreaseMovementsQty();
                Brd.PutPiece(R, destinationR);
            }

            //#Special Play: Castling long
            if (P is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Row, origin.Column - 4);
                Position destinationR = new Position(origin.Row, origin.Column - 1);
                Piece R = Brd.RemovePiece(originR);
                R.IncreaseMovementsQty();
                Brd.PutPiece(R, destinationR);
            }

            //#Special Play: En Passant
            if (P is Pawn)
            {
                if (origin.Column != destination.Column && CapturedPiece == null)
                {
                    Position posP;
                    if (P.Color == Color.White)
                    {
                        posP = new Position(destination.Row + 1, destination.Column);
                    }
                    else
                    {
                        posP = new Position(destination.Row - 1, destination.Column);
                    }
                    CapturedPiece = Brd.RemovePiece(posP);
                    Captured.Add(CapturedPiece);
                }
            }

            return CapturedPiece;
        }

        public void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece P = Brd.RemovePiece(destination);
            P.DecreaseMovementsQty();
            if (capturedPiece != null)
            {
                Brd.PutPiece(capturedPiece, destination);
                Captured.Remove(capturedPiece);
            }
            Brd.PutPiece(P, origin);

            //#Special Play: Castling short
            if (P is King && destination.Column == origin.Column + 2)
            {
                Position originR = new Position(origin.Row, origin.Column + 3);
                Position destinationR = new Position(origin.Row, origin.Column + 1);
                Piece R = Brd.RemovePiece(destinationR);
                R.DecreaseMovementsQty();
                Brd.PutPiece(R, originR);
            }

            //#Special Play: Castling long
            if (P is King && destination.Column == origin.Column - 2)
            {
                Position originR = new Position(origin.Row, origin.Column - 4);
                Position destinationR = new Position(origin.Row, origin.Column - 1);
                Piece R = Brd.RemovePiece(destinationR);
                R.DecreaseMovementsQty();
                Brd.PutPiece(R, originR);
            }

            //#Special Play: En Passant
            if (P is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Brd.RemovePiece(destination);
                    Position posP;
                    if (P.Color == Color.White)
                    {
                        posP = new Position(3, destination.Column);
                    }
                    else
                    {
                        posP = new Position(4, destination.Column);
                    }
                    Brd.PutPiece(pawn, posP);
                }

            }

        }

        public void MakeThePlay(Position origin, Position destination)
        {
            Piece CapturedPiece = RunMovement(origin, destination);

            if (IsItChecked(CurrentPlayer))
            {
                UndoMovement(origin, destination, CapturedPiece);
                throw new BoardException("You Can't put yourself in check!");
            }

            Piece p = Brd.Piece(destination);

            //#Special Play: Promotion
            if (p is Pawn)
            {
                if (p.Color == Color.White && destination.Row == 0 || p.Color == Color.Black && destination.Row == 7)
                {
                    p = Brd.RemovePiece(destination);
                    Pieces.Remove(p);
                    Piece queen = new Queen(Brd, p.Color);
                    Brd.PutPiece(queen, destination);
                    Pieces.Add(queen);
                }
            }


            if (IsItChecked(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (CheckmateTest(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            //#Special Play: En Passant
            if (p is Pawn && destination.Row == origin.Row - 2 || destination.Row == origin.Row + 2)
            {
                VulnerableEnPassant = p;
            }
            else
            {
                VulnerableEnPassant = null;
            }

        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Brd.Piece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position!");
            }

            if (CurrentPlayer != Brd.Piece(pos).Color)
            {
                throw new BoardException("The chosen piece is not yours!");
            }

            if (!Brd.Piece(pos).IsTherePossibleMovements())
            {
                throw new BoardException("There is no possible movements for the chosen piece!");
            }
        }

        public void ValidadeDestinationPosition(Position origin, Position destination)
        {
            if (!Brd.Piece(origin).PossibleMovement(destination))
            {
                throw new BoardException("Invalid destination position!");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            return Color.White;
        }

        private Piece King(Color color)
        {
            foreach (Piece p in InGamePieces(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        public bool IsItChecked(Color color)
        {
            Piece k = King(color);
            if (k == null)
            {
                throw new BoardException("There is no " + color + "King on the board!");
            }

            foreach (Piece p in InGamePieces(Opponent(color)))
            {
                bool[,] mat = p.PossibleMovements();
                if (mat[k.Position.Row, k.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckmateTest(Color color)
        {
            if (!IsItChecked(color))
            {
                return false;
            }
            foreach (Piece p in InGamePieces(color))
            {
                bool[,] mat = p.PossibleMovements();
                for (int i = 0; i < Brd.Rows; i++)
                {
                    for (int j = 0; j < Brd.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = RunMovement(origin, destination);
                            bool checkTest = IsItChecked(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int row, Piece piece)
        {
            Brd.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        public void MountBoard()
        {

            PutNewPiece('a', 1, new Rook(Brd, Color.White));
            PutNewPiece('b', 1, new Knight(Brd, Color.White));
            PutNewPiece('c', 1, new Bishop(Brd, Color.White));
            PutNewPiece('d', 1, new Queen(Brd, Color.White));
            PutNewPiece('e', 1, new King(Brd, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Brd, Color.White));
            PutNewPiece('g', 1, new Knight(Brd, Color.White));
            PutNewPiece('h', 1, new Rook(Brd, Color.White));
            PutNewPiece('a', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Brd, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Brd, Color.White, this));
            

            PutNewPiece('a', 8, new Rook(Brd, Color.Black));
            PutNewPiece('b', 8, new Knight(Brd, Color.Black));
            PutNewPiece('c', 8, new Bishop(Brd, Color.Black));
            PutNewPiece('d', 8, new Queen(Brd, Color.Black));
            PutNewPiece('e', 8, new King(Brd, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Brd, Color.Black));
            PutNewPiece('g', 8, new Knight(Brd, Color.Black));
            PutNewPiece('h', 8, new Rook(Brd, Color.Black));
            PutNewPiece('a', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Brd, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Brd, Color.Black, this));
        }
    }
}

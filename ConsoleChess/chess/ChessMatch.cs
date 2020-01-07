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

        public ChessMatch()
        {
            Brd = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
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
            return CapturedPiece;
        }

        public void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Brd.RemovePiece(destination);
            p.ReduceMovementsQty();
            if (capturedPiece != null)
            {
                Brd.PutPiece(capturedPiece, destination);
                Captured.Remove(capturedPiece);
            }
            Brd.PutPiece(p, origin);
        }

        public void MakeThePlay(Position origin, Position destination)
        {
            Piece CapturedPiece = RunMovement(origin, destination);

            if (IsItChecked(CurrentPlayer))
            {
                UndoMovement(origin, destination, CapturedPiece);
                throw new BoardException("You Can't put yourself in check!");
            }

            if (IsItChecked(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            Turn++;
            ChangePlayer();

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
            if (!Brd.Piece(origin).CanMoveTo(destination))
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

        public void PutNewPiece(char column, int row, Piece piece)
        {
            Brd.PutPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        public void MountBoard()
        {
            PutNewPiece('c', 1, new Tower(Brd, Color.White));
            PutNewPiece('c', 2, new Tower(Brd, Color.White));
            PutNewPiece('d', 2, new Tower(Brd, Color.White));
            PutNewPiece('e', 2, new Tower(Brd, Color.White));
            PutNewPiece('e', 1, new Tower(Brd, Color.White));
            PutNewPiece('d', 1, new King(Brd, Color.White));

            PutNewPiece('c', 7, new Tower(Brd, Color.Black));
            PutNewPiece('c', 8, new Tower(Brd, Color.Black));
            PutNewPiece('d', 7, new Tower(Brd, Color.Black));
            PutNewPiece('e', 7, new Tower(Brd, Color.Black));
            PutNewPiece('e', 8, new Tower(Brd, Color.Black));
            PutNewPiece('d', 8, new King(Brd, Color.Black));
        }
    }
}

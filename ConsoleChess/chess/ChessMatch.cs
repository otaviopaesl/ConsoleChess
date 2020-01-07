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

        public ChessMatch()
        {
            Brd = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            MountBoard();
        }

        public void RunMovement(Position origin, Position destination)
        {
            Piece P = Brd.RemovePiece(origin);
            P.MovementsQtyIncrement();
            Piece CapturedPiece = Brd.RemovePiece(destination);
            Brd.PutPiece(P, destination);
            if (CapturedPiece != null)
            {
                Captured.Add(CapturedPiece);
            }
        }

        public void MakeThePlay(Position origin, Position destination)
        {
            RunMovement(origin, destination);
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

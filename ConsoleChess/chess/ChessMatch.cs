using System;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board Brd { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Brd = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            MountBoard();
        }

        public void RunMovement(Position origin, Position destination)
        {
            Piece P = Brd.RemovePiece(origin);
            P.MovementsQtyIncrement();
            Piece CapturedPiece = Brd.RemovePiece(destination);
            Brd.PutPiece(P, destination);
        }

        public void MakePlay(Position origin, Position destination)
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

        public void MountBoard()
        {
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('c', 1).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('c', 2).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('d', 2).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('e', 2).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.White), new ChessPosition('e', 1).ToPosition());
            Brd.PutPiece(new King(Brd, Color.White), new ChessPosition('d', 1).ToPosition());

            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('c', 7).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('c', 8).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('d', 7).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('e', 7).ToPosition());
            Brd.PutPiece(new Tower(Brd, Color.Black), new ChessPosition('e', 8).ToPosition());
            Brd.PutPiece(new King(Brd, Color.Black), new ChessPosition('d', 8).ToPosition());

        }
    }
}

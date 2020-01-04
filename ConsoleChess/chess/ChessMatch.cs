using System;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board Brd { get; private set; }
        private int Turn;
        private Color ActualPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Brd = new Board(8, 8);
            Turn = 1;
            ActualPlayer = Color.White;
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

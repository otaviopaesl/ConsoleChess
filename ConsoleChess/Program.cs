using System;
using board;
using chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board brd = new Board(8, 8);

                brd.PutPiece(new Tower(brd, Color.Black), new Position(0, 0));
                brd.PutPiece(new Tower(brd, Color.Black), new Position(1, 3));
                brd.PutPiece(new King(brd, Color.Black), new Position(2, 4));

                Screen.PrintBoard(brd);
            }

            catch (BoardException e)
            {
                Console.Write(e.Message);
            }
        }
    }
}

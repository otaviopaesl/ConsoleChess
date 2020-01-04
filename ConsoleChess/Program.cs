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
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Brd);

                    Console.WriteLine("");
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();

                    bool[,] possiblePositions = match.Brd.Piece(origin).PossibleMovements();
                    Console.Clear();
                    Screen.PrintBoard(match.Brd, possiblePositions);

                    Console.WriteLine("");
                    Console.Write("Destination: ");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    match.RunMovement(origin, destination);

                }
            }

            catch (BoardException e)
            {
                Console.Write(e.Message);
            }
        }
    }
}

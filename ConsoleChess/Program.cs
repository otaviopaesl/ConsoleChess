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
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine("");
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possiblePositions = match.Brd.Piece(origin).PossibleMovements();
                        Console.Clear();
                        Screen.PrintBoard(match.Brd, possiblePositions);

                        Console.WriteLine("");
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        match.ValidadeDestinationPosition(origin, destination);

                        match.MakeThePlay(origin, destination);
                    }

                    catch(BoardException e)
                    {
                        Console.Write(e.Message);
                        Console.ReadLine();
                    }

                }
                Console.Clear();
                Screen.PrintMatch(match);
            }

            catch (BoardException e)
            {
                Console.Write(e.Message);
            }
        }
    }
}

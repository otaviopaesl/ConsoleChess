using System;
using board;

namespace ConsoleChess
{
    class Screen
    {
        public static void PrintBoard(Board brd)
        {
            for (int i = 0; i < brd.Columns; i++)
            {
                for(int j = 0; j < brd.Rows; j++)
                {
                    if (brd.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(brd.Piece(i, j) + " ");
                    }
                    
                }

                Console.WriteLine();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using board;
using chess;

namespace ConsoleChess
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Brd);
            Console.WriteLine("");
            PrintCapturedPieces(match);
            Console.WriteLine("");
            Console.WriteLine("Turn: " + match.Turn);
            Console.WriteLine("Waiting play: " + match.CurrentPlayer);
        }

        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("White: ");
            PrintHashset(match.CapturedPieces(Color.White));
            Console.WriteLine("");
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintHashset(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine("");

        }

        public static void PrintHashset(HashSet<Piece> set)
        {
            Console.Write("[ ");
            foreach (Piece p in set)
            {
                Console.Write(p + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(Board brd)
        {
            for (int i = 0; i < brd.Columns; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.Rows; j++)
                {
                    PrintPiece(brd.Piece(i, j));
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }


        public static void PrintBoard(Board brd, bool[,] possiblePositions)
        {
            ConsoleColor OriginalBackground = Console.BackgroundColor;
            ConsoleColor ChangedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < brd.Columns; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.Rows; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = ChangedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = OriginalBackground;
                    }
                    PrintPiece(brd.Piece(i, j));
                    Console.BackgroundColor = OriginalBackground;
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = OriginalBackground;
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}

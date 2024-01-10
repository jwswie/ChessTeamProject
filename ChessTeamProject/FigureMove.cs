using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ChessTeamProject
{
    public class FigureMove
    {

        public static void PerformPawnMove(Image pawn, int newRow, int newCol, int[,] chessBoard, string currentSide)
        {
            if (pawn != null && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol <= 8)
            {
                var grid = pawn.Parent as Grid;

                if (grid != null && newRow < grid.RowDefinitions.Count && newCol < grid.ColumnDefinitions.Count)
                {
                    Grid.SetRow(pawn, newRow);
                    Grid.SetColumn(pawn, newCol);

                    if (currentSide == "white")
                    {
                        chessBoard[newRow, newCol] = 2;
                    }
                    else if (currentSide == "black")
                    {
                        chessBoard[newRow, newCol] = 1;
                    }

                }
            }
        }

        public static void PerformKingMove(Image king, int newRow, int newCol, int[,] chessBoard)
        {
            if (king != null && newRow >= 0 && newRow <= 8 && newCol >= 0 && newCol <= 8)
            {
                var grid = king.Parent as Grid;

                if (grid != null && newRow < grid.RowDefinitions.Count && newCol < grid.ColumnDefinitions.Count)
                {
                    Grid.SetRow(king, newRow);
                    Grid.SetColumn(king, newCol);
                    chessBoard[newRow, newCol] = 1;
                }
            }
        }

        public static void PerformQueenMove()
        {
            Console.WriteLine("Performing queen's move...");
        }

        public static void PerformBishopMove()
        {
            Console.WriteLine("Performing bishop's move...");
        }

        public static void PerformKnightMove()
        {
            Console.WriteLine("Performing knight's move...");
        }

        public static void PerformRookMove(Image rook, int newRow, int newCol, int[,] chessBoard)
        {
            if (rook != null && newRow >= 0 && newRow <= 8 && newCol >= 0 && newCol <= 8)
            {
                var grid = rook.Parent as Grid;

                if (grid != null && newRow < grid.RowDefinitions.Count && newCol < grid.ColumnDefinitions.Count)
                {
                    Grid.SetRow(rook, newRow);
                    Grid.SetColumn(rook, newCol);
                    chessBoard[newRow, newCol] = 1;
                }
            }
        }
    }
}

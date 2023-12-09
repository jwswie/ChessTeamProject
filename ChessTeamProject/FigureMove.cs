using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessTeamProject
{
    public class FigureMove
    {

        public static void PerformPawnMove(Image pawn, int newRow, int newCol, int[,] chessBoard)
        {
            if (pawn != null && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol <= 8)
            {
                var grid = pawn.Parent as Grid;

                if (grid != null && newRow < grid.RowDefinitions.Count && newCol < grid.ColumnDefinitions.Count)
                {
                    Grid.SetRow(pawn, newRow);
                    Grid.SetColumn(pawn, newCol);
                    chessBoard[newRow, newCol] = 1;
                }
            }
        }


        public static void PerformKingMove()
        {
            Console.WriteLine("Performing kings's move...");
        }

        public static void PerformQueenMove(Image bishop, int newRow, int newCol, int[,] chessBoard)
        {
            if (bishop != null && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol <= 8)
            {
                var grid = bishop.Parent as Grid;

                if (grid != null && newRow < grid.RowDefinitions.Count && newCol < grid.ColumnDefinitions.Count)
                {
                    Grid.SetRow(bishop, newRow);
                    Grid.SetColumn(bishop, newCol);
                    chessBoard[newRow, newCol] = 1;
                }
            }
        }

        public static void PerformBishopMove(Image bishop, int newRow, int newCol, int[,] chessBoard)
        {
            if (bishop != null && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol <= 8)
            {
                var grid = bishop.Parent as Grid;

                if (grid != null && newRow < grid.RowDefinitions.Count && newCol < grid.ColumnDefinitions.Count)
                {
                    Grid.SetRow(bishop, newRow);
                    Grid.SetColumn(bishop, newCol);
                    chessBoard[newRow, newCol] = 1;
                }
            }
        }

        public static void PerformKnightMove(Image knight, int newRow, int newCol, int[,] chessBoard)
        {
            if (knight != null && newRow >= 0 && newRow < 8 && newCol >= 0 && newCol <= 8)
            {
                var grid = knight.Parent as Grid;

                if (grid != null && newRow < grid.RowDefinitions.Count && newCol < grid.ColumnDefinitions.Count)
                {
                    Grid.SetRow(knight, newRow);
                    Grid.SetColumn(knight, newCol);
                    chessBoard[newRow, newCol] = 1;
                }
            }
        }

        public static void PerformRookMove()
        {
            Console.WriteLine("Performing rook's move...");
        }
    }
}

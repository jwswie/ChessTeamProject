using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessTeamProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
 
    public partial class MainWindow : Window
    {
        private IPawn selectedPawn;

        private static IChessFactory whiteFactory = new WhiteSideFactory();
        private static IChessFactory blackFactory = new BlackSideFactory();

        private static Board whiteBoard;
        private static Board blackBoard;

        public MainWindow()
        {
            InitializeComponent();

            var game = new Game();

            whiteBoard = game.CreateBoard(whiteFactory);
            blackBoard = game.CreateBoard(blackFactory);

            for (int i = 1; i <= 8; i++) // Adding white pawns
            {
                var pawn = whiteBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 6);
                image.MouseLeftButtonDown += PawnClicked;
                chessBoard.Children.Add(image);
            }

            for (int i = 1; i <= 8; i++) // Adding black pawns
            {
                var pawn = blackBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 1);
                image.MouseLeftButtonDown += PawnClicked;
                chessBoard.Children.Add(image);
            }


            for (int i = 0; i < 2; i++) // Adding white bishops
            {
                var bishop = whiteBoard.B[i];
                var image = bishop.Image;
                int col;
                if(i == 0)
                {
                    col = 3;
                }
                else
                {
                    col = 6;
                }
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 7);
                chessBoard.Children.Add(image);
            }

            for (int i = 0; i < 2; i++) // Adding black bishops
            {
                var bishop = blackBoard.B[i];
                var image = bishop.Image;
                int col;
                if (i == 0)
                {
                    col = 3;
                }
                else
                {
                    col = 6;
                }
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 0);
                chessBoard.Children.Add(image);
            }
        }

        private void PawnClicked(object sender, MouseButtonEventArgs e)
        {
            var clickedImage = (Image)sender;

            var colIndex = Grid.GetColumn(clickedImage) - 1; // Finding pawns index in the List by its column


            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if (colIndex >= 0 && colIndex < whiteBoard.P.Count) // Check that index is in the allowed range
            {
                selectedPawn = whiteBoard.P[colIndex]; // Find the pawn by col index

                ResetCellHighlighting();

                var possibleMoves = GetPossiblePawnMoves(selectedPawn, currentRow, currentCol);

                HighlightCells(possibleMoves);
            }
        }


        private List<Point> GetPossiblePawnMoves(IPawn pawn, int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            // Calculating possible moves for the pawn

            if (pawn is WhitePawn && currentRow > 1)
            {
                possibleMoves.Add(new Point(currentCol, currentRow - 1)); // One cell ahead

                if (currentRow == 6) // If pawn has not moved yet
                {
                    possibleMoves.Add(new Point(currentCol, currentRow - 2)); // Two cells ahead
                }
            }
            else if (pawn is BlackPawn && currentRow < 8)
            {
                possibleMoves.Add(new Point(currentCol, currentRow + 1)); 

                if (currentRow == 1) 
                {
                    possibleMoves.Add(new Point(currentCol, currentRow + 2));
                }
            }

            return possibleMoves;
        }

        private void HighlightCells(List<Point> cells)
        {
            foreach (var cell in cells)
            {
                var border = new Border
                {
                    BorderBrush = (Brush)new BrushConverter().ConvertFromString("Blue"),
                    BorderThickness = new Thickness(5),
                    Opacity = 0.5
                };

                Grid.SetColumn(border, (int)cell.X);
                Grid.SetRow(border, (int)cell.Y);

                chessBoard.Children.Add(border);
            }
        }

        private void ResetCellHighlighting()
        {
            var bordersToRemove = chessBoard.Children.OfType<Border>().ToList();
            foreach (var border in bordersToRemove)
            {
                chessBoard.Children.Remove(border);
            }
        }

    }
}

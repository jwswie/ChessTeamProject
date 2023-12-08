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
        private Image clickedImage;

        private static IChessFactory whiteFactory = new WhiteSideFactory();
        private static IChessFactory blackFactory = new BlackSideFactory();

        private static Board whiteBoard;
        private static Board blackBoard;

        private static int[,] chessBoard = new int[9, 9];

        public MainWindow()
        {
            InitializeComponent();

            GenerateChessBoard();
            GenerateFigures();
        }

        private void GenerateFigures()
        {
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
                MainGrid.Children.Add(image);
            }

            for (int i = 1; i <= 8; i++) // Adding black pawns
            {
                var pawn = blackBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 1);
                image.MouseLeftButtonDown += PawnClicked;
                MainGrid.Children.Add(image);
            }


            for (int i = 0; i < 2; i++) // Adding white bishops
            {
                var bishop = whiteBoard.B[i];
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
                Grid.SetRow(image, 7);
                MainGrid.Children.Add(image);
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
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 2; i++) // Adding white rook
            {
                var rook = whiteBoard.R[i];
                var image = rook.Image;
                int col;
                if (i == 0)
                {
                    col = 1;
                }
                else
                {
                    col = 8;
                }
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 7);
                image.MouseLeftButtonDown += RookClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 2; i++) // Adding black rook
            {
                var rook = blackBoard.R[i];
                var image = rook.Image;
                int col;
                if (i == 0)
                {
                    col = 1;
                }
                else
                {
                    col = 8;
                }
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 0);
                image.MouseLeftButtonDown += RookClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 1; i++) // Adding white king
            {
                var king = whiteBoard.K[i];
                var image = king.Image;
                int col = 5;
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 7);
                image.MouseLeftButtonDown += KingClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 1; i++) // Adding black king
            {
                var king = blackBoard.K[i];
                var image = king.Image;
                int col = 5;
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 0);
                image.MouseLeftButtonDown += KingClicked;
                MainGrid.Children.Add(image);
            }
        }

        private void GenerateChessBoard()
        {
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h" };

            for (int row = 0; row < 9; row++)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Background = new SolidColorBrush(Color.FromRgb(47, 48, 44));
                Label label = new Label();

                if (row != 8)
                {
                    label.Content = 8 - row;
                }
                else
                {
                    label.Content = "";
                }

                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.Foreground = new SolidColorBrush(Color.FromRgb(215, 215, 215));
                label.Height = 91;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.FontSize = 22;
                stackPanel.Children.Add(label);

                Grid.SetRow(stackPanel, row);
                Grid.SetColumn(stackPanel, 0);
                MainGrid.Children.Add(stackPanel);
            }
            for (int col = 1; col < 9; col++)
            {

                StackPanel stackPanel1 = new StackPanel();
                stackPanel1.Background = new SolidColorBrush(Color.FromRgb(47, 48, 44));

                Label label = new Label();
                label.Content = letters[col - 1];
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.Foreground = new SolidColorBrush(Color.FromRgb(215, 215, 215));
                label.Height = 47;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.FontSize = 22;
                stackPanel1.Children.Add(label);


                Grid.SetRow(stackPanel1, 8);
                Grid.SetColumn(stackPanel1, col);
                MainGrid.Children.Add(stackPanel1);
            }
            for (int str = 0; str < 8; str++)
            {
                for (int col = 1; col < 9; col++)
                {
                    StackPanel stackPanel2 = new StackPanel();
                    if (str % 2 == 0)
                    {
                        if (col % 2 == 0)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(58, 128, 43));
                        if (col % 2 == 1)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(244, 255, 221));
                    }
                    else
                    {
                        if (col % 2 == 0)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(244, 255, 221));
                        if (col % 2 == 1)
                            stackPanel2.Background = new SolidColorBrush(Color.FromRgb(58, 128, 43));
                    }
                    Grid.SetRow(stackPanel2, str);
                    Grid.SetColumn(stackPanel2, col);
                    MainGrid.Children.Add(stackPanel2);
                }
            }

        }

        private void PawnClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var colIndex = Grid.GetColumn(clickedImage) - 1; // Finding pawns index in the List by its column

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if (clickedImage.Source.ToString().Contains("white"))
            {
                if (colIndex >= 0 && colIndex < whiteBoard.P.Count) // Check that index is in the allowed range
                {
                    selectedPawn = whiteBoard.P[colIndex]; // Find the pawn by col index

                    ResetCellHighlighting();

                    var possibleMoves = GetPossiblePawnMoves(selectedPawn, currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    foreach (var cell in possibleMoves) // Adding an event handler for each selected cell
                    {
                        var panel = new WrapPanel
                        {
                            Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                            Opacity = 0.5
                        };

                        Grid.SetColumn(panel, (int)cell.X);
                        Grid.SetRow(panel, (int)cell.Y);

                        panel.MouseLeftButtonDown += CellClicked;

                        MainGrid.Children.Add(panel);
                    }
                }
            }
            else if (clickedImage.Source.ToString().Contains("black"))
            {
                if (colIndex >= 0 && colIndex < blackBoard.P.Count)
                {
                    selectedPawn = blackBoard.P[colIndex];

                    ResetCellHighlighting();

                    var possibleMoves = GetPossiblePawnMoves(selectedPawn, currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    foreach (var cell in possibleMoves)
                    {
                        var panel = new WrapPanel
                        {
                            Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                            Opacity = 0.5
                        };

                        Grid.SetColumn(panel, (int)cell.X);
                        Grid.SetRow(panel, (int)cell.Y);

                        panel.MouseLeftButtonDown += CellClicked;

                        MainGrid.Children.Add(panel);
                    }
                }
            }

        }

        private void RookClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var colIndex = Grid.GetColumn(clickedImage) - 1; // Finding pawns index in the List by its column

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if (clickedImage.Source.ToString().Contains("white"))
            {
                if (colIndex >= 0 && colIndex < whiteBoard.P.Count) // Check that index is in the allowed range
                {
                    ResetCellHighlighting();

                    var possibleMoves = GetPossibleRookMoves(currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    foreach (var cell in possibleMoves) // Adding an event handler for each selected cell
                    {
                        var panel = new WrapPanel
                        {
                            Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                            Opacity = 0.5
                        };

                        Grid.SetColumn(panel, (int)cell.X);
                        Grid.SetRow(panel, (int)cell.Y);

                        panel.MouseLeftButtonDown += CellClicked;

                        MainGrid.Children.Add(panel);
                    }
                }
            }
            else if (clickedImage.Source.ToString().Contains("black"))
            {
                if (colIndex >= 0 && colIndex < blackBoard.P.Count)
                {
                    ResetCellHighlighting();

                    var possibleMoves = GetPossibleRookMoves(currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    foreach (var cell in possibleMoves)
                    {
                        var panel = new WrapPanel
                        {
                            Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                            Opacity = 0.5
                        };

                        Grid.SetColumn(panel, (int)cell.X);
                        Grid.SetRow(panel, (int)cell.Y);

                        panel.MouseLeftButtonDown += CellClicked;

                        MainGrid.Children.Add(panel);
                    }
                }
            }

        }

        private void KingClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var colIndex = Grid.GetColumn(clickedImage) - 1; // Finding pawns index in the List by its column

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if (clickedImage.Source.ToString().Contains("white"))
            {
                if (colIndex >= 0 && colIndex < whiteBoard.P.Count) // Check that index is in the allowed range
                {
                    ResetCellHighlighting();

                    var possibleMoves = GetPossibleKingMoves(currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    foreach (var cell in possibleMoves) // Adding an event handler for each selected cell
                    {
                        var panel = new WrapPanel
                        {
                            Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                            Opacity = 0.5
                        };

                        Grid.SetColumn(panel, (int)cell.X);
                        Grid.SetRow(panel, (int)cell.Y);

                        panel.MouseLeftButtonDown += CellClicked;

                        MainGrid.Children.Add(panel);
                    }
                }
            }
            else if (clickedImage.Source.ToString().Contains("black"))
            {
                if (colIndex >= 0 && colIndex < blackBoard.P.Count)
                {
                    ResetCellHighlighting();

                    var possibleMoves = GetPossibleKingMoves(currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    foreach (var cell in possibleMoves)
                    {
                        var panel = new WrapPanel
                        {
                            Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                            Opacity = 0.5
                        };

                        Grid.SetColumn(panel, (int)cell.X);
                        Grid.SetRow(panel, (int)cell.Y);

                        panel.MouseLeftButtonDown += CellClicked;

                        MainGrid.Children.Add(panel);
                    }
                }
            }

        }

        private List<Point> GetPossiblePawnMoves(IPawn pawn, int currentRow, int currentCol) // Point - coordinates (x, y)
        {
            //Element of the list - pair of (x, y) coordinates for a cell of possible move

            var possibleMoves = new List<Point>();

            if (pawn is WhitePawn && currentRow > 1)
            {
                var oneCellAhead = new Point(currentCol, currentRow - 1);

                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead); // One cell ahead

                    if (currentRow == 6) // If pawn has not moved yet
                    {
                        var twoCellsAhead = new Point(currentCol, currentRow - 2);
                        if (!IsCellOccupied(chessBoard, (int)twoCellsAhead.X, (int)twoCellsAhead.Y))
                        {
                            possibleMoves.Add(twoCellsAhead); // Two cells ahead
                        }
                    }
                }

            }
            else if (pawn is BlackPawn && currentRow < 8)
            {
                var oneCellAhead = new Point(currentCol, currentRow + 1);
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);

                    if (currentRow == 1)
                    {
                        var twoCellsAhead = new Point(currentCol, currentRow + 2);
                        if (!IsCellOccupied(chessBoard, (int)twoCellsAhead.X, (int)twoCellsAhead.Y))
                        {
                            possibleMoves.Add(twoCellsAhead);
                        }
                    }
                }
            }

            return possibleMoves;
        }

        private List<Point> GetPossibleRookMoves(int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            // Horizontal moves
            for (int col = currentCol + 1; col <= 8; col++)
            {
                Point oneCellAhead = new Point(col, currentRow);
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }

            for (int col = currentCol - 1; col >= 1; col--)
            {
                Point oneCellAhead = new Point(col, currentRow);
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }

            // Vertical moves
            for (int row = currentRow + 1; row <= 7; row++)
            {
                Point oneCellAhead = new Point(currentCol, row);
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }

            for (int row = currentRow - 1; row >= 0; row--)
            {
                Point oneCellAhead = new Point(currentCol, row);
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }

            return possibleMoves;
        }

        private List<Point> GetPossibleKingMoves(int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            for (int row = currentRow - 1; row <= currentRow + 1; row++)
            {
                for (int col = currentCol - 1; col <= currentCol + 1; col++)
                {
                    if (row >= 0 && row <= 7 && col >= 0 && col <= 7 && (row != currentRow || col != currentCol))
                    {
                        Point oneCellAhead = new Point(col, row);
                        if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                        {
                            possibleMoves.Add(oneCellAhead);
                        }
                    }
                }
            }

            return possibleMoves;
        }

        private bool IsCellOccupied(int[,] chessBoard, int col, int row)
        {

            if (chessBoard[row, col] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        private void CellClicked(object sender, MouseButtonEventArgs e)
        {
            var clickedBorder = (WrapPanel)sender;
            var newCol = Grid.GetColumn(clickedBorder);
            var newRow = Grid.GetRow(clickedBorder);

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            FigureMove.PerformPawnMove(clickedImage, newRow, newCol, chessBoard);
            FigureMove.PerformRookMove(clickedImage, newRow, newCol, chessBoard);
            FigureMove.PerformKingMove(clickedImage, newRow, newCol, chessBoard);

            chessBoard[currentRow, currentCol] = 0;

            ResetCellHighlighting();
        }

        private void HighlightCells(List<Point> cells)
        {
            foreach (var cell in cells)
            {
                var panel = new WrapPanel
                {
                    Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                    Opacity = 0.5
                };

                Grid.SetColumn(panel, (int)cell.X);
                Grid.SetRow(panel, (int)cell.Y);

                MainGrid.Children.Add(panel);
            }
        }

        private void ResetCellHighlighting()
        {
            var panelsToRemove = MainGrid.Children.OfType<WrapPanel>().ToList();
            foreach (var panel in panelsToRemove)
            {
                panel.MouseLeftButtonDown -= CellClicked;
                MainGrid.Children.Remove(panel);
            }
        }
    }
}

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
using System.Windows.Media.Animation;

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

        private static int[,] chessBoard = new int[9, 9]; // Array showing the position of figures on the board (0 - empty cell, 1 - white, 2 - black)

        private static string currentSide;
        private static bool flag;

        public MainWindow()
        {
            InitializeComponent();

            LogInWindow log = new LogInWindow();
            log.ShowDialog();

            GenerateChessBoard();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    chessBoard[row, col] = 0;
                }
            }

            var game = new Game();

            whiteBoard = game.CreateBoard(whiteFactory);
            blackBoard = game.CreateBoard(blackFactory);

            currentSide = "white";

            for (int i = 1; i <= 8; i++) // Adding white pawns
            {
                var pawn = whiteBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 6);
                chessBoard[6, i] = 1;
                image.MouseLeftButtonDown += PawnClicked;
                image.Tag = "white";
                MainGrid.Children.Add(image);

                Animation.AnimatePiece(image);
            }

            for (int i = 1; i <= 8; i++) // Adding black pawns
            {
                var pawn = blackBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 1);
                chessBoard[1, i] = 2;
                image.MouseLeftButtonDown += PawnClicked;
                image.Tag = "black";
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
                chessBoard[7, col] = 1;
                image.MouseLeftButtonDown += BishopClicked;
                image.Tag = "white";
                MainGrid.Children.Add(image);

                Animation.AnimatePiece(image);
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
                chessBoard[0, col] = 2;
                image.MouseLeftButtonDown += BishopClicked;
                image.Tag = "black";
                MainGrid.Children.Add(image);
            }

            Animation.SetFiguresClickability("black", false, MainGrid);
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

        private void PawnClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var colIndex = Grid.GetColumn(clickedImage) - 1; // Finding pawns index in the List by its column

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if ((string)clickedImage.Tag == "white")
            {
                if (colIndex >= 0 && colIndex < whiteBoard.P.Count) // Check that index is in the allowed range
                {
                    selectedPawn = whiteBoard.P[colIndex]; // Find the pawn by col index

                    ResetCellHighlighting();

                    var possibleMoves = GetPossiblePawnMoves(selectedPawn, currentRow, currentCol);

                    HighlightCells(possibleMoves);
                }
            }

            else if ((string)clickedImage.Tag == "black")
            {
                if (colIndex >= 0 && colIndex < blackBoard.P.Count)
                {
                    selectedPawn = blackBoard.P[colIndex];

                    ResetCellHighlighting();

                    var possibleMoves = GetPossiblePawnMoves(selectedPawn, currentRow, currentCol);

                    HighlightCells(possibleMoves);
                }
            }

        }

        private void BishopClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if ((string)clickedImage.Tag == "white")
            {
                ResetCellHighlighting();

                var possibleMoves = GetPossibleBishopMoves(currentRow, currentCol);

                HighlightCells(possibleMoves);
            }
            else if ((string)clickedImage.Tag == "black")
            {
                ResetCellHighlighting();

                var possibleMoves = GetPossibleBishopMoves(currentRow, currentCol);

                HighlightCells(possibleMoves);
            }
        }

        private List<Point> GetPossiblePawnMoves(IPawn pawn, int currentRow, int currentCol) // Point - coordinates (x, y)
        {
            //Element of the list - pair of (x, y) coordinates for a cell of possible move
            var possibleMoves = new List<Point>();

            if (pawn.Side == "White")
            {
                var oneCellAhead = new Point(currentCol, currentRow - 1);

                if (!IsCellOccupiedPawn(chessBoard, (int)oneCellAhead.Y, (int)oneCellAhead.X))
                {
                    possibleMoves.Add(oneCellAhead); // One cell ahead

                    if (currentRow == 6) // If pawn has not moved yet
                    {
                        var twoCellsAhead = new Point(currentCol, currentRow - 2);
                        if (!IsCellOccupiedPawn(chessBoard, (int)twoCellsAhead.Y, (int)twoCellsAhead.X))
                        {
                            possibleMoves.Add(twoCellsAhead); // Two cells ahead
                        }
                    }
                }
                if (currentCol - 1 > 0 && currentRow - 1 >= 0)
                {
                    var leftDiagonal = new Point(currentCol - 1, currentRow - 1);

                    if (IsThereEnemyFigure(chessBoard, (int)leftDiagonal.Y, (int)leftDiagonal.X))
                    {
                        int newRow = (int)leftDiagonal.Y;
                        int newCol = (int)leftDiagonal.X;

                        possibleMoves.Add(new Point(newCol, newRow));
                    }
                }
                if (currentCol + 1 < 9 && currentRow - 1 >= 0)
                {
                    var rightDiagonal = new Point(currentCol + 1, currentRow - 1);

                    if (IsThereEnemyFigure(chessBoard, (int)rightDiagonal.Y, (int)rightDiagonal.X))
                    {
                        int newRow = (int)rightDiagonal.Y;
                        int newCol = (int)rightDiagonal.X;

                        possibleMoves.Add(new Point(newCol, newRow));
                    }
                }
            }

            else if (pawn.Side == "Black" && currentRow < 8)
            {
                var oneCellAhead = new Point(currentCol, currentRow + 1);

                if (!IsCellOccupiedPawn(chessBoard, (int)oneCellAhead.Y, (int)oneCellAhead.X))
                {
                    possibleMoves.Add(oneCellAhead);

                    if (currentRow == 1)
                    {
                        var twoCellsAhead = new Point(currentCol, currentRow + 2);
                        if (!IsCellOccupiedPawn(chessBoard, (int)twoCellsAhead.Y, (int)twoCellsAhead.X))
                        {
                            possibleMoves.Add(twoCellsAhead);
                        }
                    }
                }

                if (currentCol - 1 > 0 && currentRow + 1 < 9)
                {
                    var leftDiagonal = new Point(currentCol - 1, currentRow + 1);

                    if (IsThereEnemyFigure(chessBoard, (int)leftDiagonal.Y, (int)leftDiagonal.X))
                    {
                        int newRow = (int)leftDiagonal.Y;
                        int newCol = (int)leftDiagonal.X;

                        possibleMoves.Add(new Point(newCol, newRow));
                    }
                }
                if (currentCol + 1 < 9 && currentRow + 1 < 9)
                {
                    var rightDiagonal = new Point(currentCol + 1, currentRow + 1);

                    if (IsThereEnemyFigure(chessBoard, (int)rightDiagonal.Y, (int)rightDiagonal.X))
                    {
                        int newRow = (int)rightDiagonal.Y;
                        int newCol = (int)rightDiagonal.X;

                        possibleMoves.Add(new Point(newCol, newRow));
                    }
                }
            }

            return possibleMoves;
        }

        private List<Point> GetPossibleBishopMoves(int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            if ((string)clickedImage.Tag == "white" && currentRow >= 0)
            {
                CheckDiagonal(possibleMoves, currentRow, currentCol, chessBoard);

            }
            else if ((string)clickedImage.Tag == "black" && currentRow >= 0)
            {
                CheckDiagonal(possibleMoves, currentRow, currentCol, chessBoard);
            }

            return possibleMoves;
        }

        private void CheckDiagonal(List<Point> possibleMoves, int currentRow, int currentCol, int[,] chessBoard)
        {
            for (int n = 1; n <= Math.Min(currentRow, currentCol - 1); n++) // LEFT UP
            {
                int newRow = currentRow - n;
                int newCol = currentCol - n;

                if (IsCellOccupied(chessBoard, newCol, newRow))
                {
                    break; // If the cell is occupied, stop moving diagonally
                }

                if (flag)
                {
                    flag = false;
                    possibleMoves.Add(new Point(newCol, newRow));
                    break;
                }

                possibleMoves.Add(new Point(newCol, newRow));

                // Check if the current cell is a corner cell and free, add it to possible moves
                if (n == Math.Min(currentRow, currentCol - 1) && !IsCellOccupied(chessBoard, newCol, newRow))
                {
                    possibleMoves.Add(new Point(newCol, newRow));
                }
            }

            for (int n = 1; n <= Math.Min(currentRow, 8 - currentCol); n++) // RIGHT UP
            {
                int newRow = currentRow - n;
                int newCol = currentCol + n;

                if (IsCellOccupied(chessBoard, newCol, newRow))
                {
                    break;
                }

                if (flag)
                {
                    flag = false;
                    possibleMoves.Add(new Point(newCol, newRow));
                    break;
                }

                possibleMoves.Add(new Point(newCol, newRow));

                if (n == Math.Min(currentRow, 8 - currentCol) && !IsCellOccupied(chessBoard, newCol, newRow))
                {
                    possibleMoves.Add(new Point(newCol, newRow));
                }
            }

            for (int n = 1; n <= Math.Min(7 - currentRow, currentCol - 1); n++) // LEFT DOWN
            {
                int newRow = currentRow + n;
                int newCol = currentCol - n;

                if (IsCellOccupied(chessBoard, newCol, newRow))
                {
                    break;
                }

                if (flag)
                {
                    flag = false;
                    possibleMoves.Add(new Point(newCol, newRow));
                    break;
                }

                possibleMoves.Add(new Point(newCol, newRow));

                if (n == Math.Min(7 - currentRow, currentCol - 1) && !IsCellOccupied(chessBoard, newRow, newCol))
                {
                    possibleMoves.Add(new Point(newCol, newRow));
                }
            }

            for (int n = 1; n <= Math.Min(7 - currentRow, 8 - currentCol); n++) // RIGHT DOWN
            {
                int newRow = currentRow + n;
                int newCol = currentCol + n;

                if (IsCellOccupied(chessBoard, newCol, newRow))
                {
                    break;
                }

                if (flag)
                {
                    flag = false;
                    possibleMoves.Add(new Point(newCol, newRow));
                    break;
                }

                possibleMoves.Add(new Point(newCol, newRow));

                if (n == Math.Min(7 - currentRow, 8 - currentCol) && !IsCellOccupied(chessBoard, newRow, newCol))
                {
                    possibleMoves.Add(new Point(newCol, newRow));
                }
            }
        }

        private bool IsCellOccupied(int[,] chessBoard, int col, int row)
        {
            if (currentSide == "white" && chessBoard[row, col] == 1) // White and White
            {
                flag = false;
                return true;
            }
            if (currentSide == "white" && chessBoard[row, col] == 2) // White and Black
            {
                flag = true;
                return false;
            }
            if (currentSide == "black" && chessBoard[row, col] == 1) // Black and White
            {
                flag = true;
                return false;
            }
            if (currentSide == "black" && chessBoard[row, col] == 2) // Black and Black
            {
                flag = false;
                return true;
            }
            return false;
        }

        private bool IsThereEnemyFigure(int[,] chessBoard, int row, int col)
        {
            if (currentSide == "white" && chessBoard[row, col] == 2) // White and Black
            {
                return true;
            }
            if (currentSide == "black" && chessBoard[row, col] == 1) // Black and White
            {
                return true;
            }
            return false;
        }

        private bool IsCellOccupiedPawn(int[,] chessBoard, int row, int col)
        {
            if (chessBoard[row, col] == 1 || chessBoard[row, col] == 2)
            {
                return true;
            }
            return false;
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

            if (currentSide == "white")
            {
                if (chessBoard[newRow, newCol] == 2)
                {
                    UIElement elementToRemove = null;

                    foreach (UIElement element in MainGrid.Children)
                    {
                        if (element is Image && Grid.GetRow(element) == newRow && Grid.GetColumn(element) == newCol)
                        {
                            elementToRemove = element;
                            break;
                        }
                    }

                    if (elementToRemove != null)
                    {
                        MainGrid.Children.Remove(elementToRemove);
                        chessBoard[newRow, newCol] = 0;
                    }
                }
                currentSide = "black";
                Animation.SetFiguresClickability("black", true, MainGrid); Animation.SetFiguresClickability("white", false, MainGrid);
                Animation.GetSideAnimation("black", MainGrid);
            }
            else if (currentSide == "black")
            {
                if (chessBoard[newRow, newCol] == 1)
                {
                    UIElement elementToRemove = null;

                    foreach (UIElement element in MainGrid.Children)
                    {
                        if (element is Image && Grid.GetRow(element) == newRow && Grid.GetColumn(element) == newCol)
                        {
                            elementToRemove = element;
                            break;
                        }
                    }

                    if (elementToRemove != null)
                    {
                        MainGrid.Children.Remove(elementToRemove);
                        chessBoard[newRow, newCol] = 0;
                    }
                }

                currentSide = "white";
                Animation.SetFiguresClickability("white", true, MainGrid); Animation.SetFiguresClickability("black", false, MainGrid);
                Animation.GetSideAnimation("white", MainGrid);
            }

            FigureMove.PerformPawnMove(clickedImage, newRow, newCol, chessBoard, currentSide);

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

                panel.MouseLeftButtonDown += CellClicked; // Adding an event handler for each selected cell

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

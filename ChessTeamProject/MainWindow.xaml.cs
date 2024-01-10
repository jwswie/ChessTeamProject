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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChessTeamProject
{
    public partial class MainWindow : Window
    {
        private IPawn selectedPawn;
        private Image clickedImage;

        private static IChessFactory whiteFactory = new WhiteSideFactory();
        private static IChessFactory blackFactory = new BlackSideFactory();

        private static Board whiteBoard;
        private static Board blackBoard;

        private static int[,] chessBoard = new int[9, 9];

        private bool whiteKingMoved = false;
        private bool blackKingMoved = false;
        private bool whiteLeftRookMoved = false;
        private bool whiteRightRookMoved = false;
        private bool blackLeftRookMoved = false;
        private bool blackRightRookMoved = false;

        private static string currentSide;
        private static bool flag;

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

            currentSide = "white";

            for (int i = 1; i <= 8; i++) // Adding white pawns
            {
                var pawn = whiteBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 6);
                image.MouseLeftButtonDown += PawnClicked;
                image.Tag = "white";
                MainGrid.Children.Add(image);
            }

            for (int i = 1; i <= 8; i++) // Adding black pawns
            {
                var pawn = blackBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 1);
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
                image.Tag = "white";
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
                image.Tag = "black";
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
                image.Tag = "white";
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
                image.Tag = "black";
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

        private void RookClicked(object sender, MouseButtonEventArgs e)
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
            else if ((string)clickedImage.Tag == "black")
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

            var colIndex = Grid.GetColumn(clickedImage) - 1;

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if ((string)clickedImage.Tag == "white")
            {
                if (colIndex >= 0 && colIndex < whiteBoard.P.Count)
                {
                    ResetCellHighlighting();

                    var possibleMoves = GetPossibleKingMoves(currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    if (!whiteKingMoved)
                    {
                        PerformCastling(whiteLeftRookMoved, whiteRightRookMoved, currentRow, currentCol, colIndex == 1 || colIndex == 6);
                    }
                }
            }
            else if ((string)clickedImage.Tag == "black")
            {
                if (colIndex >= 0 && colIndex < blackBoard.P.Count)
                {
                    ResetCellHighlighting();

                    var possibleMoves = GetPossibleKingMoves(currentRow, currentCol);

                    HighlightCells(possibleMoves);

                    if (!blackKingMoved)
                    {
                        PerformCastling(blackLeftRookMoved, blackRightRookMoved, currentRow, currentCol, colIndex == 1 || colIndex == 6);
                    }
                }
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

        private List<Point> GetPossibleRookMoves(int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            // Горизонтальные ходы
            for (int col = currentCol + 1; col <= 8; col++)
            {
                Point oneCellAhead = new Point(col, currentRow);
                if (IsCellOccupied(chessBoard, col, currentRow))
                {
                    if (chessBoard[currentRow, col] != chessBoard[currentRow, currentCol])
                    {
                        possibleMoves.Add(oneCellAhead);
                    }
                    break;
                }
                possibleMoves.Add(oneCellAhead);
            }

            for (int col = currentCol - 1; col >= 1; col--)
            {
                Point oneCellAhead = new Point(col, currentRow);
                if (IsCellOccupied(chessBoard, col, currentRow))
                {
                    if (chessBoard[currentRow, col] != chessBoard[currentRow, currentCol])
                    {
                        possibleMoves.Add(oneCellAhead);
                    }
                    break;
                }
                possibleMoves.Add(oneCellAhead);
            }

            // Вертикальные ходы
            for (int row = currentRow + 1; row <= 7; row++)
            {
                Point oneCellAhead = new Point(currentCol, row);
                if (IsCellOccupied(chessBoard, currentCol, row))
                {
                    if (chessBoard[row, currentCol] != chessBoard[currentRow, currentCol])
                    {
                        possibleMoves.Add(oneCellAhead);
                    }
                    break;
                }
                possibleMoves.Add(oneCellAhead);
            }

            for (int row = currentRow - 1; row >= 0; row--)
            {
                Point oneCellAhead = new Point(currentCol, row);
                if (IsCellOccupied(chessBoard, currentCol, row))
                {
                    if (chessBoard[row, currentCol] != chessBoard[currentRow, currentCol])
                    {
                        possibleMoves.Add(oneCellAhead);
                    }
                    break;
                }
                possibleMoves.Add(oneCellAhead);
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
                        if (!IsCellOccupied(chessBoard, col, row) || IsThereEnemyFigure(chessBoard, row, col))
                        {
                            possibleMoves.Add(oneCellAhead);
                        }
                    }
                }
            }

            return possibleMoves;
        }


        private void PerformCastling(bool leftRookMoved, bool rightRookMoved, int currentRow, int currentCol, bool longCastle)
        {
            var leftRookCell = new Point(1, currentRow);
            var rightRookCell = new Point(8, currentRow);

            if (longCastle)
            {
                leftRookCell = new Point(4, currentRow);
            }

            if (!leftRookMoved && !longCastle)
            {
                var intermediateCells = new List<Point> { new Point(2, currentRow), new Point(3, currentRow), new Point(4, currentRow) };
                if (intermediateCells.All(cell => !IsCellOccupied(chessBoard, (int)cell.X, (int)cell.Y)))
                {
                    HighlightCells(new List<Point> { leftRookCell });

                    var leftRookPanel = new WrapPanel
                    {
                        Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                        Opacity = 0.5
                    };

                    Grid.SetColumn(leftRookPanel, (int)leftRookCell.X);
                    Grid.SetRow(leftRookPanel, (int)leftRookCell.Y);
                    leftRookPanel.MouseLeftButtonDown += CastlingCellClicked;
                    MainGrid.Children.Add(leftRookPanel);
                }
            }

            if (!rightRookMoved && !longCastle)
            {
                var intermediateCells = new List<Point> { new Point(6, currentRow), new Point(7, currentRow) };
                if (intermediateCells.All(cell => !IsCellOccupied(chessBoard, (int)cell.X, (int)cell.Y)))
                {
                    HighlightCells(new List<Point> { rightRookCell });

                    var rightRookPanel = new WrapPanel
                    {
                        Background = (Brush)new BrushConverter().ConvertFromString("Blue"),
                        Opacity = 0.5
                    };

                    Grid.SetColumn(rightRookPanel, (int)rightRookCell.X);
                    Grid.SetRow(rightRookPanel, (int)rightRookCell.Y);
                    rightRookPanel.MouseLeftButtonDown += CastlingCellClicked;
                    MainGrid.Children.Add(rightRookPanel);
                }
            }
        }

        private void MoveFigure(Image figure, int newRow, int newCol)
        {
            Grid.SetRow(figure, newRow);
            Grid.SetColumn(figure, newCol);
        }

        private Image GetRookByDirection(int row, int direction)
        {
            return MainGrid.Children.OfType<Image>()
                .FirstOrDefault(img => Grid.GetRow(img) == row && Grid.GetColumn(img) == (direction > 0 ? 8 : 1));
        }

        private void CastlingCellClicked(object sender, MouseButtonEventArgs e)
        {
            var clickedPanel = (WrapPanel)sender;
            var newCol = Grid.GetColumn(clickedPanel);
            var newRow = Grid.GetRow(clickedPanel);

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);

            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            int direction = newCol > currentCol ? 1 : -1;

            if (direction == 1) // Короткая рокировка
            {
                MoveFigure(clickedImage, newRow, 7); // Король перемещается на g1
                MoveFigure(GetRookByDirection(currentRow, direction), newRow, 6); // Ладья перемещается на f1
            }
            else // Длинная рокировка
            {
                MoveFigure(clickedImage, newRow, 3); // Король перемещается на c1
                MoveFigure(GetRookByDirection(currentRow, direction), newRow, 4); // Ладья перемещается на d1
            }

            chessBoard[currentRow, currentCol] = 0;
            chessBoard[newRow, newCol] = (string)clickedImage.Tag == "white" ? 1 : 2;

            if ((string)clickedImage.Tag == "white")
            {
                whiteKingMoved = true;
                if (direction == -1)
                {
                    whiteLeftRookMoved = true;
                }
                else
                {
                    whiteRightRookMoved = true;
                }
            }
            else
            {
                blackKingMoved = true;
                if (direction == -1)
                {
                    blackLeftRookMoved = true;
                }
                else
                {
                    blackRightRookMoved = true;
                }
            }

            ResetCellHighlighting();
        }


        private bool IsCellOccupied(int[,] chessBoard, int col, int row)
        {
            return chessBoard[row, col] != 0;
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

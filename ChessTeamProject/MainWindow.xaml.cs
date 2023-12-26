using ChessTeamProject;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp17
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool f = true;
        private int CurrentMove = 1;
        int NumberOfGame = 1;
        private IPawn selectedPawn;
        private Image clickedImage;

        private static IChessFactory whiteFactory = new WhiteSideFactory();
        private static IChessFactory blackFactory = new BlackSideFactory();

        private static Board whiteBoard;
        private static Board blackBoard;

        private static int[,] chessBoard = new int[9, 9];
        private static int[] NumbersOnBoard = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static string[] LettersOnBoard = { "a", "b", "c", "d", "e", "f", "g", "h" };
        private static string[] AllImages = { new WhitePawn().Image.Name, new BlackPawn().Image.Name, new WhiteKnight().Image.Name, new BlackKnight().Image.Name, new WhiteBishop().Image.Name, new BlackBishop().Image.Name, new WhiteRook().Image.Name, new BlackRook().Image.Name, new WhiteQueen().Image.Name, new BlackQueen().Image.Name, new WhiteKing().Image.Name, new BlackKing().Image.Name };
        private static string[] Allsymbols = { new WhitePawn().Symbol, new BlackPawn().Symbol, new WhiteKnight().Symbol, new BlackKnight().Symbol, new WhiteBishop().Symbol, new BlackBishop().Symbol, new WhiteRook().Symbol, new BlackRook().Symbol, new WhiteQueen().Symbol, new BlackQueen().Symbol, new WhiteKing().Symbol, new BlackKing().Symbol };
        public MainWindow()
        {
            InitializeComponent();
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

            for (int i = 1; i <= 8; i++) // Adding white pawns
            {
                var pawn = whiteBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 6);
                chessBoard[6, i] = 1;
                image.MouseLeftButtonDown += PawnClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 1; i <= 8; i++) // Adding black pawns
            {
                var pawn = blackBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 1);
                chessBoard[1, i] = 1;
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
                chessBoard[7, col] = 1;
                image.MouseLeftButtonDown += BishopClicked;
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
                chessBoard[0, col] = 1;
                image.MouseLeftButtonDown += BishopClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 2; i++) // Adding white knights
            {
                var knight = whiteBoard.Kn[i];
                var image = knight.Image;
                int col = 0;
                switch (i)
                {
                    case 0:
                        { col = 2; break; }
                    case 1:
                        { col = 7; break; }
                };
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 7);
                chessBoard[7, col] = 1;
                image.MouseLeftButtonDown += KnightClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 2; i++) // Adding black knights
            {
                var knight = blackBoard.Kn[i];
                var image = knight.Image;
                int col = 0;
                switch (i)
                {
                    case 0:
                        { col = 2; break; }
                    case 1:
                        { col = 7; break; }
                };
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 0);
                chessBoard[0, col] = 1;
                image.MouseLeftButtonDown += KnightClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 1; i++) // Adding white queen
            {
                var king = whiteBoard.Q[i];
                var image = king.Image;
                int col = 4;
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 7);
                chessBoard[7, col] = 1;
                image.MouseLeftButtonDown += QueenClicked;
                MainGrid.Children.Add(image);
            }

            for (int i = 0; i < 1; i++) // Adding black queen
            {
                var king = blackBoard.Q[i];
                var image = king.Image;
                int col = 4;
                Grid.SetColumn(image, col);
                Grid.SetRow(image, 0);
                chessBoard[0, col] = 1;
                image.MouseLeftButtonDown += QueenClicked;
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

        private void BishopClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);



            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            if (clickedImage.Source.ToString().Contains("white"))
            {

                ResetCellHighlighting();

                var possibleMoves = GetPossibleBishopMoves(currentRow, currentCol);

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
            else if (clickedImage.Source.ToString().Contains("black"))
            {


                ResetCellHighlighting();

                var possibleMoves = GetPossibleBishopMoves(currentRow, currentCol);

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

        private void KnightClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);



            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            ResetCellHighlighting();

            var possibleMoves = GetPossibleKnightMoves(currentRow, currentCol);

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
        private void QueenClicked(object sender, MouseButtonEventArgs e)
        {
            clickedImage = (Image)sender;

            var row = Grid.GetRow(clickedImage);
            var col = Grid.GetColumn(clickedImage);



            int currentRow = int.Parse(row.ToString());
            int currentCol = int.Parse(col.ToString());

            ResetCellHighlighting();

            var possibleMoves = GetPossibleQueenMoves(currentRow, currentCol);

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

        private List<Point> GetPossiblePawnMoves(IPawn pawn, int currentRow, int currentCol) // Point - coordinates (x, y)
        {
            //Element of the list - pair of (x, y) coordinates for a cell of possible move

            var possibleMoves = new List<Point>();

            if (pawn.Side == "White" && currentRow > 1)
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
            else if (pawn.Side == "Black" && currentRow < 8)
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

        private List<Point> GetPossibleBishopMoves(int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            if (clickedImage.Source.ToString().Contains("white") && currentRow >= 0)
            {
                CheckDiagonal(possibleMoves, currentRow, currentCol, chessBoard);

            }
            else if (clickedImage.Source.ToString().Contains("black") && currentRow >= 0)
            {
                CheckDiagonal(possibleMoves, currentRow, currentCol, chessBoard);
            }

            return possibleMoves;
        }

        private List<Point> GetPossibleKnightMoves(int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            if (currentRow >= 0 && currentCol>=1)
            {
                CheckKnightMoves(possibleMoves, currentRow, currentCol, chessBoard);
            }

            return possibleMoves;
        }
        private List<Point> GetPossibleQueenMoves(int currentRow, int currentCol)
        {
            var possibleMoves = new List<Point>();

            if (currentRow >= 0)
            {
                CheckVertical(possibleMoves, currentRow, currentCol, chessBoard);
                CheckDiagonal(possibleMoves, currentRow, currentCol, chessBoard);
            }

            return possibleMoves;
        }

        private void CheckKnightMoves(List<Point> possibleMoves, int currentRow, int currentCol, int[,] chessBoard)
        {
            //lower-right
            for (int n = 2, m = 1; n>0; n--, m++)
            {
                if (7 >= currentRow + n && 8 >= currentCol + m)
                {
                    int newRow = currentRow + n;
                    int newCol = currentCol + m;

                    if (!IsCellOccupied(chessBoard, newCol, newRow))
                    {
                        possibleMoves.Add(new Point(newCol, newRow));

                    }
                }
            }
            //upper-left
            for (int n = -2, m = -1; n<0; n++, m--)
            {
                if (0 <= currentRow + n && 1 <= currentCol + m)
                {
                    int newRow = currentRow + n;
                    int newCol = currentCol + m;

                    if (!IsCellOccupied(chessBoard, newCol, newRow))
                    {
                        possibleMoves.Add(new Point(newCol, newRow));

                    }
                }
            }
            //upper-right
            for (int n = -2, m = 1; n<0; n++, m++)
            {
                if (0 <= currentRow + n && 8 >= currentCol + m)
                {
                    int newRow = currentRow + n;
                    int newCol = currentCol + m;

                    if (!IsCellOccupied(chessBoard, newCol, newRow))
                    {
                        possibleMoves.Add(new Point(newCol, newRow));

                    }
                }
            }
            //lower-left
            for (int n = 1, m = -2; m<0; n++, m++)
            {
                if (7 >= currentRow + n && 1 <= currentCol + m)
                {
                    int newRow = currentRow + n;
                    int newCol = currentCol + m;

                    if (!IsCellOccupied(chessBoard, newCol, newRow))
                    {
                        possibleMoves.Add(new Point(newCol, newRow));


                    }
                }
            }

        }

        private void CheckDiagonal(List<Point> possibleMoves, int currentRow, int currentCol, int[,] chessBoard)
        {
            for (int n = 1; n <= Math.Min(currentRow, currentCol - 1); n++) // LEFT Up
            {
                int newRow = currentRow - n;
                int newCol = currentCol - n;

                if (IsCellOccupied(chessBoard, newCol, newRow))
                {
                    break; // If the cell is occupied, stop moving diagonally
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

                possibleMoves.Add(new Point(newCol, newRow));

                if (n == Math.Min(currentRow, 8 - currentCol) && !IsCellOccupied(chessBoard, newCol, newRow))
                {
                    possibleMoves.Add(new Point(newCol, newRow));
                }
            }

            for (int n = 1; n <= Math.Min(7 - currentRow, currentCol); n++) // LEFT DOWN
            {
                int newRow = currentRow + n;
                int newCol = currentCol - n;

                if (IsCellOccupied(chessBoard, newRow, newCol))
                {
                    break;
                }

                possibleMoves.Add(new Point(newCol, newRow));

                if (n == Math.Min(8 - currentRow, currentCol) && !IsCellOccupied(chessBoard, newRow, newCol))
                {
                    possibleMoves.Add(new Point(newCol, newRow));
                }
            }

            for (int n = 1; n <= Math.Min(7 - currentRow, 8 - currentCol); n++) // RIGHT DOWN
            {
                int newRow = currentRow + n;
                int newCol = currentCol + n;

                if (IsCellOccupied(chessBoard, newRow, newCol))
                {
                    break;
                }

                possibleMoves.Add(new Point(newCol, newRow));

                if (n == Math.Min(7 - currentRow, 8 - currentCol) && !IsCellOccupied(chessBoard, newRow, newCol))
                {
                    possibleMoves.Add(new Point(newCol, newRow));
                }
            }
        }
        private void CheckVertical(List<Point> possibleMoves, int currentRow, int currentCol, int[,] chessBoard)
        {

            // Horizontal moves
            for (int col = currentCol + 1; col <= 8; col++)
            {
                Point oneCellAhead = new Point(col, currentRow);
                if (IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                    break;
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }

            for (int col = currentCol - 1; col >= 1; col--)
            {
                Point oneCellAhead = new Point(col, currentRow);
                if (IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                    break;
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }

            // Vertical moves
            for (int row = currentRow + 1; row <= 7; row++)
            {
                Point oneCellAhead = new Point(currentCol, row);
                if (IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                    break;
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }

            for (int row = currentRow - 1; row >= 0; row--)
            {
                Point oneCellAhead = new Point(currentCol, row);
                if (IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                    break;
                if (!IsCellOccupied(chessBoard, (int)oneCellAhead.X, (int)oneCellAhead.Y))
                {
                    possibleMoves.Add(oneCellAhead);
                }
            }
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
            for (int i = 0; i < AllImages.Length; i++)
                if (clickedImage.Name == AllImages[i])
                {
                    if (clickedImage.Name.Contains("White"))
                        SavingInformation(newCol, newRow, Allsymbols[i], "White");
                    else
                        SavingInformation(newCol, newRow, Allsymbols[i], "Black");
                }
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

        private async Task SavingInformation(int col, int row, string symbol, string ColorSide)
        {

            while (f)
            {
                if (!File.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\ChessGame{NumberOfGame}.txt"))
                {
                    FileStream fileStream = new FileStream($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\ChessGame{NumberOfGame}.txt", FileMode.Create);
                    fileStream.Close();
                    f = false;
                    break;
                }
                else
                    NumberOfGame+=1;
            }
            using (StreamWriter fstream = new StreamWriter($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\ChessGame{NumberOfGame}.txt", true))
            {
                if (ColorSide=="White")
                {
                    fstream.Write($"{CurrentMove}.{symbol}{LettersOnBoard[col]}{NumbersOnBoard[row]}");
                    CurrentMove += 1;
                }
                else
                    fstream.Write($"\t{symbol}{LettersOnBoard[col]}{NumbersOnBoard[row]}\n");
            }
        }
    }
}

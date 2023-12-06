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

        public MainWindow()
        {
            InitializeComponent();

            var game = new Game();
            var whiteFactory = new WhiteSideFactory();
            var blackFactory = new BlackSideFactory();

            var whiteBoard = game.CreateBoard(whiteFactory);
            var blackBoard = game.CreateBoard(blackFactory);

            for (int i = 1; i <= 8; i++) // Adding white pawns
            {
                var pawn = whiteBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 6);
                chessBoard.Children.Add(image);
            }

            for (int i = 1; i <= 8; i++) // Adding black pawns
            {
                var pawn = blackBoard.P[i - 1];
                var image = pawn.Image;
                Grid.SetColumn(image, i);
                Grid.SetRow(image, 1);
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
    }
}

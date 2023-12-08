using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ChessTeamProject
{
    public class WhitePawn : IPawn
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\whitepan.png", UriKind.RelativeOrAbsolute)) };

    }

    public class WhiteKing : IKing
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\whiteking.png", UriKind.RelativeOrAbsolute)) };
    }

    public class WhiteQueen : IQueen
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformQueenMove();
        }
    }

    public class WhiteBishop : IBishop
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\whitebishop.png", UriKind.RelativeOrAbsolute)) };

        public void Move()
        {
            FigureMove.PerformBishopMove();
        }
    }

    public class WhiteKnight : IKnight
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformKnightMove();
        }
    }

    public class WhiteRook : IRook
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\whiterook.png", UriKind.RelativeOrAbsolute)) };
    }

    public class BlackPawn : IPawn
    {
        public string Side { get; set; } = "Black";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\blackpan.png", UriKind.RelativeOrAbsolute)) };

    }

    public class BlackKing : IKing
    {
        public string Side { get; set; } = "Black";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\blackking.png", UriKind.RelativeOrAbsolute)) };
    }

    public class BlackQueen : IQueen
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformQueenMove();
        }
    }

    public class BlackBishop : IBishop
    {
        public string Side { get; set; } = "Black";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\blackbishop.png", UriKind.RelativeOrAbsolute)) };

        public void Move()
        {
            FigureMove.PerformBishopMove();
        }
    }

    public class BlackKnight : IKnight
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformKnightMove();
        }
    }

    public class BlackRook : IRook
    {
        public string Side { get; set; } = "Black";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\rtt1f\\source\\repos\\ChessTeamProject\\ChessTeamProject\\Figures\\blackrook.png", UriKind.RelativeOrAbsolute)) };
    }
}

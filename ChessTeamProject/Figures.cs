using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ChessTeamProject
{

    public class WhitePawn : IPawn
    {
        public string Side { get; set; } = "White";
        public string Symbol { get; set; } = "";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitepan.png", UriKind.RelativeOrAbsolute)), Name = "WhitePawn" };

    }

    public class WhiteKing : IKing
    {
        public string Side { get; set; } = "White";
        public string Symbol { get; set; } = "K";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whiteking.png", UriKind.RelativeOrAbsolute)), Name = "WhiteKing" };

    }

    public class WhiteQueen : IQueen
    {
        public string Side { get; set; } = "White";
        public string Symbol { get; set; } = "Q";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitequeen.png", UriKind.RelativeOrAbsolute)), Name = "WhiteQueen" };

    }

    public class WhiteBishop : IBishop
    {
        public string Side { get; set; } = "White";
        public string Symbol { get; set; } = "B";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitebishop.png", UriKind.RelativeOrAbsolute)), Name = "WhiteBishop" };


    }

    public class WhiteKnight : IKnight
    {
        public string Side { get; set; } = "White";
        public string Symbol { get; set; } = "Kn";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitehorse.png", UriKind.RelativeOrAbsolute)), Name = "WhiteKnight" };

    }

    public class WhiteRook : IRook
    {
        public string Side { get; set; } = "White";
        public string Symbol { get; set; } = "R";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whiterook.png", UriKind.RelativeOrAbsolute)), Name = "WhiteRook" };

    }

    public class BlackPawn : IPawn
    {
        public string Side { get; set; } = "Black";
        public string Symbol { get; set; } = "";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackpan.png", UriKind.RelativeOrAbsolute)), Name = "BlackPawn" };

    }

    public class BlackKing : IKing
    {
        public string Side { get; set; } = "Black";
        public string Symbol { get; set; } = "K";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackking.png", UriKind.RelativeOrAbsolute)), Name = "BlackKing" };

    }

    public class BlackQueen : IQueen
    {
        public string Side { get; set; } = "Black";
        public string Symbol { get; set; } = "Q";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackqueen.png", UriKind.RelativeOrAbsolute)), Name = "BlackQueen" };

    }

    public class BlackBishop : IBishop
    {
        public string Side { get; set; } = "Black";
        public string Symbol { get; set; } = "B";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackbishop.png", UriKind.RelativeOrAbsolute)), Name = "BlackBishop" };

    }

    public class BlackKnight : IKnight
    {
        public string Side { get; set; } = "Black";
        public string Symbol { get; set; } = "Kn";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackhorse.png", UriKind.RelativeOrAbsolute)), Name = "BlackKnight" };

    }

    public class BlackRook : IRook
    {
        public string Side { get; set; } = "Black";
        public string Symbol { get; set; } = "R";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackrook.png", UriKind.RelativeOrAbsolute)), Name = "BlackRook" };

    }
}

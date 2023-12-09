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

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitepan.png", UriKind.RelativeOrAbsolute)) };

    }

    public class WhiteKing : IKing
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformKingMove();
        }
    }

    public class WhiteQueen : IQueen
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitequeen.png", UriKind.RelativeOrAbsolute)) };

    }

    public class WhiteBishop : IBishop
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitebishop.png", UriKind.RelativeOrAbsolute)) };


    }

    public class WhiteKnight : IKnight
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\whitehorse.png", UriKind.RelativeOrAbsolute)) };
    }

    public class WhiteRook : IRook
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformRookMove();
        }
    }

    public class BlackPawn : IPawn
    {
        public string Side { get; set; } = "Black";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackpan.png", UriKind.RelativeOrAbsolute)) };

    }

    public class BlackKing : IKing
    {
        public string Side { get; set; } = "Black";

        public void Move()
        {
            FigureMove.PerformKingMove();
        }
    }

    public class BlackQueen : IQueen
    {
        public string Side { get; set; } = "Black";

        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackqueen.png", UriKind.RelativeOrAbsolute)) };
    }

    public class BlackBishop : IBishop
    {
        public string Side { get; set; } = "Black";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackbishop.png", UriKind.RelativeOrAbsolute)) };

    }

    public class BlackKnight : IKnight
    {
        public string Side { get; set; } = "Black";
        public Image Image => new Image { Source = new BitmapImage(new Uri("C:\\Users\\Desik\\source\\repos\\WpfApp17\\WpfApp17\\Paint\\blackhorse.png", UriKind.RelativeOrAbsolute)) };
    }

    public class BlackRook : IRook
    {
        public string Side { get; set; } = "Black";

        public void Move()
        {
            FigureMove.PerformRookMove();
        }
    }
}

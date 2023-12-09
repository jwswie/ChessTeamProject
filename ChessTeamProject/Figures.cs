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

        public Image Image => new Image { Source = new BitmapImage(new Uri("whitepan.png", UriKind.RelativeOrAbsolute)) };
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

        public void Move()
        {
            FigureMove.PerformQueenMove();
        }
    }

    public class WhiteBishop : IBishop
    {
        public string Side { get; set; } = "White";

        public Image Image => new Image { Source = new BitmapImage(new Uri("whitebishop.png", UriKind.RelativeOrAbsolute)) };
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

        public void Move()
        {
            FigureMove.PerformRookMove();
        }
    }

    public class BlackPawn : IPawn
    {
        public string Side { get; set; } = "Black";

        public Image Image => new Image { Source = new BitmapImage(new Uri("blackpan.png", UriKind.RelativeOrAbsolute)) };
    }

    public class BlackKing : IKing
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformKingMove();
        }
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

        public Image Image => new Image { Source = new BitmapImage(new Uri("blackbishop.png", UriKind.RelativeOrAbsolute)) };
        
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
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformRookMove();
        }
    }
}

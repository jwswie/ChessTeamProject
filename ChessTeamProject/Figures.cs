using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTeamProject
{
    public class WhitePawn : IPawn
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformPawnMove();
        }
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

        public void Move()
        {
            FigureMove.PerformRookMove();
        }
    }

    public class BlackPawn : IPawn
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformPawnMove();
        }
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
        public string Side { get; set; } = "White";

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
        public string Side { get; set; } = "White";

        public void Move()
        {
            FigureMove.PerformRookMove();
        }
    }
}

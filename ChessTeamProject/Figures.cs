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
            Console.WriteLine("Performing pawn's move...");
        }
    }

    public class WhiteKing : IKing
    {
        public string Side { get; set; } = "White";

        public void Move()
        {
            Console.WriteLine("Performing king's move...");
        }
    }

    // Same for other classes (WhiteQueen, WhiteBishop, WhiteKnight, WhiteRook, BlackPawn, BlackKing etc.)
}

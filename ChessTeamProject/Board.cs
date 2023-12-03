using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTeamProject
{
    public class Board
    {
        public List<IPawn> P { get; } = new List<IPawn>();
        public List<IKing> K { get; } = new List<IKing>();
        // Same for other interfaces

        public void Move()
        {
            foreach (var pawn in P) pawn.Move();
            foreach (var king in K) king.Move();
            // Same for other classes
        }
    }
}

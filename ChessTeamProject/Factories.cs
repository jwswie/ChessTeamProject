using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTeamProject
{
    public interface IChessFactory
    {
        IPawn CreatePawn();
        IKing CreateKing();
        // Same for other interfaces
    }

    public class WhiteSideFactory : IChessFactory
    {
        public IPawn CreatePawn() => new WhitePawn();
        public IKing CreateKing() => new WhiteKing();
        // Same for other classes
    }

    // Same for BlackSideFactory
}

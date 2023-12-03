using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTeamProject
{
    public class Game
    {
        public Board CreateBoard(IChessFactory factory)
        {
            var board = new Board();
            board.P.Add(factory.CreatePawn());
            board.K.Add(factory.CreateKing());
            // Same for other classes
            return board;
        }
    }
}

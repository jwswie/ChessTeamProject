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

            for (int i = 1; i <= 8; i++)
            {
                board.P.Add(factory.CreatePawn());
            }
            board.K.Add(factory.CreateKing());
            for (int i = 1; i <= 2; i++)
            {
                board.Q.Add(factory.CreateQueen());
            }
            //board.B.Add(factory.CreateBishop());
            for (int i = 1; i <= 4; i++)
            {
                board.B.Add(factory.CreateBishop());
            }
            for (int i = 1; i <= 4; i++)
            {
                board.Kn.Add(factory.CreateKnight());
            }
            board.R.Add(factory.CreateRook());

            return board;
        }
    }
}

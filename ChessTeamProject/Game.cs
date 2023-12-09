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
            board.Q.Add(factory.CreateQueen());

            for (int i = 1; i <= 4; i++)
            {
                board.B.Add(factory.CreateBishop());
            }
            board.Kn.Add(factory.CreateKnight());
            board.R.Add(factory.CreateRook());

            return board;
        }
    }
}

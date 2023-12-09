namespace ChessTeamProject
{
    public interface IChessFactory // Defines methods for creating each type of figure
    {
        IPawn CreatePawn();
        IKing CreateKing();
        IQueen CreateQueen();
        IBishop CreateBishop();
        IKnight CreateKnight();
        IRook CreateRook();
    }

    public class WhiteSideFactory : IChessFactory // Specific realisations of methods for creating figures of the each side (white or black)
    {
        public IPawn CreatePawn() => new WhitePawn();
        public IKing CreateKing() => new WhiteKing();
        public IQueen CreateQueen() => new WhiteQueen();
        public IBishop CreateBishop() => new WhiteBishop();
        public IKnight CreateKnight() => new WhiteKnight();
        public IRook CreateRook() => new WhiteRook();
    }

    public class BlackSideFactory : IChessFactory
    {
        public IPawn CreatePawn() => new BlackPawn();
        public IKing CreateKing() => new BlackKing();
        public IQueen CreateQueen() => new BlackQueen();
        public IBishop CreateBishop() => new BlackBishop();
        public IKnight CreateKnight() => new BlackKnight();
        public IRook CreateRook() => new BlackRook();
    }
}

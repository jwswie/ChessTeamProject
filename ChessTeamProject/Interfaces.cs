using System.Windows.Controls;

namespace ChessTeamProject
{
    public interface IPawn
    {
        string Side { get; set; }
        Image Image { get; }
    }

    public interface IKing
    {
        string Side { get; set; }
        void Move();
    }

    public interface IQueen
    {
        string Side { get; set; }
        void Move();
    }

    public interface IBishop
    {
        string Side { get; set; }
        Image Image { get; }
    }

    public interface IKnight
    {
        string Side { get; set; }
        void Move();
    }

    public interface IRook
    {
        string Side { get; set; }
        void Move();
    }
}
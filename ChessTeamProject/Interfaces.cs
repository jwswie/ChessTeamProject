using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTeamProject
{
    public interface IPawn
    {
        string Side { get; set; }
        void Move();
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
        void Move();
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

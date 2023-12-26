﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChessTeamProject
{
    public interface IPawn
    {
        string Side { get; set; }
        string Symbol { get; set; }
        Image Image { get; }
    }

    public interface IKing
    {
        string Side { get; set; }
        string Symbol { get; set; }
        Image Image { get; }
    }

    public interface IQueen
    {
        string Side { get; set; }
        string Symbol { get; set; }
        Image Image { get; }
    }

    public interface IBishop
    {
        string Side { get; set; }
        string Symbol { get; set; }
        Image Image { get; }
    }

    public interface IKnight
    {
        string Side { get; set; }
        string Symbol { get; set; }
        Image Image { get; }
    }

    public interface IRook
    {
        string Side { get; set; }
        string Symbol { get; set; }
        Image Image { get; }
    }
}
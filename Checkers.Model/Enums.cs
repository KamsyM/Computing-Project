using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    //public enum PieceTypes
    //{
    //    Black, White, BlackKing, WhiteKing
    //}

    public enum SquareValues
    {
        Empty/*Taken*/, Black, White, BlackKing, WhiteKing
    }

    public enum Modality
    {
        BlackTurn, WhiteTurn
    }

    public enum CheckNo
    {
        RightUp, LeftUp, RightDown, LeftDown
    }
}

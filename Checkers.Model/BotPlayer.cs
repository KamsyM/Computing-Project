using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public abstract class BotPlayer
    {
        public SquareValues Type;
        public GameBoard Board;
        public int Difficuty;

        public BotPlayer(GameBoard board, SquareValues type)
        {
            Type = type;
            Board = board;
           // Difficuty = difficulty;
        }

        public abstract void Move();

    }
}

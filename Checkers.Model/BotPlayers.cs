using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class BotPlayers
    {
        public SquareValues Type;
        public GameBoard Board;

        public BotPlayers(GameBoard board, SquareValues type)
        {
            Type = type;
            Board = board;

        }
    }
}

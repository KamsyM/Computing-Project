using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class History
    {
        public GameBoard Board;
        public List<Move> Plays = new List<Move>();

        public History(GameBoard board)
        {
            Board = board;
        }

        public void Add(Move play)
        {
            Plays.Add(play);
        }

        public void Clear()
        {
            Plays.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Plays)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString();
        }
    }
}

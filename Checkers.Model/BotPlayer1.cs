using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class BotPlayer1 : BotPlayer
    {
        public BotPlayer1(GameBoard board, SquareValues type) : base(board, type)
        {
            Board = board;
            Type = type;
            
        }

        public override void Move()
        {
            for (int oldrow = 0; oldrow < Board.Size; oldrow++)
            {
                for (int oldcol = 0; oldcol < Board.Size; oldcol++)
                {
                    if (!Board.IsEmptySquare(oldcol, oldrow) && !Board.NotYourPiece(Type, oldcol, oldrow))
                    {
                        var realtype = Board.Squares[oldcol, oldrow];
                        for (int newrow = 0; newrow < Board.Size; newrow++)
                        {
                            for (int newcol = 0; newcol < Board.Size; newcol++)
                            {
                                if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, oldcol, oldrow, newcol, newrow))
                                {
                                    Board.MovePiece(realtype, oldcol, oldrow, newcol, newrow);
                                    return;
                                }

                            }

                        }

                    }

                }

            }
        }
    }
}

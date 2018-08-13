using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class BotPlayer2 : BotPlayer
    {
        public BotPlayer2(GameBoard board, SquareValues type) : base(board, type)
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
                        switch (Board.CanJump(realtype, oldcol, oldrow))
                        {
                            case 0:
                                continue;
                            case 1:
                                Board.MovePiece(realtype, oldcol, oldrow, oldcol + 2, oldrow - 2);
                                return;
                            case 2:
                                Board.MovePiece(realtype, oldcol, oldrow, oldcol - 2, oldrow - 2);
                                return;
                            case 3:
                                Board.MovePiece(realtype, oldcol, oldrow, oldcol + 2, oldrow + 2);
                                return;
                            case 4:
                                Board.MovePiece(realtype, oldcol, oldrow, oldcol - 2, oldrow + 2);
                                return;
                            default:
                                return;
                        }


                    }

                }

            }
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

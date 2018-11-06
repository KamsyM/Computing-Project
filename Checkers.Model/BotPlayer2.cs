using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    [Description("Jumps over a Piece Whenever it can")]
    public class BotPlayer2 : BotPlayer
    {
        public BotPlayer2(GameBoard board, SquareValues type) : base(board, type)
        {
            Board = board;
            Type = type;
            BotName = "Level 2";
        }

        public override string ToString()
        {
            return BotName;
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
                                    try
                                    {
                                        pos.Add(oldcol, oldrow);
                                        pos.Add(newcol, newrow);
                                        OldPosition = pos.First();
                                        NewPosition = pos.Last();
                                        pos.Clear();
                                        BotPositions.Add(OldPosition, NewPosition);
                                    }
                                    catch (Exception)
                                    {
                                        List<KeyValuePair<int, int>> Options = new List<KeyValuePair<int, int>>();
                                        Options.Add(BotPositions[OldPosition]);
                                        Options.Add(NewPosition);
                                        BotPositions.Remove(OldPosition);
                                        BotPositions.Add(OldPosition, Options[rnd.Next(2)]);

                                    }
                                    //Board.MovePiece(realtype, oldcol, oldrow, newcol, newrow);
                                    //return;
                                }

                            }

                        }

                    }

                }

            }
            var a = RandomValues(BotPositions);
            var oldCol = a.Key;
            var oldRow = a.Value;
            var NewPlaces = BotPositions[a];
            var newCol = NewPlaces.Key;
            var newRow = NewPlaces.Value;
            Board.MovePiece(Board.Squares[oldCol, oldRow], oldCol, oldRow, newCol, newRow);
            BotPositions.Clear();
            return;
        }
    }
}

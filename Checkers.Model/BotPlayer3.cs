using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class BotPlayer3 : BotPlayer
    {
        public BotPlayer3(GameBoard board, SquareValues type) : base(board, type)
        {
            Board = board;
            Type = type;
            BotName = "Level 3";
        }

        public override string ToString()
        {
            return BotName;
        }

        public override void Move()
        {
            var a = new KeyValuePair<int, int>();
            var oldCol = 0;
            var oldRow = 0;
            var NewPlaces = new KeyValuePair<int, int>();
            var newCol = 0;
            var newRow = 0;
            //int jumpNo = 0;
            for (int oldrow = 0; oldrow < Board.Size; oldrow++)
            {
                for (int oldcol = 0; oldcol < Board.Size; oldcol++)
                {
                    if (!Board.IsEmptySquare(oldcol, oldrow) && !Board.NotYourPiece(Type, oldcol, oldrow))
                    {
                        Jumper(oldcol, oldrow);
                        if (Board.IsEmptySquare(oldcol, oldrow))
                        {
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
            a = RandomValues(BotPositions);
            oldCol = a.Key;
            oldRow = a.Value;
            NewPlaces = BotPositions[a];
            newCol = NewPlaces.Key;
            newRow = NewPlaces.Value;
            Board.MovePiece(Board.Squares[oldCol, oldRow], oldCol, oldRow, newCol, newRow);
            BotPositions.Clear();
            return;
        }
    }
}

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
        public string BotName;
        public SquareValues a;
        public SquareValues b;
        public Random rnd = new Random();
        public KeyValuePair<int, int> OldPosition = new KeyValuePair<int, int>();
        public KeyValuePair<int, int> NewPosition = new KeyValuePair<int, int>();
        public Dictionary<int, int> pos = new Dictionary<int, int>();
        public Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>> BotPositions = new Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>>();

        public BotPlayer(GameBoard board, SquareValues type)
        {
            Type = type;
            Board = board;
        }


        public abstract void Move();

        public static SquareValues PlayBots(GameBoard Board, BotPlayer Bot, BotPlayer Bot2)
        {
            int turn = 1;
            Board.InitialiseEmptyBoard();
            Board.InitializePieces();
            bool cont = true;
            while (!Board.GameIsWon() && cont == true)
            {
                if (turn == 1)
                {
                    Bot.Move();
                    if (Board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return Bot.Type;
                    }
                    if (!Board.CanMove(Board.OpponentType(Bot.Type)) && !Board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return Bot.Type;
                    }
                    turn = 2;
                }
                if (turn == 2)
                {
                    Bot2.Move();
                    if (Board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return Bot2.Type;
                    }
                    if (!Board.CanMove(Board.OpponentType(Bot2.Type)) && !Board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return Bot2.Type;
                    }
                    turn = 1;

                }
            }
            turn = 1;
            return SquareValues.Empty;
        }

        /// <summary>
        /// Checks if a jump is safe
        /// </summary>
        /// <param name="oldcol"></param>
        /// <param name="oldrow"></param>
        /// <param name="type"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public bool CheckingJumps(int oldcol, int oldrow, SquareValues type, CheckNo mode)
        {
            switch (mode)
            {
                case CheckNo.RightUp:
                    try
                    {
                        a = Board.Squares[oldcol + 2, oldrow - 2];
                        b = Board.Squares[oldcol + 1, oldrow - 1];
                        Board.Squares[oldcol + 2, oldrow - 2] = type;
                        Board.Squares[oldcol + 1, oldrow - 1] = SquareValues.Empty;
                        if (!Board.CanBeJumped(oldcol + 2, oldrow - 2) || Board.CanJump(type, oldcol + 2, oldrow - 2) != 0)
                        {
                            Board.Squares[oldcol + 2, oldrow - 2] = a;
                            Board.Squares[oldcol + 1, oldrow - 1] = b;
                            return true;
                        }
                        else
                        {
                            Board.Squares[oldcol + 2, oldrow - 2] = a;
                            Board.Squares[oldcol + 1, oldrow - 1] = b;
                            return false;
                        }


                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol + 2, oldrow - 2] = a;
                            Board.Squares[oldcol + 1, oldrow - 1] = b;
                            return false;
                        }
                        catch (Exception)
                        {

                            return false;
                        }
                        
                    }
                case CheckNo.LeftUp:
                    try
                    {
                        a = Board.Squares[oldcol - 2, oldrow - 2];
                        b = Board.Squares[oldcol - 1, oldrow - 1];
                        Board.Squares[oldcol - 2, oldrow - 2] = type;
                        Board.Squares[oldcol - 1, oldrow - 1] = SquareValues.Empty;
                        if (!Board.CanBeJumped(oldcol - 2, oldrow - 2) || Board.CanJump(type, oldcol - 2, oldrow - 2) != 0)
                        {
                            Board.Squares[oldcol - 2, oldrow - 2] = a;
                            Board.Squares[oldcol - 1, oldrow - 1] = b;
                            return true;
                        }
                        else
                        {
                            Board.Squares[oldcol - 2, oldrow - 2] = a;
                            Board.Squares[oldcol - 1, oldrow - 1] = b;
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol - 2, oldrow - 2] = a;
                            Board.Squares[oldcol - 1, oldrow - 1] = b;
                            return false;
                        }
                        catch (Exception)
                        {

                            return false;
                        }
                       
                    }
                  
                case CheckNo.RightDown:
                    try
                    {
                        a = Board.Squares[oldcol + 2, oldrow + 2];
                        b = Board.Squares[oldcol + 1, oldrow + 1];
                        Board.Squares[oldcol + 2, oldrow + 2] = type;
                        Board.Squares[oldcol + 1, oldrow + 1] = SquareValues.Empty;
                        if (!Board.CanBeJumped(oldcol + 2, oldrow + 2) || Board.CanJump(type, oldcol + 2, oldrow + 2) != 0)
                        {
                            Board.Squares[oldcol + 2, oldrow + 2] = a;
                            Board.Squares[oldcol + 1, oldrow + 1] = b;
                            return true;
                        }
                        else
                        {
                            Board.Squares[oldcol + 2, oldrow + 2] = a;
                            Board.Squares[oldcol + 1, oldrow + 1] = b;
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol + 2, oldrow + 2] = a;
                            Board.Squares[oldcol + 1, oldrow + 1] = b;
                            return false;
                        }
                        catch (Exception)
                        {

                            return false;
                        }
                    }

                case CheckNo.LeftDown:
                    try
                    {
                        a = Board.Squares[oldcol - 2, oldrow + 2];
                        b = Board.Squares[oldcol - 1, oldrow + 1];
                        Board.Squares[oldcol - 2, oldrow + 2] = type;
                        Board.Squares[oldcol - 1, oldrow + 1] = SquareValues.Empty;
                        if (!Board.CanBeJumped(oldcol - 2, oldrow + 2) || Board.CanJump(type, oldcol - 2, oldrow + 2) != 0)
                        {
                            Board.Squares[oldcol - 2, oldrow + 2] = a;
                            Board.Squares[oldcol - 1, oldrow + 1] = b;
                            return true; ;
                        }
                        else
                        {
                            Board.Squares[oldcol - 2, oldrow + 2] = a;
                            Board.Squares[oldcol - 1, oldrow + 1] = b;
                            return false;
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol - 2, oldrow + 2] = a;
                            Board.Squares[oldcol - 1, oldrow + 1] = b;
                            return false;
                        }
                        catch (Exception)
                        {

                            return false;
                        }
                    }
                default:
                    return false;
            }

        }

        /// <summary>
        /// Checks if a move is safe
        /// </summary>
        public bool CheckingSpaces(int oldcol, int oldrow, int newcol, int newrow)
        {
            try
            {
                var type = Board.ReadSquare(oldcol, oldrow);
                a = Board.Squares[oldcol, oldrow];
                b = Board.Squares[newcol, newrow];
                Board.Squares[oldcol, oldrow] = SquareValues.Empty;
                Board.Squares[newcol, newrow] = type;
                if (!Board.CanBeJumped(newcol, newrow))
                {
                    Board.Squares[oldcol, oldrow] = a;
                    Board.Squares[newcol, newrow] = b;
                    return true;
                }
                else
                {
                    Board.Squares[oldcol, oldrow] = a;
                    Board.Squares[newcol, newrow] = b;
                    return false;
                }
            }
            catch (Exception)
            {
                Board.Squares[oldcol, oldrow] = a;
                Board.Squares[newcol, newrow] = b;
                return false;
            }

        }

        /// <summary>
        /// Checks to see if a piece can become King on the next move
        /// </summary>
        /// <param name="newrow"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CheckKing( int newrow, SquareValues type)
        {
            switch (type)
            {
                case SquareValues.Black:
                    if (newrow == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case SquareValues.White:
                    if (newrow == 7)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        /// <summary>
        /// Makes the Bot Jump over other pieces on the Board
        /// </summary>
        /// <param name="oldcol"></param>
        /// <param name="oldrow"></param>
        public void Jumper(int oldcol, int oldrow)
        {
            var realtype = Board.Squares[oldcol, oldrow];
            switch (Board.CanJump(realtype, oldcol, oldrow))
            {
                case 0:
                    return;
                case 1:
                    Board.MovePiece(realtype, oldcol, oldrow, oldcol + 2, oldrow - 2);

                    if (Board.CanJump(realtype, oldcol + 2, oldrow - 2) != 0)
                    {
                        //Thread.Sleep(1000);
                        Jumper(oldcol + 2, oldrow - 2);
                    }
                    return;
                case 2:
                    Board.MovePiece(realtype, oldcol, oldrow, oldcol - 2, oldrow - 2);
                    if (Board.CanJump(realtype, oldcol - 2, oldrow - 2) != 0)
                    {
                        //Thread.Sleep(1000);
                        Jumper(oldcol - 2, oldrow - 2);
                    }
                    return;
                case 3:
                    Board.MovePiece(realtype, oldcol, oldrow, oldcol + 2, oldrow + 2);
                    if (Board.CanJump(realtype, oldcol + 2, oldrow + 2) != 0)
                    {
                        //Thread.Sleep(1000);
                        Jumper(oldcol + 2, oldrow + 2);
                    }
                    return;
                case 4:
                    Board.MovePiece(realtype, oldcol, oldrow, oldcol - 2, oldrow + 2);
                    if (Board.CanJump(realtype, oldcol - 2, oldrow + 2) != 0)
                    {
                        //Thread.Sleep(1000);
                        Jumper(oldcol - 2, oldrow + 2);
                    }
                    return;
                default:
                    return;
            }
        }

        public void ForcedJumper(int oldcol, int oldrow,int newcol, int newrow)
        {
            var realtype = Board.Squares[oldcol, oldrow];
            Board.MovePiece(realtype, oldcol, oldrow, newcol, newrow);

            if (Board.CanJump(realtype, newcol, newrow) != 0)
            {
                //Thread.Sleep(1000);
                Jumper(newcol, newrow);
            }
        }

        public KeyValuePair<int, int> RandomValues(Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>> dict)
        {
            List<KeyValuePair<int, int>> values = new List<KeyValuePair<int, int>>();
            foreach (var item in dict)
            {
                values.Add(item.Key);
            }
            int size = values.Count;
            return values[rnd.Next(size)];
        }
    }
}

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
           // Difficuty = difficulty;
        }

        public abstract void Move();

        public static int PlayBots(GameBoard Board, BotPlayer Bot, BotPlayer Bot2)
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
                        return 1;
                    }
                    if (!Board.CanMove(SquareValues.White) && !Board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return 1;
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
                        return 2;
                    }
                    if (!Board.CanMove(SquareValues.Black) && !Board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return 2;
                    }
                    turn = 1;

                }
            }
            turn = 1;
            return 0;
        }

        /// <summary>
        /// Checks if a jump is safe
        /// </summary>
        /// <param name="oldcol"></param>
        /// <param name="oldrow"></param>
        /// <param name="mode"></param>
        public void CheckingJumps(int oldcol, int oldrow, SquareValues type, CheckNo mode)
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
                            Jumper(oldcol, oldrow);
                        }
                        else
                        {
                            Board.Squares[oldcol + 2, oldrow - 2] = a;
                            Board.Squares[oldcol + 1, oldrow - 1] = b;
                        }


                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol + 2, oldrow - 2] = a;
                            Board.Squares[oldcol + 1, oldrow - 1] = b;
                        }
                        catch (Exception)
                        {

                            return;
                        }
                        return;
                    }

                    break;
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
                            Jumper(oldcol, oldrow);
                        }
                        else
                        {
                            Board.Squares[oldcol - 2, oldrow - 2] = a;
                            Board.Squares[oldcol - 1, oldrow - 1] = b;
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol - 2, oldrow - 2] = a;
                            Board.Squares[oldcol - 1, oldrow - 1] = b;
                        }
                        catch (Exception)
                        {

                            return;
                        }
                        return;
                    }

                    break;
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
                            Jumper(oldcol, oldrow);
                        }
                        else
                        {
                            Board.Squares[oldcol + 2, oldrow + 2] = a;
                            Board.Squares[oldcol + 1, oldrow + 1] = b;
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol + 2, oldrow + 2] = a;
                            Board.Squares[oldcol + 1, oldrow + 1] = b;
                        }
                        catch (Exception)
                        {

                            return;
                        }
                        return;
                    }

                    break;
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
                            Jumper(oldcol, oldrow);
                        }
                        else
                        {
                            Board.Squares[oldcol - 2, oldrow + 2] = a;
                            Board.Squares[oldcol - 1, oldrow + 1] = b;
                        }
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Board.Squares[oldcol - 2, oldrow + 2] = a;
                            Board.Squares[oldcol - 1, oldrow + 1] = b;
                        }
                        catch (Exception)
                        {

                            return;
                        }
                        return;
                    }
                    break;
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

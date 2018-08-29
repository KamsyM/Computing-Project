using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Checkers.Model
{
    public class BotPlayers
    {
        public SquareValues Type;
        public GameBoard Board;
        public int Difficuty;
        public SquareValues a;
        public SquareValues b;

        public BotPlayers(GameBoard board, SquareValues type, int difficulty)
        {
            Type = type;
            Board = board;
            Difficuty = difficulty;
            
        }

        public void Move()
        {
            switch (Difficuty)
            {
                case 1:
                    BotPlayer1();
                    break;
                case 2:
                    BotPlayer3();
                    break;
                case 3:
                    BotPlayer4();
                    break;
            }
        }

        private void BotPlayer1()
        {
            for (int oldrow = 0; oldrow < Board.Size; oldrow++)
            {
                for (int oldcol = 0; oldcol < Board.Size; oldcol++)
                {
                    if (!Board.IsEmptySquare(oldcol,oldrow) && !Board.NotYourPiece(Type, oldcol, oldrow))
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

        private void BotPlayer2()
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
                                Board.MovePiece(realtype, oldcol, oldrow, oldcol + 2, oldrow -2);
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

        private void BotPlayer3()
        {
            //int jumpNo = 0;
            for (int oldrow = 0; oldrow < Board.Size; oldrow++)
            {
                for (int oldcol = 0; oldcol < Board.Size; oldcol++)
                {
                    if (!Board.IsEmptySquare(oldcol, oldrow) && !Board.NotYourPiece(Type, oldcol, oldrow))
                    {
                        Jumper(oldcol,oldrow);
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
                                    Board.MovePiece(realtype, oldcol, oldrow, newcol, newrow);
                                    return;
                                }

                            }

                        }

                    }

                }

            }
        }

        private void BotPlayer4()
        {
            //int jumpNo = 0;

            for (int oldrow = 0; oldrow < Board.Size; oldrow++)
            {
                for (int oldcol = 0; oldcol < Board.Size; oldcol++)
                {
                    if (!Board.IsEmptySquare(oldcol, oldrow) && !Board.NotYourPiece(Type, oldcol, oldrow))
                    {
                        var realtype = Board.Squares[oldcol, oldrow];
                        switch (Board.CanJump(realtype,oldcol,oldrow))
                        {
                            case 0:
                                break;
                            case 1:
                                Checking(oldcol, oldrow,realtype, CheckNo.RightUp);
                                break;
                            case 2:
                                Checking(oldcol, oldrow,realtype, CheckNo.LeftUp);
                                break;
                            case 3:
                                Checking(oldcol, oldrow,realtype, CheckNo.RightDown);
                                break;
                            case 4:
                                Checking(oldcol, oldrow,realtype, CheckNo.LeftDown);
                                break;
                            default:
                                break;
                        }
                        //Jumper(oldcol, oldrow);
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
                                if (newrow == oldrow - 1 || newrow == oldrow +1)
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

        /// <summary>
        /// Checks if a jump is safe
        /// </summary>
        /// <param name="oldcol"></param>
        /// <param name="oldrow"></param>
        /// <param name="mode"></param>
        private void Checking(int oldcol, int oldrow, SquareValues type, CheckNo mode)
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
                        if (!Board.CanBeJumped(oldcol+2, oldrow -2) || Board.CanJump(type, oldcol + 2, oldrow - 2) != 0)
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
        /// Makes the Bot Jump over other pieces on the Board
        /// </summary>
        /// <param name="oldcol"></param>
        /// <param name="oldrow"></param>
        private void Jumper(int oldcol, int oldrow)
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
                        Jumper(oldcol+2, oldrow-2);
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
    }
}
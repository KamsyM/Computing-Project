using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Checkers.Model
{
    public class BotPlayerTempV
    {
        public SquareValues Type;
        public GameBoard Board;
        public int Difficuty;
        public SquareValues a;
        public SquareValues b;
        public Random rnd = new Random();
        private KeyValuePair<int, int> OldPosition = new KeyValuePair<int, int>();
        private KeyValuePair<int, int> NewPosition = new KeyValuePair<int, int>();
        private Dictionary<int, int> pos = new Dictionary<int, int>();
        private Dictionary<KeyValuePair<int,int>, KeyValuePair<int, int>> BotPositions = new Dictionary<KeyValuePair<int, int>, KeyValuePair<int, int>>();
        public BotPlayerTempV(GameBoard board, SquareValues type, int difficulty)
        {
            Type = type;
            Board = board;
            Difficuty = difficulty;
            
        }

        /// <summary>
        /// Simulates a game with 2 given Bots and returns the winner in terms of the player number
        /// </summary>
        /// <param name="board"></param>
        /// <param name="Bot"></param>
        /// <param name="Bot2"></param>
        /// <returns></returns>
        public static int PlayBots(GameBoard board, BotPlayerTempV Bot, BotPlayerTempV Bot2)
        {
            int turn = 1;
            board.InitialiseEmptyBoard();
            board.InitializePieces();
            bool cont = true;
            while (!board.GameIsWon() && cont == true)
            {
                if (turn == 1)
                {
                    Bot.Move();
                    if (board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return 1;
                    }
                    if (!board.CanMove(SquareValues.White) && !board.GameIsWon())
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
                    if (board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return 2;
                    }
                    if (!board.CanMove(SquareValues.Black) && !board.GameIsWon())
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
                    BotPlayer5();
                    break;
            }
        }



        /// <summary>
        /// Picks a Random Place to move to
        /// </summary>
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

        /// <summary>
        /// Jumps over a Piece Whenever it can
        /// </summary>
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

        /// <summary>
        /// Jumps over a Piece Whenever it can and can also double jump
        /// </summary>
        private void BotPlayer3()
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

        /// <summary>
        /// Checks to see if a space is safe before jumping
        /// </summary>
        private void BotPlayer4()
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
                        var realtype = Board.Squares[oldcol, oldrow];
                        switch (Board.CanJump(realtype,oldcol,oldrow))
                        {
                            case 0:
                                break;
                            case 1:
                                CheckingJumps(oldcol, oldrow,realtype, CheckNo.RightUp);
                                break;
                            case 2:
                                CheckingJumps(oldcol, oldrow,realtype, CheckNo.LeftUp);
                                break;
                            case 3:
                                CheckingJumps(oldcol, oldrow,realtype, CheckNo.RightDown);
                                break;
                            case 4:
                                CheckingJumps(oldcol, oldrow,realtype, CheckNo.LeftDown);
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

            }
            if (BotPositions.Count != 0)
            {
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

        /// <summary>
        /// Checks to see if a move as well as a jump is safe before moving
        /// </summary>
        private void BotPlayer5()
        {
            var a = new KeyValuePair<int,int>();
            var oldCol = 0;
            var oldRow = 0;
            var NewPlaces = new KeyValuePair<int, int>();
            var newCol = 0;
            var newRow = 0;
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
                                break;
                            case 1:
                                CheckingJumps(oldcol, oldrow, realtype, CheckNo.RightUp);
                                break;
                            case 2:
                                CheckingJumps(oldcol, oldrow, realtype, CheckNo.LeftUp);
                                break;
                            case 3:
                                CheckingJumps(oldcol, oldrow, realtype, CheckNo.RightDown);
                                break;
                            case 4:
                                CheckingJumps(oldcol, oldrow, realtype, CheckNo.LeftDown);
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
                                if (newrow == oldrow - 1 || newrow == oldrow + 1)
                                {
                                    
                                    if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, oldcol, oldrow, newcol, newrow))
                                    {
                                        if (CheckingSpaces(oldcol, oldrow, newcol, newrow))
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

                }

            }
            if (BotPositions.Count != 0)
            {
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

        /// <summary>
        /// Checks if a jump is safe
        /// </summary>
        /// <param name="oldcol"></param>
        /// <param name="oldrow"></param>
        /// <param name="mode"></param>
        private void CheckingJumps(int oldcol, int oldrow, SquareValues type, CheckNo mode)
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
        /// Checks if a move is safe
        /// </summary>
        private bool CheckingSpaces(int oldcol, int oldrow, int newcol, int newrow)
        {
            try
            {
                var type = Board.ReadSquare(oldcol,oldrow);
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
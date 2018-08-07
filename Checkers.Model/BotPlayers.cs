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
        public int Difficuty;

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
                    BotPlayer2();
                    break;
                case 3:
                    BotPlayer3();
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
            throw new NotImplementedException();
        }

        private void BotPlayer3()
        {
            throw new NotImplementedException();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Checkers.Model
{
    public class GameBoard
    {
        public int Size { get; private set; }
        public SquareValues[,] Squares;
        private Piece[] Pieces1;

        //public void NewPiece(int column, int row, SquareValues type)
        //{
        //    Squares[column, row] = type;
        //}

        private Piece[] Pieces2;
        //private ILogger Logger;

        public GameBoard(int size, Piece[] pieces1, Piece[] pieces2)
        {
            Size = size;
            Squares = new SquareValues[Size, Size];
            InitialiseEmptyBoard();
            
            Pieces1 = pieces1;
            Pieces2 = pieces2;
        }

        public void PrintBoard()
        {
            int boardSize = Size;
            Console.WriteLine();
            Console.WriteLine("The board looks like this: ");
            Console.WriteLine();
            Console.Write(" ");
            for (int Column = 0; Column < boardSize; Column++)
            {
                Console.Write(" " + Column + "  ");
            }
            Console.WriteLine();
            for (int row = 0; row < boardSize; row++)
            {
                Console.Write(row + " ");
                for (int col = 0; col < boardSize; col++)
                {
                    SquareValues square = ReadSquare(col, row);
                    switch (square)
                    {
                        case SquareValues.Empty:
                            Console.Write(' ');
                            break;
                        case SquareValues.Black:
                            Console.Write('*');
                            break;
                        case SquareValues.BlackKing:
                            Console.Write('@');
                            break;
                        case SquareValues.White:
                            Console.Write('%');
                            break;
                        case SquareValues.WhiteKing:
                            Console.Write('/');
                            break;

                    }


                    if (col != boardSize - 1)
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //Sets all squares to empty
        private void InitialiseEmptyBoard()
        {
            for (int Row = 0; Row < Size; Row++)
            {
                for (int Column = 0; Column < Size; Column++)
                {
                    Squares[Column, Row] = SquareValues.Empty;
                }
            }
        }

        public SquareValues ReadSquare(int col, int row)
        {
            return Squares[col, row];
        }

        public void InitializePieces()
        {
            foreach (var piece in Pieces1)
            {
                Squares[piece.startCol, piece.startRow] = piece.PieceType;
            }

            foreach (var piece in Pieces2)
            {
                Squares[piece.startCol, piece.startRow] = piece.PieceType;
            }
        }

        public bool IsEmptySquare( int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.Empty;
        }

        public bool IsBlackSquare( int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.Black;
        }

        public bool IsWhiteSquare( int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.White;
        }

        public bool IsBlackKingSquare( int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.BlackKing;
        }

        public bool IsWhiteKingSquare( int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.WhiteKing;
        }

        public void MovePiece(SquareValues type, int oldCol, int oldRow, int newCol, int newRow)
        {
            Squares[oldCol, oldRow] = SquareValues.Empty;
            Squares[newCol, newRow] = type;
            if (type == SquareValues.Black && newRow == 0)
            {
                Squares[newCol, newRow] = SquareValues.BlackKing;
            }

            if (type == SquareValues.White && newRow == 7)
            {
                Squares[newCol, newRow] = SquareValues.WhiteKing;
            }

            if (newCol == oldCol + 2 && newRow == oldRow - 2)
            {
                Squares[oldCol + 1, oldRow - 1] = SquareValues.Empty;
            }

            if (newCol == oldCol +2 && newRow == oldRow +2)
            {
                Squares[oldCol + 1, oldRow + 1] = SquareValues.Empty;
            }

            if (newCol == oldCol - 2 && newRow == oldRow + 2)
            {
                Squares[oldCol - 1, oldRow + 1] = SquareValues.Empty;
            }

            if (newCol == oldCol - 2 && newRow == oldRow - 2)
            {
                Squares[oldCol - 1, oldRow - 1] = SquareValues.Empty;
            }

        }

        public bool IsValidMove( SquareValues type, int oldCol, int oldRow, int newCol, int newRow)
        {
            if (IsEmptySquare( newCol, newRow) == false)
            {
                return false;
            }

            if (newCol == oldCol || newRow == oldRow)
            {
                return false;
            }

            switch (type)
            {
                case SquareValues.Black:
                    return IsValidBlackMove(oldCol, oldRow, newCol, newRow);
                case SquareValues.White:
                    return IsValidWhiteMove(oldCol, oldRow, newCol, newRow);
                case SquareValues.BlackKing:
                    return IsValidBlackKingMove(oldCol, oldRow, newCol, newRow);
                case SquareValues.WhiteKing:
                    return IsValidWhiteKingMove(oldCol, oldRow, newCol, newRow);
                default:
                    throw new Exception("Invalid SquareValue");
            }
            
        }

        private bool IsValidWhiteKingMove(int oldCol, int oldRow, int newCol, int newRow)
        {
            if (oldRow + 2 <= 7 || oldRow - 2 >= 0)
            {
                if (oldCol + 2 <= 7 || oldCol - 2 >= 0)
                {
                    if (IsBlackSquare(oldCol + 1, oldRow + 1) || IsBlackKingSquare(oldCol + 1, oldRow + 1))
                    {
                        if (IsEmptySquare(oldCol + 2, oldRow + 2) && newCol == oldCol + 2 && newRow == oldRow + 2)
                        {
                            return true;
                        }
                    }

                    if (IsBlackSquare(oldCol - 1, oldRow + 1) || IsBlackKingSquare(oldCol - 1, oldRow + 1))
                    {
                        if (IsEmptySquare(oldCol - 2, oldRow + 2) && newCol == oldCol - 2 && newRow == oldRow + 2)
                        {
                            return true;
                        }
                    }
                    if (IsBlackSquare(oldCol + 1, oldRow - 1) || IsBlackKingSquare(oldCol + 1, oldRow - 1))
                    {
                        if (IsEmptySquare(oldCol + 2, oldRow - 2) && newCol == oldCol + 2 && newRow == oldRow - 2)
                        {
                            return true;
                        }
                    }

                    if (IsBlackSquare(oldCol - 1, oldRow - 1) || IsBlackKingSquare(oldCol - 1, oldRow - 1))
                    {
                        if (IsEmptySquare(oldCol - 2, oldRow - 2) && newCol == oldCol - 2 && newRow == oldRow - 2)
                        {
                            return true;
                        }
                    }
                }
            }
            if (newRow != oldRow + 1 && newRow != oldRow - 1)
            {
                return false;
            }

            if (newCol != oldCol + 1 && newCol != oldCol - 1)
            {
                return false;
            }
            return true;
        }

        private bool IsValidWhiteMove(int oldCol, int oldRow, int newCol, int newRow)
        {
            
            if (oldRow + 2 <= 7)
            {
                if (oldCol + 2 <= 7 || oldCol - 2 >= 0)
                {
                    if (IsBlackSquare(oldCol + 1, oldRow + 1) || IsBlackKingSquare(oldCol + 1, oldRow + 1))
                    {
                        if (IsEmptySquare(oldCol + 2, oldRow + 2) && newCol == oldCol + 2 && newRow == oldRow + 2)
                        {
                            return true;
                        }
                    }

                    if (IsBlackSquare(oldCol - 1, oldRow + 1) || IsBlackKingSquare(oldCol - 1, oldRow + 1))
                    {
                        if (IsEmptySquare(oldCol - 2, oldRow + 2) && newCol == oldCol - 2 && newRow == oldRow + 2)
                        {
                            return true;
                        }
                    }
                }
            }

            if (newRow != oldRow + 1)
            {
                return false;
            }

            if (newCol != oldCol + 1 && newCol != oldCol - 1)
            {
                return false;
            }
            return true;
        }

        private bool IsValidBlackKingMove(int oldCol, int oldRow, int newCol, int newRow)
        {
            if (oldRow + 2 <= 7 || oldRow - 2 >= 0)
            {
                if (oldCol + 2 <= 7 || oldCol - 2 >= 0)
                {
                    if (IsWhiteSquare(oldCol + 1, oldRow - 1) || IsWhiteKingSquare(oldCol + 1, oldRow - 1))
                    {
                        if (IsEmptySquare(oldCol + 2, oldRow - 2) && newCol == oldCol + 2 && newRow == oldRow - 2)
                        {
                            return true;
                        }
                    }

                    if (IsWhiteSquare(oldCol - 1, oldRow - 1) || IsWhiteKingSquare(oldCol - 1, oldRow - 1))
                    {
                        if (IsEmptySquare(oldCol - 2, oldRow - 2) && newCol == oldCol - 2 && newRow == oldRow - 2)
                        {
                            return true;
                        }
                    }
                    if (IsWhiteSquare(oldCol + 1, oldRow + 1) || IsWhiteKingSquare(oldCol + 1, oldRow + 1))
                    {
                        if (IsEmptySquare(oldCol + 2, oldRow + 2) && newCol == oldCol + 2 && newRow == oldRow + 2)
                        {
                            return true;
                        }
                    }

                    if (IsWhiteSquare(oldCol - 1, oldRow + 1) || IsWhiteKingSquare(oldCol - 1, oldRow + 1))
                    {
                        if (IsEmptySquare(oldCol - 2, oldRow + 2) && newCol == oldCol - 2 && newRow == oldRow + 2)
                        {
                            return true;
                        }
                    }
                }
            }

            if (newRow != oldRow + 1 && newRow != oldRow - 1)
            {
                return false;
            }

            if (newCol != oldCol + 1 && newCol != oldCol - 1)
            {
                return false;
            }
            return true;
        }

        private bool IsValidBlackMove(int oldCol, int oldRow, int newCol, int newRow)
        {
            //if (oldCol + 2 < 7 && oldCol - 2 > 0 && oldRow + 2 < 7 && oldRow - 2 > 0)
            //{
            //    if (IsWhiteSquare(oldCol + 1, oldRow - 1) || IsWhiteKingSquare(oldCol + 1, oldRow - 1))
            //    {
            //        if (IsEmptySquare(oldCol + 2, oldRow - 2) && newCol == oldCol + 2 && newRow == oldRow - 2)
            //        {
            //            return true;
            //        }
            //    }

            //    if (IsWhiteSquare(oldCol - 1, oldRow - 1) || IsWhiteKingSquare(oldCol - 1, oldRow - 1))
            //    {
            //        if (IsEmptySquare(oldCol - 2, oldRow - 2) && newCol == oldCol - 2 && newRow == oldRow - 2)
            //        {
            //            return true;
            //        }
            //    }
            //}

            if (oldRow - 2 >= 0)
            {
                if (oldCol + 2 <= 7 || oldCol - 2 >= 0)
                {
                    if (IsWhiteSquare(oldCol + 1, oldRow - 1) || IsWhiteKingSquare(oldCol + 1, oldRow - 1))
                    {
                        if (IsEmptySquare(oldCol + 2, oldRow - 2) && newCol == oldCol + 2 && newRow == oldRow - 2)
                        {
                            return true;
                        }
                    }

                    if (IsWhiteSquare(oldCol - 1, oldRow - 1) || IsWhiteKingSquare(oldCol - 1, oldRow - 1))
                    {
                        if (IsEmptySquare(oldCol - 2, oldRow - 2) && newCol == oldCol - 2 && newRow == oldRow - 2)
                        {
                            return true;
                        }
                    }
                }
            }

            if (newRow != oldRow - 1)
            {
                return false;
            }

            if (newCol != oldCol + 1 && newCol != oldCol - 1)
            {
                return false;
            }

           
            return true;
        }

        public bool NotYourPiece( SquareValues type, int oldCol, int oldRow)
        {
            if (type == SquareValues.Black)
            {
                if (Squares[oldCol, oldRow] != SquareValues.White && Squares[oldCol, oldRow] != SquareValues.WhiteKing)
                {
                    return false;
                }
                return true;
            }

            if (type == SquareValues.White )
            {
                if (Squares[oldCol, oldRow] != SquareValues.Black && Squares[oldCol, oldRow] != SquareValues.BlackKing)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// if value is valid, it returns null
        /// if it's invalid it returns an error message
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string IsInvalidEntry( int rowOrColumn)
        {
            if (rowOrColumn > 7 || rowOrColumn < 0)
            {
                return "Must be in range 0-7";
            }
            return null;
        }

        public bool GameIsWon()
        {
            int blackCount = 0;
            int whiteCount = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var square = ReadSquare(col, row);
                    if (square == SquareValues.Black || square == SquareValues.BlackKing)
                    {
                        blackCount++;
                    }

                    if (square == SquareValues.White || square == SquareValues.WhiteKing)
                    {
                        whiteCount++;
                    }
                }
            }
            if (blackCount == 0 || whiteCount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

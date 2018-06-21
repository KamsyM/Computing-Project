using System;
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

        public bool IsEmptySquare(GameBoard board, int col, int row)
        {
            return board.Squares[col, row] == SquareValues.Empty;
        }

        public bool IsBlackSquare(GameBoard board, int col, int row)
        {
            return board.Squares[col, row] == SquareValues.Black;
        }

        public bool IsWhiteSquare(GameBoard board, int col, int row)
        {
            return board.Squares[col, row] == SquareValues.White;
        }

        public bool IsBlackKingSquare(GameBoard board, int col, int row)
        {
            return board.Squares[col, row] == SquareValues.BlackKing;
        }

        public bool IsWhiteKingSquare(GameBoard board, int col, int row)
        {
            return board.Squares[col, row] == SquareValues.WhiteKing;
        }

        public void MovePiece(GameBoard board, SquareValues type, int oldCol, int oldRow, int newCol, int newRow)
        {
            board.Squares[oldCol, oldRow] = SquareValues.Empty;
            board.Squares[newCol, newRow] = type;
            if (type == SquareValues.Black && newRow == 0)
            {
                board.Squares[newCol, newRow] = SquareValues.BlackKing;
            }

            if (type == SquareValues.White && newRow == 7)
            {
                board.Squares[newCol, newRow] = SquareValues.WhiteKing;
            }
        }

        public bool IsValidMove(GameBoard board, SquareValues type, int oldCol, int oldRow, int newCol, int newRow)
        {
            if (board.IsEmptySquare(board, newCol, newRow) == false)
            {
                return false;
            }

            if (newCol == oldCol || newRow == oldRow)
            {
                return false;
            }

            if (type == SquareValues.Black)
            {
                if (newRow != oldRow -1)
                {
                    return false;
                }

                if (newCol != oldCol +1 && newCol != oldCol - 1)
                {
                    return false;
                }
                return true;
            }

            if (type == SquareValues.BlackKing)
            {
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

            if (type == SquareValues.White)
            {
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

            if (type == SquareValues.WhiteKing)
            {
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
            return true;
        }

        public bool NotYourPiece(GameBoard board, SquareValues type, int oldCol, int oldRow)
        {
            if (type == SquareValues.Black)
            {
                if (board.Squares[oldCol, oldRow] != SquareValues.White && board.Squares[oldCol, oldRow] != SquareValues.WhiteKing)
                {
                    return false;
                }
                return true;
            }

            if (type == SquareValues.White )
            {
                if (board.Squares[oldCol, oldRow] != SquareValues.Black && board.Squares[oldCol, oldRow] != SquareValues.BlackKing)
                {
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}

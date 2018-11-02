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

        /// <summary>
        /// GameBoard Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="pieces1"></param>
        /// <param name="pieces2"></param>
        public GameBoard(int size, Piece[] pieces1, Piece[] pieces2)
        {
            Size = size;
            Squares = new SquareValues[Size, Size];
            InitialiseEmptyBoard();

            List<Type> BotNames = typeof(BotPlayer).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BotPlayer))).ToList();

            Pieces1 = pieces1;
            Pieces2 = pieces2;


        }



        string[] ColALphabet = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H" };

        /// <summary>
        /// Creates the Board in CLI
        /// </summary>
        public void PrintBoard()
        {
            int boardSize = Size;
            Console.WriteLine();
            Console.WriteLine("The board looks like this: ");
            Console.WriteLine();
            Console.Write(" ");
            for (int Column = 0; Column < boardSize; Column++)
            {
                Console.Write(" " + ColALphabet[Column] + "  ");
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

        /// <summary>
        /// Sets all squares to empty
        /// </summary>
        public void InitialiseEmptyBoard()
        {
            for (int Row = 0; Row < Size; Row++)
            {
                for (int Column = 0; Column < Size; Column++)
                {
                    Squares[Column, Row] = SquareValues.Empty;
                }
            }
        }

        /// <summary>
        /// Returns the Square Value or the Piece type contained in the Square in Question
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public SquareValues ReadSquare(int col, int row)
        {
            return Squares[col, row];
        }

        /// <summary>
        /// Puts the Pieces on the Board
        /// </summary>
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

        /// <summary>
        /// Checks if Square is contains no pieces
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsEmptySquare(int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.Empty;
        }

        /// <summary>
        /// Checks if Square contains a Black Piece
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsBlackSquare(int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.Black;
        }

        /// <summary>
        /// Checks if Square contains a White Piece
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsWhiteSquare(int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.White;
        }

        /// <summary>
        /// Checks if Square contains a Black-King Piece
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsBlackKingSquare(int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.BlackKing;
        }

        /// <summary>
        /// Checks if Square contains a White-King Piece
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool IsWhiteKingSquare(int col, int row)
        {
            if (col > 7 || row > 7 || col < 0 || row < 0)
            {
                return false;
            }
            return Squares[col, row] == SquareValues.WhiteKing;
        }

        /// <summary>
        /// Moves a Piece on the Board
        /// </summary>
        /// <param name="type"></param>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newRow"></param>
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

            if (newCol == oldCol + 2 && newRow == oldRow + 2)
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

        /// <summary>
        /// Asserts whether a move on the checkers Board by any piece is Valid
        /// </summary>
        /// <param name="type"></param>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public bool IsValidMove(SquareValues type, int oldCol, int oldRow, int newCol, int newRow)
        {
            if (IsEmptySquare(newCol, newRow) == false)
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

        /// <summary>
        /// Asserts whether a move made by a White-King Piece is valid
        /// </summary>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Asserts whether a move made by a White Piece is valid
        /// </summary>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Asserts whether a move made by a Black-King Piece is valid
        /// </summary>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Asserts whether a move made by a Black Piece is valid
        /// </summary>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        private bool IsValidBlackMove(int oldCol, int oldRow, int newCol, int newRow)
        {

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

        /// <summary>
        /// Used to check if a piece has already taken out another piece
        /// </summary>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <param name="newCol"></param>
        /// <param name="newRow"></param>
        /// <returns></returns>
        public bool HasJumped(int oldCol, int oldRow, int newCol, int newRow)
        {
            if (newCol == oldCol + 2 || newCol == oldCol - 2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks to see if a piece has the option to jump and returns an int which tells of the direction of jump
        /// </summary>
        /// <param name="type"></param>
        /// <param name="currentCol"></param>
        /// <param name="currentRow"></param>
        /// <returns></returns>
        public int CanJump(SquareValues type, int currentCol, int currentRow)
        {
            var B = SquareValues.Black;
            var Bk = SquareValues.BlackKing;
            var W = SquareValues.White;
            var Wk = SquareValues.WhiteKing;
            var E = SquareValues.Empty;
            if (type == B || type == Bk)
            {
                if (IsInvalidEntry(currentRow - 2) == null)
                {
                    if (IsInvalidEntry(currentCol + 2) == null)
                    {

                        if (Squares[currentCol + 1, currentRow - 1] == W || Squares[currentCol + 1, currentRow - 1] == Wk)
                        {
                            if (Squares[currentCol + 2, currentRow - 2] == E)
                            {
                                return 1;
                            }
                        }
                    }
                    if (IsInvalidEntry(currentCol - 2) == null)
                    {

                        if (Squares[currentCol - 1, currentRow - 1] == W || Squares[currentCol - 1, currentRow - 1] == Wk)
                        {
                            if (Squares[currentCol - 2, currentRow - 2] == E)
                            {
                                return 2;
                            }
                        }
                    }
                }
                //--------------------------------------------------------------------
                if (IsInvalidEntry(currentRow + 2) == null && type == Bk)
                {
                    if (IsInvalidEntry(currentCol + 2) == null)
                    {

                        if (Squares[currentCol + 1, currentRow + 1] == W || Squares[currentCol + 1, currentRow + 1] == Wk)
                        {
                            if (Squares[currentCol + 2, currentRow + 2] == E)
                            {
                                return 3;
                            }
                        }
                    }
                    if (IsInvalidEntry(currentCol - 2) == null)
                    {

                        if (Squares[currentCol - 1, currentRow + 1] == W || Squares[currentCol - 1, currentRow + 1] == Wk)
                        {
                            if (Squares[currentCol - 2, currentRow + 2] == E)
                            {
                                return 4;
                            }
                        }
                    }
                }
                //-----------------------------------------------------------------------
                return 0;
            }

            if (type == W || type == Wk)
            {
                if (IsInvalidEntry(currentRow + 2) == null)
                {
                    if (IsInvalidEntry(currentCol + 2) == null)
                    {

                        if (Squares[currentCol + 1, currentRow + 1] == B || Squares[currentCol + 1, currentRow + 1] == Bk)
                        {
                            if (Squares[currentCol + 2, currentRow + 2] == E)
                            {
                                return 3;
                            }
                        }
                    }

                    if (IsInvalidEntry(currentCol - 2) == null)
                    {

                        if (Squares[currentCol - 1, currentRow + 1] == B || Squares[currentCol - 1, currentRow + 1] == Bk)
                        {
                            if (Squares[currentCol - 2, currentRow + 2] == E)
                            {
                                return 4;
                            }
                        }
                    }
                }
                //-----------------------------------------------------------
                if (IsInvalidEntry(currentRow - 2) == null && type == Wk)
                {
                    if (IsInvalidEntry(currentCol + 2) == null)
                    {

                        if (Squares[currentCol + 1, currentRow - 1] == B || Squares[currentCol + 1, currentRow - 1] == Bk)
                        {
                            if (Squares[currentCol + 2, currentRow - 2] == E)
                            {
                                return 1;
                            }
                        }
                    }

                    if (IsInvalidEntry(currentCol - 2) == null)
                    {

                        if (Squares[currentCol - 1, currentRow - 1] == B || Squares[currentCol - 1, currentRow - 1] == Bk)
                        {
                            if (Squares[currentCol - 2, currentRow - 2] == E)
                            {
                                return 2;
                            }
                        }
                    }
                }
                //---------------------------------------------------------------
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// Checks if a piece can be Jumped
        /// </summary>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <returns></returns>
        public bool CanBeJumped(int oldCol, int oldRow)
        {
            try
            {
                if (CanJump(Squares[oldCol + 1, oldRow - 1], oldCol + 1, oldRow - 1) != 4 && CanJump(Squares[oldCol - 1, oldRow - 1], oldCol - 1, oldRow - 1) != 3)
                {
                    if (CanJump(Squares[oldCol + 1, oldRow + 1], oldCol + 1, oldRow + 1) != 2 && CanJump(Squares[oldCol - 1, oldRow + 1], oldCol - 1, oldRow + 1) != 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        /// <summary>
        /// Checks to see if a Piece can make any moves
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CanMove(SquareValues type)
        {
            for (int oldrow = 0; oldrow < 8; oldrow++)
            {
                for (int oldcol = 0; oldcol < 8; oldcol++)
                {
                    if (!IsEmptySquare(oldcol, oldrow) && !NotYourPiece(type, oldcol, oldrow))
                    {
                        var realtype = Squares[oldcol, oldrow];
                        for (int newrow = 0; newrow < 8; newrow++)
                        {
                            for (int newcol = 0; newcol < 8; newcol++)
                            {
                                if (IsEmptySquare(newcol, newrow) && IsValidMove(realtype, oldcol, oldrow, newcol, newrow))
                                {
                                    return true;
                                }

                            }

                        }

                    }
                }

            }
            return false;
        }

        /// <summary>
        /// Checks if the Piecetype is correct relative to the SquareValue put in
        /// </summary>
        /// <param name="type"></param>
        /// <param name="oldCol"></param>
        /// <param name="oldRow"></param>
        /// <returns></returns>
        public bool NotYourPiece(SquareValues type, int oldCol, int oldRow)
        {
            if (type == SquareValues.Black)
            {
                if (Squares[oldCol, oldRow] != SquareValues.White && Squares[oldCol, oldRow] != SquareValues.WhiteKing)
                {
                    return false;
                }
                return true;
            }

            if (type == SquareValues.White)
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
        public string IsInvalidEntry(int rowOrColumn)
        {
            if (rowOrColumn > 7 || rowOrColumn < 0)
            {
                return "Must be in range 0-7 for Row and in Range A-H for Column";
            }
            return null;
        }

        /// <summary>
        /// Checks to see of game is won by seeing if there is none of one piecetype
        /// </summary>
        /// <returns></returns>
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
            //if (!CanMove(SquareValues.White) || !CanMove(SquareValues.Black))
            //{
            //    return true;
            //}
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Converts numbers up to 320 to numbers 0 - 7
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int MouseConverter(int num)
        {
            if (num <= 40)
            {
                return 0;
            }
            if (num <= 80 && num > 40)
            {
                return 1;
            }
            if (num <= 120 && num > 80)
            {
                return 2;
            }
            if (num <= 160 && num > 120)
            {
                return 3;
            }
            if (num <= 200 && num > 160)
            {
                return 4;
            }
            if (num <= 240 && num > 200)
            {
                return 5;
            }
            if (num <= 280 && num > 240)
            {
                return 6;
            }
            if (num <= 320 && num > 280)
            {
                return 7;
            }

            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Records all the Pieces and their position on the board
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int[,] RecordPieces()
        {
            int[,] PiecePos = new int[8,8];
            for (int col = 0; col < 8; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    switch (Squares[col,row])
                    {
                        case SquareValues.Empty:
                            PiecePos[col, row] = 0;
                            break;
                        case SquareValues.Black:
                            PiecePos[col, row] = 1;
                            break;
                        case SquareValues.BlackKing:
                            PiecePos[col, row] = 2;
                            break;
                        case SquareValues.White:
                            PiecePos[col, row] = 3;
                            break;
                        case SquareValues.WhiteKing:
                            PiecePos[col, row] = 4;
                            break;
                        default:
                            break;
                    }
                }
            }
            return PiecePos;
        }
    }
}

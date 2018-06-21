using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;
using Checkers.DataFixture;

namespace Checkers.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------- CHECKERS --------");
            Console.WriteLine();
            bool running = true;
            GameBoard Board = null;
            while (running == true)
            {
                DisplayMenu();
                char menuchoice = Console.ReadLine().ToCharArray()[0];
                switch (menuchoice)
                {
                    case '1':
                        var blackpieces = Pieces.BlackPlacements();
                        var whitepieces = Pieces.WhitePlacements();
                        Board = new GameBoard(8, blackpieces, whitepieces);
                        Start1PGame(Board);
                        break;
                    case '2':
                        var blackpieces2 = Pieces.BlackPlacements();
                        var whitepieces2 = Pieces.WhitePlacements();
                        Board = new GameBoard(8, blackpieces2, whitepieces2);
                        Start2PGame(Board);
                        break;
                    case '0':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Menu Choice");
                        break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("--- Menu ---");
            Console.WriteLine();
            Console.WriteLine("1. Single Player");
            Console.WriteLine("2. Two Players");
            Console.WriteLine("0. Quit");
            Console.WriteLine();
            Console.WriteLine("Enter your choice number");
        }

        private static void Start1PGame(GameBoard board)
        {
            bool GameWon = false;
            board.InitializePieces();
            while (!GameWon)
            {
                PrintBoard(board);
                Console.WriteLine("You are the Black Piece");
                Console.WriteLine();
                Move(board, SquareValues.Black);

                break;
            }
        }

        private static void Start2PGame(GameBoard board)
        {
            bool GameWon = false;
            board.InitializePieces();
            while (!GameWon)
            {
                PrintBoard(board);
                Console.WriteLine();
                Console.WriteLine("You are the Black Piece");
                Console.WriteLine();
                Move(board, SquareValues.Black);
                Console.WriteLine();
                Console.WriteLine("You are the White Piece");
                Move(board, SquareValues.White);
                break;
            }
        }

        private static void Move(GameBoard board, SquareValues type)
        {
            Console.WriteLine("Select the Piece you would like to move");
            var Problem = true;
            while (Problem)
            {

                Console.Write("Put in its column: ");
                var OldColumn = Convert.ToInt32(Console.ReadLine());

                if (OldColumn > 7 || OldColumn < 0)
                {
                    Console.WriteLine("This Column doesn't exist");
                    Console.WriteLine();
                    continue;
                }

                Console.Write("Now the Piece row: ");
                var OldRow = Convert.ToInt32(Console.ReadLine());

                if (OldRow > 7 || OldRow < 0)
                {
                    Console.WriteLine("This Row doesn't exist");
                    Console.WriteLine();
                    continue;
                }
                Console.WriteLine();
              
                if (board.IsEmptySquare(board, OldColumn, OldRow))
                {
                    Console.WriteLine("This square is empty");
                    Console.WriteLine();
                }

                else if (board.NotYourPiece(board, type, OldColumn, OldRow)/*board.IsWhiteSquare(board, OldColumn, OldRow) || board.IsWhiteKingSquare(board, OldColumn, OldRow)*/)
                {
                    Console.WriteLine("This is not your piece");
                    Console.WriteLine();
                }

                else if(board.NotYourPiece(board, type, OldColumn, OldRow) == false/*board.IsBlackSquare(board, OldColumn, OldRow) || board.IsBlackKingSquare(board, OldColumn, OldRow)*/)
                {
                    var realtype = board.Squares[OldColumn, OldRow];
                    Console.Write("Now put in new column: ");
                    var NewColumn = Convert.ToInt32(Console.ReadLine());

                    if (NewColumn > 7 || NewColumn < 0)
                    {
                        Console.WriteLine("This Column doesn't exist");
                        Console.WriteLine();
                        continue;
                    }

                    Console.Write("Now put in new row: ");
                    var NewRow = Convert.ToInt32(Console.ReadLine());

                    if (NewRow > 7 || NewRow < 0)
                    {
                        Console.WriteLine("This Row doesn't exist");
                        Console.WriteLine();
                        continue;
                    }


                    if (board.IsValidMove(board, realtype, OldColumn, OldRow, NewColumn, NewRow))
                    {                 
                        board.MovePiece(board, realtype, OldColumn, OldRow, NewColumn, NewRow);
                        Problem = false;
                        PrintBoard(board);
                    }

                    else
                    {
                        Console.WriteLine("Not a Valid Move");
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void PrintBoard(GameBoard board)
        {
            int boardSize = board.Size;
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
                    SquareValues square = board.ReadSquare(col, row);
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
        }
    }
}

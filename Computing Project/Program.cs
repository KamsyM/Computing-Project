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
            bool running = true;
            while (running)
            {
                Console.WriteLine("Black or White");
                string response = Console.ReadLine().ToUpper();
                char PlayerType = response[0];
                bool GameWon = false;
                board.InitializePieces();
                if (PlayerType == 'B')
                {
                    while (!GameWon)
                    {
                        PrintBoard(board);
                        Console.WriteLine("You are the Black Piece");
                        Console.WriteLine();
                        Move(board, SquareValues.Black);
                        if (board.GameIsWon())
                        {
                            Console.WriteLine("Black Wins!!!!");
                            GameWon = true;
                            running = false;
                            break;
                        }
                        CompMove(board, SquareValues.White);
                        if (board.GameIsWon())
                        {
                            Console.WriteLine("White Wins!!!!");
                            GameWon = true;
                            running = false;
                        }
                    }
                }

                if (PlayerType == 'W')
                {
                    while (!GameWon)
                    {
                        PrintBoard(board);
                        Console.WriteLine("You are the White Piece");
                        Console.WriteLine();
                        CompMove(board, SquareValues.Black);
                        if (board.GameIsWon())
                        {
                            Console.WriteLine("Black Wins!!!!");
                            GameWon = true;
                            running = false;
                            break;
                        }
                        Move(board, SquareValues.White);
                        if (board.GameIsWon())
                        {
                            Console.WriteLine("White Wins!!!!");
                            GameWon = true;
                            running = false;
                        }
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Entry");
                }
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
                if (board.GameIsWon())
                {
                    Console.WriteLine("Black Wins!!!!");
                    GameWon = true;
                    break;
                }
                PrintBoard(board);
                Console.WriteLine();
                Console.WriteLine("You are the White Piece");
                Move(board, SquareValues.White);
                if (board.GameIsWon())
                {
                    Console.WriteLine("White Wins!!!!");
                    GameWon = true;
                }
            }
        }

        private static void Move(GameBoard board, SquareValues type)
        {
            Console.WriteLine("Select the Piece you would like to move");
            var Problem = true;
            string msg = "";
            while (Problem)
            {
                Console.Write("Put in its column: ");
                var OldColumn = Convert.ToInt32(Console.ReadLine());

                msg = board.IsInvalidEntry(OldColumn);
                if (msg != null)
                {
                    Console.WriteLine(msg);
                    PrintBoard(board);
                    continue;
                }

                Console.Write("Now the Piece row: ");
                var OldRow = Convert.ToInt32(Console.ReadLine());

                msg = board.IsInvalidEntry(OldRow);
                if (msg != null)
                {
                    Console.WriteLine(msg);
                    PrintBoard(board);
                    continue;
                }
                Console.WriteLine();
              
                if (board.IsEmptySquare( OldColumn, OldRow))
                {
                    Console.WriteLine("This square is empty");
                    PrintBoard(board);
                    Console.WriteLine();
                }

                else if (board.NotYourPiece( type, OldColumn, OldRow))
                {
                    Console.WriteLine("This is not your piece");
                    PrintBoard(board);
                    Console.WriteLine();
                }

                else if(board.NotYourPiece( type, OldColumn, OldRow) == false)
                {
                    var realtype = board.Squares[OldColumn, OldRow];
                    Console.Write("Now put in new column: ");
                    var NewColumn = Convert.ToInt32(Console.ReadLine());

                    msg = board.IsInvalidEntry(NewColumn);
                    if (msg != null)
                    {
                        Console.WriteLine(msg);
                        PrintBoard(board);
                        continue;
                    }

                    Console.Write("Now put in new row: ");
                    var NewRow = Convert.ToInt32(Console.ReadLine());

                    msg = board.IsInvalidEntry(NewRow);
                    if (msg != null)
                    {
                        Console.WriteLine(msg);
                        PrintBoard(board);
                        continue;
                    }


                    if (board.IsValidMove( realtype, OldColumn, OldRow, NewColumn, NewRow))
                    {                 
                        board.MovePiece( realtype, OldColumn, OldRow, NewColumn, NewRow);
                        Problem = false;
                    }

                    else if(!board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
                    {
                        Console.WriteLine("Not a Valid Move");
                        PrintBoard(board);
                        Console.WriteLine();
                    }

                }
            }
        }

        private static void CompMove(GameBoard board, SquareValues type)
        {
            throw new NotImplementedException();
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
            Console.WriteLine();
        }

       
       
    }
}

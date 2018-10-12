using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;
using Checkers.DataFixture;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using Computing_Project.Properties;

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
            Restart:
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
                    case '4':
                        var TestBlacks = Pieces.JumpedBlack();
                        var TestWhites = Pieces.JumpingWhite();
                        Board = new GameBoard(8, TestBlacks, TestWhites);
                        Start1PGame(Board);
                        break;
                    case '3':
                        var retry = true;
                        while (retry)
                        {
                            try
                            {
                                string tempExeName = Path.Combine(Directory.GetCurrentDirectory(), "GUI.exe");
                                using (FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
                                {
                                    byte[] bytes = Resources.GetGUI();

                                    fsDst.Write(bytes, 0, bytes.Length);
                                }
                                Process.Start(tempExeName);
                                retry = false;
                            }
                            catch (Exception)
                            {
                                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "GUI.exe"));

                            }
                        }
                        running = false;
                        Application.Exit();                       
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
            Console.WriteLine("Enter ` in order to return back to menu at any time");
            Console.WriteLine();
            Console.WriteLine("1. Single Player");
            Console.WriteLine("2. Two Players");
            Console.WriteLine("3. GUI Version");
            Console.WriteLine("0. Quit");
            Console.WriteLine();
            Console.WriteLine("Enter your choice number");
        }

        private static void Start1PGame(GameBoard board)
        {
            bool running = true;
            while (running)
            {
                BotPlayerTempV Bot = null;
                Console.WriteLine("Black or White");
                string response = Console.ReadLine().ToUpper();
                char PlayerType = response[0];
                bool GameWon = false;
                board.InitializePieces();
                if (PlayerType == 'B')
                {
                    Console.WriteLine();
                    Console.WriteLine("Select Player Difficulty (1 - 3)");
                    int difficulty = Convert.ToInt32(Console.ReadLine());
                    Bot = new BotPlayerTempV(board, SquareValues.White, difficulty);
                    while (!GameWon)
                    {
                        board.PrintBoard();
                        Console.WriteLine("You are the Black Piece");
                        Console.WriteLine();
                        Move(board, SquareValues.Black);
                        if (board.GameIsWon())
                        {
                            Console.WriteLine();
                            board.PrintBoard();
                            Console.WriteLine();
                            Console.WriteLine("Black Wins!!!!");
                            GameWon = true;
                            running = false;
                            return;
                        }
                        Bot.Move();
                        if (board.GameIsWon())
                        {
                            Console.WriteLine();
                            board.PrintBoard();
                            Console.WriteLine();
                            Console.WriteLine("White Wins!!!!");
                            GameWon = true;
                            running = false;
                            return;
                        }
                    }
                }

                if (PlayerType == 'W')
                {
                    Console.WriteLine();
                    Console.WriteLine("Select Player Difficulty (1 - 3)");
                    int difficulty = Convert.ToInt32(Console.ReadLine());
                    Bot = new BotPlayerTempV(board, SquareValues.Black, difficulty);
                    while (!GameWon)
                    {
                        
                        Console.WriteLine("You are the White Piece");
                        Console.WriteLine();
                        Bot.Move();
                        board.PrintBoard();
                        if (board.GameIsWon())
                        {
                            Console.WriteLine();
                            board.PrintBoard();
                            Console.WriteLine();
                            Console.WriteLine("Black Wins!!!!");
                            GameWon = true;
                            running = false;
                            return;
                        }
                        Move(board, SquareValues.White);
                        if (board.GameIsWon())
                        {
                            Console.WriteLine();
                            board.PrintBoard();
                            Console.WriteLine();
                            Console.WriteLine("White Wins!!!!");
                            GameWon = true;
                            running = false;
                            return;
                        }
                    }
                }

                else
                {
                    Console.WriteLine("Invalid Entry");
                    Console.WriteLine();
                }
            }
        }

        

        private static void Start2PGame(GameBoard board)
        {
            bool GameWon = false;
            board.InitializePieces();
            while (!GameWon)
            {
                board.PrintBoard();
                Console.WriteLine();
                Console.WriteLine("You are the Black Piece");
                Console.WriteLine();
                Move(board, SquareValues.Black);               
                if (board.GameIsWon())
                {
                    Console.WriteLine();
                    Console.WriteLine("Black Wins!!!!");
                    GameWon = true;
                    return;
                }
                board.PrintBoard();
                Console.WriteLine();
                Console.WriteLine("You are the White Piece");
                Move(board, SquareValues.White);
                if (board.GameIsWon())
                {
                    Console.WriteLine();
                    Console.WriteLine("White Wins!!!!");
                    GameWon = true;
                    return;
                }
            }
        }

        private static void Move(GameBoard board, SquareValues type)
        {
            string[] ColAlphabet = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H" };
            Console.WriteLine("Select the Piece you would like to move");
            var Problem = true;
            string msg = "";
            while (Problem)
            {

                Console.Write("Put in its column and row: ");
                var OldSquare = Console.ReadLine().ToUpper();
                if (OldSquare.Length != 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Incorrect number of values, only two is required");
                    Console.WriteLine();
                    continue;
                }
               
                int OldColumn = Array.IndexOf(ColAlphabet, Convert.ToString(OldSquare[0]));
                int OldRow = Convert.ToInt32(Convert.ToString(OldSquare[1]));


                msg = board.IsInvalidEntry(OldColumn);
                if (msg != null)
                {
                    Console.WriteLine(msg);
                    board.PrintBoard();
                    continue;
                }


                msg = board.IsInvalidEntry(OldRow);
                if (msg != null)
                {
                    Console.WriteLine(msg);
                    board.PrintBoard();
                    continue;
                }
                Console.WriteLine();
              
                if (board.IsEmptySquare( OldColumn, OldRow))
                {
                    Console.WriteLine("This square is empty");
                    board.PrintBoard();
                    Console.WriteLine();
                }

                else if (board.NotYourPiece( type, OldColumn, OldRow))
                {
                    Console.WriteLine("This is not your piece");
                    board.PrintBoard();
                    Console.WriteLine();
                }

                else if(board.NotYourPiece( type, OldColumn, OldRow) == false)
                {
                    var realtype = board.Squares[OldColumn, OldRow];
                    Console.Write("Put in the new Column and Row: ");
                    var NewSquare = Console.ReadLine().ToUpper();
                    if (NewSquare.Length != 2)
                    {
                        Console.WriteLine("Incorrect number of values, only two is required");
                        continue;
                    }

                    int NewColumn = Array.IndexOf(ColAlphabet, Convert.ToString(NewSquare[0]));
                    int NewRow = Convert.ToInt32(Convert.ToString(NewSquare[1]));

                    msg = board.IsInvalidEntry(NewColumn);
                    if (msg != null)
                    {
                        Console.WriteLine(msg);
                        board.PrintBoard();
                        continue;
                    }


                    msg = board.IsInvalidEntry(NewRow);
                    if (msg != null)
                    {
                        Console.WriteLine(msg);
                        board.PrintBoard();
                        continue;
                    }


                    if (board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
                    {
                        board.MovePiece(realtype, OldColumn, OldRow, NewColumn, NewRow);

                        if (board.HasJumped(OldColumn, OldRow, NewColumn, NewRow))
                        {
                            if (board.IsValidMove(realtype, NewColumn, NewRow, NewColumn + 2, NewRow - 2) || board.IsValidMove(realtype, NewColumn, NewRow, NewColumn - 2, NewRow - 2))
                            {
                                board.PrintBoard();
                                Console.WriteLine("Would you like to jump again: Yes/No");
                                string ans = Console.ReadLine().ToUpper();
                                char realans = ans[0];
                                if (realans == 'Y')
                                {
                                    Console.WriteLine("Jump Again");
                                    bool MultiJump = true;
                                    while (MultiJump)
                                    {
                                        OldColumn = NewColumn;
                                        OldRow = NewRow;
                                        NewColumn = Jumping(board, OldColumn, OldRow);
                                        if (board.ReadSquare(OldColumn,OldRow) == SquareValues.Empty)
                                        {
                                            board.PrintBoard();
                                            if (board.CanJump(type,NewColumn,OldRow - 2) != 0)
                                            {
                                                Console.WriteLine("Would you like to jump again: Yes/No");
                                                string ans2 = Console.ReadLine().ToUpper();
                                                char realans2 = ans2[0];
                                                if (realans2 == 'Y')
                                                {
                                                    NewRow = OldRow - 2;
                                                    continue;
                                                }
                                                if(realans2 != 'Y')
                                                {
                                                    MultiJump = false;
                                                    Problem = false;
                                                    return;
                                                }
                                            }
                                            MultiJump = false;
                                            Problem = false;
                                            return;
                                        }
                                        if (board.ReadSquare(OldColumn, OldRow) != SquareValues.Empty && MultiJump)
                                        {
                                            continue;
                                        }
                                    }
                                    return;
                                }
                                
                            }

                            if (board.IsValidMove(realtype, NewColumn, NewRow, NewColumn + 2, NewRow + 2) || board.IsValidMove(realtype, NewColumn, NewRow, NewColumn - 2, NewRow + 2))
                            {
                                board.PrintBoard();
                                Console.WriteLine("Would you like to jump again: Yes/No");
                                string ans = Console.ReadLine().ToUpper();
                                char realans = ans[0];
                                if (realans == 'Y')
                                {
                                    Console.WriteLine("Jump Again");
                                    bool MultiJump = true;
                                    while (MultiJump)
                                    {
                                        OldColumn = NewColumn;
                                        OldRow = NewRow;
                                        NewColumn = Jumping(board, OldColumn, OldRow);
                                        if (board.ReadSquare(OldColumn, OldRow) == SquareValues.Empty)
                                        {
                                            board.PrintBoard();
                                            if (board.CanJump(type, NewColumn, OldRow + 2) != 0)
                                            {
                                                Console.WriteLine("Would you like to jump again: Yes/No");
                                                string ans2 = Console.ReadLine().ToUpper();
                                                char realans2 = ans2[0];
                                                if (realans2 == 'Y')
                                                {
                                                    NewRow = OldRow + 2;
                                                    continue;
                                                }
                                                if(realans2 != 'Y')
                                                {
                                                    MultiJump = false;
                                                    Problem = false;
                                                    return;
                                                }
                                            }
                                            MultiJump = false;
                                            Problem = false;
                                            return;
                                        }
                                        if (board.ReadSquare(OldColumn, OldRow) != SquareValues.Empty && MultiJump)
                                        {
                                            continue;
                                        }
                                    }
                                    return;
                                }
                            }
                        }
                        Problem = false;
                    }

                    else if(!board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
                    {
                        Console.WriteLine("Not a Valid Move");
                        board.PrintBoard();
                        Console.WriteLine();
                    }

                }
            }
        }

        private static int Jumping(GameBoard board, int OldColumn, int OldRow)
        {
            string[] ColAlphabet = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H" };
            string msg = "";
            var realtype = board.Squares[OldColumn, OldRow];
            Console.Write("Put in the new Column and Row: ");
            var NewSquare = Console.ReadLine().ToUpper();
            if (NewSquare.Length != 2)
            {
                Console.WriteLine("Incorrect number of values, only two is required");
                return -1;
                //continue;
            }

            int NewColumn = Array.IndexOf(ColAlphabet, Convert.ToString(NewSquare[0]));
            int NewRow = Convert.ToInt32(Convert.ToString(NewSquare[1]));

            msg = board.IsInvalidEntry(NewColumn);
            if (msg != null)
            {
                Console.WriteLine(msg);
                board.PrintBoard();
                return-1;
                //continue;
            }


            msg = board.IsInvalidEntry(NewRow);
            if (msg != null)
            {
                Console.WriteLine(msg);
                board.PrintBoard();
                return-1;
                //continue;
            }


            if (board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                board.MovePiece(realtype, OldColumn, OldRow, NewColumn, NewRow);
                return NewColumn;
            }

            if (!board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                return -1;
            }
            return -1;
        }
    }
}

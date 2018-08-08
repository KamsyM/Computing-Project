using Checkers.DataFixture;
using Checkers.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGUI
{
    public partial class Form1 : Form
    {
        private Pen blackPen = new Pen(Color.Black);
        private Brush whiteBrush = new SolidBrush(Color.White);
        private Brush greyBrush = new SolidBrush(Color.Gray);
        private Brush redBrush = new SolidBrush(Color.Red);
        private Brush blackBrush = new SolidBrush(Color.Black);
        private Brush yellowBrush = new SolidBrush(Color.Yellow);
        const int squareSize = 40;
        const int boardSize = 8;
        Graphics g;
        private GameBoard Board;
        private BotPlayers Bot;
        private BotPlayers Bot2;
        private Modality Mode;
        private int gamemode = 0;
        private Dictionary<int,int> Positions = new Dictionary<int, int>();
        private int turn = 1;
        private bool PlayerBlack = true;
        private Menu menu = new Menu();
        private SquareValues PType = SquareValues.Empty;
        private Point Player1Pic = new Point(518, 87);
        private Point Player2Pic = new Point(518, 250);


        public Form1()
        {
            InitializeComponent();
            g = Grid.CreateGraphics();
            Mode = Modality.BlackTurn;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            //var blackpieces = Pieces.TestBlack();
            //var whitepieces = Pieces.TestWhite();
            Board = new GameBoard(8, blackpieces, whitepieces);

        }


        private void Grid_MouseClick(object sender, MouseEventArgs e)
        {
            int X = e.X ;
            int Y = e.Y;
            int Col = Board.MouseConverter(X);
            int row = Board.MouseConverter(Y);
            //Messages.Text = Col.ToString() + "," + row.ToString();
            try
            {
                Positions.Add(Col, row);
            }
            catch (Exception)
            {

                Positions.Clear();
                Messages.Text = "Invalid Move";
            }

            if (Positions.Count == 1)
            {
                var OldPosition = Positions.First();
                var OldColumn = OldPosition.Key;
                var OldRow = OldPosition.Value;
                if (Board.IsEmptySquare(OldColumn,OldRow))
                {
                    Messages.Text = "There is no piece in this square";
                    Positions.Clear();
                }

                if (!Board.IsEmptySquare(OldColumn, OldRow))
                {
                    switch (Mode)
                    {
                        case Modality.BlackTurn:
                            if (Board.NotYourPiece(SquareValues.Black, OldColumn, OldRow))
                            {
                                Messages.Text = "This is not your piece";
                                Positions.Clear();
                            }
                            else
                            {
                                Messages.Text = "Now select where you would like to move to";
                            }
                            break;
                        case Modality.WhiteTurn:
                            if (Board.NotYourPiece(SquareValues.White, OldColumn, OldRow))
                            {
                                Messages.Text = "This is not your piece";
                                Positions.Clear();
                            }
                            else
                            {
                                Messages.Text = "Now select where you would like to move to";
                            }
                            break;
                    }

                }
                
            }

            if (Positions.Count == 2)
            {
                if (gamemode == 0)
                {
                    if (PlayerBlack)
                    {
                  
                        if (turn == 1)
                        {
                            BlackTurn();
                            Messages.Text = "You are the Black Piece";
                        }
                        if (turn == 2)
                        {
                            WhiteBotMove();
                            return;
                        }
                    }
                    if (!PlayerBlack)
                    {
                        WhiteTurn();
                        if (Board.GameIsWon())
                        {
                            Messages.Text = "White Wins!!!!";
                            DrawBoard();
                            turn = -2;
                            return;
                        }
                        Messages.Text = "You are the White Piece";
                        BlackBotMove();
                        return;

                    }

                    if(turn == -1)
                    {
                        Messages.Text = "Black Wins!!!";
                        return;
                    }

                    if (turn == -2)
                    {
                        Messages.Text = "White Wins!!";
                        return;
                    }
                }
                if (gamemode == 1)
                {
                    if (turn == 1)
                    {
                        BlackTurn();
                        return;
                    }
                    if (turn == 2)
                    {
                        WhiteTurn();
                        return;
                    }

                    if(turn == -1)
                    {
                        Messages.Text = "Black Wins!!";
                        return;
                    }

                    if (turn == -2)
                    {
                        Messages.Text = "White Wins!!";
                        return;
                    }

                }
            }
           
        }

        private void BlackBotMove()
        {
            Bot2.Move();
            if (Board.GameIsWon())
            {
                Messages.Text = "Black Wins!!!!";
                DrawBoard();
                turn = -2;
                return;
            }
            DrawBoard();
            Mode = Modality.WhiteTurn;
        }

        private void WhiteBotMove()
        {
            Bot.Move();
            if (Board.GameIsWon())
            {
                Messages.Text = "White Wins!!!!";
                DrawBoard();
                turn = -2;
                return;
            }
            DrawBoard();
            turn = 1;
            Mode = Modality.BlackTurn;
        }



        //private void Start1PGame_Click(object sender, EventArgs e)
        //{
        //    PopUp PlayerType = new PopUp();
        //    PlayerType.ShowDialog();
        //    if (PlayerType.PType == SquareValues.Black)
        //    {
        //        Messages.Text = "You are the Black Piece";
        //        turn = 1;
        //        Mode = Modality.BlackTurn;
        //        Board.InitialiseEmptyBoard();
        //        Board.InitializePieces();
        //        DrawBoard();
        //    }

        //    if (PlayerType.PType == SquareValues.White)
        //    {
        //        PlayerBlack = false;
        //        Messages.Text = "You are the White Piece";
        //        turn = 1;
        //        Mode = Modality.BlackTurn;
        //        Board.InitialiseEmptyBoard();
        //        Board.InitializePieces();
        //        DrawBoard();
        //        BlackBotMove();
        //    }

        //}

        private void StartGame1P()
        {
            if (PType == SquareValues.Black)
            {
                Messages.Text = "You are the Black Piece";
                turn = 1;
                Mode = Modality.BlackTurn;
                Board.InitialiseEmptyBoard();
                Board.InitializePieces();
                DrawBoard();
            }

            if (PType == SquareValues.White)
            {
                PlayerBlack = false;
                BlackPiecePic.Location = Player2Pic;
                WhitePiecePic.Location = Player1Pic;
                Messages.Text = "You are the White Piece";
                turn = 1;
                Mode = Modality.BlackTurn;
                Board.InitialiseEmptyBoard();
                Board.InitializePieces();
                DrawBoard();
                BlackBotMove();
            }

            if (PType == SquareValues.Empty)
            {
                Messages.Text = "It's empty";
            }

        }

        //private void Start2PGame_Click(object sender, EventArgs e)
        //{
        //    Board.InitialiseEmptyBoard();
        //    Board.InitializePieces();
        //    gamemode = 1;
        //    DrawBoard();
        //    Messages.Text = "You are the Black Piece";
        //    turn = 1;
        //    Mode = Modality.BlackTurn;
        //}

        private void StartGame2P()
        {
            Board.InitialiseEmptyBoard();
            Board.InitializePieces();
            gamemode = 1;
            DrawBoard();
            Messages.Text = "You are the Black Piece";
            turn = 1;
            Mode = Modality.BlackTurn;
            DrawSquare(3, 3, blackPen, greyBrush);
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void BlackTurn()
        {
            Messages.Text = "You are the White Piece";
            var type = SquareValues.Black;
            var OldPosition = Positions.First();
            Positions.Remove(OldPosition.Key);
            var NewPosition = Positions.First();
            Positions.Clear();
            var OldColumn = OldPosition.Key;
            var OldRow = OldPosition.Value;
            var NewColumn = NewPosition.Key;
            var NewRow = NewPosition.Value;
            var realtype = Board.Squares[OldColumn, OldRow];

            if (Board.NotYourPiece(type, OldColumn, OldRow))
            {
                Messages.Text = "This is not your piece";
                return;
            }

            if (!Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Messages.Text = "Not a Valid Move";
                return;
            }
            if (Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Board.MovePiece(realtype, OldColumn, OldRow, NewColumn, NewRow);
            }
            if (Board.GameIsWon())
            {
                Messages.Text = "Black Wins!!!!";
                DrawBoard();
                turn = -1;
                return;
            }
            DrawBoard();
            turn = 2;
            Mode = Modality.WhiteTurn;
            return;
        }

        private void WhiteTurn()
        {
            Messages.Text = "You are the Black Piece";
            var type = SquareValues.White;
            var OldPosition = Positions.First();
            Positions.Remove(OldPosition.Key);
            var NewPosition = Positions.First();
            Positions.Clear();
            var OldColumn = OldPosition.Key;
            var OldRow = OldPosition.Value;
            var NewColumn = NewPosition.Key;
            var NewRow = NewPosition.Value;
            var realtype = Board.Squares[OldColumn, OldRow];
            if (Board.IsEmptySquare(OldColumn, OldRow))
            {
                Messages.Text = "This square is empty";
                return;
            }

            if (Board.NotYourPiece(type, OldColumn, OldRow))
            {
                Messages.Text = "This is not your piece";
                return;
            }

            if (!Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Messages.Text = "Not a Valid Move";
                return;
            }
            if (Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Board.MovePiece(realtype, OldColumn, OldRow, NewColumn, NewRow);
            }
            if (Board.GameIsWon())
            {
                Messages.Text = "White Wins!!!!";
                DrawBoard();
                turn = -2;
                return;
            }
            DrawBoard();
            turn = 1;
            Mode = Modality.BlackTurn;
            return;
        }


        private void DrawBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var square = Board.ReadSquare(col,row);

                    Brush brush = whiteBrush;
                    if (row % 2 == 0 && col % 2 == 0)
                    {
                        DrawSquare(col, row, blackPen, whiteBrush);
                    }

                    if (row % 2 == 0 && col % 2 == 1)
                    {
                        DrawSquare(col, row, blackPen, greyBrush);
                    }

                    if (row % 2 == 1 && col % 2 == 1)
                    {
                        DrawSquare(col, row, blackPen, whiteBrush);
                    }

                    if (row % 2 == 1 && col % 2 == 0)
                    {
                        DrawSquare(col, row, blackPen, greyBrush);
                    }
                    switch (square)
                    {
                        case SquareValues.Empty:
                            break;
                        case SquareValues.Black:
                            DrawCircle(col, row, blackPen, blackBrush);
                            break;
                        case SquareValues.BlackKing:
                            DrawCircle(col, row, blackPen, blackBrush);
                            DrawInnerSquare(col, row, blackPen, yellowBrush);
                            break;
                        case SquareValues.White:
                            DrawCircle(col, row, blackPen, whiteBrush);
                            break;
                        case SquareValues.WhiteKing:
                            DrawCircle(col, row, blackPen, whiteBrush);
                            DrawInnerSquare(col, row, blackPen, yellowBrush);
                            break;

                    }
                }
            }
            if (PlayerBlack)
            {
                P1remain.Text = Convert.ToString(BlackCount());
                P2remain.Text = Convert.ToString(WhiteCount());
            }
            if (!PlayerBlack)
            {
                P1remain.Text = Convert.ToString(WhiteCount());
                P2remain.Text = Convert.ToString(BlackCount());
            }
        }

        private void DrawSquare(int col, int row, Pen pen, Brush fill)
        {
            g.DrawRectangle(pen, col * squareSize, row * squareSize, squareSize, squareSize);
            if (fill != null)
            {
                g.FillRectangle(fill, col * squareSize + 1, row * squareSize + 1, squareSize - 1, squareSize - 1);
            }
        }

        private void DrawCircle(int col, int row, Pen pen, Brush fill)
        {
            g.DrawEllipse(pen, col * squareSize, row * squareSize, squareSize, squareSize);
            if (fill != null)
            {
                g.FillEllipse(fill, col * squareSize + 1, row * squareSize + 1, squareSize - 1, squareSize - 1);
            }
        }


        private void DrawStar(Pen pen, Brush fill)
        {
            g.DrawPolygon(pen,Starpoints());
            if (fill != null)
            {
                g.FillPolygon(fill, Starpoints());
            }
        }

        private Point[] Starpoints()
        {
            var p = new Point[8];
            p[0] = new Point(0, 25);
            p[1] = new Point(20, 20);
            p[2] = new Point(25, 0);
            p[3] = new Point(30, 20);
            p[4] = new Point(50, 25);
            p[5] = new Point(30, 30);
            p[6] = new Point(25, 50);
            p[7] = new Point(20, 30);
            return p;
        }

        private void DrawInnerSquare(int col, int row, Pen pen, Brush fill)
        {
            g.DrawRectangle(pen, col * squareSize+10, row * squareSize+10, squareSize - 20, squareSize - 20);
            if (fill != null)
            {
                g.FillRectangle(fill, col * squareSize + 11, row * squareSize + 11, squareSize - 21, squareSize - 21);
            }
        }

        private void MenuNewGame_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            menu.ShowDialog();
            PType = menu.PType;
            gamemode = menu.gamemode;
            Bot = new BotPlayers(Board, SquareValues.White, menu.difficulty);
            Bot2 = new BotPlayers(Board, SquareValues.Black, menu.difficulty);
            switch (gamemode)
            {
                case 0:
                    lblNameP1.Text = menu.name1P;
                    StartGame1P();
                    break;
                case 1:
                    lblNameP1.Text = menu.name2P1;
                    lblNameP2.Text = menu.name2P2;
                    StartGame2P();
                    break;
                default:
                    lblNameP1.Text = menu.name1P;
                    StartGame1P();
                    break;
            }

        }

        private int BlackCount()
        {
            int count = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var square = Board.ReadSquare(col, row);
                    if (square == SquareValues.Black || square == SquareValues.BlackKing)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        private int WhiteCount()
        {
            int count = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var square = Board.ReadSquare(col, row);
                    if (square == SquareValues.White || square == SquareValues.WhiteKing )
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}

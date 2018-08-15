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
using System.Threading;
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
        private Brush blueBrush = new SolidBrush(Color.LightBlue);
        const int squareSize = 40;
        const int boardSize = 8;
        Graphics g;
        public GameBoard Board;
        private BotPlayers Bot;
        //private BotPlayers Bot2;
        private Modality Mode;
        private int gamemode = 0;
        private Dictionary<int,int> Positions = new Dictionary<int, int>();
        private int turn = 1;
        private bool PlayerBlack = true;
        private Menu menu = new Menu();
        private PopUp popUp;
        private SquareValues PType = SquareValues.Empty;
        private Point Player1Pic = new Point(518, 87);
        private Point Player2Pic = new Point(518, 250);
        private bool MultiJump = false;
        private SquareValues BotType = SquareValues.Empty;
        private List<BotPlayer> Bots = new List<BotPlayer>();
        private Bitmap BlackWins = new Bitmap(@"C:\Users\Kamsi\Pictures\Black Checker Piece.png");
        private Bitmap WhiteWins = new Bitmap(@"C:\Users\Kamsi\Pictures\White Checker Piece.png");


        public Form1()
        {
            InitializeComponent();
            g = Grid.CreateGraphics();
            Mode = Modality.BlackTurn;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            //var blackpieces = Pieces.JumpingBlack();
            //var whitepieces = Pieces.JumpedWhite();
            Board = new GameBoard(8, blackpieces, whitepieces);
            Messages.Text = "WELCOME TO CHECKERS" +
                " \nClick the Game tab on the top left to begin";
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

                if (!MultiJump)
                {
                    Positions.Clear();
                }
                Messages.Text = "Invalid Move";
                DrawBoard();
                return;
            }

            if (MultiJump)
            {
               
                var OldPosition = Positions.First();
                var OldColumn = OldPosition.Key;
                var OldRow = OldPosition.Value;
                var NewPosition = Positions.Last();
                var NewColumn = NewPosition.Key;
                var NewRow = NewPosition.Value;
                if (NewColumn == OldColumn + 2 || NewColumn == OldColumn - 2)
                {
                    if (NewRow == OldRow +2 || NewRow == OldRow - 2)
                    {
                        switch (Mode)
                        {
                            case Modality.BlackTurn:
                                BlackTurn();
                                while (MultiJump)
                                {
                                    return;
                                }
                                break;
                            case Modality.WhiteTurn:
                                WhiteTurn();
                                while (MultiJump)
                                {
                                    return;
                                }
                                break;
                            default:
                                break;
                        }
                        
                    }
                }
                if (MultiJump)
                {
                    Positions.Remove(NewPosition.Key);
                    return;
                }

                //Positions.Add(0,1);
                //Positions.Add(3,1);
                //Positions.Clear();
                //return;
            }

            if (Positions.Count == 1)
            {
                var OldPosition = Positions.First();
                var OldColumn = OldPosition.Key;
                var OldRow = OldPosition.Value;
                var realtype = Board.ReadSquare(OldColumn, OldRow);
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
                                switch (realtype)
                                {
                                    case SquareValues.Black:
                                        DrawSquare(OldColumn, OldRow, blackPen, blueBrush);
                                        DrawCircle(OldColumn, OldRow, blackPen, blackBrush);
                                        break;
                                    case SquareValues.BlackKing:
                                        DrawSquare(OldColumn, OldRow, blackPen, blueBrush);
                                        DrawCircle(OldColumn, OldRow, blackPen, blackBrush);
                                        DrawInnerSquare(OldColumn, OldRow, blackPen, yellowBrush);
                                        break;
                                    default:
                                        break;
                                }
                                for (int newrow = 0; newrow < Board.Size; newrow++)
                                {
                                    for (int newcol = 0; newcol < Board.Size; newcol++)
                                    {
                                        if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, OldColumn, OldRow, newcol, newrow))
                                        {
                                            DrawSquare(newcol, newrow, blackPen, blueBrush);

                                        }

                                    }

                                }
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
                                switch (realtype)
                                {
                                    case SquareValues.White:
                                        DrawSquare(OldColumn, OldRow, blackPen, blueBrush);
                                        DrawCircle(OldColumn, OldRow, blackPen, whiteBrush);
                                        break;
                                    case SquareValues.WhiteKing:
                                        DrawSquare(OldColumn, OldRow, blackPen, blueBrush);
                                        DrawCircle(OldColumn, OldRow, blackPen, whiteBrush);
                                        DrawInnerSquare(OldColumn, OldRow, blackPen, yellowBrush);
                                        break;
                                    default:
                                        break;
                                }

                                for (int newrow = 0; newrow < Board.Size; newrow++)
                                {
                                    for (int newcol = 0; newcol < Board.Size; newcol++)
                                    {
                                        if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, OldColumn, OldRow, newcol, newrow))
                                        {
                                            DrawSquare(newcol, newrow, blackPen, blueBrush);

                                        }

                                    }

                                }
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
                            Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
                        }
                        if (turn == 2)
                        {
                            Thread.Sleep(1000);
                            WhiteBotMove();
                            if (!Board.CanMove(SquareValues.Black))
                            {
                                GameWonProcedure(2);
                                return;
                            }
                            Positions.Clear();
                            return;
                        }
                    }
                    if (!PlayerBlack)
                    {
                        if (turn == 2)
                        {
                            WhiteTurn();
                            if (Board.GameIsWon())
                            {
                                turn = -1;
                            }
                            Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
                        }
                        if (turn == 1)
                        {
                            Thread.Sleep(1000);
                            BlackBotMove();
                            if (!Board.CanMove(SquareValues.White))
                            {
                                GameWonProcedure(4);
                                return;
                            }
                            turn = 2;
                            Positions.Clear();
                            return;
                        }


                    }

                    if(turn == -1)
                    {
                        Messages.Text = lblNameP1.Text + " Wins!!!";
                        return;
                    }

                    if (turn == -2)
                    {
                        Messages.Text = lblNameP2.Text +  " Wins!!";
                        return;
                    }
                }
                if (gamemode == 1)
                {
                    if (turn == 1)
                    {
                        BlackTurn();
                        if (!Board.CanMove(SquareValues.White))
                        {
                            GameWonProcedure(1);
                            return;
                        }
                        return;
                    }
                    if (turn == 2)
                    {
                        WhiteTurn();
                        if (!Board.CanMove(SquareValues.Black))
                        {
                            GameWonProcedure(2);
                            return;
                        }
                        return;
                    }

                    if(turn == -1)
                    {
                        Messages.Text = lblNameP1.Text + " Wins!!";
                        return;
                    }

                    if (turn == -2)
                    {
                        Messages.Text = lblNameP2.Text + " Wins!!";
                        return;
                    }

                }
            }
           
        }



        private void BlackBotMove()
        {
            if (!Board.CanMove(SquareValues.Black))
            {
                GameWonProcedure(3);
                return;
            }
            //Bot2.Move();
            Bot.Move();
            // Bots.First().Move();
            if (Board.GameIsWon())
            {
                GameWonProcedure(4);
                return;
            }
            DrawBoard();
            Mode = Modality.WhiteTurn;
        }

        private void WhiteBotMove()
        {
            if (!Board.CanMove(SquareValues.White))
            {
                GameWonProcedure(1);
                return;
            }
            Bot.Move();
            if (Board.GameIsWon())
            {
                GameWonProcedure(2);
                return;
            }
            DrawBoard();
            turn = 1;
            Mode = Modality.BlackTurn;
        }




        private void StartGame1P()
        {
            if (PType == SquareValues.Black)
            {
                Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
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
                Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
                turn = 1;
                Mode = Modality.BlackTurn;
                Board.InitialiseEmptyBoard();
                Board.InitializePieces();
                DrawBoard();
                BlackBotMove();
                turn = 2;
            }

            if (PType == SquareValues.Empty)
            {
                Messages.Text = "It's empty";
            }

        }


        private void StartGame2P()
        {
            Board.InitialiseEmptyBoard();
            Board.InitializePieces();
            gamemode = 1;
            DrawBoard();
            Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
            turn = 1;
            Mode = Modality.BlackTurn;
            DrawSquare(3, 3, blackPen, greyBrush);
        }


        private void BlackTurn()
        {
            Messages.Text = "Your Turn " + lblNameP2.Text + "\nSelect Piece to Move";
            var type = SquareValues.Black;
            var OldPosition = Positions.First();
            var NewPosition = Positions.Last();
            if (!MultiJump)
            {
                Positions.Clear();
            }
            var OldColumn = OldPosition.Key;
            var OldRow = OldPosition.Value;
            var NewColumn = NewPosition.Key;
            var NewRow = NewPosition.Value;
            var realtype = Board.Squares[OldColumn, OldRow];

            if (Board.NotYourPiece(type, OldColumn, OldRow))
            {
                Messages.Text = "This is not your piece";
                DrawBoard();
                return;
            }

            if (!Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Messages.Text = "Not a Valid Move";
                DrawBoard();
                if (MultiJump)
                {
                    Positions.Remove(NewPosition.Key);
                }
                return;
            }
            if (Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Board.MovePiece(realtype, OldColumn, OldRow, NewColumn, NewRow);

                if (Board.HasJumped(OldColumn, OldRow, NewColumn, NewRow))
                {
                    if (Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn + 2, NewRow - 2) || Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn - 2, NewRow - 2))
                    {
                        DrawBoard();
                        if (MessageBox.Show("Jump again?", "Double Jump ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (Positions.Count > 0)
                            {
                                Positions.Clear();
                            }
                            Messages.Text = "Jump Again";
                            Positions.Add(NewColumn, NewRow);
                            MultiJump = true;
                            return;
                        }
                    }

                    if (Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn + 2, NewRow + 2) || Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn - 2, NewRow + 2))
                    {
                        DrawBoard();
                        if (MessageBox.Show("Jump again?", "Double Jump ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (Positions.Count > 0)
                            {
                                Positions.Clear();
                            }
                            Messages.Text = "Jump Again";
                            Positions.Add(NewColumn, NewRow);
                            MultiJump = true;
                            return;
                        }
                    }
                }
                
            }

            if (Board.GameIsWon())
            {
                GameWonProcedure(1);
                return;
            }
            if (MultiJump)
            {
                Messages.Text = "Your Turn " + lblNameP2.Text + "\nSelect Piece to Move";
                //Positions.Clear();
            }
            MultiJump = false;
            DrawBoard();
            turn = 2;
            Mode = Modality.WhiteTurn;
            return;
        }



        private void WhiteTurn()
        {
            Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
            var type = SquareValues.White;
            var OldPosition = Positions.First();
            var NewPosition = Positions.Last();
            if (!MultiJump)
            {
                Positions.Clear();
            }
            var OldColumn = OldPosition.Key;
            var OldRow = OldPosition.Value;
            var NewColumn = NewPosition.Key;
            var NewRow = NewPosition.Value;
            var realtype = Board.Squares[OldColumn, OldRow];


            if (Board.NotYourPiece(type, OldColumn, OldRow))
            {
                Messages.Text = "This is not your piece";
                DrawBoard();
                return;
            }

            if (!Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Messages.Text = "Not a Valid Move";
                DrawBoard();
                if (!MultiJump)
                {
                    Positions.Remove(NewPosition.Key);
                }
                return;
            }
            if (Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                Board.MovePiece(realtype, OldColumn, OldRow, NewColumn, NewRow);
                if (Board.HasJumped(OldColumn, OldRow, NewColumn, NewRow))
                {
                    if (Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn + 2, NewRow + 2) || Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn - 2, NewRow + 2))
                    {
                        DrawBoard();
                        if (MessageBox.Show("Jump again?", "Double Jump ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (Positions.Count > 0)
                            {
                                Positions.Clear();
                            }
                            Messages.Text = "Jump Again";
                            Positions.Add(NewColumn, NewRow);
                            MultiJump = true;
                            return;
                        }
                    }
                    if (Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn + 2, NewRow - 2) || Board.IsValidMove(realtype, NewColumn, NewRow, NewColumn - 2, NewRow - 2))
                    {
                        DrawBoard();
                        if (MessageBox.Show("Jump again?", "Double Jump ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (Positions.Count > 0)
                            {
                                Positions.Clear();
                            }
                            Messages.Text = "Jump Again";
                            Positions.Add(NewColumn, NewRow);
                            MultiJump = true;
                            return;
                        }
                    }
                }
                
            }

            if (Board.GameIsWon())
            {
                if (gamemode == 1)
                {
                    GameWonProcedure(2);
                }
                if (gamemode == 0)
                {
                    GameWonProcedure(3);
                    Messages.Text = lblNameP1.Text + " Wins!!!!";
                }
                return;
            }
            if (MultiJump)
            {
                Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
               // Positions.Clear();
            }
            MultiJump = false;
            DrawBoard();
            turn = 1;
            Mode = Modality.BlackTurn;
            return;
        }

        private void GameWonProcedure(int i)
        {
            switch (i)
            {
                case 1:
                    DrawBoard();
                    popUp = new PopUp(lblNameP1.Text + " Wins!!!!", BlackWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    Messages.Text = lblNameP1.Text + " Wins!!!!";
                    DrawBoard();
                    turn = -1;
                    return;
                case 2:
                    DrawBoard();
                    popUp = new PopUp(lblNameP2.Text + " Wins!!!!", WhiteWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    Messages.Text = lblNameP2.Text + " Wins!!!!";
                    DrawBoard();
                    turn = -2;
                    break;
                case 3:
                    DrawBoard();
                    popUp = new PopUp(lblNameP1.Text + " Wins!!!!", WhiteWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    Messages.Text = lblNameP1.Text + " Wins!!!!";
                    DrawBoard();
                    turn = -1;
                    break;
                case 4:
                    DrawBoard();
                    popUp = new PopUp(lblNameP2.Text + " Wins!!!!", BlackWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    Messages.Text = lblNameP2.Text + " Wins!!!!";
                    DrawBoard();
                    turn = -2;
                    break;
                default:
                    Messages.Text = "I have no idea who won, U put in a wrong number idiot, FIX ME!!!!!";
                    break;
            }

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
            //Bots = menu.Bots;
            switch (PType)
            {
                case SquareValues.Black:
                    BotType = SquareValues.White;
                    break;
                case SquareValues.White:
                    BotType = SquareValues.Black;
                    break;
                default:
                    break;
            }
            //Bots.Add(new BotPlayer1(Board, BotType));
            Bot = new BotPlayers(Board, BotType, menu.difficulty);
            //Bot2 = new BotPlayers(Board, SquareValues.Black, menu.difficulty);
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

        private void endGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Board.InitialiseEmptyBoard();
            DrawBoard();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

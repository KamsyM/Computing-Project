using Checkers.DataFixture;
using Checkers.Model;
using CheckersGUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace CheckersGUI
{
    public partial class Form1 : Form
    {
        private Pen blackPen = new Pen(Color.Black);
        private Pen yellowPen = new Pen(Color.Yellow);
        private Brush whiteBrush = new SolidBrush(Color.White);
        private Brush greyBrush = new SolidBrush(Color.Gray);
        private Brush redBrush = new SolidBrush(Color.Red);
        private Brush blackBrush = new SolidBrush(Color.Black);
        private Brush yellowBrush = new SolidBrush(Color.Yellow);
        private Brush blueBrush = new SolidBrush(Color.LightBlue);
        const int squareSize = 40;
        const int boardSize = 8;
        private int BotSpeed;
        Graphics g;
        public GameBoard Board;
        private BotPlayer Bot;
        private BotPlayer Bot2;
        private Modality Mode;
        private int gamemode = 0;
        private Dictionary<int,int> Positions = new Dictionary<int, int>();
        private int turn = 1;
        private bool PlayerBlack = true;
        private Menu menu;
        private PopUp popUp;
        private SquareValues PType = SquareValues.Empty;
        private Point Player1Pic = new Point(518, 87);
        private Point Player2Pic = new Point(518, 250);
        private bool MultiJump = false;
        private bool Highlight = false;
        bool cont = true;
        //private SquareValues BotType = SquareValues.Empty;
        private List<BotPlayer> Bots = new List<BotPlayer>();
        private Bitmap BlackWins = Properties.Resources.Black_Checker_Piece;
        private Bitmap WhiteWins = Properties.Resources.White_Checker_Piece;
        private System.Media.SoundPlayer StartSound = new System.Media.SoundPlayer(Properties.Resources.GameStart);
        CancellationTokenSource source = new CancellationTokenSource();


        public Form1()
        {
            InitializeComponent();
            g = Grid.CreateGraphics();
            Mode = Modality.BlackTurn;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            //var blackpieces = Pieces.TestingComp3();
            //var whitepieces = Pieces.Empty();
            Board = new GameBoard(8, blackpieces, whitepieces);
            menu = new Menu(Board);
            Messages.Text = "WELCOME TO CHECKERS" +
                " \nClick the Game tab on the top left to begin";
        }


        private async void Grid_MouseClick(object sender, MouseEventArgs e)
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
                                BlackHighlightConditions(OldColumn, OldRow, realtype);
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
                                WhiteHighlightConditions(OldColumn, OldRow, realtype);
                            }
                            break;
                    }

                }
                
            }

            if (Positions.Count == 2)
            {
                DrawBoard();
                var newcol = Positions.Last().Key;
                var newrow = Positions.Last().Value;
                if (gamemode == 0)
                {
                    if (PlayerBlack)
                    {
                  
                        if (turn == 1)
                        {
                            if (Board.Squares[newcol,newrow] == SquareValues.Black)
                            {
                                Positions.Clear();
                                Positions.Add(newcol, newrow);
                                BlackHighlightConditions(newcol,newrow,SquareValues.Black);
                                return;
                            }
                            Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
                            BlackTurn();                          
                        }
                        if (turn == 2)
                        {
                            await Task.Delay(1000);
                            WhiteBotMove();
                            if (!Board.CanMove(SquareValues.Black) && !Board.GameIsWon())
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
                            if (Board.Squares[newcol, newrow] == SquareValues.White)
                            {
                                Positions.Clear();
                                Positions.Add(newcol, newrow);
                                WhiteHighlightConditions(newcol, newrow, SquareValues.White);
                                return;
                            }
                            Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
                            WhiteTurn();
                            if (Board.GameIsWon())
                            {
                                turn = -1;
                            }
                        }
                        if (turn == 1)
                        {
                            await Task.Delay(1000);
                            BlackBotMove();
                            if (!Board.CanMove(SquareValues.White) && !Board.GameIsWon())
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
                        if (Board.Squares[newcol, newrow] == SquareValues.Black)
                        {
                            Positions.Clear();
                            Positions.Add(newcol, newrow);
                            BlackHighlightConditions(newcol, newrow, SquareValues.Black);
                            return;
                        }
                        BlackTurn();
                        if (!Board.CanMove(SquareValues.White) && !Board.GameIsWon())
                        {
                            GameWonProcedure(1);
                            return;
                        }
                        return;
                    }
                    if (turn == 2)
                    {
                        if (Board.Squares[newcol, newrow] == SquareValues.White)
                        {
                            Positions.Clear();
                            Positions.Add(newcol, newrow);
                            WhiteHighlightConditions(newcol, newrow, SquareValues.White);
                            return;
                        }
                        WhiteTurn();
                        if (!Board.CanMove(SquareValues.Black) && !Board.GameIsWon())
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

        /// <summary>
        /// Checks to see if the White pieces should be Highlighted
        /// </summary>
        /// <param name="OldColumn"></param>
        /// <param name="OldRow"></param>
        /// <param name="realtype"></param>
        private void WhiteHighlightConditions(int OldColumn, int OldRow, SquareValues realtype)
        {
            if (menu.highlight && gamemode == 1)
            {
                WhiteHighlight(OldColumn, OldRow, realtype, false);
            }
            if (menu.beginner && gamemode == 0)
            {
                WhiteHighlight(OldColumn, OldRow, realtype, false);
            }
            if (Highlight)
            {
                WhiteHighlight(OldColumn, OldRow, realtype, false);
            }
            Messages.Text = "Now select where you would like to move to";
        }

        /// <summary>
        /// Checks to see if the Black pieces should be Highlighted
        /// </summary>
        /// <param name="OldColumn"></param>
        /// <param name="OldRow"></param>
        /// <param name="realtype"></param>
        private void BlackHighlightConditions(int OldColumn, int OldRow, SquareValues realtype)
        {
            if (menu.highlight && gamemode == 1)
            {
                BlackHighlight(OldColumn, OldRow, realtype, false);
            }
            if (menu.beginner && gamemode == 0)
            {
                BlackHighlight(OldColumn, OldRow, realtype, false);
            }
            if (Highlight)
            {
                BlackHighlight(OldColumn, OldRow, realtype, false);
            }
            Messages.Text = "Now select where you would like to move to";
        }

        /// <summary>
        /// Plays a match against bots
        /// </summary>
        private async void BotMatch()
        {
            Bot = menu.Bot1;
            Bot2 = menu.Bot2;
            BotSpeed = menu.playspeed;
            //Bot = new BotPlayerTempV(Board, SquareValues.Black, menu.CG1diff);
            //Bot2 = new BotPlayerTempV(Board, SquareValues.White, menu.CG2diff);
            Board.InitialiseEmptyBoard();
            Board.InitializePieces();
            DrawBoard();
            cont = true;
            Messages.Text = "Simulating Game...";
            while (!Board.GameIsWon() && cont == true)
            {            
                if (turn == 1)
                {
                    await Task.Delay(BotSpeed);
                    BlackBotMove();
                    if (Board.GameIsWon())
                    {
                        cont = false;
                        turn = 1;
                        return;
                    }
                    if (!Board.CanMove(SquareValues.White) && !Board.GameIsWon())
                    {
                        GameWonProcedure(1);
                        cont = false;
                        turn = 1;
                        return;
                    }
                    if (cont == false)
                    {
                        return;
                    }
                    turn = 2;
                }
                if (turn == 2)
                {
                    await Task.Delay(BotSpeed);
                    WhiteBotMove();
                    if (!Board.CanMove(SquareValues.Black) && !Board.GameIsWon())
                    {
                        GameWonProcedure(2);
                        cont = false;
                        turn = 1;
                        return;
                    }
                    turn = 1;
                    
                }
            }
            turn = 1;
            return;
        }

        /// <summary>
        /// Highlights White Users possible moves
        /// </summary>
        /// <param name="OldColumn"></param>
        /// <param name="OldRow"></param>
        /// <param name="realtype"></param>
        /// <param name="DoubleJump"></param>
        private void WhiteHighlight(int OldColumn, int OldRow, SquareValues realtype, bool DoubleJump)
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
                    DrawStar(OldColumn, OldRow, blackPen, redBrush);
                    //DrawInnerSquare(OldColumn, OldRow, blackPen, yellowBrush);
                    break;
                default:
                    break;
            }

            for (int newrow = 0; newrow < Board.Size; newrow++)
            {
                for (int newcol = 0; newcol < Board.Size; newcol++)
                {
                    if (!DoubleJump)
                    {                    
                        if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, OldColumn, OldRow, newcol, newrow))
                        {
                            DrawSquare(newcol, newrow, blackPen, blueBrush);

                        }
                    }
                    if (DoubleJump)
                    {
                        if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, OldColumn, OldRow, newcol, newrow) && Math.Abs(newrow-OldRow) > 1)
                        {
                            DrawSquare(newcol, newrow, blackPen, blueBrush);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Highlights Black Users possible moves
        /// </summary>
        /// <param name="OldColumn"></param>
        /// <param name="OldRow"></param>
        /// <param name="realtype"></param>
        /// <param name="DoubleJump"></param>
        private void BlackHighlight(int OldColumn, int OldRow, SquareValues realtype, bool DoubleJump)
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
                    DrawStar(OldColumn, OldRow, blackPen, redBrush);
                    //DrawInnerSquare(OldColumn, OldRow, blackPen, yellowBrush);
                    break;
                default:
                    break;
            }
            for (int newrow = 0; newrow < Board.Size; newrow++)
            {
                for (int newcol = 0; newcol < Board.Size; newcol++)
                {
                    if (!DoubleJump)
                    {                 
                        if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, OldColumn, OldRow, newcol, newrow))
                        {
                            DrawSquare(newcol, newrow, blackPen, blueBrush);

                        }
                    }
                    if (DoubleJump)
                    {
                        if (Board.IsEmptySquare(newcol, newrow) && Board.IsValidMove(realtype, OldColumn, OldRow, newcol, newrow) && Math.Abs(newrow - OldRow) > 1)
                        {
                            DrawSquare(newcol, newrow, blackPen, blueBrush);

                        }
                    }
                }

            }
        }

        /// <summary>
        /// Moves the Black bot piece
        /// </summary>
        private void BlackBotMove()
        {
            if (gamemode == 2)
            {
                if (!Board.CanMove(SquareValues.Black))
                {
                    GameWonProcedure(2);
                    return;
                }
            }
            if (!Board.CanMove(SquareValues.Black))
            {
                GameWonProcedure(3);
                return;
            }
            var a = Board.RecordPieces();
            //Bot2.Move();
            Bot.Move();
            // Bots.First().Move();
            var b = Board.RecordPieces();
            HighlightMoves(a,b);
            if (gamemode == 2)
            {
                if (Board.GameIsWon())
                {
                    GameWonProcedure(1);
                    return;
                }
            }
            if (Board.GameIsWon())
            {
                GameWonProcedure(4);
                return;
            }
            //DrawBoard();
            Mode = Modality.WhiteTurn;
        }

        /// <summary>
        /// Moves the white bot piece
        /// </summary>
        private void WhiteBotMove()
        {
            if (!Board.CanMove(SquareValues.White))
            {
                GameWonProcedure(1);
                return;
            }
            var a = Board.RecordPieces();
            if (gamemode == 2)
            {
                Bot2.Move();
            }
            else
            {
                Bot.Move();
            }          
            var b = Board.RecordPieces();
            HighlightMoves(a, b);
            if (Board.GameIsWon())
            {
                GameWonProcedure(2);
                return;
            }
            //DrawBoard();
            turn = 1;
            Mode = Modality.BlackTurn;
        }

        /// <summary>
        /// Highlights the Bots Moves
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private async void HighlightMoves(int[,] a, int[,] b)
        {
            var B = SquareValues.Black;
            var Bk = SquareValues.BlackKing;
            var W = SquareValues.White;
            var Wk = SquareValues.WhiteKing;
            var E = SquareValues.Empty;
            var realtype = E;
            var NoRowsMoved = 0;
            var row1 = 0;
            var row2 = 0;
            for (int col = 0; col < 8; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if (a[col, row] != b[col, row] && Board.ReadSquare(col, row) != E)
                    {
                        realtype = Board.ReadSquare(col,row);
                        row1 = row;
                    }
                }
            }
            for (int col = 0; col < 8; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if (a[col, row] != b[col, row] && Board.ReadSquare(col, row) == E)
                    {
                        if (realtype == W || realtype == Wk)
                        {
                            if (a[col,row] == 3 || a[col,row] == 4)
                            {
                                row2 = row;
                            }
                        }
                        if (realtype == B || realtype == Bk)
                        {
                            if (a[col, row] == 1 || a[col, row] == 2)
                            {
                                row2 = row;
                            }
                        }
                        
                    }
                }
            }
            NoRowsMoved = Math.Abs(row1 - row2);
            if (NoRowsMoved != 1)
            {

                for (int col = 0; col < 8; col++)
                {
                    for (int row = 0; row < 8; row++)
                    {
                        if (a[col, row] != b[col, row] && Board.ReadSquare(col, row) == E)
                        {
                            //DrawBoard();
                            if (realtype == W)
                            {
                                if (a[col, row] == 3)
                                {
                                    DrawSquare(col, row, blackPen, yellowBrush);
                                    DrawCircle(col, row, blackPen, whiteBrush);
                                    await Task.Delay(200);
                                }

                            }
                            if (realtype == Wk)
                            {
                                if (a[col, row] == 4)
                                {
                                    DrawSquare(col, row, blackPen, yellowBrush);
                                    DrawCircle(col, row, blackPen, whiteBrush);
                                    DrawStar(col, row, blackPen, redBrush);
                                    await Task.Delay(200);
                                }
                            }
                            if (realtype == B)
                            {
                                if (a[col, row] == 1)
                                {
                                    DrawSquare(col, row, blackPen, yellowBrush);
                                    DrawCircle(col, row, blackPen, blackBrush);
                                    await Task.Delay(200);
                                }
                            }
                            if (realtype == Bk)
                            {
                                if (a[col, row] == 2)
                                {
                                    DrawSquare(col, row, blackPen, yellowBrush);
                                    DrawCircle(col, row, blackPen, blackBrush);
                                    DrawStar(col, row, blackPen, redBrush);
                                    await Task.Delay(200);
                                }
                            }
                        }
                    }
                }
                for (int col = 0; col < 8; col++)
                {
                    for (int row = 0; row < 8; row++)
                    {
                        if (a[col, row] != b[col, row] && Board.ReadSquare(col, row) != E)
                        {
                            DrawBoard();
                            if (realtype == W)
                            {
                                DrawSquare(col, row, blackPen, yellowBrush);
                                DrawCircle(col, row, blackPen, whiteBrush);
                                await Task.Delay(200);
                                DrawBoard();
                            }
                            if (realtype == Wk)
                            {
                                DrawSquare(col, row, blackPen, yellowBrush);
                                DrawCircle(col, row, blackPen, whiteBrush);
                                DrawStar(col, row, blackPen, redBrush);
                                await Task.Delay(200);
                                DrawBoard();
                            }
                            if (realtype == B)
                            {
                                DrawSquare(col, row, blackPen, yellowBrush);
                                DrawCircle(col, row, blackPen, blackBrush);
                                await Task.Delay(200);
                                DrawBoard();
                            }
                            if (realtype == Bk)
                            {
                                DrawSquare(col, row, blackPen, yellowBrush);
                                DrawCircle(col, row, blackPen, blackBrush);
                                DrawStar(col, row, blackPen, redBrush);
                                await Task.Delay(200);
                                DrawBoard();
                            }
                        }
                    }
                }
                return;
            }
            for (int col = 0; col < 8; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if (a[col, row] != b[col, row] && Board.ReadSquare(col, row) == E)
                    {
                        if (realtype == W)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, whiteBrush);
                            await Task.Delay(200);
                            //Thread.Sleep(200);
                            DrawSquare(col, row, blackPen, greyBrush);
                        }
                        if (realtype == Wk)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, whiteBrush);
                            DrawStar(col, row, blackPen, redBrush);
                            await Task.Delay(200);
                            DrawSquare(col, row, blackPen, greyBrush);
                        }
                        if (realtype == B)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, blackBrush);
                            await Task.Delay(200);
                            DrawSquare(col, row, blackPen, greyBrush);
                        }
                        if (realtype == Bk)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, blackBrush);
                            DrawStar(col, row, blackPen, redBrush);
                            await Task.Delay(200);
                            DrawSquare(col, row, blackPen, greyBrush);
                        }
                    }
                }
            }
            for (int col = 0; col < 8; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    if (a[col, row] != b[col, row] && Board.ReadSquare(col, row) != E)
                    {
                        if (realtype == W)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, whiteBrush);
                            await Task.Delay(200);
                            DrawBoard();
                            //Thread.Sleep(200);
                        }
                        if (realtype == Wk)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, whiteBrush);
                            DrawStar(col, row, blackPen, redBrush);
                            await Task.Delay(200);
                            DrawBoard();
                        }
                        if (realtype == B)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, blackBrush);
                            await Task.Delay(200);
                            DrawBoard();
                        }
                        if (realtype == Bk)
                        {
                            DrawSquare(col, row, blackPen, yellowBrush);
                            DrawCircle(col, row, blackPen, blackBrush);
                            DrawStar(col, row, blackPen, redBrush);
                            await Task.Delay(200);
                            DrawBoard();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts a One Player Game
        /// </summary>
        private void StartGame1P()
        {
            if (PType == SquareValues.Black)
            {
                PlayerBlack = true;
                BlackPiecePic.Location = Player1Pic;
                WhitePiecePic.Location = Player2Pic;
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

        /// <summary>
        /// Starts a two player game
        /// </summary>
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

        /// <summary>
        /// Performs the Black Players move
        /// </summary>
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
                            BlackHighlight(NewColumn,NewRow, Board.ReadSquare(NewColumn,NewRow),true);
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
                            BlackHighlight(NewColumn, NewRow, Board.ReadSquare(NewColumn, NewRow),true);
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

        /// <summary>
        /// Performs the White players move
        /// </summary>
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
                            WhiteHighlight(NewColumn, NewRow, Board.ReadSquare(NewColumn, NewRow),true);
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
                            WhiteHighlight(NewColumn, NewRow, Board.ReadSquare(NewColumn,NewRow),true);
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

        /// <summary>
        /// Set of Game Winning Procedures depending on input
        /// </summary>
        /// <param name="i"></param>
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

        /// <summary>
        /// Draws the Game Board
        /// </summary>
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
                            DrawStar(col,row,blackPen,redBrush);
                            //DrawInnerSquare(col, row, blackPen, yellowBrush);
                            break;
                        case SquareValues.White:
                            DrawCircle(col, row, blackPen, whiteBrush);
                            break;
                        case SquareValues.WhiteKing:
                            DrawCircle(col, row, blackPen, whiteBrush);
                            DrawStar(col, row, blackPen, redBrush);
                            //DrawInnerSquare(col, row, blackPen, yellowBrush);
                            break;

                    }
                }
            }
            if (PlayerBlack)
            {
                P1remain.Text = Convert.ToString(Board.Count(SquareValues.Black));
                P2remain.Text = Convert.ToString(Board.Count(SquareValues.White));
            }
            if (!PlayerBlack)
            {
                P1remain.Text = Convert.ToString(Board.Count(SquareValues.White));
                P2remain.Text = Convert.ToString(Board.Count(SquareValues.Black));
            }
        }

        /// <summary>
        /// Draws a Square
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="pen"></param>
        /// <param name="fill"></param>
        private void DrawSquare(int col, int row, Pen pen, Brush fill)
        {
            g.DrawRectangle(pen, col * squareSize, row * squareSize, squareSize, squareSize);
            if (fill != null)
            {
                g.FillRectangle(fill, col * squareSize + 1, row * squareSize + 1, squareSize - 1, squareSize - 1);
            }
        }

        /// <summary>
        /// Draws a Circle
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="pen"></param>
        /// <param name="fill"></param>
        private void DrawCircle(int col, int row, Pen pen, Brush fill)
        {
            g.DrawEllipse(pen, col * squareSize, row * squareSize, squareSize, squareSize);
            if (fill != null)
            {
                g.FillEllipse(fill, col * squareSize + 1, row * squareSize + 1, squareSize - 1, squareSize - 1);
            }
        }

        /// <summary>
        /// Draws a Star
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="pen"></param>
        /// <param name="fill"></param>
        private void DrawStar(int col, int row, Pen pen, Brush fill)
        {
            g.DrawPolygon(pen,Starpoints(col * squareSize + 10, row * squareSize + 20));
            if (fill != null)
            {
                g.FillPolygon(fill, Starpoints(col * squareSize + 10, row * squareSize + 20));
            }
        }

        /// <summary>
        /// Contains the Coordinates for DrawStar
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private Point[] Starpoints(int col, int row)
        {
            var p = new Point[8];
            p[0] = new Point(col, row);
            p[1] = new Point(col+8, row-2);
            p[2] = new Point(col+10, row-10);
            p[3] = new Point(col+12, row-2);
            p[4] = new Point(col+20, row);
            p[5] = new Point(col+12, row+2);
            p[6] = new Point(col+10, row+10);
            p[7] = new Point(col+8, row+2);
            //p[0] = new Point(0, 25);
            //p[1] = new Point(20, 20);
            //p[2] = new Point(25, 0);
            //p[3] = new Point(30, 20);
            //p[4] = new Point(50, 25);
            //p[5] = new Point(30, 30);
            //p[6] = new Point(25, 50);
            //p[7] = new Point(20, 30);
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
            cont = false;
            StartNewGame();
        }

        private void StartNewGame()
        {
            //CancellationToken token = source.Token;

            menu.ShowDialog();
            StartSound.Load();
            StartSound.Play();
            PType = menu.PType;
            gamemode = menu.gamemode;
            //Bots = menu.Bots;
            if (gamemode == 2)
            {
                lblNameP1.Text = menu.nameCG1;
                lblNameP2.Text = menu.nameCG2;
                BotMatch();
                return;
            }
            //switch (PType)
            //{
            //    case SquareValues.Black:
            //        BotType = SquareValues.White;
            //        break;
            //    case SquareValues.White:
            //        BotType = SquareValues.Black;
            //        break;
            //    default:
            //        break;
            //}
            //Bots.Add(new BotPlayer1(Board, BotType));
            Bot = menu.Bot;
            //Bot = new BotPlayerTempV(Board, BotType, menu.difficulty);
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

        //private int BlackCount()
        //{
        //    int count = 0;
        //    for (int row = 0; row < 8; row++)
        //    {
        //        for (int col = 0; col < 8; col++)
        //        {
        //            var square = Board.ReadSquare(col, row);
        //            if (square == SquareValues.Black || square == SquareValues.BlackKing)
        //            {
        //                count++;
        //            }
        //        }
        //    }
        //    return count;
        //}

        //private int WhiteCount()
        //{
        //    int count = 0;
        //    for (int row = 0; row < 8; row++)
        //    {
        //        for (int col = 0; col < 8; col++)
        //        {
        //            var square = Board.ReadSquare(col, row);
        //            if (square == SquareValues.White || square == SquareValues.WhiteKing )
        //            {
        //                count++;
        //            }
        //        }
        //    }
        //    return count;
        //}

        private void endGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cont = false;
            Board.InitialiseEmptyBoard();
            DrawBoard();
            Messages.Text = "Click on New Game to Start Again";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnHighlight_Click(object sender, EventArgs e)
        {
            Highlight = true;
            OnHighlight.Checked = true;
            OffHighlight.Checked = false;
        }

        private void OffHighlight_Click(object sender, EventArgs e)
        {
            Highlight = false;
            OnHighlight.Checked = false;
            OffHighlight.Checked = true;
        }

        private void OnTextBox_Click(object sender, EventArgs e)
        {
            Messages.Visible = true;
            OffTextBox.Checked = false;
            OnTextBox.Checked = true;
        }

        private void OffTextBox_Click(object sender, EventArgs e)
        {
            Messages.Visible = false;
            OffTextBox.Checked = true;
            OnTextBox.Checked = false;
        }

        private void CLIversion_Click(object sender, EventArgs e)
        {
            cont = false;
            var retry = true;
            while (retry)
            {
                try
                {
                    string tempExeName = Path.Combine(Directory.GetCurrentDirectory(), "Checkers.exe");
                    using (FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
                    {
                        byte[] bytes = Resources.GetComputingProject();

                        fsDst.Write(bytes, 0, bytes.Length);
                    }
                    Process.Start(tempExeName);
                    retry = false;
                }
                catch (Exception)
                {
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Checkers.exe"));

                }
            }
            Application.Exit();
        }
    }
}

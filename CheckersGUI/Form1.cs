using Checkers.DataFixture;
using Checkers.Model;
using CheckersGUI.Properties;
using NetComm;
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
        NetComm.Host Server;
        NetComm.Client client;
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
        public History Log;
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
        private bool past = false;
        bool cont = true;
        bool running = true;
        //private SquareValues BotType = SquareValues.Empty;
        private List<BotPlayer> Bots = new List<BotPlayer>();
        private Bitmap BlackWins = Properties.Resources.Black_Checker_Piece;
        private Bitmap WhiteWins = Properties.Resources.White_Checker_Piece;
        private System.Media.SoundPlayer StartSound = new System.Media.SoundPlayer(Properties.Resources.GameStart);
        private SquareValues CurrentType;
        private int CurrCol;
        private int CurrRow;
        private int NxtCol;
        private int NxtRow;
        //private static Piece[] blackplacements = Pieces.TestingComp3();
        //private static Piece[] whiteplacements = Pieces.Empty();
        private static Piece[] blackplacements = Pieces.BlackPlacements();
        private static Piece[] whiteplacements = Pieces.WhitePlacements();
        private GameBoard board = new GameBoard(8, blackplacements, whiteplacements);
        private string OnlineID;
        private string HostName;
        private string ClientName;
        private bool MyTurn = false;
        private bool GameWon;
        private int[,] IniBoardPlacements;
        private int[,] FinBoardPlacements;



        public Form1()
        {
            InitializeComponent();
            g = Grid.CreateGraphics();
            Mode = Modality.BlackTurn;
            Board = board;
            menu = new Menu(Board);
            Log = new History(Board);
            //Log.AddString("oh lolers");
            Messages.Text = "WELCOME TO CHECKERS" +
                " \nClick the Game tab on the top left to begin" + Environment.NewLine;
            
        }

        /// <summary>
        /// Scrolls to the bottom of the Match Log
        /// </summary>
        void Matchscroll()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                MatchLog.SelectionStart = MatchLog.Text.Length;
                MatchLog.ScrollToCaret();
            }));
        }

        void Messagescroll()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                Messages.SelectionStart = Messages.Text.Length;
                Messages.ScrollToCaret();
            }));
        }

        private async void Grid_MouseClick(object sender, MouseEventArgs e)
        {
            Task MatchScroll = new Task(new Action(Matchscroll));
            Task MessageScroll = new Task(new Action(Messagescroll));
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
                if (gamemode == 3)
                {
                    Messages.AppendText(Environment.NewLine + "Invalid Move" + Environment.NewLine);
                    MessageScroll.Start();
                }
                else if (gamemode != 3)
                {
                    Messages.Text = "Invalid Move" + Environment.NewLine;
                }

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
                                if (gamemode == 3)
                                {
                                    //Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
                                    WhoTurn.Text = lblNameP2.Text + "'s turn";
                                }
                                break;
                            case Modality.WhiteTurn:
                                WhiteTurn();
                                while (MultiJump)
                                {
                                    return;
                                }
                                if (gamemode == 3)
                                {
                                    //Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
                                    WhoTurn.Text = lblNameP2.Text + "'s turn";
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
                if (gamemode == 3)
                {
                    if (!MyTurn)
                    {
                        Messages.AppendText (Environment.NewLine + "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine);
                        MessageScroll.Start();
                        WhoTurn.Text = lblNameP2.Text + "'s turn";
                        Positions.Clear();
                        return;
                    }
                }
                var OldPosition = Positions.First();
                var OldColumn = OldPosition.Key;
                var OldRow = OldPosition.Value;
                var realtype = Board.ReadSquare(OldColumn, OldRow);
                if (Board.IsEmptySquare(OldColumn,OldRow))
                {
                    if (gamemode == 3)
                    {
                        Messages.AppendText(Environment.NewLine + "There is no piece in this square" + Environment.NewLine);
                        MessageScroll.Start();
                    }
                    else
                    {
                        Messages.Text = "There is no piece in this square" + Environment.NewLine;
                    }                   
                    Positions.Clear();
                }

                if (!Board.IsEmptySquare(OldColumn, OldRow))
                {

                    switch (Mode)
                    {
                        case Modality.BlackTurn:
                            if (Board.NotYourPiece(SquareValues.Black, OldColumn, OldRow))
                            {
                                if (gamemode==3)
                                {
                                    Messages.AppendText(Environment.NewLine +"This is not your piece" + Environment.NewLine);
                                    MessageScroll.Start();
                                }
                                else
                                {
                                    Messages.Text = "This is not your piece" + Environment.NewLine;
                                }                                
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
                                if (gamemode == 3)
                                {
                                    Messages.AppendText(Environment.NewLine + "This is not your piece" + Environment.NewLine);
                                    MessageScroll.Start();
                                }
                                else
                                {
                                    Messages.Text = "This is not your piece" + Environment.NewLine;
                                }
                                
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
                            BlackTurn();

                            

                        }
                        if (turn == 2)
                        {
                            Messages.Text = "Selecting a Piece to Move";
                            await Task.Delay(1000);
                            WhiteBotMove();
                            Log.Add(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow));
                            MatchLog.Text = Log.ToString();
                            MatchScroll.Start();
                            Messages.Text = lblNameP1.Text + ", Select a Piece to Move";
                            WhoTurn.Text = "Your Turn";
                            //var sb = new StringBuilder();
                            //sb.AppendLine("Your Turn " + lblNameP1.Text + "\nSelect Piece to Move");
                            //sb.AppendLine();
                            //sb.AppendLine(Log.ToString());
                            //Messages.Text = sb.ToString();
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
                            WhiteTurn();



                            if (Board.GameIsWon())
                            {
                                turn = -1;
                            }
                        }
                        if (turn == 1)
                        {
                            Messages.Text = "Selecting a Piece to Move";
                            WhoTurn.Text = lblNameP2.Text + "'s Turn";
                            await Task.Delay(1000);
                            BlackBotMove();
                            Log.Add(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow));
                             MatchLog.Text = Log.ToString();
                            MatchScroll.Start();
                            Messages.Text = lblNameP1.Text + ", Select a Piece to Move";
                            WhoTurn.Text = "Your Turn";
                            //var sb = new StringBuilder();
                            //sb.AppendLine("Your Turn " + lblNameP1.Text + "\nSelect Piece to Move");
                            //sb.AppendLine();
                            //sb.AppendLine(Log.ToString());
                            //Messages.Text = sb.ToString();
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
                        Messages.Text = lblNameP1.Text + " Wins!!!" + Environment.NewLine;
                        WhoTurn.Text = "";
                        return;
                    }

                    if (turn == -2)
                    {
                        Messages.Text = lblNameP2.Text +  " Wins!!" + Environment.NewLine;
                        WhoTurn.Text = "";
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
                        Messages.Text = lblNameP1.Text + " Wins!!" + Environment.NewLine;
                        WhoTurn.Text = "";
                        return;
                    }

                    if (turn == -2)
                    {
                        Messages.Text = lblNameP2.Text + " Wins!!" + Environment.NewLine;
                        WhoTurn.Text = "";
                        return;
                    }

                }

                if (gamemode == 3)
                {
                    if (GameWon)
                    {
                        return;
                    }

                    if (MyTurn)
                    {
                        if (PType == SquareValues.Black)
                        {                       
                            if (Board.Squares[newcol, newrow] == SquareValues.Black)
                            {
                                Positions.Clear();
                                Positions.Add(newcol, newrow);
                                BlackHighlightConditions(newcol, newrow, SquareValues.Black);
                                return;
                            }
                            BlackTurn();
                            MyTurn = false;
                            if (!MultiJump)
                            {
                                //Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
                                WhoTurn.Text = lblNameP2.Text + "'s turn" ;
                            }
                            if (!Board.CanMove(SquareValues.White) && !Board.GameIsWon())
                            {
                                GameWonProcedure(5);
                                return;
                            }
                            return;
                        }

                        if (PType == SquareValues.White)
                        {
                            if (Board.Squares[newcol, newrow] == SquareValues.White)
                            {
                                Positions.Clear();
                                Positions.Add(newcol, newrow);
                                WhiteHighlightConditions(newcol, newrow, SquareValues.White);
                                return;
                            }
                            WhiteTurn();
                            MyTurn = false;
                            if (!MultiJump)
                            {
                                //Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
                                WhoTurn.Text = lblNameP2.Text + "'s turn";
                            }
                            if (!Board.CanMove(SquareValues.Black) && !Board.GameIsWon())
                            {
                                GameWonProcedure(6);
                                return;
                            }
                            return;
                        }
                    }
                    else
                    {
                        //Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
                        WhoTurn.Text = lblNameP2.Text + "'s turn" ;
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
            if (gamemode != 3)
            {
                Messages.Text = "Now select where you would like to move to" + Environment.NewLine;
            }           
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
            if (gamemode != 3)
            {
                Messages.Text = "Now select where you would like to move to" + Environment.NewLine;
            }
        }

        /// <summary>
        /// Plays a match against bots
        /// </summary>
        private async void BotMatch()
        {
            Task MatchScroll = new Task(new Action(Matchscroll));
            PlayPause.Visible = true;
            running = true;
            //BotSpeed = menu.playspeed;
            //Bot = new BotPlayerTempV(Board, SquareValues.Black, menu.CG1diff);
            //Bot2 = new BotPlayerTempV(Board, SquareValues.White, menu.CG2diff);
            Board.InitialiseEmptyBoard();
            Board.InitializePieces();

            DrawBoard();
            cont = true;
            Messages.Text = "Simulating Game..." + Environment.NewLine;

            while (!Board.GameIsWon() && cont == true)
            {               
                await Task.Delay(BotSpeed);
                //check for paused
                while (!running)
                {
                    await Task.Delay(500);
                }
                if (turn == 1)
                {
                    BlackBotMove();
                    if (cont)
                    {                    
                        Log.Add(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow));
                        MatchLog.Text = Log.ToString();
                        ScrollDown();
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
                }

                await Task.Delay(BotSpeed);
                while (!running)
                {
                    await Task.Delay(500);
                }
                if (turn == 2)
                {
                    WhiteBotMove();
                    if (cont)
                    {                    
                        Log.Add(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow));
                        MatchLog.Text = Log.ToString();
                        ScrollDown();
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

            }
            turn = 1;
            return;
        }

        private void ScrollDown()
        {
            Task MatchScroll = new Task(new Action(Matchscroll));
            MatchScroll.Start();
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
                        CurrentType = realtype;
                        NxtCol = col;
                        NxtRow = row;
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
                        CurrCol = col;
                        CurrRow = row;
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
                Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move" + Environment.NewLine;
                WhoTurn.Text = "Your Turn";
                //turn = 1;
                if (turn == 1)
                {
                    Mode = Modality.BlackTurn;
                }
                if (turn == 2)
                {
                    Mode = Modality.WhiteTurn;
                }
                Board.InitialiseEmptyBoard();
                Board.InitializePieces();
                DrawBoard();
            }

            if (PType == SquareValues.White)
            {
                PlayerBlack = false;
                BlackPiecePic.Location = Player2Pic;
                WhitePiecePic.Location = Player1Pic;
                Messages.Text = "Selecting a Piece to Move" + Environment.NewLine;
                WhoTurn.Text = lblNameP2.Text + "'s Turn" ;
                //turn = 1;
                if (turn == 1)
                {
                    Mode = Modality.BlackTurn;
                }
                if (turn == 2)
                {
                    Mode = Modality.WhiteTurn;
                }
                Board.InitialiseEmptyBoard();
                Board.InitializePieces();
                DrawBoard();
                BlackBotMove();
                Log.Add(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow));
                MatchLog.Text = Log.ToString();
                Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move";
                WhoTurn.Text = "Your Turn";
                //var sb = new StringBuilder();
                //sb.AppendLine("Your Turn " + lblNameP1.Text + "\nSelect Piece to Move");
                //sb.AppendLine();
                //sb.AppendLine(Log.ToString());
                //Messages.Text = sb.ToString();
                turn = 2;
            }

            if (PType == SquareValues.Empty)
            {
                Messages.Text = "It's empty" + Environment.NewLine;
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
            Messages.Text = lblNameP1.Text + ", Select Piece to Move" + Environment.NewLine;
            WhoTurn.Text = lblNameP1.Text + "'s Turn";
            //turn = 1;
            if (turn == 1)
            {
                Mode = Modality.BlackTurn;
            }
            if (turn == 2)
            {
                Mode = Modality.WhiteTurn;
            }
        }

        /// <summary>
        /// Performs the Black Players move
        /// </summary>
        private void BlackTurn()
        {
            Task MatchScroll = new Task(new Action(Matchscroll));
            Task MesssageScroll = new Task(new Action(Messagescroll));
            if (gamemode != 3)
            {
                Messages.Text = lblNameP2.Text + " Select Piece to Move" + Environment.NewLine;
            }
            WhoTurn.Text = lblNameP2.Text + "'s Turn";
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
            CurrentType = realtype;
            CurrCol = OldColumn;
            CurrRow = OldRow;
            NxtCol = NewColumn;
            NxtRow = NewRow;

            if (Board.NotYourPiece(type, OldColumn, OldRow))
            {
                if (gamemode == 3)
                {
                    Messages.AppendText(Environment.NewLine + "This is not your piece" + Environment.NewLine);
                    MesssageScroll.Start();
                }
                else
                {
                    Messages.Text = "This is not your piece" + Environment.NewLine;
                }
                
                DrawBoard();
                return;
            }

            if (!Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                if (gamemode == 3)
                {
                    Messages.AppendText(Environment.NewLine + "Not a Valid Move" + Environment.NewLine);
                    MesssageScroll.Start();
                }
                if (gamemode != 3)
                {
                    Messages.Text = "Not a Valid Move" + Environment.NewLine;
                }

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
                Log.Add(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow));
                MatchLog.Text = Log.ToString();
                MatchScroll.Start();
                if (gamemode == 3)
                {
                    if (menu.joingame)
                    {
                        client.SendData(ConvertStringToBytes(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow).ToString()));
                    }
                    else
                    {
                        Server.Brodcast(ConvertStringToBytes(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow).ToString()));
                    }
                }
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
                            if (gamemode == 3)
                            {
                                Messages.AppendText(Environment.NewLine +"Jump Again" + Environment.NewLine);
                                MesssageScroll.Start();
                            }
                            if (gamemode != 3)
                            {
                                Messages.Text = "Jump Again" + Environment.NewLine;
                            }
                            
                            Positions.Add(NewColumn, NewRow);
                            BlackHighlight(NewColumn,NewRow, Board.ReadSquare(NewColumn,NewRow),true);
                            MultiJump = true;
                            if (gamemode == 3)
                            {
                                if (menu.joingame)
                                {
                                    client.SendData(ConvertStringToBytes("^"));
                                }
                                else
                                {
                                    Server.Brodcast(ConvertStringToBytes("^"));
                                }
                            }
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
                            if (gamemode == 3)
                            {
                                Messages.AppendText(Environment.NewLine + "Jump Again" + Environment.NewLine);
                                MesssageScroll.Start();
                            }
                            if (gamemode != 3)
                            {
                                Messages.Text = "Jump Again" + Environment.NewLine;
                            }
                            Positions.Add(NewColumn, NewRow);
                            BlackHighlight(NewColumn, NewRow, Board.ReadSquare(NewColumn, NewRow),true);
                            MultiJump = true;
                            if (gamemode == 3)
                            {
                                if (menu.joingame)
                                {
                                    client.SendData(ConvertStringToBytes("^"));
                                }
                                else
                                {
                                    Server.Brodcast(ConvertStringToBytes("^"));
                                }
                            }
                            return;
                        }
                    }
                }
                
            }

            if (gamemode != 3 && Board.GameIsWon())
            {

                GameWonProcedure(1);
                return;
            }
            if (gamemode == 3 && Board.GameIsWon())
            {
                GameWonProcedure(5);
                return;
            }
            //if (MultiJump)
            //{
            //    Messages.Text = "Your Turn " + lblNameP2.Text + "\nSelect Piece to Move" + Environment.NewLine;
            //    //Positions.Clear();
            //}
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
            Task MatchScroll = new Task(new Action(Matchscroll));
            Task MesssageScroll = new Task(new Action(Messagescroll));
            if (gamemode != 3)
            {
                Messages.Text = lblNameP1.Text + "Select Piece to Move" + Environment.NewLine;
                WhoTurn.Text = lblNameP1.Text + "'s Turn";
            }
            if (gamemode == 3)
            {
                WhoTurn.Text = lblNameP2.Text + "'s Turn"; ;
            }
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
            CurrentType = realtype;
            CurrCol = OldColumn;
            CurrRow = OldRow;
            NxtCol = NewColumn;
            NxtRow = NewRow;

            if (Board.NotYourPiece(type, OldColumn, OldRow))
            {
                if (gamemode == 3)
                {
                    Messages.AppendText(Environment.NewLine + "This is not your piece" + Environment.NewLine);
                    MesssageScroll.Start();
                }
                else
                {
                    Messages.Text = "This is not your piece" + Environment.NewLine;
                }
                DrawBoard();
                return;
            }

            if (!Board.IsValidMove(realtype, OldColumn, OldRow, NewColumn, NewRow))
            {
                if (gamemode == 3)
                {
                    Messages.AppendText(Environment.NewLine + "Not a Valid Move" + Environment.NewLine);
                    MesssageScroll.Start();
                }
                if (gamemode != 3)
                {
                    Messages.Text = "Not a Valid Move" + Environment.NewLine;
                }
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
                Log.Add(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow));
                MatchLog.Text = Log.ToString();
                MatchScroll.Start();
                if (gamemode == 3)
                {
                    if (menu.joingame)
                    {
                        client.SendData(ConvertStringToBytes(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow).ToString()));
                    }
                    else
                    {
                        Server.Brodcast(ConvertStringToBytes(new Move(Mode, CurrentType, CurrCol, CurrRow, NxtCol, NxtRow).ToString()));
                    }
                }
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
                            if (gamemode == 3)
                            {
                                Messages.AppendText(Environment.NewLine + "Jump Again" + Environment.NewLine);
                                MesssageScroll.Start();
                            }
                            if (gamemode != 3)
                            {
                                Messages.Text = "Jump Again" + Environment.NewLine;
                            }
                            Positions.Add(NewColumn, NewRow);
                            WhiteHighlight(NewColumn, NewRow, Board.ReadSquare(NewColumn, NewRow),true);
                            MultiJump = true;
                            if (gamemode == 3)
                            {
                                if (menu.joingame)
                                {
                                    client.SendData(ConvertStringToBytes("^"));
                                }
                                else
                                {
                                    Server.Brodcast(ConvertStringToBytes("^"));
                                }
                            }
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
                            if (gamemode == 3)
                            {
                                Messages.AppendText(Environment.NewLine + "Jump Again" + Environment.NewLine);
                                MesssageScroll.Start();
                            }
                            if (gamemode != 3)
                            {
                                Messages.Text = "Jump Again" + Environment.NewLine;
                            }
                            Positions.Add(NewColumn, NewRow);
                            WhiteHighlight(NewColumn, NewRow, Board.ReadSquare(NewColumn,NewRow),true);
                            MultiJump = true;
                            if (gamemode == 3)
                            {
                                if (menu.joingame)
                                {
                                    client.SendData(ConvertStringToBytes("^"));
                                }
                                else
                                {
                                    Server.Brodcast(ConvertStringToBytes("^"));
                                }
                            }
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
                    Messages.Text = lblNameP1.Text + " Wins!!!!" + Environment.NewLine;
                }
                if (gamemode == 3)
                {
                    GameWonProcedure(6);
                }
                return;
            }
            //if (MultiJump)
            //{
            //    Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move" + Environment.NewLine;
            //   // Positions.Clear();
            //}
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
            Task MatchScroll = new Task(new Action(Matchscroll));
            switch (i)
            {
                case 1:
                    DrawBoard();
                    popUp = new PopUp(lblNameP1.Text + " Wins!!!!", BlackWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP1.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP1.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    GameWon = true;
                    turn = -1;
                    return;
                case 2:
                    DrawBoard();
                    popUp = new PopUp(lblNameP2.Text + " Wins!!!!", WhiteWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP2.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP2.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    GameWon = true;
                    turn = -2;
                    break;
                case 3:
                    DrawBoard();
                    popUp = new PopUp(lblNameP1.Text + " Wins!!!!", WhiteWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP1.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP1.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    turn = -1;
                    break;
                case 4:
                    DrawBoard();
                    popUp = new PopUp(lblNameP2.Text + " Wins!!!!", BlackWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP2.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP2.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    turn = -2;
                    break;
                case 5:
                    DrawBoard();
                    popUp = new PopUp(lblNameP1.Text + " Wins!!!!", BlackWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP1.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP1.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    GameWon = true;
                    if (menu.joingame)
                    {
                        client.SendData(ConvertStringToBytes("$"));
                    }
                    else
                    {
                        Server.Brodcast(ConvertStringToBytes("$"));
                    }
                    return;
                case 6:
                    DrawBoard();
                    popUp = new PopUp(lblNameP1.Text + " Wins!!!!", WhiteWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP2.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP1.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    GameWon = true;
                    if (menu.joingame)
                    {
                        client.SendData(ConvertStringToBytes("$"));
                    }
                    else
                    {
                        Server.Brodcast(ConvertStringToBytes("$"));
                    }
                    break;
                case 7:
                    DrawBoard();
                    popUp = new PopUp(lblNameP2.Text + " Wins!!!!", BlackWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP2.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP2.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    GameWon = true;
                    break;
                case 8:
                    DrawBoard();
                    popUp = new PopUp(lblNameP2.Text + " Wins!!!!", WhiteWins);
                    popUp.ShowDialog();
                    MultiJump = false;
                    //Messages.Text = lblNameP2.Text + " Wins!!!!";
                    WhoTurn.Text = lblNameP2.Text + " Won";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    Log.Clear();
                    DrawBoard();
                    GameWon = true;
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

        /// <summary>
        /// Draws smaller square to fit inside pieces
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="pen"></param>
        /// <param name="fill"></param>
        private void DrawInnerSquare(int col, int row, Pen pen, Brush fill)
        {
            g.DrawRectangle(pen, col * squareSize+10, row * squareSize+10, squareSize - 20, squareSize - 20);
            if (fill != null)
            {
                g.FillRectangle(fill, col * squareSize + 11, row * squareSize + 11, squareSize - 21, squareSize - 21);
            }
        }

        /// <summary>
        /// Instructions for when the New Game button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuNewGame_Click(object sender, EventArgs e)
        {

            cont = false;
            Board = board;
            StartNewGame();
        }

        /// <summary>
        /// Starts a game of the required mode
        /// </summary>
        private void StartNewGame()
        {
            WhoTurn.Text = "";
            try
            {
                Server.CloseConnection(); //Closes all of the opened connections and stops listening
            }
            catch (Exception)
            {

            }
            try
            {
                client.Disconnect();
            }
            catch (Exception)
            {
            }
            try
            {
                //var blackpieces = Pieces.BlackPlacements();
                //var whitepieces = Pieces.WhitePlacements();
                //Board = new GameBoard(8, blackpieces, whitepieces);
                Log.Clear();
                Log.Board = Board;
            }
            catch (Exception)
            {

            }
            Messages.Clear();
            MatchLog.Clear();
            GameWon = false;
            PlayPause.Visible = false;
            menu.ShowDialog();
            StartSound.Load();
            StartSound.Play();
            PType = menu.PType;
            gamemode = menu.gamemode;
            OnlineID = menu.onlineid;
            turn = 1;
            if (gamemode == 2)
            {
                ChatMessage.Visible = false;
                SendButton.Visible = false;
                cont = true;
                BotSpeed = menu.playspeed;
                Bot = menu.Bot1;
                Bot2 = menu.Bot2;
                lblNameP1.Text = menu.nameCG1;
                lblNameP2.Text = menu.nameCG2;
                BotMatch();
                return;
            }
            Bot = menu.Bot;
            switch (gamemode)
            {
                case 0:
                    lblNameP1.Text = menu.name1P;
                    ChatMessage.Visible = false;
                    SendButton.Visible = false;
                    StartGame1P();
                    break;
                case 1:
                    lblNameP1.Text = menu.name2P1;
                    lblNameP2.Text = menu.name2P2;
                    ChatMessage.Visible = false;
                    SendButton.Visible = false;
                    StartGame2P();
                    break;
                case 3:
                    Messages.Clear();
                    ChatMessage.Visible = true;
                    SendButton.Visible = true;
                    if (menu.joingame)
                    {
                        Clientside(menu.hostid, menu.port);
                        lblNameP1.Text = OnlineID;

                    }
                    else
                    {
                        Hostside(PType,menu.port);
                        lblNameP1.Text = OnlineID;

                        if (PType == SquareValues.Black)
                        {
                            PlayerBlack = true;
                            BlackPiecePic.Location = Player1Pic;
                            WhitePiecePic.Location = Player2Pic;
                            MyTurn = true;
                        }
                        if (PType == SquareValues.White)
                        {
                            PlayerBlack = false;
                            BlackPiecePic.Location = Player2Pic;
                            WhitePiecePic.Location = Player1Pic;
                            MyTurn = false;
                        }
                    }
                    break;
                default:
                    lblNameP1.Text = menu.name1P;
                    StartGame1P();
                    break;
            }

        }

        /// <summary>
        /// Adjusts the Piecetype pictures depending on the type the client plays as
        /// </summary>
        private void ClientPlay()
        {
            if (PType == SquareValues.Black)
            {
                PlayerBlack = true;
                BlackPiecePic.Location = Player1Pic;
                WhitePiecePic.Location = Player2Pic;
            }
            if (PType == SquareValues.White)
            {
                PlayerBlack = false;
                BlackPiecePic.Location = Player2Pic;
                WhitePiecePic.Location = Player1Pic;

            }
        }

        /// <summary>
        /// Starts a loaded game
        /// </summary>
        private void StartLoadGame()
        {
            WhoTurn.Text = "";
            PlayPause.Visible = false;
            StartSound.Load();
            StartSound.Play();
            if (gamemode == 2)
            {
                // lblNameP1.Text = menu.nameCG1;
                // lblNameP2.Text = menu.nameCG2;
                BotMatch();
                return;
            }

            switch (gamemode)
            {
                case 0:
                    //lblNameP1.Text = menu.name1P;
                    StartGame1P();
                    break;
                case 1:
                    //lblNameP1.Text = menu.name2P1;
                    //lblNameP2.Text = menu.name2P2;
                    StartGame2P();
                    break;
                default:
                    //lblNameP1.Text = menu.name1P;
                    StartGame1P();
                    break;
            }
        }

        /// <summary>
        /// Starts Online game
        /// </summary>
        private void StartGameOnline()
        {

            Board.InitialiseEmptyBoard();
            Board.InitializePieces();
            DrawBoard();
            IniBoardPlacements = Board.RecordPieces();
            //if (MyTurn)
            //{
            //    Messages.Text = "Your Turn " + lblNameP1.Text + "\nSelect Piece to Move" + Environment.NewLine;
            //}
            //else
            //{
            //    Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
            //}
        }

        /// <summary>
        /// Instructions for when the End Game button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WhoTurn.Text = "";
            try
            {
                Server.CloseConnection(); //Closes all of the opened connections and stops listening
            }
            catch (Exception)
            {

            }
            try
            {
                client.Disconnect();
            }
            catch (Exception)
            {
            }
            ChatMessage.Visible = false;
            SendButton.Visible = false;
            cont = false;
            Board.InitialiseEmptyBoard();
            DrawBoard();
            Messages.Text = "Click on New Game to Start Again" + Environment.NewLine;
        }

        /// <summary>
        /// Instructions for when the Exit button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (gamemode == 3 && menu.joingame)
            {
                if (client.isConnected)
                {
                    ChatMessage.Visible = true;
                    SendButton.Visible = true;
                }
            }
            else if (gamemode == 3 && menu.hostgame)
            {
                if (Server.Listening)
                {
                    ChatMessage.Visible = true;
                    SendButton.Visible = true;
                }
            }
        }

        private void OffTextBox_Click(object sender, EventArgs e)
        {
            Messages.Visible = false;
            OffTextBox.Checked = true;
            OnTextBox.Checked = false;
            ChatMessage.Visible = false;
            SendButton.Visible = false;
        }

        private void CLIversion_Click(object sender, EventArgs e)
        {
            WhoTurn.Text = "";
            try
            {
                Server.CloseConnection(); //Closes all of the opened connections and stops listening
            }
            catch (Exception)
            {

            }
            try
            {
                client.Disconnect();
            }
            catch (Exception)
            {
            }
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

        private void PlayPause_Click(object sender, EventArgs e)
        {
            if (PlayPause.Visible == true)
            {
                Messages.Text = "Simulating Game...";
                if (running)
                {
                    running = false;
                    Messages.Text = "Game is Paused" + Environment.NewLine;
                    return;
                }

                if (!running)
                {
                    running = true;
                    return;
                }
            }
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gamemode == 3)
            {
                MessageBox.Show("An Online Game Can't be saved", "CAUTION", MessageBoxButtons.OK);
            }
            else
            {           
                string filename = "";
                string filepath = "";
                SaveFileDialog sfd = new SaveFileDialog();
                DialogResult dr = sfd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    filename = sfd.FileName;
                    filepath = Path.Combine(Directory.GetCurrentDirectory(), filename);
                }
                try
                {
                    using (TextWriter Writer = new StreamWriter(new FileStream(filename + ".chk", FileMode.Create)))
                    {

                        for (int row = 0; row < 8; row++)
                        {
                            for (int col = 0; col < 8; col++)
                            {
                                var square = Board.ReadSquare(col, row);

                                switch (square)
                                {
                                    case SquareValues.Empty:
                                        Writer.Write(col);
                                        Writer.Write(row);
                                        Writer.Write(square);
                                        Writer.Write(Environment.NewLine);
                                        break;
                                    case SquareValues.Black:
                                        Writer.Write(col);
                                        Writer.Write(row);
                                        Writer.Write(square);
                                        Writer.Write(Environment.NewLine);
                                        break;
                                    case SquareValues.BlackKing:
                                        Writer.Write(col);
                                        Writer.Write(row);
                                        Writer.Write(square);
                                        Writer.Write(Environment.NewLine);
                                        break;
                                    case SquareValues.White:
                                        Writer.Write(col);
                                        Writer.Write(row);
                                        Writer.Write(square);
                                        Writer.Write(Environment.NewLine);
                                        break;
                                    case SquareValues.WhiteKing:
                                        Writer.Write(col);
                                        Writer.Write(row);
                                        Writer.Write(square);
                                        Writer.Write(Environment.NewLine);
                                        break;

                                }
                            }
                        }
                        Writer.Write(lblNameP1.Text);
                        Writer.Write(Environment.NewLine);
                        Writer.Write(lblNameP2.Text);
                        Writer.Write(Environment.NewLine);
                        Writer.Write(gamemode);
                        Writer.Write(Environment.NewLine);
                        Writer.Write(turn);
                        if (gamemode == 0)
                        {
                            Writer.Write(Environment.NewLine);
                            Writer.Write(PType);
                            Writer.Write(Environment.NewLine);
                            Writer.Write(Bot);
                        }
                        if (gamemode == 2)
                        {
                            Writer.Write(Environment.NewLine);
                            Writer.Write(Bot);
                            Writer.Write(Environment.NewLine);
                            Writer.Write(Bot2);
                            Writer.Write(Environment.NewLine);
                            Writer.Write(BotSpeed);
                        }
                        //To-Do
                        foreach (var item in Log.Plays)
                        {
                            Writer.Write(Environment.NewLine);
                            Writer.Write(item.ToString());
                        }
                        Writer.Close();
                    }
                }
                catch (Exception z)
                {
                    Messages.Text = z.Message;
                }
            }
        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Server.CloseConnection(); //Closes all of the opened connections and stops listening
            }
            catch (Exception)
            {

            }
            try
            {
                client.Disconnect();
            }
            catch (Exception)
            {
            }
            try
            {
                Log.Clear();
            }
            catch (Exception)
            {

            }
            string filename = "";
            string filepath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Checker Files (*.chk)|*.CHK|All Files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                filename = ofd.FileName;
                filepath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            }

            try
            {
                using (TextReader Reader = new StreamReader(File.Open(filename, FileMode.Open)))
                {
                    List<Piece> blacks = new List<Piece>();
                    List<Piece> whites = new List<Piece>();
                   //var blackpieces = new Piece[12];
                    //var whitepieces = new Piece[12];
                    var blackcount = 0;
                    var whitecount = 0;
                    var empty = Pieces.Empty();

                    for (int i = 0; i < 64; i++)
                    {
                        string line = Reader.ReadLine();
                        int col = Convert.ToInt32(line.Substring(0,1));
                        int row = Convert.ToInt32(line.Substring(1, 1));
                        SquareValues type = FindType(line.Substring(2,line.Length - 2));
                        if (type == SquareValues.Empty)
                        {
                            continue;
                        }
                        if (type == SquareValues.Black || type == SquareValues.BlackKing)
                        {
                            //blackpieces[blackcount] = new Piece(col, row, type);
                            blacks.Add(new Piece(col, row, type));
                            blackcount++;
                        }
                        if (type == SquareValues.White || type == SquareValues.WhiteKing)
                        {
                            //whitepieces[whitecount] = new Piece(col, row, type);
                            whites.Add(new Piece(col, row, type));
                            whitecount++;
                        }
                       // pieces[i] = new Piece(col, row, type);
                    }
                    var blackpieces = new Piece[blacks.Count];
                    var whitepieces = new Piece[whites.Count];
                    for (int i = 0; i < blacks.Count; i++)
                    {
                        blackpieces[i] = blacks[i];
                    }
                    for (int i = 0; i < whites.Count; i++)
                    {
                        whitepieces[i] = whites[i];
                    }
                    lblNameP1.Text = Reader.ReadLine();
                    lblNameP2.Text = Reader.ReadLine();
                    gamemode = Convert.ToInt32(Reader.ReadLine());
                    turn = Convert.ToInt32(Reader.ReadLine());
                    Board = new GameBoard(8, blackpieces, whitepieces);
                    Log.Board = Board;
                    if (gamemode == 0)
                    {
                        PType = FindType(Reader.ReadLine());
                        var BotName = Reader.ReadLine();
                        var BotType = Board.OpponentType(PType);
                        List<BotPlayer> botlist = new List<BotPlayer>();
                        List<Type> BotNames = typeof(BotPlayer).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BotPlayer))).ToList();
                        foreach (var item in BotNames)
                        {
                            botlist.Add((BotPlayer)Activator.CreateInstance(item, Board, BotType));
                        }
                        foreach (var item in botlist)
                        {
                            if (item.ToString() == BotName)
                            {
                                Bot = item;
                            }
                        }
                        
                        //Bot = (BotPlayer)Activator.CreateInstance(item, Board, BotType);
                    }
                    if (gamemode == 2)
                    {
                        var BotType = SquareValues.Black;
                        List<BotPlayer> botlist = new List<BotPlayer>();
                        List<BotPlayer> botlist2 = new List<BotPlayer>();
                        List<Type> BotNames = typeof(BotPlayer).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BotPlayer))).ToList();
                        foreach (var item in BotNames)
                        {
                            botlist.Add((BotPlayer)Activator.CreateInstance(item, Board, BotType));
                            botlist2.Add((BotPlayer)Activator.CreateInstance(item, Board, BotType));
                        }
                        var BotName1 = Reader.ReadLine();
                        var BotName2 = Reader.ReadLine();
                        BotSpeed = Convert.ToInt32(Reader.ReadLine());
                        foreach (var item in botlist)
                        {
                            if (item.ToString() == BotName1)
                            {
                                Bot = item;
                                Bot.Type = SquareValues.Black;
                            }
                        }
                        foreach (var item in botlist2)
                        {
                            if (item.ToString() == BotName2)
                            {
                                //Bot2 = menu.Bot2;
                                Bot2 = item;
                                Bot2.Type = SquareValues.White;
                            }
                        }
                        
                    }
                    Log.AddString(Reader.ReadToEnd());
                    StartLoadGame();
                }
            }
            catch (Exception z)
            {
                Messages.Text = z.Message;
            }
        }

        private SquareValues FindType(string type)
        {
            var realtype = type.ToUpper();
            switch (realtype)
            {
                case "BLACK":
                    return SquareValues.Black;
                case "WHITE":
                    return SquareValues.White;
                case "BLACK KING":
                    return SquareValues.BlackKing;
                case "WHITE KING":
                    return SquareValues.WhiteKing;
                case "EMPTY":
                    return SquareValues.Empty;
                default:
                    return SquareValues.Empty;
            }
        }

        #region HOST
        private void Hostside(SquareValues pType, int port)
        {

            Server = new NetComm.Host(menu.port); //Initialize the Server object, connection will use the 2020 port number
            Server.StartConnection(); //Starts listening for incoming clients

            //Adding event handling methods, to handle the server messages
            Server.onConnection += new NetComm.Host.onConnectionEventHandler(Server_onConnection);
            Server.lostConnection += new NetComm.Host.lostConnectionEventHandler(Server_lostConnection);
            Server.DataReceived += new NetComm.Host.DataReceivedEventHandler(Server_DataReceived);
        }

        void Server_DataReceived(string ID, byte[] Data)
        {
            Task MessageScroll = new Task(new Action(Messagescroll));
            Task MatchScroll = new Task(new Action(Matchscroll));
            string data = ConvertBytesToString(Data);
            switch (data[0])
            {
                case '*':
                    Messages.AppendText(ID + ": " + data.Substring(1) + Environment.NewLine);
                    MessageScroll.Start();
                    break;
                case '$':
                    if (PType == SquareValues.Black)
                    {
                        GameWonProcedure(8);
                    }
                    if (PType == SquareValues.White)
                    {
                        GameWonProcedure(7);
                    }

                    break;
                case '^':
                    MyTurn = false;
                    //Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
                    WhoTurn.Text = lblNameP2.Text + "'s turn";
                    break;
                default:
                    var Movement = ConvertStringToMove(data);
                    if (Movement.Mode == Modality.BlackTurn)
                    {
                        Mode = Modality.WhiteTurn;
                    }
                    if (Movement.Mode == Modality.WhiteTurn)
                    {
                        Mode = Modality.BlackTurn;
                    }
                    Board.MovePiece(Movement);
                    FinBoardPlacements = Board.RecordPieces();
                    HighlightMoves(IniBoardPlacements, FinBoardPlacements);
                    IniBoardPlacements = FinBoardPlacements;
                    DrawBoard();
                    Log.Add(Movement);
                    MyTurn = true;
                    WhoTurn.Text = "Your Turn";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    //var sb = new StringBuilder();
                    //sb.AppendLine("Your Turn " + lblNameP1.Text + "\nSelect Piece to Move");
                    //sb.AppendLine();
                    //sb.AppendLine(Log.ToString());
                    //Messages.Text = sb.ToString();
                    break;
            }
           
        }

        void Server_lostConnection(string id)
        {
            if (Messages.IsDisposed) return; //Fixes the invoke error
            Messages.AppendText(id + " disconnected" + Environment.NewLine); //Updates the log textbox when user leaves the room
        }

        void Server_onConnection(string id)
        {
            Task MessageScroll = new Task(new Action(Messagescroll));
            lblNameP2.Text = id;
            Messages.AppendText(Environment.NewLine + id + " connected!" + Environment.NewLine); //Updates the log textbox when new user joined
            MessageScroll.Start();
            Server.SendData(id, ConvertStringToBytes( "`" + OnlineID));
            Server.SendData(id, ConvertStringToBytes("!" + Convert.ToString(PType)));
            Mode = Modality.BlackTurn;
            if (MyTurn)
            {
                Messages.AppendText(Environment.NewLine + "Your Turn " + OnlineID + "\nSelect Piece to Move" + Environment.NewLine);
                MessageScroll.Start();
                WhoTurn.Text = "Your Turn " ;
            }
            if (!MyTurn)
            {
                Messages.AppendText(Environment.NewLine + "It's " + id + "'s turn" + Environment.NewLine);
                MessageScroll.Start();
                WhoTurn.Text = id + "'s turn";
            }
            StartGameOnline();
        }
        #endregion

        #region CLIENT
        private void Clientside(string hostid, int port)
        {
            client = new NetComm.Client(); //Initialize the client object

            //Adding event handling methods for the client
            client.Connected += new NetComm.Client.ConnectedEventHandler(client_Connected);
            client.Disconnected += new NetComm.Client.DisconnectedEventHandler(client_Disconnected);
            client.DataReceived += new NetComm.Client.DataReceivedEventHandler(client_DataReceived);

            //Connecting to the host
            client.Connect(menu.hostid, menu.port, menu.onlineid); //Connecting to the host (on the same machine) with port 2020 and ID "Jack"
        }

        void client_DataReceived(byte[] Data, string ID)
        {
            Task MessageScroll = new Task(new Action(Messagescroll));
            Task MatchScroll = new Task(new Action(Matchscroll));
            string data = ConvertBytesToString(Data);
            switch (data[0])
            {
                case '*':
                    Messages.AppendText(ID + HostName + ": " + data.Substring(1) + Environment.NewLine);
                    MessageScroll.Start();
                    break;
                case '`':
                    HostName = data.Substring(1);
                    lblNameP2.Text = HostName;
                    break;
                case '!':
                    PType = Board.OpponentType(ConvertStringToSquareValues(data.Substring(1)));
                    ClientPlay();
                    if (PType == SquareValues.Black)
                    {
                        MyTurn = true;
                    }
                    if (PType == SquareValues.White)
                    {
                        MyTurn = false;
                    }
                    if (MyTurn)
                    {
                        Messages.AppendText(Environment.NewLine + "Your Turn " + OnlineID + "\nSelect Piece to Move" + Environment.NewLine);
                        MessageScroll.Start();
                        WhoTurn.Text = "Your Turn ";
                    }
                    if (!MyTurn)
                    {
                        Messages.AppendText(Environment.NewLine + "It's " + HostName + "'s turn" + Environment.NewLine);
                        MessageScroll.Start();
                        WhoTurn.Text = HostName + "'s turn";
                    }
                    break;
                case '$':
                    if (PType == SquareValues.Black)
                    {
                        GameWonProcedure(8);
                    }
                    if (PType == SquareValues.White)
                    {
                        GameWonProcedure(7);
                    }

                    break;
                case '^':
                    MyTurn = false;
                    //Messages.Text = "It's " + lblNameP2.Text + "'s turn" + Environment.NewLine;
                    WhoTurn.Text = lblNameP2.Text + "'s turn";
                    break;
                default:
                    var Movement = ConvertStringToMove(data);
                    if (Movement.Mode == Modality.BlackTurn)
                    {
                        Mode = Modality.WhiteTurn;
                    }
                    if (Movement.Mode == Modality.WhiteTurn)
                    {
                        Mode = Modality.BlackTurn;
                    }
                    Board.MovePiece(Movement);
                    FinBoardPlacements = Board.RecordPieces();
                    HighlightMoves(IniBoardPlacements,FinBoardPlacements);
                    IniBoardPlacements = FinBoardPlacements;
                    Log.Add(Movement);
                    DrawBoard();
                    MyTurn = true;
                    WhoTurn.Text = "Your Turn";
                    MatchLog.Text = Log.ToString();
                    MatchScroll.Start();
                    //var sb = new StringBuilder();
                    //sb.AppendLine("Your Turn " + lblNameP1.Text + "\nSelect Piece to Move");
                    //sb.AppendLine();
                    //sb.AppendLine(Log.ToString());
                    //Messages.Text = sb.ToString();
                    break;
            }

        }

        void client_Disconnected()
        {
            Messages.AppendText("Disconnected from host!" + Environment.NewLine); //Updates the log with the current connection state
        }

        void client_Connected()
        {
            Messages.AppendText(Environment.NewLine + "Connected succesfully!" + Environment.NewLine); //Updates the log with the current connection state
            Mode = Modality.BlackTurn;
            StartGameOnline();
        }
        #endregion

        private void SendButton_Click_1(object sender, EventArgs e)
        {
            if (menu.joingame)
            {
                client.SendData(ConvertStringToBytes("*" +ChatMessage.Text)); //Sends the message to the host
            }

            else
            {                  
              Server.Brodcast(ConvertStringToBytes("*" + ChatMessage.Text)); //Sends the message to the client
                            
            }
            Messages.AppendText(OnlineID + ": " + ChatMessage.Text + Environment.NewLine);
            ChatMessage.Clear(); //Clears the chatmessage textbox text
        }

        private void ChatMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendButton.PerformClick();
            }
        }

        #region String to Byte Conversions
        string ConvertBytesToString(byte[] bytes)
        {
            return ASCIIEncoding.ASCII.GetString(bytes);
        }

        byte[] ConvertStringToBytes(string str)
        {
            return ASCIIEncoding.ASCII.GetBytes(str);
        }




        #endregion

        SquareValues ConvertStringToSquareValues(string text)
        {
            switch (text)
            {
                case "Black":
                    return SquareValues.Black;
                case "White":
                    return SquareValues.White;
                case "BlackKing":
                    return SquareValues.BlackKing;
                case "WhiteKing":
                    return SquareValues.WhiteKing;
                default:
                    return SquareValues.Empty;
            }
        }

        Move ConvertStringToMove(string txt)
        {
            char[] ColAlphabet = new char[8] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            char[] RowNumbers = new char[8] { '1', '2', '3', '4', '5', '6', '7', '8' };
            int BracketPosition = -1;
            foreach (var item in txt)
            {
                if (item == '(')
                {
                    BracketPosition = BracketPosition + 1;
                    break;
                }
                else
                {
                    BracketPosition = BracketPosition + 1;
                }
            }
            int inicol = Array.IndexOf(ColAlphabet, txt[BracketPosition + 1], 0);
            int inirow = Array.IndexOf(RowNumbers, txt[BracketPosition + 3], 0);
            int fincol = Array.IndexOf(ColAlphabet, txt[BracketPosition + 9], 0);
            int finrow = Array.IndexOf(RowNumbers, txt[BracketPosition + 11], 0);
            if (Mode == Modality.BlackTurn)
            {
                return new Move(Modality.BlackTurn, SquareValues.Black, inicol, inirow, fincol, finrow);
            }
            else
            {
                return new Move(Modality.WhiteTurn,SquareValues.White, inicol, inirow, fincol, finrow);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Server.CloseConnection(); //Closes all of the opened connections and stops listening
            }
            catch (Exception)
            {

            }
            try
            {
                client.Disconnect();
            }
            catch (Exception)
            {
            }
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatchLog.Visible = false;
            OffMatchLog.Checked = true;
            OnMatchLog.Checked = false;
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MatchLog.Visible = true;
            OffMatchLog.Checked = false;
            OnMatchLog.Checked = true;

        }
    }

}

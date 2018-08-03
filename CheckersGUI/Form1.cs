using Checkers.DataFixture;
using Checkers.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private int gamemode = 1;
        private Dictionary<int,int> Positions = new Dictionary<int, int>();
        private int turn = 1;


        public Form1()
        {
            InitializeComponent();
            g = Grid.CreateGraphics();
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            Board = new GameBoard(8, blackpieces, whitepieces);
            Bot = new BotPlayers(Board, SquareValues.White, 1);
            Board.InitializePieces();

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

            if (Positions.Count == 2)
            {
                if (gamemode == 1)
                {
                    if (turn == 1)
                    {
                        BlackTurn();
                        Messages.Text = "You are the Black Piece";
                    }
                    if (turn == 2)
                    {
                        Bot.Move();
                        if (Board.GameIsWon())
                        {
                            Messages.Text = "Black Wins!!!!";
                            DrawBoard();
                            turn = -2;
                            return;
                        }
                        DrawBoard();
                        turn = 1;
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
                if (gamemode == 2)
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





        private void Start1PGame_Click(object sender, EventArgs e)
        {
            DrawBoard();
        }

        private void Start2PGame_Click(object sender, EventArgs e)
        {
            gamemode = 2;
            DrawBoard();
            Messages.Text = "You are the Black Piece";
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
                Messages.Text = "Black Wins!!!!";
                DrawBoard();
                turn = -1;
                return;
            }
            //Initialize2PGame(Positions);
            DrawBoard();
            turn = 2;
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
                turn = -2;
                return;
            }
            //Initialize2PGame2(Positions);
            DrawBoard();
            turn = 1;
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


    }
}

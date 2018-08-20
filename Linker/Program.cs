using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;
namespace Linker
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        private void DrawBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var square = Board.ReadSquare(col, row);

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
                            DrawStar(col, row, blackPen, redBrush);
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


        private void DrawStar(int col, int row, Pen pen, Brush fill)
        {
            g.DrawPolygon(pen, Starpoints(col * squareSize + 10, row * squareSize + 20));
            if (fill != null)
            {
                g.FillPolygon(fill, Starpoints(col * squareSize + 10, row * squareSize + 20));
            }
        }

        private Point[] Starpoints(int col, int row)
        {
            var p = new Point[8];
            p[0] = new Point(col, row);
            p[1] = new Point(col + 8, row - 2);
            p[2] = new Point(col + 10, row - 10);
            p[3] = new Point(col + 12, row - 2);
            p[4] = new Point(col + 20, row);
            p[5] = new Point(col + 12, row + 2);
            p[6] = new Point(col + 10, row + 10);
            p[7] = new Point(col + 8, row + 2);
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
    }
}

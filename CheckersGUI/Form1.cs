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
        private Brush blackBrush = new SolidBrush(Color.Black);
        const int squareSize = 40;
        const int boardSize = 8;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = Grid.CreateGraphics();
        }

        private void Start1PGame_Click(object sender, EventArgs e)
        {
            DrawBoard();
        }

        private void DrawBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Brush brush = whiteBrush;
                    if (row % 2 == 0 && col % 2 == 0)
                    {
                        DrawSquare(col, row, blackPen, whiteBrush);
                    }

                    if (row % 2 == 0 && col % 2 == 1)
                    {
                        DrawSquare(col, row, blackPen, blackBrush);
                    }

                    if (row % 2 == 1 && col % 2 == 1)
                    {
                        DrawSquare(col, row, blackPen, whiteBrush);
                    }

                    if (row % 2 == 1 && col % 2 == 0)
                    {
                        DrawSquare(col, row, blackPen, blackBrush);
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


    }
}

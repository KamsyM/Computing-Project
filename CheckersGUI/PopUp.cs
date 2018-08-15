using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkers.DataFixture;
using Checkers.Model;
using CheckersGUI;

namespace CheckersGUI
{
    public partial class PopUp : Form
    {
        private Form1 iniForm;
        //private Bitmap BlackWins = new Bitmap(@"C:\Users\Kamsi\Pictures\Black Checker Piece.jpg");
        //private Bitmap WhiteWins = new Bitmap(@"C:\Users\Kamsi\Pictures\White Checker Piece.bmp");
        //private Image stuff = new Image();
        public PopUp(string txt, Bitmap image)
        {
            InitializeComponent();
            //WinningText.BackColor = Color.Transparent;
            WinningText.Text = txt;
            WinningPic.Image = image;
        }



    }
}

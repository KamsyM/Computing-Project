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
       // private Bitmap Black Wins;
        public PopUp(string txt)
        {
            InitializeComponent();
            WinningText.Text = txt;
            //WinningPic.Image = new Bitmap();
        }



    }
}

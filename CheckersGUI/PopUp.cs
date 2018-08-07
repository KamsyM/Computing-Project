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
        public SquareValues PType;
        public PopUp()
        {
            InitializeComponent();
            PType = SquareValues.Empty;
        }

        private void PlayerBlack_Click(object sender, EventArgs e)
        {
            PType = SquareValues.Black;
            this.Close();
        }

        private void PlayerWhite_Click(object sender, EventArgs e)
        {
            PType = SquareValues.White;
            this.Close();
        }

        //public SquareValues PType
        //{
        //    get
        //    {
        //        return PType;
        //    }
        //    set
        //    {
        //        PType = value;
        //    }
        //}
    }
}

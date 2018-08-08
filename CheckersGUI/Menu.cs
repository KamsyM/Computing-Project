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
    public partial class Menu : Form
    {
        public SquareValues PType;
        public int difficulty;
        public int gamemode;
        public string name1P;
        public string name2P1;
        public string name2P2;


        public Menu()
        {
            InitializeComponent();


        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            name1P = Name1p.Text;
            name2P1 = Name2P1.Text;
            name2P2 = Name2P2.Text;
            if (Gamemode.SelectedTab == Tab1P)
            {
                gamemode = 0;
            }
            if (Gamemode.SelectedTab == Tab2P)
            {
                gamemode = 1;
            }
            switch (Convert.ToString(PlayerPieceType.SelectedItem))
            {
                case "Black":
                    PType = SquareValues.Black;
                    break;
                case "White":
                    PType = SquareValues.White;
                    break;
                default:
                    PType = SquareValues.Black;
                    break;
            }

            switch (Convert.ToString(Difficulty.SelectedItem))
            {
                case "Beginner":
                    difficulty = 1;
                    break;
                case "Intermediate":
                    difficulty = 2;
                    break;
                case "Advanced":
                    difficulty = 3;
                    break;
                default:
                    difficulty = 1;
                    break;
            }
        }
    }
}

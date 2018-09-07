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
       //private Form1 IniForm;
        public SquareValues PType;
        public int difficulty;
        public int gamemode;
        public bool highlight;
        public string name1P;
        public string name2P1;
        public string name2P2;
        //public List<BotPlayer> Bots = new List<BotPlayer>();
        //private SquareValues BotType;


        public Menu()
        {
            InitializeComponent();
            
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            name1P = Name1p.Text;
            name2P1 = Name2P1.Text;
            name2P2 = Name2P2.Text;
            highlight = Highlight.Checked;
            if (Gamemode.SelectedTab == Tab1P)
            {
                gamemode = 0;
            }
            if (Gamemode.SelectedTab == Tab2P)
            {
                gamemode = 1;
            }
            switch ((string)PlayerPieceType.SelectedItem)
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


            switch ((string)Difficulty.SelectedItem)
            {
                case "Beginner":
                    //Bots.Add(new BotPlayer1(IniForm.Board,BotType));
                    difficulty = 1;
                    break;
                case "Intermediate":
                   // Bots.Add(new BotPlayer2(IniForm.Board,BotType));
                    difficulty = 2;
                    break;
                case "Advanced":
                    difficulty = 3;
                    break;
                default:
                    difficulty = 2;
                    break;
            }
        }

    }
}

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
using System.Reflection;

namespace CheckersGUI
{
    public partial class Menu : Form
    {
        public SquareValues PType;
        public SquareValues BotType;
        public bool beginner = false;
        public int gamemode;
        public bool highlight;
        public string name1P;
        public string name2P1;
        public string name2P2;
        public string nameCG1;
        public string nameCG2;
        public BotPlayer Bot;
        public BotPlayer Bot1;
        public BotPlayer Bot2;
        public GameBoard Board;
        public List<Type> BotNames = typeof(BotPlayer).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BotPlayer))).ToList();
        //public List<object> instances = BotNames.Select(t => Activator.CreateInstance(t) as t);

        public Menu(GameBoard board)
        {
            Board = board;
            InitializeComponent();        
        }

        public List<BotPlayer> BotList()
        {
            List<BotPlayer> botlist = new List<BotPlayer>();
            botlist.Add(new BotPlayer1(Board, BotType));
            botlist.Add(new BotPlayer2(Board, BotType));
            botlist.Add(new BotPlayer3(Board, BotType));
            botlist.Add(new BotPlayer4(Board, BotType));
            botlist.Add(new BotPlayer5(Board, BotType));
            return botlist;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            name1P = Name1p.Text;
            name2P1 = Name2P1.Text;
            name2P2 = Name2P2.Text;
            nameCG1 = NameCG1.Text;
            nameCG2 = NameCG2.Text;

            highlight = Highlight.Checked;
            if (Gamemode.SelectedTab == Tab1P)
            {
                gamemode = 0;
            }
            if (Gamemode.SelectedTab == Tab2P)
            {
                gamemode = 1;
            }
            if (Gamemode.SelectedTab == TabCG)
            {
                gamemode = 2;
            }
            switch ((string)PlayerPieceType.SelectedItem)
            {
                case "Black":
                    PType = SquareValues.Black;
                    BotType = SquareValues.White;
                    break;
                case "White":
                    PType = SquareValues.White;
                    BotType = SquareValues.Black;
                    break;
                default:
                    PType = SquareValues.Black;
                    BotType = SquareValues.White;
                    break;
            }



            if (Difficulty.SelectedItem != null)
            {
               // Bot = (BotPlayer)Difficulty.SelectedItem;    //This is called casting
                //Bot.Type = BotType;
                Bot = (BotPlayer)Activator.CreateInstance((Type)Difficulty.SelectedItem,Board,BotType);
            }



            if (CG1Diff.SelectedItem != null)
            {

                //Bot1 = (BotPlayer)CG1Diff.SelectedItem;    //This is called casting
                //Bot1.Type = SquareValues.Black;
                Bot1 = (BotPlayer)Activator.CreateInstance((Type)CG1Diff.SelectedItem, Board, SquareValues.Black);

            }

            if (CG2Diff.SelectedItem != null)
            {
                //Bot2 = (BotPlayer)CG2Diff.SelectedItem;    //This is called casting
                //Bot2.Type = SquareValues.White;
                Bot2 = (BotPlayer)Activator.CreateInstance((Type)CG2Diff.SelectedItem, Board, SquareValues.White);
            }

        }

    }
}

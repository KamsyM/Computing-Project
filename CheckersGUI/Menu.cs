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
        public int port;
        public int playspeed;
        public bool highlight;
        public bool joingame;
        public bool hostgame;
        public string name1P;
        public string name2P1;
        public string name2P2;
        public string nameCG1;
        public string nameCG2;
        public string onlineid;
        public string hostid;
        public BotPlayer Bot;
        public BotPlayer Bot1;
        public BotPlayer Bot2;
        public GameBoard Board;
        //public List<Type> BotNames = typeof(BotPlayer).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BotPlayer))).ToList();
        //public List<object> instances = BotNames.Select(t => Activator.CreateInstance(t) as t);

        public Menu(GameBoard board)
        {
            Board = board;
            InitializeComponent();        
        }

        public List<BotPlayer> BotList()
        {
            List<BotPlayer> botlist = new List<BotPlayer>();
            List<Type> BotNames = typeof(BotPlayer).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(BotPlayer))).ToList();
            foreach (var item in BotNames)
            {
                botlist.Add((BotPlayer)Activator.CreateInstance(item, Board, BotType));
            }
            return botlist;
        }

        static string GetDescription(Type type)
        {
            var descriptions = (DescriptionAttribute[])
                type.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptions.Length == 0)
            {
                return null;
            }
            return descriptions[0].Description;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            name1P = Name1p.Text;
            name2P1 = Name2P1.Text;
            name2P2 = Name2P2.Text;
            nameCG1 = NameCG1.Text;
            nameCG2 = NameCG2.Text;
            onlineid = OnlineID.Text;
            hostid = HostID.Text;
            port = Convert.ToInt32(PortNo.Text);
            try
            {
                playspeed = Convert.ToInt32(Convert.ToDouble(PlaySpeed.Text) * 1000);
            }
            catch (Exception)
            {

                playspeed = 800;
            }


            highlight = Highlight.Checked;
            joingame = JoinGame.Checked;
            hostgame = HostGame.Checked;
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
            if (Gamemode.SelectedTab == TabOnline)
            {
                gamemode = 3;
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
            if (gamemode == 3)
            {
                switch ((string)HostPiecetype.SelectedItem)
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
            }


            if (Difficulty.SelectedItem != null)
            {
                Bot = (BotPlayer)Difficulty.SelectedItem;    //This is called casting
                Bot.Type = BotType;
                var BeginnerBot = new BotPlayer1(Board,BotType);
                var b = BeginnerBot.GetType();
                //Bot = (BotPlayer)Activator.CreateInstance((Type)Difficulty.SelectedItem,Board,BotType);
                var c = Bot.GetType();
                if (c == b)
                {
                    beginner = true;
                }
                if (c != b)
                {
                    beginner = false;
                }
            }

            if (Difficulty.SelectedItem == null)
            {
                Bot = new BotPlayer3(Board, BotType);
            }


            if (CG1Diff.SelectedItem != null)
            {

                Bot1 = (BotPlayer)CG1Diff.SelectedItem;    //This is called casting
                Bot1.Type = SquareValues.Black;
                //Bot1 = (BotPlayer)Activator.CreateInstance((Type)CG1Diff.SelectedItem, Board, SquareValues.Black);

            }

            if (CG1Diff.SelectedItem == null)
            {
                Bot1 = new BotPlayer3(Board, SquareValues.Black);
            }

            if (CG2Diff.SelectedItem != null)
            {
                Bot2 = (BotPlayer)CG2Diff.SelectedItem;    //This is called casting
                Bot2.Type = SquareValues.White;
                //Bot2 = (BotPlayer)Activator.CreateInstance((Type)CG2Diff.SelectedItem, Board, SquareValues.White);
            }

            if (CG2Diff.SelectedItem == null)
            {
                Bot2 = new BotPlayer3(Board, SquareValues.White);
            }
        }


        private void JoinGame_CheckStateChanged(object sender, EventArgs e)
        {
            if (JoinGame.Checked)
            {
                HostGame.Checked = false;
                HostLabel.Visible = true;
                HostID.Visible = true;
                HostIpInfo.Visible = true;
                HostPiecetypeLabel.Visible = false;
                HostPiecetype.Visible = false;
            }

            else
            {
                HostGame.Checked = true;
                HostLabel.Visible = false;
                HostID.Visible = false;
                HostIpInfo.Visible = false;
                HostPiecetypeLabel.Visible = true;
                HostPiecetype.Visible = true;
            }
        }

        private void HostGame_CheckStateChanged(object sender, EventArgs e)
        {
            if (HostGame.Checked)
            {
                JoinGame.Checked = false;
            }

            else
            {
                JoinGame.Checked = true;
            }
        }

        private void Advanced_CheckStateChanged(object sender, EventArgs e)
        {
            if (Advanced.Checked)
            {
                PortLabel.Visible = true;
                PortNo.Visible = true;
            }

            else
            {
                PortLabel.Visible = false;
                PortNo.Visible = false;
            }
        }
    }
}

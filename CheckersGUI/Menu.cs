﻿using System;
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
        //public List<BotPlayer> Bots = new List<BotPlayer>();
        //private SquareValues BotType;


        public Menu(GameBoard board)
        {
            InitializeComponent();
            Board = board;
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


            Bot = (BotPlayer)Difficulty.SelectedItem;    //This is called casting
            // Difficulty.Items =

            //switch ((string)Difficulty.SelectedItem)
            //{
            //    case "Beginner":
            //        //Bots.Add(new BotPlayer1(IniForm.Board,BotType));
            //        Bot = new BotPlayer1(Board, BotType);
            //        beginner = true;
            //        break;
            //    case "Intermediate":
            //        // Bots.Add(new BotPlayer2(IniForm.Board,BotType));
            //        Bot = new BotPlayer3(Board, BotType);
            //        break;
            //    case "Advanced":
            //        Bot = new BotPlayer5(Board, BotType);
            //        break;
            //    default:
            //        Bot = new BotPlayer3(Board, BotType);
            //        break;
            //}

            switch ((string)CG1Diff.SelectedItem)
            {
                case "Beginner":
                    //Bots.Add(new BotPlayer1(IniForm.Board,BotType));
                    Bot1 = new BotPlayer1(Board, SquareValues.Black);
                    break;
                case "Intermediate":
                    // Bots.Add(new BotPlayer2(IniForm.Board,BotType));
                    Bot1 = new BotPlayer3(Board, SquareValues.Black);
                    break;
                case "Advanced":
                    Bot1 = new BotPlayer5(Board, SquareValues.Black);
                    break;
                default:
                    Bot1 = new BotPlayer3(Board, SquareValues.Black);
                    break;
            }

            switch ((string)CG2Diff.SelectedItem)
            {
                case "Beginner":
                    //Bots.Add(new BotPlayer1(IniForm.Board,BotType));
                    Bot2 = new BotPlayer1(Board, SquareValues.White);
                    break;
                case "Intermediate":
                    // Bots.Add(new BotPlayer2(IniForm.Board,BotType));
                    Bot2 = new BotPlayer3(Board, SquareValues.White);
                    break;
                case "Advanced":
                    Bot2 = new BotPlayer5(Board, SquareValues.White);
                    break;
                default:
                    Bot2 = new BotPlayer3(Board, SquareValues.White);
                    break;
            }
        }

    }
}

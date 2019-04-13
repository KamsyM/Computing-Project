using System;
using System.Collections.Generic;
using Checkers.DataFixture;
using Checkers.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public Random rnd = new Random();
        public List<SquareValues> Types = new List<SquareValues>() { SquareValues.Black, SquareValues.White };
        [TestMethod]
        public void IntermediateBeatsBeginner()
        {
            int BotWins = 0;
            int Bot2Wins = 0;
            SquareValues winner = 0;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            var Board = new GameBoard(8, blackpieces, whitepieces);
            var Bot = new BotPlayer3(Board, Types[rnd.Next(Types.Count)]);
            var Bot2 = new BotPlayer1(Board, Board.OpponentType(Bot.Type));
            //var Bot = new BotPlayerTempV(Board, SquareValues.Black, 3);
            //var Bot2 = new BotPlayerTempV(Board, SquareValues.White, 2);
            for (int i = 0; i < 100; i++)
            {
                winner = BotPlayer.PlayBots(Board, Bot, Bot2);
                if (winner == Bot.Type)
                {
                    BotWins++;
                }
                else
                {
                    Bot2Wins++;
                }
            }
            Assert.IsTrue(BotWins > Bot2Wins);

        }

        [TestMethod]
        public void AdvancedBeatsIntermediate()
        {
            int BotWins = 0;
            int Bot2Wins = 0;
            SquareValues winner = 0;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            var Board = new GameBoard(8, blackpieces, whitepieces);
            var Bot = new BotPlayer7(Board, Types[rnd.Next(Types.Count)]);
            var Bot2 = new BotPlayer5(Board, Board.OpponentType(Bot.Type));
            //var Bot = new BotPlayerTempV(Board, SquareValues.Black, 3);
            //var Bot2 = new BotPlayerTempV(Board, SquareValues.White, 2);
            for (int i = 0; i < 100; i++)
            {
                winner = BotPlayer.PlayBots(Board, Bot, Bot2);
                if (winner == Bot.Type)
                {
                    BotWins++;
                }
                else
                {
                    Bot2Wins++;
                }
            }
            Assert.IsTrue(BotWins > Bot2Wins);
        }

        [TestMethod]
        public void AdvancedVsAdvanced()
        {
            int BotWins = 0;
            int Bot2Wins = 0;
            SquareValues winner = 0;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            var Board = new GameBoard(8, blackpieces, whitepieces);
            var Bot = new BotPlayer5(Board, Types[rnd.Next(Types.Count)]);
            var Bot2 = new BotPlayer5(Board, Board.OpponentType(Bot.Type));
            //var Bot = new BotPlayerTempV(Board, SquareValues.Black, 3);
            //var Bot2 = new BotPlayerTempV(Board, SquareValues.White, 2);
            for (int i = 0; i < 100; i++)
            {
                winner = BotPlayer.PlayBots(Board, Bot, Bot2);
                if (winner == Bot.Type)
                {
                    BotWins++;
                }
                else
                {
                    Bot2Wins++;
                }
            }
            Assert.IsFalse(Math.Abs(BotWins - Bot2Wins) > 20);
        }
    }
}

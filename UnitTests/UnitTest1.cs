using System;
using Checkers.DataFixture;
using Checkers.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IntermediateBeatsBeginner()
        {
            int BlackWins = 0;
            int WhiteWins = 0;
            SquareValues winner = 0;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            var Board = new GameBoard(8, blackpieces, whitepieces);
            var Bot = new BotPlayer3(Board, SquareValues.Black);
            var Bot2 = new BotPlayer1(Board, SquareValues.White);
            for (int i = 0; i < 100; i++)
            {
                winner = BotPlayer.PlayBots(Board, Bot, Bot2);
                switch (winner)
                {
                    case SquareValues.Black:
                        BlackWins++;
                        break;
                    case SquareValues.White:
                        WhiteWins++;
                        break;
                    default:
                        break;
                }
            }
            Assert.IsTrue(BlackWins>WhiteWins);
      
        }

        //[TestMethod]
        //public void AdvancedBeatsIntermediate()
        //{
        //    int BlackWins = 0;
        //    int WhiteWins = 0;
        //    int winner = 0;
        //    var blackpieces = Pieces.BlackPlacements();
        //    var whitepieces = Pieces.WhitePlacements();
        //    var Board = new GameBoard(8, blackpieces, whitepieces);
        //    var Bot = new BotPlayer5(Board, SquareValues.Black);
        //    var Bot2 = new BotPlayer3(Board, SquareValues.White);
        //    //var Bot = new BotPlayerTempV(Board, SquareValues.Black, 3);
        //    //var Bot2 = new BotPlayerTempV(Board, SquareValues.White, 2);
        //    for (int i = 0; i < 100; i++)
        //    {
        //        winner = BotPlayer.PlayBots(Board, Bot, Bot2);
        //        switch (winner)
        //        {
        //            case 1:
        //                BlackWins++;
        //                break;
        //            case 2:
        //                WhiteWins++;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    Assert.IsTrue(BlackWins > WhiteWins);
        //}

        [TestMethod]
        public void AdvancedVsAdvanced()
        {
            int BlackWins = 0;
            int WhiteWins = 0;
            SquareValues winner = 0;
            var blackpieces = Pieces.BlackPlacements();
            var whitepieces = Pieces.WhitePlacements();
            var Board = new GameBoard(8, blackpieces, whitepieces);
            var Bot = new BotPlayer5(Board, SquareValues.Black);
            var Bot2 = new BotPlayer5(Board, SquareValues.White);
            for (int i = 0; i < 100; i++)
            {
                winner = BotPlayer.PlayBots(Board, Bot, Bot2);
                switch (winner)
                {
                    case SquareValues.Black:
                        BlackWins++;
                        break;
                    case SquareValues.White:
                        WhiteWins++;
                        break;
                    default:
                        break;
                }
            }
            Assert.IsFalse(Math.Abs(BlackWins - WhiteWins) > 20);
        }
    }
}

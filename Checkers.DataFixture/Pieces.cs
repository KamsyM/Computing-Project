using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Model;

namespace Checkers.DataFixture
{
    public static class Pieces
    {
        public static Piece[] BlackPlacements()
        {
            var pieces = new Piece[12];
            pieces[0] = new Piece(0,7,SquareValues.Black);
            pieces[1] = new Piece(2, 7, SquareValues.Black);
            pieces[2] = new Piece(4, 7, SquareValues.Black);
            pieces[3] = new Piece(6, 7, SquareValues.Black);
            pieces[4] = new Piece(1, 6, SquareValues.Black);
            pieces[5] = new Piece(3, 6, SquareValues.Black);
            pieces[6] = new Piece(5, 6, SquareValues.Black);
            pieces[7] = new Piece(7, 6, SquareValues.Black);
            pieces[8] = new Piece(0, 5, SquareValues.Black);
            pieces[9] = new Piece(2, 5, SquareValues.Black);
            pieces[10] = new Piece(4, 5, SquareValues.Black);
            pieces[11] = new Piece(6, 5, SquareValues.Black);
            return pieces;
        }

        public static Piece[] WhitePlacements()
        {
            var pieces = new Piece[12];
            pieces[0] = new Piece(1, 0, SquareValues.White);
            pieces[1] = new Piece(3, 0, SquareValues.White);
            pieces[2] = new Piece(5, 0, SquareValues.White);
            pieces[3] = new Piece(7, 0, SquareValues.White);
            pieces[4] = new Piece(0, 1, SquareValues.White);
            pieces[5] = new Piece(2, 1, SquareValues.White);
            pieces[6] = new Piece(4, 1, SquareValues.White);
            pieces[7] = new Piece(6, 1, SquareValues.White);
            pieces[8] = new Piece(1, 2, SquareValues.White);
            pieces[9] = new Piece(3, 2, SquareValues.White);
            pieces[10] = new Piece(5, 2, SquareValues.White);
            pieces[11] = new Piece(7, 2, SquareValues.White);
            return pieces;
        }

        public static Piece[] TestBlack()
        {
            var pieces = new Piece[1];
            pieces[0] = new Piece(0,5, SquareValues.Black);
            return pieces;
        }

        public static Piece[] TestWhite()
        {
            var pieces = new Piece[1];
            pieces[0] = new Piece(0, 1, SquareValues.White);
            return pieces;
        }

        public static Piece[] DoubleJumpWhite()
        {
            var pieces = new Piece[3];
            pieces[0] = new Piece(1, 4, SquareValues.White);
            pieces[1] = new Piece(3, 2, SquareValues.White);
            pieces[2] = new Piece(1, 2, SquareValues.White);
            return pieces;
        }

        public static Piece[] DoubleJumpBlack()
        {
            var pieces = new Piece[3];
            pieces[0] = new Piece(1, 4, SquareValues.Black);
            pieces[1] = new Piece(1, 2, SquareValues.Black);
            pieces[2] = new Piece(4, 5, SquareValues.Black);
            return pieces;
        }
    }
}

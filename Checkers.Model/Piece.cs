using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Piece
    {
        public int startRow;
        public int startCol;
        public SquareValues PieceType; 

        public Piece(int col = 0, int row = 0, SquareValues piecetype = 0)
        {
            SetPosition(col, row, piecetype);
        }

        public void SetPosition(int col, int row, SquareValues piecetype)
        {
            startCol = col;
            startRow = row;
            PieceType = piecetype;
        }
    }
}

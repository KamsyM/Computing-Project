using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Move
    {
        public string[] ColAlphabet = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H" };
        public SquareValues Type;
        public Modality Mode;
        public int IniCol;
        public int IniRow;
        public int FinCol;
        public int FinRow;

        public Move(Modality mode, SquareValues type, int inicol, int inirow, int fincol, int finrow)
        {
            Mode = mode;
            Type = type;
            IniCol = inicol;
            IniRow = inirow;
            FinCol = fincol;
            FinRow = finrow;
        }

        public override string ToString()
        {
            return Type.ToString() + " (" + ColAlphabet[IniCol] + "," + (IniRow + 1) + ") : (" + ColAlphabet[FinCol] + "," + (FinRow + 1) + ")";
        }
    }
}

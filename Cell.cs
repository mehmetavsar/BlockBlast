using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlast
{
    internal class Cell(int row, int col)
    {
        public CellState State { get; set; } = CellState.Empty;
        public int Row { get; } = row;
        public int Col { get; } = col;

        public bool SetTrial()
        {
            if (this.State == CellState.Empty) 
                this.State = CellState.Trial;
            else throw new Exception($"Cell was already {this.State}");
            
            return true;
        }
    }

    public enum CellState
    {
        Empty = 0,
        Trial = 1,
        Filled = 2
    }
}

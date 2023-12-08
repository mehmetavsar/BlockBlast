using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlast
{
    internal class Grid 
    {
        public Grid(int rows, int cols) 
        {
            Rows = rows;
            Cols = cols;

            Cells = [];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    Cells.Add(new Cell(r, c) { State = CellState.Empty });
        }

        public void SetRow(int row, string cols)
        {
            if (cols.Length != this.Cols)
                throw new Exception("Wrong number of columns");

            int col = 0;
            foreach(char c in cols.ToCharArray())
            {
                this[row, col].State = (c == '1' ? CellState.Filled : CellState.Empty);
                col++;
            }
        }

        private readonly List<Cell> Cells;

        public Cell this[int row, int col] => Cells.Where(c => c.Row == row && c.Col == col).FirstOrDefault();

        public int Rows { get; }
        public int Cols { get; }

        public void ClearTrials()
        {
            foreach (Cell cell in Cells)
                if (cell.State == CellState.Trial)
                    cell.State = CellState.Empty;
        }

        public Match? Match(Grid piece, Position position)
        {
            try
            {
                for (int i = 0; i < piece.Rows; i++)
                {
                    for (int j = 0; j < piece.Cols; j++)
                    {
                        if (piece[i, j].State == CellState.Filled)
                        {
                            this[i + position.Row, j + position.Col].SetTrial();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            Match match = new()
            {
                Piece = piece,
                Position = position
            };

            (int Rows, int Cols) blasted_count = (0,0);
            
            bool blasted;
            int col; int row;

            row = position.Row;
            while (row < position.Row + piece.Rows)
            {
                blasted = true;
                col = 0;
                
                while (blasted && col < this.Cols)
                {
                    if (this[row, col].State == CellState.Empty)
                        blasted = false;

                    col++;
                }

                if (blasted)
                    blasted_count.Rows++;

                row++;
            }

            col = position.Col;
            while (col < position.Col + piece.Cols)
            {
                blasted = true;
                row = 0;
                
                while (blasted && row < this.Rows)
                {
                    if (this[row, col].State == CellState.Empty)
                        blasted = false;
                    row++;
                }

                if (blasted)
                    blasted_count.Cols++;

                col++;
            }

            match.Score = blasted_count.Rows + blasted_count.Cols;

            ClearTrials();

            return match;
        }

    }
}

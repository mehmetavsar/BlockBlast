using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;

namespace BlockBlast
{
    internal class Grid
    {
        public Grid(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;

            data = new Cell[rows,cols];
            for (int r =0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    data[r,c] = new Cell() { State = CellState.Empty};
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

        Cell[,] data;

        public Cell this[int r, int c]
        {
            get => data[r,c];
            set => data[r, c] = value;
        }

        public int Rows { get; }
        public int Cols { get; }


        List<Position> trials = [];
        public void SetTrial(Position pos)
        {
            Cell cell = this[pos.Row, pos.Col];
            if (cell.State == CellState.Empty)
                cell.State = CellState.Trial;
            else throw new Exception($"Cell {pos.Row},{pos.Col} is already {cell.State}");
        }

        public void ClearTrials()
        {
            trials = [];
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

            return match;
        }
    }
}

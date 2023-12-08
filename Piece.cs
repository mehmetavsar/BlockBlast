namespace BlockBlast
{
    internal static class Piece
    {
        private static readonly Dictionary<string, Grid> Pieces = [];

        private static Grid GetByKey(string key) => Pieces[key];

        private static void ParsePiece(string key, List<string> rows)
        {
            if (rows.Count == 0) return;
            if (rows.GroupBy(r => r.Length, r=>r).Distinct().Count() > 1) return;

            int col_count = rows[0].Length;

            Grid piece = new(rows.Count, col_count);
            int row_index = 0;
            foreach(string row in rows)
            {
                piece.SetRow(row_index, row);
                row_index++;
            }

            Pieces.Add(key, piece);
        }

        internal async static Task ReadPieces()
        {
            string piece_key = null;
            List<string> piece_rows = [];
            
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pieces.txt");

            string[] lines = await File.ReadAllLinesAsync(path);
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                if (line[0]=='#')
                {
                    ParsePiece(piece_key, piece_rows);
                    piece_key = line[1..];
                    piece_rows = [];
                }
                else piece_rows.Add(line);
            }

            if (piece_rows.Any())
                ParsePiece(piece_key, piece_rows);
        }
    }
}

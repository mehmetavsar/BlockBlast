namespace BlockBlast
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

        

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            await Piece.ReadPieces();

            Grid grid = new(8, 8);
            grid.SetRow(0, "00000000");
            grid.SetRow(1, "00000000");
            grid.SetRow(2, "11101111");
            grid.SetRow(3, "11101111");
            grid.SetRow(4, "11100011");
            grid.SetRow(5, "00000000");
            grid.SetRow(6, "00000000");
            grid.SetRow(7, "00000000");


            Grid piece = new(3, 3);
            piece.SetRow(0, "100");
            piece.SetRow(1, "100");
            piece.SetRow(2, "111");

            Match x = grid.Match(piece, new(2, 3)); // must match with 3 horizontal 

            piece = new(3, 3);
            piece.SetRow(0, "111");
            piece.SetRow(1, "111");
            piece.SetRow(2, "111");

            x = grid.Match(piece, new(2, 0)); // must not match
            x = grid.Match(piece, new(5, 0)); // must match with score 0
        }
    }
}
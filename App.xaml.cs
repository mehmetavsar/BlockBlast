namespace BlockBlast
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Grid grid = new(8, 8);
            grid.SetRow(0,"00000000");
            grid.SetRow(1,"00000000");
            grid.SetRow(2,"11101111");
            grid.SetRow(3,"11101111");
            grid.SetRow(4,"11100011");
            grid.SetRow(5,"00000000");
            grid.SetRow(6,"00000000");
            grid.SetRow(7,"00000000");


            Grid piece = new(3, 3);
            piece.SetRow(0,"100");
            piece.SetRow(1,"100");
            piece.SetRow(2,"111");

            Match x = grid.Match(piece, new(2, 3));

            MainPage = new AppShell();
        }
    }
}
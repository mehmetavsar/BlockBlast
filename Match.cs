using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBlast
{
    internal class Match
    {
        public decimal Score = 0;
        public Position Position { get; set; }
        public Grid Piece { get; set; }
    }
}

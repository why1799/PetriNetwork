using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri_Network
{
    public class Place
    {
        public int X { get; set; }
        public int Y { get; set; }
        public uint Amount { get; set; }

        public Place()
        {
        }

        public Place(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

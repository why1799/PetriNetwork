using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri_Network
{
    public class Bridge
    {
        public int X { get; set; }
        public int Y { get; set; }
        public uint Amount { get; set; }

        public Bridge()
        {
        }

        public Bridge(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

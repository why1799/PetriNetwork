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
        public uint Await { get; set; }

        public Place()
        {
        }

        public Place(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Place left, Place right)
        {
            return left?.X == right?.X && left?.Y == right?.Y && left?.Amount == right?.Amount && left?.Await == right?.Await;
        }

        public static bool operator !=(Place left, Place right)
        {
            return left == right;
        }
    }
}

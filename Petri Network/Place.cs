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

        public override bool Equals(object obj)
        {
            return this == obj as Place;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public static bool operator ==(Place left, Place right)
        {
            if (Equals(left, null) == true && Equals(right, null) == true)
            {
                return true;
            }
            if (Equals(left, null) == true && Equals(right, null) == false ||
                Equals(left, null) == false && Equals(right, null) == true)
                return false;
            return left.X == right.X
                && left.Y == right.Y
                && left.Amount == right.Amount
                && left.Await == right.Await;
        }
        public static bool operator !=(Place left, Place right)
        {
            return !(left == right);
        }
    }
}

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

        public Bridge()
        {
        }

        public Bridge(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return this == obj as Bridge;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public static bool operator ==(Bridge left, Bridge right)
        {
            if (Equals(left, null) == true && Equals(right, null) == true)
            {
                return true;
            }
            if (Equals(left, null) == true && Equals(right, null) == false ||
                Equals(left, null) == false && Equals(right, null) == true)
                return false;
            return left.X == right.X
                && left.Y == right.Y;
        }
        public static bool operator !=(Bridge left, Bridge right)
        {
            return !(left == right);
        }
    }
}

﻿using System;
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

        public static bool operator == (Bridge left, Bridge right)
        {
            return left?.X == right?.X && left?.Y == right?.Y;
        }

        public static bool operator !=(Bridge left, Bridge right)
        {
            return left == right;
        }
    }
}

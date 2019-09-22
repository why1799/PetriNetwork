using System;
using System.Drawing;
using System.Windows.Forms;

namespace Petri_Network
{
    class MyPanel : Panel
    {
        public MyPanel()
        {
            this.DoubleBuffered = true;
            this.AutoScroll = true;
        }

        public void SetAutoScroll(int x, int y)
        {
            this.AutoScrollMinSize = new Size(x, y);
        }
    }
}
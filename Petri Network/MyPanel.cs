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
            this.AutoScrollMinSize = new Size(2000, 2000);
        }
    }
}
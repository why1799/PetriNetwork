using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri_Network
{
    public enum State { Normal, DragAndDrop, AddPlace, AddBridge, AddLink}

    public partial class Form1 : Form
    {
        List<Place> places;
        List<Bridge> bridges;
        List<Link> links;
        State now;
        Cursor cursor;

        private const int placesradius = 20;
        private const int amountradius = 8;

        private const int bridgeswidth = 16;
        private const int bridgesheight = 60;

        private int gridradius;

        private bool settinglinkplace;
        private bool settinglinkbridge;
        private int settinglinkx;
        private int settinglinky;

        private bool settinglinknowplace;
        private bool settinglinknowbridge;
        private int settinglinknowx;
        private int settinglinknowy;

        private int mousex;
        private int mousey;

        public Form1()
        {
            InitializeComponent();

            places = new List<Place>();
            bridges = new List<Bridge>();
            links = new List<Link>();
            now = State.AddLink;
            
            обычныйToolStripMenuItem.Checked = true;

            gridradius = 2 * placesradius + placesradius / 4;
            settinglinkplace = false;
            settinglinkbridge = false;
            settinglinknowplace = false;
            settinglinknowbridge = false;
        }

        private void func()
        {
            
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void RefreshStates()
        {
            обычныйToolStripMenuItem.Checked = false;
            перенестиToolStripMenuItem.Checked = false;
            добавлениеДугиToolStripMenuItem.Checked = false;
            добавлениеПереходаToolStripMenuItem.Checked = false;
            добавлениеПозицииToolStripMenuItem.Checked = false;
            now = State.Normal;
        }

        private void обычныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshStates();
            обычныйToolStripMenuItem.Checked = true;
            now = State.Normal;
        }

        private void перенестиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshStates();
            перенестиToolStripMenuItem.Checked = true;
            now = State.DragAndDrop;
        }

        private void добавлениеПереходаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshStates();
            добавлениеПереходаToolStripMenuItem.Checked = true;
            now = State.AddBridge;
        }

        private void добавлениеПозицииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshStates();
            добавлениеПозицииToolStripMenuItem.Checked = true;
            now = State.AddPlace;
        }

        private void добавлениеДугиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshStates();
            добавлениеДугиToolStripMenuItem.Checked = true;
            now = State.AddLink;
        }

        private void DrawGrid(Graphics g, int width, int height)
        {
            g.Clear(Color.White);//Фон
            using (Pen pen = new Pen(Color.Gray, 2))
            {
                //Горизонтальные линии
                for (int i = gridradius / 2; i < width; i += gridradius)
                {
                    g.DrawLine(pen, i, 0, i, height);
                }
                //Вертикальные линии
                for (int i = gridradius / 2; i < height; i += gridradius)
                {
                    g.DrawLine(pen, 0, i, width, i);
                }
            }
        }

        private void DrawPlaces(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            Brush brushes = Brushes.Black;

            foreach (var place in places)
            {
                var diameter = placesradius * 2;
                

                if((settinglinkplace && settinglinkx == place.X && settinglinky == place.Y) ||
                    (settinglinknowplace && settinglinknowx == place.X && settinglinknowy == place.Y))
                {
                    pen = new Pen(Color.OrangeRed, 2);
                    brushes = Brushes.OrangeRed;
                }
                else
                {
                    pen = new Pen(Color.Black, 2);
                    brushes = Brushes.Black;
                }
                g.DrawEllipse(pen, new RectangleF(place.X - placesradius, place.Y - placesradius, diameter, diameter));
                SolidBrush solidbrush = new SolidBrush(pen.Color);
                
                if (place.Amount == 1)
                {
                    var amountdiameter = amountradius * 2;
                    g.FillEllipse(solidbrush, place.X - amountradius, place.Y - amountradius, amountdiameter, amountdiameter);
                }
                else if (place.Amount == 2)
                {
                    var amountdiameter = amountradius * 2;
                    g.FillEllipse(solidbrush, place.X - amountradius - (int)(placesradius / 2.5), place.Y - amountradius, amountdiameter, amountdiameter);
                    g.FillEllipse(solidbrush, place.X - amountradius + (int)(placesradius / 2.5), place.Y - amountradius, amountdiameter, amountdiameter);
                }
                else if (place.Amount == 3)
                {
                    var amountdiameter = amountradius * 2;
                    g.FillEllipse(solidbrush, place.X - amountradius, place.Y - amountradius - placesradius / 2, amountdiameter, amountdiameter);
                    g.FillEllipse(solidbrush, place.X - amountradius - (int)(placesradius / 2.5), place.Y - amountradius + placesradius / 3, amountdiameter, amountdiameter);
                    g.FillEllipse(solidbrush, place.X - amountradius + (int)(placesradius / 2.5), place.Y - amountradius + placesradius / 3, amountdiameter, amountdiameter);
                }
                else if (place.Amount > 999)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Amount.ToString().Substring(0, 2) + "...", myFont, brushes, new Point(place.X - 3 * size / 2, place.Y - (int)(size / 1.5)));
                }
                else if (place.Amount > 99)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Amount.ToString(), myFont, brushes, new Point(place.X - 3 * size / 2, place.Y - (int)(size / 1.5)));
                }
                else if (place.Amount > 9)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Amount.ToString(), myFont, brushes, new Point(place.X - size, place.Y - (int)(size / 1.5)));
                }
                else if (place.Amount > 3)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Amount.ToString(), myFont, brushes, new Point(place.X - size / 2, place.Y - (int)(size / 1.5)));
                }
            }
        }

        private void DrawBridges(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            SolidBrush brush = new SolidBrush(Color.Black);

            foreach (var bridge in bridges)
            {
                if ((settinglinkbridge && settinglinkx == bridge.X && settinglinky == bridge.Y) ||
                     (settinglinknowbridge && settinglinknowx == bridge.X && settinglinknowy == bridge.Y))
                {
                    pen = new Pen(Color.OrangeRed, 2);
                }
                else
                {
                    pen = new Pen(Color.Black, 2);
                }
                g.DrawRectangle(pen, new Rectangle(bridge.X - bridgeswidth / 2, bridge.Y - bridgesheight / 2, bridgeswidth, bridgesheight));
            }
        }

        private void DrawLinks(Graphics g)
        {
            int startx, endx, starty, endy;
            Pen p = new Pen(Color.GreenYellow, 2);
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            foreach (var link in links)
            {
                var place = GetXYPlace(link.Place.X, link.Place.Y, link.Bridge.X, link.Bridge.Y);
                var bridge = GetXYBridge(link.Bridge.X, link.Bridge.Y, link.Place.X, link.Place.Y);
                if (link.FromPlace)
                {
                    startx = place.Value.X;
                    starty = place.Value.Y;
                    endx = bridge.Value.X;
                    endy = bridge.Value.Y;
                }
                else
                {
                    startx = bridge.Value.X;
                    starty = bridge.Value.Y;
                    endx = place.Value.X;
                    endy = place.Value.Y;
                }
                g.DrawLine(p, startx, starty, endx, endy);
            }
        }

        private Point? GetXYPlace(int x1, int y1, int x2, int y2)
        {
            if(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)) < placesradius)
            {
                return null;
            }

            double z = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            double cx = -Math.Abs(x1 - x2);
            double cy = -Math.Abs(y2 - y1);
            Point point1 = new Point((int)(Math.Abs(x1 - x2) * placesradius / z) + x1, (int)(Math.Abs(y2 - y1) * placesradius / z) + y1);
            Point point2 = new Point((int)(-Math.Abs(x1 - x2) * placesradius / z) + x1, (int)(Math.Abs(y2 - y1) * placesradius / z) + y1);
            Point point3 = new Point((int)(Math.Abs(x1 - x2) * placesradius / z) + x1, (int)(-Math.Abs(y2 - y1) * placesradius / z) + y1);
            Point point4 = new Point((int)(-Math.Abs(x1 - x2) * placesradius / z) + x1, (int)(-Math.Abs(y2 - y1) * placesradius / z) + y1);

            Point nowpoint = point1;
            double distance = Math.Sqrt((nowpoint.X - x2) * (nowpoint.X - x2) + (nowpoint.Y - y2) * (nowpoint.Y - y2));

            double tempdistance = Math.Sqrt((point2.X - x2) * (point2.X - x2) + (point2.Y - y2) * (point2.Y - y2));
            if (tempdistance < distance)
            {
                distance = tempdistance;
                nowpoint = point2;
            }

            tempdistance = Math.Sqrt((point3.X - x2) * (point3.X - x2) + (point3.Y - y2) * (point3.Y - y2));
            if (tempdistance < distance)
            {
                distance = tempdistance;
                nowpoint = point3;
            }

            tempdistance = Math.Sqrt((point4.X - x2) * (point4.X - x2) + (point4.Y - y2) * (point4.Y - y2));
            if (tempdistance < distance)
            {
                distance = tempdistance;
                nowpoint = point4;
            }

            return nowpoint;
        }

        private Point? GetXYBridge(int x1, int y1, int x2, int y2)
        {
            int xx1 = x1 - bridgeswidth / 2;
            int yy1 = y1 - bridgesheight / 2;
            int xx2 = x1 + bridgeswidth / 2;
            int yy2 = y1 + bridgesheight / 2;
            if (x2 >= xx1 && x2 <= xx2 && y2 >= yy1 && y2 <= yy2)
            {
                return null;
            }
            
            int y;
            int x;
            int a = y2 - y1;
            int b = x2 - x1;
            int c = y1*(x2 - x1) - x1*(y2 - y1);
            Point? nowpoint;
            Point? point1;
            if (a == 0)
            {
                point1 = null;
            }
            else
            {
                y = yy1;
                x = (b * y - c) / a;
                point1 = new Point(x, y);
                if (x > xx2 || x < xx1)
                {
                    point1 = null;
                }
            }

            nowpoint = point1;
            double distance = int.MaxValue;
            if(nowpoint != null)
            {
                distance = Math.Sqrt((nowpoint.Value.X - x2) * (nowpoint.Value.X - x2) + (nowpoint.Value.Y - y2) * (nowpoint.Value.Y - y2));
            }

            Point? point2;
            if(b == 0)
            {
                point2 = null;
            }
            else
            {
                x = xx1;
                y = (- c - a * x) / -b;
                point2 = new Point(x, y);
                if (y > yy2 || y < yy1)
                {
                    point2 = null;
                }
            }

            if(point2 != null)
            {
                var tempdistance = Math.Sqrt((point2.Value.X - x2) * (point2.Value.X - x2) + (point2.Value.Y - y2) * (point2.Value.Y - y2));
                if (tempdistance < distance)
                {
                    distance = tempdistance;
                    nowpoint = point2;
                }
            }

            Point? point3;
            if (a == 0)
            {
                point3 = null;
            }
            else
            {
                y = yy2;
                x = (b * y - c) / a;
                point3 = new Point(x, y);
                if (x > xx2 || x < xx1)
                {
                    point3 = null;
                }
            }

            if (point3 != null)
            {
                var tempdistance = Math.Sqrt((point3.Value.X - x2) * (point3.Value.X - x2) + (point3.Value.Y - y2) * (point3.Value.Y - y2));
                if (tempdistance < distance)
                {
                    distance = tempdistance;
                    nowpoint = point3;
                }
            }

            Point? point4;
            if (b == 0)
            {
                point4 = null;
            }
            else
            {
                x = xx2;
                y = (-c - a * x) / -b;
                point4 = new Point(x, y);
                if (y > yy2 || y < yy1)
                {
                    point4 = null;
                }
            }

            if (point4 != null)
            {
                var tempdistance = Math.Sqrt((point4.Value.X - x2) * (point4.Value.X - x2) + (point4.Value.Y - y2) * (point4.Value.Y - y2));
                if (tempdistance < distance)
                {
                    distance = tempdistance;
                    nowpoint = point4;
                }
            }


            return nowpoint;
        }

        private void DrawTempLink(Graphics g)
        {
            Pen p = new Pen(Color.OrangeRed, 2);
            p.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            int startx, endx, starty, endy;


            if (settinglinkplace)
            {
                var a = GetXYPlace(settinglinkx, settinglinky, mousex, mousey);
                if(a == null)
                {
                    return;
                }
                startx = a.Value.X;
                starty = a.Value.Y;
                endx = mousex;
                endy = mousey;
            }
            else if(settinglinkbridge)
            {
                var a = GetXYBridge(settinglinkx, settinglinky, mousex, mousey);
                if (a == null)
                {
                    return;
                }
                startx = a.Value.X;
                starty = a.Value.Y;
                endx = mousex;
                endy = mousey;
            }
            else
            {
                return;
            }


            g.DrawLine(p, startx, starty, endx, endy);
        }

        private bool commonSectionCircle(double x1, double y1, double x2, double y2, double xC, double yC, double R)
        {
            x1 -= xC;
            y1 -= yC;
            x2 -= xC;
            y2 -= yC;

            double dx = x2 - x1;
            double dy = y2 - y1;

            //составляем коэффициенты квадратного уравнения на пересечение прямой и окружности.
            //если на отрезке [0..1] есть отрицательные значения, значит отрезок пересекает окружность
            double a = dx * dx + dy * dy;
            double b = 2.0 * (x1 * dx + y1 * dy);
            double c = x1 * x1 + y1 * y1 - R * R;

            //а теперь проверяем, есть ли на отрезке [0..1] решения
            if (-b < 0)
                return (c < 0);
            if (-b < (2.0 * a))
                return ((4.0 * a * c - b * b) < 0);

            return (a + b + c < 0);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //DrawGrid(g, pictureBox1.Width, pictureBox1.Height);
            g.Clear(Color.White);//Фон

            DrawPlaces(e.Graphics);
            DrawBridges(e.Graphics);
            DrawTempLink(e.Graphics);
            DrawLinks(e.Graphics);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var args = e as MouseEventArgs;

            if (args.Button == MouseButtons.Left)
            {
                LeftPictureClick(args);
            }
        }

        private void LeftPictureClick(MouseEventArgs e)
        {
            if (now == State.AddPlace)
            {
                AddPlace(e.X, e.Y);
                pictureBox1.Invalidate();
            }
            if(now == State.AddBridge)
            {
                AddBridge(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void AddPlace(int x, int y)
        {
            foreach (var place in places)
            {
                if (Math.Sqrt((place.X -x) * (place.X - x) + (place.Y - y) * (place.Y - y)) <= placesradius)
                {
                    place.Amount++;
                    return;
                }
            }

            foreach (var place in places)
            {
                if (Math.Sqrt((place.X - x) * (place.X - x) + (place.Y - y) * (place.Y - y)) <= placesradius * 2)
                {
                    //To Do.. сообщение, что так близко невозможно установить
                    return;
                }
            }

            foreach(var bridge in bridges)
            {
                bool result = commonSectionCircle(bridge.X - bridgeswidth / 2, bridge.Y - bridgesheight / 2, bridge.X - bridgeswidth / 2, bridge.Y + bridgesheight / 2, x, y, placesradius) ||
                    commonSectionCircle(bridge.X - bridgeswidth / 2, bridge.Y + bridgesheight / 2, bridge.X + bridgeswidth / 2, bridge.Y + bridgesheight / 2, x, y, placesradius) ||
                    commonSectionCircle(bridge.X + bridgeswidth / 2, bridge.Y + bridgesheight / 2, bridge.X + bridgeswidth / 2, bridge.Y - bridgesheight / 2, x, y, placesradius) ||
                    commonSectionCircle(bridge.X + bridgeswidth / 2, bridge.Y - bridgesheight / 2, bridge.X - bridgeswidth / 2, bridge.Y - bridgesheight / 2, x, y, placesradius);
                if(result)
                {
                    //To Do.. сообщение, что так близко невозможно установить
                    return;
                }
            }

            places.Add(new Place(x, y));
        }

        private void AddBridge(int x, int y)
        {
            foreach (var bridge in bridges)
            {
                if(Math.Abs(x - bridge.X) <= bridgeswidth && Math.Abs(y - bridge.Y) <= bridgesheight)
                {
                    //To Do.. сообщение, что так близко невозможно установить
                    return;
                }
            }

            foreach (var place in places)
            {
                bool result = commonSectionCircle(x - bridgeswidth / 2, y - bridgesheight / 2, x - bridgeswidth / 2, y + bridgesheight / 2, place.X, place.Y, placesradius) ||
                    commonSectionCircle(x - bridgeswidth / 2, y + bridgesheight / 2, x + bridgeswidth / 2, y + bridgesheight / 2, place.X, place.Y, placesradius) ||
                    commonSectionCircle(x + bridgeswidth / 2, y + bridgesheight / 2, x + bridgeswidth / 2, y - bridgesheight / 2, place.X, place.Y, placesradius) ||
                    commonSectionCircle(x + bridgeswidth / 2, y - bridgesheight / 2, x - bridgeswidth / 2, y - bridgesheight / 2, place.X, place.Y, placesradius);
                if (result)
                {
                    //To Do.. сообщение, что так близко невозможно установить
                    return;
                }
            }

            bridges.Add(new Bridge(x, y));
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (now == State.AddLink)
            {
                AddLinkStart(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void AddLinkStart(int x, int y)
        {
            foreach (var place in places)
            {
                if (Math.Sqrt((place.X - x) * (place.X - x) + (place.Y - y) * (place.Y - y)) <= placesradius)
                {
                    settinglinkplace = true;
                    settinglinkx = place.X;
                    settinglinky = place.Y;
                }
            }

            foreach (var bridge in bridges)
            {
                if (Math.Abs(x - bridge.X) <= bridgeswidth / 2 && Math.Abs(y - bridge.Y) <= bridgesheight / 2)
                {
                    settinglinkbridge = true;
                    settinglinkx = bridge.X;
                    settinglinky = bridge.Y;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mousex = e.X;
            mousey = e.Y;
            if (settinglinkbridge || settinglinkplace)
            {
                AddLinkMove(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }

        private void AddLinkMove(int x, int y)
        {
            settinglinknowplace = false;
            settinglinknowbridge = false;
            foreach (var place in places)
            {
                if (Math.Sqrt((place.X - x) * (place.X - x) + (place.Y - y) * (place.Y - y)) <= placesradius)
                {
                    settinglinknowplace = true;
                    settinglinknowx = place.X;
                    settinglinknowy = place.Y;
                }
            }

            foreach (var bridge in bridges)
            {
                if (Math.Abs(x - bridge.X) <= bridgeswidth / 2 && Math.Abs(y - bridge.Y) <= bridgesheight / 2)
                {
                    settinglinknowbridge = true;
                    settinglinknowx = bridge.X;
                    settinglinknowy = bridge.Y;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (settinglinkbridge || settinglinkplace)
            {
                AddLinkEnd();
            }
        }

        private void AddLinkEnd()
        {
            if(settinglinknowbridge && settinglinkbridge)
            {
                //To do сообщение о том, что сделать это невозможно
            }
            else if (settinglinknowplace && settinglinkplace)
            {
                //To do сообщение о том, что сделать это невозможно
            }
            else if (settinglinkbridge && settinglinknowplace)
            {


                Bridge bridge = new Bridge();
                foreach(var tbridge in bridges)
                {
                    if(tbridge.X == settinglinkx && tbridge.Y == settinglinky)
                    {
                        bridge = tbridge;
                        break;
                    }
                }

                Place place = new Place();
                foreach (var tplace in places)
                {
                    if (tplace.X == settinglinknowx && tplace.Y == settinglinknowy)
                    {
                        place = tplace;
                        break;
                    }
                }

                links.Add(new Link(bridge, place));
            }
            else if (settinglinkplace && settinglinknowbridge)
            {
                Bridge bridge = new Bridge();
                foreach (var tbridge in bridges)
                {
                    if (tbridge.X == settinglinknowx && tbridge.Y == settinglinknowy)
                    {
                        bridge = tbridge;
                        break;
                    }
                }

                Place place = new Place();
                foreach (var tplace in places)
                {
                    if (tplace.X == settinglinkx && tplace.Y == settinglinky)
                    {
                        place = tplace;
                        break;
                    }
                }

                links.Add(new Link(place, bridge));
            }

            settinglinkbridge = false;
            settinglinkplace = false;
            settinglinknowbridge = false;
            settinglinknowplace = false;
            pictureBox1.Invalidate();
        }
    }
}

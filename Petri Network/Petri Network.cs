﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri_Network
{
    public enum State { Normal, DragAndDrop, AddPlace, AddBridge, AddLink, ChooseForLink}

    public partial class PetriNetwork : Form
    {
        Matrix A, B, C, D;

        List<Place> places;
        List<Bridge> bridges;
        List<Link> links;
        State now;
        Cursor cursor;

        private const int TIME_SLEEP = 1000;
        private List<Tuple<Place, uint>> Sleeping_Places;

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

        private bool draganddropplace;
        private bool draganddropbridge;
        private int draganddropx;
        private int draganddropy;
        private int draganddropitem;
        private int draganddropaddx;
        private int draganddropaddy;

        private int mousex;
        private int mousey;

        private bool firstpoint;
        private int firstpointx;
        private int firstpointy;

        private bool secondpoint;
        private int secondpointx;
        private int secondpointy;

        private int scrollsizex;
        private int scrollsizey;

        public Link ChangingLink { get; set; }

        private ContextMenuStrip CurrentContextMenuStrip;
        private int zoom;
        private float zoomf;

        public PetriNetwork()
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
            draganddropplace = false;
            draganddropbridge = false;
            firstpoint = false;
            secondpoint = false;
            ChangingLink = null;
            A = new Matrix();
            B = new Matrix();
            C = new Matrix();
            D = new Matrix();

            CurrentContextMenuStrip = new ContextMenuStrip();
            zoom = 100;
            scrollsizex = 0;
            scrollsizey = 0;
            SetNewZoom();
            SetScrollBars();

            now = State.Normal;
        }

        private void RefreshStates()
        {
            обычныйToolStripMenuItem.Checked = false;
            перенестиToolStripMenuItem.Checked = false;
            добавлениеДугиToolStripMenuItem.Checked = false;
            добавлениеПереходаToolStripMenuItem.Checked = false;
            добавлениеПозицииToolStripMenuItem.Checked = false;
            выбратьДугуПоВершинамToolStripMenuItem.Checked = false;
            firstpoint = false;
            secondpoint = false;
            now = State.Normal;
            pictureBox1.Refresh();
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

        /// <summary>
        /// Выбрать дугу по вершинам
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void выбратьДугуПоВершинамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshStates();
            выбратьДугуПоВершинамToolStripMenuItem.Checked = true;
            now = State.ChooseForLink;
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

        /// <summary>
        /// Отрисовка всех позиций
        /// </summary>
        /// <param name="g">Место для отрисовки</param>
        private void DrawPlaces(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            Brush brushes = Brushes.Black;

            foreach (var place in places)
            {
                var diameter = placesradius * 2;
                
                if ((settinglinkplace && settinglinkx == place.X && settinglinky == place.Y) ||
                    (settinglinknowplace && settinglinknowx == place.X && settinglinknowy == place.Y))
                {
                    pen = new Pen(Color.OrangeRed, 2);
                    brushes = Brushes.OrangeRed;
                }
                else if ((firstpoint && firstpointx == place.X && firstpointy == place.Y) ||
                    (secondpoint && secondpointx == place.X && secondpointy == place.Y))
                {
                    pen = new Pen(Color.OrangeRed, 2);
                    brushes = Brushes.OrangeRed;
                }
                else if(draganddropplace && place == places[draganddropitem])
                {
                    places[draganddropitem].X = int.MinValue;
                    places[draganddropitem].Y = int.MinValue;


                    if (PlaceTouches(mousex + draganddropaddx, mousey + draganddropaddy) || mousex + draganddropaddx < 0 || mousey + draganddropaddy < 0)
                    {
                        pen = new Pen(Color.OrangeRed, 2);
                        brushes = Brushes.OrangeRed;
                    }
                    else
                    {
                        pen = new Pen(Color.LawnGreen, 2);
                        brushes = Brushes.LawnGreen;
                    }
                    places[draganddropitem].X = mousex + draganddropaddx;
                    places[draganddropitem].Y = mousey + draganddropaddy;
                }
                else
                {
                    pen = new Pen(Color.Black, 2);
                    brushes = Brushes.Black;
                }
                g.DrawEllipse(pen, new RectangleF(place.X - placesradius, place.Y - placesradius, diameter, diameter));
                SolidBrush solidbrush = new SolidBrush(pen.Color);

                //Отрисовка фишек
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

                //Отрисовка задержки
                if (place.Await > 999)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Await.ToString().Substring(0, 2) + "...", myFont, brushes, new Point(place.X - 3 * size / 2, place.Y - (int)(size / 0.66) - placesradius));
                }
                else if (place.Await > 99)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Await.ToString(), myFont, brushes, new Point(place.X - 3 * size / 2, place.Y - (int)(size / 0.66) - placesradius));
                }
                else if (place.Await > 9)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Await.ToString(), myFont, brushes, new Point(place.X - size, place.Y - (int)(size / 0.66) - placesradius));
                }
                else if(place.Await > 0)
                {
                    var size = 14;
                    Font myFont = new Font("Arial", size);
                    g.DrawString(place.Await.ToString(), myFont, brushes, new Point(place.X - size / 2, place.Y - (int)(size / 0.66) - placesradius));
                }
            }
        }

        /// <summary>
        /// Отрисовка всех переходов
        /// </summary>
        /// <param name="g">Место для отрисовки</param>
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
                else if ((firstpoint && firstpointx == bridge.X && firstpointy == bridge.Y) ||
                    (secondpoint && secondpointx == bridge.X && secondpointy == bridge.Y))
                {
                    pen = new Pen(Color.OrangeRed, 2);
                }
                else if (draganddropbridge && bridge == bridges[draganddropitem])
                {
                    bridges[draganddropitem].X = int.MinValue;
                    bridges[draganddropitem].Y = int.MinValue;


                    if (BridgeTouches(mousex + draganddropaddx, mousey + draganddropaddy) || mousex + draganddropaddx < 0 || mousey + draganddropaddy < 0)
                    {
                        pen = new Pen(Color.OrangeRed, 2);
                    }
                    else
                    {
                        pen = new Pen(Color.LawnGreen, 2);
                    }
                    bridges[draganddropitem].X = mousex + draganddropaddx;
                    bridges[draganddropitem].Y = mousey + draganddropaddy;
                }
                else
                {
                    pen = new Pen(Color.Black, 2);
                }
                g.DrawRectangle(pen, new Rectangle(bridge.X - bridgeswidth / 2, bridge.Y - bridgesheight / 2, bridgeswidth, bridgesheight));
            }
        }

        /// <summary>
        /// Отрисовка всех дуг
        /// </summary>
        /// <param name="g">Место для отрисовки</param>
        private void DrawLinks(Graphics g)
        {
            int startx, endx, starty, endy;
            Pen p;
            foreach (var link in links)
            {
                p = new Pen(Color.GreenYellow, 2);
                if (link == ChangingLink)
                {
                    p = new Pen(Color.Red, 2);
                }

                var place = GetXYPlace(link.Place.X, link.Place.Y, link.Bridge.X, link.Bridge.Y);
                if (place == null)
                {
                    continue;
                }
                var bridge = GetXYBridge(link.Bridge.X, link.Bridge.Y, link.Place.X, link.Place.Y);
                if (bridge == null)
                {
                    continue;
                }
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

                Point pointstart = new Point(startx, starty);
                Point pointend = new Point(endx, endy);
                Point? pointmiddle = GetMiddlePoint(pointstart, pointend, link.Сurvature * 10);

                if(pointmiddle == null)
                {
                    continue;
                }

                Point[] points;
                try
                {
                    g.DrawBezier(p, pointstart, (Point)pointmiddle, (Point)pointmiddle, pointend);
                    points = GetArrowEnd((Point)pointmiddle, pointend);
                }
                catch
                {
                    MessageBox.Show("Не удаётся нарисовать дугу!");
                    link.Сurvature = 0;
                    g.DrawLine(p, pointstart, pointend);
                    points = GetArrowEnd(pointstart, pointend);
                }

                try
                {
                    g.DrawLine(p, points[0], pointend);
                    g.DrawLine(p, points[1], pointend);
                }
                catch
                {
                    points = GetArrowEnd(pointstart, pointend);
                    g.DrawLine(p, points[0], pointend);
                    g.DrawLine(p, points[1], pointend);
                }
            }
        }

        /// <summary>
        /// Создание наконечника для стрелки
        /// </summary>
        /// <param name="start">Начало прямой</param>
        /// <param name="end">Конец прямой</param>
        /// <returns>Две точки наконечника</returns>
        private Point[] GetArrowEnd(Point start, Point end)
        {
            double X1 = start.X;
            double Y1 = start.Y;
            double X2 = end.X;
            double Y2 = end.Y;

            // координаты центра отрезка
            int X3 = (int)X2;
            int Y3 = (int)Y2;

            // длина отрезка
            double d = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));

            // координаты вектора
            double X = X2 - X1;
            double Y = Y2 - Y1;

            // координаты точки, удалённой от центра к началу отрезка на 14px
            double X4 = X3 - (X / d) * 14;
            double Y4 = Y3 - (Y / d) * 14;

            // из уравнения прямой { (x - x1)/(x1 - x2) = (y - y1)/(y1 - y2) } получаем вектор перпендикуляра
            // (x - x1)/(x1 - x2) = (y - y1)/(y1 - y2) =>
            // (x - x1)*(y1 - y2) = (y - y1)*(x1 - x2) =>
            // (x - x1)*(y1 - y2) - (y - y1)*(x1 - x2) = 0 =>
            // полученные множители x и y => координаты вектора перпендикуляра
            double Xp = Y2 - Y1;
            double Yp = X1 - X2;

            // координаты перпендикуляров, удалённой от точки X4;Y4 на 7px в разные стороны
            int X5 = (int)(X4 + (Xp / d) * 7);
            int Y5 = (int)(Y4 + (Yp / d) * 7);
            int X6 = (int)(X4 - (Xp / d) * 7);
            int Y6 = (int)(Y4 - (Yp / d) * 7);

            Point[] points = { new Point(X5, Y5), new Point(X6, Y6) };
            return points;
        }

        /// <summary>
        /// Находит точку пересечения позиции и прямой
        /// </summary>
        /// <param name="x1">Координата X центра позиции</param>
        /// <param name="y1">Координата Y центра позиции</param>
        /// <param name="x2">Координата X до которой идет прямая из центра</param>
        /// <param name="y2">Координата Y до которой идет прямая из центра</param>
        /// <returns>Возвращает точку пересечения. Если её не существует, то null</returns>
        private Point? GetXYPlace(int x1, int y1, int x2, int y2)
        {
            if (Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)) < placesradius)
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

        /// <summary>
        /// Находит точку пересечения перехода и прямой
        /// </summary>
        /// <param name="x1">Координата X центра перехода</param>
        /// <param name="y1">Координата Y центра перехода</param>
        /// <param name="x2">Координата X до которой идет прямая из центра</param>
        /// <param name="y2">Координата Y до которой идет прямая из центра</param>
        /// <returns>Возвращает точку пересечения. Если её не существует, то null</returns>
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
            int c = y1 * (x2 - x1) - x1 * (y2 - y1);
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
            if (nowpoint != null)
            {
                distance = Math.Sqrt((nowpoint.Value.X - x2) * (nowpoint.Value.X - x2) + (nowpoint.Value.Y - y2) * (nowpoint.Value.Y - y2));
            }

            Point? point2;
            if (b == 0)
            {
                point2 = null;
            }
            else
            {
                x = xx1;
                y = (-c - a * x) / -b;
                point2 = new Point(x, y);
                if (y > yy2 || y < yy1)
                {
                    point2 = null;
                }
            }

            if (point2 != null)
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

        /// <summary>
        /// Отрисовка временных дуг
        /// </summary>
        /// <param name="g">Место для отрисовки</param>
        private void DrawTempLink(Graphics g)
        {
            Pen p = new Pen(Color.OrangeRed, 2);

            int startx, endx, starty, endy;


            if (settinglinkplace)
            {
                var a = GetXYPlace(settinglinkx, settinglinky, mousex, mousey);
                if (a == null)
                {
                    return;
                }
                startx = a.Value.X;
                starty = a.Value.Y;
                endx = mousex;
                endy = mousey;
            }
            else if (settinglinkbridge)
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

            Point pointstart = new Point(startx, starty);
            Point pointend = new Point(endx, endy);
            Point[] points = points = GetArrowEnd(pointstart, pointend);

            try
            {
                g.DrawLine(p, pointstart, pointend);
                g.DrawLine(p, points[0], pointend);
                g.DrawLine(p, points[1], pointend);
            }
            catch
            {

            }
        }

        /// <summary>
        /// Определяет есть ли пересечени у прямой и окружности
        /// </summary>
        /// <param name="x1">Координата X1 прямой</param>
        /// <param name="y1">Координата Y1 прямой</param>
        /// <param name="x2">Координата X2 прямой</param>
        /// <param name="y2">Координата Y2 прямой</param>
        /// <param name="xC">Координата X центра окружности</param>
        /// <param name="yC">Координата Y центра окружности</param>
        /// <param name="R">Радиус окружности</param>
        /// <returns>True, если окружность и прямая пересекаются. Иначе - False</returns>
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

        /// <summary>
        /// Вызывается каждый раз при отрисовки pixtureBox1
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            SetScrollBars();
            CurrentContextMenuStrip.Visible = false;

            Graphics g = e.Graphics;
            
            //DrawGrid(g, pictureBox1.Width, pictureBox1.Height);
            g.Clear(Color.White);//Фон

            g.TranslateTransform(pictureBox1.AutoScrollPosition.X, pictureBox1.AutoScrollPosition.Y);
            g.ScaleTransform(zoomf, zoomf);
            DrawPlaces(e.Graphics);
            DrawBridges(e.Graphics);
            DrawTempLink(e.Graphics);
            DrawLinks(e.Graphics);
        }

        /// <summary>
        /// Клик по picturebox
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var args = e as MouseEventArgs;
            args = new MouseEventArgs(args.Button, args.Clicks, (int)((args.X - pictureBox1.AutoScrollPosition.X) / zoomf), (int)((args.Y - pictureBox1.AutoScrollPosition.Y) / zoomf), args.Delta);

            if (args.Button == MouseButtons.Left)
            {
                LeftPictureClick(args);
            }

            if(args.Button == MouseButtons.Right)
            {
                RightPictureClick(args);
            }
        }

        /// <summary>
        /// Обрабатывает левый клик по picture box
        /// </summary>
        /// <param name="e">Данные о мыши</param>
        private void LeftPictureClick(MouseEventArgs e)
        {
            if (now == State.AddPlace)
            {
                AddPlace(e.X, e.Y);
                pictureBox1.Refresh();
            }
            if (now == State.AddBridge)
            {
                AddBridge(e.X, e.Y);
                pictureBox1.Refresh();
            }

            if(now == State.ChooseForLink)
            {
                ChooseForLinkClick(e);
                pictureBox1.Refresh();
            }
        }

        /// <summary>
        ///  Обрабатывает левый клик по picture box, при выборе вершин между для нахождения дуг
        /// </summary>
        /// <param name="e">Данные о мыши</param>
        private void ChooseForLinkClick(MouseEventArgs e)
        {
            foreach (var place in places)
            {
                if (Math.Sqrt((place.X - e.X) * (place.X - e.X) + (place.Y - e.Y) * (place.Y - e.Y)) <= placesradius)
                {
                    if(!firstpoint)
                    {
                        firstpoint = true;
                        firstpointx = place.X;
                        firstpointy = place.Y;
                    }
                    else
                    {
                        secondpoint = true;
                        secondpointx = place.X;
                        secondpointy = place.Y;
                        EditLink();
                    }
                    return;
                }
            }

            foreach (var bridge in bridges)
            {
                if (Math.Abs(e.X - bridge.X) <= bridgeswidth / 2 && Math.Abs(e.Y - bridge.Y) <= bridgesheight / 2)
                {
                    if (!firstpoint)
                    {
                        firstpoint = true;
                        firstpointx = bridge.X;
                        firstpointy = bridge.Y;
                    }
                    else
                    {
                        secondpoint = true;
                        secondpointx = bridge.X;
                        secondpointy = bridge.Y;
                        EditLink();
                    }
                    return;
                }
            }
        }

        private void EditLink()
        {
            List<Link> links = this.links.FindAll(x => (x.Bridge.X == firstpointx && x.Bridge.Y == firstpointy && x.Place.X == secondpointx && x.Place.Y == secondpointy) ||
                (x.Place.X == firstpointx && x.Place.Y == firstpointy && x.Bridge.X == secondpointx && x.Bridge.Y == secondpointy));
            pictureBox1.Refresh();
            if (links.Count == 0)
            {
                MessageBox.Show("Между выбранными вершинами нет дуг!");
                firstpoint = secondpoint = false;
                return;
            }
            EditLink editer = new EditLink(links, this);
            editer.ShowDialog();
            ChangingLink = null;
            firstpoint = secondpoint = false;
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Обрабатывает правый клик по picture box
        /// </summary>
        /// <param name="e">Данные о мыши</param>
        private void RightPictureClick(MouseEventArgs e)
        {
            foreach (var place in places)
            {
                if (Math.Sqrt((place.X - e.X) * (place.X - e.X) + (place.Y - e.Y) * (place.Y - e.Y)) <= placesradius)
                {
                    DrawMenuPlace(place);
                    return;
                }
            }

            foreach (var bridge in bridges)
            {
                if (Math.Abs(e.X - bridge.X) <= bridgeswidth / 2 && Math.Abs(e.Y - bridge.Y) <= bridgesheight / 2)
                {
                    DrawMenuToBridge(bridge);
                    return;
                }
            }
        }

        /// <summary>
        /// Открытие меню для позиции
        /// </summary>
        /// <param name="place">Позиция</param>
        private void DrawMenuPlace(Place place)
        {
            CurrentContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem edit = new ToolStripMenuItem("Редактировать");
            CurrentContextMenuStrip.Items.Add(edit);
            ToolStripMenuItem remove = new ToolStripMenuItem("Удалить");
            CurrentContextMenuStrip.Items.Add(remove);
            CurrentContextMenuStrip.Show(pictureBox1, (int)((place.X + placesradius + 1 + pictureBox1.AutoScrollPosition.X) * zoomf), (int)((place.Y - placesradius - 1 + pictureBox1.AutoScrollPosition.Y) * zoomf));

            edit.Click += (s, e) =>
            {
                PlaceEditor placeeditor = new PlaceEditor(place);
                placeeditor.ShowDialog();
                pictureBox1.Refresh();
            };

            remove.Click += (s, e) =>
            {
               List<int> lid = new List<int>();

                for (int i = links.Count - 1; i >= 0; i--)
                {
                    if (links[i].Place == place)
                    {
                        lid.Add(i);
                    }
                }

                foreach (var i in lid)
                {
                    links.RemoveAt(i);
                }

                places.Remove(place);
                pictureBox1.Refresh();
            };
        }

        /// <summary>
        /// Открытие меню для перехода
        /// </summary>
        /// <param name="bridge">Переход</param>
        private void DrawMenuToBridge(Bridge bridge)
        {
            CurrentContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem("Удалить");
            CurrentContextMenuStrip.Items.Add(item);
            CurrentContextMenuStrip.Show(pictureBox1, (int)((bridge.X + bridgeswidth / 2 + 1 + pictureBox1.AutoScrollPosition.X) * zoomf), (int)((bridge.Y - bridgesheight / 2 - 1 + pictureBox1.AutoScrollPosition.Y) * zoomf));

            item.Click += (s, e) =>
            {
                List<int> lid = new List<int>();

                for(int i = links.Count - 1; i >= 0; i--)
                {
                    if(links[i].Bridge == bridge)
                    {
                        lid.Add(i);
                    }
                }

                foreach(var i in lid)
                {
                    links.RemoveAt(i);
                }

                bridges.Remove(bridge);
                pictureBox1.Refresh();
            };
        }

        /// <summary>
        /// Добавление позиции с указанными координатами
        /// </summary>
        /// <param name="x">Координата по X</param>
        /// <param name="y">Координата по Y</param>
        private void AddPlace(int x, int y)
        {
            foreach (var place in places)
            {
                if (Math.Sqrt((place.X - x) * (place.X - x) + (place.Y - y) * (place.Y - y)) <= placesradius)
                {
                    place.Amount++;
                    return;
                }
            }

            if(PlaceTouches(x, y))
            {
                MessageBox.Show("Объекты не могут пересекаться");
                return;
            }

            places.Add(new Place(x, y));
        }

        /// <summary>
        /// Добавление перехода с указанными координатами
        /// </summary>
        /// <param name="x">Координата по X</param>
        /// <param name="y">Координата по Y</param>
        private void AddBridge(int x, int y)
        {
            if(BridgeTouches(x, y))
            {
                MessageBox.Show("Объекты не могут пересекаться");
                return;
            }

            bridges.Add(new Bridge(x, y));
        }

        /// <summary>
        /// Срабатывает в случае зажатия мыши
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            e = new MouseEventArgs(e.Button, e.Clicks, (int)((e.X - pictureBox1.AutoScrollPosition.X) / zoomf), (int)((e.Y - pictureBox1.AutoScrollPosition.Y) / zoomf), e.Delta);
            if (now == State.AddLink)
            {
                AddLinkStart(e.X, e.Y);
                pictureBox1.Refresh();
            }
            if(e.Button == MouseButtons.Left)
            {
                if(now == State.DragAndDrop)
                {
                    DradAndDropStart(e.X, e.Y);
                    pictureBox1.Refresh();
                }
            }
        }

        /// <summary>
        /// Выполняется для перемещения объектов, когда кнопку мыши зажали
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        private void DradAndDropStart(int x, int y)
        {
            int i = 0;
            foreach (var place in places)
            {
                if (Math.Sqrt((place.X - x) * (place.X - x) + (place.Y - y) * (place.Y - y)) <= placesradius)
                {
                    draganddropplace = true;
                    draganddropx = place.X;
                    draganddropy = place.Y;
                    draganddropaddx = place.X - x;
                    draganddropaddy = place.Y - y;
                    draganddropitem = i;
                    return;
                }
                i++;
            }

            i = 0;
            foreach (var bridge in bridges)
            {
                if (Math.Abs(x - bridge.X) <= bridgeswidth / 2 && Math.Abs(y - bridge.Y) <= bridgesheight / 2)
                {
                    draganddropbridge = true;
                    draganddropx = bridge.X;
                    draganddropy = bridge.Y;
                    draganddropaddx = bridge.X - x;
                    draganddropaddy = bridge.Y - y;
                    draganddropitem = i;
                    return;
                }
                i++;
            }
        }

        /// <summary>
        /// Выполняется для добавление дуги между двумя объектами, когда кнопку мыши зажали
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
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

        /// <summary>
        /// Срабатывает в случае передвижении мыши
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            e = new MouseEventArgs(e.Button, e.Clicks, (int)((e.X - pictureBox1.AutoScrollPosition.X) / zoomf), (int)((e.Y - pictureBox1.AutoScrollPosition.Y) / zoomf), e.Delta);
            mousex = e.X;
            mousey = e.Y;
            if (settinglinkbridge || settinglinkplace)
            {
                AddLinkMove(e.X, e.Y);
                pictureBox1.Refresh();
            }
            if(draganddropbridge || draganddropplace)
            {
                pictureBox1.Refresh();
            }
        }

        /// <summary>
        /// Выполняется для добавление дуги между двумя объектами, когда мыш передвинули
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
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

        /// <summary>
        /// Срабатывает в случае, когда кнопку мыши отпускают
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (settinglinkbridge || settinglinkplace)
            {
                AddLinkEnd();
            }
            if(draganddropplace || draganddropbridge)
            {
                DragAndDropEnd();
            }
        }

        /// <summary>
        /// Выполняется по окончанию переноса объектов
        /// </summary>
        private void DragAndDropEnd()
        {
            if (draganddropplace)
            {
                places[draganddropitem].X = int.MinValue;
                places[draganddropitem].Y = int.MinValue;
                if (PlaceTouches(mousex + draganddropaddx, mousey + draganddropaddy))
                {
                    MessageBox.Show("Объекты не могут пересекаться");
                    places[draganddropitem].X = draganddropx;
                    places[draganddropitem].Y = draganddropy;
                }
                else if(mousex + draganddropaddx < 0 || mousey + draganddropaddy < 0)
                {
                    MessageBox.Show("Объекты не могут находиться за пределами поля");
                    places[draganddropitem].X = draganddropx;
                    places[draganddropitem].Y = draganddropy;
                }
                else
                {
                    places[draganddropitem].X = mousex + draganddropaddx;
                    places[draganddropitem].Y = mousey + draganddropaddy;
                }
            }

            if(draganddropbridge)
            {
                bridges[draganddropitem].X = int.MinValue;
                bridges[draganddropitem].Y = int.MinValue;
                if (BridgeTouches(mousex + draganddropaddx, mousey + draganddropaddy))
                {
                    MessageBox.Show("Объекты не могут пересекаться");
                    bridges[draganddropitem].X = draganddropx;
                    bridges[draganddropitem].Y = draganddropy;
                }
                else if (mousex + draganddropaddx < 0 || mousey + draganddropaddy < 0)
                {
                    MessageBox.Show("Объекты не могут находиться за пределами поля");
                    bridges[draganddropitem].X = draganddropx;
                    bridges[draganddropitem].Y = draganddropy;
                }
                else
                {
                    bridges[draganddropitem].X = mousex + draganddropaddx;
                    bridges[draganddropitem].Y = mousey + draganddropaddy;
                }
            }

            draganddropplace = false;
            draganddropbridge = false;
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Выполняется для добавление дуги между двумя объектами, когда кнопку мыши отпустили
        /// </summary>
        private void AddLinkEnd()
        {
            bool ok = true;
            if (settinglinknowbridge && settinglinkbridge)
            {
                if (settinglinknowx != settinglinkx && settinglinknowy != settinglinky)
                {
                    MessageBox.Show("Невозможно добавить дугу из перехода в переход!");
                }
            }
            else if (settinglinknowplace && settinglinkplace)
            {
                if (settinglinknowx != settinglinkx && settinglinknowy != settinglinky)
                {
                    MessageBox.Show("Невозможно добавить дугу из позиции в позицию!");
                }
            }
            else if (settinglinkbridge && settinglinknowplace)
            {
                foreach (var link in links)
                {
                    if (!link.FromPlace && link.Place.X == settinglinknowx && link.Place.Y == settinglinknowy)
                    {
                        MessageBox.Show("В одну позицию может входить только одна дуга!");
                        ok = false;
                    }
                }

                if (ok)
                {
                    Bridge bridge = new Bridge();
                    foreach (var tbridge in bridges)
                    {
                        if (tbridge.X == settinglinkx && tbridge.Y == settinglinky)
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
            }
            else if (settinglinkplace && settinglinknowbridge)
            {
                foreach (var link in links)
                {
                    if (link.FromPlace && link.Place.X == settinglinkx && link.Place.Y == settinglinky)
                    {
                        MessageBox.Show("Из одной позиции может выходить только одна дуга!");
                        ok = false;
                    }
                }

                if (ok)
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
            }

            if(!OneBetweenAnyTwoBridges())
            {
                links.RemoveAt(links.Count - 1);
                MessageBox.Show("Дугу невозможно добавить! Между двумя переходами может быть максимум одна позиция!");
            }

            settinglinkbridge = false;
            settinglinkplace = false;
            settinglinknowbridge = false;
            settinglinknowplace = false;
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Проверяет есть ли входной переход
        /// </summary>
        /// <returns>возвращает True, если есть, иначе - False</returns>
        private bool HasEnter()
        {
            bool has = false;
            foreach (var bringe in bridges)
            {
                bool enter = false, exit = false;
                foreach (var link in links)
                {
                    if (link.Bridge == bringe)
                    {
                        if (link.FromPlace)
                        {
                            enter = true;
                        }
                        else
                        {
                            exit = true;
                        }
                    }
                    if (exit && enter)
                    {
                        break;
                    }
                }
                if (!enter && exit)
                {
                    has = true;
                    break;
                }
            }
            return has;
        }

        /// <summary>
        /// Проверяет есть ли выходной переход
        /// </summary>
        /// <returns>возвращает True, если есть, иначе - False</returns>
        private bool HasExit()
        {
            bool has = false;
            foreach (var bringe in bridges)
            {
                bool enter = false, exit = false;
                foreach (var link in links)
                {
                    if (link.Bridge == bringe)
                    {
                        if (link.FromPlace)
                        {
                            enter = true;
                        }
                        else
                        {
                            exit = true;
                        }
                    }
                    if (exit && enter)
                    {
                        break;
                    }
                }
                if (enter && !exit)
                {
                    has = true;
                    break;
                }
            }
            return has;
        }

        /// <summary>
        /// Проверяет является ли граф связанным
        /// </summary>
        /// <returns>возвращает True, иначе - False</returns>
        private bool IsGraphConnected()
        {
            bool[] pla = new bool[places.Count];
            bool[] bri = new bool[bridges.Count];

            for (int i = 0; i < pla.Length; i++)
            {
                pla[i] = false;
            }

            for (int i = 0; i < bri.Length; i++)
            {
                bri[i] = false;
            }

            if ((places.Count == 0 && bridges.Count == 1) || (places.Count == 1 && bridges.Count == 0) || (places.Count == 0 && bridges.Count == 0))
            {
                return true;
            }

            ForPlace(ref pla, ref bri, places[0]);

            foreach(var p in pla)
            {
                if(!p)
                {
                    return false;
                }
            }

            foreach (var b in bri)
            {
                if (!b)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Обход в глубину от позиции
        /// </summary>
        /// <param name="pla">Массив посещеных позиций</param>
        /// <param name="bri">Массив посещеных переходов</param>
        /// <param name="place">Позиция</param>
        private void ForPlace(ref bool[] pla, ref bool[] bri, Place place)
        {
            int i;
            for(i = 0; i < places.Count; i++)
            {
                if (places[i] == place)
                {
                    break;
                }
            }

            if(pla[i])
            {
                return;
            }

            pla[i] = true;

            foreach(var link in links)
            {
                if(link.Place == place)
                {
                    ForBridge(ref pla, ref bri, link.Bridge);
                }
            }
        }

        /// <summary>
        /// Обход в глубину от перехода
        /// </summary>
        /// <param name="pla">Массив посещеных позиций</param>
        /// <param name="bri">Массив посещеных переходов</param>
        /// <param name="place">Переход</param>
        private void ForBridge(ref bool[] pla, ref bool[] bri, Bridge bridge)
        {
            int i;
            for (i = 0; i < bridges.Count; i++)
            {
                if (bridges[i] == bridge)
                {
                    break;
                }
            }

            if (bri[i])
            {
                return;
            }

            bri[i] = true;

            foreach (var link in links)
            {
                if (link.Bridge == bridge)
                {
                    ForPlace(ref pla, ref bri, link.Place);
                }
            }
        }

        /// <summary>
        /// Проверят что между двумя переходами не больше одной позиции
        /// </summary>
        /// <returns>Возвращает True, если это так, иначе - False</returns>
        private bool OneBetweenAnyTwoBridges()
        {
            for(int i = 0; i < bridges.Count; i++)
            {
                for (int j = i; j < bridges.Count; j++)
                {
                    if(!OneBetweenAnyTwoBridges(bridges[i], bridges[j]) || !OneBetweenAnyTwoBridges(bridges[j], bridges[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Проверят что между двумя контретными переходами не больше одной позиции
        /// </summary>
        /// <param name="bridge1">Выходной переход</param>
        /// <param name="bridge2">Входной переход</param>
        /// <returns>Возвращает True, если это так, иначе - False</returns>
        private bool OneBetweenAnyTwoBridges(Bridge bridge1, Bridge bridge2)
        {
            int positions = 0;
            foreach (var link1 in links.Where(l => l.Bridge == bridge1 && !l.FromPlace))
            {
                foreach(var link2 in links.Where(l => l.Bridge == bridge2 && l.FromPlace))
                {
                    if(link1.Place == link2.Place)
                    {
                        positions++;
                    }
                }
            }
            return positions <= 1;
        }

        /// <summary>
        /// Пересекалась ли бы позиция, если находилась на заданных координатах
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>Возвращает True, если пересекалась бы, иначе False</returns>
        private bool PlaceTouches(int x, int y)
        {
            foreach (var place in places)
            {
                if(place.X == int.MinValue && place.Y == int.MinValue)
                {
                    continue;
                }
                if (Math.Sqrt((place.X - x) * (place.X - x) + (place.Y - y) * (place.Y - y)) <= placesradius * 2)
                {
                    return true;
                }
            }

            foreach (var bridge in bridges)
            {
                bool result = commonSectionCircle(bridge.X - bridgeswidth / 2, bridge.Y - bridgesheight / 2, bridge.X - bridgeswidth / 2, bridge.Y + bridgesheight / 2, x, y, placesradius) ||
                    commonSectionCircle(bridge.X - bridgeswidth / 2, bridge.Y + bridgesheight / 2, bridge.X + bridgeswidth / 2, bridge.Y + bridgesheight / 2, x, y, placesradius) ||
                    commonSectionCircle(bridge.X + bridgeswidth / 2, bridge.Y + bridgesheight / 2, bridge.X + bridgeswidth / 2, bridge.Y - bridgesheight / 2, x, y, placesradius) ||
                    commonSectionCircle(bridge.X + bridgeswidth / 2, bridge.Y - bridgesheight / 2, bridge.X - bridgeswidth / 2, bridge.Y - bridgesheight / 2, x, y, placesradius);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Пересекался ли бы переход, если находилась на заданных координатах
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>Возвращает True, если пересекалась бы, иначе False</returns>
        private bool BridgeTouches(int x, int y)
        {
            foreach (var bridge in bridges)
            {
                if (bridge.X == int.MinValue && bridge.Y == int.MinValue)
                {
                    continue;
                }

                if (Math.Abs(x - bridge.X) <= bridgeswidth && Math.Abs(y - bridge.Y) <= bridgesheight)
                {
                    return true;
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
                    return true;
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //StartWorking();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Срабатывает через каждый определенный промежуток таймера
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        //Автоматические передвижение фишек согласно их задержкам
        /*/// <summary>
        /// Начинается работа модели
        /// </summary>
        private void StartWorking()
        {
            //MaxSleep = places.Max(x => x.Await);
            //NowSleep = 1;
            Sleeping_Places = new List<Tuple<Place, uint>>();

            foreach (var p in places)
            {
                Sleeping_Places.Add(new Tuple<Place, uint>(p, p.Amount == 0 ? 0 : p.Await));
            }

            WorkingIteration();
            //timer1.Enabled = true;
        }

        private void WorkingIteration()
        {
            MoveImmediately();
            pictureBox1.Refresh();

            foreach (var sp in Sleeping_Places.Where(sp => sp.Item2 == 0 && sp.Item1.Await != 0 && sp.Item1.Amount != 0).ToList())
            {
                MovePlace(sp.Item1);
            }
            pictureBox1.Refresh();

            MoveImmediately();

            foreach (var sp in Sleeping_Places.Where(sp => sp.Item2 != 0 && sp.Item1.Await != 0 && sp.Item1.Amount != 0).ToList())
            {
                Sleeping_Places[Sleeping_Places.FindIndex(x => x.Item1 == sp.Item1)] = new Tuple<Place, uint>(sp.Item1, sp.Item2 - 1);
            }

            pictureBox1.Refresh();
        }

        private void MoveImmediately()
        {
            bool was;
            do
            {
                was = false;
                foreach (var p in places.Where(p => p.Await == 0 && p.Amount != 0))
                {
                    was = MovePlace(p) || was;
                }
            }
            while (was);
        }

        private bool MovePlace(Place place)
        {
            var bridge = links.FirstOrDefault(l => l.Place == place && l.FromPlace)?.Bridge;
            if(bridge == null)
            {
                return false;
            }

            var newplace = links.FirstOrDefault(l => l.Bridge == bridge && !l.FromPlace)?.Place;

            if(newplace == null)
            {
                return false;
            }

            place.Amount--;
            newplace.Amount++;

            Sleeping_Places[Sleeping_Places.FindIndex(sp => sp.Item1 == place)] = new Tuple<Place, uint>(place, place.Await);

            ChangeOrder(bridge);

            return true;
        }

        private void ChangeOrder(Bridge bridge)
        {
            var lnks = links.Where(l => l.Bridge == bridge && !l.FromPlace).ToList();
            
            if(lnks == null || lnks.Count == 0 || lnks.Count == 1)
            {
                return;
            }

            for(int i = 0, j = lnks.Count - 1; i < lnks.Count; i++, j++)
            {
                j %= lnks.Count;
                links[links.FindIndex(x => x == lnks[j])] = lnks[i];
            }
        }*/

        /// <summary>
        /// Нахождение середины дуги
        /// </summary>
        /// <param name="point1">Первая точка</param>
        /// <param name="point2">Вторая точка</param>
        /// <param name="length">Удаленность от прямой</param>
        /// <returns>Точку-середину дуги</returns>
        private Point? GetMiddlePoint(Point point1, Point point2, int length)
        {
            Point pointMiddle = new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);

            Point AB = new Point(pointMiddle.X - point1.X, pointMiddle.Y - point1.Y);
            Point BC = new Point();

            if(AB.X == 0 && AB.Y == 0)
            {
                return null;
            }
            else if(AB.X == 0)
            {
                BC.X = (int)Math.Sqrt(((long)length * (long)length * (long)AB.Y * (long)AB.Y) / (double)((long)AB.Y * (long)AB.Y + (long)AB.X * (long)AB.X));
                BC.Y = -AB.X * BC.X / AB.Y;
            }
            else
            {
                BC.Y = (int)Math.Sqrt(((long)length * (long)length * (long)AB.X * (long)AB.X) / (double)((long)AB.Y * (long)AB.Y + (long)AB.X * (long)AB.X));
                BC.X = -AB.Y * BC.Y / AB.X;
            }

            if(length > 0)
            {
                return new Point(pointMiddle.X + BC.X, pointMiddle.Y + BC.Y);
            }
            else
            {
                return new Point(pointMiddle.X - BC.X, pointMiddle.Y - BC.Y);
            }
        }
        
        /// <summary>
        /// Перерисовка picture box
        /// </summary>
        public void PictureboxIndalidate()
        {
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Удаление дуги
        /// </summary>
        /// <param name="link">Дуга</param>
        /// <returns>Возвращается значение true, если элемент item успешно удален, в противном случае — значение false.</returns>
        public bool RemoveLink(Link link)
        {
            return links.Remove(link);
        }

        /// <summary>
        /// Создание матриц A, B, C и D
        /// </summary>
        private void CreateABCD()
        {
            List<Bridge> s = new List<Bridge>();
            List<Bridge> x = new List<Bridge>();
            List<Bridge> y = new List<Bridge>();

            foreach (var bridge in bridges)
            {
                bool exit = false, enter = false;
                foreach(var link in links.Where(l => l.Bridge == bridge))
                {
                    if(link.FromPlace)
                    {
                        enter = true;
                    }

                    if (!link.FromPlace)
                    {
                        exit = true;
                    }
                }

                if(exit && enter)
                {
                    s.Add(bridge);
                }
                else if(!exit && enter)
                {
                    y.Add(bridge);
                }
                else if(exit && !enter)
                {
                    x.Add(bridge);
                }
            }

            CreateMatrixFromBridges(s, s, A);
            CreateMatrixFromBridges(x, s, B);
            CreateMatrixFromBridges(s, y, C);
            CreateMatrixFromBridges(x, y, D);
        }

        /// <summary>
        /// Приближение модели
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AddZoom_Click(object sender, EventArgs e)
        {
            zoom += 10;
            SetNewZoom();
        }

        /// <summary>
        /// Отдаление модели
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MinusZoom_Click(object sender, EventArgs e)
        {
            zoom -= 10;
            SetNewZoom();
        }

        /// <summary>
        /// Устанавливается новый zoom
        /// </summary>
        private void SetNewZoom()
        {
            zoom = zoom < 10 ? 10 : zoom;
            zoom = zoom > 200 ? 200 : zoom;
            label1.Text = zoom.ToString();
            zoomf = zoom / (float)100.0;
            trackBar1.Value = zoom;
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Установка горизонтальных и вертикальных прокруток
        /// </summary>
        private void SetScrollBars()
        {
            scrollsizex = 0;
            scrollsizey = 0;

            foreach (var o in places)
            {
                scrollsizex = o.X > scrollsizex ? o.X : scrollsizex;
                scrollsizey = o.Y > scrollsizey ? o.Y : scrollsizey;
            }

            foreach (var o in bridges)
            {
                scrollsizex = o.X > scrollsizex ? o.X : scrollsizex;
                scrollsizey = o.Y > scrollsizey ? o.Y : scrollsizey;
            }

            scrollsizex += 500;
            scrollsizey += 500;
            pictureBox1.SetAutoScroll((int)(scrollsizex * zoomf), (int)(scrollsizey * zoomf));
        }

        /// <summary>
        /// Событие при прокрутке 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void pictureBox1_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Открывается новая
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bridges = new List<Bridge>();
            places = new List<Place>();
            links = new List<Link>();
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Открывает проект из файла
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string s;
                    using (StreamReader read = new StreamReader(openFileDialog1.FileName))
                    {
                        s = read.ReadToEnd();
                    }

                    var definition = new { bridges, places, links };

                    var save = new
                    {
                        A = new List<List<List<string>>>(),
                        B = new List<List<List<string>>>(),
                        C = new List<List<List<string>>>(),
                        D = new List<List<List<string>>>(),
                        bridges,
                        places,
                        links
                    };

                    var a = JsonConvert.DeserializeAnonymousType(s, save);
                    bridges = a.bridges;
                    places = a.places;
                    links = new List<Link>();

                    foreach (var link in a.links)
                    {
                        var bridge = bridges.First(b => b == link.Bridge);
                        var place = places.First(p => p == link.Place);
                        links.Add(new Link(place, bridge, link.Сurvature, link.FromPlace));
                    }

                    A = MatrixFromList(a.A);
                    B = MatrixFromList(a.B);
                    C = MatrixFromList(a.C);
                    D = MatrixFromList(a.D);

                    PictureboxIndalidate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Невозможно загрузить выбранный файл!");
            }
        }

        /// <summary>
        /// Сохраняет сеть
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    CreateABCD();


                    var save = new
                    {
                        A = ListFromMatrix(A),
                        B = ListFromMatrix(B),
                        C = ListFromMatrix(C),
                        D = ListFromMatrix(D),
                        bridges,
                        places,
                        links
                    };

                    using (StreamWriter write = new StreamWriter(saveFileDialog1.FileName))
                    {
                        write.Write(JsonConvert.SerializeObject(save, Formatting.Indented));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Невозможно выполнить сохранение!");
            }
        }

        /// <summary>
        /// Закрывает приложение
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">E</param>
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Изменение приближение и отдаления сети
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            zoom = trackBar1.Value;
            zoom = zoom < 10 ? 10 : zoom;
            zoom = zoom > 200 ? 200 : zoom;
            label1.Text = zoom.ToString();
            zoomf = zoom / (float)100.0;
            pictureBox1.Refresh();
        }

        /// <summary>
        /// Запись в матрицу по двум спискам переходов
        /// </summary>
        /// <param name="bridges1">Первый список переходов</param>
        /// <param name="bridges2">Второй список переходов</param>
        /// <param name="m">Матрица, в которую будет произведена запись</param>
        private void CreateMatrixFromBridges(List<Bridge> bridges1, List<Bridge> bridges2, Matrix m)
        {
            m.Value = new List<List<Tuple<string, string>>>();
            for (int i = 0; i < bridges1.Count; i++)
            {
                List<Tuple<string, string>> t = new List<Tuple<string, string>>();
                for (int j = 0; j < bridges2.Count; j++)
                {
                    bool was = false;
                    foreach (var link1 in links.Where(l => l.Bridge == bridges1[i] && !l.FromPlace))
                    {
                        foreach (var link2 in links.Where(l => l.Bridge == bridges2[j] && l.FromPlace))
                        {
                            if (link1.Place == link2.Place)
                            {
                                was = true;
                                t.Add(new Tuple<string, string>(link1.Place.Amount.ToString(), link1.Place.Await.ToString()));
                            }
                        }
                    }
                    if (!was)
                    {
                        t.Add(new Tuple<string, string>("+∞", "-∞"));
                    }
                }
                m.Value.Add(t);
            }
        }

        /// <summary>
        /// Список из матрицы
        /// </summary>
        /// <param name="M">Матрица</param>
        /// <returns>Список</returns>
        private List<List<List<string>>> ListFromMatrix(Matrix M)
        {
            List < List <List<string>>> t = new List<List<List<string>>>();

            foreach (var l1 in M.Value)
            {
                List<List<string>> t2 = new List<List<string>>();
                foreach(var l2 in l1)
                {
                    List<string> t1 = new List<string>();
                    t1.Add(l2.Item1);
                    t1.Add(l2.Item2);
                    t2.Add(t1);
                }
                t.Add(t2);
            }

            return t;
        }

        /// <summary>
        /// Матрица из списка
        /// </summary>
        /// <param name="list">список</param>
        /// <returns>Матрица</returns>
        private Matrix MatrixFromList(List<List<List<string>>> list)
        {
            List<List<Tuple<string, string>>> t = new List<List<Tuple<string, string>>>();

            foreach (var l1 in list)
            {
                List<Tuple<string, string>> t2 = new List<Tuple<string, string>>();
                foreach (var l2 in l1)
                {
                    t2.Add(new Tuple<string, string>(l2[0], l2[1]));
                }
                t.Add(t2);
            }

            Matrix m = new Matrix();
            m.Value = t;

            return m;
        }
    }
}

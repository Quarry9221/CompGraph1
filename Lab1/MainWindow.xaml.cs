using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int canvas1width;
        private int canvas1height;
        private int pixel = 25;
        private int degreebegin = 180;
        private int degreeend = 360;
        private double resolution;
        private double r;
        private double gamma = 0;
        private double theta = 0;
        private double left;
        private double top;
        private double leftPositionCenter;
        private double topPositionCenter;
        private Ellipse pivotpoint = new Ellipse
        {
            Width = 7,
            Height = 7,
            Fill = Brushes.Pink,
            Stroke = Brushes.Green,
        };
        Point a;
        Point b;
        Point c;
        Point d;
        Point e;
        Point f;
        Point g;
        Point h;
        Point i;
        Point cent;

        Line line1;
        Line line2;
        Line line3;
        Line line4;
        Line line5;
        Line line6;
        Line line7;
        Line line8;
        Line line9;

        public void Createline(Canvas canvas, Line line, SolidColorBrush brush, double x1, double y1, double x2, double y2) //функція для побудови ліній
        {
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = brush;
            line.StrokeThickness = 1;
            canvas.Children.Add(line);
        }
        public void CreateCustomArc(Canvas canvas, Line line, Point p, SolidColorBrush brush, int degreebegin, int degreend)
        {
            for(int j = degreebegin ; j <= degreend; j++)
            {
                gamma = degtoRad(j);
                theta = degtoRad(j + 1);
                Createline(canvas, new Line(), new SolidColorBrush(Colors.Black),
                    r * Math.Cos(gamma) + p.X, r * Math.Sin(gamma) + p.Y,
                    r * Math.Cos(theta) + p.X, r * Math.Sin(theta) + p.Y);
            }
        }
        public void Transform(Point p1, Point p2, double xvector, double yvector)
        {
            p1.X += xvector;
            p1.Y += yvector;
            p2.X += xvector;
            p2.Y += yvector;

            Createline(BlankCanvas, new Line(), new SolidColorBrush(Colors.Black), p1.X - (canvas2.Width/2), p1.Y - (canvas2.Height/2), p2.X - (canvas2.Width/2), p2.Y - (canvas2.Height/2));
        }
        public void TransformArc(Point p1, double xvector, double yvector)
        {
            p1.X = p1.X + xvector - (canvas2.Width / 2);
            p1.Y = p1.Y + yvector - (canvas2.Height / 2);
            CreateCustomArc(BlankCanvas, new Line(), p1, new SolidColorBrush(Colors.Black), degreebegin, degreeend);

        }
        public double degtoRad(int i)
        {
            return Math.PI * i / 180;
        }

        public double Pbeg(double x, double y, double gamma)
        {
            return Pbeg(r * Math.Cos(gamma) + cent.X, r * Math.Sin(gamma) + cent.Y, gamma);
        }
        public double Pend(double x, double y, double theta)
        {
            return Pend(r * Math.Cos(theta) + cent.X, r * Math.Sin(theta) + cent.Y, theta);
        }
        public double SizeinMM(int size)
        {
            return size * resolution;
        }

        int[,] arr = new int[3, 3];
        public MainWindow()
        {
            SolidColorBrush blackbrush = new SolidColorBrush();

            InitializeComponent();
            resolution = pixel/10.0;
            left = canvas2.Width / 2;
            top = canvas2.Height / 2;

            ////////Початок поля/////////
            canvas1width = (int)canvas1.Width;
            canvas1height = (int)canvas1.Height;
            int countwidth = canvas1width / pixel; //кількість вертикальних ліній
            int countheight = canvas1height / pixel; //кількість горизонтальних ліній
            r = (int)(20 * resolution);
            for (int j = 0; j < 3; j++) //одинична матриця
            {
                for (int k = 0; k < 3; k++)
                {
                    if (j == k) arr[j, k] = 1;
                    else arr[j, k] = 0;
                }
            }
            Createline(coordCanvas, new Line(), new SolidColorBrush(Colors.Green), 0, 0, 0, canvas1height); //вертикальна лінія осі
            Createline(coordCanvas, new Line(), new SolidColorBrush(Colors.Red), 0, 0, canvas1width, 0); //горизонтальна лінія осі

            for (int j = 1; j < countwidth + 1; j++) //лінії клітинок по вертикалі
            {
                Line vertG = new Line();
                Createline(coordCanvas, vertG, new SolidColorBrush(Colors.LightGray), j * pixel, 0, j * pixel, canvas1height);
            }

            for (int j = 1; j < countheight + 1; j++) //лінії клитинок по горизонталі
            {
                Line horG = new Line();
                Createline(coordCanvas, horG, new SolidColorBrush(Colors.LightGray), 0, j * pixel, canvas1width, j * pixel);
            }
            Canvas.SetLeft(pivotpoint, left);
            Canvas.SetTop(pivotpoint, top);

            canvas2.Children.Add(pivotpoint);
            ////////Кінець поля/////////
            a = new Point(0, 0);
            b = new Point(0, 0 + SizeinMM(16));
            c = new Point(0 + SizeinMM(32), 0 + SizeinMM(16));
            d = new Point(0 + SizeinMM(45), 0 + SizeinMM(60));
            e = new Point(0 + SizeinMM(54), 0 + SizeinMM(60));
            f = new Point(SizeinMM(116) - SizeinMM(22), SizeinMM(60));
            g = new Point(SizeinMM(116), SizeinMM(60));
            h = new Point(SizeinMM(116), 0);
            i = new Point(0 + SizeinMM(32), 0);
            cent = new Point(SizeinMM(116) - SizeinMM(42), SizeinMM(60));
            line1 = new Line();
            line2 = new Line();
            line3 = new Line();
            line4 = new Line();
            line5 = new Line();
            line6 = new Line();
            line7 = new Line();
            line8 = new Line();
            line9 = new Line();
            Createline(canvas2, line1, new SolidColorBrush(Colors.Black), a.X, a.Y, b.X, b.Y);//ліва сторона прямокутника AB
            Createline(canvas2, line2, new SolidColorBrush(Colors.Black), b.X, b.Y, c.X, c.Y);//верхня сторона прямокутника BC
            Createline(canvas2, line3, new SolidColorBrush(Colors.Black), c.X, c.Y, d.X, d.Y); //CD
            Createline(canvas2, line4, new SolidColorBrush(Colors.Black), d.X, d.Y, e.X, e.Y); //DE
            Createline(canvas2, line5, new SolidColorBrush(Colors.Black), g.X, g.Y, f.X, f.Y); //GF
            Createline(canvas2, line6, new SolidColorBrush(Colors.Black), g.X, g.Y, h.X, h.Y); //вертикальна лінія основи GH
            Createline(canvas2, line7, new SolidColorBrush(Colors.Black), c.X, c.Y, i.X, i.Y);//права сторона прямокутника CI
            Createline(canvas2, line8, new SolidColorBrush(Colors.Black), a.X, a.Y, h.X, h.Y);//нижня сторона основи AH
            CreateCustomArc(canvas2, line9, cent, new SolidColorBrush(Colors.Black), degreebegin, degreeend);
            PositionOfCenterX.Text = ((Canvas.GetTop(pivotpoint)) / pixel).ToString();
            PositionOfCenterY.Text = ((Canvas.GetTop(pivotpoint)) / pixel).ToString();
        }
        

        private void Button_Click(object sender, RoutedEventArgs ev)
        {
            double xvec = Convert.ToDouble(transformX.Text) * pixel;
            double yvec = Convert.ToDouble(transformY.Text) * pixel;

            canvas2.Children.Clear();
            BlankCanvas.Children.Clear();
            Canvas.SetLeft(pivotpoint, (left + xvec) - (canvas2.Width / 2));
            Canvas.SetTop(pivotpoint, (top + yvec) - (canvas2.Height/2));
            leftPositionCenter = Canvas.GetLeft(canvas2);
            topPositionCenter = Canvas.GetTop(canvas2);
            PositionOfCenterX.Text = ((Canvas.GetLeft(pivotpoint)) / pixel).ToString();
            PositionOfCenterY.Text = ((Canvas.GetTop(pivotpoint)) / pixel).ToString();
            Transform(a, b, xvec, yvec);
            Transform(b, c, xvec, yvec);
            Transform(c, d, xvec, yvec);
            Transform(d, e, xvec, yvec);
            Transform(g, f, xvec, yvec);
            Transform(g, h, xvec, yvec);
            Transform(a, h, xvec, yvec);
            Transform(c, i, xvec, yvec);
            TransformArc(cent, xvec, yvec);
            BlankCanvas.Children.Add(pivotpoint);

        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            PositionX.Text = (Mouse.GetPosition(coordCanvas).X / pixel).ToString();
            PositionY.Text = (Mouse.GetPosition(coordCanvas).Y / pixel).ToString();
        }

        private void canvas_DragOver(object sender, DragEventArgs e)
        {
            Point dropPositionCenterCanvas = e.GetPosition(canvas1);
            Canvas.SetLeft(canvas2, dropPositionCenterCanvas.X);
            Canvas.SetTop(canvas2, dropPositionCenterCanvas.Y);
        }

        private void Mouse_move(object sender, MouseEventArgs e)
        {
            if (e.LeftButton is MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(canvas2, canvas2, DragDropEffects.Move);
            }
        }

        private void ChangeClick(object sender, RoutedEventArgs ev)
        {
            canvas2.Children.Clear();
            pixel = Convert.ToInt32(pixelcm.Text);
            resolution = pixel / 10.0;
            a = new Point(0, 0);
            b = new Point(0, 0 + SizeinMM(16));
            c = new Point(0 + SizeinMM(32), 0 + SizeinMM(16));
            d = new Point(0 + SizeinMM(45), 0 + SizeinMM(60));
            e = new Point(0 + SizeinMM(54), 0 + SizeinMM(60));
            f = new Point(SizeinMM(116) - SizeinMM(22), SizeinMM(60));
            g = new Point(SizeinMM(116), SizeinMM(60));
            h = new Point(SizeinMM(116), 0);
            i = new Point(0 + SizeinMM(32), 0);
            r = SizeinMM(20);
            cent = new Point(SizeinMM(116) - SizeinMM(42), SizeinMM(60));

            Createline(canvas2, line1, new SolidColorBrush(Colors.Black), a.X, a.Y, b.X, b.Y);//ліва сторона прямокутника AB
            Createline(canvas2, line2, new SolidColorBrush(Colors.Black), b.X, b.Y, c.X, c.Y);//верхня сторона прямокутника BC
            Createline(canvas2, line3, new SolidColorBrush(Colors.Black), c.X, c.Y, d.X, d.Y); //CD
            Createline(canvas2, line4, new SolidColorBrush(Colors.Black), d.X, d.Y, e.X, e.Y); //DE
            Createline(canvas2, line5, new SolidColorBrush(Colors.Black), g.X, g.Y, f.X, f.Y); //GF
            Createline(canvas2, line6, new SolidColorBrush(Colors.Black), g.X, g.Y, h.X, h.Y); //вертикальна лінія основи GH
            Createline(canvas2, line7, new SolidColorBrush(Colors.Black), c.X, c.Y, i.X, i.Y);//права сторона прямокутника CI
            Createline(canvas2, line8, new SolidColorBrush(Colors.Black), a.X, a.Y, h.X, h.Y);//нижня сторона основи AH
            CreateCustomArc(canvas2, line9, cent, new SolidColorBrush(Colors.Black), degreebegin, degreeend);
        }
    }
}

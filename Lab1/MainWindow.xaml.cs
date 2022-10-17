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
using static System.Formats.Asn1.AsnWriter;
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
        Drawer drawer;
        CoordSystem coordinateSystemPointsFinder;
        FigureDrawer figureDrawer;
        SolidColorBrush blackbrush = new SolidColorBrush();
        static int RESOLUTION = 25;
        
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


        private double[,] transformationMatrix;

        public List<Point> mainFigurePart = new List<Point>();

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

            InitializeComponent();

            left = coordCanvas.Width / 2;
            top = coordCanvas.Height / 2;
            drawer = new Drawer(coordCanvas);
            coordinateSystemPointsFinder = new CoordSystem(drawer, coordCanvas, RESOLUTION);
            figureDrawer = new FigureDrawer(coordCanvas, drawer,
                                           CmToPixels(Double.Parse(SizeArc.Text)),
                                           CmToPixels(Double.Parse(SizeA.Text)), CmToPixels(Double.Parse(SizeB.Text)), RESOLUTION);

            DrawScene();
        }
        private void DrawScene()
        {
            coordinateSystemPointsFinder.DrawCoordinateSystem();
            figureDrawer.DrawFigure();
        }


     



        private void Button_ChangeSize_Click(object sender, RoutedEventArgs ev)
        {
            drawer = new Drawer(coordCanvas);

            figureDrawer = new FigureDrawer(coordCanvas, drawer,
                                           CmToPixels(Double.Parse(SizeArc.Text)),
                                           CmToPixels(Double.Parse(SizeA.Text)), CmToPixels(Double.Parse(SizeB.Text)), RESOLUTION);
            coordCanvas.Children.Clear();
            DrawScene();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            PositionX.Text = (Math.Round(Mouse.GetPosition(coordCanvas).X / pixel, 2)).ToString();
            PositionY.Text = (Math.Round(Mouse.GetPosition(coordCanvas).Y / pixel, 2)).ToString();
        }

        private void canvas_DragOver(object sender, DragEventArgs e)
        {
            Point dropPositionCenterCanvas = e.GetPosition(canvas1);
            Canvas.SetLeft(coordCanvas, dropPositionCenterCanvas.X);
            Canvas.SetTop(coordCanvas, dropPositionCenterCanvas.Y);
        }

        private void Mouse_move(object sender, MouseEventArgs e)
        {
            if (e.LeftButton is MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(coordCanvas, coordCanvas, DragDropEffects.Move);
            }
        }

        private void Button_Default_Click(object sender, RoutedEventArgs e)
        {
            SizeArc.Text = "2";
            SizeA.Text = "3,2";
            SizeB.Text = "3,2";
            pixelcm.Text = "25";
            RESOLUTION = 25;
            Canvas.SetLeft(pivot, 5 * RESOLUTION);
            Canvas.SetTop(pivot, 5 * RESOLUTION);

            coordinateSystemPointsFinder = new CoordSystem(drawer, coordCanvas, RESOLUTION);

            figureDrawer = new FigureDrawer(coordCanvas, drawer,
                                           CmToPixels(Double.Parse(SizeArc.Text)),
                                           CmToPixels(Double.Parse(SizeA.Text)), CmToPixels(Double.Parse(SizeB.Text)), RESOLUTION);
            coordCanvas.Children.Clear();
            coordCanvas.Children.Add(pivot);
            DrawScene();
        }

        private void ChangeEuclid_Click(object sender, RoutedEventArgs e)
        {
            Euclid euclideanTransformation = new Euclid();
            figureDrawer.mainFigurePart = euclideanTransformation.Translate(figureDrawer.mainFigurePart, CmToPixels(Double.Parse(transformX.Text)), CmToPixels(Double.Parse(transformY.Text)));
          
            figureDrawer.additionalFigurePart = euclideanTransformation.Translate(figureDrawer.additionalFigurePart, CmToPixels(Double.Parse(transformX.Text)), CmToPixels(Double.Parse(transformY.Text)));
            coordCanvas.Children.Clear();
            DrawScene();
        }

        private void ChangeRotate_Click(object sender, RoutedEventArgs e)
        {
            double xvec = CmToPixels(Convert.ToDouble(XPoint.Text));
            double yvec = CmToPixels(Convert.ToDouble(YPoint.Text));

            Canvas.SetLeft(pivot, xvec);
            Canvas.SetTop(pivot, yvec);

            Euclid euclideanTransformation = new Euclid();
            figureDrawer.mainFigurePart = euclideanTransformation.Rotate(figureDrawer.mainFigurePart, Double.Parse(degree.Text), CmToPixels(Double.Parse(XPoint.Text)), CmToPixels(Double.Parse(YPoint.Text)));
            figureDrawer.additionalFigurePart = euclideanTransformation.Rotate(figureDrawer.additionalFigurePart, Double.Parse(degree.Text), CmToPixels(Double.Parse(XPoint.Text)), CmToPixels(Double.Parse(YPoint.Text)));
            coordCanvas.Children.Clear();

            
            coordCanvas.Children.Add(pivot);
            DrawScene();
        }

        
        private void Affine_Click(object sender, RoutedEventArgs e)
        {
            Affine affineTransformation = new Affine();
            figureDrawer.mainFigurePart = affineTransformation.ChangeCoordinateSystem(figureDrawer.mainFigurePart,
                                                                                      Double.Parse(Xx.Text), Double.Parse(Xy.Text),
                                                                                      Double.Parse(Yx.Text), Double.Parse(Yy.Text),
                                                                                      Double.Parse(Ox.Text), Double.Parse(Oy.Text));
            figureDrawer.additionalFigurePart = affineTransformation.ChangeCoordinateSystem(figureDrawer.additionalFigurePart,
                                                                          Double.Parse(Xx.Text), Double.Parse(Xy.Text),
                                                                          Double.Parse(Yx.Text), Double.Parse(Yy.Text),
                                                                          Double.Parse(Ox.Text), Double.Parse(Oy.Text));
            coordinateSystemPointsFinder.systemPoints = affineTransformation.ChangeCoordinateSystem(coordinateSystemPointsFinder.systemPoints,
                                                              Double.Parse(Xx.Text), Double.Parse(Xy.Text),
                                                              Double.Parse(Yx.Text), Double.Parse(Yy.Text),
                                                              Double.Parse(Ox.Text), Double.Parse(Oy.Text));
            coordCanvas.Children.Clear();
            DrawScene();
        }
        
        
        private void Projective_Click(object sender, RoutedEventArgs e)
        {
            Projective projectiveTransformation = new Projective();

            figureDrawer.mainFigurePart = projectiveTransformation.ChangeCoordinateSystem(figureDrawer.mainFigurePart,
                                                                            Double.Parse(XXP.Text), Double.Parse(XYP.Text), Double.Parse(XWP.Text),
                                                                            Double.Parse(YXP.Text), Double.Parse(YYP.Text), Double.Parse(YWP.Text),
                                                                            Double.Parse(OXP.Text), Double.Parse(OYP.Text), Double.Parse(OWP.Text));
            figureDrawer.additionalFigurePart = projectiveTransformation.ChangeCoordinateSystem(figureDrawer.additionalFigurePart,
                                                                            Double.Parse(XXP.Text), Double.Parse(XYP.Text), Double.Parse(XWP.Text),
                                                                            Double.Parse(YXP.Text), Double.Parse(YYP.Text), Double.Parse(YWP.Text),
                                                                            Double.Parse(OXP.Text), Double.Parse(OYP.Text), Double.Parse(OWP.Text));
            coordinateSystemPointsFinder.systemPoints = projectiveTransformation.ChangeCoordinateSystem(coordinateSystemPointsFinder.systemPoints,
                                                                            Double.Parse(XXP.Text), Double.Parse(XYP.Text), Double.Parse(XWP.Text),
                                                                            Double.Parse(YXP.Text), Double.Parse(YYP.Text), Double.Parse(YWP.Text),
                                                                            Double.Parse(OXP.Text), Double.Parse(OYP.Text), Double.Parse(OWP.Text));
            coordCanvas.Children.Clear();

            DrawScene();
        }

        //private void Change_Zoom_Click(object sender, RoutedEventArgs e)
        //{
        //    Affine affineTransformation = new Affine();
        //    figureDrawer.mainFigurePart = affineTransformation.Zoom(figureDrawer.mainFigurePart, Double.Parse(pixelcm.Text));
        //    figureDrawer.additionalFigurePart = affineTransformation.Zoom(figureDrawer.additionalFigurePart, Double.Parse(pixelcm.Text));
        //    coordinateSystemPointsFinder.systemPoints = affineTransformation.Zoom(coordinateSystemPointsFinder.systemPoints, Double.Parse(pixelcm.Text));
        //    coordCanvas.Children.Clear();
        //    DrawScene();
        //}
        private void Change_Zoom_Click(object sender, RoutedEventArgs e)
        {
            RESOLUTION = Int32.Parse(pixelcm.Text);
            drawer = new Drawer(coordCanvas);
            double xvec = CmToPixels(Convert.ToDouble(XPoint.Text));
            double yvec = CmToPixels(Convert.ToDouble(YPoint.Text));

            Canvas.SetLeft(pivot, xvec);
            Canvas.SetTop(pivot, yvec);
            coordinateSystemPointsFinder = new CoordSystem(drawer, coordCanvas, RESOLUTION);
            
            figureDrawer = new FigureDrawer(coordCanvas, drawer,
                                           CmToPixels(Double.Parse(SizeArc.Text)),
                                           CmToPixels(Double.Parse(SizeA.Text)), CmToPixels(Double.Parse(SizeB.Text)), RESOLUTION);
            coordCanvas.Children.Clear();
            coordCanvas.Children.Add(pivot);
            DrawScene();
        }


        double CmToPixels(double value) => value * RESOLUTION;





        double[] Multiplicate(double[] a, double[,] b)
        {
            double[] r = new double[a.Length];
            for (int i = 0; i < b.GetLength(1); i++)
            {
                for (int j = 0; j < b.GetLength(0); j++)
                {
                    r[j] = 0;
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[j] += a[k] * b[k, j];
                    }
                }
            }
            return r;
        }

    }
}

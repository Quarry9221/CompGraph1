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
        

        
        public MainWindow()
        {

            InitializeComponent();

            left = coordCanvas.Width / 2;
            top = coordCanvas.Height / 2;
            drawer = new Drawer(coordCanvas);
            coordinateSystemPointsFinder = new CoordSystem(drawer, coordCanvas, RESOLUTION);
            figureDrawer = new FigureDrawer(coordCanvas, drawer,
                                           CmToPixels(Double.Parse(SizeArc.Text)), CmToPixels(Double.Parse(bottomArc.Text)),
                                           RESOLUTION, CmToPixels(Double.Parse(leftCircle.Text)), CmToPixels(Double.Parse(rightCircle.Text)),
                                           CmToPixels(Double.Parse(middlecircle.Text)));
            
            Canvas.SetLeft(pivot, 15.5 * RESOLUTION);
            Canvas.SetTop(pivot, 8.5 * RESOLUTION);

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
                                           CmToPixels(Double.Parse(SizeArc.Text)), CmToPixels(Double.Parse(bottomArc.Text)),
                                           RESOLUTION, CmToPixels(Double.Parse(leftCircle.Text)), CmToPixels(Double.Parse(rightCircle.Text)), 
                                           CmToPixels(Double.Parse(middlecircle.Text)));
            
            coordCanvas.Children.Clear();
            coordCanvas.Children.Add(pivot);
            DrawScene();
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
            bottomArc.Text = "2";
            leftCircle.Text = "0,5";
            rightCircle.Text = "0,5";
            middlecircle.Text = "1,5";
            pixelcm.Text = "25";
            RESOLUTION = 25;
            Canvas.SetLeft(pivot, 15.5 * RESOLUTION);
            Canvas.SetTop(pivot, 8.5 * RESOLUTION);

            coordinateSystemPointsFinder = new CoordSystem(drawer, coordCanvas, RESOLUTION);

            figureDrawer = new FigureDrawer(coordCanvas, drawer,
                                           CmToPixels(Double.Parse(SizeArc.Text)), CmToPixels(Double.Parse(bottomArc.Text)), RESOLUTION,
                                           CmToPixels(Double.Parse(leftCircle.Text)), CmToPixels(Double.Parse(rightCircle.Text)),
                                           CmToPixels(Double.Parse(middlecircle.Text)));
            
            coordCanvas.Children.Clear();
            
            coordCanvas.Children.Add(pivot);
            DrawScene();
        }

        private void ChangeEuclid_Click(object sender, RoutedEventArgs e)
        {
            Euclid euclideanTransformation = new Euclid();
            figureDrawer.mainFigurePart = euclideanTransformation.Translate(figureDrawer.mainFigurePart, CmToPixels(Double.Parse(transformX.Text)), CmToPixels(Double.Parse(transformY.Text)));
          
            figureDrawer.additionalFigurePart = euclideanTransformation.Translate(figureDrawer.additionalFigurePart, CmToPixels(Double.Parse(transformX.Text)), CmToPixels(Double.Parse(transformY.Text)));
            figureDrawer.leftCircle = euclideanTransformation.Translate(figureDrawer.leftCircle, CmToPixels(Double.Parse(transformX.Text)), CmToPixels(Double.Parse(transformY.Text)));
            figureDrawer.rightCircle = euclideanTransformation.Translate(figureDrawer.rightCircle, CmToPixels(Double.Parse(transformX.Text)), CmToPixels(Double.Parse(transformY.Text)));
            figureDrawer.middleCircle = euclideanTransformation.Translate(figureDrawer.middleCircle, CmToPixels(Double.Parse(transformX.Text)), CmToPixels(Double.Parse(transformY.Text)));
            coordCanvas.Children.Clear();

            coordCanvas.Children.Add(pivot);
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
            figureDrawer.leftCircle = euclideanTransformation.Rotate(figureDrawer.leftCircle, Double.Parse(degree.Text), CmToPixels(Double.Parse(XPoint.Text)), CmToPixels(Double.Parse(YPoint.Text)));
            figureDrawer.rightCircle = euclideanTransformation.Rotate(figureDrawer.rightCircle, Double.Parse(degree.Text), CmToPixels(Double.Parse(XPoint.Text)), CmToPixels(Double.Parse(YPoint.Text)));
            figureDrawer.middleCircle = euclideanTransformation.Rotate(figureDrawer.middleCircle, Double.Parse(degree.Text), CmToPixels(Double.Parse(XPoint.Text)), CmToPixels(Double.Parse(YPoint.Text)));
            coordCanvas.Children.Clear();

            
            coordCanvas.Children.Add(pivot);
            DrawScene();
        }

        
        private void Affine_Click(object sender, RoutedEventArgs e)
        {
            Affine affineTransformation = new Affine();
            figureDrawer.mainFigurePart = affineTransformation.ChangeCoordinateSystem(figureDrawer.mainFigurePart,
                                                                                      CmToPixels(Double.Parse(Xx.Text)), CmToPixels(Double.Parse(Xy.Text)),
                                                                                      CmToPixels(Double.Parse(Yx.Text)), CmToPixels(Double.Parse(Yy.Text)),
                                                                                      CmToPixels(Double.Parse(Ox.Text)), CmToPixels(Double.Parse(Oy.Text)));
            figureDrawer.additionalFigurePart = affineTransformation.ChangeCoordinateSystem(figureDrawer.additionalFigurePart,
                                                                          CmToPixels(Double.Parse(Xx.Text)), CmToPixels(Double.Parse(Xy.Text)),
                                                                          CmToPixels(Double.Parse(Yx.Text)), CmToPixels(Double.Parse(Yy.Text)),
                                                                          CmToPixels(Double.Parse(Ox.Text)), CmToPixels(Double.Parse(Oy.Text)));
            figureDrawer.leftCircle = affineTransformation.ChangeCoordinateSystem(figureDrawer.leftCircle,
                                                              CmToPixels(Double.Parse(Xx.Text)), CmToPixels(Double.Parse(Xy.Text)),
                                                              CmToPixels(Double.Parse(Yx.Text)), CmToPixels(Double.Parse(Yy.Text)),
                                                              CmToPixels(Double.Parse(Ox.Text)), CmToPixels(Double.Parse(Oy.Text)));
            figureDrawer.rightCircle = affineTransformation.ChangeCoordinateSystem(figureDrawer.rightCircle,
                                                              CmToPixels(Double.Parse(Xx.Text)), CmToPixels(Double.Parse(Xy.Text)),
                                                              CmToPixels(Double.Parse(Yx.Text)), CmToPixels(Double.Parse(Yy.Text)),
                                                              CmToPixels(Double.Parse(Ox.Text)), CmToPixels(Double.Parse(Oy.Text)));
            figureDrawer.middleCircle = affineTransformation.ChangeCoordinateSystem(figureDrawer.middleCircle,
                                                              CmToPixels(Double.Parse(Xx.Text)), CmToPixels(Double.Parse(Xy.Text)),
                                                              CmToPixels(Double.Parse(Yx.Text)), CmToPixels(Double.Parse(Yy.Text)),
                                                              CmToPixels(Double.Parse(Ox.Text)), CmToPixels(Double.Parse(Oy.Text)));
            coordinateSystemPointsFinder.systemPoints = affineTransformation.ChangeCoordinateSystem(coordinateSystemPointsFinder.systemPoints,
                                                              CmToPixels(Double.Parse(Xx.Text)), CmToPixels(Double.Parse(Xy.Text)),
                                                              CmToPixels(Double.Parse(Yx.Text)), CmToPixels(Double.Parse(Yy.Text)),
                                                              CmToPixels(Double.Parse(Ox.Text)), CmToPixels(Double.Parse(Oy.Text)));
            coordCanvas.Children.Clear();
            DrawScene();
        }
        
        
        private void Projective_Click(object sender, RoutedEventArgs e)
        {
            Projective projectiveTransformation = new Projective();

            figureDrawer.mainFigurePart = projectiveTransformation.ChangeCoordinateSystem(figureDrawer.mainFigurePart,
                                                                            CmToPixels (Double.Parse(XXP.Text)), CmToPixels(Double.Parse(XYP.Text)), CmToPixels(Double.Parse(XWP.Text)),
                                                                            CmToPixels(Double.Parse(YXP.Text)), CmToPixels(Double.Parse(YYP.Text)), CmToPixels(Double.Parse(YWP.Text)),
                                                                            CmToPixels(Double.Parse(OXP.Text)), CmToPixels(Double.Parse(OYP.Text)), CmToPixels(Double.Parse(OWP.Text)));
            figureDrawer.additionalFigurePart = projectiveTransformation.ChangeCoordinateSystem(figureDrawer.additionalFigurePart,
                                                                            CmToPixels(Double.Parse(XXP.Text)), CmToPixels(Double.Parse(XYP.Text)), CmToPixels(Double.Parse(XWP.Text)),
                                                                            CmToPixels(Double.Parse(YXP.Text)), CmToPixels(Double.Parse(YYP.Text)), CmToPixels(Double.Parse(YWP.Text)),
                                                                            CmToPixels(Double.Parse(OXP.Text)), CmToPixels(Double.Parse(OYP.Text)), CmToPixels(Double.Parse(OWP.Text)));
            figureDrawer.leftCircle = projectiveTransformation.ChangeCoordinateSystem(figureDrawer.leftCircle,
                                                                CmToPixels(Double.Parse(XXP.Text)), CmToPixels(Double.Parse(XYP.Text)), CmToPixels(Double.Parse(XWP.Text)),
                                                                CmToPixels(Double.Parse(YXP.Text)), CmToPixels(Double.Parse(YYP.Text)), CmToPixels(Double.Parse(YWP.Text)),
                                                                CmToPixels(Double.Parse(OXP.Text)), CmToPixels(Double.Parse(OYP.Text)), CmToPixels(Double.Parse(OWP.Text)));
            figureDrawer.rightCircle = projectiveTransformation.ChangeCoordinateSystem(figureDrawer.rightCircle,
                                                                CmToPixels(Double.Parse(XXP.Text)), CmToPixels(Double.Parse(XYP.Text)), CmToPixels(Double.Parse(XWP.Text)),
                                                                CmToPixels(Double.Parse(YXP.Text)), CmToPixels(Double.Parse(YYP.Text)), CmToPixels(Double.Parse(YWP.Text)),
                                                                CmToPixels(Double.Parse(OXP.Text)), CmToPixels(Double.Parse(OYP.Text)), CmToPixels(Double.Parse(OWP.Text)));
            figureDrawer.middleCircle = projectiveTransformation.ChangeCoordinateSystem(figureDrawer.middleCircle,
                                                                CmToPixels(Double.Parse(XXP.Text)), CmToPixels(Double.Parse(XYP.Text)), CmToPixels(Double.Parse(XWP.Text)),
                                                                CmToPixels(Double.Parse(YXP.Text)), CmToPixels(Double.Parse(YYP.Text)), CmToPixels(Double.Parse(YWP.Text)),
                                                                CmToPixels(Double.Parse(OXP.Text)), CmToPixels(Double.Parse(OYP.Text)), CmToPixels(Double.Parse(OWP.Text)));
            coordinateSystemPointsFinder.systemPoints = projectiveTransformation.ChangeCoordinateSystem(coordinateSystemPointsFinder.systemPoints,
                                                                            CmToPixels(Double.Parse(XXP.Text)), CmToPixels(Double.Parse(XYP.Text)), CmToPixels(Double.Parse(XWP.Text)),
                                                                            CmToPixels(Double.Parse(YXP.Text)), CmToPixels(Double.Parse(YYP.Text)), CmToPixels(Double.Parse(YWP.Text)),
                                                                            CmToPixels(Double.Parse(OXP.Text)), CmToPixels(Double.Parse(OYP.Text)), CmToPixels(Double.Parse(OWP.Text)));
            coordCanvas.Children.Clear();

            DrawScene();
        }

        private void Change_Zoom_Click(object sender, RoutedEventArgs e)
        {
            Euclid euclidTransformation = new Euclid();
            figureDrawer.mainFigurePart = euclidTransformation.Zoom(figureDrawer.mainFigurePart, Double.Parse(pixelcm.Text));
            figureDrawer.additionalFigurePart = euclidTransformation.Zoom(figureDrawer.additionalFigurePart, Double.Parse(pixelcm.Text));
            figureDrawer.leftCircle = euclidTransformation.Zoom(figureDrawer.leftCircle, Double.Parse(pixelcm.Text));
            figureDrawer.rightCircle = euclidTransformation.Zoom(figureDrawer.rightCircle, Double.Parse(pixelcm.Text));
            figureDrawer.middleCircle = euclidTransformation.Zoom(figureDrawer.middleCircle, Double.Parse(pixelcm.Text));
            coordinateSystemPointsFinder.systemPoints = euclidTransformation.Zoom(coordinateSystemPointsFinder.systemPoints, Double.Parse(pixelcm.Text));
            coordCanvas.Children.Clear();
            DrawScene();
        }
        //private void Change_Zoom_Click(object sender, RoutedEventArgs e)
        //{
        //    RESOLUTION = Int32.Parse(pixelcm.Text);
        //    drawer = new Drawer(coordCanvas);
        //    double xvec = CmToPixels(Convert.ToDouble(XPoint.Text));
        //    double yvec = CmToPixels(Convert.ToDouble(YPoint.Text));

        //    Canvas.SetLeft(pivot, xvec);
        //    Canvas.SetTop(pivot, yvec);
        //    coordinateSystemPointsFinder = new CoordSystem(drawer, coordCanvas, RESOLUTION);

        //    figureDrawer = new FigureDrawer(coordCanvas, drawer,
        //                                   CmToPixels(Double.Parse(SizeArc.Text)),
        //                                   CmToPixels(Double.Parse(SizeA.Text)), CmToPixels(Double.Parse(SizeB.Text)), RESOLUTION);
        //    coordCanvas.Children.Clear();
        //    coordCanvas.Children.Add(pivot);
        //    DrawScene();
        //}


        double CmToPixels(double value) => value * RESOLUTION;

    }
}

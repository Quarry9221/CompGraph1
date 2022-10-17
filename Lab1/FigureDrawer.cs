using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab1
{
    internal class FigureDrawer
    {

        Drawer drawer;
        int pixel;
        public List<Point> mainFigurePart = new List<Point>();
        public List<Point> additionalFigurePart = new List<Point>();
        CirclePointsFinder circlePoints = new CirclePointsFinder();
        public FigureDrawer(Canvas scene, Drawer drawer, double r1, double s1, double s2, int pixel)
        {
            this.pixel = pixel;
            List<Point> bigCirclePoints = circlePoints.GetCirclePoints(new Point(CmToPixels(15.4), CmToPixels(9)), r1, 180, 360);
            mainFigurePart = mainFigurePart.Concat(bigCirclePoints).ToList();
            mainFigurePart.Add(bigCirclePoints.Last());
            mainFigurePart.Add(new Point(CmToPixels(19.6), CmToPixels(9))); //490 225
            mainFigurePart.Add(new Point(CmToPixels(19.6), CmToPixels(3)));
            mainFigurePart.Add(new Point(CmToPixels(10.4), CmToPixels(3)));
            mainFigurePart.Add(new Point(CmToPixels(10.4), CmToPixels(4.6)));
            mainFigurePart.Add(new Point(CmToPixels(12.5), CmToPixels(9)));
            mainFigurePart.Add(bigCirclePoints.First());
            additionalFigurePart.Add(new Point(CmToPixels(10.4) - s1, CmToPixels(3)));  //ліва сторона
            additionalFigurePart.Add(new Point(CmToPixels(10.4) - s2, CmToPixels(4.6)));

            additionalFigurePart.Add(new Point(CmToPixels(10.4), CmToPixels(4.6)));
            additionalFigurePart.Add(new Point(CmToPixels(10.4) - s2, CmToPixels(4.6)));

            additionalFigurePart.Add(new Point(CmToPixels(10.4), CmToPixels(3)));
            additionalFigurePart.Add(new Point(CmToPixels(10.4) - s1, CmToPixels(3)));


            this.drawer = drawer;
        }


        public void DrawFigure()
        {
            drawer.DrawLinePointToPoint(mainFigurePart);

            drawer.DrawLineByTwoPoints(additionalFigurePart);
        }

        double CmToPixels(double value) => value * pixel;
    }
}

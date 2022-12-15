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


        public List<Point> leftCircle;
        public List<Point> rightCircle;
        public List<Point> middleCircle;
        CirclePointsFinder circlePoints = new CirclePointsFinder();
        public FigureDrawer(Canvas scene, Drawer drawer, double r1, double r2, int pixel, double radiusleftCircle1, double radiusrightCircle1, double middlecircle1)
        {
            this.pixel = pixel;
            List<Point> topArcPoints = circlePoints.GetCirclePoints(new Point(CmToPixels(15.4), CmToPixels(10)), r1, 65, 110);
            mainFigurePart = mainFigurePart.Concat(topArcPoints).ToList();
            mainFigurePart.Add(topArcPoints.Last());
            mainFigurePart.Add(new Point(CmToPixels(10.8), CmToPixels(10.5))); //left top


            List<Point> leftArcPoints = circlePoints.GetCirclePoints(new Point(CmToPixels(12.3), CmToPixels(9.2)), CmToPixels(2), 140, 220);
            mainFigurePart = mainFigurePart.Concat(leftArcPoints).ToList();
            mainFigurePart.Add(leftArcPoints.Last());


            List<Point> bottomArcPoints = circlePoints.GetCirclePoints(new Point(CmToPixels(15.4), CmToPixels(8)), r2, 245, 290);
            mainFigurePart = mainFigurePart.Concat(bottomArcPoints).ToList();
            mainFigurePart.Add(bottomArcPoints.Last());
            
            List<Point> rightArcPoints = circlePoints.GetCirclePoints(new Point(CmToPixels(18.9), CmToPixels(9)), CmToPixels(2), 320, 400);
            mainFigurePart = mainFigurePart.Concat(rightArcPoints).ToList();
            mainFigurePart.Add(rightArcPoints.Last());

            mainFigurePart.Add(topArcPoints.First()); //right top

            

            leftCircle = circlePoints.GetCirclePoints(new Point(CmToPixels(12), CmToPixels(9)), radiusleftCircle1, 0, 360);
            rightCircle = circlePoints.GetCirclePoints(new Point(CmToPixels(19), CmToPixels(9)), radiusrightCircle1, 0, 360);
            middleCircle = circlePoints.GetCirclePoints(new Point(CmToPixels(15.5), CmToPixels(9)), middlecircle1, 0, 360);
            //15.5x8.6
            this.drawer = drawer;
        }


        public void DrawFigure()
        {
            drawer.DrawLinePointToPoint(mainFigurePart);
            drawer.DrawLinePointToPoint(leftCircle);
            drawer.DrawLinePointToPoint(rightCircle);
            drawer.DrawLinePointToPoint(middleCircle);
            drawer.DrawLineByTwoPoints(additionalFigurePart);
        }

        double CmToPixels(double value) => value * pixel;
    }
}

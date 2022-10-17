using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1
{
    internal class CirclePointsFinder
    {
        double DegreeToRadian(double degree)
        {
            return (degree * Math.PI / 180);
        }

        public List<Point> GetCirclePoints(Point centerPoint, double radius, double startDeegre, double endDeegre)
        {
            List<Point> circlePoints = new List<Point>();
            double currentAngleInRad;
            double nextAngleInRad;
            for (double i = startDeegre; i <= endDeegre; i++)
            {
                currentAngleInRad = DegreeToRadian(i);
                nextAngleInRad = DegreeToRadian(i + 1);
                circlePoints.Add(new Point(radius * Math.Cos(currentAngleInRad) + centerPoint.X,
                                           radius * Math.Sin(currentAngleInRad) + centerPoint.Y));
                circlePoints.Add(new Point(radius * Math.Cos(nextAngleInRad) + centerPoint.X,
                                           radius * Math.Sin(nextAngleInRad) + centerPoint.Y));
            }
            return circlePoints;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1
{
    internal class Euclid
    {
        double DegreeToRadian(double degree)
        {
            return (degree * Math.PI / 180);
        }
        public List<Point> Translate(List<Point> listOfPoints, double m, double n)
        {
            List<Point> points = new List<Point>();
            double[,] transformationMatrix = { { 1, 0, 0 },
                                               { 0, 1, 0 },
                                               { m, n, 1 } };

            for (int i = 0; i < listOfPoints.Count; i++)
            {
                double[] pointCoordinates = { listOfPoints[i].X, listOfPoints[i].Y, 1 };
                double[] result = Multiplicate(pointCoordinates, transformationMatrix);
                points.Add(new Point(result[0], result[1]));
            }
            return points;
        }
        public List<Point> Rotate(List<Point> listOfPoints, double degree, double m, double n)
        {
            List<Point> points = new List<Point>();
            double[,] transformationMatrix = { {  Math.Cos(DegreeToRadian(degree)), Math.Sin(DegreeToRadian(degree)), 0 },
                                               { -Math.Sin(DegreeToRadian(degree)), Math.Cos(DegreeToRadian(degree)), 0 },
                                               { -m * (Math.Cos(DegreeToRadian(degree)) - 1) + n * Math.Sin(DegreeToRadian(degree)), -m * Math.Sin(DegreeToRadian(degree)) - n * (Math.Cos(DegreeToRadian(degree)) - 1), 1 } };

            for (int i = 0; i < listOfPoints.Count; i++)
            {
                double[] pointCoordinates = { listOfPoints[i].X, listOfPoints[i].Y, 1 };
                double[] result = Multiplicate(pointCoordinates, transformationMatrix);

                points.Add(new Point(result[0], result[1]));
            }
            return points;
        }

        public List<Point> Zoom(List<Point> listOfPoints, double m)
        {
            List<Point> points = new List<Point>();
            double[,] transformationMatrix = { { 1, 0, 0 },
                                               { 0, 1, 0 },
                                               { m, m, 1 } };
            
            for (int i = 0; i < listOfPoints.Count; i++)
            {
                double[] pointCoordinates = { listOfPoints[i].X, listOfPoints[i].Y, 1 };
                double[] result = Multiplicate(pointCoordinates, transformationMatrix);
                points.Add(new Point(result[0], result[1]));
            }
            return points;
        }

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

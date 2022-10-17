using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1
{
    internal class Affine
    {
        
        public List<Point> ChangeCoordinateSystem(List<Point> listOfPoints, double xx, double yx, double xy, double yy, double x0, double y0)
        {
            List<Point> points = new List<Point>();
            double[,] transformationMatrix = { { xx, yx, 0 },
                                               { xy, yy, 0 },
                                               { x0, y0, 1 } };

            for (int i = 0; i < listOfPoints.Count; i++)
            {
                double[] pointCoordinates = { listOfPoints[i].X, listOfPoints[i].Y, 1 };
                double[] result = Multiplicate(pointCoordinates, transformationMatrix);
                points.Add(new Point(result[0], result[1]));
            }
            return points;
        }
        public List<Point> XYSymmetry(List<Point> listOfPoints)
        {
            List<Point> points = new List<Point>();
            double[,] transformationMatrix = { { 1, 0, 0 },
                                               { 0, 1, 0 },
                                               { 0, 0, -1 } };

            for (int i = 0; i < listOfPoints.Count; i++)
            {
                double[] pointCoordinates = { listOfPoints[i].X, listOfPoints[i].Y, 1 };
                double[] result = Multiplicate(pointCoordinates, transformationMatrix);
                points.Add(new Point(result[0], result[1]));
            }
            return points;
        }

        public List<Point> YZSymmetry(List<Point> listOfPoints)
        {
            List<Point> points = new List<Point>();
            double[,] transformationMatrix = { { -1, 0, 0 },
                                               { 0, 1, 0 },
                                               { 0, 0, 1 } };

            for (int i = 0; i < listOfPoints.Count; i++)
            {
                double[] pointCoordinates = { listOfPoints[i].X, listOfPoints[i].Y, 1 };
                double[] result = Multiplicate(pointCoordinates, transformationMatrix);
                points.Add(new Point(result[0], result[1]));
            }
            return points;
        }

        public List<Point> XZSymmetry(List<Point> listOfPoints)
        {
            List<Point> points = new List<Point>();
            double[,] transformationMatrix = { { 1, 0, 0 },
                                               { 0, -1, 0 },
                                               { 0, 0, 1 } };

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
            double[,] transformationMatrix = { { m, 0, 0 },
                                               { 0, m, 0 },
                                               { 0, 0, m } };

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab1
{
    internal class Drawer
    {
        Canvas scene;

        public Drawer(Canvas scene)
        {
            this.scene = scene;
        }

        public void DrawCoordinateSystem(List<Point> listOfPoints)
        {
            Line xAxis = new Line
            {
                Stroke = Brushes.LightGreen,
                StrokeThickness = 5,
                X1 = listOfPoints[0].X,
                Y1 = listOfPoints[0].Y,
                X2 = listOfPoints[1].X,
                Y2 = listOfPoints[1].Y
            };
            Line yAxis = new Line
            {
                Stroke = Brushes.Red,
                StrokeThickness = 5,
                X1 = listOfPoints[0].X,
                Y1 = listOfPoints[0].Y,
                X2 = listOfPoints[2].X,
                Y2 = listOfPoints[2].Y
            };
            scene.Children.Add(xAxis);
            scene.Children.Add(yAxis);

            for (int i = 3; i < listOfPoints.Count; i += 2)
            {
                Line partOfSystem = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 0.1,
                    X1 = listOfPoints[i].X,
                    Y1 = listOfPoints[i].Y,
                    X2 = listOfPoints[i + 1].X,
                    Y2 = listOfPoints[i + 1].Y
                };
                scene.Children.Add(partOfSystem);
            }
        }
        public void DrawLinePointToPoint(List<Point> listOfPoints)
        {
            for (int i = 0; i < listOfPoints.Count - 1; i++)
            {
                Line partOfFigure = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    X1 = listOfPoints[i].X,
                    Y1 = listOfPoints[i].Y,
                    X2 = listOfPoints[i + 1].X,
                    Y2 = listOfPoints[i + 1].Y
                };
                scene.Children.Add(partOfFigure);
            }
        }
        public void DrawLineByTwoPoints(List<Point> listOfPoints)
        {
            for (int i = 0; i < listOfPoints.Count - 1; i += 2)
            {
                Line partOfSystem = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    X1 = listOfPoints[i].X,
                    Y1 = listOfPoints[i].Y,
                    X2 = listOfPoints[i + 1].X,
                    Y2 = listOfPoints[i + 1].Y
                };
                scene.Children.Add(partOfSystem);
            }
        }
    }
}

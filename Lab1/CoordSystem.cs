using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Lab1
{
    
    internal class CoordSystem
    {
        public List<Point> systemPoints = new List<Point>();
        Drawer drawer;
        Canvas scene;
        int pixel { get; set; }


        public CoordSystem(Drawer drawer, Canvas scene, int pixel)
        {
            systemPoints = GetCoordinateSystemPoints(new Point(0, 0), scene, pixel);
            this.drawer = drawer;
            this.scene = scene;
            this.pixel = pixel;
        }
        public List<Point> GetCoordinateSystemPoints(Point startPoint, Canvas scene, int pixel)
        {
            systemPoints.Clear();
            systemPoints.Add(new Point(startPoint.X, startPoint.Y));
            systemPoints.Add(new Point(startPoint.X + scene.Width, startPoint.Y));
            systemPoints.Add(new Point(startPoint.X, startPoint.Y + scene.Height));

            for (int i = 0; i < scene.Width; i += pixel)
            {
                systemPoints.Add(new Point(i, startPoint.Y));
                systemPoints.Add(new Point(i, scene.Height));
            }

            for (int i = 0; i < scene.Height; i += pixel)
            {
                systemPoints.Add(new Point(startPoint.X, i));
                systemPoints.Add(new Point(scene.Width, i));
            }
            return systemPoints;
        }
        public void DrawCoordinateSystem()
        {
            drawer.DrawCoordinateSystem(systemPoints);
        }
    }
    
}

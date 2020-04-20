using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Geometry2.Models
{
    public class CreateShapes
    {
        public Figures CreateLine(double X1, double X2, double Y1, double Y2, string letter,
            Thickness margin, DoubleCollection strokeDashArray, Brush brush, string name, int id)
        {
            Figures figure = new Figures
            {
                PointX1 = X1,
                PointX2 = X2,
                PointY1 = Y1,
                PointY2 = Y2,
                Letter = letter,
                LetterPosition = margin,
                StrokeDashArray = strokeDashArray,
                Color = brush,
                Name = name,
                Id = id

            };

            return figure;

        }

        public List<Figures> CreateCube()
        {
            List<Figures> figures = new List<Figures>();
            var dottedLine = new DoubleCollection() { 3 };
            var Line = new DoubleCollection() { 100 };
            BrushConverter brush = new BrushConverter();
            Brush colorBrush;

            colorBrush = brush.ConvertFromString("#DCE1E4") as Brush;

            figures.Add(CreateLine(100, 305, 70, 70, "D1", new Thickness(70, 10, 0, 0), Line, colorBrush, "D1C1", 0)); // D1 C1
            figures.Add(CreateLine(100, 100, -2, 190, "D", new Thickness(85, 190, 0, 0), dottedLine, colorBrush, "D1D", 1)); // D1 D
            figures.Add(CreateLine(305, 100, -50, -50, "C", new Thickness(290, -50, 0, 0), dottedLine, colorBrush, "D1DC", 2)); //DC
            figures.Add(CreateLine(305, 305, -245, -50, "C1", new Thickness(290, -300, 0, 0), Line, colorBrush, "CC1", 3)); // C C1
            figures.Add(CreateLine(100, 30, -50, 10, "A", new Thickness(0, 0, 0, 0), dottedLine, colorBrush, "DA", 4)); // DA
            figures.Add(CreateLine(240, 305, -40, -100, "B", new Thickness(240, -50, 0, 0), Line, colorBrush, "BC", 5)); // BC
            figures.Add(CreateLine(30, 240, -40, -40, "B", new Thickness(240, -50, 0, 0), Line, colorBrush, "AB", 6)); // AB
            figures.Add(CreateLine(30, 30, -40, -245, "A1", new Thickness(0, -300, 0, 0), Line, colorBrush, "AA1", 7)); // A A1
            figures.Add(CreateLine(30, 100, -245, -295, "A1", new Thickness(0, -300, 0, 0), Line, colorBrush, "A1D1", 8)); // A1 D1
            figures.Add(CreateLine(30, 240, -245, -245, "B1", new Thickness(210, -300, 0, 0), Line, colorBrush, "A1B1", 9)); // A1 B1
            figures.Add(CreateLine(240, 240, -245, -40, "B1", new Thickness(210, -300, 0, 0), Line, colorBrush, "BB1", 10)); // B B1
            figures.Add(CreateLine(240, 305, -245, -295, "B1", new Thickness(210, -300, 0, 0), Line, colorBrush, "B1C1", 11)); // B1 C1


            return figures;
        }

       

    }
}

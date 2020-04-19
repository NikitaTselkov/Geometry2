using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Geometry2.Models
{
    public class Figures
    {
        public double PointX1 { get; set; }

        public double PointY1 { get; set; }

        public double PointX2 { get; set; }

        public double PointY2 { get; set; }

        public string Letter { get; set; }

        public Thickness LetterPosition { get; set; }

        public DoubleCollection StrokeDashArray { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

    }
}

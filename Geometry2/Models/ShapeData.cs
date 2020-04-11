using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Geometry2.Models
{
    public class ShapeData
    {

        public int ShapeId { get; set; }

        public int Value { get; set; }

        public string Letter { get; set; }

        public MathematicalProperty MathematicalProperty { get; set; }

        public Visibility MyVisibility { get; set; }


    }
}

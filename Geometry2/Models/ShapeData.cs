using Caliburn.Micro;
using System.Windows;

namespace Geometry2.Models
{
    public class ShapeData
    {

        public int ShapeId { get; set; }

        public string Value { get; set; }

        public string Letter { get; set; }

        public TextBoxDropDownModel MathematicalProperty { get; set; }

        public Visibility MyVisibility { get; set; }

        public string Repeat { get; set; }

        public bool IsRepeat { get; set; }


    }
}

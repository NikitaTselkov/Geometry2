using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Navigation
{
    public class NavigeteShapes
    {
        public NavigeteShapes()
        {

        }

        public NavigeteShapes(object shape)
        {
            Shape = shape;
        }

        public object Shape { get; set; }

    }
}

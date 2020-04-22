using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Navigation
{
    public class NavigateDataDecision
    {
        public NavigateDataDecision()
        {

        }

        public NavigateDataDecision(object data)
        {
            Data = data;
        }

        public object Data { get; set; }
    }
}

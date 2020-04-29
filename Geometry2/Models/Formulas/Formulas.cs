using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Models.Formulas
{
    public class Formulas
    {
        public List<DecisionFormat> Decision { get; set; }

        public int Answer { get; set; }

        public Formulas(List<DecisionFormat> decision, int answer)
        {
            Decision = decision;
            Answer = answer;
        }

        public Formulas((List<DecisionFormat> decision, int answer) input)
        {
            Decision = input.decision;
            Answer = input.answer;
        }

        public override string ToString()
        {
            return $"{Decision}{Answer}";
        }

    }
}

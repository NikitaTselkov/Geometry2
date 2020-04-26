using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Models.Formulas
{
    public class DecisionFormat
    {
        public string Letter { get; set; }

        public string Symbol { get; set; }


        public DecisionFormat(string letter, string symbol)
        {
            Letter = letter;
            Symbol = symbol;
        }

        public DecisionFormat((string letter, string symbol) input)
        {
            Letter = input.letter;
            Symbol = input.symbol;
        }

    }
}

using Caliburn.Micro;
using Geometry2.Models.Formulas;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Geometry2.Models
{
    public class FormulasCube
    {
        public (List<DecisionFormat> format, int answer) FindRibs(BindableCollection<ShapeData> givenRibs, BindableCollection<Figures> cubeRibs)
        {
            List<DecisionFormat> result = new List<DecisionFormat>();

            if (givenRibs.Count == 0)
            {
                throw new ArgumentNullException($"{nameof(givenRibs)} не может быть null", nameof(givenRibs));
            }
            if (cubeRibs.Count == 0)
            {
                throw new ArgumentNullException($"{nameof(cubeRibs)} не может быть null", nameof(cubeRibs));
            }

            int answer = Convert.ToInt32(givenRibs[0].Value);

            foreach (var item in cubeRibs.Distinct())
            {
                result.Add(new DecisionFormat(item.Name, "="));
            }

            return (result, answer);
        }

        public (List<DecisionFormat>, int) FindVolumes(BindableCollection<ShapeData> givenRibs)
        {
            List<DecisionFormat> result = new List<DecisionFormat>();

            
                var value = Convert.ToInt32(givenRibs[0].Value);

                int answer = (int)Math.Pow(value, 3);

                result.Add(new DecisionFormat("V", "= Rib³ ="));

            
            return (result, answer);
        }
    }
}

using Caliburn.Micro;
using Geometry2.Models.Formulas;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Geometry2.Models
{
    public class FormulasCube
    {
        public (List<DecisionFormat>, int) FindRibs(BindableCollection<ShapeData> givenRibs, BindableCollection<Figures> cubeRibs)
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
    }
}

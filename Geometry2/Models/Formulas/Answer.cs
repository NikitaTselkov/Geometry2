using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Models.Formulas
{
    public class Answer
    {

        public ShapeData FindAnswerRib(BindableCollection<Formulas> decision, BindableCollection<ShapeData> find)
        {
            ShapeData result = null;
            

            for (int i = 0; i < find.Count; i++)
            {
                ShapeData shape = new ShapeData();

                if (find[i].MathematicalProperty.Name == "Rib")
                {
                    if (decision.Count > i)
                    {
                        shape.Letter = find[i].Letter;
                        shape.MathematicalProperty = find[i].MathematicalProperty;
                        shape.Value = decision[i].Answer.ToString();
                        result = shape;
                    }
                }
            }

            return result;
        }

        public ShapeData FindAnswerVolume(BindableCollection<Formulas> decision, BindableCollection<ShapeData> find)
        {
            ShapeData result = null;

            for (int i = 0; i < find.Count; i++)
            {
                ShapeData shape = new ShapeData();

                if (find[i].MathematicalProperty.Name == "Volume")
                {
                    if (decision.Count > i)
                    {
                        shape.Letter = find[i].Letter;
                        shape.MathematicalProperty = find[i].MathematicalProperty;
                        shape.Value = decision[i].Answer.ToString();
                        result = shape;
                    }
                }
            }

            return result;
        }


    }
}

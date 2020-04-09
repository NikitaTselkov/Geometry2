using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Models
{
    public class DataAccess
    {

        public List<ShapeData> GetShape(int total = 0)
        {
            List<ShapeData> output = new List<ShapeData>();

            for (int i = 0; i < total; i++)
            {
                output.Add(GetDataShape(i + 1));
            }

            return output;

        }

        public List<MathematicalProperty> GetDataMathProp()
        {
            List<MathematicalProperty> output = new List<MathematicalProperty>();

            foreach (MathematicalProperty item in Enum.GetValues(typeof(MathematicalProperty)))
            {
                output.Add(item);
            }

            return output;


        }


        public ShapeData GetDataShape(int id)
        {
            ShapeData output = new ShapeData();

            output.ShapeId = id;
            output.Value = 55;
            output.Letter = "AB";
            output.MathematicalProperty = MathematicalProperty.Length;

            return output;

        }
    }
}

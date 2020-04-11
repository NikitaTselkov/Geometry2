using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Geometry2.Models
{
    public class DataAccess
    {

        public List<ShapeData> GetShape(int total = 1)
        {
            List<ShapeData> output = new List<ShapeData>();

            for (int i = 0; i < total; i++)
            {
                output.Add(GetDataShape(i + 1));
            }

            return output;

        }

        
        Random Random = new Random();

        public ShapeData GetDataShape(int id)
        {
            List<MathematicalProperty> properties = GetDataMathProp();

            var rnd = Random.Next(0, 4);

            ShapeData output = new ShapeData();

            output.ShapeId = id;
            output.Value = 55;
            output.Letter = "AB";
            output.MathematicalProperty = properties[rnd];
            output.MyVisibility = Visibility.Collapsed;

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

    }
}

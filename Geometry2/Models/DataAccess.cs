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

        public List<ShapeData> GetShape(int total = 0)
        {
            List<ShapeData> output = new List<ShapeData>();

            for (int i = 0; i < total; i++)
            {
                output.Add(GetDataShape(i + 1));
            }

            return output;

        }

        public ShapeData GetDataShape(int id, MathematicalProperty mathematical = MathematicalProperty.Rib)
        {
            if (id < 0)
            {
                throw new ArgumentException($"{id} не может быть меньше 0", nameof(id));
            }

            ShapeData output = new ShapeData();

            output.ShapeId = id;
            output.Value = "0";
            output.Letter = "AB";
            output.MathematicalProperty = mathematical;
            output.MyVisibility = Visibility.Visible;

            return output;

        }

        public List<TextBoxDropDownModel> AddMathValues()
        {
            List<TextBoxDropDownModel> output = new List<TextBoxDropDownModel>();
            List<string> values = new List<string>();

            foreach (var item in GetDataMathProp())
            {
                values.Add(item.ToString());
            }

            for (int i = 0; i < values.Count; i++)
            {
                output.Add(new TextBoxDropDownModel() { Id = i, Name = values[i], Description = "" });
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

        public List<Figures> CreateShape(object input)
        {
            if (input == null)
            {
                input = "Cube";
            }

            CreateShapes cs = new CreateShapes();

            var result = new List<Figures>();
            var value = input.ToString();

            switch (value)
            {
                case "Cube":
                    result = cs.CreateCube();
                    break;

                default:
                    break;
            }


            return result;
        }

    }
}

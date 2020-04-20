using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Geometry2.Models
{
    public class DataAccess
    {

        public TextBoxDropDownModel AddTextBoxProp(int id, string name)
        {

            TextBoxDropDownModel output = new TextBoxDropDownModel();

            output.Id = id;
            output.Name = name;

            return output;
        }


        public ShapeData GetDataShape(int id, string mathematical)
        {
            if (id < 0)
            {
                throw new ArgumentException($"{id} не может быть меньше 0", nameof(id));
            }

            ShapeData output = new ShapeData();
            TextBoxDropDownModel textBox = new TextBoxDropDownModel();

            textBox.Name = mathematical;
            textBox.Id = id;

            output.ShapeId = id;
            output.Value = "0";
            output.Letter = "AB";
            output.MathematicalProperty = textBox;
            output.MyVisibility = Visibility.Collapsed;

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
                output.Add(new TextBoxDropDownModel() { Id = i, Name = values[i]});
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

        public BindableCollection<Figures> BrushLine(BindableCollection<ShapeData> givenDatas,
            BindableCollection<ShapeData> findDatas, string brush1, string brush2, BindableCollection<Figures> figures)
        {
            BrushConverter brushConverter = new BrushConverter();

            for (var i = 0; i < figures.Count; i++)
            {
                figures[i].Color = brushConverter.ConvertFromString("#DCE1E4") as Brush;
            }

            foreach (var item in givenDatas)
            {
                for (var i = 0; i < figures.Count; i++)
                {
                    if (item.Letter == figures[i].Name)
                    {
                        figures[i].Color = brushConverter.ConvertFromString(brush1) as Brush;
                    }
                }
            }

            foreach (var item in findDatas)
            {
                for (var i = 0; i < figures.Count; i++)
                {
                    if (item.Letter == figures[i].Name)
                    {
                        figures[i].Color = brushConverter.ConvertFromString(brush2) as Brush;
                    }
                }
            }

            figures.Refresh();

            return figures;
        }

    }
}

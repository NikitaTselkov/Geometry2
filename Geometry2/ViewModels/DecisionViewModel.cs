using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Geometry2.Models;

namespace Geometry2.ViewModels
{
    public class DecisionViewModel : Navigation.NavigateViewModel
    {
        public RelayCommand AddShapeCommand { get; set; }

        public BindableCollection<ShapeData> Figure { get; set; }

        public BindableCollection<MathematicalProperty> AddWindow { get; set; }


        public DecisionViewModel()
        {
            DataAccess da = new DataAccess();
            Figure = new BindableCollection<ShapeData>(da.GetShape());
            AddWindow = new BindableCollection<MathematicalProperty>(da.GetDataMathProp());

            AddShapeCommand = new RelayCommand(AddShape);
        }

        public void AddShape(object param)
        {
            DataAccess da = new DataAccess();

            int maxId = 0;

            if (Figure.Count > 0)
            {
                maxId = Figure.Max(x => x.ShapeId);
            }

            Figure.Add(da.GetDataShape(maxId + 1));

        }



    }
}

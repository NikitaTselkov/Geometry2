using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Caliburn.Micro;
using Geometry2.Models;

namespace Geometry2.ViewModels
{
    public class DecisionViewModel : INotifyPropertyChanged
    {
        public RelayCommand AddShapeCommand { get; set; }

        public RelayCommand OpenInputCommand { get; set; }

        public RelayCommand FindButtonCommand { get; set; }

        public BindableCollection<ShapeData> Figure { get; set; }

        public BindableCollection<MathematicalProperty> AddWindow { get; set; }

        private Visibility _IsVisibility = Visibility.Collapsed;
        public Visibility IsVisibility
        {
            get { return _IsVisibility; }
            set
            {
                _IsVisibility = value;
                this.OnPropertyChanged();
            }
        }


        public DecisionViewModel()
        {
            DataAccess da = new DataAccess();
            Figure = new BindableCollection<ShapeData>(da.GetShape());
            AddWindow = new BindableCollection<MathematicalProperty>(da.GetDataMathProp());

            AddShapeCommand = new RelayCommand(AddShape);
            OpenInputCommand = new RelayCommand(OpenInput);
            FindButtonCommand = new RelayCommand(FindButton);
        }

        public void FindButton(object param)
        {
           var par = (int)param - 1;

           var shapeData = Figure[par];

           shapeData.MyVisibility = VisCheck(shapeData.MyVisibility);

           Figure[par] = shapeData;

           Figure.Refresh();
        }


        public void OpenInput(object param)
        {
           IsVisibility = VisCheck(IsVisibility);
        }

        public void AddShape(object param)
        {
            DataAccess da = new DataAccess();

            int maxId = 0;

            if (Figure.Count > 0)
            {
                maxId = Figure.Max(x => x.ShapeId);
            }

            Figure.Add(da.GetDataShape(maxId + 1, (MathematicalProperty)param));
            IsVisibility = Visibility.Collapsed;


        }

        private Visibility VisCheck(Visibility IsVisible)
        {
            if (IsVisible == Visibility.Collapsed)
            {
                return Visibility.Visible;
            }
            else
            {
               return Visibility.Collapsed;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}

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

        public RelayCommand AddShapeGivenCommand { get; set; }

        public RelayCommand OpenInputGivenCommand { get; set; }

        public RelayCommand FindButtonCommand { get; set; }

        public RelayCommand FindButtonGivenCommand { get; set; }

        public BindableCollection<ShapeData> Figure { get; set; }

        public BindableCollection<ShapeData> GetFigure { get; set; }

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

        private Visibility _IsVisibility2 = Visibility.Collapsed;
        public Visibility IsVisibility2
        {
            get { return _IsVisibility2; }
            set
            {
                _IsVisibility2 = value;
                this.OnPropertyChanged();
            }
        }


        public DecisionViewModel()
        {
            DataAccess da = new DataAccess();
            Figure = new BindableCollection<ShapeData>(da.GetShape());
            GetFigure = new BindableCollection<ShapeData>(da.GetShape());
            AddWindow = new BindableCollection<MathematicalProperty>(da.GetDataMathProp());

            AddShapeCommand = new RelayCommand(AddShape);
            OpenInputCommand = new RelayCommand(OpenInput);
            FindButtonCommand = new RelayCommand(FindButton);

            AddShapeGivenCommand = new RelayCommand(AddShapeGiven);
            OpenInputGivenCommand = new RelayCommand(OpenInputGiven);
            FindButtonGivenCommand = new RelayCommand(FindButtonGiven);
        }

        public void FindButton(object param)
        {
           var par = (int)param - 1;

            FindButtonHelp(Figure, par);
        }

        public void FindButtonGiven(object param)
        {
            var par = (int)param - 1;

            FindButtonHelp(GetFigure, par);
        }


        public void OpenInput(object param)
        {
           IsVisibility = VisCheck(IsVisibility);
        }

        public void OpenInputGiven(object param)
        {
            IsVisibility2 = VisCheck(IsVisibility2);
        }

        public void AddShape(object param)
        {
            AddShapeHelp(Figure, param);
            IsVisibility = Visibility.Collapsed;
        }

        public void AddShapeGiven(object param)
        {
            AddShapeHelp(GetFigure, param);
            IsVisibility2 = Visibility.Collapsed;
        }

        private void AddShapeHelp(BindableCollection<ShapeData> Collection, object param)
        {
            DataAccess da = new DataAccess();

            int maxId = 0;

            if (Collection.Count > 0)
            {
                maxId = Collection.Max(x => x.ShapeId);
            }

            var F = da.GetDataShape(maxId + 1, (MathematicalProperty)param);

            F.MyVisibility = Visibility.Visible;

            Collection.Add(F);


        }

        private void FindButtonHelp(BindableCollection<ShapeData> Collection, int par)
        {

            var shapeData = Collection[par];

            shapeData.MyVisibility = VisCheck(shapeData.MyVisibility);

            Collection[par] = shapeData;

            Collection.Refresh();
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

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Media3D;
using Caliburn.Micro;
using GalaSoft.MvvmLight.Messaging;
using Geometry2.Models;

namespace Geometry2.ViewModels
{
    public class DecisionViewModel : INotifyPropertyChanged
    {
        readonly DataAccess da = new DataAccess();

        #region RelayCommand

        public RelayCommand AddShapeCommand { get; set; }

        public RelayCommand OpenInputCommand { get; set; }

        public RelayCommand AddShapeGivenCommand { get; set; }

        public RelayCommand OpenInputGivenCommand { get; set; }

        public RelayCommand FindButtonCommand { get; set; }

        public RelayCommand FindButtonGivenCommand { get; set; }

        #endregion

        public BindableCollection<ShapeData> Figure { get; set; }

        public BindableCollection<ShapeData> GetFigure { get; set; }

        public BindableCollection<MathematicalProperty> AddWindow { get; set; }

        public BindableCollection<Figures> Figures { get; set; }

        #region IsVisibility

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

        #endregion
        
        public DecisionViewModel()
        {

            Figure = new BindableCollection<ShapeData>(da.GetShape());
            GetFigure = new BindableCollection<ShapeData>(da.GetShape());
            AddWindow = new BindableCollection<MathematicalProperty>(da.GetDataMathProp());

            AddShapeCommand = new RelayCommand(AddShape);
            OpenInputCommand = new RelayCommand(OpenInput);
            FindButtonCommand = new RelayCommand(FindButton);

            AddShapeGivenCommand = new RelayCommand(AddShapeGiven);
            OpenInputGivenCommand = new RelayCommand(OpenInputGiven);
            FindButtonGivenCommand = new RelayCommand(FindButtonGiven);

            NavigationSetup();
        }

        #region Methods

        void NavigationSetup()
        {
            Messenger.Default.Register<Navigation.NavigeteShapes>(this, (x) =>
            {
                Figures = new BindableCollection<Figures>(da.CreateShape(x.Shape));
            });
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

        #endregion

        #region Private Methods

        private void AddShapeHelp(BindableCollection<ShapeData> Collection, object param)
        {
            DataAccess da = new DataAccess();

            int maxId = 0;

            if (Collection.Count > 0)
            {
                maxId = Collection.Max(x => x.ShapeId);
            }

             var data = da.GetDataShape(maxId + 1, (MathematicalProperty)param);

            data = Formatting(data);

            Collection.Add(data);

        }

        private ShapeData Formatting(ShapeData data)
        {

            if (data.MathematicalProperty == MathematicalProperty.Volume)
            {
                //if (data.Letter.FirstOrDefault() == Convert.ToChar("∠"))
                data.Letter = "V";
            }
            if (data.MathematicalProperty == MathematicalProperty.Corner)
            {
                if (data.Letter.FirstOrDefault() != Convert.ToChar("∠"))
                {
                    if (data.Letter == "V")
                    {
                        data.Letter = "AB";
                    }
                    data.Letter = data.Letter.Insert(0, "∠");
                }
            }

            return data;
        }

        private void FindButtonHelp(BindableCollection<ShapeData> Collection, int par)
        {

            var shapeData = Collection[par];

            shapeData.MyVisibility = VisCheck(shapeData.MyVisibility);

            Formatting(shapeData);

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

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}

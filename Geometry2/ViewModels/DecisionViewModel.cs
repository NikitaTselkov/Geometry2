using System;
using System.Collections.Generic;
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

        public List<TextBoxDropDownModel> MathValues { get; set; }

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

            AddShapeCommand = new RelayCommand(AddShape);
            OpenInputCommand = new RelayCommand(OpenInput);
            FindButtonCommand = new RelayCommand(FindButton);

            AddShapeGivenCommand = new RelayCommand(AddShapeGiven);
            OpenInputGivenCommand = new RelayCommand(OpenInputGiven);
            FindButtonGivenCommand = new RelayCommand(FindButtonGiven);

            NavigationSetup();
            CreateList();
        }

        #region Methods

        void NavigationSetup()
        {
            Messenger.Default.Register<Navigation.NavigeteShapes>(this, (x) =>
            {
                CreateList();
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

        private void CreateList()
        {
            Figure = new BindableCollection<ShapeData>(da.GetShape());
            GetFigure = new BindableCollection<ShapeData>(da.GetShape());
            Figures = new BindableCollection<Figures>(da.CreateShape("Cube"));
            AddWindow = new BindableCollection<MathematicalProperty>(da.GetDataMathProp());
            MathValues = new List<TextBoxDropDownModel>(da.AddMathValues());
        }

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
            #region Проверка обьёма

            if (data.MathematicalProperty == MathematicalProperty.Volume)
            {
                //if (data.Letter.FirstOrDefault() == Convert.ToChar("∠"))
                data.Letter = "V";
            }

            #endregion

            #region Проверка Угла

            if (data.MathematicalProperty == MathematicalProperty.Corner)
            {
                if (data.Letter.FirstOrDefault() != Convert.ToChar("∠"))
                {
                    if (data.Letter == "V")
                    {
                        data.Letter = "AB";
                    }
                    var len = data.Value.Length;

                    data.Value = data.Value.Insert(len, "°");
                    data.Letter = data.Letter.Insert(0, "∠");
                }
            }
            if (data.MathematicalProperty != MathematicalProperty.Corner)
            {
                if (data.Letter.FirstOrDefault() == Convert.ToChar("∠"))
                {
                    data.Letter = data.Letter.Remove(0, 1);
                }
                if (data.Value.LastOrDefault() == Convert.ToChar("°"))
                {
                    var len = data.Value.Length - 1;
                    data.Value = data.Value.Remove(len, 1);
                }
            }

            #endregion

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

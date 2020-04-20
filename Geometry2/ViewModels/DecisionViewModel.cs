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

        public List<TextBoxDropDownModel> AddWindow { get; set; }

        public BindableCollection<Figures> Figures { get; set; }

        public string MathProp { get; set; }

        public string MathPropFind { get; set; }

        public string GivenLetter { get; set; }

        public string FindLetter { get; set; }


        private List<TextBoxDropDownModel> MathProps { get; set; }

        private List<TextBoxDropDownModel> LetterProps { get; set; }

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

           MathPropFind = FindButtonHelp(Figure, par, MathProps, MathPropFind);
           FindLetter = SetLetter(Figure, par, LetterProps, FindLetter);
        }

        public void FindButtonGiven(object param)
        {
            var par = (int)param - 1;

            MathProp = FindButtonHelp(GetFigure, par, MathProps, MathProp);
            GivenLetter = SetLetter(GetFigure, par, LetterProps, GivenLetter);
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
            AddShapeHelp(Figure, param, MathProps, LetterProps);
            IsVisibility = Visibility.Collapsed;
        }

        public void AddShapeGiven(object param)
        {
            AddShapeHelp(GetFigure, param, MathProps, LetterProps);
            IsVisibility2 = Visibility.Collapsed;
        }

        #endregion

        #region Private Methods

        private void CreateList()
        {
            Figure = new BindableCollection<ShapeData>();
            GetFigure = new BindableCollection<ShapeData>();
            Figures = new BindableCollection<Figures>(da.CreateShape("Cube"));
            AddWindow = new List<TextBoxDropDownModel>(da.AddMathValues());
            MathProps = new List<TextBoxDropDownModel>();
            LetterProps = new List<TextBoxDropDownModel>();
        }

        private void AddShapeHelp(BindableCollection<ShapeData> Collection, object param,
            List<TextBoxDropDownModel> props, List<TextBoxDropDownModel> Letterprops)
        {
            DataAccess da = new DataAccess();

            int maxId = 0;

            if (Collection.Count > 0)
            {
                maxId = Collection.Max(x => x.ShapeId);
            }

            var data = da.GetDataShape(maxId + 1, param.ToString());
            var textProp = da.AddTextBoxProp(maxId + 1, param.ToString());
            var textLetterProp = da.AddTextBoxProp(maxId + 1, "AB");

            data = Formatting(data);

            Collection.Add(data);
            props.Add(textProp);
            Letterprops.Add(textLetterProp);

        }

        private ShapeData Formatting(ShapeData data)
        {

            #region Проверка обьёма

            if (data.MathematicalProperty.Name == "Volume")
            {
                data.Letter = "V";
            }

            #endregion

            #region Проверка Угла

            if (data.MathematicalProperty.Name == "Corner")
            {
                if (data.Letter.FirstOrDefault() != Convert.ToChar("∠"))
                {
                    if (data.Letter == "V")
                    {
                        data.Letter = "AB";
                    }
                    var len = data.Value.Length;

                    if (data.Value.LastOrDefault() != Convert.ToChar("°"))
                    {
                        data.Value = data.Value.Insert(len, "°");
                    }
                    data.Letter = data.Letter.Insert(0, "∠");
                }
            }
            if (data.MathematicalProperty.Name != "Corner")
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

        private string SetLetter(BindableCollection<ShapeData> Collection, int par, List<TextBoxDropDownModel> props,
            string Letter)
        {
            var textBox = new TextBoxDropDownModel();

            var shapeData = Collection[par];

            var Prop = props[par];

            Prop.Name = shapeData.Letter;

            if (shapeData.MyVisibility == Visibility.Collapsed)
            {
                textBox.Name = Letter;

                shapeData.Letter = Letter;

                Formatting(shapeData);

                Collection[par] = shapeData;

                props[par] = textBox;
            }

            Collection.Refresh();

            Figures = da.BrushLine(GetFigure, Figure, "#5983EB", "#33AF63", Figures);

            return Prop.Name;
        }

        private string FindButtonHelp(BindableCollection<ShapeData> Collection, int par, List<TextBoxDropDownModel> props,
            string MathProp)
        {
            var textBox = new TextBoxDropDownModel();

            var shapeData = Collection[par];

            var Prop = props[par];

            Prop = shapeData.MathematicalProperty;

            СheckVisibility(Collection, par);

            if (shapeData.MyVisibility == Visibility.Collapsed)
            {
                textBox.Name = MathProp;

                shapeData.MathematicalProperty.Name = MathProp;

                Formatting(shapeData);

                Collection[par] = shapeData;

                props[par] = textBox;
            }

            Collection.Refresh();

            return Prop.Name;
     
        }

        private void СheckVisibility(BindableCollection<ShapeData> Collection, int par)
        {
            for (int i = 0; i < Collection.Count; i++)
            {
                if (i == par)
                {
                    Collection[par].MyVisibility = VisCheck(Collection[par].MyVisibility);
                    continue;
                }
                Collection[i].MyVisibility = Visibility.Collapsed;
            }
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

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
    public class DecisionViewModel : Navigation.NavigateViewModel, INotifyPropertyChanged
    {
        readonly DataAccess da = new DataAccess();

        #region RelayCommand

        public RelayCommand AddShapeCommand { get; set; }

        public RelayCommand OpenInputCommand { get; set; }

        public RelayCommand AddShapeGivenCommand { get; set; }

        public RelayCommand OpenInputGivenCommand { get; set; }

        public RelayCommand FindButtonCommand { get; set; }

        public RelayCommand FindButtonGivenCommand { get; set; }

        public RelayCommand AnswerButton { get; set; }

        #endregion

        public BindableCollection<ShapeData> FindInput { get; set; }

      //  public BindableCollection<ShapeData> GivenInput { get; set; }

        public BindableCollection<ShapeData> _givenInput;
        public BindableCollection<ShapeData> GivenInput
        {
            get { return _givenInput; }
            set
            {
                _givenInput = value;
                //OnPropertyChanged();
                //if (_givenInput.Count != 0)
                //{
                //    Formatting(_givenInput.Last());
                //}
            }
        }

        public List<TextBoxDropDownModel> MathProperties { get; set; }

        public BindableCollection<Figures> Figures { get; set; }

        public string MathProp { get; set; }

        public string MathPropFind { get; set; }

        public string GivenLetter { get; set; }

        public string FindLetter { get; set; }

        public object Shape { get; set; } = "Cube";

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

        private Visibility _IsVisibilityAnswer = Visibility.Collapsed;
        public Visibility IsVisibilityAnswer
        {
            get { return _IsVisibilityAnswer; }
            set
            {
                _IsVisibilityAnswer = value;
                this.OnPropertyChanged();
            }
        }

        #endregion
        
        public DecisionViewModel()
        {

            /// <summary>
            /// FindCommands
            /// </summary>
            AddShapeCommand = new RelayCommand(AddShape);
            OpenInputCommand = new RelayCommand(OpenInput);
            FindButtonCommand = new RelayCommand(FindButton);

            /// <summary>
            /// GivenCommands
            /// </summary>
            AddShapeGivenCommand = new RelayCommand(AddShapeGiven);
            OpenInputGivenCommand = new RelayCommand(OpenInputGiven);
            FindButtonGivenCommand = new RelayCommand(FindButtonGiven);

            AnswerButton = new RelayCommand(AnswerButtonLogic);

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
                Shape = x.Shape;
            });
        }

        public void FindButton(object param)
        {
           var par = (int)param - 1;

           MathPropFind = FindButtonHelp(FindInput, par, MathProps, MathPropFind);
           FindLetter = SetLetter(FindInput, par, LetterProps, FindLetter);
        }

        public void FindButtonGiven(object param)
        {
            var par = (int)param - 1;
            MathProp = FindButtonHelp(GivenInput, par, MathProps, MathProp);
            GivenLetter = SetLetter(GivenInput, par, LetterProps, GivenLetter);
        }

        public void AnswerButtonLogic(object param)
        {
            IsVisibilityAnswer = VisCheck(IsVisibilityAnswer);

            SendDataDecision(FindInput, GivenInput, Figures, Shape, IsVisibilityAnswer);
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
            AddShapeHelp(FindInput, param, MathProps, LetterProps);
            IsVisibility = Visibility.Collapsed;
        }

        public void AddShapeGiven(object param)
        {
            AddShapeHelp(GivenInput, param, MathProps, LetterProps);
            IsVisibility2 = Visibility.Collapsed;
        }

        #endregion

        #region Private Methods

        private void CreateList()
        {
            FindInput = new BindableCollection<ShapeData>();
            GivenInput = new BindableCollection<ShapeData>();
            Figures = new BindableCollection<Figures>(da.CreateShape("Cube"));
            MathProperties = new List<TextBoxDropDownModel>(da.AddRibMathValues());
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

            if (data.IsRepeat == false)
            {
                Collection.Add(data);
                props.Add(textProp);
                Letterprops.Add(textLetterProp);

            }
        }

        //private void RemoveShapeHelp(BindableCollection<ShapeData> Collection, ShapeData data,
        //    List<TextBoxDropDownModel> props, List<TextBoxDropDownModel> Letterprops)
        //{

        //    if (Collection.Count > data.ShapeId)
        //    {
        //        ShapeData repeat = data;

        //        repeat = Collection.Last((l)=> l.Repeat != string.Empty);

        //        Collection.Remove(Collection[repeat.ShapeId - 1]);
        //        props.Remove(data.MathematicalProperty);
        //        Letterprops.Remove(data.MathematicalProperty);

        //        for (int i = 1; i < Collection.Count; i++)
        //        {
        //            Collection[i].ShapeId = i;
        //        }
        //    }
        //}

        private ShapeData Formatting(ShapeData data)
        {
            #region Проверка обьёма

            if (data.MathematicalProperty.Name == "Volume")
            {
                data.Letter = "V";

              //  data.IsRepeat = IsRepeat(true, true, data, GivenInput);
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

            #region Проверка грани

            if (data.MathematicalProperty.Name == "Rib")
            {
                //data.Repeat = IsRepeat(true, false, data, GivenInput);

                //if (data.Repeat != string.Empty)
                //{
                //    data.IsRepeat = true;
                //}

                //if (data.IsRepeat == true)
                //{
                //    RemoveShapeHelp(GivenInput, data, MathProps, LetterProps);
                //    data.Repeat = string.Empty;
                //}
            }

            #endregion



            return data;
        }

        //private string IsRepeat(bool mathProp, bool Letter, ShapeData data, BindableCollection<ShapeData> given)
        //{
        //    string result = string.Empty;

        //    var repeatMath = 0;
        //    var repeatLetter = 0;

        //    if (mathProp == true)
        //    {
        //        foreach (var item in given)
        //        {
        //            if (item.MathematicalProperty.Name == data.MathematicalProperty.Name)
        //            {
        //                repeatMath++;
        //                if (repeatMath == 1)
        //                {
        //                    repeatMath = 0;
        //                    return data.MathematicalProperty.Name;
        //                }
        //            }
        //        }                    
        //    }
        //    if (Letter == true)
        //    {
        //        foreach (var item in given)
        //        {
        //            if (item.Letter == data.Letter)
        //            {
        //                repeatLetter++;
        //                if (repeatLetter == 1)
        //                {
        //                    repeatLetter = 0;
        //                    return data.Letter;
        //                }
        //            }
        //        }
        //    }
        //
        //    return result;
        //}

        private string SetLetter(BindableCollection<ShapeData> Collection, int par, List<TextBoxDropDownModel> props,
            string Letter)
        {
            var textBox = new TextBoxDropDownModel();

            if (Collection.Count > par)
            {
                var shapeData = Collection[par];

                var Prop = props[par];

                if (shapeData != null)
                {
                    Prop.Name = shapeData.Letter;


                    if (shapeData.MyVisibility == Visibility.Collapsed)
                    {
                        textBox.Name = Letter;

                        shapeData.Letter = Letter;

                        Collection[par] = shapeData;

                        props[par] = textBox;

                        
                    }
                }

                Formatting(shapeData);

                //shapeData.IsRepeat = IsRepeat(true, false, shapeData, GivenInput);

                //if (shapeData.IsRepeat)
                //{
                //    RemoveShapeHelp(GivenInput, shapeData, MathProps, LetterProps);
                //}

                Collection.Refresh();

                Figures = da.BrushLine(GivenInput, FindInput, "#5983EB", "#33AF63", Figures);

                return Prop.Name;
            }

            
            return null;
        }

        private string FindButtonHelp(BindableCollection<ShapeData> Collection, int par, List<TextBoxDropDownModel> props,
            string MathProp)
        {
            var textBox = new TextBoxDropDownModel();

            //for (int i = 0; i < Collection.Count; i++)
            //{
            //    Collection[i].ShapeId = i;
            //}

            if (Collection.Count > par)
            {

                var shapeData = Collection[par];

                var Prop = props[par];

                Prop = shapeData.MathematicalProperty;

                СheckVisibility(Collection, par);

                if (shapeData.MyVisibility == Visibility.Collapsed)
                {
                    textBox.Name = MathProp;

                    shapeData.MathematicalProperty.Name = MathProp;

                    Collection[par] = shapeData;

                    props[par] = textBox;

                    //Formatting(shapeData);
                }

                Collection.Refresh();

                return Prop.Name;
            }
            return null;
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

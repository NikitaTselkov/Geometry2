using Caliburn.Micro;
using GalaSoft.MvvmLight.Messaging;
using Geometry2.Models;
using Geometry2.Models.Formulas;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Geometry2.ViewModels
{
    public class DecisionAnswerViewModel : Navigation.NavigateViewModel, INotifyPropertyChanged
    {
        FormulasCube cube = new FormulasCube();

        #region Public property

        public BindableCollection<ShapeData> Given
        {
            get { return _given; }
            set
            {
                _given = value;
                OnPropertyChanged();
            }
        }

        public BindableCollection<ShapeData> Find
        {
            get { return _find; }
            set
            {
                _find = value;
                OnPropertyChanged();
            }
        }

        public BindableCollection<Figures> Figure
        {
            get { return _figure; }
            set
            {
                _figure = value;
                OnPropertyChanged();
            }
        }

        public BindableCollection<ShapeData> Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged();
            }
        }

        public BindableCollection<Formulas> DecisionList
        {
            get { return _decisionList; }
            set
            {
                _decisionList = value;
                OnPropertyChanged();
            }
        }

        public object Shape
        {
            get { return _shape; }
            set
            {
                _shape = value;
                OnPropertyChanged();
            }
        }

        public Visibility IsVisibilityAnswer
        {
            get { return _isVisibilityAnswer; }
            set
            {
                _isVisibilityAnswer = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Private property

        private BindableCollection<ShapeData> _given;

        private BindableCollection<ShapeData> _find;

        private BindableCollection<ShapeData> _answer;

        private BindableCollection<Formulas> _decisionList;

        private BindableCollection<Figures> _figure;

        private object _shape;
        private Visibility _isVisibilityAnswer;

        #endregion

        public DecisionAnswerViewModel()
        {
            NavigationSetup();
        }

        #region Methods

        void NavigationSetup()
        {
            Messenger.Default.Register<Navigation.NavigateDataDecision>(this, (x) =>
            {
                Find = x.Find;
                Given = x.Given;
                Figure = x.Figure;
                Shape = x.Shape;
                IsVisibilityAnswer = x.IsVisibilityAnswer;

                DecisionList = new BindableCollection<Formulas>();

                if (IsVisibilityAnswer == Visibility.Visible)
                {
                    if (Given.Count > 0)
                    {
                        DecisionList.Add(new Formulas(cube.FindRibs(Given, Figure)));
                    }
                }
            });
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

    

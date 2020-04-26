using Geometry2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.ViewModels
{
    public class MainViewModel : Navigation.NavigateViewModel, INotifyPropertyChanged
    {

        public RelayCommand GoToFigurePage { get; set; }
        public RelayCommand GoToDecisionPage { get; set; }

        #region IsPressed
        private bool _IsGoToFigurePagePressed;

        public bool IsGoToFigurePagePressed
        {
            get { return _IsGoToFigurePagePressed; }
            set 
            {
                _IsGoToFigurePagePressed = value;
                this.OnPropertyChanged();
            }
        }

        private bool _IsGoToDecisionPagePressed;

        public bool IsGoToDecisionPagePressed
        {
            get { return _IsGoToDecisionPagePressed; }
            set
            {
                _IsGoToDecisionPagePressed = value;
                this.OnPropertyChanged();
            }
        }

        #endregion

        public MainViewModel()
        {
            GoToFigurePage = new RelayCommand(PathToFigurePage);
            GoToDecisionPage = new RelayCommand(PathToDecisionPage);

            PathToDecisionPage("");
        }

        public void PathToFigurePage(object param)
        {
            Navigate("Views/Figure.xaml");
            IsGoToFigurePagePressed = true;
        }
        
        public void PathToDecisionPage(object param)
        {
            IsGoToDecisionPagePressed = true;
            Navigate("Views/Decision.xaml");
            SendFigure(param);
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }

}

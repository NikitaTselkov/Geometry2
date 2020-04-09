using Geometry2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.ViewModels
{
    public class MainViewModel : Navigation.NavigateViewModel
    {

        public RelayCommand GoToFigurePage { get; set; }
        public RelayCommand GoToDecisionPage { get; set; }      
 
        public MainViewModel()
        {
            GoToFigurePage = new RelayCommand(PathToFigurePage);
            GoToDecisionPage = new RelayCommand(PathToDecisionPage);

        }

        public void PathToFigurePage(object param)
        {
            Navigate("Views/Figure.xaml");
        }
        
        public void PathToDecisionPage(object param)
        {
            Navigate("Views/Decision.xaml");
        }
    }

}

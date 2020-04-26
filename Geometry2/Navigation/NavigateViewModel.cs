using Caliburn.Micro;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Geometry2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Geometry2.Navigation
{
    public class NavigateViewModel : ViewModelBase
    {
        public NavigateViewModel()
        {

        }

        public string Title { get; set; }
        public void Navigate(string url)
        {
            Messenger.Default.Send<NavigateArgs>(new NavigateArgs(url));
        }

        public void SendFigure(object shape)
        {
            Messenger.Default.Send<NavigeteShapes>(new NavigeteShapes(shape));
        }

        public void SendDataDecision(BindableCollection<ShapeData> find, BindableCollection<ShapeData> given,
            BindableCollection<Figures> figure, object shape, Visibility isVisibilityAnswer)
        {
            Messenger.Default.Send<NavigateDataDecision>(new NavigateDataDecision(find, given, figure, shape, isVisibilityAnswer));
        }

    }
}

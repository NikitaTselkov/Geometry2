using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.ViewModels
{
    public class DecisionAnswerViewModel
    {
        public DecisionAnswerViewModel()
        {
            NavigationSetup();
        }

        void NavigationSetup()
        {
            Messenger.Default.Register<Navigation.NavigateDataDecision>(this, (x) =>
            {
                Console.WriteLine(x.Data);
            });

        }
    }
}

    

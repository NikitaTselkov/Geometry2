using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2.Navigation
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ViewModels.MainViewModel>();

            SimpleIoc.Default.Register<ViewModels.DecisionViewModel>();

            SimpleIoc.Default.Register<ViewModels.DecisionAnswerViewModel>();

        }

        public ViewModels.MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModels.MainViewModel>();
            }
        }

        public ViewModels.DecisionViewModel Decision
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModels.DecisionViewModel>();
            }
        }

        public ViewModels.DecisionAnswerViewModel DecisionAnswer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModels.DecisionAnswerViewModel>();
            }
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}

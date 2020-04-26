using Caliburn.Micro;
using Geometry2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Geometry2.Navigation
{
    public class NavigateDataDecision
    {
        public NavigateDataDecision()
        {

        }

        public NavigateDataDecision(BindableCollection<ShapeData> find, BindableCollection<ShapeData> given,
            BindableCollection<Figures> figure, object shape, Visibility isVisibilityAnswer)
        {
            Find = find;
            Given = given;
            Figure = figure;
            Shape = shape;
            IsVisibilityAnswer = isVisibilityAnswer;
        }

        public BindableCollection<ShapeData> Find { get; set; }
        public BindableCollection<ShapeData> Given { get; set; }
        public BindableCollection<Figures> Figure { get; set; }
        public object Shape { get; set; }
        public Visibility IsVisibilityAnswer { get; set; }
    }
}

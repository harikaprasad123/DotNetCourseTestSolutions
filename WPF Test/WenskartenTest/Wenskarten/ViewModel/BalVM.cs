using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Wenskarten.ViewModel
{
    public class BalVM : ViewModelBase
    {
        public Brush Fill { get; set; }
        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                RaisePropertyChanged("X");
            }
        }
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                RaisePropertyChanged("Y");
            }
        }
        public ICommand BalMoveCommand { get; set; }
        public override string ToString()
        {
            return $"{Fill}_{X}_{Y}_{Tag}";
        }
        public object Tag { get; set; }
    }
}

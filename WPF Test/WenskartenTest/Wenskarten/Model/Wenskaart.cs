using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Wenskarten.Model
{
    public class Wenskaart
    {
        public Wenskaart(ImageBrush nCanvasAchtergrond, string nWensText, string nWensFontFamily, int nWensFontSize)
        {
            CanvasAchtergrond = nCanvasAchtergrond;
            WensText = nWensText;
            WensFontFamily = nWensFontFamily;
            WensFontSize = nWensFontSize;
        }
        public ImageBrush CanvasAchtergrond { get; set; }
        public string WensText { get; set; }
        public string WensFontFamily { get; set; }
        public int WensFontSize { get; set; }
    }
}

 
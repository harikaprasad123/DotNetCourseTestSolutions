using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Wenskarten.Converters
{
    class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 7;
        }
        public object ConvertBack(object value, Type targetType, object parameter,CultureInfo culture)
        {
            return null;
        }
    }
}

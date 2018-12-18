using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TogglTimeManager.Converters
{
    //Check PipedConverters project on OneDrive if need be
    public class NullToVisibilityConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Hidden;
            }

            if (value is string s)
            {
                return string.IsNullOrEmpty(s) ? Visibility.Hidden : Visibility.Visible;
            }

            return Visibility.Visible;
            
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("There is no conversion from a Visibility value to an instance");
        }
    }
}

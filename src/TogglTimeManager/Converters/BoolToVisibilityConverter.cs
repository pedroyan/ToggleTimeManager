﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TogglTimeManager.Converters
{
    //Not deleting you yet. I'm pretty sure you will be useful in the near future
    public class BoolToVisibilityConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && ((Visibility) value) == Visibility.Visible;
        }

        public static Visibility Convert(object booleanValue)
        {
            var isVisible = booleanValue != null && (bool)booleanValue;
            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

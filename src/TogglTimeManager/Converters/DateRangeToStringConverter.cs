using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Converters
{
    public class DateRangeToStringConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateRange range))
            {
                string type = value?.GetType().ToString();

                if (!string.IsNullOrEmpty(type) && type.Contains("Mocks.TogglTimeManager"))
                    return $"{DateTime.Today:d} - {DateTime.Today.AddDays(1):d}"; //Return a bogus string to provide design-time preview
                    
                throw new ArgumentException($"The converter can only be used with the type {nameof(DateRange)}, type provided {value?.GetType().ToString() ?? "null"}");
            }

            return $"{range.StartDate:d} - {range.EndDate:d}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Nobody using this for now");
        }
    }
}

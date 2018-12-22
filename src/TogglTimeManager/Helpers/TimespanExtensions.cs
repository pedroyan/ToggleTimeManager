using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogglTimeManager.Helpers
{
    public static class TimespanExtensions
    {
        public static string ToHoursString(this TimeSpan timeSpan)
        {
            var absolute = timeSpan.Duration();
            var minus = timeSpan < TimeSpan.Zero ? "-" : "";
            var hours = absolute.Days * 24 + absolute.Hours;
            return $"{minus}{hours:D2}:{absolute.Minutes:D2}:{absolute.Seconds:D2}";
        }
    }
}

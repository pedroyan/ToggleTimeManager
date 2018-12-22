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
            var minus = timeSpan < TimeSpan.Zero ? "- " : "";
            var hours = timeSpan.Days * 24 + timeSpan.Hours;
            return $"{minus}{hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }
}

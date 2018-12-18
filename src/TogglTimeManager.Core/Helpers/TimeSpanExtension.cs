using System;
using System.Collections.Generic;
using System.Text;

namespace TogglTimeManager.Core.Helpers
{
    //Credits: https://stackoverflow.com/questions/9909086/multiply-timespan-in-net
    //Unfortunately, .NET Standard doesn't provide direct multiplication between an int and a timespan value
    public static class TimeSpanExtension
    {
        /// <summary>
        /// Multiplies a timespan by an integer value
        /// </summary>
        public static TimeSpan Multiply(this TimeSpan multiplicand, int multiplier)
        {
            return TimeSpan.FromTicks(multiplicand.Ticks * multiplier);
        }

        /// <summary>
        /// Multiplies a timespan by a double value
        /// </summary>
        public static TimeSpan Multiply(this TimeSpan multiplicand, double multiplier)
        {
            return TimeSpan.FromTicks((long)(multiplicand.Ticks * multiplier));
        }
    }
}

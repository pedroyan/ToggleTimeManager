using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Core.Helpers
{
    public static class DateRangeHelper
    {
        /// <summary>
        /// Flattens overlapping date ranges into a single record and returns a list containing
        /// a timeline without overlapping ranges
        /// </summary>
        /// <param name="ranges">Dates to be flattened</param>
        /// <returns>a list containing
        /// a timeline without overlapping ranges</returns>
        public static IEnumerable<DateRange> Flatten(IEnumerable<DateRange> ranges)
        {
            if (ranges == null)
                return ranges;

            DateRange[] rangeArr = ranges.ToArray();
            return rangeArr.Length < 2 ? rangeArr : LazyFlatten(rangeArr);
        }

        private static IEnumerable<DateRange> LazyFlatten(IEnumerable<DateRange> ranges)
        {
            DateRange[] ordered = ranges.OrderBy(d => d.StartDate).ToArray();
            DateRange accumulator = ordered.First();
            IEnumerable<DateRange> intervals = ordered.Skip(1);

            foreach (DateRange interval in intervals)
            {
                if (interval.StartDate <= accumulator.EndDate)
                {
                    accumulator = Combine(accumulator, interval);
                }
                else
                {
                    yield return accumulator;
                    accumulator = interval;
                }
            }

            yield return accumulator;
        }

        private static DateRange Combine(DateRange start, DateRange end)
        {
            return new DateRange(start.StartDate, Max(start.EndDate, end.EndDate));
        }

        private static DateTime Max(DateTime left, DateTime right)
        {
            return (left > right) ? left : right;
        }
    }
}

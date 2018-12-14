using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ToggleTimeManager.Core.Models;

namespace ToggleTimeManager.Core
{
    public class TimeSummaryCalculator
    {
        public UserTimeSummary CalculateTimeSummary(TimeSpan workDayDuration, TimeSheet timeSheet)
        {
            if (timeSheet == null) throw new ArgumentNullException(nameof(timeSheet), "A time sheet instance is needed to calculate the summary");

            if (workDayDuration < TimeSpan.Zero)
            {
                throw new ArgumentException("A workday cannot have a negative duration");
            }

            if (!timeSheet.Period.HasValue)
            {
                throw new ArgumentException("A time sheet must contain the period for this calculation");
            }

            var workDays = GetWorkDays(timeSheet.Period.Value);

            var totalWorkHours = timeSheet.TimeEntries.Select(te => te.Duration)
                .Aggregate((sum, next) => sum + next);

            return new UserTimeSummary()
            {
                ContractedHours = workDays * workDayDuration,
                Period = timeSheet.Period.Value,
                WorkedHours = totalWorkHours
            };
        }

        /// <summary>
        /// Gets the number of working days on a <paramref name="period"/>
        /// </summary>
        /// <param name="period">The period being analyzed</param>
        /// <returns>The amount of thw workdays in the period</returns>
        private int GetWorkDays(DateRange period)
        {
            throw new NotImplementedException();
        }
    }
}

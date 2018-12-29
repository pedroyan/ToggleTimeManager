using System;
using System.Collections.Generic;
using System.Linq;
using TogglTimeManager.Core.Helpers;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Core
{
    public class TimeSummaryCalculator
    {
        /// <summary>
        /// Calculates the Work Hours summary
        /// </summary>
        /// <exception cref="ArgumentException">Gets thrown when the workday duration is negative or the time sheet provided
        /// has no period assigned to it</exception>
        /// <param name="workDayDuration">The duration of the workday stipulated on the contract</param>
        /// <param name="timeSheet">The time sheet to be evaluated</param>
        /// <param name="timeOffs">The time offs registered by a user</param>
        public static WorkHoursSummary CalculateHoursSummary(TimeSpan workDayDuration, TimeSheet timeSheet, IReadOnlyCollection<TimeOff> timeOffs)
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

            TimeSpan hoursWorked = timeSheet.TimeEntries.Select(te => te.Duration)
                .Aggregate((sum, next) => sum + next);

            TimeSpan plannedWork = CalculatePlannedWork(workDayDuration, timeSheet.Period.Value, timeOffs);

            return new WorkHoursSummary()
            {
                WorkDayDuration = workDayDuration,
                PlannedWork = plannedWork,
                Period = timeSheet.Period.Value,
                TimeWorked = hoursWorked
            };
        }

        /// <summary>
        /// Calculates the amount of planned work on a given period
        /// </summary>
        /// <param name="workDayDuration">The duration of a workday</param>
        /// <param name="analyzedPeriod">The period being analyzed</param>
        /// <param name="timeOffs">The time offs recorded by the user</param>
        /// <returns></returns>
        private static TimeSpan CalculatePlannedWork(TimeSpan workDayDuration, DateRange analyzedPeriod, IReadOnlyCollection<TimeOff> timeOffs)
        {
            int workDays = GetWorkingDays(analyzedPeriod);

            if (timeOffs != null && timeOffs.Count > 0)
                workDays = workDays - GeWorkDaysOff(analyzedPeriod, timeOffs.Select(to => to.Period));

            return workDayDuration.Multiply(workDays);
        }

        /// <summary>
        /// Gets the number of working days on a <paramref name="period"/>
        /// </summary>
        /// <param name="period">The period being analyzed</param>
        /// <returns>The amount of workdays in the period</returns>
        private static int GetWorkingDays(DateRange period)
        {
            int numberDays = (period.EndDate - period.StartDate).Days;

            int weeksPassed = numberDays / 7;
            int daysPassed = numberDays % 7;

            //Complete weeks always have 5 working days
            int workingDays = weeksPassed * 5;

            //The remaining of workdays depends on the starting week day and the week offset
            return workingDays + GetIntraWeekWorkingDays(period.StartDate, daysPassed);
        }

        /// <summary>
        /// Gets the number of working days between two weekdays based on a
        /// <paramref name="startDate"/> and a <paramref name="daysOffset"/>
        /// </summary>
        /// <param name="startDate">The reference weekday for the calculation</param>
        /// <param name="daysOffset">The number of days passed after the <paramref name="startDate"/>. Cannot be greater than 6 </param>
        /// <returns></returns>
        private static int GetIntraWeekWorkingDays(DateTime startDate, int daysOffset)
        {
            if (daysOffset > 6 || daysOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(daysOffset));
            }

            var dateIterator = startDate;
            var targetDate = startDate.AddDays(daysOffset);

            //This loop is fine since on the worst case scenario it will repeat itself only 6 times
            //(a constant maximum), therefore it's time complexity is O(1)
            var workdays = 0;
            while (dateIterator <= targetDate)
            {
                if (dateIterator.DayOfWeek != DayOfWeek.Saturday && dateIterator.DayOfWeek != DayOfWeek.Sunday)
                {
                    workdays++;
                }

                dateIterator = dateIterator.AddDays(1);
            }

            return workdays;
        }

        private static int GeWorkDaysOff(DateRange analyzedPeriod, IEnumerable<DateRange> timeOffs)
        {
            IEnumerable<DateRange> flattened = DateRangeHelper.Flatten(timeOffs);

            return (from interval in flattened
                where analyzedPeriod.Overlaps(interval)
                let minDate = interval.StartDate < analyzedPeriod.StartDate
                    ? analyzedPeriod.StartDate
                    : interval.StartDate
                let maxDate = interval.EndDate > analyzedPeriod.EndDate
                    ? analyzedPeriod.EndDate
                    : interval.EndDate
                select new DateRange(minDate, maxDate)
                into trimmed
                select GetWorkingDays(trimmed)).Sum();
        }
    }
}

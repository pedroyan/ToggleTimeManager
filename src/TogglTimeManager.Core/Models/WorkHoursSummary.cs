using System;

namespace TogglTimeManager.Core.Models
{
    public class WorkHoursSummary
    {
        /// <summary>
        /// The amount of time worked on the time period
        /// </summary>
        public TimeSpan TimeWorked { get; set; }

        /// <summary>
        /// The amount of time allocated to work on the time period
        /// </summary>
        public TimeSpan ExpectedWork { get; set; }

        /// <summary>
        /// The period described by this time summary
        /// </summary>
        public DateRange Period { get; set; }

        /// <summary>
        /// The time balance of the specified time period. A positive balance
        /// means the user worked more than the expected
        /// </summary>
        public TimeSpan WorkTimeBalance => TimeWorked - ExpectedWork;

    }
}

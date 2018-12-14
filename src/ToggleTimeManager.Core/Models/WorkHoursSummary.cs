using System;
using System.Collections.Generic;
using System.Text;

namespace ToggleTimeManager.Core.Models
{
    public class WorkHoursSummary
    {
        /// <summary>
        /// The amount of hours worked for the time period
        /// </summary>
        public TimeSpan WorkedHours { get; set; }

        /// <summary>
        /// The amount of hours allocated to work
        /// </summary>
        public TimeSpan ContractedHours { get; set; }

        /// <summary>
        /// The period described by this time summary
        /// </summary>
        public DateRange Period { get; set; }

        /// <summary>
        /// The work hours balance
        /// </summary>
        public TimeSpan WorkHoursBalance => WorkedHours - ContractedHours;

    }
}

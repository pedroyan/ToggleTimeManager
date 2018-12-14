using System;
using System.Collections.Generic;

namespace ToggleTimeManager.Core.Models
{
    public class TimeSheet
    {
        /// <summary>
        /// The start date of the time sheet
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The end date of the time sheet
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// All the time entries contained inside a TimeSheet
        /// </summary>
        public List<TimeEntry> TimeEntries { get; set; }
    }
}

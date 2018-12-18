using System.Collections.Generic;

namespace TogglTimeManager.Core.Models
{
    public class TimeSheet
    {
        public TimeSheet()
        {
            TimeEntries = new List<TimeEntry>();
        }

        /// <summary>
        /// The period of time covered by this time sheet
        /// </summary>
        public DateRange? Period { get; set; }

        /// <summary>
        /// All the time entries contained inside a TimeSheet
        /// </summary>
        public List<TimeEntry> TimeEntries { get; set; }
    }
}

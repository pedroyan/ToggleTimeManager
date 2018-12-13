using System;
using System.Collections.Generic;
using System.Text;

namespace ToggleTimeManager.Core
{
    public class TimeSheet
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public List<TimeEntry> TimeEntries { get; set; }
    }
}

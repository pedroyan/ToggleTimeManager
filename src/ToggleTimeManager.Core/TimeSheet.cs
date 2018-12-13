using System;
using System.Collections.Generic;
using System.Text;

namespace ToggleTimeManager.Core
{
    public class TimeSheet
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TimeEntry> TimeEntries { get; set; }
    }
}

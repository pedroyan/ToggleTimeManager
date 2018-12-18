using System;

namespace TogglTimeManager.Core.Models
{
    public class TimeEntry
    {
        /// <summary>
        /// The client of this time entry
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// The project of this time entry
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// The amount of time registered by this time entry
        /// </summary>
        public TimeSpan Duration { get; set; }
    }
}

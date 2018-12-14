using System;

namespace ToggleTimeManager.Core.Models
{
    public class DetailedTimeEntry : TimeEntry
    {
        /// <summary>
        /// The description of the entry
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The start time of the entry
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The end time of the entry
        /// </summary>
        public DateTime End { get; set; }
    }
}

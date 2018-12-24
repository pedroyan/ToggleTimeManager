using System;
using System.Collections.Generic;
using System.Text;

namespace TogglTimeManager.Core.Models
{
    public class TimeOff
    {
        /// <summary>
        /// The period of the time off
        /// </summary>
        public DateRange Period { get; set; }

        /// <summary>
        /// A description for the time off
        /// </summary>
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ToggleTimeManager.Core.Models
{
    public struct DateRange
    {
        /// <summary>
        /// The start of the date range
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the date range
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}

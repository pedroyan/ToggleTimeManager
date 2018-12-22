using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;

namespace TogglTimeManager.ViewModels
{
    public class MainDashboardViewModel : BoundObject
    {
        private readonly WorkHoursSummary _summary;

        public MainDashboardViewModel(WorkHoursSummary summary)
        {
            _summary = summary ?? throw new ArgumentNullException(nameof(summary), "Summary cannot be null");
        }

        public string TimeWorked => _summary.TimeWorked.ToHoursString();
    }
}

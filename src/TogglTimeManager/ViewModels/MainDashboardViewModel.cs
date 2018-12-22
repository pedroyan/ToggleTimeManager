using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TogglTimeManager.Core.Models;
using TogglTimeManager.Helpers;

namespace TogglTimeManager.ViewModels
{
    public class MainDashboardViewModel : BoundObject
    {
        private readonly WorkHoursSummary _summary;

        public MainDashboardViewModel(WorkHoursSummary summary, TimeSpan workdayDuration)
        {
            _summary = summary ?? throw new ArgumentNullException(nameof(summary), "Summary cannot be null");
        }

        public string TimeWorked => _summary.TimeWorked.ToHoursString();

        public string ExpectedWork => _summary.PlannedWork.ToHoursString();

        public string WorkTimeBalance => _summary.WorkTimeBalance.ToHoursString();

        public string AnalyzedPeriod => $"Analyzed period: {_summary.Period.StartDate:d} - {_summary.Period.EndDate:d}";

        public Brush WorkTimeBalanceColor
        {
            get
            {
                
                if (_summary.WorkTimeBalance == TimeSpan.Zero)
                {
                    return new SolidColorBrush(Colors.Black);
                }

                if (_summary.WorkTimeBalance > TimeSpan.Zero)
                {
                    return new SolidColorBrush(Colors.Green);
                }

                return new SolidColorBrush(Colors.Red);
            }
        }
    }
}

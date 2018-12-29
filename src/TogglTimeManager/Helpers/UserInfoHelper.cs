using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TogglTimeManager.Core;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Helpers
{
    public static class UserInfoHelper
    {
        public static WorkHoursSummary CalculateSummary(this UserInfo user)
        {
            return TimeSummaryCalculator.CalculateHoursSummary(user.WorkContract, user.TimeSheet, user.TimeOffs);
        }
    }
}

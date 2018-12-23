using System;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager
{
    public class UserInfo
    {
        public WorkHoursSummary Summary { get; set; }
        public TimeSpan WorkContract { get; set; }
    }
}

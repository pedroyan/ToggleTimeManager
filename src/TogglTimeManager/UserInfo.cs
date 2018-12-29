using System;
using System.Collections.Generic;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager
{
    public class UserInfo
    {
        public TimeSheet TimeSheet { get; set; }
        public TimeSpan WorkContract { get; set; }
        public List<TimeOff> TimeOffs { get; set; } = new List<TimeOff>();
    }
}

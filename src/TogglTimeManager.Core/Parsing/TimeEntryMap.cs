using CsvHelper.Configuration;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Core.Parsing
{
    public sealed class TimeEntryMap : ClassMap<TimeEntry>
    {
        public TimeEntryMap()
        {
            AutoMap();
            Map(m => m.Duration)
                .TypeConverter<TimeConverter>().Name("Registered time");
        }
    }
}

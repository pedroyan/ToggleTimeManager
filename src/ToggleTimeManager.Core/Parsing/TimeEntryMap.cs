using CsvHelper.Configuration;

namespace ToggleTimeManager.Core.Parsing
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

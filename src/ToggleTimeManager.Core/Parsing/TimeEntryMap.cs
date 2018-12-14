using CsvHelper.Configuration;

namespace ToggleTimeManager.Core.Parsing
{
    public sealed class TimeEntryMap : ClassMap<TimeEntry>
    {
        public TimeEntryMap()
        {
            AutoMap();
            Map(m => m.RegisteredTime)
                .TypeConverter<TimeConverter>().Name("Registered time");
        }
    }
}

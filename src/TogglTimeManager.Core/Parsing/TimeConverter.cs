using System;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace TogglTimeManager.Core.Parsing
{
    public class TimeConverter : ITypeConverter
    {
        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            //We wont ever be writing a type back to CSV
            throw new NotImplementedException();
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
            {
                return TimeSpan.Zero;
            }

            var splitTime = text.Split(':');

            if (splitTime.Length != 3)
            {
                throw new FormatException($"The time format is invalid. Ensure the time on the csv follows the format (HH:mm:ss).\nValue: {text}, Line {row.Context.Row}");
            }

            if (!int.TryParse(splitTime[0], out var hour))
            {
                throw new FormatException($"The hour is not a valid integer.\nValue: {text}, Line {row.Context.Row}");
            }

            if (!int.TryParse(splitTime[1], out var minutes))
            {
                throw new FormatException($"The minute is not a valid integer.\nValue: {text}, Line {row.Context.Row}");
            }

            if (!int.TryParse(splitTime[2], out var seconds))
            {
                throw new FormatException($"The second is not a valid integer.\nValue: {text}, Line {row.Context.Row}");
            }

            var days = hour / 24;
            var remainingHours = hour % 24;

            return new TimeSpan(days, remainingHours, minutes, seconds);
        }
    }
}

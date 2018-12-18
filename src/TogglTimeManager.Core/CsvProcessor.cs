using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ToggleTimeManager.Core.Models;
using ToggleTimeManager.Core.Parsing;
using TogglTimeManager.Core.Parsing;

namespace ToggleTimeManager.Core
{
    public static class CsvProcessor
    {
        /// <summary>
        /// Obtains a <see cref="TimeSheet"/> from a Toggl csv summary file
        /// </summary>
        /// <param name="filePath">the path to the csv file</param>
        /// <returns>The parsed <see cref="TimeSheet"/></returns>
        public static TimeSheet ProcessCsvFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("The file path cannot be null or empty", nameof(filePath));
            }

            var fileName = Path.GetFileName(filePath);
            if (!fileName.EndsWith(".csv"))
            {
                throw new ArgumentException("The select file is not a CSV");
            }

            List<TimeEntry> records = TogglCsvParser.ParseCsv(filePath);
            var timeSheet = new TimeSheet
            {
                TimeEntries = records.ToList(),
                Period = ParseDateRangeFromFileName(fileName)
            };

            return timeSheet;
        }

        private static DateRange? ParseDateRangeFromFileName(string fileName)
        {
            var match = Regex.Match(fileName,
                "^Toggl_projects_([0-9]+-[0-9]+-[0-9]+)_to_([0-9]+-[0-9]+-[0-9]+)\\.csv");

            if (!match.Success)
            {
                //Cant infer date from file name if the name is not on the default format
                return null;
            }

            var startDateStr = match.Groups[1].Value;
            if (!DateTime.TryParseExact(startDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var parsedStart))
            {
                return null;
            }

            var endDateStr = match.Groups[2].Value;
            if (!DateTime.TryParseExact(endDateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedEnd))
            {
                return null;
            }

            if (parsedStart > parsedEnd)
            {
                //The date on the csv file is invalid, therefore null is returned
                return null;
            }

            return new DateRange(parsedStart, parsedEnd);
        }
    }
}

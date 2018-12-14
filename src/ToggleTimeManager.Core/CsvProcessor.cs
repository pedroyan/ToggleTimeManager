using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToggleTimeManager.Core.Parsing;

namespace ToggleTimeManager.Core
{
    public static class CsvProcessor
    {
        public static TimeSheet ProcessCsvFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("The file path cannot be null or empty", nameof(filePath));
            }

            if (!filePath.EndsWith(".csv"))
            {
                throw new ArgumentException("The select file is not a CSV");
            }

            List<TimeEntry> records = TogglCsvParser.ParseCsv(filePath);

            return new TimeSheet
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                TimeEntries = records.ToList()
            };
        }

   
    }
}

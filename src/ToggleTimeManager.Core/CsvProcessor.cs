using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

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


            IEnumerable<TimeEntry> records;
            using (var reader = File.OpenText(filePath))
            {
                var csvParser = new CsvReader(reader);
                records = csvParser.GetRecords<TimeEntry>();
            }

            return new TimeSheet()
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                TimeEntries = records.ToList()
            };

        }

    }
}

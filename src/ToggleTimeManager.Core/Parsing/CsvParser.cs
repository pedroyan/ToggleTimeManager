using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace ToggleTimeManager.Core.Parsing
{
    public static class TogglCsvParser
    {
        internal static List<TimeEntry> ParseCsv(string filePath)
        {
            using (var reader = File.OpenText(filePath))
            {
                var csvParser = new CsvReader(reader);
                csvParser.Configuration.RegisterClassMap<TimeEntryMap>();
                return csvParser.GetRecords<TimeEntry>().ToList();
            }
        }
    }
}

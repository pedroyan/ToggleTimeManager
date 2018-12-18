using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using TogglTimeManager.Core.Models;

namespace TogglTimeManager.Core.Parsing
{
    internal static class TogglCsvParser
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

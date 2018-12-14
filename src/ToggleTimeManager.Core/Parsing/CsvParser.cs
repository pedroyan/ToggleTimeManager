using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace ToggleTimeManager.Core.Parsing
{
    internal static class TogglCsvParser
    {
        internal static List<TimeEntry> ParseCsv(string filePath)
        {
            using (var reader = File.OpenText(filePath))
            {
                //TODO: Logic to figure out if this is a detailed time entry or not
                var csvParser = new CsvReader(reader);
                csvParser.Configuration.RegisterClassMap<TimeEntryMap>();
                return csvParser.GetRecords<TimeEntry>().ToList();
            }
        }
    }
}

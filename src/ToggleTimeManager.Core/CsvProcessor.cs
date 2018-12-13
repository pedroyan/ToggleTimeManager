using System;

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


            //Remember to check the book Domain Driven Design by Eric Evans on the chapter that
            //talks about factories and reconstitution (page 146)
            throw new NotImplementedException("Actual parsing still needs implementation");
        }
    }
}

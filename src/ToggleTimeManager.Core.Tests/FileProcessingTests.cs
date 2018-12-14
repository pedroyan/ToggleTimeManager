using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace ToggleTimeManager.Core.Tests
{
    [TestFixture]
    public class FileProcessingTests
    {
        [Test]
        public void SuccessfulProcessingTest()
        {
            var resultSheet = CsvProcessor.ProcessCsvFile("TestFiles/Toggl_projects_2018-11-01_to_2018-11-30.csv");

            Assert.AreEqual(5, resultSheet.TimeEntries.Count);

            resultSheet.TimeEntries[0]
                .AssertRecord("Pedro Company", "CT", new TimeSpan(14, 41, 36));

            resultSheet.TimeEntries[1]
                .AssertRecord("Pedro Company", "MbOS", new TimeSpan(2, 5, 39));

            resultSheet.TimeEntries[2]
                .AssertRecord("Pedro Company", "Game Engine", new TimeSpan(2, 31, 38));

            resultSheet.TimeEntries[3]
                .AssertRecord("Pedro Company", "HiddenProject", new TimeSpan(4,14,24,53));
        }
    }
}

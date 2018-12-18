using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using NUnit.Framework;
using TogglTimeManager.Core;

namespace ToggleTimeManager.Core.Tests
{
    [TestFixture]
    public class FileProcessingTests
    {
        [Test]
        public void SuccessfulProcessedSimpleEntriesSheetTest()
        {
            var resultSheet = CsvProcessor.ProcessCsvFile("TestFiles/Toggl_projects_2018-11-01_to_2018-11-30.csv");

            Assert.AreEqual(5, resultSheet.TimeEntries.Count);

            Debug.Assert(resultSheet.Period != null, "resultSheet.Period != null");
            Assert.AreEqual(new DateTime(2018, 11, 01), resultSheet.Period.Value.StartDate);
            Assert.AreEqual(new DateTime(2018, 11, 30), resultSheet.Period.Value.EndDate);

            resultSheet.TimeEntries[0]
                .AssertRecord("Pedro Company", "CT", new TimeSpan(14, 41, 36));

            resultSheet.TimeEntries[1]
                .AssertRecord("Pedro Company", "MbOS", new TimeSpan(2, 5, 39));

            resultSheet.TimeEntries[2]
                .AssertRecord("Pedro Company", "Game Engine", new TimeSpan(2, 31, 38));

            resultSheet.TimeEntries[3]
                .AssertRecord("Pedro Company", "HiddenProject", new TimeSpan(4, 14, 24, 53));
        }

        [Test]
        public void ShouldThrowInvalidExtension()
        {
            var file = "TestFiles/Toggl_projects_2018-11-01_to_2018-11-30.xml";

            TestDelegate code = () => CsvProcessor.ProcessCsvFile(file);

            Assert.Throws<ArgumentException>(code);
        }

        [Test]
        public void SuccessfulProcessedTimeSheetWithoutDate()
        {
            var resultSheet = CsvProcessor.ProcessCsvFile("TestFiles/MyProjects.csv");

            Assert.AreEqual(5, resultSheet.TimeEntries.Count);

            Debug.Assert(resultSheet.Period == null, "resultSheet.Period == null");

            resultSheet.TimeEntries[0]
                .AssertRecord("Pedro Company", "CT", new TimeSpan(14, 41, 36));

            resultSheet.TimeEntries[1]
                .AssertRecord("Pedro Company", "MbOS", new TimeSpan(2, 5, 39));

            resultSheet.TimeEntries[2]
                .AssertRecord("Pedro Company", "Game Engine", new TimeSpan(2, 31, 38));

            resultSheet.TimeEntries[3]
                .AssertRecord("Pedro Company", "HiddenProject", new TimeSpan(4, 14, 24, 53));
        }

        //TODO: Add tests for files with invalid headers
    }
}

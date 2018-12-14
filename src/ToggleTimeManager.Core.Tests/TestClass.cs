using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace ToggleTimeManager.Core.Tests
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void SomeTestMethod()
        {
            var resultSheet = CsvProcessor.ProcessCsvFile("TestFiles/Toggl_projects_2018-11-01_to_2018-11-30.csv");
            
            Assert.AreEqual(5, resultSheet.TimeEntries.Count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToggleTimeManager.Core.Models;

namespace ToggleTimeManager.Core.Tests
{
    [TestFixture]
    public class SummaryCalculationTests
    {
        [Test]
        public void SummaryCalculationTest()
        {
            var timeSheet = new TimeSheet()
            {
                Period = new DateRange(new DateTime(2018, 12, 7), new DateTime(2018, 12, 17)),
                TimeEntries = new List<TimeEntry>()
                {
                    new TimeEntry()
                    {
                        Duration = TimeSpan.FromHours(7 * 6), //6 hours during 7 workdays
                        Client = "Mock client",
                        Project = "Normal project"
                    },
                    new TimeEntry()
                    {
                        Duration = TimeSpan.FromHours(10),
                        Client = "Mock client",
                        Project = "Extra hours project "
                    }
                }
            };

            const int expectedWorkdays = 7;
            var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), timeSheet);

            Assert.AreEqual(summary.Period, timeSheet.Period);
            Assert.AreEqual(summary.ExpectedWork, TimeSpan.FromHours(expectedWorkdays * 6));
            Assert.AreEqual(summary.TimeWorked, timeSheet.TimeEntries[0].Duration + timeSheet.TimeEntries[1].Duration);
            Assert.AreEqual(summary.WorkTimeBalance, TimeSpan.FromHours(10));
        }
    }
}

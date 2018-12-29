using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TogglTimeManager.Core;
using TogglTimeManager.Core.Models;

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
                //7 business days
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
            var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), timeSheet, null);

            Assert.AreEqual(summary.Period, timeSheet.Period);
            Assert.AreEqual(summary.PlannedWork, TimeSpan.FromHours(expectedWorkdays * 6));
            Assert.AreEqual(summary.TimeWorked, timeSheet.TimeEntries[0].Duration + timeSheet.TimeEntries[1].Duration);
            Assert.AreEqual(summary.WorkTimeBalance, TimeSpan.FromHours(10));
        }

        [Test]
        public void SingleDaySummaryCalculationTest()
        {
            var timeSheet = new TimeSheet()
            {
                //7 business days
                Period = new DateRange(new DateTime(2018, 12, 7), new DateTime(2018, 12, 7)),
                TimeEntries = new List<TimeEntry>()
                {
                    new TimeEntry()
                    {
                        Duration = TimeSpan.FromHours(6), //6 hours during 7 workdays
                        Client = "Mock client",
                        Project = "Normal project"
                    }
                }
            };

            var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), timeSheet, null);

            Assert.AreEqual(timeSheet.Period, summary.Period);
            Assert.AreEqual(TimeSpan.FromHours(6), summary.PlannedWork);
            Assert.AreEqual(timeSheet.TimeEntries[0].Duration, summary.TimeWorked);
            Assert.AreEqual(TimeSpan.FromHours(0), summary.WorkTimeBalance);
        }

        [Test]
        public void SummaryCalculationWithVacations()
        {
            var timeSheet = new TimeSheet()
            {
                //21 week days in the month
                Period = new DateRange(new DateTime(2018, 12, 1), new DateTime(2018, 12, 31)),
                TimeEntries = new List<TimeEntry>()
                {
                    new TimeEntry()
                    {
                        Duration = TimeSpan.FromHours(6*7),
                        Client = "Mock Client",
                        Project = "Normal project"
                    }
                }
            };

            var timeOff = new List<TimeOff>()
            {
                new TimeOff()
                {
                    //11 workdays on vacations, 15 total days on vacations
                    Period = new DateRange(new DateTime(2018, 12, 7), new DateTime(2018, 12, 21)),
                    Description = "Vacations"
                },
                new TimeOff()
                {
                    //Overlapping with vacations, so it shouldn't affect the calculations
                    Period = new DateRange(new DateTime(2018,12,11), new DateTime(2018,12,13)),
                    Description = "No clients scheduled"
                },
                new TimeOff()
                {
                    //3 days off
                    Period = new DateRange(new DateTime(2018,12,24), new DateTime(2018,12,26)),
                    Description = "Christmas!"
                }
            };

            var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), timeSheet, timeOff);

            Assert.AreEqual(timeSheet.Period, summary.Period);
            Assert.AreEqual(TimeSpan.FromHours(6 * 7), summary.PlannedWork);
            Assert.AreEqual(TimeSpan.FromHours(6 * 7), summary.TimeWorked);
            Assert.AreEqual(TimeSpan.Zero, summary.WorkTimeBalance);
        }

        [Test]
        public void SummaryCalculationWithVacationsAfterTheAnalyzedPeriod()
        {
            int workDays = 93;
            var timeSheet = new TimeSheet()
            {
                //98 week days
                Period = new DateRange(new DateTime(2018, 8, 1), new DateTime(2018, 12, 7)),
                TimeEntries = new List<TimeEntry>()
                {
                    new TimeEntry()
                    {
                        Duration = TimeSpan.FromHours(6*workDays),
                        Client = "Mock Client",
                        Project = "Normal project"
                    }
                }
            };

            var timeOff = new List<TimeOff>()
            {
                new TimeOff()
                {
                    //Vacations is after analyzed period
                    Period = new DateRange(new DateTime(2018, 12, 29), new DateTime(2018, 12, 31)),
                    Description = "Vacations"
                }
            };

            var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), timeSheet, timeOff);

            Assert.AreEqual(timeSheet.Period, summary.Period);
            Assert.AreEqual(TimeSpan.FromHours(6 * workDays), summary.PlannedWork);
            Assert.AreEqual(TimeSpan.FromHours(6 * workDays), summary.TimeWorked);
            Assert.AreEqual(TimeSpan.Zero, summary.WorkTimeBalance);
        }

        [Test]
        public void SummaryCalculationWithVacationsBeforeTheAnalyzedPeriod()
        {
            const int workDays = 93;
            var timeSheet = new TimeSheet()
            {
                //98 week days
                Period = new DateRange(new DateTime(2018, 8, 1), new DateTime(2018, 12, 7)),
                TimeEntries = new List<TimeEntry>()
                {
                    new TimeEntry()
                    {
                        Duration = TimeSpan.FromHours(6*workDays),
                        Client = "Mock Client",
                        Project = "Normal project"
                    }
                }
            };

            var timeOff = new List<TimeOff>()
            {
                new TimeOff()
                {
                    //Vacations is after analyzed period
                    Period = new DateRange(new DateTime(2018, 1, 10), new DateTime(2018, 2, 10)),
                    Description = "Vacations"
                }
            };

            var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), timeSheet, timeOff);

            Assert.AreEqual(timeSheet.Period, summary.Period);
            Assert.AreEqual(TimeSpan.FromHours(6 * workDays), summary.PlannedWork);
            Assert.AreEqual(TimeSpan.FromHours(6 * workDays), summary.TimeWorked);
            Assert.AreEqual(TimeSpan.Zero, summary.WorkTimeBalance);
        }

        [Test]
        public void SummaryCalculationWithVacationsPartiallyOverlappingTheWorkingPeriod()
        {
            const int weekDays = 23;
            var timeSheet = new TimeSheet()
            {
                Period = new DateRange(new DateTime(2018, 8, 1), new DateTime(2018, 8, 31)),
                TimeEntries = new List<TimeEntry>()
                {
                    new TimeEntry()
                    {
                        Duration = TimeSpan.FromHours(6*weekDays),
                        Client = "Mock Client",
                        Project = "Normal project"
                    }
                }
            };

            var timeOff = new List<TimeOff>()
            {
                new TimeOff()
                {
                    //Intersects analyzed period from the bottom
                    Period = new DateRange(new DateTime(2018, 7, 10), new DateTime(2018, 8, 1)),
                    Description = "Vacations"
                },
                new TimeOff()
                {
                    //Intersects analyzed period from the top
                    Period = new DateRange(new DateTime(2018, 8, 31), new DateTime(2018, 9, 10)),
                    Description = "Vacations 2"
                },
            };

            var summary = TimeSummaryCalculator.CalculateHoursSummary(TimeSpan.FromHours(6), timeSheet, timeOff);

            Assert.AreEqual(timeSheet.Period, summary.Period);
            Assert.AreEqual(TimeSpan.FromHours(6 * (weekDays - 2)), summary.PlannedWork);
            Assert.AreEqual(TimeSpan.FromHours(6 * weekDays), summary.TimeWorked);
            Assert.AreEqual(TimeSpan.FromHours(6 * 2), summary.WorkTimeBalance);
        }
    }
}

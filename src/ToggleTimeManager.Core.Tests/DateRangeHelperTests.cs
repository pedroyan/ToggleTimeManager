using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TogglTimeManager.Core.Helpers;
using TogglTimeManager.Core.Models;

namespace ToggleTimeManager.Core.Tests
{
    [TestFixture]
    public class DateRangeHelperTests
    {
        private static DateTime Today => DateTime.Today;

        [Test]
        public void FlattenIntoASingleRangeTest()
        {

            //[(1, 5), (2, 4), (3, 6)] --->  [(1,6)]
            var ranges = new List<DateRange>()
            {
                QuickRange(1, 5),
                QuickRange(2, 4),
                QuickRange(3, 6),
            };

            var flattened = DateRangeHelper.Flatten(ranges).ToList();

            Assert.IsTrue(flattened.Count == 1, "flattened.Count == 1");
            Assert.AreEqual(QuickRange(1,6), flattened[0]);
        }

        [Test]
        public void FlattenIntoMultipleRanges()
        {
            //[(0, 3), (1, 2), (2, 4), (5, 9), (6, 8)] --> [(0, 4), (5, 9)]
            var ranges = new List<DateRange>()
            {
                QuickRange(1, 2),
                QuickRange(5, 9),
                QuickRange(2, 4),
                QuickRange(6, 8),
                QuickRange(0, 3),
            };

            List<DateRange> flattened = DateRangeHelper.Flatten(ranges).ToList();

            Assert.IsTrue(flattened.Count == 2, "flattened.Count == 2");
            Assert.AreEqual(QuickRange(0,4), flattened[0]);
            Assert.AreEqual(QuickRange(5,9), flattened[1]);
        }

        private static DateRange QuickRange(int start, int end)
        {
            return new DateRange(Today.AddDays(start), Today.AddDays(end));
        }
    }
}

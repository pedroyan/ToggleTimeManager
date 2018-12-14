using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ToggleTimeManager.Core.Tests
{
    public static class TestExtensions
    {
        public static void AssertRecord(this TimeEntry entry, string client,
            string project, TimeSpan duration)
        {
            Assert.AreEqual(client, entry.Client);
            Assert.AreEqual(project, entry.Project);
            Assert.AreEqual(duration, entry.Duration);
        }
    }
}

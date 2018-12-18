using System;

namespace TogglTimeManager.Core.Models
{
    public struct DateRange : IEquatable<DateRange>
    {
        /// <summary>
        /// Creates a <see cref="DateRange"/>
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the end date comes before the start date</exception>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException("End date cannot come before start date");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// The start of the date range
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// The end date of the date range
        /// </summary>
        public DateTime EndDate { get; }

        #region Equality

        public bool Equals(DateRange other)
        {
            return StartDate.Equals(other.StartDate) && EndDate.Equals(other.EndDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DateRange other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StartDate.GetHashCode() * 397) ^ EndDate.GetHashCode();
            }
        }

        public static bool operator ==(DateRange left, DateRange right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DateRange left, DateRange right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}

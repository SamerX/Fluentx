using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// A class represents a time period between to date item instances.
    /// </summary>
    public class Period
    {
        /// <summary>
        /// Period start datetime
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// Period end datetime
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// Period length as a timespan
        /// </summary>
        public TimeSpan Length { get { return End - Start; } }
        /// <summary>
        /// Creates a period with the specified start and end dates
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Period(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }
        /// <summary>
        /// Returns if the specified period overlaps with the current one, a boolean value indicates wether edges should be calculated in the overlap or not, by default its false
        /// </summary>
        /// <param name="otherPeriod"></param>
        /// <param name="edgesOverlap"></param>
        /// <returns></returns>
        public bool IsOverlap(Period otherPeriod, bool edgesOverlap = false)
        {
            Guard.Against<ArgumentNullException>(otherPeriod.IsNull(), "Specified period is Null.");

            if (edgesOverlap)
            {
                return this.Start <= otherPeriod.End && this.End >= otherPeriod.Start;
            }
            else
            {
                return this.Start < otherPeriod.End && this.End > otherPeriod.Start;
            }
        }
        /// <summary>
        /// Returns a boolean wether the specified date is within the current period.(edges are not calculated within)
        /// </summary>
        /// <param name="date"></param>
        /// /// <param name="includeEdges"></param>
        /// <returns></returns>
        public bool IsWrap(DateTime date, bool includeEdges = false)
        {
            if (includeEdges)
            {
                return this.Start <= date && this.End >= date;
            }
            else
            {
                return this.Start < date && this.End > date;
            }
        }
    }
}

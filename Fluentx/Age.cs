using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// A class representing a simple age entity.
    /// </summary>
    public struct Age
    {
        /// <summary>
        /// Years
        /// </summary>
        public int Years { get; set; }
        /// <summary>
        /// Months
        /// </summary>
        public int Months { get; set; }
        /// <summary>
        /// Days
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// Hours
        /// </summary>
        public int Hours { get; set; }
        /// <summary>
        /// Minutes
        /// </summary>
        public int Minutes { get; set; }
        /// <summary>
        /// Seconds
        /// </summary>
        public int Seconds { get; set; }
        /// <summary>
        /// Returns a human readable text for the age entity.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Years} Years {Months} Months {Days} Days {Hours} Hours {Minutes} Minutes {Seconds} Seconds";
        }
        /// <summary>
        /// Returns a human readable text for the age entity with only years, months and days.
        /// </summary>
        /// <returns></returns>
        public string ToYMDString()
        {
            return $"{Years} Years {Months} Months {Days} Days";
        }
    }
}

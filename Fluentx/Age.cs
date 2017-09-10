using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    public struct Age
    {
        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public override string ToString()
        {
            return $"{Years} Years {Months} Months {Days} Days {Hours} Hours {Minutes} Minutes {Seconds} Seconds";
        }
        public string ToYMDString()
        {
            return $"{Years} Years {Months} Months {Days} Days";
        }
    }
}

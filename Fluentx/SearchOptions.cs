using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// A simple container class to transfer search related data. Equivalent for UISearch with tweaks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchOptions<T>
    {
        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Sort By
        /// </summary>
        public string SortBy { get; set; }
        /// <summary>
        /// Sort Direction ASC or DESC, default is true ASC.
        /// </summary>
        public bool IsAscending { get; set; } = true;
        /// <summary>
        /// The criteria used for filter
        /// </summary>
        public T Criteria { get; set; }
    }
}

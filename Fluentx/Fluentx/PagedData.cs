using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// A simple container class for paged data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedData<T>
    {
        /// <summary>
        /// The paged data returned
        /// </summary>
        public IList<T> Data { get; set; }
        /// <summary>
        /// Total count of data without paging
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Sorty by
        /// </summary>
        public string SortBy { get; set; }
        /// <summary>
        /// Sort direction
        /// </summary>
        public string SortDirection { get; set; }
    }
}

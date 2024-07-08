using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// A simple container class to transfer ui search related data. This class is the same as Search class, preferably to use Search class instead.
    /// </summary>
    public class UISearch
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
        /// Sort Direction
        /// </summary>
        public string SortDirection { get; set; }
    }

    /// <summary>
    /// A simple container class to transfer ui search related data. This class is the same as Search class, preferably to use Search class instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UISearch<T> : UISearch
    {
        /// <summary>
        /// The criteria used for filter
        /// </summary>
        public T Criteria { get; set; }
    }
}
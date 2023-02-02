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
    public class FxSearch
    {
        /// <summary>
        /// Page Index
        /// </summary>
        public int? PageIndex { get; set; }
        /// <summary>
        /// Page Size
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// Sort By
        /// </summary>
        public string SortBy { get; set; }
        /// <summary>
        /// Sort Direction ASC or DESC, default value is false which is sort ASC.
        /// </summary>
        public bool? IsDescending { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public FxSearch()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        public FxSearch(int? pageIndex = null, int? pageSize = null, string sortBy = null, bool? isDescending = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortBy = sortBy;
            IsDescending = isDescending;
        }
    }
    /// <summary>
    /// A simple container class to transfer search related data. Equivalent for UISearch with tweaks
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FxSearch<T> : FxSearch
    {
        /// <summary>
        /// The criteria used for filter
        /// </summary>
        public T Criteria { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <param name="isDescending"></param>
        public FxSearch(int? pageIndex = null, int? pageSize = null, string sortBy = null, bool? isDescending = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortBy = sortBy;
            IsDescending = isDescending;
        }
    }
}

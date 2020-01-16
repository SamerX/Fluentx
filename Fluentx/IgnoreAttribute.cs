using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// A generic purpose ignore attribute with support of categorization (Multipurpose Attribute)
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class IgnoreAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public IgnoreAttribute()
        {

        }
        /// <summary>
        /// This category is for categorization purposes to allow the using of the attribute simultaniously by different models
        /// </summary>
        /// <param name="category"></param>
        public IgnoreAttribute(string category)
        {
            this.Category = category;
        }
    }
}

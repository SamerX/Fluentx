using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    internal class FxEqualityComparer<TSource, TKey>
        : IEqualityComparer<TSource>
    {
        readonly Func<TSource, TKey> projection;
        readonly IEqualityComparer<TKey> comparer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projection"></param>
        public FxEqualityComparer(Func<TSource, TKey> projection)
            : this(projection, null)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projection"></param>
        /// <param name="comparer"></param>
        public FxEqualityComparer(Func<TSource, TKey> projection, IEqualityComparer<TKey> comparer)
        {
            if (projection is null)
            {
                throw new ArgumentNullException("Projection parameter cant be null");
            }
            this.comparer = comparer ?? EqualityComparer<TKey>.Default;
            this.projection = projection;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(TSource x, TSource y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            return comparer.Equals(projection(x), projection(y));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(TSource obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("GetHashCode argument obj cant be null.");
            }
            return comparer.GetHashCode(projection(obj));
        }
    }
}

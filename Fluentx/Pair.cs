using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    /// <summary>
    /// Represents a pair of left and right
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public class Pair<TLeft, TRight>
    {
        /// <summary>
        /// Left
        /// </summary>
        public TLeft Left { get; set; }
        /// <summary>
        /// Right
        /// </summary>
        public TRight Right { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public Pair(TLeft left, TRight right)
        {
            Left = left;
            Right = right;
        }
        /// <summary>
        /// 
        /// </summary>
        public Pair()
        {

        }
    }

    /// <summary>
    /// Represent a parent child data structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Filial<T>
    {
        /// <summary>
        /// Parent
        /// </summary>
        public T Parent { get; set; }
        /// <summary>
        /// Child
        /// </summary>
        public T Child { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public Filial(T parent, T child)
        {
            Parent = parent;
            Child = child;
        }
        /// <summary>
        /// 
        /// </summary>
        public Filial()
        {

        }
    }



}

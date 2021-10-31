//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;

//namespace Fluentx
//{
//    /// <summary>
//    /// A circular list where next for the end will return you to the begining of the list
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class CircularList<T> : List<T>
//    {
//        public T GetNext
//    }
//    /// <summary>
//    /// Circular Enumerator
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class CircularEnumerator<T> : IEnumerator<T>
//    {
//        private readonly List<T> list;
//        int i = 0;
//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="list"></param>
//        public CircularEnumerator(List<T> list)
//        {
//            this.list = list;
//        }
//        /// <summary>
//        /// Current
//        /// </summary>
//        public T Current => list[i];
//        /// <summary>
//        /// Current
//        /// </summary>
//        object IEnumerator.Current => this;
//        /// <summary>
//        /// Dispose
//        /// </summary>
//        public void Dispose()
//        {

//        }
//        /// <summary>
//        /// Move Next
//        /// </summary>
//        /// <returns></returns>
//        public bool MoveNext()
//        {
//            i = (i + 1) % list.Count;
//            return true;
//        }
//        /// <summary>
//        /// Reset
//        /// </summary>
//        public void Reset()
//        {
//            i = 0;
//        }
//    }
//}

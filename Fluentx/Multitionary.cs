using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TKuple"></typeparam>
    public abstract class MultitionaryBase<TKey, TKuple> where TKuple : IKuple<TKey>//, IEnumerator<TKuple>, IEnumerable<TKuple>
    {
        List<TKuple> list = new List<TKuple>();
        int position = 0;
        /// <summary>
        /// 
        /// </summary>
        public MultitionaryBase()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public TKuple Current
        {
            get { return list[position]; }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            list = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            position++;
            return position < list.Count;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            position = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TKuple> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(TKuple item)
        {
            list.Add(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            int index = list.FindIndex(x => Comparer<TKey>.Default.Compare(x.Key, key) == 0);
            if (index > -1)
            {
                list.RemoveAt(index);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            foreach (var item in list)
            {
                if (Comparer<TKey>.Default.Compare(item.Key, key) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TKuple this[TKey key]
        {
            get
            {
                int index = list.FindIndex(x => Comparer<TKey>.Default.Compare(x.Key, key) == 0);
                return list[index];
            }
            set
            {
                int index = list.FindIndex(x => Comparer<TKey>.Default.Compare(x.Key, key) == 0);
                list[index] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
    }
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public class Multitionary<TKey, T1> : MultitionaryBase<TKey, Kuple<TKey, T1>>
    {

    }
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class Multitionary<TKey, T1, T2> : MultitionaryBase<TKey, Kuple<TKey, T1, T2>>
    {

    }
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public class Multitionary<TKey, T1, T2, T3> : MultitionaryBase<TKey, Kuple<TKey, T1, T2, T3>>
    {

    }
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    public class Multitionary<TKey, T1, T2, T3, T4> : MultitionaryBase<TKey, Kuple<TKey, T1, T2, T3, T4>>
    {

    }
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    public class Multitionary<TKey, T1, T2, T3, T4, T5> : MultitionaryBase<TKey, Kuple<TKey, T1, T2, T3, T4, T5>>
    {

    }
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    public class Multitionary<TKey, T1, T2, T3, T4, T5, T6> : MultitionaryBase<TKey, Kuple<TKey, T1, T2, T3, T4, T5, T6>>
    {

    }
    /// <summary>
    /// Multi-value dictionary identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    public class Multitionary<TKey, T1, T2, T3, T4, T5, T6, T7> : MultitionaryBase<TKey, Kuple<TKey, T1, T2, T3, T4, T5, T6, T7>>
    {

    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    public interface IKuple
    {
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IKuple<TKey> : IKuple
    {
        /// <summary>
        /// 
        /// </summary>
        TKey Key { get; set; }
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    
    public class Kuple<TKey, T1, T2, T3, T4, T5, T6, T7> : IKuple<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T2 Value2 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T3 Value3 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T4 Value4 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T5 Value5 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T6 Value6 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T7 Value7 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="value5"></param>
        /// <param name="value6"></param>
        /// <param name="value7"></param>
        public Kuple(TKey key, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
        {
            this.Key = key;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
            this.Value4 = value4;
            this.Value5 = value5;
            this.Value6 = value6;
            this.Value7 = value7;
        }
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    
    public class Kuple<TKey, T1, T2, T3, T4, T5, T6> : IKuple<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T2 Value2 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T3 Value3 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T4 Value4 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T5 Value5 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T6 Value6 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="value5"></param>
        /// <param name="value6"></param>
        public Kuple(TKey key, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
        {
            this.Key = key;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
            this.Value4 = value4;
            this.Value5 = value5;
            this.Value6 = value6;
        }
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    
    public class Kuple<TKey, T1, T2, T3, T4, T5> : IKuple<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T2 Value2 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T3 Value3 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T4 Value4 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T5 Value5 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        /// <param name="value5"></param>
        public Kuple(TKey key, T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            this.Key = key;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
            this.Value4 = value4;
            this.Value5 = value5;
        }
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    
    public class Kuple<TKey, T1, T2, T3, T4> : IKuple<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T2 Value2 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T3 Value3 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T4 Value4 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="value4"></param>
        public Kuple(TKey key, T1 value1, T2 value2, T3 value3, T4 value4)
        {
            this.Key = key;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
            this.Value4 = value4;
        }
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    
    public class Kuple<TKey, T1, T2, T3> : IKuple<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T2 Value2 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T3 Value3 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        public Kuple(TKey key, T1 value1, T2 value2, T3 value3)
        {
            this.Key = key;
            this.Value1 = value1;
            this.Value2 = value2;
            this.Value3 = value3;
        }
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    
    public class Kuple<TKey, T1, T2> : IKuple<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public T2 Value2 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public Kuple(TKey key, T1 value1, T2 value2)
        {
            this.Key = key;
            this.Value1 = value1;
            this.Value2 = value2;
        }
    }
    /// <summary>
    /// Interface to represent multivalue data structures identified by a key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="T1"></typeparam>
    
    public class Kuple<TKey, T1> : IKuple<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T1 Value1 { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value1"></param>
        public Kuple(TKey key, T1 value1)
        {
            this.Key = key;
            this.Value1 = value1;
        }
    }
}

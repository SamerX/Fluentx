﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Fluentx
{
    /// <summary>
    /// Set of very useful extension methods for hour by hour use in .NET code.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Extension method to perform For Each operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this != null)
            {
                foreach (var item in @this)
                {
                    action(item);
                }
            }
        }
        /// <summary>
        /// Performs a foreach loop on the specified list by excuting action for each item in the Enumerable providing the current index of the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            if (@this != null)
            {
                int index = 0;
                foreach (var item in @this)
                {
                    action(item, index);
                    index += 1;
                }
            }
        }
        /// <summary>
        /// (Synonym to ForEach) Extension method to perform For Each operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void ForEvery<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this != null)
            {
                foreach (var item in @this)
                {
                    action(item);
                }
            }
        }
        /// <summary>
        /// (Synonym to ForEach) Performs a foreach loop on the specified list by excuting action for each item in the Enumerable providing the current index of the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void ForEvery<T>(this IEnumerable<T> @this, Action<T, int> action)
        {
            if (@this != null)
            {
                int index = 0;
                foreach (var item in @this)
                {
                    action(item, index);
                    index += 1;
                }
            }
        }
        /// <summary>
        /// Returns whether the specified source doesn't contain the specified value or not.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool NotContains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return !source.Contains(value);
        }
        /// <summary>
        /// Returns whether the specified source doesn't contain the specified value or not.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            return !source.Contains(value, comparer);
        }
        /// <summary>
        /// Extension method to perform random return of an item within the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T Random<T>(this IEnumerable<T> @this)
        {
            if (@this != null)
            {
                int index = new Random().Next(0, @this.Count());
                return @this.ElementAt(index);
            }
            return default(T);
        }
        
        /// <summary>
        /// Extension method to evaluate if object is null.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T @this)
        {
            return @this == null;
        }
        /// <summary>
        /// Extension method to evaluate if object is not null
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T @this)
        {
            return @this != null;
        }
        /// <summary>
        /// Extension method to evaluate if the specified object exists within the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In<T>(this T @this, params T[] list)
        {
            //if (null == @this)
            //throw new ArgumentNullException("instance is null, can't check against null.");
            return list.Contains(@this);
        }
        /// <summary>
        /// Extension method to evaluate if the specified object doest not exists within the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool NotIn<T>(this T @this, params T[] list)
        {
            return !list.Contains(@this);
        }
        /// <summary>
        /// Extension method that returns whether the specified Enumerable is null or empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return (@this == null || @this.Count() == 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
        {
            return IsNullOrEmpty(@this).Not();
        }
        /// <summary>
        /// Extension method that performs the action if the value is true.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static bool IfTrue(this bool @this, Action action)
        {
            if (@this)
                action();
            return @this;
        }
        /// <summary>
        /// Returns the specified expression value when the value is true.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static TResult IfTrue<TResult>(this bool @this, Func<TResult> exp)
        {
            return @this ? exp() : default(TResult);
        }
        /// <summary>
        /// Returns the specified expression value when the value is true.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TResult WhenTrue<TResult>(this bool @this, TResult content)
        {
            return @this ? content : default(TResult);
        }
        /// <summary>
        /// Returns the specified expression value when the value is false.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static TResult WhenFalse<TResult>(this bool @this, Func<TResult> exp)
        {
            return !@this ? exp() : default(TResult);
        }
        /// <summary>
        /// Returns the specified expression value when the value is false.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static TResult WhenFalse<TResult>(this bool @this, TResult content)
        {
            return !@this ? content : default(TResult);
        }
        /// <summary>
        /// Extension method that performs the action if the value is false. Returns the same boolean value.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static bool IfFalse(this bool @this, Action action)
        {
            if (!@this)
                action();
            return @this;
        }
        /// <summary>
        /// Extension method that performs a boolean evaluation if @this is of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool Is<T>(this T @this)
        {
            return @this is T;
        }
        /// <summary>
        /// Extension method that performs a safe cast for @this as T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T As<T>(this T @this) where T : class
        {
            return @this as T;
        }
        /// <summary>
        /// Performs a lock operation (using a private object) on the specified action with @this as the parameter for the action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static T Lock<T>(this T @this, Action<T> action)
        {
            object lockObject = new object();
            lock (lockObject)
            {
                action(@this);
            }
            return @this;
        }
        /// <summary>
        /// Returns if a @this in between the specified range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static bool Between<T>(this T @this, T lower, T upper) where T : IComparable<T>
        {
            return @this.CompareTo(lower) >= 0 && @this.CompareTo(upper) < 0;
        }
        /// <summary>
        /// Parses a string to enum and throughs exceptions as if it fails.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignorecase"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(this string value, bool ignorecase = false) where T : struct, IConvertible
        {
            if (value == null)
            {
                throw new ArgumentNullException("Enum parsing failed. value is null");
            }

            value = value.Trim();

            if (value.Length == 0)
            {
                throw new ArgumentException("Enum parsing failed. value is empty", "value");
            }

            Type t = typeof(T);

            if (!t.IsEnum)
            {
                throw new ArgumentException("Enum parsing failed. Provided Type (" + t.Name + ") is not an enum", "T");
            }

            return (T)Enum.Parse(t, value, ignorecase);
        }
        /// <summary>
        /// Tries to Parse a string as an enum, if failed it returns the default value of the provided Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignorecase"></param>
        /// <returns></returns>
        public static T TryParseEnum<T>(this string value, bool ignorecase = false) where T : struct, IConvertible
        {
            if (value == null)
            {
                return default(T);
            }

            value = value.Trim();

            if (value.Length == 0)
            {
                return default(T);
            }

            Type t = typeof(T);

            if (!t.IsEnum)
            {
                return default(T);
            }

            return (T)Enum.Parse(t, value, ignorecase);
        }
        /// <summary>
        /// Simple update of instance memebers using lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void Set<T>(this T @this, Action<T> action) { action(@this); }
        /// <summary>
        /// Safely tries to evaluate the specified expression path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TResult Safe<T, TResult>(this T @this, Func<T, TResult> action)
        {
            if (@this == null)
            {
                return default(TResult);
            }
            else
            {
                try
                {
                    return action(@this);
                }
                catch
                {
                    return default(TResult);
                }
            }
        }
        /// <summary>
        /// Returns a comma separated string of the specified enumerable
        /// </summary>
        /// <param name="list"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToCSV<T>(this IEnumerable<T> list, string separator = ",")
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }
            return string.Join(separator, list);
        }
        /// <summary>
        /// Returns the min element in the IEnumerable according to the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T MinBy<T, TMember>(this IEnumerable<T> source, Func<T, TMember> predicate)
        {
            return MinBy(source, predicate, Comparer<TMember>.Default);
        }
        /// <summary>
        /// Returns the min element in the IEnumerable according to the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static T MinBy<T, TMember>(this IEnumerable<T> source, Func<T, TMember> predicate, IComparer<TMember> comparer)
        {
            var min = source.FirstOrDefault();

            var minValue = predicate(min);
            foreach (var item in source)
            {
                if (comparer.Compare(predicate(item), minValue) < 0)
                {
                    min = item;
                    minValue = predicate(min);
                }
            }
            return min;
        }
        /// <summary>
        /// Returns the max element in the IEnumerable according to the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static T MaxBy<T, TMember>(this IEnumerable<T> source, Func<T, TMember> predicate)
        {
            return MaxBy(source, predicate, Comparer<TMember>.Default);
        }
        /// <summary>
        /// Returns the max element in the IEnumerable according to the predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static T MaxBy<T, TMember>(this IEnumerable<T> source, Func<T, TMember> predicate, IComparer<TMember> comparer)
        {
            var max = source.FirstOrDefault();

            var maxValue = predicate(max);
            foreach (var item in source)
            {
                if (comparer.Compare(predicate(item), maxValue) > 0)
                {
                    max = item;
                    maxValue = predicate(max);
                }
            }
            return max;
        }
        /// <summary>
        /// Shuffles the specified enumeration randomly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
        {
            var dice = new Random();
            var buffer = list.ToList();

            for (int i = 0; i < buffer.Count; i++)
            {
                int dicePick = dice.Next(i, buffer.Count);
                yield return buffer[dicePick];
                buffer[dicePick] = buffer[i];
            }
        }
        /// <summary>
        /// K stands for Kilo. The value mutliplied by 1024. e.g. 3.K()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int K(this int value)
        {
            return value * 1024;
        }
        /// <summary>
        /// M stands for Mega. The value mutliplied by 1024 * 1024. e.g. 4.M()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int M(this int value)
        {
            return value * 1024 * 1024;
        }
        /// <summary>
        /// Returns an empty Enumerable if the specified enumerable is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> @this)
        {
            return @this ?? Enumerable.Empty<T>();
        }
        /// <summary>
        /// Tries to return the value of the specifed expression without checking for nullability.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="this"></param>
        /// <param name="exp"></param>
        /// <param name="elseValue"></param>
        /// <returns></returns>
        public static TReturn NullOr<T, TReturn>(this T @this, Func<T, TReturn> exp, TReturn elseValue = default(TReturn)) where T : class
        {
            return @this != null ? exp(@this) : elseValue;
        }
        /// <summary>
        /// Converts all elements of the specified enum to a concatenated string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToString<T>(this IEnumerable<T> collection, string separator = " ")
        {
            return ToString(collection, t => t.ToString(), separator);
        }
        /// <summary>
        /// Converts all elements of the specified enum to a concatenated string using the specifed exp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="exp"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToString<T>(this IEnumerable<T> collection, Func<T, string> exp, string separator = " ")
        {
            StringBuilder sBuilder = new StringBuilder();
            foreach (var item in collection)
            {
                sBuilder.Append(exp(item));
                sBuilder.Append(separator);
            }
            return sBuilder.ToString(0, Math.Max(0, sBuilder.Length - separator.Length));
        }
    }
}
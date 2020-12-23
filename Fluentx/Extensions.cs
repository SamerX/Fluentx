﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Fluentx
{
    /// <summary>
    /// Set of very useful extension methods for hour by hour use in .NET code.
    /// </summary>
    public static partial class Extensions
    {
        private static readonly Random random = new Random();
        /// <summary>
        /// Extension method to perform For Each operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this != null)
            {
                foreach (var item in @this)
                {
                    action(item);
                }
            }
            return @this;
        }
        /// <summary>
        /// Performs a foreach loop on the specified list by excuting action for each item in the Enumerable providing the current index of the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Action<T, int> action)
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
            return @this;
        }
        /// <summary>
        /// (Synonym to ForEach) Extension method to perform For Each operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static IEnumerable<T> ForEvery<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this != null)
            {
                foreach (var item in @this)
                {
                    action(item);
                }
            }
            return @this;
        }
        /// <summary>
        /// (Synonym to ForEach) Performs a foreach loop on the specified list by excuting action for each item in the Enumerable providing the current index of the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static IEnumerable<T> ForEvery<T>(this IEnumerable<T> @this, Action<T, int> action)
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
            return @this;
        }

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
                int index = random.Next(0, @this.Count());
                return @this.ElementAt(index);
            }
            return default(T);
        }
#if NETSTANDARD2_1
        /// <summary>
        /// Return a random value from the specified range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int Random(this Range @this)
        {

            return random.Next(@this.Start.Value, @this.End.Value);
        }
#endif
        /// <summary>
        /// Returns a random sequential range from the specified Enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<T> RandomRange<T>(this IEnumerable<T> @this)
        {
            if (@this != null)
            {
                int i1 = random.Next(0, @this.Count());
                int i2 = random.Next(0, @this.Count());
                return @this.Where((x, index) => { return index.BetweenRegardlessIncludeEdges(i1, i2); });
            }
            return null;
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
        /// Returns wether the Enumerable is not null or empty
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
        public static bool Is<T>(this object @this) => @this is T;

        /// <summary>
        /// Extension method that performs a safe cast for @this as T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T As<T>(this object @this) => (T)@this; //where T : class => @this as T;


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
        /// Returns if a @this in between the specified range without including the edges.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static bool Between<T>(this T @this, T lower, T upper) where T : IComparable<T>
        {
            return @this.CompareTo(lower) > 0 && @this.CompareTo(upper) < 0;
        }
        /// <summary>
        /// Returns if a @this in between the specified range including the edges.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static bool BetweenIncludeEdges<T>(this T @this, T lower, T upper) where T : IComparable<T>
        {
            return @this.CompareTo(lower) >= 0 && @this.CompareTo(upper) <= 0;
        }
        /// <summary>
        /// Returns if a @this in between the specified range regardless of the order without including the edges.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="endpoint1"></param>
        /// <param name="endpoint2"></param>
        /// <returns></returns>
        public static bool BetweenRegardless<T>(this T @this, T endpoint1, T endpoint2) where T : IComparable<T>
        {
            return (@this.CompareTo(endpoint1) > 0 && @this.CompareTo(endpoint2) < 0) || (@this.CompareTo(endpoint2) > 0 && @this.CompareTo(endpoint1) < 0);
        }
        /// <summary>
        /// Returns if a @this in between the specified range regardless of the order including the edges.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="endpoint1"></param>
        /// <param name="endpoint2"></param>
        /// <returns></returns>
        public static bool BetweenRegardlessIncludeEdges<T>(this T @this, T endpoint1, T endpoint2) where T : IComparable<T>
        {
            return (@this.CompareTo(endpoint1) >= 0 && @this.CompareTo(endpoint2) <= 0) || (@this.CompareTo(endpoint2) >= 0 && @this.CompareTo(endpoint1) <= 0);
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

            if (!t.GetTypeInfo().IsEnum)
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

            if (!t.GetTypeInfo().IsEnum)
            {
                return default(T);
            }

            return (T)Enum.Parse(t, value, ignorecase);
        }
        /// <summary>
        /// Converts an integer into a Hex string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToHex(this int @this)
        {
            return @this.ToString("X");
        }
        /// <summary>
        /// Parses a Hex string to integer
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int ParseHex(this string @this)
        {
            return int.Parse(@this, System.Globalization.NumberStyles.HexNumber);
        }
        /// <summary>
        /// Simple update of instance memebers using lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static T Set<T>(this T @this, Action<T> action) { action(@this); return @this; }
        /// <summary>
        /// Get property value for the specified object
        /// </summary>
        /// <typeparam name="TPropertyType"></typeparam>
        /// <param name="this"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static TPropertyType PropertyValue<TPropertyType>(this object @this, string propertyName)
        {
            return (TPropertyType)@this.GetType().GetTypeInfo().GetProperty(propertyName).GetValue(@this, null);
        }
        /// <summary>
        /// Get property value for the specified object
        /// </summary>
        /// <param name="this"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object PropertyValue(this object @this, string propertyName)
        {
            return @this.GetType().GetTypeInfo().GetProperty(propertyName).GetValue(@this, null);
        }
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
                var itemValue = predicate(item);
                if (comparer.Compare(itemValue, minValue) <= 0)
                {
                    min = item;
                    minValue = itemValue;
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
                var itemValue = predicate(item);
                if (comparer.Compare(itemValue, maxValue) >= 0)
                {
                    max = item;
                    maxValue = itemValue;
                }
            }
            return max;
        }


        /// <summary>
        /// Shuffles the specified list items randomly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            //int iterations = random.Next(list.Count / 2, list.Count + 1);

            for (int i = 0; i < list.Count; i++)
            {
                int dicePick = random.Next(list.Count);
                T temp = list[dicePick];
                list[dicePick] = list[i];
                list[i] = temp;
            }
        }

        /// <summary>
        /// <value></value>Hundred
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //public static int Hundred(this int value)
        //{
        //    return value * 100;
        //}
        public static int Hundred<T>(this int value)
        {
            return value * 100;
        }
        /// <summary>
        /// <value></value>Thousand
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Thousand(this int value)
        {
            return value * 1000;
        }
        /// <summary>
        /// <value></value>Million
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Million(this int value)
        {
            return value * 1_000_000;
        }
        /// <summary>
        /// <value></value>Billion
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Billion(this int value)
        {
            return value * 1_000_000_000;
        }
        /// <summary>
        /// KB stands for Kilo Byte. The value mutliplied by 1024. e.g. 3.KB()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int KB(this int value)
        {
            return value * 1024;
        }
        /// <summary>
        /// MB stands for Mega Byte. The value mutliplied by 1024 * 1024. e.g. 4.MB()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int MB(this int value)
        {
            return value * 1024 * 1024;
        }
        /// <summary>
        /// GB stands for Giga Byte. The value mutliplied by 1024 * 1024 * 1024. e.g. 4.GB()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GB(this int value)
        {
            return value * 1024 * 1024 * 1024;
        }

        /// <summary>
        /// TB stands for Tera Byte. The value mutliplied by 1024 * 1024 * 1024 * 1024. e.g. 4.TB()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int TB(this int value)
        {
            return value * 1024 * 1024 * 1024 * 1024;
        }
        /// <summary>
        /// PB stands for Peta Byte. The value mutliplied by 1024 * 1024 * 1024 * 1024. e.g. 4.PB()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int PB(this int value)
        {
            return value * 1024 * 1024 * 1024 * 1024 * 1024;
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
        /// Fahrenheit to Celcius
        /// </summary>
        /// <param name="fahrenheit"></param>
        /// <returns></returns>
        public static double ToCelcius(this double fahrenheit)
        {
            return (5.0 / 9.0) * (fahrenheit - 32);
        }
        /// <summary>
        /// Celcius to Fahrenheit 
        /// </summary>
        /// <param name="celius"></param>
        /// <returns></returns>
        public static double ToFahrenheit(this double celius)
        {
            return ((9.0 / 5.0) * celius) + 32;
        }
        /// <summary>
        /// Fahrenheit to Celcius
        /// </summary>
        /// <param name="fahrenheit"></param>
        /// <returns></returns>
        public static double ToCelcius(this int fahrenheit)
        {
            return (5.0 / 9.0) * (fahrenheit - 32);
        }
        /// <summary>
        /// Celcius to Fahrenheit
        /// </summary>
        /// <param name="celius"></param>
        /// <returns></returns>
        public static double ToFahrenheit(this int celius)
        {
            return ((9.0 / 5.0) * celius) + 32;
        }
        /// <summary>
        /// KG to LBS
        /// </summary>
        /// <param name="kg"></param>
        /// <returns></returns>
        public static double ToLBS(this double kg)
        {
            return (kg * 2.20462262185);
        }
        /// <summary>
        /// LBS to KG
        /// </summary>
        /// <param name="lbs"></param>
        /// <returns></returns>
        public static double ToKG(this double lbs)
        {
            return (lbs / 2.20462262185);
        }
        /// <summary>
        /// KG to LBS
        /// </summary>
        /// <param name="kg"></param>
        /// <returns></returns>
        public static double ToLBS(this int kg)
        {
            return (kg * 2.20462262185);
        }
        /// <summary>
        /// LBS to KG
        /// </summary>
        /// <param name="lbs"></param>
        /// <returns></returns>
        public static double ToKG(this int lbs)
        {
            return (lbs / 2.20462262185);
        }
        /// <summary>
        /// Converts meters to feets.
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public static double ToFeet(this int meter)
        {
            return (meter * 3.2808398950131);
        }
        /// <summary>
        /// LBS to KG
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double ToMeter(this int feet)
        {
            return (feet / 3.2808398950131);
        }
        /// <summary>
        /// Meters to Feet
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public static double ToFeet(this double meter)
        {
            return (meter * 3.2808398950131);
        }
        /// <summary>
        /// LBS to KG
        /// </summary>
        /// <param name="feet"></param>
        /// <returns></returns>
        public static double ToMeter(this double feet)
        {
            return (feet / 3.2808398950131);
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
        /// Converts all elements of the specified enumerable to a concatenated string.
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
        /// Converts all elements of the specified enumerable to a concatenated string using the specifed exp.
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

        /// <summary>
        /// Invokes the specified method (Not Generic Method) on the target object dynamically with its required parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="methodName"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static object InvokeMethod<T>(this T @this, string methodName, params object[] @params)
        {
            Guard.Against<ArgumentNullException>(@this == null, "InvokeMethod failed as target object is null");
            var method = @this.GetType().GetTypeInfo().GetMethods().Where(x => x.Name == methodName).FirstOrDefault();
            var data = method.Invoke(@this, @params);
            return data;
        }
#if !NETSTANDARD1_5 && !NETSTANDARD1_6
        /// <summary>
        /// Invokes the specified method (Not Generic Method) ASYNCROUNOUSLY (if its an async method) on the target object dynamically with its required parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="methodName"></param>
        /// <param name="genericParams"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static async Task<object> InvokeMethodAsync<T>(this T @this, string methodName, params object[] @params)
        {
            Guard.Against<ArgumentNullException>(@this == null, "InvokeMethodAsync failed as target object is null");
            var task = (Task)@this.InvokeMethod(methodName, @params);
            await task.ConfigureAwait(false);
            var result = task.GetType().GetProperty("Result");
            return result.GetValue(task);
        }
#endif
        /// <summary>
        /// Invokes the specified method on the target object supplying the generic parameters dynamically with its required parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="methodName"></param>
        /// <param name="genericParams"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static object InvokeGenericMethod<T>(this T @this, string methodName, Type[] genericParams, params object[] @params)
        {
            Guard.Against<ArgumentNullException>(@this == null, "InvokeGenericMethod failed as target object is null");
            var method = @this.GetType().GetTypeInfo().GetMethods().Where(x => x.Name == methodName && x.IsGenericMethod && x.GetGenericArguments().Length == genericParams.Length).FirstOrDefault().MakeGenericMethod(genericParams);
            var data = method.Invoke(@this, @params);
            return data;
        }
        /// <summary>
        /// Invokes the specified method on the target object supplying the generic parameter dynamically with its required parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="methodName"></param>
        /// <param name="genericParam"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static object InvokeGenericMethod<T>(this T @this, string methodName, Type genericParam, params object[] @params)
        {
            return InvokeGenericMethod<T>(@this, methodName, genericParam.WrapAsArray(), @params);
        }

#if !NETSTANDARD1_5 && !NETSTANDARD1_6
        /// <summary>
        /// Invokes the specified method ASYNCROUNOUSLY (if its an async method) on the target object supplying the generic parameters dynamically with its required parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="methodName"></param>
        /// <param name="genericParams"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static async Task<object> InvokeGenericMethodAsync<T>(this T @this, string methodName, Type[] genericParams, params object[] @params)
        {
            Guard.Against<ArgumentNullException>(@this == null, "InvokeGenericMethodAsync failed as target object is null");
            var task = (Task)@this.InvokeGenericMethod(methodName, genericParams, @params);
            await task.ConfigureAwait(false);
            var result = task.GetType().GetProperty("Result");
            return result.GetValue(task);
        }
        /// <summary>
        /// Invokes the specified method ASYNCROUNOUSLY (if its an async method) on the target object supplying the generic parameter dynamically with its required parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="methodName"></param>
        /// <param name="genericParam"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public static async Task<object> InvokeGenericMethodAsync<T>(this T @this, string methodName, Type genericParam, params object[] @params)
        {
            return await InvokeGenericMethodAsync<T>(@this, methodName, genericParam.WrapAsArray(), @params);
        }
#endif
        /// <summary>
        /// Returns whether @this type Implements the specified interface type, this is only for interfaces not for classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool Implements<T>(this Type @this)
        {
            return @this.Implements(typeof(T));
        }
        /// <summary>
        /// Returns whether @this type Implements the specified interface type, this is only for interfaces not for classes.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static bool Implements(this Type @this, Type interfaceType)
        {
            return @this.GetTypeInfo().GetInterfaces().Any(x => x == interfaceType);
        }
        /// <summary>
        /// Returns whether @this type DOES NOT Implement the specified interface type, this is only for interfaces not for classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool NotImplement<T>(this Type @this)
        {
            return @this.NotImplement(typeof(T));
        }
        /// <summary>
        /// Returns whether @this type Implements the specified interface type, this is only for interfaces not for classes.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static bool NotImplement(this Type @this, Type interfaceType)
        {
            return !@this.Implements(interfaceType);
        }
        /// <summary>
        /// Returns whether @this type is a sub class of the specified class type, this is only for classes not for classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsSubclass<T>(this Type @this)
        {
            return @this.IsSubclass(typeof(T));
        }
        /// <summary>
        /// Returns whether @this type is a sub class of the specified class type, this is only for classes not for classes.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static bool IsSubclass(this Type @this, Type classType)
        {
            if (@this == classType || @this.GetTypeInfo().IsInterface || classType.GetTypeInfo().IsInterface) return false;

            while (@this != null && @this != typeof(object))
            {
                var current = @this.GetTypeInfo().IsGenericType ? @this.GetGenericTypeDefinition() : @this;
                if (classType == current || (classType.GetTypeInfo().IsGenericType && current == classType.GetGenericTypeDefinition() && Enumerable.SequenceEqual(@this.GetTypeInfo().GetGenericArguments(), classType.GetTypeInfo().GetGenericArguments())))
                {
                    return true;
                }
                @this = @this.GetTypeInfo().BaseType;
            }
            return false;
        }
        /// <summary>
        /// Returns whether @this type IS NOT a sub class of the specified class type, this is only for classes not for classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotSubclass<T>(this Type @this)
        {
            return !@this.IsSubclass(typeof(T));
        }
        /// <summary>
        /// Returns whether @this type IS NOT a sub class of the specified class type, this is only for classes not for classes.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        public static bool IsNotSubclass(this Type @this, Type classType)
        {
            return !@this.IsSubclass(classType);
        }
        /// <summary>
        /// Returns whether @this type inherits the specified type, this works for both interfaces and classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool Inherits<T>(this Type @this)
        {
            return @this.Inherits(typeof(T));
        }
        /// <summary>
        /// Returns whether @this type inherits the specified type, this works for both interfaces and classes.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Inherits(this Type @this, Type type)
        {
            return @this.Implements(type) || @this.IsSubclass(type);
        }
        /// <summary>
        /// Returns whether @this type DOES NOT inherit the specified type, this works for both interfaces and classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool NotInherit<T>(this Type @this)
        {
            return !@this.Inherits(typeof(T));
        }
        /// <summary>
        /// Returns whether @this type DOES NOT inherit the specified type, this works for both interfaces and classes.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool NotInherit(this Type @this, Type type)
        {
            return @this.NotImplement(type) && @this.IsNotSubclass(type);
        }
        /// <summary>
        /// Returns whether the specified types belongs to one of the simple types below:
        /// Int16, Int32, Int64, UInt16, UInt32, UInt64, DateTime, Boolean, Byte, SByte, Char, 
        /// Double, Single, Decimal, IntPtr, UIntPtr, String, Guid And thier Nullable counterparts.
        /// Please note that the difinition of simple types is completly different from primitive types.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type @this)
        {
            var simpleTypes = new Type[]
            {
                typeof(Int16),typeof(Int32),typeof(Int64),typeof(UInt16),typeof(UInt32),typeof(UInt64),
                typeof(DateTime), typeof(Boolean), typeof(Byte), typeof(SByte), typeof(Char), typeof(Double),
                typeof(Single), typeof(Decimal), typeof(IntPtr), typeof(UIntPtr), typeof(String), typeof(Guid)
            };

            return simpleTypes.Contains(@this) || @this.IsSubclass(typeof(Nullable<>)) && simpleTypes.Contains(@this.GenericTypeArguments.FirstOrDefault());

        }
        /// <summary>
        /// Returns if the specified value within the range returns the range, if its lower then returns the min, if its higher then returns the max.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            return (value.CompareTo(min) < 0) ? min : (value.CompareTo(max) > 0) ? max : value;
        }
        /// <summary>
        /// Return the integer value for Hour enum
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static int Value(this Hour hour)
        {
            return (int)hour;
        }

        /// <summary>
        /// Return the integer value for Month enum
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int Value(this Month month)
        {
            return (int)month;
        }
        /// <summary>
        /// Return an enumerable of enumerables from the specfied Enumerable, it actually divides an enumerable to several enumerables based on size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size)
        {
            while (source.Any())
            {
                yield return source.Take(size);
                source = source.Skip(size);
            }
        }
        /// <summary>
        /// Same as where but only added when the condition is met
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, bool condition)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
        /// <summary>
        /// Returns an Enumerable of the specified size of an Enumerable. convert the list to bach of list using the specified size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> ToBatches<T>(this IEnumerable<T> list, int batchSize)
        {
            if (list.IsNull())
            {
                return null;
            }
            else if (list.IsNullOrEmpty())
            {
                return list.WrapAsEnumerable();
            }
            if (batchSize <= 0)
            {
                throw new ArgumentException($"Invalid BatchSize value {batchSize}");
            }

            var batched = list?
                .Select((Value, Index) => new { Value, Index })
                .GroupBy(p => p.Index / batchSize)
                .Select(g => g.Select(p => p.Value));
            return batched;
        }
        /// <summary>
        /// Wraps the specified instance in an IEnumerable<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IEnumerable<T> WrapAsEnumerable<T>(this T instance)
        {
            return new[] { instance };
        }
        /// <summary>
        /// Wraps the specified instance in an Array<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T[] WrapAsArray<T>(this T instance)
        {
            return new[] { instance };
        }

        /// <summary>
        /// Wraps the specified instance in an IList<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IList<T> WrapAsList<T>(this T instance)
        {
            return new List<T> { instance };
        }
#if !NETSTANDARD1_5 && !NETSTANDARD1_6
        /// <summary>
        /// Same as where but only added when the condition is met
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, bool condition)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
#endif

    }
}

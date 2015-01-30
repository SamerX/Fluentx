using System;
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
        /// Extension method that performs the operation string.Format 
        /// </summary>
        /// <param name="this"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string @this, params object[] args)
        {
            return string.Format(@this, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T To<T>(this IConvertible obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt32(this string @this, int defaultValue = default(int))
        {
            Int32 x;
            if (Int32.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string @this, uint defaultValue = default(uint))
        {
            UInt32 x;
            if (UInt32.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string @this, int defaultValue = default(int))
        {
            Int32 x;
            if (Int32.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt(this string @this, uint defaultValue = default(uint))
        {
            UInt32 x;
            if (UInt32.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this string @this, long defaultValue = default(long))
        {
            long x;
            if (long.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToULong(this string @this, ulong defaultValue = default(ulong))
        {
            ulong x;
            if (ulong.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(this string @this, short defaultValue = default(short))
        {
            Int16 x;
            if (Int16.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this string @this, ushort defaultValue = default(ushort))
        {
            UInt16 x;
            if (UInt16.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(this string @this, long defaultValue = default(long))
        {
            Int64 x;
            if (Int64.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string @this, ulong defaultValue = default(ulong))
        {
            UInt64 x;
            if (UInt64.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string @this, double defaultValue = default(double))
        {
            double x;
            if (Double.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string @this, float defaultValue = default(float))
        {
            float x;
            if (Single.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string @this, decimal defaultValue = default(decimal))
        {
            decimal x;
            if (decimal.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this string @this, byte defaultValue = default(byte))
        {
            byte x;
            if (Byte.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string @this, sbyte defaultValue = default(sbyte))
        {
            sbyte x;
            if (SByte.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string @this, bool defaultValue = default(bool))
        {
            bool x;
            if (bool.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string @this, DateTime defaultValue = default(DateTime))
        {
            DateTime x;
            if (DateTime.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string @this, Guid defaultValue = default(Guid))
        {
            Guid x;
            if (Guid.TryParse(@this, out x))
                return x;
            else
                return defaultValue;
        }
        /// <summary>
        /// Extension method to compare two strings for equality ignoring character case. (Note: uses Equals(string, StringComparison.OrdinalIgnoreCase)).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="compareOperand"></param>
        /// <returns></returns>
        public static bool IgnoreCaseEqual(this string @this, string compareOperand)
        {
            return @this.Equals(compareOperand, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// Returns a string that holds the words splitted by space.e.g. thisIsGood => this Is Good
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Wordify(this string @this)
        {
            // if the word is all upper, just return it
            if (!Regex.IsMatch(@this, "[a-z]"))
                return @this;

            return string.Join(" ", Regex.Split(@this, @"(?<!^)(?=[A-Z])"));
        }
        /// <summary>
        /// Counts the words within the specified strings, two algorithms can be used, Regex is the default one used: more accurate but slower. Loop method is much much faster but less accurate.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static int WordCount(this string @this, WordCountAlgorithm algorithm = WordCountAlgorithm.Regex)
        {
            if (algorithm == WordCountAlgorithm.Regex)
            {
                MatchCollection collection = Regex.Matches(@this, @"[\S]+");
                return collection.Count;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < @this.Length; i++)
                {
                    if (char.IsWhiteSpace(@this[i - 1]))
                    {
                        if (char.IsLetterOrDigit(@this[i]) || char.IsPunctuation(@this[i]))
                        {
                            count++;
                        }
                    }
                }

                if (@this.Length > 2)
                {
                    count++;
                }
                return count;
            }
        }
        /// <summary>
        /// Reverses the specifed string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Reverse(this string @this)
        {
            if (@this == null || @this.Length < 2)
            {
                return @this;
            }

            int length = @this.Length;
            int loop = (length >> 1) + 1;
            int j;
            char[] charArray = new char[length];
            for (int i = 0; i < loop; i++)
            {
                j = length - i - 1;
                charArray[i] = @this[j];
                charArray[j] = @this[i];
            }
            return new string(charArray);
        }
        /// <summary>
        /// Returns a new string in which the last occurrence of a specified string
        /// in this instance are replaced with another specified string.  
        /// </summary>
        /// <param name="this"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceLast(this string @this, string oldValue, string newValue)
        {
            int index = @this.LastIndexOf(oldValue);
            if (index > -1)
            {
                string newString = @this.Remove(index, oldValue.Length).Insert(index, newValue);
                return newString;
            }
            else
            {
                return @this;
            }
        }
        /// <summary>
        /// Counts the occurences of the specified string within a string
        /// </summary>
        /// <param name="this"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int CountOccurences(this string @this, string match)
        {
            if (!match.IsNullOrEmpty())
            {
                int count = (@this.Length - @this.Replace(match, "").Length) / match.Length;
                return count;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Returns a new string in which the last occurrence of a specified string
        /// in this instance are replaced with another specified string.  
        /// </summary>
        /// <param name="this"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string @this, string oldValue, string newValue)
        {
            int index = @this.IndexOf(oldValue);
            if (index > -1)
            {
                string newString = @this.Remove(index, oldValue.Length).Insert(index, newValue);
                return newString;
            }
            else
            {
                return @this;
            }
        }
        /// <summary>
        /// Enumeration to specify which algorithm to use when counting words for the WordCount extension method
        /// </summary>
        public enum WordCountAlgorithm
        {
            /// <summary>
            /// Uses regular expressions to count words, its not fast for long strings but its accurate in terms of comparing to Microsoft Word, deviation is 0.02%.
            /// Note: this benchmark is taken from http://www.dotnetperls.com
            /// </summary>
            Regex,
            /// <summary>
            /// Uses a simple loop to check characters and count words, this method is faster than regex, but less accurate.
            /// </summary>
            Loop,
        }
    }

}

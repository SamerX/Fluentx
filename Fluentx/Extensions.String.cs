using System;
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
        public static int ToInt32(this string @this, int defaultValue)
        {
            if (Int32.TryParse(@this, out int x))
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
        public static int? ToInt32(this string @this)
        {
            if (Int32.TryParse(@this, out int x))
                return x;
            else
                return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt32(this string @this, uint defaultValue)
        {
            if (UInt32.TryParse(@this, out uint x))
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
        public static uint? ToUInt32(this string @this)
        {
            if (UInt32.TryParse(@this, out uint x))
                return x;
            else
                return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string @this, int defaultValue)
        {
            if (Int32.TryParse(@this, out int x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int? ToInt(this string @this)
        {
            if (Int32.TryParse(@this, out int x))
                return x;
            return default(int?);
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUInt(this string @this, uint defaultValue)
        {
            if (UInt32.TryParse(@this, out uint x))
                return x;
            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this string @this, long defaultValue)
        {
            if (long.TryParse(@this, out long x))
                return x;
            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long? ToLong(this string @this)
        {
            if (long.TryParse(@this, out long x))
                return x;
            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToULong(this string @this, ulong defaultValue)
        {
            if (ulong.TryParse(@this, out ulong x))
                return x;
            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ulong? ToULong(this string @this)
        {
            if (ulong.TryParse(@this, out ulong x))
                return x;
            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToInt16(this string @this, short defaultValue)
        {
            if (Int16.TryParse(@this, out short x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static short? ToInt16(this string @this)
        {
            if (Int16.TryParse(@this, out short x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this string @this, ushort defaultValue)
        {
            if (UInt16.TryParse(@this, out ushort x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ushort? ToUInt16(this string @this)
        {
            if (UInt16.TryParse(@this, out ushort x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToInt64(this string @this, long defaultValue)
        {
            if (Int64.TryParse(@this, out long x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long? ToInt64(this string @this)
        {
            if (Int64.TryParse(@this, out long x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this string @this, ulong defaultValue)
        {
            if (UInt64.TryParse(@this, out ulong x))
                return x;

            return defaultValue;
        }
        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ulong? ToUInt64(this string @this)
        {
            if (UInt64.TryParse(@this, out ulong x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string @this, double defaultValue)
        {
            if (Double.TryParse(@this, out double x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double? ToDouble(this string @this)
        {
            if (Double.TryParse(@this, out double x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string @this, float defaultValue)
        {
            if (Single.TryParse(@this, out float x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static float? ToFloat(this string @this)
        {
            if (Single.TryParse(@this, out float x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string @this, decimal defaultValue)
        {
            if (decimal.TryParse(@this, out decimal x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string @this)
        {
            if (decimal.TryParse(@this, out decimal x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this string @this, byte defaultValue)
        {
            if (Byte.TryParse(@this, out byte x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte? ToByte(this string @this)
        {
            if (Byte.TryParse(@this, out byte x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ToSByte(this string @this, sbyte defaultValue)
        {
            if (SByte.TryParse(@this, out sbyte x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static sbyte? ToSByte(this string @this)
        {
            if (SByte.TryParse(@this, out sbyte x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string @this, bool defaultValue)
        {
            if (bool.TryParse(@this, out bool x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool? ToBool(this string @this)
        {
            if (bool.TryParse(@this, out bool x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string @this, DateTime defaultValue)
        {
            if (DateTime.TryParse(@this, out DateTime x))
                return x;

            return defaultValue;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string @this)
        {
            if (DateTime.TryParse(@this, out DateTime x))
                return x;

            return default;
        }

        /// <summary>
        /// Extension method that tries to parse the string, if parsing faild it returns the default value (specified default value or implicit default value).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string @this, Guid defaultValue)
        {
            if (Guid.TryParse(@this, out Guid x))
                return x;

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
        /// Returns a string that holds the words splitted by space (optional separator).e.g. thisIsGood => this Is Good.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Wordify(this string @this, string separator = " ")
        {
            // if the word is all upper, just return it
            if (!Regex.IsMatch(@this, "[a-z]"))
                return @this;

            return string.Join(separator, Regex.Split(@this, @"(?<!^)(?=[A-Z])"));
        }

        /// <summary>
        /// Makes the first letter of the specified string capital.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Capitalize(this string @this)
        {
            if (@this.IsNullOrEmpty())
            {
                return @this;
            }
            var characters = @this.ToCharArray();
            characters[0] = char.ToUpper(characters[0]);
            return new string(characters);
        }

        /// <summary>
        /// Makes the first letter of the specified string in lower case.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UnCapitalize(this string @this)
        {
            if (@this.IsNullOrEmpty())
            {
                return @this;
            }
            var characters = @this.ToCharArray();
            characters[0] = char.ToLower(characters[0]);
            return new string(characters);
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
        /// Encrypts the specified text with the specified key, decryption can be done using the same method on the ciphered data with the same key.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string XORCipher(this string @this, string key)
        {
            int dataLen = @this.Length;
            int keyLen = key.Length;
            char[] output = new char[dataLen];

            for (int i = 0; i < dataLen; ++i)
            {
                output[i] = (char)(@this[i] ^ key[i % keyLen]);
            }

            return new string(output);
        }

        /// <summary>
        /// AP is a hybrid rotative and additive hash function algorithm.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static uint APHash(this string value)
        {
            uint hash = 0xAAAAAAAA;
            uint i = 0;

            for (i = 0; i < value.Length; i++)
            {
                hash ^= ((i & 1) == 0) ? ((hash << 7) ^ ((byte)value[(int)i]) * (hash >> 3)) :
                                        (~((hash << 11) + (((byte)value[(int)i]) ^ (hash >> 5))));
            }
            return hash;
        }

        /// <summary>
        /// Encrypts a string using the Cesar algorithm.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CesarEncrypt(this string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += InternalCesarEncrypt(ch, key);

            return output;

            char InternalCesarEncrypt(char text, int _key)
            {
                if (!char.IsLetter(text))
                    return text;

                char offset = char.IsUpper(text) ? 'A' : 'a';
                return (char)((((text + _key) - offset) % 26) + offset);
            }
        }

        ///// <summary>
        ///// This method do a trim start and end for all string properties on the specified object.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="this"></param>
        ///// <param name="bindingFlags"></param>
        ///// <returns></returns>
        //public static T TrimStringProperties<T>(this T @this, BindingFlags bindingFlags)
        //{
        //    var stringMembers = typeof(T).GetTypeInfo().GetProperties(bindingFlags).Where(p => p.DeclaringType == typeof(string) && p.MemberType == MemberTypes.Property || p.MemberType == MemberTypes.Field);

        //    foreach (var stringProperty in stringMembers)
        //    {
        //        if (stringProperty.CanWrite)
        //        {
        //            string currentValue = (string)stringProperty.GetValue(@this, null);
        //            stringProperty.SetValue(@this, currentValue?.Trim(), null);
        //        }
        //    }
        //    return @this;
        //}
        /// <summary>
        /// Decrypts a string using cesar algorithm.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string CesarDecrypt(this string input, int key)
        {
            return CesarEncrypt(input, 26 - key);
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
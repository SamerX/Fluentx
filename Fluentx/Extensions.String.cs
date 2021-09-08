using System;
using System.Linq;
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
        /// Converts the specified object using Convert.Change type, if failed returns a default value.
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
        /// Returns with the specified string is numeric or not.
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string theValue)
        {
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, result: out _);
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
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string ReplaceLast(this string @this, string oldValue, string newValue, StringComparison comparison = StringComparison.Ordinal)
        {
            int index = @this.LastIndexOf(oldValue, comparison);
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
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static string ReplaceFirst(this string @this, string oldValue, string newValue, StringComparison comparison = StringComparison.Ordinal)
        {
            int index = @this.IndexOf(oldValue, comparison);
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

        /// <summary>
        /// Wraps the specified instance with the specified tex
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WrapWith(this string instance, object text)
        {
            return text + instance + text;
        }

        /// <summary>
        /// Wraps the specified instance with the specified tex
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public static string WrapWith(this string instance, object before, object after)
        {
            return before + instance + after;
        }

        /// <summary>
        /// Wraps the text with double quotations
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string DoubleQuote(this string instance)
        {
            return Fx.DoubleQuote + instance + Fx.DoubleQuote;
        }

        /// <summary>
        /// Wraps the text with single quotations
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string SingleQuote(this string instance)
        {
            return Fx.SingleQuote + instance + Fx.SingleQuote;
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
        /// Returns with the character is a vowel in european languages.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsVowel(this char ch)
        {
            return "aeiouyáéíóúýa̋e̋i̋őűàèìòùỳầềồḕṑǜừằȁȅȉȍȕăĕĭŏŭy̆ắằẳẵặḝȃȇȋȏȗǎěǐǒǔy̌a̧ȩə̧ɛ̧i̧ɨ̧o̧u̧âêîôûŷḙṷẩểổấếốẫễỗậệộäëïöüÿṳḯǘǚṏǟȫǖṻȧėıȯẏǡạẹịọụỵậȩ̇ǡȱảẻỉỏủỷơướứờừởửỡữợựāǣēīōūȳḗṓȭǭąęįǫųy̨åi̊ůḁǻą̊ãẽĩõũỹаэыуояеёюийⱥɇɨøɵꝋʉᵿɏөӫұɨαεηιοωυάέήίόώύὰὲὴὶὸὼὺἀἐἠἰὀὠὐἁἑἡἱὁὡὑᾶῆῖῶῦἆἦἶὦὖἇἧἷὧὗᾳῃῳᾷῇῷᾴῄῴᾲῂῲᾀᾐᾠᾁᾑᾡᾆᾖᾦᾇᾗᾧϊϋΐΰῒῢῗῧἅἕἥἵὅὥὕἄἔἤἴὄὤὔἂἒἢἲὂὢὒἃἓἣἳὃὣὓᾅᾕᾥᾄᾔᾤᾂᾒᾢᾃᾓᾣæɯɪʏʊøɘɤəɛœɜɞʌɔɐɶɑɒιυ"
                     .Contains("" + ch);
        }

        /// <summary>
        /// Performs like operation on the specified input using a pattern
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="CaseInsensitive"></param>
        /// <returns></returns>
        public static bool Like(this string input, string pattern, bool CaseInsensitive = true)
        {
            //Nothing matches a null mask or null input string
            if (pattern == null || input == null)
                return false;
            //Null strings are treated as empty and get checked against the mask.
            //If checking is case-insensitive we convert to uppercase to facilitate this.
            if (CaseInsensitive)
            {
                input = input.ToUpperInvariant();
                pattern = pattern.ToUpperInvariant();
            }
            //Keeps track of our position in the primary string - s.
            int j = 0;
            //Used to keep track of multi-character wildcards.
            bool matchanymulti = false;
            //Used to keep track of multiple possibility character masks.
            string multicharmask = null;
            bool inversemulticharmask = false;
            for (int i = 0; i < pattern.Length; i++)
            {
                //If this is the last character of the mask and its a % or * we are done
                if (i == pattern.Length - 1 && (pattern[i] == '%' || pattern[i] == '*'))
                    return true;
                //A direct character match allows us to proceed.
                var charcheck = true;
                //Backslash acts as an escape character.  If we encounter it, proceed
                //to the next character.
                if (pattern[i] == '\\')
                {
                    i++;
                    if (i == pattern.Length)
                        i--;
                }
                else
                {
                    //If this is a wildcard mask we flag it and proceed with the next character
                    //in the mask.
                    if (pattern[i] == '%' || pattern[i] == '*')
                    {
                        matchanymulti = true;
                        continue;
                    }
                    //If this is a single character wildcard advance one character.
                    if (pattern[i] == '_')
                    {
                        //If there is no character to advance we did not find a match.
                        if (j == input.Length)
                            return false;
                        j++;
                        continue;
                    }
                    if (pattern[i] == '[')
                    {
                        var endbracketidx = pattern.IndexOf(']', i);
                        //Get the characters to check for.
                        multicharmask = pattern.Substring(i + 1, endbracketidx - i - 1);
                        //Check for inversed masks
                        inversemulticharmask = multicharmask.StartsWith("^");
                        //Remove the inversed mask character
                        if (inversemulticharmask)
                            multicharmask = multicharmask.Remove(0, 1);
                        //Unescape \^ to ^
                        multicharmask = multicharmask.Replace("\\^", "^");

                        //Prevent direct character checking of the next mask character
                        //and advance to the next mask character.
                        charcheck = false;
                        i = endbracketidx;
                        //Detect and expand character ranges
                        if (multicharmask.Length == 3 && multicharmask[1] == '-')
                        {
                            var newmask = "";
                            var first = multicharmask[0];
                            var last = multicharmask[2];
                            if (last < first)
                            {
                                first = last;
                                last = multicharmask[0];
                            }
                            var c = first;
                            while (c <= last)
                            {
                                newmask += c;
                                c++;
                            }
                            multicharmask = newmask;
                        }
                        //If the mask is invalid we cannot find a mask for it.
                        if (endbracketidx == -1)
                            return false;
                    }
                }
                //Keep track of match finding for this character of the mask.
                var matched = false;
                while (j < input.Length)
                {
                    //This character matches, move on.
                    if (charcheck && input[j] == pattern[i])
                    {
                        j++;
                        matched = true;
                        break;
                    }
                    //If we need to check for multiple charaters to do.
                    if (multicharmask != null)
                    {
                        var ismatch = multicharmask.Contains(input[j]);
                        //If this was an inverted mask and we match fail the check for this string.
                        //If this was not an inverted mask check and we did not match fail for this string.
                        if (inversemulticharmask && ismatch ||
                            !inversemulticharmask && !ismatch)
                        {
                            //If we have a wildcard preceding us we ignore this failure
                            //and continue checking.
                            if (matchanymulti)
                            {
                                j++;
                                continue;
                            }
                            return false;
                        }
                        j++;
                        matched = true;
                        //Consumse our mask.
                        multicharmask = null;
                        break;
                    }
                    //We are in an multiple any-character mask, proceed to the next character.
                    if (matchanymulti)
                    {
                        j++;
                        continue;
                    }
                    break;
                }
                //We've found a match - proceed.
                if (matched)
                {
                    matchanymulti = false;
                    continue;
                }

                //If no match our mask fails
                return false;
            }
            //Some characters are left - our mask check fails.
            if (j < input.Length)
                return false;
            //We've processed everything - this is a match.
            return true;
        }

        /// <summary>
        /// Default masking character used in a mask.
        /// </summary>
        public static readonly char DefaultMaskCharacter = '*';

        /// <summary>
        /// Returns true if the string is non-null and at least the specified number of characters.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <param name="length">The minimum length.</param>
        /// <returns>True if string is non-null and at least the length specified.</returns>
        /// <exception>throws ArgumentOutOfRangeException if length is not a non-negative number.</exception>
        public static bool IsLengthAtLeast(this string value, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", length,
                                                        "The length must be a non-negative number.");
            }

            return value != null
                        ? value.Length >= length
                        : false;
        }

        /// <summary>
        /// Mask the source string with the mask char except for the last exposed digits.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="maskChar">The character to use to mask the source.</param>
        /// <param name="numExposed">Number of characters exposed in masked value.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, char maskChar, int numExposed)
        {
            var maskedString = sourceValue;

            if (sourceValue.IsLengthAtLeast(numExposed))
            {
                var builder = new StringBuilder(sourceValue.Length);
                int index = maskedString.Length - numExposed;

                builder.Append(maskChar, index);

                builder.Append(sourceValue.Substring(index));
                maskedString = builder.ToString();
            }

            return maskedString;
        }

        /// <summary>
        /// Mask the source string with the default mask char except for the last exposed digits.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="numExposed">Number of characters exposed in masked value.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, int numExposed)
        {
            return Mask(sourceValue, DefaultMaskCharacter, numExposed);
        }

        /// <summary>
        /// Mask the source string with the default mask char.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue)
        {
            return Mask(sourceValue, DefaultMaskCharacter, 0);
        }

        /// <summary>
        /// Mask the source string with the mask char.
        /// </summary>
        /// <param name="sourceValue">Original string to mask.</param>
        /// <param name="maskChar">The character to use to mask the source.</param>
        /// <returns>The masked account number.</returns>
        public static string Mask(this string sourceValue, char maskChar)
        {
            return Mask(sourceValue, maskChar, 0);
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
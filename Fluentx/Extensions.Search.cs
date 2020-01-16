using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    public static partial class Extensions
    {
        /// <summary>
        /// Linear search implementation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public static int LinearSearch<T>(this IList<T> list, T valueToFind) where T : IComparable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (valueToFind.CompareTo(list[i]) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Do a binary search on the specified ASSUMED sorted list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public static int BinarySearchFx<T>(this IList<T> list, T valueToFind) where T : IComparable<T>
        {
            //if (list.IsSorted().Not())
            //{
            //    throw new InvalidOperationException("Specified list is not sorted");
            //}
            var tempList = list;
            tempList.QuickSort();
            // Returns index of searchValue in sorted array x, or -1 if not found
            int left = 0;
            int right = list.Count;
            return tempList.BinarySearchFx(valueToFind, left, right);
        }
        /// <summary>
        /// Do a binary search on the specified ASSUMED sorted list, if not sorted an exception will be thrown
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="valueToFind"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int BinarySearchFx<T>(this IList<T> list, T valueToFind, int left, int right) where T : IComparable<T>
        {
            //if (list.IsSorted().Not())
            //{
            //    throw new InvalidOperationException("Specified list is not sorted");
            //}
            if (right < left)
            {
                return -1;
            }
            int mid = (left + right) >> 1;
            if (valueToFind.CompareTo(list[mid]) > 0)
            {
                return BinarySearchFx(list, valueToFind, mid + 1, right);
            }
            else if (valueToFind.CompareTo(list[mid]) < 0)
            {
                return list.BinarySearchFx(valueToFind, left, mid - 1);
            }
            else
            {
                return mid;
            }
        }
        /// <summary>
        /// Boyer Moore Search implementation
        /// </summary>
        /// <param name="text"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public static int[] BoyerMooreSearch(this string text, string valueToFind)
        {
            var retVal = new List<int>();
            int m = valueToFind.Length;
            int n = text.Length;

            int[] badChar = new int[256];

            BadCharHeuristic(valueToFind, m, ref badChar);

            int s = 0;
            while (s <= (n - m))
            {
                int j = m - 1;

                while (j >= 0 && valueToFind[j] == text[s + j])
                    --j;

                if (j < 0)
                {
                    retVal.Add(s);
                    s += (s + m < n) ? m - badChar[text[s + m]] : 1;
                }
                else
                {
                    s += Math.Max(1, j - badChar[text[s + j]]);
                }
            }

            return retVal.ToArray();

            void BadCharHeuristic(string str, int size, ref int[] _badChar)
            {
                int i;

                for (i = 0; i < 256; i++)
                    _badChar[i] = -1;

                for (i = 0; i < size; i++)
                    _badChar[(int)str[i]] = i;
            }
        }
        /// <summary>
        /// Knuth Morris Pratt Search implementation.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public static int[] KnuthMorrisPrattSearch(this string text, string valueToFind)
        {
            List<int> retVal = new List<int>();
            int M = valueToFind.Length;
            int N = text.Length;
            int i = 0;
            int j = 0;
            int[] lps = new int[M];

            ComputeLPSArray(valueToFind, M, lps);

            while (i < N)
            {
                if (valueToFind[j] == text[i])
                {
                    j++;
                    i++;
                }

                if (j == M)
                {
                    retVal.Add(i - j);
                    j = lps[j - 1];
                }

                else if (i < N && valueToFind[j] != text[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }

            return retVal.ToArray();

            void ComputeLPSArray(string _pat, int m, int[] _lps)
            {
                int len = 0;
                int k = 1;

                _lps[0] = 0;

                while (k < m)
                {
                    if (_pat[k] == _pat[len])
                    {
                        len++;
                        _lps[k] = len;
                        k++;
                    }
                    else
                    {
                        if (len != 0)
                        {
                            len = _lps[len - 1];
                        }
                        else
                        {
                            _lps[k] = 0;
                            k++;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Brute force search algorithm.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startLength"></param>
        /// <param name="endLength"></param>
        /// <param name="testCallback"></param>
        /// <returns></returns>
        public static bool BruteForce(this string text, int startLength, int endLength, Func<string, bool> testCallback)
        {
            for (int len = startLength; len <= endLength; ++len)
            {
                char[] chars = new char[len];

                for (int i = 0; i < len; ++i)
                    chars[i] = text[0];

                if (testCallback(chars.ToString()))
                    return true;

                for (int i1 = len - 1; i1 > -1; --i1)
                {
                    int i2;
                    for (i2 = text.IndexOf(chars[i1]) + 1; i2 < text.Length; ++i2)
                    {
                        chars[i1] = text[i2];

                        if (testCallback(chars.ToString()))
                            return true;

                        for (int i3 = i1 + 1; i3 < len; ++i3)
                        {
                            if (chars[i3] != text[text.Length - 1])
                            {
                                i1 = len;
                                goto outerBreak;
                            }
                        }
                    }

                outerBreak:
                    if (i2 == text.Length)
                        chars[i1] = text[0];
                }
            }

            return false;
        }
        /// <summary>
        /// A Search algorithm that uses hashing to find any one of a set of pattern strings in a text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public static int[] RabinKarpSearch(string text, string valueToFind)
        {
            var retVal = new List<int>();
            ulong siga = 0;
            ulong sigb = 0;
            ulong Q = 100007;
            ulong D = 256;

            for (int i = 0; i < valueToFind.Length; ++i)
            {
                siga = (siga * D + (ulong)text[i]) % Q;
                sigb = (sigb * D + (ulong)valueToFind[i]) % Q;
            }

            if (siga == sigb)
                retVal.Add(0);

            ulong pow = 1;

            for (int k = 1; k <= valueToFind.Length - 1; ++k)
                pow = (pow * D) % Q;

            for (int j = 1; j <= text.Length - valueToFind.Length; ++j)
            {
                siga = (siga + Q - pow * (ulong)text[j - 1] % Q) % Q;
                siga = (siga * D + (ulong)text[j + valueToFind.Length - 1]) % Q;

                if (siga == sigb)
                    if (text.Substring(j, valueToFind.Length) == valueToFind)
                        retVal.Add(j);
            }

            return retVal.ToArray();
        }
        /// <summary>
        /// This algorithm searches for all the occurrences of pattern and its permutations (or anagrams) in the specified text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public static List<int> AnagramSearch(string text, string valueToFind)
        {
            var MAX = 256;

            List<int> retVal = new List<int>();
            char[] countP = new char[MAX];
            char[] countT = new char[MAX];

            for (int i = 0; i < valueToFind.Length; ++i)
            {
                (countP[valueToFind[i]])++;
                (countT[text[i]])++;
            }

            for (int i = valueToFind.Length; i < text.Length; ++i)
            {
                if (Compare(countP, countT))
                    retVal.Add((i - valueToFind.Length));

                (countT[text[i]])++;
                countT[text[i - valueToFind.Length]]--;
            }

            if (Compare(countP, countT))
                retVal.Add(text.Length - valueToFind.Length);

            return retVal;

            bool Compare(char[] arr1, char[] arr2)
            {
                for (int i = 0; i < MAX; ++i)
                    if (arr1[i] != arr2[i])
                        return false;

                return true;
            }
        }
        /// <summary>
        /// Returns a value of how much similar the two strings are.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double SorensenDiceMatch(this string first, string second)
        {
            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
                return 0;

            if (first == second)
                return 1;

            int strlen1 = first.Length;
            int strlen2 = second.Length;

            if (strlen1 < 2 || strlen2 < 2)
                return 0;

            int length1 = strlen1 - 1;
            int length2 = strlen2 - 1;

            double matches = 0;
            int i = 0;
            int j = 0;

            while (i < length1 && j < length2)
            {
                string a = first.Substring(i, 2);
                string b = second.Substring(j, 2);
                int cmp = string.Compare(a, b);

                if (cmp == 0)
                    matches += 2;

                ++i;
                ++j;
            }

            return matches / (length1 + length2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int DamerauLevenshteinDistance(this string first, string second)
        {
            var bounds = new { Height = first.Length + 1, Width = second.Length + 1 };

            int[,] matrix = new int[bounds.Height, bounds.Width];

            for (int height = 0; height < bounds.Height; height++) { matrix[height, 0] = height; };
            for (int width = 0; width < bounds.Width; width++) { matrix[0, width] = width; };

            for (int height = 1; height < bounds.Height; height++)
            {
                for (int width = 1; width < bounds.Width; width++)
                {
                    int cost = (first[height - 1] == second[width - 1]) ? 0 : 1;
                    int insertion = matrix[height, width - 1] + 1;
                    int deletion = matrix[height - 1, width] + 1;
                    int substitution = matrix[height - 1, width - 1] + cost;

                    int distance = Math.Min(insertion, Math.Min(deletion, substitution));

                    if (height > 1 && width > 1 && first[height - 1] == second[width - 2] && first[height - 2] == second[width - 1])
                    {
                        distance = Math.Min(distance, matrix[height - 2, width - 2] + cost);
                    }

                    matrix[height, width] = distance;
                }
            }

            //return matrix[bounds.Height - 1, bounds.Width - 1];
            return (int)(Math.Round(1.0 - ((double)matrix[bounds.Height - 1, bounds.Width - 1] / (double)Math.Max(first.Length, second.Length)), 2) * 100);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int LevenshteinDistance(this string first, string second)
        {
            int n = first.Length;
            int m = second.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (second[j - 1] == first[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            //return d[n, m];
            return (int)(Math.Round(1.0 - (d[n, m] / (double)Math.Max(first.Length, second.Length)), 2) * 100);
        }
        ///// <summary>
        ///// Returns the Jaro-Winkler distance between the specified  
        ///// strings. The distance is symmetric and will fall in the 
        ///// range 0 (no match) to 1 (perfect match). 
        ///// </summary>
        ///// <param name="aString1">First String</param>
        ///// <param name="aString2">Second String</param>
        ///// <returns></returns>
        //public static int JaroWinkler(this string aString1, string aString2)
        //{
        //    /* The Winkler modification will not be applied unless the 
        //    * percent match was at or above the mWeightThreshold percent 
        //    * without the modification. 
        //    * Winkler's paper used a default value of 0.7
        //    */
        //    double mWeightThreshold = 0.7;
        //    /* Size of the prefix to be concidered by the Winkler modification. 
        //     * Winkler's paper used a default value of 4
        //     */
        //    int mNumChars = 4;

        //    int lLen1 = aString1.Length;
        //    int lLen2 = aString2.Length;

        //    if (lLen1 == 0)
        //        return lLen2 == 0 ? 1 : 0;

        //    int lSearchRange = Math.Max(0, Math.Max(lLen1, lLen2) / 2 - 1);

        //    // default initialized to false
        //    bool[] lMatched1 = new bool[lLen1];
        //    bool[] lMatched2 = new bool[lLen2];

        //    int lNumCommon = 0;
        //    for (int i = 0; i < lLen1; ++i)
        //    {
        //        int lStart = Math.Max(0, i - lSearchRange);
        //        int lEnd = Math.Min(i + lSearchRange + 1, lLen2);
        //        for (int j = lStart; j < lEnd; ++j)
        //        {
        //            if (lMatched2[j]) continue;
        //            if (aString1[i] != aString2[j])
        //                continue;
        //            lMatched1[i] = true;
        //            lMatched2[j] = true;
        //            ++lNumCommon;
        //            break;
        //        }
        //    }
        //    if (lNumCommon == 0) return 0;

        //    int lNumHalfTransposed = 0;
        //    int k = 0;
        //    for (int i = 0; i < lLen1; ++i)
        //    {
        //        if (!lMatched1[i]) continue;
        //        while (!lMatched2[k]) ++k;
        //        if (aString1[i] != aString2[k])
        //            ++lNumHalfTransposed;
        //        ++k;
        //    }
        //    // System.Diagnostics.Debug.WriteLine("numHalfTransposed=" + numHalfTransposed);
        //    int lNumTransposed = lNumHalfTransposed / 2;

        //    // System.Diagnostics.Debug.WriteLine("numCommon=" + numCommon + " numTransposed=" + numTransposed);
        //    double lNumCommonD = lNumCommon;
        //    double lWeight = (lNumCommonD / lLen1
        //                     + lNumCommonD / lLen2
        //                     + (lNumCommon - lNumTransposed) / lNumCommonD) / 3.0;

        //    if (lWeight <= mWeightThreshold) return (int)Math.Round(lWeight, 2);
        //    int lMax = Math.Min(mNumChars, Math.Min(aString1.Length, aString2.Length));
        //    int lPos = 0;
        //    while (lPos < lMax && aString1[lPos] == aString2[lPos])
        //        ++lPos;
        //    if (lPos == 0) return (int)Math.Round(lWeight, 2);
        //    return (int)Math.Round((lWeight + 0.1 * lPos * (1.0 - lWeight)), 2);

        //}
    }
}

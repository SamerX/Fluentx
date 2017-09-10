using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    public static partial class Extensions
    {
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
                    int i2 = 0;

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
    }
}

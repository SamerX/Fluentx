using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    public static partial class Extensions
    {
        /// <summary>
        /// Sorts the list in ascending order using Insertion Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSort<T>(this IList<T> list)
        {
            list.InsertionSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Insertion Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void InsertionSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            list.InsertionSort(0, list.Count - 1, comparer);
        }
        /// <summary>
        /// Sorts the list in ascending order using Insertion Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void InsertionSort<T>(this IList<T> list, int left, int right)
        {
            list.InsertionSort(left, right, Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Insertion Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparer"></param>
        public static void InsertionSort<T>(this IList<T> list, int left, int right, IComparer<T> comparer)
        {
            int i, j;
            T temp;

            for (i = left + 1; i <= right; ++i)
            {
                temp = list[i];
                for (j = i - 1; j >= 0; --j)
                {

                    if (comparer.Compare(temp, list[j]) < 0)
                    {
                        list[j + 1] = list[j];
                    }
                    else
                    {
                        break;
                    }
                }
                list[j + 1] = temp;
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Insertions Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSortDescending<T>(this IList<T> list)
        {
            list.InsertionSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Insertion Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void InsertionSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            list.InsertionSortDescending(0, list.Count - 1, comparer);
        }
        /// <summary>
        /// Sorts the list in descending order using Insertion Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void InsertionSortDescending<T>(this IList<T> list, int left, int right)
        {
            list.InsertionSortDescending(left, right, Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Insertion Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparer"></param>
        public static void InsertionSortDescending<T>(this IList<T> list, int left, int right, IComparer<T> comparer)
        {
            int i, j;
            T temp;

            for (i = left + 1; i <= right; ++i)
            {
                temp = list[i];
                for (j = i - 1; j >= 0; --j)
                {

                    if (comparer.Compare(temp, list[j]) > 0)
                    {
                        list[j + 1] = list[j];
                    }
                    else
                    {
                        break;
                    }
                }
                list[j + 1] = temp;
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Quick Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void QuickSort<T>(this IList<T> list)
        {
            list.QuickSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Quick Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void QuickSort<T>(this IList<T> list, IComparer<T> comparer)// where T : IComparable<T>
        {
            InternalQuickSort(list, 0, list.Count - 1, comparer);

            void InternalQuickSort(IList<T> _list, int left, int right, IComparer<T> _comparer)
            {
                int i, j;
                T pivot, temp;

                i = left;
                j = right;
                pivot = _list[(left + right) / 2];

                do
                {
                    while (_comparer.Compare(_list[i], pivot) < 0 && (i < right))
                    {
                        i++;
                    }

                    while (_comparer.Compare(pivot, _list[j]) < 0 && (j > left))
                    {
                        j--;
                    }

                    if (i <= j)
                    {
                        temp = _list[i];
                        _list[i] = _list[j];
                        _list[j] = temp;
                        i++; j--;
                    }
                } while (i <= j);

                if (left < j)
                {
                    InternalQuickSort(_list, left, j, _comparer);
                }

                if (i < right)
                {
                    InternalQuickSort(_list, i, right, _comparer);
                }
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Quick Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void QuickSortDescending<T>(this IList<T> list)
        {
            list.QuickSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Wuick Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void QuickSortDescending<T>(this IList<T> list, IComparer<T> comparer)// where T : IComparable<T>
        {
            InternalQuickSortDescending(list, 0, list.Count - 1, comparer);

            void InternalQuickSortDescending(IList<T> _list, int left, int right, IComparer<T> _comparer)
            {
                int i, j;
                T pivot, temp;

                i = left;
                j = right;
                pivot = _list[(left + right) / 2];

                do
                {
                    while (_comparer.Compare(_list[i], pivot) > 0 && (i < right))
                    {
                        i++;
                    }

                    while (_comparer.Compare(pivot, _list[j]) > 0 && (j > left))
                    {
                        j--;
                    }

                    if (i <= j)
                    {
                        temp = _list[i];
                        _list[i] = _list[j];
                        _list[j] = temp;
                        i++; j--;
                    }
                } while (i <= j);

                if (left < j)
                {
                    InternalQuickSortDescending(_list, left, j, _comparer);
                }

                if (i < right)
                {
                    InternalQuickSortDescending(_list, i, right, _comparer);
                }
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Cocktail Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void CocktailSort<T>(this IList<T> list)
        {
            list.CocktailSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Cocktail Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void CocktailSort<T>(this IList<T> list, IComparer<T> comparer)// where T : IComparable<T>
        {
            for (int k = list.Count - 1; k > 0; k--)
            {
                bool swapped = false;
                for (int i = k; i > 0; i--)
                {
                    if (comparer.Compare(list[i], list[i - 1]) < 0)
                    {
                        // swap
                        T temp = list[i];
                        list[i] = list[i - 1];
                        list[i - 1] = temp;
                        swapped = true;
                    }
                }

                for (int i = 0; i < k; i++)
                {
                    if (comparer.Compare(list[i], list[i + 1]) > 0)
                    {
                        // swap
                        T temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Cocktail Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void CocktailSortDescending<T>(this IList<T> list)
        {
            list.CocktailSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Cocktail Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void CocktailSortDescending<T>(this IList<T> list, IComparer<T> comparer)// where T : IComparable<T>
        {
            for (int k = list.Count - 1; k > 0; k--)
            {
                bool swapped = false;
                for (int i = k; i > 0; i--)
                {
                    if (comparer.Compare(list[i], list[i - 1]) > 0)
                    {
                        // swap
                        T temp = list[i];
                        list[i] = list[i - 1];
                        list[i - 1] = temp;
                        swapped = true;
                    }
                }

                for (int i = 0; i < k; i++)
                {
                    if (comparer.Compare(list[i], list[i + 1]) < 0)
                    {
                        // swap
                        T temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Odd Even Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void OddEvenSort<T>(this IList<T> list)
        {
            list.OddEvenSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Odd Even Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void OddEvenSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            T temp;

            for (int i = 0; i < list.Count / 2; ++i)
            {
                for (int j = 0; j < list.Count - 1; j += 2)
                {
                    if (comparer.Compare(list[j], list[j + 1]) > 0)
                    {
                        temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }

                for (int j = 1; j < list.Count - 1; j += 2)
                {
                    if (comparer.Compare(list[j], list[j + 1]) > 0)
                    {
                        temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Odd Even Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void OddEvenSortDescending<T>(this IList<T> list)
        {
            list.OddEvenSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Odd Even Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void OddEvenSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            T temp;

            for (int i = 0; i < list.Count / 2; ++i)
            {
                for (int j = 0; j < list.Count - 1; j += 2)
                {
                    if (comparer.Compare(list[j], list[j + 1]) < 0)
                    {
                        temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }

                for (int j = 1; j < list.Count - 1; j += 2)
                {
                    if (comparer.Compare(list[j], list[j + 1]) < 0)
                    {
                        temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Comb Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void CombSort<T>(this IList<T> list)
        {
            list.CombSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Comb Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void CombSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            int gap = list.Count;
            bool swapped;
            do
            {
                swapped = false;
                gap = newGap(gap);
                for (int i = 0; i < (list.Count - gap); i++)
                {
                    if (comparer.Compare(list[i], list[i + gap]) > 0)
                    {
                        swapped = true;
                        T temp = list[i];
                        list[i] = list[i + gap];
                        list[i + gap] = temp;
                    }
                }
            } while (gap > 1 || swapped);

            int newGap(int _gap)
            {
                _gap = _gap * 10 / 13;
                if (_gap == 9 || _gap == 10)
                {
                    _gap = 11;
                }

                if (_gap < 1)
                {
                    return 1;
                }

                return _gap;
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Comb Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void CombSortDescending<T>(this IList<T> list)
        {
            list.CombSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Comb Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void CombSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            int gap = list.Count;
            bool swapped;
            do
            {
                swapped = false;
                gap = newGap(gap);
                for (int i = 0; i < (list.Count - gap); i++)
                {
                    if (comparer.Compare(list[i], list[i + gap]) < 0)
                    {
                        swapped = true;
                        T temp = list[i];
                        list[i] = list[i + gap];
                        list[i + gap] = temp;
                    }
                }
            } while (gap > 1 || swapped);

            int newGap(int _gap)
            {
                _gap = _gap * 10 / 13;
                if (_gap == 9 || _gap == 10)
                {
                    _gap = 11;
                }

                if (_gap < 1)
                {
                    return 1;
                }

                return _gap;
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Gnome Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void GnomeSort<T>(this IList<T> list)
        {
            list.GnomeSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Gnome Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void GnomeSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            int i = 0;
            while (i < list.Count)
            {
                if (i == 0 || comparer.Compare(list[i - 1], list[i]) <= 0)
                {
                    i++;
                }
                else
                {
                    T temp = list[i];
                    list[i] = list[i - 1];
                    list[--i] = temp;
                }
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Gnome Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void GnomeSortDescending<T>(this IList<T> list)
        {
            list.GnomeSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Gnome Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void GnomeSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            int i = 0;
            while (i < list.Count)
            {
                if (i == 0 || comparer.Compare(list[i - 1], list[i]) >= 0)
                {
                    i++;
                }
                else
                {
                    T temp = list[i];
                    list[i] = list[i - 1];
                    list[--i] = temp;
                }
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Shell Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void ShellSort<T>(this IList<T> list)
        {
            list.ShellSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Shell Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void ShellSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            T temp;
            int i, j;
            int increment = 3;

            while (increment > 0)
            {
                for (i = 0; i < list.Count; i++)
                {
                    j = i;
                    temp = list[i];

                    while ((j >= increment) && (comparer.Compare(list[j - increment], temp) > 0))
                    {
                        list[j] = list[j - increment];
                        j = j - increment;
                    }

                    list[j] = temp;
                }

                if (increment / 2 != 0)
                {
                    increment = increment / 2;
                }
                else if (increment == 1)
                {
                    increment = 0;
                }
                else
                {
                    increment = 1;
                }
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Shell Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void ShellSortDescending<T>(this IList<T> list)
        {
            list.ShellSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Shell Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void ShellSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            T temp;
            int i, j;
            int increment = 3;

            while (increment > 0)
            {
                for (i = 0; i < list.Count; i++)
                {
                    j = i;
                    temp = list[i];

                    while ((j >= increment) && (comparer.Compare(list[j - increment], temp) < 0))
                    {
                        list[j] = list[j - increment];
                        j = j - increment;
                    }

                    list[j] = temp;
                }

                if (increment / 2 != 0)
                {
                    increment = increment / 2;
                }
                else if (increment == 1)
                {
                    increment = 0;
                }
                else
                {
                    increment = 1;
                }
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Intro Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void IntroSort<T>(this IList<T> list)
        {
            list.IntroSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Intro Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void IntroSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            int partitionSize = Partition(list, 0, list.Count - 1, comparer);

            if (partitionSize < 16)
            {
                list.InsertionSort(comparer);
            }
            else if (partitionSize > (2 * Math.Log(list.Count)))
            {
                list.HeapSort(comparer);
            }
            else
            {
                list.QuickSort(comparer);
            }

            int Partition(IList<T> data, int left, int right, IComparer<T> _comparer)
            {
                T pivot = data[right];
                T temp;
                int i = left;

                for (int j = left; j < right; ++j)
                {
                    if (_comparer.Compare(data[j], pivot) <= 0)
                    {
                        temp = data[j];
                        data[j] = data[i];
                        data[i] = temp;
                        i++;
                    }
                }

                data[right] = data[i];
                data[i] = pivot;

                return i;
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Intro Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void IntroSortDescending<T>(this IList<T> list)
        {
            list.IntroSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Intro Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void IntroSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            int partitionSize = Partition(list, 0, list.Count - 1);

            if (partitionSize < 16)
            {
                list.InsertionSortDescending(comparer);
            }
            else if (partitionSize > (2 * Math.Log(list.Count)))
            {
                list.MergeSortDescending(comparer);//Should be Heap Sort Descending
            }
            else
            {
                list.QuickSortDescending(comparer);
            }

            int Partition(IList<T> data, int left, int right)
            {
                T pivot = data[right];
                T temp;
                int i = left;

                for (int j = left; j < right; ++j)
                {
                    if (comparer.Compare(data[j], pivot) <= 0)
                    {
                        temp = data[j];
                        data[j] = data[i];
                        data[i] = temp;
                        i++;
                    }
                }

                data[right] = data[i];
                data[i] = pivot;

                return i;
            }
        }

        /// <summary>
        /// NOT RECOMMENDED SEARCH, i have it here for experimental purposes only, it returns the number of times shuffling occured to get the list sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static int BogoSort<T>(this IList<T> list)
        {
            int iterationsCount = 0;
            while (!list.IsSorted())
            {
                iterationsCount++;
                list.Shuffle();
            }
            return iterationsCount;
        }
        /// <summary>
        /// NOT RECOMMENDED SEARCH, I have it here for experimental purposes only, it returns the number of times shuffling occured to get the list sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static int BogoSortDescending<T>(this IList<T> list)
        {
            int iterationsCount = 0;
            while (!list.IsSortedDescending())
            {
                iterationsCount++;
                list.Shuffle();
            }
            return iterationsCount;
        }
        /// <summary>
        /// Sorts the list in ascending order using Selection Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void SelectionSort<T>(this IList<T> list)
        {
            list.SelectionSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Selection Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void SelectionSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            T temp;
            int i, j;
            int min;

            for (i = 0; i < list.Count - 1; i++)
            {
                min = i;
                for (j = i + 1; j < list.Count; j++)
                {
                    if (comparer.Compare(list[j], list[min]) < 0)
                    {
                        min = j;
                    }
                }
                temp = list[i];
                list[i] = list[min];
                list[min] = temp;
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Selection Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void SelectionSortDescending<T>(this IList<T> list)
        {
            list.SelectionSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Selection Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void SelectionSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            T temp;
            int i, j;
            int min;

            for (i = 0; i < list.Count - 1; i++)
            {
                min = i;
                for (j = i + 1; j < list.Count; j++)
                {
                    if (comparer.Compare(list[j], list[min]) > 0)
                    {
                        min = j;
                    }
                }
                temp = list[i];
                list[i] = list[min];
                list[min] = temp;
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Heap Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void HeapSort<T>(this IList<T> list)
        {
            list.HeapSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Heap Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void HeapSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            int i;
            T temp;
            int n = list.Count;

            for (i = (n / 2) - 1; i >= 0; i--)
            {
                ShiftDown(list, i, n);
            }

            for (i = n - 1; i >= 1; i--)
            {
                temp = list[0];
                list[0] = list[i];
                list[i] = temp;
                ShiftDown(list, 0, i - 1);
            }

            void ShiftDown(IList<T> _list, int root, int bottom)
            {
                bool done = false;
                int maxChild;
                T _temp;

                while ((root * 2 <= bottom) && (!done))
                {
                    if (root * 2 == bottom)
                        maxChild = root * 2;
                    else if (comparer.Compare(_list[root * 2], _list[root * 2 + 1]) > 0)
                        maxChild = root * 2;
                    else
                        maxChild = root * 2 + 1;

                    if (comparer.Compare(_list[root], _list[maxChild]) < 0)
                    {
                        _temp = _list[root];
                        _list[root] = _list[maxChild];
                        _list[maxChild] = _temp;
                        root = maxChild;
                    }
                    else
                    {
                        done = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sorts the list in ascending order using Bubble Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void BubbleSort<T>(this IList<T> list)
        {
            list.BubbleSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Bubble Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void BubbleSort<T>(this IList<T> list, IComparer<T> comparer)
        {

            for (int pass = 1; pass < list.Count - 1; pass++)
            {
                // Count how many times this next looop
                // becomes shorter and shorter
                for (int i = 0; i < list.Count - pass; i++)
                {
                    if (comparer.Compare(list[i], list[i + 1]) > 0)
                    {
                        // Exchange elements
                        T temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Bubble Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void BubbleSortDescending<T>(this IList<T> list)
        {
            list.BubbleSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Bubble Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void BubbleSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            for (int pass = 1; pass < list.Count - 1; pass++)
            {
                // Count how many times this next looop
                // becomes shorter and shorter
                for (int i = 0; i < list.Count - pass; i++)
                {
                    if (comparer.Compare(list[i], list[i + 1]) < 0)
                    {
                        // Exchange elements
                        T temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                    }
                }
            }
        }
        /// <summary>
        /// Sorts the list in ascending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void MergeSort<T>(this IList<T> list)
        {
            list.MergeSort(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void MergeSort<T>(this IList<T> list, IComparer<T> comparer)
        {
            list.MergeSort(0, list.Count - 1, comparer);
        }
        /// <summary>
        /// Sorts the list in descending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void MergeSortDescending<T>(this IList<T> list)
        {
            list.MergeSortDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void MergeSortDescending<T>(this IList<T> list, IComparer<T> comparer)
        {
            list.MergeSortDescending(0, list.Count - 1, comparer);
        }
        /// <summary>
        /// Sorts the list in ascending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void MergeSort<T>(this IList<T> list, int left, int right)
        {
            list.MergeSort(left, right, Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in ascending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparer"></param>
        public static void MergeSort<T>(this IList<T> list, int left, int right, IComparer<T> comparer)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSort(list, left, middle, comparer);
                MergeSort(list, middle + 1, right, comparer);
                Merge(list, left, middle, middle + 1, right, comparer);
            }

            void Merge(IList<T> _list, int _left, int _middle, int _middle1, int _right, IComparer<T> _comparer)
            {
                int oldPosition = _left;
                int size = _right - _left + 1;
                T[] temp = new T[size];
                int i = 0;

                while (_left <= _middle && _middle1 <= _right)
                {
                    if (comparer.Compare(_list[_left], _list[_middle1]) <= 0)
                        temp[i++] = _list[_left++];
                    else
                        temp[i++] = _list[_middle1++];
                }
                if (_left > _middle)
                    for (int j = _middle1; j <= _right; j++)
                        temp[i++] = _list[_middle1++];
                else
                    for (int j = _left; j <= _middle; j++)
                        temp[i++] = _list[_left++];

                for (int k = oldPosition; k < oldPosition + size; k++)
                {
                    _list[k] = temp[k - oldPosition];
                }
                //Array.Copy(temp, 0, _list, oldPosition, size);
            }
        }
        /// <summary>
        /// Sorts the list in descending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void MergeSortDescending<T>(this IList<T> list, int left, int right)
        {
            list.MergeSortDescending(left, right, Comparer<T>.Default);
        }
        /// <summary>
        /// Sorts the list in descending order using Merge Sort algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="comparer"></param>
        public static void MergeSortDescending<T>(this IList<T> list, int left, int right, IComparer<T> comparer)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSortDescending(list, left, middle, comparer);
                MergeSortDescending(list, middle + 1, right, comparer);
                MergeDescending(list, left, middle, middle + 1, right, comparer);
            }

            void MergeDescending(IList<T> _list, int _left, int _middle, int _middle1, int _right, IComparer<T> _comparer)
            {
                int oldPosition = _left;
                int size = _right - _left + 1;
                T[] temp = new T[size];
                int i = 0;

                while (_left <= _middle && _middle1 <= _right)
                {
                    if (_comparer.Compare(_list[_left], _list[_middle1]) >= 0)
                        temp[i++] = _list[_left++];
                    else
                        temp[i++] = _list[_middle1++];
                }
                if (_left > _middle)
                    for (int j = _middle1; j <= _right; j++)
                        temp[i++] = _list[_middle1++];
                else
                    for (int j = _left; j <= _middle; j++)
                        temp[i++] = _list[_left++];

                for (int k = oldPosition; k < oldPosition + size; k++)
                {
                    _list[k] = temp[k - oldPosition];
                }
                //Array.Copy(temp, 0, _list, oldPosition, size);
            }
        }
        /// <summary>
        /// Returns whether the specified list is sorted in ascending order or not, it uses linear search to validate order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsSorted<T>(this IList<T> list)
        {
            return list.IsSorted(Comparer<T>.Default);
        }
        /// <summary>
        /// Returns whether the specified list is sorted in ascending order or not, it uses linear search to validate order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsSorted<T>(this IList<T> list, IComparer<T> comparer)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (comparer.Compare(list[i - 1], list[i]) > 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Returns whether the specified list is sorted in descending order or not, it uses linear search to validate order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsSortedDescending<T>(this IList<T> list)
        {
            return list.IsSortedDescending(Comparer<T>.Default);
        }
        /// <summary>
        /// Returns whether the specified list is sorted in descending order or not, it uses linear search to validate order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsSortedDescending<T>(this IList<T> list, IComparer<T> comparer)// where T : IComparable<T>
        {
            for (int i = 1; i < list.Count; i++)
            {
                var v = comparer.Compare(list[i - 1], list[i]);
                if (comparer.Compare(list[i - 1], list[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Reverses the list backwords.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Reverse<T>(this IList<T> list)// where T : IComparable<T>
        {
            int mid = list.Count / 2;

            for (int i = 0; i < mid; i++)
            {
                T temp = list[i];
                list[i] = list[list.Count - i - 1];
                list[list.Count - i - 1] = temp;
            }
        }
    }
}


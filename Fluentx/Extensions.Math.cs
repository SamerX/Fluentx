using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentx
{
    public static partial class Extensions
    {
        /// <summary>
        /// Returns the distance between two points on earth in meters.
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon1"></param>
        /// <param name="lng2"></param>
        /// <returns></returns>
        public static double Haversine(double lat1, double lng1, double lat2, double lng2)
        {
            const double r = 6371; // meters

            var sdlat = Math.Sin((lat2 - lat1) / 2);
            var sdlon = Math.Sin((lng2 - lng1) / 2);
            var q = sdlat * sdlat + Math.Cos(lat1) * Math.Cos(lat2) * sdlon * sdlon;
            var d = 2 * r * Math.Asin(Math.Sqrt(q));

            return d;
        }
        /// <summary>
        /// Levenshtein Distance is a string metric for measuring the difference between two sequences, distance between two words is the minimum number of single-character edits (insertions, deletions or substitutions) required to change one word into the other.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int LevenshteinDistance(string first, string second)
        {
            uint s1len, s2len, x, y, lastdiag, olddiag;
            s1len = (uint)first.Length;
            s2len = (uint)second.Length;
            uint[] column = new uint[s1len + 1];

            for (y = 1; y <= s1len; ++y)
                column[y] = y;

            for (x = 1; x <= s2len; ++x)
            {
                column[0] = x;

                for (y = 1, lastdiag = x - 1; y <= s1len; ++y)
                {
                    olddiag = column[y];
                    column[y] = MIN3(column[y] + 1, column[y - 1] + 1, (uint)(lastdiag + (first[(int)(y - 1)] == second[(int)(x - 1)] ? 0 : 1)));
                    lastdiag = olddiag;
                }
            }

            return (int)(column[s1len]);

            uint MIN3(uint a, uint b, uint c)
            {
                return ((a) < (b) ? ((a) < (c) ? (a) : (c)) : ((b) < (c) ? (b) : (c)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] Fibonacci(this int count)
        {
            int[] numbers = new int[count];

            if (count > 1)
            {
                numbers[0] = 0;
                numbers[1] = 1;
            }

            int i = 2;
            while (i < count)
            {
                numbers[i] = numbers[i - 1] + numbers[i - 2];
                ++i;
            }

            return numbers;
        }

        public static long Factorial(this int number)
        {
            if (number < 0)
                return -1; //Error

            long result = 1;

            for (int i = 1; i <= number; ++i)
                result *= i;

            return result;
        }

        public static int GreatestCommonDivisor(int first, int second)
        {
            if (first == 0)
                return second;

            while (second != 0)
            {
                if (first > second)
                    first -= second;
                else
                    second -= first;
            }

            return first;
        }
        /// <summary>
        /// An Armstrong number is an integer such that the sum of the cubes of its digits is equal to the number itself.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsArmstrong(this double num)
        {
            double sum = 0;
            int remainder;
            int digits = 0;
            double temp = num;

            while (temp != 0)
            {
                ++digits;
                temp /= 10;
            }

            temp = num;

            while (temp != 0)
            {
                remainder = (int)(temp % 10);
                sum += Math.Pow(remainder, digits);
                temp /= 10;
            }

            return num == sum;
        }
        /// <summary>
        /// This method reverses any number backwards.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int Reverse(this int number)
        {
            int reverse = 0;

            while (number != 0)
            {
                reverse *= 10;
                reverse += number % 10;
                number /= 10;
            }

            return reverse;
        }
        /// <summary>
        /// This returns whether a number is palindrome or not.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsPalindrome(this int num)
        {
            return num == Reverse(num);
        }
        /// <summary>
        /// A perfect number is a positive integer that is equal to the sum of its proper positive divisors, that is, the sum of its positive divisors excluding the number itself.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPerfect(this int number)
        {
            int sum = 0;
            int i = 1;

            while (i < number)
            {
                if (number % i == 0)
                    sum += i;

                ++i;
            }

            return sum == number;
        }
        /// <summary>
        /// A strong number is a number where the sum of factorial of its digits is equal the number it self.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsStrong(this int number)
        {
            long fact;
            int num = number;
            long sum = 0;

            while (number != 0)
            {
                fact = 1;

                for (int i = 1; i <= number % 10; ++i)
                    fact *= i;

                sum += fact;

                number /= 10;
            }

            return sum == num;
        }

        public static IEnumerable<int> PrimeFactor(this int number)
        {
            var factors = new List<int>();

            while (number % 2 == 0)
            {
                factors.Add(2);
                number /= 2;
            }

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                while (number % i == 0)
                {
                    factors.Add(i);
                    number /= i;
                }
            }

            if (number > 2)
            {
                factors.Add(number);
            }

            return factors;
        }
        /// <summary>
        /// Hypotenuse is the longest side of a right-angled triangle, the side opposite of the right angle.
        /// </summary>
        /// <param name="side1"></param>
        /// <param name="side2"></param>
        /// <returns></returns>
        public static double Hypotenuse(double side1, double side2)
        {
            return Math.Sqrt((side1 * side1) + (side2 * side2));
        }
        /// <summary>
        /// Returns the wording of a specific number in english.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToWords(this int number)
        {
            var words = new StringBuilder();
            string[] singles = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] teens = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tens = new string[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] powers = new string[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion" };

            if (number >= 1000)
            {
                for (int i = powers.Length - 1; i >= 0; i--)
                {
                    int power = (int)Math.Pow(1000, i);
                    int count = (number - (number % power)) / power;

                    if (number > power)
                    {
                        words.Append(ToWords(count) + " " + powers[i]);
                        number -= count * power;
                    }
                }
            }

            if (number >= 100)
            {
                int count = (number - (number % 100)) / 100;
                words.Append(ToWords(count) + " hundred");
                number -= count * 100;
            }

            if (number >= 20 && number < 100)
            {
                int count = (number - (number % 10)) / 10;
                words.Append(" " + tens[count]);
                number -= count * 10;
            }

            if (number >= 10 && number < 20)
            {
                words.Append(" " + teens[number - 10]);
                number = 0;
            }

            if (number > 0 && number < 10)
            {
                words.Append(" " + singles[number]);
            }

            return words.ToString();
        }
        /// <summary>
        /// Returns whether a number is prime or not.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static bool IsPrime(this int number)
        {
            // Test whether the parameter is a prime number.
            if ((number & 1) == 0)
            {
                if (number == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // Note:
            // ... This version was changed to test the square.
            // ... Original version tested against the square root.
            // ... Also we exclude 1 at the end.
            for (int i = 3; (i * i) <= number; i += 2)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }
            return number != 1;
        }

        
    }
}

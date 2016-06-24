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
        /// Returns the date in January of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime January(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 1, @this);
        }
        /// <summary>
        /// Returns the date in February of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime February(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 2, @this);
        }
        /// <summary>
        /// Returns the date in March of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime March(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 3, @this);
        }
        /// <summary>
        /// Returns the date in April of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime April(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 4, @this);
        }
        /// <summary>
        /// Returns the date in May of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime May(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 5, @this);
        }
        /// <summary>
        /// Returns the date in June of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime June(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 6, @this);
        }
        /// <summary>
        /// Returns the date in July of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime July(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 7, @this);
        }
        /// <summary>
        /// Returns the date in August of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime August(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 8, @this);
        }
        /// <summary>
        /// Returns the date in September of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime September(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 9, @this);
        }
        /// <summary>
        /// Returns the date in October of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime October(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 10, @this);
        }
        /// <summary>
        /// Returns the date in November of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime November(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 11, @this);
        }
        /// <summary>
        /// Returns the date in December of the specified year or current year.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime December(this int @this, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, 12, @this);
        }
        /// <summary>
        /// Returns the date of the specifed day in year. e.g 365.DayInYear(2014) => 31/12/2014
        /// </summary>
        /// <param name="this"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime DayInYear(this int @this, int? year = null)
        {
            var firstDayOfYear = new DateTime(year ?? DateTime.Now.Year, 1, 1);
            return firstDayOfYear.AddDays(@this - 1);
        }
        /// <summary>
        /// Returns the current datetime - the specifed number of seconds
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime SecondsAgo(this int @this)
        {
            return DateTime.Now.AddSeconds(-@this);
        }
        /// <summary>
        /// Returns the current datetime - the specifed number of minutes
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime MinutesAgo(this int @this)
        {
            return DateTime.Now.AddMinutes(-@this);
        }
        /// <summary>
        /// Returns the current datetime - the specifed number of hours
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime HoursAgo(this int @this)
        {
            return DateTime.Now.AddHours(-@this);
        }
        /// <summary>
        /// Returns the current datetime - the specifed number of days
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime DaysAgo(this int @this)
        {
            return DateTime.Now.AddDays(-@this);
        }
        /// <summary>
        /// Returns the current datetime - the specifed number of months
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime MonthsAgo(this int @this)
        {
            return DateTime.Now.AddMonths(-@this);
        }
        /// <summary>
        /// Returns the current datetime - the specifed number of years
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime YearsAgo(this int @this)
        {
            return DateTime.Now.AddYears(-@this);
        }
        /// <summary>
        /// Returns the current datetime + the specifed number of seconds
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime SecondsFromNow(this int @this)
        {
            return DateTime.Now.AddSeconds(@this);
        }
        /// <summary>
        /// Returns the current datetime + the specifed number of minutes
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime MinutesFromNow(this int @this)
        {
            return DateTime.Now.AddMinutes(@this);
        }
        /// <summary>
        /// Returns the current datetime + the specifed number of hours
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime HoursFromNow(this int @this)
        {
            return DateTime.Now.AddHours(@this);
        }
        /// <summary>
        /// Returns the current datetime + the specifed number of days
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime DaysFromNow(this int @this)
        {
            return DateTime.Now.AddDays(@this);
        }
        /// <summary>
        /// Returns the current datetime + the specifed number of months
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime MonthsFromNow(this int @this)
        {
            return DateTime.Now.AddMonths(@this);
        }
        /// <summary>
        /// Returns the current datetime + the specifed number of years
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime YearsFromNow(this int @this)
        {
            return DateTime.Now.AddYears(@this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime LastYear(this DateTime @this)
        {
            return @this.AddYears(-1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime NextYear(this DateTime @this)
        {
            return @this.AddYears(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime LastMonth(this DateTime @this)
        {
            return @this.AddMonths(-1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime NextMonth(this DateTime @this)
        {
            return @this.AddMonths(1);
        }

        /// <summary>
        /// Returns a boolean value wether the date is within the specifed period. (edges are not calculated within)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="period"></param>
        /// /// <param name="includeEdges"></param>
        /// <returns></returns>
        public static bool IsWithin(this DateTime @this, Period period, bool includeEdges = false)
        {
            return period.IsWrap(@this, includeEdges);
        }
        /// <summary>
        /// returns a boolean value wether the date is NOT within the specifed period. (edges are not calculated within)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="period"></param>
        /// <param name="includeEdges"></param>
        /// <returns></returns>
        public static bool IsNotWithin(this DateTime @this, Period period, bool includeEdges = false)
        {
            return period.IsWrap(@this, includeEdges).Not();
        }
        /// <summary>
        /// Returns end of day 23:59:59;
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime EndOfDay(this DateTime @this)
        {
            return @this.Date.AddDays(1).AddSeconds(-1);
        }
        /// <summary>
        /// Returns start of day 00:00:00
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime StartOfDay(this DateTime @this)
        {
            return @this.Date.Date;
        }
        /// <summary>
        /// Returns tomorrow of the specified day
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime Tomorrow(this DateTime @this)
        {
            return @this.AddDays(1);
        }
        /// <summary>
        /// Returns next day of the specified day
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime NextDay(this DateTime @this)
        {
            return @this.AddDays(1);
        }
        /// <summary>
        /// Returns yesterday of the specified day
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime Yesterday(this DateTime @this)
        {
            return @this.AddDays(-1);
        }
        /// <summary>
        /// Returns yesterday of the specified day
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateTime PreviousDay(this DateTime @this)
        {
            return @this.AddDays(-1);
        }
    }
}

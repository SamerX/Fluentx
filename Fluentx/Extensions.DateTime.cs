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
        /// Parses a unit time to datetime.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(this long unixTime)
        {
            return Fx.EPOCH.AddSeconds(unixTime);
        }
        /// <summary>
        /// Converts to unitx time in seconds
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnixTimeInSeconds(this DateTime date)
        {
            return Convert.ToInt64((date - Fx.EPOCH).TotalSeconds);
        }
        /// <summary>
        /// Converts to unix time in milli seconds.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnixTimeInMilliSeconds(this DateTime date)
        {
            return Convert.ToInt64((date - Fx.EPOCH).TotalSeconds);
        }
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
        /// Create a date time instance in human readable format.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime Of(this int @this, int month, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, month, @this);
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
        /// Returns a boolean value wether the date is within the specifed period. (default : edges are not calculated within)
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
        /// returns a boolean value wether the date is NOT within the specifed period. (default : edges are not calculated within)
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
        /// <summary>
        /// Returns a new datetime instance with the specified Hour(24 based), Minute, Second and MiliSecond
        /// </summary>
        /// <param name="this"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="millisecond"></param>
        /// <returns></returns>
        public static DateTime At(this DateTime @this, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day, hour, minute, second, millisecond);
        }
        /// <summary>
        /// Returns start of the month as datetime.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime StartOfMonth(this int month, int? year = null)
        {
            return new DateTime(year ?? DateTime.Now.Year, month, 1);
        }
        /// <summary>
        /// Returns end of the month as datetime.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this int month, int? year = null)
        {
            var days = DateTime.DaysInMonth(year ?? DateTime.Now.Year, month);
            return new DateTime(year ?? DateTime.Now.Year, month, days);
        }
        /// <summary>
        /// Returns start of the month as datetime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime StartOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1, value.Hour, value.Minute, value.Second, value.Millisecond);
        }
        /// <summary>
        /// Returns end of the month as datetime.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime value)
        {
            var days = DateTime.DaysInMonth(value.Year, value.Month);
            return new DateTime(value.Year, value.Month, days, value.Hour, value.Minute, value.Second, value.Millisecond);
        }
        /// <summary>
        /// Returns last day of the month for the specified datetime value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int LastDayOfMonth(this DateTime value)
        {
            var days = DateTime.DaysInMonth(value.Year, value.Month);
            return days;
        }
        /// <summary>
        /// Returns last day of the month. 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int LastDayOfMonth(this int month, int? year = null)
        {
            var days = DateTime.DaysInMonth(year ?? DateTime.Now.Year, month);
            return days;
        }
        /// <summary>
        /// Returns whether this date time is in a leap year.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLeapYear(this DateTime value)
        {
            return (value.Year % 4 == 0 && (value.Year % 100 != 0 || value.Year % 400 == 0));
        }
        /// <summary>
        ///  Returns whether the specified year is a leap year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool IsLeapYear(this int year)
        {
            return (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        }

        //public static int Age(this DateTime dateTime)
        //{
        //    var today = DateTime.Today;
        //    // Calculate the age.
        //    var age = today.Year - dateTime.Year;
        //    // Go back to the year the person was born in case of a leap year
        //    if (dateTime > today.AddYears(-age)) age--;
        //    return age;
        //}
        /// <summary>
        /// Creates an age instance from the specified datetime instance.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Age Age(this DateTime dateTime)
        {
            var age = new Age();
            DateTime Now = DateTime.Now;
            age.Years = new DateTime(DateTime.Now.Subtract(dateTime).Ticks).Year - 1;
            DateTime PastYearDate = dateTime.AddYears(age.Years);

            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    age.Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    age.Months = i - 1;
                    break;
                }
            }
            age.Days = Now.Subtract(PastYearDate.AddMonths(age.Months)).Days;
            age.Hours = Now.Subtract(PastYearDate).Hours;
            age.Minutes = Now.Subtract(PastYearDate).Minutes;
            age.Seconds = Now.Subtract(PastYearDate).Seconds;
            return age;
        }

    }
}



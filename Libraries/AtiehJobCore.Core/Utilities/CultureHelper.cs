using System;
using System.Globalization;

namespace AtiehJobCore.Core.Utilities
{
    /// <summary>
    /// Culture Extensions
    /// </summary>
    public static class CultureHelper
    {

        public static string GetPersianNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 48; i < 58; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(1728 + i));
            }
            return data;
        }

        public static string GetEnglishNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 1776; i < 1786; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(i - 1728));
            }
            return data;
        }

        public static string GetPersianNumber(this long data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this int data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this decimal data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        public static string GetPersianNumber(this byte data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetPersianNumber();
        }

        /********************************************************************************/
        public static string GetArabicNumber(this string data)
        {
            if (string.IsNullOrEmpty(data)) return string.Empty;
            for (var i = 48; i < 58; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(1584 + i));
            }
            return data;
        }

        public static string GetArabicNumber(this long data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetArabicNumber();
        }

        public static string GetArabicNumber(this int data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetArabicNumber();
        }

        public static string GetArabicNumber(this decimal data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetArabicNumber();
        }

        public static string GetArabicNumber(this byte data)
        {
            return data.ToString(CultureInfo.InvariantCulture).GetArabicNumber();
        }

        public static DateTime ConvertToGregorian(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, new PersianCalendar());
        }

        public static DateTime ConvertToGregorian(int year, int month, int day)
        {
            return new DateTime(year, month, day, new PersianCalendar());
        }
    }
}

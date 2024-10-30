using System;

namespace util
{
    public class DateTimeUtil
    {
        public static string FormatUtcTime(DateTime utcTime, char separator = '_')
        {
            return utcTime.Year + separator +
                   utcTime.Month + separator +
                   utcTime.Day + separator +
                   utcTime.Hour + separator +
                   utcTime.Minute + separator +
                   utcTime.Millisecond.ToString();
        }

        public static long GetTimestampByUtcTime(DateTime utcTime)
        {
            return (long)(utcTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))).TotalSeconds;
        }
    }
}
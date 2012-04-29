using System;

namespace TheRealDeal
{
    public static class DateTimeExtensions
    {
        public static string ToDisplayableGameTime(this DateTime dateTime)
        {
            return dateTime.ToString("MMM dd @ HH:mm");
        }

        public static string ToDisplayableGameTime(this DateTimeOffset dateTime)
        {
            return dateTime.ToString("MMM dd @ HH:mm");
        }

    }
}
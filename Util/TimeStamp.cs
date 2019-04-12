using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _13AShopCart.Util
{
    // We use unix timestamps to track our dates. This class provides some
    // helper methods to perform conversion.
    public class Timestamp
    {
        public static string[] monthNames = { "Jan", "Feb", "Mar",
            "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public static long unixTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public static string dateFromTimestamp(long timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();

            return dateTime.Day + " " + monthNames[dateTime.Month] + " " + dateTime.Year;
        }
    }
}
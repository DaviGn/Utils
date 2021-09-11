using System;

namespace Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToSouthAmericaTimeZone(this DateTime data)
        {
            DateTime dateTimeUTC = data.ToUniversalTime();
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, timeZone);

            return localTime;
        }

        public static string ToTimeStamp(this DateTime data)
        {
            string timeStamp = "Agora";
            if (data.Year != DateTime.Now.Year)
            {
                timeStamp = $"{Math.Abs(DateTime.Now.Year - data.Year)}a";
            }
            else if (data.Month != DateTime.Now.Month)
            {
                timeStamp = $"{Math.Abs(DateTime.Now.Month - data.Month)}m";
            }
            else if (data.Day != DateTime.Now.Day)
            {
                timeStamp = $"{Math.Abs(DateTime.Now.Day - data.Day)}d";
            }
            else if (data.Hour != DateTime.Now.Hour)
            {
                timeStamp = $"{Math.Abs(DateTime.Now.Hour - data.Hour)}h";
            }
            else if (data.Minute != DateTime.Now.Minute)
            {
                timeStamp = $"{Math.Abs(DateTime.Now.Minute - data.Minute)}m";
            }
            else if (data.Second != DateTime.Now.Second)
            {
                timeStamp = $"{Math.Abs(DateTime.Now.Second - data.Second)}s";
            }

            return timeStamp;
        }
    }
}

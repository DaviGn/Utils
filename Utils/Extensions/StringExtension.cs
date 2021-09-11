using System;

namespace Utils.Extensions
{
    public static class StringExtension
    {
        public static string TelefoneSemMascara(this String str)
        {
            return str.Replace(" ", "")
                    .Replace("-", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Trim();
        }

        public static DateTime? ToDateTime(this String str)
        {
            if (DateTime.TryParse(str, out DateTime data))
                return data;

            return null; 
        }

        public static DateTime? ToDateTime(this String str, string time)
        {
            if (!string.IsNullOrWhiteSpace(str) && DateTime.TryParse($"{str} {time}", out DateTime data))
                return data;

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Utils
{
    public static class DateTimeUtils
    {
        private const string DateFormat = "dd/MM/YYYY";
        private const string TimeFormat = "HH:mm";

        public static string ToTime(this DateTime dateTime)
            => dateTime.ToString(TimeFormat);

        public static string ToBrazilDateFormat(this DateTime dateTime)
            => dateTime.ToString(DateFormat);

        public static string ToBrazilDateTimeFormat(this DateTime dateTime)
            => dateTime.ToString($"{DateFormat} {TimeFormat}");
        
        public static DateTime ToSouthAmericaTimeZone(this DateTime data)
        {
            DateTime dateTimeUTC = data.ToUniversalTime();
            TimeZoneInfo timeZone;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                timeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
            else
                throw new Exception("Time zone not supported");
            
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, timeZone);

           return localTime;
        }
        
        public static IEnumerable<DateTime> GerarListaDeDatas(DateTime dataInicio, DateTime dataFim, IEnumerable<DayOfWeek> diasIgnorar = null)
        {
            while (dataInicio <= dataFim)
            {
                if (diasIgnorar is null || !diasIgnorar.Contains(dataInicio.DayOfWeek))
                    yield return dataInicio;

                dataInicio = dataInicio.AddDays(1);
            }
        }

        public static IEnumerable<(DateTime, DateTime)> MontarListaDatasPartidaEChegada(IEnumerable<DateTime> datasDePartida, DateTime horaInicio, DateTime horaTermino)
        {
            foreach (var dataPartida in datasDePartida)
            {
                var dataHoraPartida = Convert.ToDateTime(dataPartida.ToString("dd/MM/yyyy") + " " + horaInicio.ToShortTimeString());
                var dataHoraChegada = Convert.ToDateTime(dataPartida.ToString("dd/MM/yyyy") + " " + horaTermino.ToShortTimeString());

                yield return (dataHoraPartida, dataHoraChegada);
            };
        }
    }
}

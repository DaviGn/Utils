using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class DateTimeUtils
    {
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

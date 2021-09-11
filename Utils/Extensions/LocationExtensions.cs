using System;
using Utils.Domain.Interfaces;

namespace Utils.Extensions
{
    public static class LocationExtensions
    {
        public static double DegreeToRadian(this double input)
            => input * Math.PI / 180;

        public static double DegreeToRadian(this decimal input)
            => DegreeToRadian(Convert.ToDouble(input));

        public static double CalcularDistanciaEntrePontos(this ILocation pontoA, ILocation pontoB)
        {
            //return Math.Sqrt(Math.Pow(latitude1 - latitude2, 2) + Math.Pow(longitude1 - longitude2, 2));

            //Calculo de distância de Havernise entre dois pontos LatLng
            //https://www.movable-type.co.uk/scripts/latlong.html
            var R = 6371e3;
            var Phi1 = pontoA.Latitude.DegreeToRadian();
            var Phi2 = pontoB.Latitude.DegreeToRadian();
            var DeltaPhi = (pontoB.Latitude - pontoA.Latitude).DegreeToRadian();
            var DeltaLambda = (pontoB.Longitude - pontoA.Longitude).DegreeToRadian();

            var a = Math.Sin(DeltaPhi / 2) * Math.Sin(DeltaPhi / 2) +
                    Math.Cos(Phi1) * Math.Cos(Phi2) *
                    Math.Sin(DeltaLambda / 2) * Math.Sin(DeltaLambda / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }
    }
}

using Android.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTourism
{
    public class Utils
    {
        /// <summary>
        /// Décodeur pour le champs "polyline" du fichier JSON
        /// </summary>
        /// <param name="encodedPoints"></param>
        /// <returns></returns>
        public static List<Location> DecodePolylinePoints(string encodedPoints)
        {
            if (encodedPoints == null || encodedPoints == "") return null;
            List<Location> poly = new List<Location>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;
            int currentLat = 0;
            int currentLng = 0;
            int next5bits, sum, shifter;
            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);
                    if (index >= polylinechars.Length)
                        break;
                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);
                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;
                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    var p = new Location("loc");
                    p.Latitude = Convert.ToDouble(currentLat) / 100000.0;
                    p.Longitude = Convert.ToDouble(currentLng) / 100000.0;
                    poly.Add(p);
                }
            }
            catch (Exception ex)
            {
                //Exception
            }
            return poly;
        }

        /// <summary>
        /// Fonctions pour calculer la distance a vol d'oiseau entre 2 points
        /// </summary>
        /// <param name="userPoint"></param>
        /// <param name="dbPoint"></param>
        /// <returns></returns>
        public static double calculDistance(Location userPoint, Location dbPoint)
        {
            var earthRadius = 6378137;
            var lat_a = convertRad(userPoint.Latitude);
            var long_a = convertRad(userPoint.Longitude);
            var lat_b = convertRad(dbPoint.Latitude);
            var long_b = convertRad(dbPoint.Longitude);

            return earthRadius * (Math.PI / 2 - Math.Asin(Math.Sin(lat_b) * Math.Sin(lat_a) + Math.Cos(long_b - long_a) * Math.Cos(lat_b) * Math.Cos(lat_a)));
        }

        /// <summary>
        /// Convertisseur de degrès en radians
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double convertRad(double input) { return (Math.PI * input) / 180; }
    }

}


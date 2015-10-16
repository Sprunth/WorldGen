using System;

namespace Noise
{
    public class LatitudeLongitude
    {
        

        static void LatLonToXYZ(double lat, double lon, out double x, out double y, out double z)
        {
            double r = Math.Cos(MathsUtils.DEG_TO_RAD * lat);
            x = r * Math.Cos(MathsUtils.DEG_TO_RAD * lon);
            y = Math.Sin(MathsUtils.DEG_TO_RAD * lat);
            z = r * Math.Sin(MathsUtils.DEG_TO_RAD * lon);
        }
    }
}

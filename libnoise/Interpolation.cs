
namespace Noise
{
    /// <summary>
    /// Provides standard interpolaton methods used by the rest of the 
    /// library
    /// </summary>
    public static class Interpolation
    {
        /// <summary>
        /// Performs cubic interpolation between two values bound between two other values
        /// </summary>
        /// <param name="n0">The value before the first value</param>
        /// <param name="n1">The first value</param>
        /// <param name="n2">The second value</param>
        /// <param name="n3">The value after the second value</param>
        /// <param name="a">The alpha value</param>
        /// <returns>The interpolated value</returns>
        /// <remarks>
        /// The alpha value should range from 0.0 to 1.0.  If the alpha value is
        /// 0.0, this function returns n1.  If the alpha value is 1.0, this
        /// function returns n2.
        /// </remarks>
        public static double CubicInterpolate(double n0, double n1, double n2, double n3, double a)
        {
            double p = (n3 - n2) - (n0 - n1);
            double q = (n0 - n1) - p;
            double r = n2 - n0;
            double s = n1;
            return p * a * a * a + q * a * a + r * a + s;
        }

        /// <summary>
        /// Performs linear interpolation between two values.
        /// </summary>
        /// <param name="n0">The first value</param>
        /// <param name="n1">The second value</param>
        /// <param name="a">The alpha value</param>
        /// <returns>The interpolted value</returns>
        /// <remarks>
        /// The alpha value should range from 0.0 to 1.0.  If the alpha value is
        /// 0.0, this function returns @a n0.  If the alpha value is 1.0, this
        /// function returns @a n1.
        /// </remarks>
        public static double LinearInterpolate(double n0, double n1, double a)
        {
            return ((1.0 - a) * n0) + (a * n1);
        }

        /// <summary>
        /// Maps a value onto a cubic S-curve.
        /// </summary>
        /// <param name="a">The value to map onto a cubic S-curve.</param>
        /// <returns>The mapped value.</returns>
        /// <remarks>
        /// a should range from 0.0 to 1.0.
        ///
        /// The derivitive of a cubic S-curve is zero at a = 0.0 and a =
        /// 1.0
        /// </remarks>
        public static double SCurve3(double a)
        {
            return (a * a * (3.0 - 2.0 * a));
        }

        /// <summary>
        /// Maps a value onto a quintic S-curve.
        /// </summary>
        /// <param name="a">The value to map onto a quintic S-curve.</param>
        /// <returns>The mapped value.</returns>
        /// <remarks>
        /// a should range from 0.0 to 1.0.
        ///
        /// The first derivitive of a quintic S-curve is zero at a = 0.0 and
        /// a = 1.0
        ///
        /// The second derivitive of a quintic S-curve is zero at a = 0.0 and
        /// a = 1.0
        /// </remarks>
        public static double SCurve5(double a)
        {
            double a3 = a * a * a;
            double a4 = a3 * a;
            double a5 = a4 * a;
            return (6.0 * a5) - (15.0 * a4) + (10.0 * a3);
        }        
    }
}

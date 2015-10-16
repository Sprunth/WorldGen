using System;


namespace Noise
{
    public class MathsUtils
    {
        public static double FMod(double x, double y)
        {
            double z = Math.IEEERemainder(x, y);
            if (z < 0) z += y;
            return z;
        }

        public static int Clamp(int input, int min, int max) 
        {            
            if (input < min) return min;
            if (input > max) return max;
            return input;
        }

        public static readonly double DEG_TO_RAD = Math.PI / 180.0;
        public static readonly double SQRT_3 = 1.7320508075688772935;
    }
}

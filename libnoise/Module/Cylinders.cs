using System;

namespace Noise.Modules
{
    public class Cylinders : Module
    {
        static readonly double DefaultFrequency = 1.0;

        public Cylinders() : this(DefaultFrequency) { }

        public Cylinders(double frequency)
        {
            Frequency = frequency;
        }

        public override double GetValue(double x, double y, double z)
        {
            x *= Frequency;
            z *= Frequency;

            double distFromCenter = Math.Sqrt(x * x + z * z);
            double distFromSmallerSphere = distFromCenter - Math.Floor(distFromCenter);
            double distFromLargerSphere = 1.0 - distFromSmallerSphere;
            double nearestDist = Math.Min(distFromSmallerSphere, distFromLargerSphere);
            return 1.0 - (nearestDist * 4.0); // Puts it in the -1.0 to +1.0 range. 
        }

        public override int ModuleCount
        {
            get { return 0; }
        }

        public double Frequency { get; set; }
    }
}

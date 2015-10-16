using System;

namespace Noise.Modules
{
    public class Voronoi : Module
    {
        public Voronoi() : this(DefaultDisplacement, DefaultFrequency, false, DefaultSeed) { }

        public Voronoi(double displacement, double frequency, bool enableDistance, int seed)
        {
            Displacement = displacement;
            Frequency = frequency;
            EnableDistance = enableDistance;
            Seed = seed;
        }

        public override double GetValue(double x, double y, double z)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            int xInt = x > 0.0 ? (int)x : (int)x - 1;
            int yInt = y > 0.0 ? (int)y : (int)y - 1;
            int zInt = z > 0.0 ? (int)z : (int)z - 1;

            double minDist = 2147483647.0;
            double xCandidate = 0;
            double yCandidate = 0;
            double zCandidate = 0;

            for (int zCur = zInt - 2; zCur <= zInt + 2; ++zCur)
            {
                for (int yCur = yInt - 2; yCur <= yInt + 2; ++yCur)
                {
                    for (int xCur = xInt - 2; xCur <= xInt + 2; ++xCur)
                    {
                        double xPos = xCur + NoiseGeneration.ValueNoise3D(xCur, yCur, zCur, Seed);
                        double yPos = yCur + NoiseGeneration.ValueNoise3D(xCur, yCur, zCur, Seed+1);
                        double zPos = zCur + NoiseGeneration.ValueNoise3D(xCur, yCur, zCur, Seed+2);
                        double xDist = xPos - x;
                        double yDist = yPos - y;
                        double zDist = zPos - z;
                        double dist = xDist * xDist + yDist * yDist + zDist * zDist;

                        if (dist < minDist)
                        {
                            minDist = dist;
                            xCandidate = xPos;
                            yCandidate = yPos;
                            zCandidate = zPos;
                        }

                    }
                }
            }

            double value;
            if (EnableDistance)
            {
                double xDist = xCandidate - x;
                double yDist = yCandidate - y;
                double zDist = zCandidate - z;

                value = Math.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist) *MathsUtils.SQRT_3 - 1.0;               
            }
            else
            {
                value = 0.0;
            }

            double result = value + Displacement * (double)NoiseGeneration.ValueNoise3D(floor(xCandidate), floor(yCandidate), floor(zCandidate),0);
            return result;
        }

        public override int ModuleCount
        {
            get { return 0; }
        }

        int floor(double input)
        {
            return input > 0.0 ? (int)input : (int)input - 1;
        }

        public bool EnableDistance { get; set; }
        public double Displacement { get; set; }
        public double Frequency { get; set; }
        public int Seed { get; set; }

        static readonly double DefaultDisplacement = 1.0;
        static readonly double DefaultFrequency = 1.0;
        static readonly int DefaultSeed = 0;
    }
}

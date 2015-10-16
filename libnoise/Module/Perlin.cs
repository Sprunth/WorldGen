using System;

namespace Noise.Modules
{
    public class Perlin : Module
    {
        #region ~ Contruction ~

        public Perlin() : this(DefaultFrequency, DefaultLacunarity, DefaultQuality, DefaultOctaveCount, DefaultPersistence, DefaultSeed) { }

        public Perlin(double frequency, double lacunarity, NoiseQuality quality, uint octaveCount, double persistence, int seed)
        {
            Frequency = frequency;
            Lacunarity = lacunarity;
            Quality = quality;
            OctaveCount = octaveCount;
            Persistence = persistence;
            Seed = seed;
        }

        #endregion

        #region ~ Module Implementation ~
        public override double GetValue(double x, double y, double z)
        {
            double value = 0.0;
            double signal = 0.0;
            double curPersistence = 1.0;
            double nx, ny, nz;
            int seed;

            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            for (int curOctave = 0; curOctave < _ocatves; curOctave++)
            {

                // Make sure that these floating-point values have the same range as a 32-
                // bit integer so that we can pass them to the coherent-noise functions.
                nx = NoiseGeneration.MakeInt32Range(x);
                ny = NoiseGeneration.MakeInt32Range(y);
                nz = NoiseGeneration.MakeInt32Range(z);

                // Get the coherent-noise value from the input value and add it to the
                // final result.
                seed = (int)((Seed + curOctave) & 0xffffffff);
                signal = NoiseGeneration.GradientCoherentNoise3D(nx, ny, nz, seed, Quality);
                value += signal * curPersistence;

                // Prepare the next octave.
                x *= Lacunarity;
                y *= Lacunarity;
                z *= Lacunarity;
                curPersistence *= Persistence;
            }

            return value;
        }

        public override int ModuleCount
        {
            get { return 0; }
        }
        #endregion

        #region ~ Properties ~

        public double Frequency { get; set; }
        public double Lacunarity { get; set; }
        public double Persistence { get; set; }
        public NoiseQuality Quality { get; set; }
        public int Seed { get; set; }

        uint _ocatves;
        public uint OctaveCount
        {
            get
            {
                return _ocatves;
            }
            set
            {
                if (value > MaximumOctaves)
                {
                    throw new ArgumentException("Value: " + value.ToString() + " is higher than MaxOctaves: " + MaximumOctaves.ToString());
                }

                _ocatves = value;
            }
        }

        #endregion

        #region ~ Default Settings ~

        readonly static double DefaultFrequency = 1.0;
        readonly static double DefaultLacunarity = 2.0;
        readonly static uint DefaultOctaveCount = 6;
        readonly static double DefaultPersistence = 0.5;
        readonly static NoiseQuality DefaultQuality = NoiseQuality.Standard;
        readonly static int DefaultSeed = 0;

        readonly static uint MaximumOctaves = 30;

        #endregion
    }
}

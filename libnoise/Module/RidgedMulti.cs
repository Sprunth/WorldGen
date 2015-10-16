using System;

namespace Noise.Modules
{
    public class RidgedMulti : Module
    {
        public RidgedMulti() : this(DefaultFrequency, DefaultLacunarity, DefaultOctaveCount, DefaultQuality, DefaultSeed) { }

        public RidgedMulti(double frequency, double lacunaruity, int octaves, NoiseQuality quality, int seed)
        {
            Frequency = frequency;
            OctaveCount = octaves;
            Quality = quality;
            Seed = seed;
            Lacunaruty = lacunaruity;
        }

		#region ~ Methods ~ 


		// Public Methods 

        public override double GetValue(double x, double y, double z)
        {
            x *= Frequency;
            y *= Frequency;
            z *= Frequency;

            double signal = 0.0;
            double value = 0.0;
            double weight = 1.0;

            double offset = 1.0;
            double gain = 2.0;

            for (int currentOctave = 0; currentOctave < _octaveCount; ++currentOctave)
            {
                double nx = NoiseGeneration.MakeInt32Range(x);
                double ny = NoiseGeneration.MakeInt32Range(y);
                double nz = NoiseGeneration.MakeInt32Range(z);

                int seed = (Seed + currentOctave) & 0x7fffffff;
                signal = NoiseGeneration.GradientCoherentNoise3D(nx, ny, nz, seed, Quality);

                signal = Math.Abs(signal);
                signal = offset - signal;
                signal *= signal;
                signal *= weight;

                weight = signal * gain;
                if (weight > 1.0) weight = 1.0;
                if (weight < 0.0) weight = 0.0;

                value += signal * _spectralWeights[currentOctave];

                x *= _lacunarity;
                y *= _lacunarity;
                z *= _lacunarity;
            }

            return (value * 1.25) - 1.0;
        }



        // Private Methods 

        private void CalculateSpectralRates()
        {
            double h = 1.0;
            double frequency = 1.0;
            for (int i = 0; i < MaxOctaves; ++i)
            {
                _spectralWeights[i] = Math.Pow(frequency, -h);
                frequency *= _lacunarity;
            }
        }


		#endregion ~ Methods ~ 

		#region ~ Properties ~ 

        public double Frequency { get; set; }

        public double Lacunaruty
        {
            get
            {
                return _lacunarity;
            }
            set
            {
                _lacunarity = value;
                CalculateSpectralRates();
            }
        }

        public override int ModuleCount
        {
            get { return 0; }
        }

        public int OctaveCount
        {
            get
            {
                return _octaveCount;
            }
            set
            {
                if (value > MaxOctaves) throw new ArgumentException("Too many octaves");

                _octaveCount = value;
            }
        }

        public NoiseQuality Quality { get; set; }

        public int Seed { get; set; }

		#endregion ~ Properties ~ 

		#region ~ Fields ~ 

        double _lacunarity;
        int _octaveCount;
        double[] _spectralWeights = new double[MaxOctaves];
        static readonly double DefaultFrequency = 1.0;
        static readonly double DefaultLacunarity = 2.0;
        static readonly int DefaultOctaveCount = 6;
        static readonly NoiseQuality DefaultQuality = NoiseQuality.Standard;
        static readonly int DefaultSeed = 0;
        static readonly int MaxOctaves = 30;

		#endregion ~ Fields ~ 

    }
}

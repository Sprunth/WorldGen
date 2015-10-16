using System.Diagnostics;

namespace Noise.Modules
{
    public class Turbulence : Module
    {
        #region ~ Construction ~

        public Turbulence() : this(DefaultFrequency, DefaultPower, DefaultRoughness, DefaultSeed) { }

        public Turbulence(double frequency, double power, uint roughness, int seed)
        {
            Frequency = frequency;
            Power = power;
            Roughness = roughness;
            Seed = seed;
        }

        #endregion

        #region ~ Methods ~
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            double x0, y0, z0;
            double x1, y1, z1;
            double x2, y2, z2;
            x0 = x + (12414.0 / 65536.0);
            y0 = y + (65124.0 / 65536.0);
            z0 = z + (31337.0 / 65536.0);
            x1 = x + (26519.0 / 65536.0);
            y1 = y + (18128.0 / 65536.0);
            z1 = z + (60493.0 / 65536.0);
            x2 = x + (53820.0 / 65536.0);
            y2 = y + (11213.0 / 65536.0);
            z2 = z + (44845.0 / 65536.0);
            double xDistort = x + (_xDistort.GetValue(x0, y0, z0)
              * Power);
            double yDistort = y + (_yDistort.GetValue(x1, y1, z1)
              * Power);
            double zDistort = z + (_zDistort.GetValue(x2, y2, z2)
              * Power);

            return _modules[0].GetValue(xDistort, yDistort, zDistort);
        }
        #endregion

        #region ~ Properties ~
        public override int ModuleCount
        {
            get { return 1; }
        }

        public double Frequency
        {
            get
            {
                return _xDistort.Frequency;
            }
            set
            {
                _xDistort.Frequency = _yDistort.Frequency = _zDistort.Frequency = value;
            }
        }
        public double Power { get; set; }
        public uint Roughness
        {
            get
            {
                return _xDistort.OctaveCount;
            }
            set
            {
                _xDistort.OctaveCount = _yDistort.OctaveCount = _zDistort.OctaveCount = value;
            }
        }
        public int Seed
        {
            get
            {
                return _xDistort.Seed;
            }
            set
            {
                _xDistort.Seed = _yDistort.Seed = _zDistort.Seed = value;
            }
        }

        #endregion

        #region ~ Fields ~

        Perlin _xDistort = new Perlin();
        Perlin _yDistort = new Perlin();
        Perlin _zDistort = new Perlin();

        #endregion

        #region ~ Defaults ~

        static readonly double DefaultFrequency = 1.0;
        static readonly double DefaultPower = 1.0;
        static readonly uint DefaultRoughness = 3;
        static readonly int DefaultSeed = 0;

        #endregion
    }
}

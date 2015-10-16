using System.Diagnostics;

namespace Noise.Modules
{
    public class Clamp : Module
    {
        #region ~ Construction ~

        public Clamp() : this(DefaultLowerBound,DefaultUpperBound)
        {
        }

        public Clamp(double lower, double upper)
        {
            LowerBound = lower;
            UpperBound = upper;
        }

        #endregion

        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            double value = _modules[0].GetValue(x, y, z);

            if (value < LowerBound) return LowerBound;
            if (value > UpperBound) return UpperBound;
            return value;
        }

        public override int ModuleCount
        {
            get { return 1; }
        }

        #region ~ Properties ~

        public double LowerBound { get; set; }
        public double UpperBound { get; set; }

        #endregion

        #region ~ Defaults ~

        readonly static double DefaultLowerBound = -1.0;
        readonly static double DefaultUpperBound = 1.0;

        #endregion
    }
}

using System.Diagnostics;

namespace Noise.Modules
{
    public class ScaleBias : Module
    {
        #region ~ Construction ~

        public ScaleBias() : this(DefaultBias, DefaultScale) { }

        public ScaleBias(double bias, double scale)
        {
            Bias = bias;
            Scale = scale;
        }

        #endregion

        #region ~ Methods ~

        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            return _modules[0].GetValue(x, y, z) * Scale + Bias;
        }

        #endregion

        #region ~ Properties ~

        public override int ModuleCount { get { return 1; } }

        public double Bias { get; set; }

        public double Scale { get; set; }

        #endregion

        #region ~ Defaults ~

        static readonly double DefaultBias = 0.0;
        static readonly double DefaultScale = 1.0;

        #endregion
    }
}

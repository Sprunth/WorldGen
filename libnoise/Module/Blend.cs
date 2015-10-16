using System.Diagnostics;

namespace Noise.Modules
{
    public class Blend : Module
    {
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);
            Debug.Assert(_modules[1] != null);
            Debug.Assert(_modules[2] != null);

            double n1 = _modules[0].GetValue(x, y, z);
            double n2 = _modules[1].GetValue(x, y, z);
            double a = (_modules[2].GetValue(x, y, z) + 1.0) / 2.0;
            return Interpolation.LinearInterpolate(n1, n2, a);
        }

        public override int ModuleCount
        {
            get { return 3; }
        }
    }
}

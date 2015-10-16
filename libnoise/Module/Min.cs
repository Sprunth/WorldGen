using System;
using System.Diagnostics;

namespace Noise.Modules
{
    public class Min : Module
    {
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);
            Debug.Assert(_modules[1] != null);

            double value0 = _modules[0].GetValue(x, y, z);
            double value1 = _modules[1].GetValue(x, y, z);

            return Math.Min(value0, value1);
        }

        public override int ModuleCount
        {
            get { return 2; }
        }
    }
}

using System;
using System.Diagnostics;

namespace Noise.Modules
{
    public class Power : Module
    {
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);
            Debug.Assert(_modules[1] != null);

            return Math.Pow(_modules[0].GetValue(x, y, z), _modules[1].GetValue(x, y, z));
        }

        public override int ModuleCount
        {
            get { return 2; }
        }
    }
}

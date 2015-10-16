using System.Diagnostics;

namespace Noise.Modules
{
    public class Invert : Module
    {
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            return -_modules[0].GetValue(x, y, z);
        }

        public override int ModuleCount
        {
            get { return 1; }
        }
    }
}

using System;

namespace Noise.Modules
{
    public class CheckerBoard : Module
    {
        public override double GetValue(double x, double y, double z)
        {
            int ix = (int)(Math.Floor(NoiseGeneration.MakeInt32Range(x)));
            int iy = (int)(Math.Floor(NoiseGeneration.MakeInt32Range(y)));
            int iz = (int)(Math.Floor(NoiseGeneration.MakeInt32Range(z)));
            return (ix & 1 ^ iy & 1 ^ iz & 1) == 1 ? -1.0 : 1.0;
        }

        public override int ModuleCount
        {
            get { return 0; }
        }
    }
}

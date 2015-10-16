using System.Diagnostics;
using Noise.Modules;

namespace Noise.Model
{
    public class Plane
    {
        public Plane(Module source)
        {
            _source = source;
        }

        public double GetValue(double x, double z)
        {
            Debug.Assert(_source != null, "Plane source module is null");
            return _source.GetValue(x, 0.0, z);
        }

        Module _source;
    }
}

using System.Diagnostics;

namespace Noise.Modules
{
    public class Cache : Module
    {
        public override double GetValue(double x, double y, double z)
        {
            Debug.Assert(_modules[0] != null);

            if (!(_isCached && x == _xCache && y == _yCache && z == _zCache))
            {
                _cachedValue = _modules[0].GetValue(x, y, z);
                _xCache = x;
                _yCache = y;
                _zCache = z;
            }
            _isCached = true;
            return _cachedValue;
        }

        public override int ModuleCount
        {
            get { return 1; }
        }

        double _cachedValue;
        bool _isCached;
        double _xCache;
        double _yCache;
        double _zCache;

    }
}

using Noise.Model;
using Noise.Modules;

namespace Noise.Utils
{
    public class PlanarNoiseMapBuilder : NoiseMapBuilder
    {
        public PlanarNoiseMapBuilder(uint destinationWidth,
            uint destinationHeight,
            double borderValue,
            Module sourceModule,
            double xLowerBound,
            double xUpperBound,
            double zLowerBound,
            double zUpperBound,
            bool seamless) :
            base(destinationWidth, destinationHeight, borderValue, sourceModule)
        {
            _xLowerBound = xLowerBound;
            _xUpperBound = xUpperBound;

            _zLowerBound = zLowerBound;
            _zUpperBound = zUpperBound;

            _seamless = seamless;
        }

        public override NoiseMap Build()
        {
            NoiseMap map = new NoiseMap();
            map.SetSize(_destinationWidth, _destinationHeight);

            Plane plane = new Plane(_source);

            double xExtent = _xUpperBound - _xLowerBound;
            double zExtent = _zUpperBound - _zLowerBound;
            double xDelta = xExtent / (double)_destinationWidth;
            double zDelta = zExtent / (double)_destinationHeight;
            double xCur = _xLowerBound;
            double zCur = _zLowerBound;

            for (uint z = 0; z < _destinationHeight; z++)
            {
                xCur = _xLowerBound;
                for (uint x = 0; x < _destinationWidth; x++)
                {
                    double finalValue;
                    if (!_seamless)
                    {
                        finalValue = plane.GetValue(xCur, zCur);
                    }
                    else
                    {
                        double swValue, seValue, nwValue, neValue;
                        swValue = plane.GetValue(xCur, zCur);
                        seValue = plane.GetValue(xCur + xExtent, zCur);
                        nwValue = plane.GetValue(xCur, zCur + zExtent);
                        neValue = plane.GetValue(xCur + xExtent, zCur + zExtent);
                        double xBlend = 1.0 - ((xCur - _xLowerBound) / xExtent);
                        double zBlend = 1.0 - ((zCur - _zLowerBound) / zExtent);
                        double z0 = Interpolation.LinearInterpolate(swValue, seValue, xBlend);
                        double z1 = Interpolation.LinearInterpolate(nwValue, neValue, xBlend);
                        finalValue = Interpolation.LinearInterpolate(z0, z1, zBlend);
                    }

                    map.SetPoint(x, z, finalValue);

                    xCur += xDelta;
                }
                zCur += zDelta;
            }

            return map;
        }

        double _xLowerBound;
        double _xUpperBound;

        double _zLowerBound;
        double _zUpperBound;

        bool _seamless;
    }
}

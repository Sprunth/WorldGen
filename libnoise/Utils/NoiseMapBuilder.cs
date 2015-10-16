using Noise.Modules;

namespace Noise.Utils
{
    public abstract class NoiseMapBuilder
    {
        public NoiseMapBuilder(uint destinationWidth, uint destinationHeight, double borderValue, Module sourceModule)
        {
            _destinationWidth = destinationWidth;
            _destinationHeight = destinationHeight;
            _borderValue = borderValue;
            _source = sourceModule;
        }

        public abstract NoiseMap Build();

        protected uint _destinationWidth;
        protected uint _destinationHeight;
        protected double _borderValue;
        protected Module _source;
    }
}

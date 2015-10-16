
namespace Noise.Utils
{
    public class NoiseMap
    {
        public double GetValue(uint x, uint y)
        {
            if (x < _width && y < _height)
            {
                return _values[x, y];
            }
            else
            {
                return _borderValue;
            }
        }

        public void SetPoint(uint x, uint y, double value)
        {
            if (x < _width && y < _height)
            {
                _values[x, y] = value;
            }
        }

        public void SetSize( uint width , uint height )
        {
            _width = width;
            _height = height;

            _values = new double[_width,_height];
        }

        public uint Height
        {
            get
            {
                return _height;
            }
        }

        public uint Width
        {
            get
            {
                return _width;
            }
        }

        public double BorderValue
        {
            get { return _borderValue; }
            set { _borderValue = value; }
        }

        private double _borderValue = 0;
        private uint _height = 100;
        private uint _width = 100;
        public double[,] _values;
    }
}

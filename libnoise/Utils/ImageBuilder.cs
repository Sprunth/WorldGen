using System.Drawing;
using System.Drawing.Imaging;

namespace Noise.Utils
{
    public class ImageBuilder
    {
        public ImageBuilder()
        {
        }

        public ImageBuilder(NoiseMap map, GradientColour colours)
        {
            _map = map;
            _colour = colours;
        }

        public Image Render()
        {
            Bitmap bitmap = new Bitmap((int)_map.Width, (int)_map.Height, PixelFormat.Format32bppArgb);

            for (int x = 0; x < _map.Width; x++)
            {
                for (int z = 0; z < _map.Height; z++)
                {
                    double value = _map.GetValue((uint)x,(uint)z);
                    Color color = _colour.GetColour(value);
                    bitmap.SetPixel(x, z, color);
                }
            }

            return bitmap;
        }

        public NoiseMap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        public GradientColour Colours
        {
            get
            {
                return _colour;
            }
            set
            {
                _colour = value;
            }
        }

        NoiseMap _map;
        GradientColour _colour;
    }
}

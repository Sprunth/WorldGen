using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Noise;
using Noise.Model;
using Noise.Modules;
using Noise.Utils;
using System.Drawing;

namespace WorldGen_libtcod
{
    class NoiseManager
    {
        static Perlin perlin = new Perlin();
        static NoiseMap heightMap = new NoiseMap();
        static PlanarNoiseMapBuilder heightMapBuilder;
        static ImageBuilder imgBuilder;
        static Image finalImage;
        static double xLower = 0, yLower = 0;
        public static double XLower { get { return xLower; } set { xLower = value; } }
        public static double YLower { get { return yLower; } set { yLower = value; } }
        static int dist = 6;


        /// <summary>
        /// This function generates a heightmap and returns it as a System.Drawing.Image
        /// </summary>
        /// <param name="imageSize">The size of the image. Square sizes work best</param>
        /// <param name="borderSize"></param>
        /// <param name="octaveCount">The higher the octave, the 'busier' it is. Values 1-6 work well.</param>
        /// <param name="frequency">Increasing frequencies adds detail but also makes the features smaller. 0.5 to 5 work well.</param>
        /// <param name="persistence">Increasing persistence adds roughness. 0.25 to 0.75 work well.</param>
        /// <returns></returns>
        public static Image GeneratePerlinImage(int imageSize, double borderSize = 2, int octaveCount = 4, double frequency = 2, double persistence = 0.4)
        {
            //Console.WriteLine("Press Enter to Start Perlin Generator");
            //Console.ReadLine();
            Console.WriteLine("Generating Perlin Heightmap...");

            //seamless so bumpmap looks good
            heightMapBuilder = new PlanarNoiseMapBuilder((uint)imageSize, (uint)imageSize, borderSize, perlin, xLower, xLower+dist, yLower, yLower+dist, true);
            heightMap = heightMapBuilder.Build();

            GradientColour colors = new GradientColour();
            colors.AddGradientPoint(-1.0F, Color.Black);
            colors.AddGradientPoint(0, Color.Gray);
            colors.AddGradientPoint(1.0F, Color.White);

            perlin.OctaveCount = (uint)octaveCount;
            perlin.Frequency = frequency;
            perlin.Persistence = persistence;

            imgBuilder = new ImageBuilder(heightMap, colors);

            finalImage = imgBuilder.Render();

            Console.WriteLine("Perlin Heightmap Generated");

            return finalImage;
        }
    }
}

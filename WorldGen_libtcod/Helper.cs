using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using libtcod;

namespace WorldGen_libtcod
{
    class Helper
    {
        public static TCODImage ImageToTCODImage(Image img)
        {
            TCODImage toReturn = new TCODImage(img.Width, img.Height);
            Bitmap imageToProcess = new Bitmap(img);
            //Console.WriteLine(imageToProcess.Width + "|" + imageToProcess.Height);
            for (int i = 0; i < imageToProcess.Width; i++)
            {
                for (int j = 0; j < imageToProcess.Height; j++)
                {
                    Color c = imageToProcess.GetPixel(i,j);
                    toReturn.putPixel(i, j, new TCODColor(c.R, c.G, c.B));
                }
            }
            return toReturn;
        }
    }
}

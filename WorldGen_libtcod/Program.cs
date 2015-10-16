using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using libtcod;
using System.Diagnostics;

namespace WorldGen_libtcod
{
    class Program
    {
        private static TCODConsole root;
        private static TCODImage img;
        private static int imageSize = 64;

        private static Stopwatch stopwatch;
        private static Random random;

        static void Main(string[] args)
        {
            Initialize();
            GameLoop();
        }

        private static void Initialize()
        {
            NoiseManager.GeneratePerlinImage(imageSize).Save("heightmap.png",System.Drawing.Imaging.ImageFormat.Png);
            img = new TCODImage("heightmap.png");

            TCODConsole.initRoot(80, 80, "MapGen", false, TCODRendererType.OpenGL);
            root = TCODConsole.root;
            TCODConsole.setCustomFont("Data/terminal12x12_gs_ro.png", (int)TCODFontFlags.LayoutAsciiInRow,16,16);
            TCODSystem.setFps(60);

            random = new Random();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private static void GameLoop()
        {
            while (!TCODConsole.isWindowClosed())
            {
                Update();
                Draw();
            }
        }

        private static void Update()
        {
            TCODKey key = TCODConsole.waitForKeypress(false);
            double move = 0.1;
            switch (key.KeyCode)
            {
                case TCODKeyCode.Left: 
                    { 
                        NoiseManager.XLower -= move;
                        img = Helper.ImageToTCODImage(NoiseManager.GeneratePerlinImage(imageSize));
                        break; 
                    }
                case TCODKeyCode.Right: 
                    { 
                        NoiseManager.XLower += move;
                        img = Helper.ImageToTCODImage(NoiseManager.GeneratePerlinImage(imageSize));
                        break; 
                    }
                case TCODKeyCode.Up: 
                    { 
                        NoiseManager.YLower -= move;
                        img = Helper.ImageToTCODImage(NoiseManager.GeneratePerlinImage(imageSize));
                        break;
                    }
                case TCODKeyCode.Down: 
                    {
                        NoiseManager.YLower += move;
                        img = Helper.ImageToTCODImage(NoiseManager.GeneratePerlinImage(imageSize));
                        break; 
                    }
            }

            TCODConsole.setWindowTitle(stopwatch.ElapsedMilliseconds.ToString());
            /*
            if (stopwatch.ElapsedMilliseconds >100)
            {
                img = Helper.ImageToTCODImage(NoiseManager.GeneratePerlinImage(imageSize));
                stopwatch.Restart();
            }
            */
        }

        private static void Draw()
        {
            root.setForegroundColor(TCODColor.grey);

            int width, height;
            img.getSize(out width,out height);

            int displaceX = 8;
            int displaceY = 8;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    TCODColor color = img.getPixel(i,j);
                    root.setForegroundColor(color);
                    root.print(i + displaceX, j + displaceY, ((char)random.Next(65,90)).ToString());
                }
            }

            //img.blitRect(root, 20, 20, imageSize / 2, imageSize / 2, TCODBackgroundFlag.Set);
            TCODConsole.flush();
        }
    }
}

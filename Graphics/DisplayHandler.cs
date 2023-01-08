using System;
using System.Drawing;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using IL2CPU.API.Attribs;

namespace CosmosOperatingSystem.Graphics
{
    internal class DisplayHandler
    {
        public static Canvas canvas;
        public static int[] SCREEN_SIZE = new int[2] { 1920, 1080 };

        [ManifestResourceStream(ResourceName = "CosmosOperatingSystem.Resources.goha-16.psf")]
        private static byte[] Font;
        public static PCScreenFont FONT;

        public static void init()
        {
            FONT = PCScreenFont.LoadFont(Font);

            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(SCREEN_SIZE[0], SCREEN_SIZE[1], ColorDepth.ColorDepth32));

            Desktop.init();
            Desktop.Display();

            //canvas.DrawString("test", FONT, new Pen(Color.Blue, 20), 100, 100);
        }

        public static void Update()
        {
            MouseHandler.Update();
            Desktop.Update();
            
            canvas.Display();
        }
    }
}

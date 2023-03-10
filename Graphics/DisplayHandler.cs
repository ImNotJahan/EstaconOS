using System;
using System.Collections.Generic;
using System.Drawing;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using EstaconOS.Graphics.Components;
using IL2CPU.API.Attribs;

namespace EstaconOS.Graphics
{
    internal class DisplayHandler
    {
        public static List<Window> windows = new();

        public static Canvas canvas;
        public static int[] SCREEN_SIZE = new int[2] { 1920, 1080 };

        [ManifestResourceStream(ResourceName = "EstaconOS.Resources.goha-16.psf")]
        private static byte[] Font;
        public static PCScreenFont FONT;

        public static Pen pen = new Pen(Color.White);

        public static void init()
        {
            FONT = PCScreenFont.LoadFont(Font);

            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(SCREEN_SIZE[0], SCREEN_SIZE[1], ColorDepth.ColorDepth32));

            Desktop.init();
            Desktop.Display();
        }

        public static void Update()
        {
            MouseHandler.Update();
            Desktop.Update();

            windows.ForEach(window => { window.Update(); });
            
            canvas.Display();
        }
    }
}

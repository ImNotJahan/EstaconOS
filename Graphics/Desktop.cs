using Cosmos.System;
using Cosmos.System.Graphics;
using EstaconOS.Graphics.Components;
using IL2CPU.API.Attribs;
using System;
using System.Drawing;

namespace EstaconOS.Graphics
{
    internal class Desktop
    {
        static Bitmap ICON_BACKGROUND;
        static Bitmap MISSING_ICON;
        static Bitmap CONSOLE_ICON;
        static Bitmap BACKGROUND;
        static Bitmap POWER_BUTTON;
        public static Bitmap WINDOW_OPERATORS;

        [ManifestResourceStream(ResourceName = "EstaconOS.Resources.iconbg.bmp")]
        static byte[] IconBackground;
        [ManifestResourceStream(ResourceName = "EstaconOS.Resources.missingicon.bmp")]
        static byte[] MissingIcon;
        [ManifestResourceStream(ResourceName = "EstaconOS.Resources.consoleicon.bmp")]
        static byte[] ConsoleIcon;
        [ManifestResourceStream(ResourceName = "EstaconOS.Resources.background.bmp")]
        static byte[] Background;
        [ManifestResourceStream(ResourceName = "EstaconOS.Resources.powerbutton.bmp")]
        static byte[] PowerButton;
        [ManifestResourceStream(ResourceName = "EstaconOS.Resources.windowoperators.bmp")]
        static byte[] WindowOperators;

        public static void init()
        {
            try
            {
                ICON_BACKGROUND = new Bitmap(IconBackground);
                MISSING_ICON = new Bitmap(MissingIcon);
                CONSOLE_ICON = new Bitmap(ConsoleIcon);
                BACKGROUND = new Bitmap(Background);
                POWER_BUTTON = new Bitmap(PowerButton);
                WINDOW_OPERATORS = new Bitmap(WindowOperators);
            }
            catch { }
        }

        public static void Display()
        {
            // I had a lot of issues regarding size and bit of backgrounds
            try
            {
                DisplayHandler.canvas.DrawImage(BACKGROUND, 0, 0, DisplayHandler.SCREEN_SIZE[0], DisplayHandler.SCREEN_SIZE[1]);
            }
            catch (Exception e)
            {
                DisplayHandler.pen.Color = Color.Red;
                DisplayHandler.canvas.DrawString(e.ToString(), DisplayHandler.FONT, DisplayHandler.pen, 100, 100);
            }

            DisplayHandler.canvas.DrawImageAlpha(ICON_BACKGROUND, 400, 400);
            DisplayHandler.canvas.DrawImageAlpha(MISSING_ICON, 400, 400);
            DisplayHandler.canvas.DrawImageAlpha(POWER_BUTTON, 0, 0);

            DisplayHandler.pen.Color = Color.Black;
            DisplayHandler.canvas.DrawString(DateTime.Now.ToString("D"), DisplayHandler.FONT, DisplayHandler.pen, 20, 16);

            GetClockPixels();
        }

        public static void Update()
        {
            UpdateClock();

            int mx = (int)MouseManager.X;
            int my = (int)MouseManager.Y;

            if(MouseManager.LastMouseState == MouseState.Left && MouseManager.MouseState == MouseState.None)
            {
                if (mx >= 1850 && my >= 20 && mx < 1900 && my < 70)
                {
                    Power.Shutdown();
                } else if (mx >= 1787 && my >= 20 && mx < 1837 && my < 70)
                {
                    Power.Reboot();
                } else if(mx >= 400 && my >= 400 && mx < 475 && my < 475)
                {
                    new Window(600, 500, 200, 200, "test window");
                }
            }
        }

        static Color[] pixelsUnderClock = new Color[11 * 16 * 16];
        static string lastTime = "";
        static void UpdateClock()
        {
            string time = DateTime.Now.ToString("T");

            if (lastTime == time) return;

            lastTime = time;

            for (int x = 0; x < 11 * 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    DisplayHandler.pen.Color = pixelsUnderClock[y * 16 * 8 + x];
                    DisplayHandler.canvas.DrawPoint(DisplayHandler.pen, 20 + x, 58 + y);
                }
            }

            DisplayHandler.pen.Color = Color.Black;
            DisplayHandler.canvas.DrawString(time, DisplayHandler.FONT, DisplayHandler.pen, 20, 58);
        }

        static void GetClockPixels()
        {
            // 8 * 16 as time is max 8 char long
            for(int x = 0; x < 8 * 16; x++)
            {
                for(int y = 0; y < 16; y++)
                {
                    pixelsUnderClock[y * 16 * 8 + x] = DisplayHandler.canvas.GetPointColor(20 + x, 58 + y); 
                }
            }
        }
    }
}

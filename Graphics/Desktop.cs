using Cosmos.System;
using Cosmos.System.Graphics;
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

        public static void init()
        {
            try
            {
                ICON_BACKGROUND = new Bitmap(IconBackground);
                MISSING_ICON = new Bitmap(MissingIcon);
                CONSOLE_ICON = new Bitmap(ConsoleIcon);
                BACKGROUND = new Bitmap(Background);
                POWER_BUTTON = new Bitmap(PowerButton);
            }
            catch { }
        }

        public static void Display()
        {
            try
            {
                DisplayHandler.canvas.DrawImage(BACKGROUND, 0, 0, DisplayHandler.SCREEN_SIZE[0], DisplayHandler.SCREEN_SIZE[1]);
            }
            catch (Exception e)
            {
                DisplayHandler.canvas.DrawString(e.ToString(), DisplayHandler.FONT, new Pen(Color.Red, 20), 100, 100);
            }

            DisplayHandler.canvas.DrawImageAlpha(ICON_BACKGROUND, 400, 400);
            DisplayHandler.canvas.DrawImageAlpha(MISSING_ICON, 400, 400);
            DisplayHandler.canvas.DrawImageAlpha(POWER_BUTTON, 0, 0);
            DisplayHandler.canvas.DrawString(DateTime.Now.ToString("D"), DisplayHandler.FONT, new Pen(Color.Black), 20, 16);

            GetClockPixels();
        }

        public static void Update()
        {
            UpdateClock();

            int mx = (int)MouseManager.X;
            int my = (int)MouseManager.Y;

            if(MouseManager.MouseState == MouseState.Left)
            {
                if (mx >= 1850 && my >= 20 && mx < 1900 && my < 70)
                {
                    Power.Shutdown();
                } else if (mx >= 1787 && my >= 20 && mx < 1837 && my < 70)
                {
                    Power.Reboot();
                }
            }
        }

        static Color[] pixelsUnderClock = new Color[11 * 16 * 16];
        static string lastTime = "";
        static void UpdateClock()
        {
            string time = DateTime.Now.ToString("t");

            if (lastTime == time) return;

            lastTime = time;

            for (int x = 0; x < 8 * 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    DisplayHandler.canvas.DrawPoint(new Pen(pixelsUnderClock[y * 16 * 8 + x]), 20 + x, 58 + y);
                }
            }

            DisplayHandler.canvas.DrawString(time, DisplayHandler.FONT, new Pen(Color.Black), 20, 58);
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

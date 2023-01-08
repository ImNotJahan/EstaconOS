using Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Drawing;

namespace CosmosOperatingSystem.Graphics
{
    internal class Desktop
    {
        static Bitmap ICON_BACKGROUND;
        static Bitmap MISSING_ICON;
        static Bitmap CONSOLE_ICON;
        static Bitmap BACKGROUND;
        static Bitmap POWER_BUTTON;

        [ManifestResourceStream(ResourceName = "CosmosOperatingSystem.Resources.iconbg.bmp")]
        static byte[] IconBackground;
        [ManifestResourceStream(ResourceName = "CosmosOperatingSystem.Resources.missingicon.bmp")]
        static byte[] MissingIcon;
        [ManifestResourceStream(ResourceName = "CosmosOperatingSystem.Resources.consoleicon.bmp")]
        static byte[] ConsoleIcon;
        [ManifestResourceStream(ResourceName = "CosmosOperatingSystem.Resources.background.bmp")]
        static byte[] Background;
        [ManifestResourceStream(ResourceName = "CosmosOperatingSystem.Resources.powerbutton.bmp")]
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
        }

        public static void Update()
        {
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
    }
}

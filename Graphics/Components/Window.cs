using Cosmos.System;
using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace EstaconOS.Graphics.Components
{
    internal class Window
    {
        static Pen TASKBAR_PEN = new Pen(Color.FromArgb(20, 20, 20));
        static Pen WINDOW_PEN = new Pen(Color.FromArgb(40, 40, 40));

        private int x;
        private int y;
        private int width;
        private int height;

        private int listIndex;

        private Color[] pixelsBehindWindow;

        public Window(int width, int height, int x, int y, string title)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            pixelsBehindWindow = new Color[width * (height + 40)];
            CalculatePixelsBehindWindow();

            DisplayHandler.canvas.DrawFilledRectangle(TASKBAR_PEN, x, y, width, 40);
            DisplayHandler.canvas.DrawFilledRectangle(WINDOW_PEN, x, y + 40, width, height);
            DisplayHandler.canvas.DrawImage(Desktop.WINDOW_OPERATORS, x + width - 120, y);
            DisplayHandler.canvas.DrawString(title, DisplayHandler.FONT, new Pen(Color.White), x + 12, y + 12);

            listIndex = DisplayHandler.windows.Count;
            DisplayHandler.windows.Add(this);
        }

        private void CalculatePixelsBehindWindow()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height + 40; y++)
                {
                    pixelsBehindWindow[y * (height + 40) + x] = DisplayHandler.canvas.GetPointColor(this.x + x, this.y + y);
                }
            }
        }

        private void RedrawPixelsBehindWindow()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height + 40; y++)
                {
                    DisplayHandler.canvas.DrawPoint(new Pen(pixelsBehindWindow[y * (height + 40) + x]), this.x + x, this.y + y); 
                }
            }
        }

        public void Update()
        {
            int mx = (int)MouseManager.X;
            int my = (int)MouseManager.Y;

            if (MouseManager.MouseState == MouseState.Left)
            {
                if (my >= y && my < y + 40)
                {
                    if (mx >= x + width - 40 && mx < x + width)
                    {
                        //DisplayHandler.windows.RemoveAt(listIndex);

                        RedrawPixelsBehindWindow();
                    } else if (mx >= x + width - 80 && mx < x + width - 40)
                    {

                    } else if (mx >= x + width - 120 && mx < x + width - 80)
                    {

                    }
                }
            }
        }
    }
}

using Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;

namespace EstaconOS.Graphics
{
    internal class MouseHandler
    {
        static int MS = 8; // Mouse size

        static Color[] pixelsUnderneath = new Color[MS * MS];
        static int[] previousMouseCords = new int[2] { -100, -100 }; // Starting cords must be less than width and height of mouse

        static Pen whitePen = new Pen(Color.White);
        static Pen blackPen = new Pen(Color.Black);

        public static void init()
        {
            MouseManager.ScreenWidth = (uint)DisplayHandler.SCREEN_SIZE[0];
            MouseManager.ScreenHeight = (uint)DisplayHandler.SCREEN_SIZE[1];
        }

        public static void Update()
        {
            int mouseX = (int)MouseManager.X;
            int mouseY = (int)MouseManager.Y;

            // Mouse points have to be drawn over first as to avoid residue
            for (int x = 0; x < MS; x++)
            {
                for (int y = 0; y < MS; y++)
                {
                    int i = y * MS + x;

                    // Draws pixels that were underneath mouse
                    DisplayHandler.pen.Color = pixelsUnderneath[i];
                    DisplayHandler.canvas.DrawPoint(DisplayHandler.pen, previousMouseCords[0] + x, previousMouseCords[1] + y);
                }
            }

            for (int x = 0; x < MS; x++)
            {
                for (int y = 0; y < MS; y++)
                {
                    int i = y * MS + x;

                    // For getting color of pixels underneath mouse
                    int mX = mouseX + x;
                    int mY = mouseY + y;

                    pixelsUnderneath[i] = DisplayHandler.canvas.GetPointColor(mX, mY);
                }
            }

            // Drawing the mouse, black with white outline
            DisplayHandler.canvas.DrawFilledRectangle(blackPen, mouseX, mouseY, MS, MS);
            DisplayHandler.canvas.DrawRectangle(whitePen, mouseX, mouseY, MS - 1, MS - 1);

            previousMouseCords[0] = mouseX;
            previousMouseCords[1] = mouseY;
        }
    }
}

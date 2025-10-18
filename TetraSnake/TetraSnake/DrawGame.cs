using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetraSnake
{
    internal static class DrawGame
    {
        private static Graphics graphics;
        
        private static Pen BlackPen = new Pen(Color.FromArgb(255, 0, 0, 0), 3);
        public static void Draw(Field field, PictureBox pictureBox, int size)
        {

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
            for (int y = 0; y < field.Size.Y; y++)
            {
                for (int x = 0; x < field.Size.X; x++)
                {
                    if (field.GetPoint(x,y)  == CellType.Tetris)
                    {
                        DrawShape(x, y, size, Brushes.DarkMagenta);
                    }
                    if (field.GetPoint(x,y) == CellType.Apple)
                    {
                        DrawShape(x, y, size, Brushes.Crimson);
                    }
                    if (field.GetPoint(x, y) == CellType.Snake)
                    {
                        int len = LogicSnake.Snake.Count;
    
                        int green = Math.Min(255, 50 + len * 10); 

                        Color color = Color.FromArgb(255, 0, green, 0);

                        using (SolidBrush brush = new SolidBrush(color))
                            DrawShape(x, y, size, brush);
                    }

                }
            }
            pictureBox.Refresh();
        }

        private static void DrawShape(int x, int y, int size, Brush brush)
        {
            int px = x * size;
            int py = y * size;

            graphics.FillRectangle(brush, px, py, size, size);
            graphics.DrawRectangle(BlackPen, px, py, size, size);
        }
    }
}

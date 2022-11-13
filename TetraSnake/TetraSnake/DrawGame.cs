using System.Drawing;
using System.Windows.Forms;

namespace TetraSnake
{
    internal static class DrawGame
    {
        private static Graphics graphics;
        public static void Draw(int[,] field, PictureBox pictureBox, int size)
        {

            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(pictureBox.Image);
            for (int y = 0; y < field.GetLength(0); y++)
            {
                for (int x = 0; x < field.GetLength(1); x++)
                {
                    /*
                     * 1 - ячейки тетриса
                     * 2 - яблоко
                     * 3 - тело змейки
                     */
                    if (field[y, x] == 1)
                    {
                        Pen blackpen = new Pen(Color.FromArgb(255, 0, 0, 0), 3);
                        graphics.FillRectangle(Brushes.DarkMagenta, x * size, y * size, size, size);
                        graphics.DrawRectangle(blackpen, x * size, y * size, size, size);
                    }
                    if (field[y, x] == 2)
                    {
                        Pen blackpen = new Pen(Color.FromArgb(255, 0, 0, 0), 3);
                        graphics.FillRectangle(Brushes.Crimson, x * size, y * size, size, size);
                        graphics.DrawRectangle(blackpen, x * size, y * size, size, size);
                    }
                    if (field[y, x] == 3)
                    {
                        Color color = new Color();
                        SolidBrush solidBrush;
                        int g = LogicSnake.Snake.Count * 5 % 255;
                        color = Color.FromArgb(255, LogicSnake.Snake.Count % 255,  (g > 255) ? 255 : g, LogicSnake.Snake.Count / 2 % 255);
                        solidBrush = new SolidBrush(color);

                        Pen blackpen = new Pen(Color.FromArgb(255, 0, 0, 0), 3);
                        graphics.FillRectangle(solidBrush, x * size, y * size, size, size);
                        graphics.DrawRectangle(blackpen, x * size, y * size, size, size);
                    }
                }
            }
            pictureBox.Refresh();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetraSnake
{
    public class GameRenderer
    {
        private readonly Pen _gridPen = new Pen(Color.FromArgb(60, 60, 60), 2);
        private Graphics _graphics;

        public void Render(Field field, PictureBox pictureBox, int cellSize)
        {
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            _graphics = Graphics.FromImage(pictureBox.Image);

            for (int y = 0; y < field.Size.Y; y++)
            {
                for (int x = 0; x < field.Size.X; x++)
                {
                    var cell = field.GetPoint(x, y);
                    switch (cell)
                    {
                        case CellType.Tetris:
                            DrawCell(x, y, cellSize, new SolidBrush(Color.FromArgb(130, 170, 255)));
                            break;
                        case CellType.Apple:
                            DrawCell(x, y, cellSize, new SolidBrush(Color.FromArgb(230, 80, 80))); 
                            break;
                        case CellType.Snake:
                            DrawCell(x, y, cellSize, new SolidBrush(Color.FromArgb(80, 200, 120))); 
                            break;
                    }
                }
            }

            pictureBox.Refresh();
        }

        private void DrawCell(int x, int y, int size, Brush brush)
        {
            int px = x * size;
            int py = y * size;
            _graphics.FillRectangle(brush, px, py, size, size);
            _graphics.DrawRectangle(_gridPen, px, py, size, size);
        }
    }
}
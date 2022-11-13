using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public class Field
    {
        public static int score;
        public static int clearLine = 0;
        private int[,] arrayField;
        public void Set(PictureBox pictureBox, int[,] arr = null, int scale = 0)
        {
            int sizeY = 0;
            int sizeX = 0;
            if (arr != null)
            {
                arrayField = arr;
            }
            else
            {
                sizeY = pictureBox.Size.Height / scale;
                sizeX = pictureBox.Size.Width / scale;
                arrayField = new int[sizeY, sizeX];
            }
        }
        public void Set(int[,] arr)
        {
            arrayField = arr;
        }
        public int[,] Get()
        {
            return arrayField;
        }
        private void LowerBlocks(int[,] figure, int X, int Y, int y)
        {

            bool equal = false;
            for (y--; y >= 0; y--)
            {
                for (int x = 0; x < arrayField.GetLength(1); x++)
                {
                    if (arrayField[y, x] != 1)
                        continue;
                    //проверяем координаты фигуры  чтобы они не совпадали с y,x
                    //если совпадают пропускаем итерацию и фигура не будет опускаться
                    //вместе с остальной конструкцией при очистке линии
                    for (int figureY = 0; figureY < figure.GetLength(0); figureY++)
                    {
                        for (int figureX = 0; figureX < figure.GetLength(1); figureX++)
                        {
                            if (figure[figureY, figureX] == 0)
                                break;
                            if ((X + figureX) == x && (Y + figureY) == y)
                            {
                                equal = true;
                                break;
                            }
                        }
                    }

                    if (equal == true)
                        continue;

                    if (y + 1 >= arrayField.GetLength(0))
                        arrayField[y, x] = arrayField[y - 1, x];
                    else 
                    {
                        arrayField[y + 1, x] = arrayField[y, x];
                        arrayField[y, x] = 0;
                    }
                }
            }

        }
        public int CheckFullLine(int[,] figure, int X, int Y, int countclearLine = 0)
        {
            bool clearLin = false;
            bool fullLine = false;
            for (int y = arrayField.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < arrayField.GetLength(1); x++)
                {
                    if (fullLine)
                    {
                        arrayField[y, x] = 0;
                        if (x == arrayField.GetLength(1) - 1)
                        {
                            fullLine = false;
                            clearLin = true;
                            clearLine++;
                        }
                    }
                    else
                    {
                        if (arrayField[y, x] == 0)
                            break;
                        if (x == arrayField.GetLength(1) - 1)
                        {
                            fullLine = true;
                            x = -1;
                        }
                    }
                }
                if (clearLin)
                {
                    countclearLine++;
                    LowerBlocks(figure, X, Y, y);
                    clearLin = false;
                    score += CheckFullLine(figure, X, Y, countclearLine);
                }
            }
            return countclearLine * (Figure.Figures.Count) * (1000 + (countclearLine * countclearLine * 100 / 5));
        }
    }
}

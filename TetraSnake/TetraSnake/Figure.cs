using System;
using System.Collections.Generic;
using System.Threading;

namespace TetraSnake
{
    public class Figure
    {
        public bool rotate = false;
        //pressButton нужен для ограничения нажатий
        public bool pressButton = true;
        Random random = new Random();
        public int nextFigure;
        public Figure(int[,] figure)
        {
            nextFigure = random.Next(Figures.Count);
            arrayFigure = new int[figure.GetLength(0), figure.GetLength(1)];
            Array.Copy(figure, arrayFigure, figure.Length);
        }
        public Figure(int[,] figure, Field field)
        {
            nextFigure = random.Next(Figures.Count);
            arrayFigure = new int[figure.GetLength(0), figure.GetLength(1)];
            Array.Copy(figure, arrayFigure, figure.Length);
            X = random.Next(field.Get().GetLength(1) - figure.GetLength(1));
            Y = 0;
        }
        public int[,] arrayFigure { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int vectorX = 0;

        public static List<int[,]> Figures = ListOfFigure.Tetramino;

        public bool IsFigurePoint(int PointX, int PointY)
        {
            // определяет является ли координаты координатами фигуры
            for (int y = 0; y < arrayFigure.GetLength(0); y++)
            {
                for (int x = 0; x < arrayFigure.GetLength(1); x++)
                {
                    if (PointX == (x + X) && PointY == (y + y))
                        return true;
                }
            }
            return false;
        }
        public void Spawn(Field field)
        {
            vectorX = 0;
            Y = 0;
            X = random.Next(field.Get().GetLength(1) - arrayFigure.GetLength(1) - 1);
            if (nextFigure >= Figures.Count)
                nextFigure = Figures.Count - 1;

            arrayFigure = new int[Figures[nextFigure].GetLength(0), Figures[nextFigure].GetLength(1)];
            Array.Copy(Figures[nextFigure], arrayFigure, Figures[nextFigure].Length);
            nextFigure = random.Next(Figures.Count);
            // проверяем полноту линии
            field.CheckFullLine(arrayFigure, X, Y);
        }
        public void Rotate(Field field)
        {
            int[,] arrayField = field.Get();
            int[,] rotateFigure = new int[arrayFigure.GetLength(1), arrayFigure.GetLength(0)];
            if (Y + rotateFigure.GetLength(0) >= arrayField.GetLength(0) || X + rotateFigure.GetLength(1) >= arrayField.GetLength(1))
                return;
            // поворачиваем фигуру
            for (int y = 0; y < arrayFigure.GetLength(0); y++)
            {
                for (int x = 0; x < arrayFigure.GetLength(1); x++)
                {
                    if (arrayFigure[y, x] == 1)
                        arrayField[Y + y, X + x] = 0;

                    rotateFigure[x, rotateFigure.GetLength(1) - (1 + y)] = arrayFigure[y, x];
                }
            }
            //проверяем можно ли повернуть фигуру
            for (int y = 0; y < rotateFigure.GetLength(0); y++)
            {
                for (int x = 0; x < rotateFigure.GetLength(1); x++)
                {
                    if (rotateFigure[y, x] == 0)
                        continue;
                    if (arrayField[Y + y, X + x] == 1 || arrayField[Y + y, X + x] == 3 || arrayField[Y + y, X + x] == 2)
                        return;
                }
            }
            arrayFigure = rotateFigure;
        }
        private bool IsEmpty()
        {
            for (int y = 0; y < arrayFigure.GetLength(0); y++)
            {
                for (int x = 0; x < arrayFigure.GetLength(1); x++)
                {
                    if (arrayFigure[y, x] == 1)
                        return false;
                }
            }
            return true;
        }
        public int[,] Move(Field field)
        {
            pressButton = false;
            int[,] arrayField = field.Get();
            CheckBorder(field);
            if (IsEmpty())
            {
                Spawn(field);
                ChangesField(arrayField);
            }
            if (rotate)
            {
                Rotate(field);
                ChangesField(arrayField);
                rotate = false;
            }
            if (BlockCheckRL(field))
            {
                X += vectorX;
                Clear(arrayField, true);
                ChangesField(arrayField);
            }
            if (BlockCheckDown(field))
            {
                Y++;
                Clear(arrayField, false);
                ChangesField(arrayField);
            }
            pressButton = true;
            return arrayField;
        }
        private void ChangesField(int[,] arrayField)
        {

            // обновляем пареметры поля
            for (int y = 0; y < arrayFigure.GetLength(0); y++)
            {
                for (int x = 0; x < arrayFigure.GetLength(1); x++)
                {
                    if (arrayFigure[y, x] == 1)
                    {
                        arrayField[y + Y, x + X] = arrayFigure[y, x];
                    }
                }
            }
        }
        private void Clear(int[,] arrayField, bool rightLeft)
        {
            //убираем след от фигуры когда она движется
            for (int y = 0; y < arrayFigure.GetLength(0); y++)
            {
                for (int x = 0; x < arrayFigure.GetLength(1); x++)
                {
                    if (arrayFigure[y, x] == 0)
                        continue;
                    if (rightLeft)
                    {
                        if (vectorX == 1)
                        {
                            arrayField[Y + y, X + x - 1] = 0;
                        }
                        else if (vectorX == -1)
                        {
                            arrayField[Y + y, X + x + 1] = 0;
                        }
                    }
                    else if (!rightLeft && Y != 0)
                    {
                        arrayField[Y + (y - 1), X + x] = 0;
                    }
                }
            }
            vectorX = 0;
        }
        private void CheckBorder(Field field)
        {
            //ограничили поле слева и справа
            int[,] arrayField = field.Get();
            if (vectorX == 1 && X + arrayFigure.GetLength(1) >= arrayField.GetLength(1))
            {
                vectorX = 0;
            }
            if (vectorX == -1 && X <= 0)
            {
                vectorX = 0;
            }
            //ограничили внизу
            if (Y + arrayFigure.GetLength(0) >= arrayField.GetLength(0))
            {
                for (int y = arrayFigure.GetLength(0) - 1; y >= 0; y--)
                {
                    for (int x = 0; x < arrayFigure.GetLength(1); x++)
                    {
                        if (arrayFigure[y, x] == 0)
                            continue;
                        if (Y + y + 1 >= arrayField.GetLength(0))
                        {
                            Spawn(field);
                            return;
                        }
                    }
                }
            }
        }
        private bool BlockCheckRL(Field field)
        {
            //проверка поверхности справа и слева в зависимости от направления
            int[,] arrayField = field.Get();
            if (vectorX != 0)
            {
                for (int y = 0; y < arrayFigure.GetLength(0); y++)
                {
                    if (vectorX == 1)
                    {
                        int x;
                        for (x = arrayFigure.GetLength(1) - 1; x >= 0; x--)
                        {
                            if (arrayFigure[y, x] == 0)
                                continue;
                            else
                                break;
                        }
                        if (X < arrayField.GetLength(1) - arrayFigure.GetLength(1) && arrayField[Y + y, X + x + 1] == 1)
                        {
                            vectorX = 0;
                            break;
                        }
                    }
                    else if (vectorX == -1)
                    {

                        int x;
                        for (x = 0; x < arrayFigure.GetLength(1); x++)
                        {
                            if (arrayFigure[y, x] == 0)
                                continue;
                            else
                                break;
                        }
                        if (X >= 1 && arrayField[Y + y, X + (x - 1)] == 1)
                        {
                            vectorX = 0;
                            break;
                        }
                    }
                }

                if (vectorX != 0)
                    return true;
            }
            return false;
        }
        private bool BlockCheckDown(Field field)
        {
            // проверка блоков под фигурой
            int[,] arrayField = field.Get();
            for (int y = 0; y < arrayFigure.GetLength(0); y++)
            {
                for (int x = 0; x < arrayFigure.GetLength(1); x++)
                {
                    if (y < arrayFigure.GetLength(0) - 1 && arrayFigure[y + 1, x] == 1)
                        continue;
                    if (arrayFigure[y, x] == 0)
                        continue;
                    if (Y + y + 1 >= arrayField.GetLength(0))
                        break;
                    //проверяем блоки тетриса
                    if (vectorX == 0 && Y <= arrayField.GetLength(0) - arrayFigure.GetLength(0) && arrayField[Y + y + 1, X + x] == 1)
                    {
                        Spawn(field);
                        return false;
                    }
                    // проверяем блоки змейки и яблока
                    if (vectorX == 0 && Y <= arrayField.GetLength(0) - arrayFigure.GetLength(0) && arrayField[Y + y + 1, X + x] == 3 ||
                        vectorX == 0 && Y <= arrayField.GetLength(0) - arrayFigure.GetLength(0) && arrayField[Y + y + 1, X + x] == 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

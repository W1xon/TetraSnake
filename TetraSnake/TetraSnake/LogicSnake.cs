using System;
using System.Collections.Generic;

namespace TetraSnake
{
    internal static class LogicSnake
    {
        private static Random random = new Random();
        public static bool isDeath;
        public static int vectorX = 0;
        public static int vectorY = 0;
        public static List<BodySnake> Snake;
        public static bool DeathTetris;

        public static void SpawnSnake(Field field)
        {
            Snake = new List<BodySnake>()
            {
                new BodySnake(field),
            };
        }
        public static void Append()
        {
            Snake.Add(new BodySnake(Snake[Snake.Count - 1].X, Snake[Snake.Count - 1].Y));
        }
        private static void Eat(Field field)
        {
            Field.score += 40;
            Apple.Spawn(field);
            Append();
        }
        private static void EatTetramino(Figure figure, BodySnake snakeHead)
        {
            for (int y = 0; y < figure.arrayFigure.GetLength(0); y++)
            {
                for (int x = 0; x < figure.arrayFigure.GetLength(1); x++)
                {
                    if (figure.X + x == snakeHead.X && figure.Y + y == snakeHead.Y)
                    {
                        figure.arrayFigure[y, x] = 0;
                    }
                }
            }
        }
        public static void Move(Field field, Figure figure = null)
        {
            int[,] arrayField = field.Get();
            //проверяем не врезаемся ли мы в 2 блок тела
            if (Snake.Count > 1 && (Snake[0].Y + vectorY == Snake[1].Y && Snake[0].X + vectorX == Snake[1].X))
            {
                if (vectorX == 1)
                {
                    vectorY = 0;
                    vectorX = -1;
                }
                else if (vectorY == 1)
                {
                    vectorX = 0;
                    vectorY = -1;
                }
                else if (vectorX == -1)
                {
                    vectorY = 0;
                    vectorX = 1;
                }
                else if (vectorY == -1)
                {
                    vectorX = 0;
                    vectorY = 1;
                }
                return;
            }
            arrayField[Snake[0].Y, Snake[0].X] = 0;
            if (Snake[0].X + vectorX >= arrayField.GetLength(1) || Snake[0].X + vectorX < 0)
                Death(field);
            if (Snake[0].Y + vectorY >= arrayField.GetLength(0) || Snake[0].Y + vectorY < 0)
                Death(field);
            int[] lastLocation = { Snake[0].X, Snake[0].Y };
            int[] lastLocationTwo = new int[2];
            Snake[0].X += vectorX;
            Snake[0].Y += vectorY;

            //проверяем задели ли мы тело змейки головой
            for (int i = 1; i < Snake.Count; i++)
            {
                if (BodySnake.Compare(Snake[0], Snake[i]))
                    Death(field);
            }

            if (arrayField[Snake[0].Y, Snake[0].X] == 2)
                Eat(field);
            if (arrayField[Snake[0].Y, Snake[0].X] == 1)
            {
                if (DeathTetris)
                    Death(field);
                EatTetramino(figure, Snake[0]);
            }

            for (int i = 1; i < Snake.Count; i++)
            {
                lastLocationTwo = new int[2] { Snake[i].X, Snake[i].Y };
                Snake[i].X = lastLocation[0];
                Snake[i].Y = lastLocation[1];
                Array.Copy(lastLocationTwo, lastLocation, lastLocationTwo.Length);
                arrayField[lastLocationTwo[1], lastLocationTwo[0]] = 0;
            }
            Draw(field);
        }
        private static void Draw(Field field)
        {
            int[,] arrayField = field.Get();
            foreach (BodySnake body in Snake)
            {
                arrayField[body.Y, body.X] = 3;
            }
        }

        public static void Death(Field field)
        {
            for (int i = 1; i != Snake.Count;)
                Snake.RemoveAt(0);
            isDeath = true;
            do
            {
                Snake[0].X = random.Next(field.Get().GetLength(1));
                Snake[0].Y = random.Next(field.Get().GetLength(0));

            } while (field.Get()[Snake[0].Y, Snake[0].X] == 1);
            vectorX = 0;
            vectorY = 0;
        }
    }
}

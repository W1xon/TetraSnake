using System;
using System.Collections.Generic;
using System.Linq;

namespace TetraSnake
{
    internal static class LogicSnake
    {
        public static bool isDeath;
        public static Vector Direction;
        public static List<BodySnake> Snake;
        public static bool DeathTetris;
        private static readonly Random _random = new Random();

        public static void SpawnSnake(Field field)
        {
            Snake = new List<BodySnake>()
            {
                new BodySnake(field),
            };
        }
        public static void Append()
        {
            Snake.Add(new BodySnake(Snake[Snake.Count - 1].Position));
        }
        private static void Eat(Field field)
        {
            Field.score += 40;
            Apple.Spawn(field);
            Append();
        }
        private static void EatTetramino(Shape shape, BodySnake snakeHead)
        {
            var localPos = snakeHead.Position - shape.Position;

            if (localPos.Y >= 0 && localPos.Y < shape.Size.Y &&
                localPos.X >= 0 && localPos.X < shape.Size.X &&
                shape.IsSelf(snakeHead.Position))
            {
                shape.SetPoint(localPos, CellType.Empty);
            }
        }


        public static void Move(Field field, Shape shape = null)
        {
            if (IsCollidingWithSecondSegment()) return;
            
            field.SetPoint(Snake[0].Position, CellType.Empty);
            if (field.IsOutBound(Snake[0].Position))
                Death(field);
            
            Vector lastLocation = Snake[0].Position;
            Snake[0].Position += Direction;

            //проверяем задели ли мы тело змейки головой
            if(Snake.Count(s => s.Position == Snake[0].Position) > 1)
                Death(field);

            if (field.GetPoint(Snake[0].Position) == CellType.Apple)
                Eat(field);
            if (field.GetPoint(Snake[0].Position) == CellType.Tetris)
            {
                if (DeathTetris)
                    Death(field);
                else
                    EatTetramino(shape, Snake[0]);
            }

            for (int i = 1; i < Snake.Count; i++)
            {
                var lastLocationTwo = Snake[i].Position;
                Snake[i].Position = lastLocation;
                lastLocation = lastLocationTwo;
                field.SetPoint(lastLocationTwo, CellType.Empty);
            }
            Draw(field);
        }
        private static bool IsCollidingWithSecondSegment()
        {
            if (Snake.Count <= 1)
                return false;

            if (Snake[0] != Snake[1])
                return false;

            // меняем направление, чтобы избежать столкновения
            if (Direction.X == 1)
            {
                Direction.X = -1;
                Direction.Y = 0;
            }
            else if (Direction.X == -1)
            {
                Direction.X = 1;
                Direction.Y = 0;
            }
            else if (Direction.Y == 1)
            {
                Direction.Y = -1;
                Direction.X = 0;
            }
            else if (Direction.Y == -1)
            {
                Direction.Y = 1;
                Direction.X = 0;
            }

            return true;
        }

        private static void Draw(Field field)
        {
            foreach (var body in Snake)
            {
                field.SetPoint(body.Position, CellType.Snake);
            }
        }

        private static void Death(Field field)
        {
            for (int i = 1; i != Snake.Count;)
                Snake.RemoveAt(0);
            isDeath = true;
            do
            {
                Snake[0].Position.X = _random.Next(field.Size.X);
                Snake[0].Position.Y = _random.Next(field.Size.Y);

            } while (field.GetPoint(Snake[0].Position) == CellType.Tetris);
            Direction.X = 0;
            Direction.Y = 0;
        }
    }
}

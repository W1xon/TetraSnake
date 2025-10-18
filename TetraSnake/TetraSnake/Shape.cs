using System;
using System.Collections.Generic;
using System.Linq;

namespace TetraSnake
{
    public class Shape
    {
        
        public event Action<bool>? OnSpawnBlocked;

        public bool CanRotate = false;
        public bool pressButton = true; 
        public int NextFigureIndex;
        public int VectorX = 0;
        public Vector Position;

        public static List<int[,]> Figures = ListOfFigure.Tetramino;

        private readonly Random _random = new Random();
        private Vector _size;
        private int[,] _shapeArray;

        
        public Vector Size => _size;

        
        public Shape(int[,] shapeArray, Field field)
        {
            NextFigureIndex = _random.Next(Figures.Count);
            _shapeArray = new int[shapeArray.GetLength(0), shapeArray.GetLength(1)];
            Array.Copy(shapeArray, _shapeArray, shapeArray.Length);

            _size = new Vector(_shapeArray.GetLength(1), _shapeArray.GetLength(0));
            Position = new Vector(_random.Next(field.Size.X - shapeArray.GetLength(1)), 0);
        }

        
        public void Move(Field field)
        {
            pressButton = false;

            CheckBorder(field);
            HandleEmpty(field);
            HandleRotation(field);
            HandleHorizontalMovement(field);
            HandleVerticalMovement(field);

            pressButton = true;
        }

        public void Spawn(Field field)
        {
            VectorX = 0;
            Position.Y = 0;

            if (NextFigureIndex >= Figures.Count)
                NextFigureIndex = Figures.Count - 1;

            _shapeArray = new int[Figures[NextFigureIndex].GetLength(0), Figures[NextFigureIndex].GetLength(1)];
            Array.Copy(Figures[NextFigureIndex], _shapeArray, Figures[NextFigureIndex].Length);
            NextFigureIndex = _random.Next(Figures.Count);

            Position.X = _random.Next(field.Size.X - _shapeArray.GetLength(1));

            if (IsSpawnBlocked(field))
            {
                OnSpawnBlocked?.Invoke(true);
                return;
            }

            field.CheckFullLine(_shapeArray, Position);
        }

        public void Rotation(Field field)
        {
            var rotatedShape = _shapeArray.RotateMatrix();
            if (!CanTransposition(field, rotatedShape)) 
                return;

            field.ClearCells(Position, new Vector(_shapeArray.GetLength(1), _shapeArray.GetLength(0)));
            _shapeArray = rotatedShape;
            _size = new Vector(rotatedShape.GetLength(1), rotatedShape.GetLength(0));
        }

        
        private void HandleEmpty(Field field)
        {
            if (IsEmpty())
            {
                Spawn(field);
                field.SetArray(_shapeArray, Position);
            }
        }

        private void HandleRotation(Field field)
        {
            if (CanRotate)
            {
                Rotation(field);
                Clear(field, false);
                field.SetArray(_shapeArray, Position);
                CanRotate = false;
            }
        }

        private void HandleHorizontalMovement(Field field)
        {
            if (BlockCheckRL(field))
            {
                Position.X += VectorX;
                Clear(field, true);
                field.SetArray(_shapeArray, Position);
            }
        }

        private void HandleVerticalMovement(Field field)
        {
            if (BlockCheckDown(field))
            {
                Position.Y++;
                Clear(field, false);
                field.SetArray(_shapeArray, Position);
            }
        }

        
        public bool IsSelf(Vector position)
        {
            for (int y = 0; y < _shapeArray.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeArray.GetLength(1); x++)
                {
                    var cellPos = new Vector(Position.X + x, Position.Y + y);
                    if (position == cellPos)
                        return true;
                }
            }
            return false;
        }

        public bool IsSpawnBlocked(Field field)
        {
            for (int y = 0; y < _shapeArray.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeArray.GetLength(1); x++)
                {
                    if (_shapeArray[y, x] == 0) continue;

                    int fx = Position.X + x;
                    int fy = Position.Y + y;

                    if (fy >= 0 && fy < field.Size.Y && fx >= 0 && fx < field.Size.X)
                    {
                        if (field.GetPoint(fx, fy) == CellType.Tetris)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool CanTransposition(Field field, int[,] rotatedShape)
        {
            for (int y = 0; y < rotatedShape.GetLength(0); y++)
            {
                for (int x = 0; x < rotatedShape.GetLength(1); x++)
                {
                    if (rotatedShape[y, x] == 0) continue;

                    int fx = Position.X + x;
                    int fy = Position.Y + y;

                    if (fx < 0 || fx >= field.Size.X || fy < 0 || fy >= field.Size.Y)
                        return false;

                    if (field.GetPoint(fx, fy) != 0 && !IsSelf(new Vector(fx, fy)))
                        return false;
                }
            }
            return true;
        }

        private bool IsEmpty() =>
            !_shapeArray.Cast<int>().Any(cell => cell == 1);

        
        private void Clear(Field field, bool rightLeft)
        {
            for (int y = 0; y < _shapeArray.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeArray.GetLength(1); x++)
                {
                    if (_shapeArray[y, x] == 0)
                        continue;

                    if (rightLeft)
                        field.SetPoint(Position.X + x - VectorX, Position.Y + y, 0);
                    else if (Position.Y != 0)
                        field.SetPoint(Position.X + x, Position.Y + (y - 1), 0);
                }
            }
            VectorX = 0;
        }

        private void CheckBorder(Field field)
        {
            if ((VectorX == 1 && field.IsOutBoundX(Position.X + _shapeArray.GetLength(1))) ||
                (VectorX == -1 && Position.X <= 0))
            {
                VectorX = 0;
            }

            if (field.IsOutBoundY(Position.Y + _shapeArray.GetLength(0)))
            {
                for (int y = _shapeArray.GetLength(0) - 1; y >= 0; y--)
                {
                    for (int x = 0; x < _shapeArray.GetLength(1); x++)
                    {
                        if (_shapeArray[y, x] == 0)
                            continue;

                        if (Position.Y + y + 1 >= field.Size.Y)
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
            if (VectorX == 0) return false;

            for (int y = 0; y < _shapeArray.GetLength(0); y++)
            {
                if (VectorX == 1)
                {
                    int x = Enumerable.Range(0, _shapeArray.GetLength(1)).Reverse().FirstOrDefault(i => _shapeArray[y, i] == 1);
                    if (Position.X < field.Size.X - _shapeArray.GetLength(1) &&
                        field.GetPoint(Position.X + x + 1, Position.Y + y) == CellType.Tetris)
                    {
                        VectorX = 0;
                        break;
                    }
                }
                else if (VectorX == -1)
                {
                    int x = Enumerable.Range(0, _shapeArray.GetLength(1)).FirstOrDefault(i => _shapeArray[y, i] == 1);
                    if (Position.X >= 1 &&
                        field.GetPoint(Position.X + x - 1, Position.Y + y) == CellType.Tetris)
                    {
                        VectorX = 0;
                        break;
                    }
                }
            }
            return VectorX != 0;
        }

        private bool BlockCheckDown(Field field)
        {
            for (int y = 0; y < _shapeArray.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeArray.GetLength(1); x++)
                {
                    if ((y < _shapeArray.GetLength(0) - 1 && _shapeArray[y + 1, x] == 1) || _shapeArray[y, x] == 0)
                        continue;

                    if (Position.Y + y + 1 >= field.Size.Y)
                        break;

                    var below = field.GetPoint(Position.X + x, Position.Y + y + 1);
                    if (VectorX == 0)
                    {
                        if (below == CellType.Tetris)
                        {
                            Spawn(field);
                            return false;
                        }
                        if (below == CellType.Snake || below == CellType.Apple)
                            return false;
                    }
                }
            }
            return true;
        }

        
        public void SetPoint(int x, int y, int type) =>
            _shapeArray[y, x] = type;

        public void SetPoint(Vector position, CellType type) =>
            _shapeArray[position.Y, position.X] = (int)type;
    }
}

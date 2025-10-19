using System;
using System.Collections.Generic;
using System.Linq;

namespace TetraSnake
{
    public class Tetramino
    {
        
        public event Action OnSpawnBlocked;
        public event Func<int[,]> GetNextTetramino;

        
        public bool CanRotate { get; set; }
        public bool IsControlEnabled { get; set; }
        public int HorizontalDirection { get; set; }
        public Vector Position { get; private set; }
        public Vector Size => _size;

        
        private readonly Random _random = new Random();
        private Vector _size;
        private int[,] _shapeMatrix;

        
        public Tetramino(int[,] shapeArray, Field field)
        {
            _shapeMatrix = (int[,])shapeArray.Clone();
            _size = new Vector(_shapeMatrix.GetLength(1), _shapeMatrix.GetLength(0));
            Position = new Vector(_random.Next(field.Size.X - _shapeMatrix.GetLength(1)), 0);
        }

        
        public void Move(Field field)
        {
            IsControlEnabled = false;

            CheckBorders(field);
            ExecuteMovementStrategies(field);

            IsControlEnabled = true;
        }

        public void Spawn(Field field)
        {
            HorizontalDirection = 0;
            _shapeMatrix = GetNextTetramino?.Invoke();
            Position = new Vector(_random.Next(field.Size.X - _shapeMatrix.GetLength(1)), 0);

            if (IsSpawnBlocked(field))
            {
                OnSpawnBlocked?.Invoke();
                return;
            }

            field.CheckFullLine(_shapeMatrix, Position);
        }

        public void Rotate(Field field)
        {
            var rotated = _shapeMatrix.RotateMatrix();
            if (!CanPlaceRotatedShape(field, rotated))
                return;

            field.ClearCells(Position, new Vector(_shapeMatrix.GetLength(1), _shapeMatrix.GetLength(0)));
            _shapeMatrix = rotated;
            _size = new Vector(rotated.GetLength(1), rotated.GetLength(0));
            field.SetArray(_shapeMatrix, Position);
        }

        public bool IsSelf(Vector cellPosition)
        {
            for (int y = 0; y < _shapeMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeMatrix.GetLength(1); x++)
                {
                    if (_shapeMatrix[y, x] == 0) continue;
                    if (Position.X + x == cellPosition.X && Position.Y + y == cellPosition.Y)
                        return true;
                }
            }
            return false;
        }

        public bool IsSpawnBlocked(Field field)
        {
            for (int y = 0; y < _shapeMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeMatrix.GetLength(1); x++)
                {
                    if (_shapeMatrix[y, x] == 0) continue;
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

        public void SetCell(Vector position, CellType type) =>
            _shapeMatrix[position.Y, position.X] = (int)type;

        
        private void ExecuteMovementStrategies(Field field)
        {
            if (IsEmpty())
            {
                Spawn(field);
                field.SetArray(_shapeMatrix, Position);
                return;
            }

            if (CanRotate)
            {
                Rotate(field);
                ClearPrevious(field, clearHorizontal: false);
                CanRotate = false;
            }

            if (HorizontalDirection != 0 && CanMoveHorizontally(field))
            {
                Position = Position.AddX(HorizontalDirection);
                ClearPrevious(field, clearHorizontal: true);
            }

            if (CanMoveDown(field))
            {
                Position = Position.AddY(1);
                ClearPrevious(field, clearHorizontal: false);
            }

            field.SetArray(_shapeMatrix, Position);
            HorizontalDirection = 0;
        }

        
        private bool IsEmpty() => !_shapeMatrix.Cast<int>().Any(cell => cell == (int)CellType.Tetris);

        private void ClearPrevious(Field field, bool clearHorizontal)
        {
            for (int y = 0; y < _shapeMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeMatrix.GetLength(1); x++)
                {
                    if (_shapeMatrix[y, x] == (int)CellType.Empty)
                        continue;

                    int px = Position.X + x;
                    int py = Position.Y + y;

                    if (clearHorizontal)
                        field.SetPoint(px - HorizontalDirection, py, CellType.Empty);
                    else if (Position.Y != 0)
                        field.SetPoint(px, py - 1, CellType.Empty);
                }
            }
        }

        private void CheckBorders(Field field)
        {
            if ((HorizontalDirection == 1 && field.IsOutBoundX(Position.X + _shapeMatrix.GetLength(1))) ||
                (HorizontalDirection == -1 && Position.X <= 0))
                HorizontalDirection = 0;

            if (field.IsOutBoundY(Position.Y + _shapeMatrix.GetLength(0)))
            {
                for (int y = _shapeMatrix.GetLength(0) - 1; y >= 0; y--)
                {
                    for (int x = 0; x < _shapeMatrix.GetLength(1); x++)
                    {
                        if (_shapeMatrix[y, x] == (int)CellType.Empty) continue;
                        if (Position.Y + y + 1 >= field.Size.Y)
                        {
                            Spawn(field);
                            return;
                        }
                    }
                }
            }
        }

        private bool CanMoveHorizontally(Field field)
        {
            for (int y = 0; y < _shapeMatrix.GetLength(0); y++)
            {
                if (HorizontalDirection == 1)
                {
                    int rightmost = GetRightmostBlock(y);
                    if (Position.X < field.Size.X - _shapeMatrix.GetLength(1) &&
                        field.GetPoint(Position.X + rightmost + 1, Position.Y + y) == CellType.Tetris)
                        return false;
                }
                else if (HorizontalDirection == -1)
                {
                    int leftmost = GetLeftmostBlock(y);
                    if (Position.X >= 1 &&
                        field.GetPoint(Position.X + leftmost - 1, Position.Y + y) == CellType.Tetris)
                        return false;
                }
            }
            return true;
        }

        private bool CanMoveDown(Field field)
        {
            for (int y = 0; y < _shapeMatrix.GetLength(0); y++)
            {
                for (int x = 0; x < _shapeMatrix.GetLength(1); x++)
                {
                    if (_shapeMatrix[y, x] == (int)CellType.Empty 
                        || (y < _shapeMatrix.GetLength(0) - 1 
                            &&_shapeMatrix[y + 1, x] == (int)CellType.Tetris))
                        continue;

                    int belowY = Position.Y + y + 1;
                    if (belowY >= field.Size.Y) break;

                    var belowCell = field.GetPoint(Position.X + x, belowY);
                    if (HorizontalDirection == 0)
                    {
                        if (belowCell == CellType.Tetris)
                        {
                            Spawn(field);
                            return false;
                        }
                        if (belowCell == CellType.Snake || belowCell == CellType.Apple)
                            return false;
                    }
                }
            }
            return true;
        }

        private bool CanPlaceRotatedShape(Field field, int[,] rotatedShape)
        {
            for (int y = 0; y < rotatedShape.GetLength(0); y++)
            {
                for (int x = 0; x < rotatedShape.GetLength(1); x++)
                {
                    if (rotatedShape[y, x] == (int)CellType.Empty) continue;

                    int fx = Position.X + x;
                    int fy = Position.Y + y;

                    if (fx < 0 || fx >= field.Size.X || fy < 0 || fy >= field.Size.Y)
                        return false;

                    if (field.GetPoint(fx, fy) != CellType.Empty && !IsSelf(new Vector(fx, fy)))
                        return false;
                }
            }
            return true;
        }

        private int GetLeftmostBlock(int row) =>
            Enumerable.Range(0, _shapeMatrix.GetLength(1)).FirstOrDefault(i => _shapeMatrix[row, i] == 1);

        private int GetRightmostBlock(int row) =>
            Enumerable.Range(0, _shapeMatrix.GetLength(1)).Reverse().FirstOrDefault(i => _shapeMatrix[row, i] == 1);
    }
}

using System;
using System.Drawing;

namespace TetraSnake
{
    public class Field
    {
        public event Action<int> ScoreChanged;
        public event Action<int> LinesCleared;

        private int[,] _tileMap;
        private Vector _size;

        public Vector Size => _size;

        public Field(Size size, int[,] arr = null, int scale = 0)
        {
            InitTileMap(size, arr, scale);
        }

        public void UpdateTileMap(Size size, int[,] arr, int scale = 0)
        {
            if (_tileMap != null && arr != null && _tileMap.SequenceEqual2D(arr)) return;
            InitTileMap(size, arr, scale);
        }

        private void InitTileMap(Size size, int[,] arr, int scale)
        {
            if (arr != null)
            {
                _size = new Vector(arr.GetLength(1), arr.GetLength(0));
                _tileMap = arr;
            }
            else
            {
                _size = new Vector(size.Width / scale, size.Height / scale);
                _tileMap = new int[_size.Y, _size.X];
            }
        }

        public void SetPoint(Vector position, CellType type)
        {
            if (IsInside(position))
                _tileMap[position.Y, position.X] = (int)type;
        }

        public void SetPoint(int x, int y, CellType type)
        {
            if (!IsOutBoundX(x) && !IsOutBoundY(y))
                _tileMap[y, x] = (int)type;
        }

        public CellType GetPoint(Vector position)
        {
            return IsInside(position) ? (CellType)_tileMap[position.Y, position.X] : CellType.Empty;
        }

        public CellType GetPoint(int x, int y)
        {
            return !IsOutBoundX(x) && !IsOutBoundY(y) ? (CellType)_tileMap[y, x] : CellType.Empty;
        }

        public void SetArray(int[,] array, Vector position, CellType type = CellType.Tetris)
        {
            for (int y = 0; y < array.GetLength(0); y++)
                for (int x = 0; x < array.GetLength(1); x++)
                    if (array[y, x] != 0)
                    {
                        int fx = position.X + x, fy = position.Y + y;
                        if (IsInside(new Vector(fx, fy))) SetPoint(fx, fy, type);
                    }
        }

        public bool IsInside(Vector position)
        {
            return position.X >= 0 && position.X < _size.X && position.Y >= 0 && position.Y < _size.Y;
        }

        public bool IsOutBoundX(int x) => x < 0 || x >= _size.X;
        public bool IsOutBoundY(int y) => y < 0 || y >= _size.Y;
        public bool IsFree(Vector position) => _tileMap[position.Y, position.X] == (int)CellType.Empty;
        public void Clear() => Array.Clear(_tileMap, 0, _tileMap.Length);

        public void ClearCells(Vector position, Vector size)
        {
            for (int y = position.Y; y < position.Y + size.Y; y++)
                for (int x = position.X; x < position.X + size.X; x++)
                    _tileMap[y, x] = 0;
        }

        private void LowerBlocks(int[,] figure, Vector position, int y)
        {
            for (y--; y >= 0; y--)
                for (int x = 0; x < _size.X; x++)
                {
                    if (_tileMap[y, x] != 1) continue;

                    bool skip = false;
                    for (int fy = 0; fy < figure.GetLength(0) && !skip; fy++)
                        for (int fx = 0; fx < figure.GetLength(1); fx++)
                            if (figure[fy, fx] != 0 && position.X + fx == x && position.Y + fy == y)
                            {
                                skip = true;
                                break;
                            }

                    if (skip) continue;

                    if (y + 1 < _size.Y)
                    {
                        _tileMap[y + 1, x] = _tileMap[y, x];
                        _tileMap[y, x] = 0;
                    }
                }
        }

        public int CheckFullLine(int[,] figure, Vector position, int count = 0)
        {
            bool clear = false, full = false;

            for (int y = _size.Y - 1; y >= 0; y--)
            {
                for (int x = 0; x < _size.X; x++)
                {
                    if (full)
                    {
                        _tileMap[y, x] = 0;
                        if (x == _size.X - 1)
                        {
                            full = false;
                            clear = true;
                            LinesCleared?.Invoke(1);
                        }
                    }
                    else
                    {
                        if (_tileMap[y, x] == 0) break;
                        if (x == _size.X - 1)
                        {
                            full = true;
                            x = -1;
                        }
                    }
                }

                if (clear)
                {
                    count++;
                    LowerBlocks(figure, position, y);
                    clear = false;
                    ScoreChanged?.Invoke(CheckFullLine(figure, position, count));
                }
            }
            return (int)(Math.Pow(count, 2.5) * 250 + count * 500);
        }
    }
}

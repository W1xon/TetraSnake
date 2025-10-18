using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetraSnake
{
    public class Field
    {
        public static int score;
        public static int clearLine = 0;
        private int[,] _tileMap;
        private Vector _size;

        public Field(Size size, int[,] arr = null, int scale = 0)
        {
            _size = new Vector();
            if (arr != null)
            {
                _size.X = arr.GetLength(1);
                _size.Y = arr.GetLength(0);
                _tileMap = arr;
            }
            else
            {
                _size.Y = size.Height / scale;
                _size.X = size.Width / scale;
                _tileMap = new int[_size.Y, _size.X];
            }
        }
        public void ChangeTileMap(Size size, int[,] arr, int scale = 0)
        {
            if (_tileMap != null && arr != null && _tileMap.SequenceEqual2D(arr))
                return;

            _size = new Vector();

            if (arr != null)
            {
                _size.X = arr.GetLength(1);
                _size.Y = arr.GetLength(0);
                _tileMap = arr;
            }
            else
            {
                _size.Y = size.Height / scale;
                _size.X = size.Width / scale;
                _tileMap = new int[_size.Y, _size.X];
            }
        }


        public Vector Size
        {
            get => _size;
        }
        public void SetPoint(Vector position, CellType type)
        {
            if (IsOutBound(position)) return;
            _tileMap[position.Y, position.X] = (int)type;
        }

        public void SetPoint(int x, int y, CellType type)
        {
            if (IsOutBoundX(x) || IsOutBoundY(y)) return;
            _tileMap[y, x] = (int)type;
        }


        public CellType GetPoint(Vector position)
        {
            if (IsOutBound(position))
                return CellType.Empty; 
            return (CellType)_tileMap[position.Y, position.X];
        }

        public CellType GetPoint(int x, int y)
        {
            if (IsOutBoundX(x) || IsOutBoundY(y))
                return CellType.Empty;
            return (CellType)_tileMap[y, x];
        }
        public void SetArray(int[,] array, Vector position, CellType type = CellType.Tetris)
        {
            for (int y = 0; y < array.GetLength(0); y++)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    if (array[y, x] != 0)
                    {
                        int fx = position.X + x;
                        int fy = position.Y + y;

                        if (fx >= 0 && fx < Size.X && fy >= 0 && fy < Size.Y)
                        {
                            SetPoint(fx, fy, type);
                        }
                    }
                }
            }
        }

        public bool IsOutBound(Vector position)
        {
            return position.X < 0
                   || position.X >= _tileMap.GetLength(1)
                   || position.Y < 0
                   || position.Y >= _tileMap.GetLength(0);
        }
        public bool IsOutBoundX(int x)
        {
            return x < 0 || x >= _tileMap.GetLength(1);
        }
        public bool IsOutBoundY(int y)
        {
            return y < 0 || y >= _tileMap.GetLength(0);
        }
        public bool IsFreeForApple(Vector position)
        {
            return _tileMap[position.Y, position.X] == 1 || _tileMap[position.Y, position.X] == 3;
        }
        public void Clear()
        {
            Array.Clear(_tileMap, 0, _tileMap.Length);
        }

        public void ClearCells(Vector position, Vector size)
        {
            for (int y = position.Y; y < position.Y + size.Y; y++)
            {
                for (int x = position.X; x < position.X + size.X; x++)
                {
                    _tileMap[y, x] = 0;
                }
            }
        }
        private void LowerBlocks(int[,] figure, Vector position, int y)
        {

            bool equal = false;
            for (y--; y >= 0; y--)
            {
                for (int x = 0; x < _tileMap.GetLength(1); x++)
                {
                    if (_tileMap[y, x] != 1)
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
                            if ((position.X + figureX) == x && (position.Y + figureY) == y)
                            {
                                equal = true;
                                break;
                            }
                        }
                    }

                    if (equal == true)
                        continue;

                    if (y + 1 >= _tileMap.GetLength(0))
                        _tileMap[y, x] = _tileMap[y - 1, x];
                    else 
                    {
                        _tileMap[y + 1, x] = _tileMap[y, x];
                        _tileMap[y, x] = 0;
                    }
                }
            }

        }
        public int CheckFullLine(int[,] figure, Vector position, int countclearLine = 0)
        {
            bool clearLin = false;
            bool fullLine = false;
            for (int y = _tileMap.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < _tileMap.GetLength(1); x++)
                {
                    if (fullLine)
                    {
                        _tileMap[y, x] = 0;
                        if (x == _tileMap.GetLength(1) - 1)
                        {
                            fullLine = false;
                            clearLin = true;
                            clearLine++;
                        }
                    }
                    else
                    {
                        if (_tileMap[y, x] == 0)
                            break;
                        if (x == _tileMap.GetLength(1) - 1)
                        {
                            fullLine = true;
                            x = -1;
                        }
                    }
                }
                if (clearLin)
                {
                    countclearLine++;
                    LowerBlocks(figure, position, y);
                    clearLin = false;
                    score += CheckFullLine(figure, position, countclearLine);
                }
            }
            return countclearLine * (Shape.Figures.Count) * (1000 + (countclearLine * countclearLine * 100 / 5));
        }
    }
}

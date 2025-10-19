using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TetraSnake
{
    public class Tetris : Game
    {
        public int TimeUpdateDown = 50;
        private int _nextFigureIndex;
        public List<int[,]> Tetraminos = StoreTetramino.Tetramino;
        private readonly Random _random = new Random();

        public Tetris(GameController controller) : base(controller)
        {
            _gameController.TetrisKeyPressed += key =>
            {
                if (key == Keys.Left) _gameController.Tetramino.HorizontalDirection = -1;
                else if (key == Keys.Right) _gameController.Tetramino.HorizontalDirection = 1;
                else if (key == Keys.Up) _gameController.Tetramino.CanRotate = true;
            };
        }

        public override void Update()
        {
            int index = _nextFigureIndex >= Tetraminos.Count ? Tetraminos.Count - 1 : _nextFigureIndex;
            _gameController.ShapePreview.UpdateTileMap(_gameController.PBoxGame.Size, Tetraminos[index]);
            _gameController.RenderPreview();
            _gameController.Tetramino.Move(_gameController.GameField);
            if (!_gameController.SnakeGame.IsStarted)
                _gameController.RenderGame();
        }

        public void SetLevelParameters(int lvl = 1)
        {
            Level = lvl;
            if (GameSettings.TetrisUpdateTime.TryGetValue(Level, out var times))
            {
                TimeUpdate = times.TimeUpdate;
                TimeUpdateDown = times.TimeUpdateDown;
            }
        }

        public int[,] GetNextTetraminoMatrix()
        {
            int index = _nextFigureIndex >= Tetraminos.Count ? Tetraminos.Count - 1 : _nextFigureIndex;
            var matrix = new int[Tetraminos[index].GetLength(0), Tetraminos[index].GetLength(1)];
            Array.Copy(Tetraminos[index], matrix, Tetraminos[index].Length);
            _nextFigureIndex = _random.Next(Tetraminos.Count);
            return matrix;
        }
    }
}

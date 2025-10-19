using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public class Snake : Game
    {
        
        public  Action<int> ScoreChanged
        {
            set
            {
                _snakeLogic.ScoreChanged += value;
            }
        }

        private SnakeLogic _snakeLogic;
        public bool CanDieFromTetramino
        {
            get => _snakeLogic.CanDieFromTetramino;
            set => _snakeLogic.CanDieFromTetramino = value;
        }

        public Snake(GameController controller) : base(controller)
        {
            _snakeLogic = new SnakeLogic();
            _snakeLogic.Died += Reset;
            _gameController.SnakeKeyPressed += key =>
            {
                switch (key)
                {
                    case Keys.W: if (_snakeLogic.Direction.Y != 1) _snakeLogic.Direction = new Vector(0, -1); break;
                    case Keys.S: if (_snakeLogic.Direction.Y != -1) _snakeLogic.Direction = new Vector(0, 1); break;
                    case Keys.A: if (_snakeLogic.Direction.X != 1) _snakeLogic.Direction = new Vector(-1, 0); break;
                    case Keys.D: if (_snakeLogic.Direction.X != -1) _snakeLogic.Direction = new Vector(1, 0); break;
                }
            };
        }

        public override void Update()
        {
            _snakeLogic.Apple.Draw(_gameController.GameField);
            _snakeLogic.UpdateMovement(_gameController.GameField, _gameController.Tetramino);
        } 

        public void SetLevelParameters(int lvl = 1)
        {
            Level = lvl;
            if (GameSettings.SnakeUpdateTime.TryGetValue(Level, out int time))
                TimeUpdate = time;
        }
        public void Spawn(Field field) => _snakeLogic.Initialize(field);
    }
}
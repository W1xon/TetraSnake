using System;

namespace TetraSnake
{
    public class Game
    {
        public event Action? GameOver;

        public int TimeUpdate { get; protected set; } = 100;
        public int Level { get;  set; } = 1;
        public bool IsStarted { get;  set; }

        protected readonly GameController _gameController;

        protected Game(GameController controller)
        {
            _gameController = controller;
        }

        public void Reset()
        {
            GameOver?.Invoke();
            _gameController.ShowMenuRestart();
        }

        public virtual void Update() { }
    }
}
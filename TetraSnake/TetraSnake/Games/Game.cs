namespace TetraSnake
{
    public class Game
    {
        
        public int TimeUpdate = 100;
        public int Level = 1;
        public bool IsStarted;
        protected readonly GameController _gameController;

        protected Game(GameController controller)
        {
            _gameController = controller;
        }
    }
}
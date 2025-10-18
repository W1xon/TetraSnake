namespace TetraSnake
{
    public class Snake : Game
    {
       
        public Snake(GameController controller) : base(controller)
        {
        }


        public void SetLevelParameters(int lvl = 1)
        {
            Level = lvl;
            switch (Level)
            {
                case 1:
                    TimeUpdate = 100;
                    break;
                case 2:
                    TimeUpdate = 70;
                    break;
                case 3:
                    TimeUpdate = 40;
                    break;
            }
        }

        public void Reset(bool showingMenu = true)
        {
            DataReset();
            if(showingMenu)
                _gameController.ShowMenuRestart();
        }
        public void DataReset()
        {
            _gameController.SaveLevel();
            Records.SaveRecord(_gameController);
            Field.score = 0;
            _gameController.GameField.Clear();
            LogicSnake.SpawnSnake(_gameController.GameField);
            LogicSnake.isDeath = false;
        }
        public void Update()
        {
            _gameController.TryRestart();
            LogicSnake.Move(_gameController.GameField, _gameController.Shape);
            Apple.Draw(_gameController.GameField);
        } 

    }
}
namespace TetraSnake
{
    public class Tetris : Game
    {
        public  int BlockSizeFutureFigure = 25;
        public  int timeUpdateDown = 50;

        public Tetris(GameController controller) : base(controller)
        {
        }


        public void SetLevelParameters(int lvl = 1)
        {
            Level = lvl;
            switch (Level)
            {
                case 1:
                    TimeUpdate = 400;
                    timeUpdateDown = 50;
                    break;

                case 2:
                    TimeUpdate = 200;
                    timeUpdateDown = 30;
                    break;
                case 3:
                    TimeUpdate = 100;
                    timeUpdateDown = 10;
                    
                    break;
            }
        }

        public void Update()
        {
            _gameController.ShapePreview.ChangeTileMap(_gameController.PBoxGame.Size,
                Shape.Figures[_gameController.Shape.NextFigureIndex >= Shape.Figures.Count ? Shape.Figures.Count -1 : _gameController.Shape.NextFigureIndex]);
            DrawGame.Draw(_gameController.ShapePreview, _gameController.PBoxPreview, BlockSizeFutureFigure);
            
            _gameController.Shape.Move(_gameController.GameField);
            
            if(!_gameController.SnakeGame.IsStarted)
                DrawGame.Draw(_gameController.GameField, _gameController.PBoxGame, _gameController.PBoxGame.Height / _gameController.GameField.Size.Y);
        }
        public  void DataReset()
        {
            _gameController.SaveLevel();
            Records.SaveRecord(_gameController);
            Field.score = 0;
            Field.clearLine = 0;
            _gameController.GameField.Clear();
            _gameController.Shape.Spawn(_gameController.GameField);
        }
        public void Reset(bool showingMenu = true)
        {
            DataReset();
            if(showingMenu)
                _gameController.ShowMenuRestart();
        }
    }
}
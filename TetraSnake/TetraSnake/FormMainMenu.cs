using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormMainMenu : Form
    {
        public static FormTetraSnake formTetraSnake;
        private readonly GameController _gameController = new GameController();
        public FormMainMenu()
        {
            InitializeComponent();
        }
        private void buttonStartTetraSnake_Click(object sender, EventArgs e)
        {
            _gameController.StartTetraSnakeGame();
            ShowTetraSnakeWindow();
        }

        private void buttonStartTetris_Click(object sender, EventArgs e)
        {
            _gameController.StartTetrisGame();
            ShowTetraSnakeWindow();
        }

        private void buttonStartSnake_Click(object sender, EventArgs e)
        {
            _gameController.StartSnakeGame();
            ShowTetraSnakeWindow();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonRecord_Click(object sender, EventArgs e)
        {
            FormRecord frmRecord = new FormRecord();
            frmRecord.ShowDialog();
        }

        private void ShowTetraSnakeWindow()
        {
            formTetraSnake = new FormTetraSnake();
            formTetraSnake.SetGameController(_gameController);
            formTetraSnake.Show();
            this.Hide();
        }
    }
}

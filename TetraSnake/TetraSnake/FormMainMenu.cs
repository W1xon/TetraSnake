using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();
        }
        private void buttonStartTetraSnake_Click(object sender, EventArgs e)
        {
            ShowTetraSnakeWindow(GameType.TetraSnake);
        }

        private void buttonStartTetris_Click(object sender, EventArgs e)
        {
            ShowTetraSnakeWindow(GameType.Tetris);
        }

        private void buttonStartSnake_Click(object sender, EventArgs e)
        {
            ShowTetraSnakeWindow(GameType.Snake);
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

        private void ShowTetraSnakeWindow(GameType type)
        {
            var formTetraSnake = new FormTetraSnake(type);
            formTetraSnake.Show();
            Hide();
        }
    }
}

using System;
using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormMainMenu : Form
    {
        public static FormTetraSnake frmTS = new FormTetraSnake();
        public FormMainMenu()
        {
            InitializeComponent();
        }
        private void buttonStartTetraSnake_Click(object sender, EventArgs e)
        {
            Game.IsStartedSnake = true;
            Game.IsStartedTetris = true; 
            frmTS = new FormTetraSnake();
            frmTS.Show();
            this.Hide();
        }

        private void buttonStartTetris_Click(object sender, EventArgs e)
        {
            Game.IsStartedTetris = true;
            frmTS = new FormTetraSnake();
            frmTS.Show();
            this.Hide();
        }

        private void buttonStartSnake_Click(object sender, EventArgs e)
        {
            Game.IsStartedSnake = true;
            frmTS = new FormTetraSnake();
            frmTS.Show();
            this.Hide();
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
    }
}

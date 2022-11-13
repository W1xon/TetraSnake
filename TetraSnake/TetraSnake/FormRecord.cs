using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormRecord : Form
    {
        public FormRecord()
        {
            InitializeComponent();


            // заполняем label  данными
            //змейка
            labelLvl1S.Text = "Lvl 1:\n" + Game.RecordScoreSnake[0];
            labelLvl2S.Text = "Lvl 2:\n" + Game.RecordScoreSnake[1];
            labelLvl3S.Text = "Lvl 3:\n" + Game.RecordScoreSnake[2];
            //тетерис
            labelLvl1T.Text = "Lvl 1:\n" + Game.RecordScoreTetris[0];
            labelLvl2T.Text = "Lvl 2:\n" + Game.RecordScoreTetris[1];
            labelLvl3T.Text = "Lvl 3:\n" + Game.RecordScoreTetris[2];
            //змейка + тетрис
            labelLvl1TS.Text = "Lvl 1:\n" + Game.RecordScoreTetraSnake[0];
            labelLvl2TS.Text = "Lvl 2:\n" + Game.RecordScoreTetraSnake[1];
            labelLvl3TS.Text = "Lvl 3:\n" + Game.RecordScoreTetraSnake[2];

        }
    }
}

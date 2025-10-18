using System.Windows.Forms;

namespace TetraSnake
{
    public partial class FormRecord : Form
    {
        public FormRecord()
        {
            InitializeComponent();


            //змейка
            labelLvl1S.Text = "Lvl 1:\n" +  Records.RecordScoreSnake[0];
            labelLvl2S.Text = "Lvl 2:\n" +  Records.RecordScoreSnake[1];
            labelLvl3S.Text = "Lvl 3:\n" +  Records.RecordScoreSnake[2];
            //тетерис
            labelLvl1T.Text = "Lvl 1:\n" +  Records.RecordScoreTetris[0];
            labelLvl2T.Text = "Lvl 2:\n" +  Records.RecordScoreTetris[1];
            labelLvl3T.Text = "Lvl 3:\n" +  Records.RecordScoreTetris[2];
            //змейка + тетрис
            labelLvl1TS.Text = "Lvl 1:\n" + Records.RecordScoreTetraSnake[0];
            labelLvl2TS.Text = "Lvl 2:\n" + Records.RecordScoreTetraSnake[1];
            labelLvl3TS.Text = "Lvl 3:\n" + Records.RecordScoreTetraSnake[2];

        }
    }
}

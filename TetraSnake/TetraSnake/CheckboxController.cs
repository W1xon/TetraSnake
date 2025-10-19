using System.Windows.Forms;

namespace TetraSnake
{
    public class CheckboxController
    {
        private readonly CheckBox[] _checkboxes = new CheckBox[3];
        
        public void InitializeCheckboxes(params CheckBox[] checkboxes)
        {
            for (int i = 0; i < checkboxes.Length; i++)
            {
                if (_checkboxes[0] == null)
                    checkboxes[0].Checked = true;

                _checkboxes[i] = checkboxes[i];
            }
        }
        public void SelectCheckbox(int index)
        {
            for (int i = 0; i < _checkboxes.Length; i++)
            {
                _checkboxes[i].Checked = (i == index);
            }
        }
        public int GetCheckedLevel() => GetCheckedIndex() + 1;

        public int GetCheckedIndex()
        {
            for (int i = 0; i < _checkboxes.Length; i++)
            {
                if (_checkboxes[i].Checked)
                    return i;
            }
            return 0;
        }
    }
}
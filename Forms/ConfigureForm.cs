
#region " Imports "
using System;
using System.Text;
using System.Windows.Forms;
using Reflexil.Utils;
using Reflexil.Properties;
#endregion

namespace Reflexil.Forms
{
	public partial class ConfigureForm: Form
    {

        #region " Events "
        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            foreach (ENumericBase item in System.Enum.GetValues(typeof(ENumericBase)))
            {
                this.InputBase.Items.Add(item);
                this.RowBase.Items.Add(item);
                this.OperandBase.Items.Add(item);
            }
            this.InputBase.SelectedItem = Settings.Default.InputBase;
            this.RowBase.SelectedItem = Settings.Default.RowIndexDisplayBase;
            this.OperandBase.SelectedItem = Settings.Default.OperandDisplayBase;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Settings.Default.InputBase = (ENumericBase)this.InputBase.SelectedItem;
            Settings.Default.RowIndexDisplayBase = (ENumericBase)this.RowBase.SelectedItem;
            Settings.Default.OperandDisplayBase = (ENumericBase)this.OperandBase.SelectedItem;
            Settings.Default.Save();
        }
        #endregion

        #region " Methods "
        public ConfigureForm()
        {
            InitializeComponent();
        }
        #endregion

	}
}
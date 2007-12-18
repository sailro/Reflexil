/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System;
using System.Windows.Forms;
using Reflexil.Compilation;
using Reflexil.Properties;
using Reflexil.Utils;
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
            foreach (ESupportedLanguage item in System.Enum.GetValues(typeof(ESupportedLanguage)))
            {
                this.Language.Items.Add(item);
            }
            this.InputBase.SelectedItem = Settings.Default.InputBase;
            this.RowBase.SelectedItem = Settings.Default.RowIndexDisplayBase;
            this.OperandBase.SelectedItem = Settings.Default.OperandDisplayBase;
            this.Language.SelectedItem = Settings.Default.Language;
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Settings.Default.InputBase = (ENumericBase)this.InputBase.SelectedItem;
            Settings.Default.RowIndexDisplayBase = (ENumericBase)this.RowBase.SelectedItem;
            Settings.Default.OperandDisplayBase = (ENumericBase)this.OperandBase.SelectedItem;
            Settings.Default.Language = (ESupportedLanguage)this.Language.SelectedItem;
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
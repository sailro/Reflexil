/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using Reflexil.Utils;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Editors
{
	public partial class TextComboUserControl: UserControl
    {

        #region " Fields "
        private ENumericBase m_previousbase = ENumericBase.Dec;
        #endregion

        #region " Properties "
        public ENumericBase PreviousBase
        {
            get
            {
                return m_previousbase;
            }
            private set
            {
                m_previousbase = value;
            }
        }

        public ENumericBase CurrentBase
        {
            get
            {
                return (ENumericBase)BaseCombo.SelectedItem;
            }
            private set
            {
                BaseCombo.SelectedItem = value;
            }
        }

        public bool UseBaseSelector
        {
            get
            {
                return !ItemSplitContainer.Panel2Collapsed;
            }
            set
            {
                ItemSplitContainer.Panel2Collapsed = !value;
            }
        }

        public string Value
        {
            get
            {
                return (UseBaseSelector) ? OperandDisplayHelper.Changebase(TextBox.Text, CurrentBase, ENumericBase.Dec) : TextBox.Text;
            }
            set
            {
                TextBox.Text = (UseBaseSelector) ? OperandDisplayHelper.Changebase(value, ENumericBase.Dec, CurrentBase) : value;
            }
        }
        #endregion

        #region " Events "
        private void BaseCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TextBox.Text = OperandDisplayHelper.Changebase(TextBox.Text, PreviousBase, CurrentBase);
            PreviousBase = CurrentBase;
        }
        #endregion

        #region " Methods "
        public TextComboUserControl()
        {
            InitializeComponent();
            foreach (ENumericBase item in System.Enum.GetValues(typeof(ENumericBase)))
            {
                BaseCombo.Items.Add(item);
            }
            BaseCombo.SelectedItem = Properties.Settings.Default.InputBase;
            PreviousBase = CurrentBase;
        }
        #endregion

	}
}

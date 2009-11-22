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
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
	public partial class ConstantEditor: UserControl
    {

        #region " Properties "
        public object Constant
        {
            get
            {
                if (ConstantTypes.SelectedItem != null)
                {
                    IOperandEditor editor = (IOperandEditor)ConstantTypes.SelectedItem;
                    return editor.SelectedOperand;
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    if (ConstantTypes.Items.Count > 0)
                    {
                        ConstantTypes.SelectedItem = ConstantTypes.Items[0];
                    }
                } 
                else 
                {
                    foreach (IOperandEditor editor in ConstantTypes.Items)
                    {
                        if (editor.IsOperandHandled(value))
                        {
                            ConstantTypes.SelectedItem = editor;
                            editor.SelectedOperand = value;
                        }
                    }
                }
            }
        }
        #endregion

        #region " Events "
        protected virtual void ConstantTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConstantPanel.Controls.Clear();
            ConstantPanel.Controls.Add((Control)ConstantTypes.SelectedItem);
            ((IOperandEditor)ConstantTypes.SelectedItem).Initialize(null);
        }
        #endregion

        #region " Methods "
        public ConstantEditor()
        {
            InitializeComponent();

            ConstantTypes.Items.Add(new NullOperandEditor());
            ConstantTypes.Items.Add(new ByteEditor());
            ConstantTypes.Items.Add(new SByteEditor());
            ConstantTypes.Items.Add(new IntegerEditor());
            ConstantTypes.Items.Add(new LongEditor());
            ConstantTypes.Items.Add(new SingleEditor());
            ConstantTypes.Items.Add(new DoubleEditor());
            ConstantTypes.Items.Add(new StringEditor());

            ConstantTypes.SelectedIndex = 0;
        }
        #endregion

    }
}

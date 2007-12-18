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
using Mono.Cecil;
using Reflexil.Editors;
#endregion

namespace Reflexil.Forms
{
    public partial class ParameterForm : Reflexil.Forms.TypeSpecificationForm
    {

        #region " Fields "
        private MethodDefinition m_mdef;
        private ParameterDefinition m_selectedparameter;
        #endregion

        #region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }

        public ParameterDefinition SelectedParameter
        {
            get
            {
                return m_selectedparameter;
            }
        }
        #endregion

        #region " Methods "
        public ParameterForm()
        {
            InitializeComponent();
        }

        protected ParameterDefinition CreateParameter()
        {
            ParameterDefinition prm = new ParameterDefinition(MethodDefinition.DeclaringType.Module.Import(TypeSpecificationEditor.SelectedTypeReference));
            prm.Name = ItemName.Text;
            prm.Attributes = (Attributes.Item as ParameterDefinition).Attributes;
            if (prm.HasDefault)
            {
                if (ConstantTypes.SelectedItem != null)
                {
                    IGlobalOperandEditor editor = (IGlobalOperandEditor)ConstantTypes.SelectedItem;
                    prm.Constant = editor.CreateObject();
                }
            }
            return prm;
        }

        public virtual DialogResult ShowDialog(MethodDefinition mdef, ParameterDefinition selected)
        {
            m_mdef = mdef;
            m_selectedparameter = selected;
            return base.ShowDialog(mdef);
        }

        public void FillControls(MethodDefinition mdef)
        {
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

        #region " Events "
        protected virtual void ConstantTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConstantPanel.Controls.Clear();
            ConstantPanel.Controls.Add((Control)ConstantTypes.SelectedItem);
            if (MethodDefinition != null)
            {
                ((IGlobalOperandEditor)ConstantTypes.SelectedItem).Initialize(MethodDefinition);
            }
        }
        #endregion

    }
}


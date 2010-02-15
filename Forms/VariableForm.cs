/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Forms
{
    public partial class VariableForm : Reflexil.Forms.TypeSpecificationForm
    {

        #region " Fields "
        private MethodDefinition m_mdef;
        private VariableDefinition m_selectedvariable;
        #endregion

        #region " Properties "
        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }

        public VariableDefinition SelectedVariable
        {
            get
            {
                return m_selectedvariable;
            }
        }
        #endregion

        #region " Methods "
        public VariableForm()
        {
            InitializeComponent();
        }

        protected VariableDefinition CreateVariable()
        {
            VariableDefinition result = new VariableDefinition(MethodDefinition.DeclaringType.Module.Import(TypeSpecificationEditor.SelectedTypeReference));
            result.Name = ItemName.Text;
            result.Method = MethodDefinition;
            result.Index = 0;
            return result;
        }

        public virtual DialogResult ShowDialog(MethodDefinition mdef, VariableDefinition selected)
        {
            m_mdef = mdef;
            m_selectedvariable = selected;
            return base.ShowDialog(mdef);
        }
        #endregion

    }
}


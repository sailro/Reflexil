
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            VariableDefinition result = new VariableDefinition(TypeSpecificationEditor.SelectedTypeReference);
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


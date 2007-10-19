
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            ParameterDefinition prm = new ParameterDefinition(TypeSpecificationEditor.SelectedTypeReference);
            prm.Name = ItemName.Text;
            prm.Attributes = (Attributes.Item as ParameterDefinition).Attributes;
            if (prm.HasDefault)
            {
                if (ConstantTypes.SelectedItem != null)
                {
                    IGlobalOperandEditor editor = (IGlobalOperandEditor)ConstantTypes.SelectedItem;
                    SelectedParameter.Constant = editor.CreateObject();
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


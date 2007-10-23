
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
	public partial class MethodAttributesControl: UserControl
    {

        #region " Fields "
        private MethodDefinition m_mdef;
        private bool m_readonly;
        #endregion

        #region " Properties "
        public bool ReadOnly
        {
            get
            {
                return m_readonly;
            }
            set
            {
                m_readonly = value;
                Enabled = !value;
            }
        }

        public MethodDefinition MethodDefinition
        {
            get
            {
                return m_mdef;
            }
        }
        #endregion

        #region " Methods "
        public MethodAttributesControl()
        {
            InitializeComponent();
            CallingConvention.DataSource = System.Enum.GetValues(typeof(Mono.Cecil.MethodCallingConvention));
        }

        public void Bind(MethodDefinition mdef, Dictionary<string, string> prefixes)
        {
            m_mdef = mdef;
            Attributes.Bind(mdef, prefixes);
            if (!ReadOnly)
            {
                Enabled = (mdef != null);
            }

            if (mdef != null)
            {
                CallingConvention.SelectedItem = mdef.CallingConvention;
                RVA.Text = mdef.RVA.ToString(); 
                ReturnType.SelectedTypeReference = mdef.ReturnType.ReturnType;
            }
            else
            {
                CallingConvention.SelectedIndex = -1;
                RVA.Text = string.Empty;
                ReturnType.SelectedTypeReference = null;
            }
        }
        #endregion

        #region " Events "
        private void CallingConvention_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (MethodDefinition != null)
            {
                MethodDefinition.CallingConvention = (Mono.Cecil.MethodCallingConvention)CallingConvention.SelectedItem;
            }
        }

        private void ReturnType_Validating(object sender, CancelEventArgs e)
        {
            bool validated;
            if (ReturnType.SelectedTypeReference is TypeSpecification)
            {
                TypeSpecification tspec = ReturnType.SelectedTypeReference as TypeSpecification;
                validated = tspec.ElementType != null;
            }
            else
            {
                validated = ReturnType.SelectedTypeReference != null;
            }

            if (!validated)
            {
                ErrorProvider.SetError(ReturnType, "Type is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(ReturnType, string.Empty);
                if (MethodDefinition != null)
                {
                    MethodDefinition.ReturnType.ReturnType = ReturnType.SelectedTypeReference;
                }
            }
        }
        #endregion

    }
}

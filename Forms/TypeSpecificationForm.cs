
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Forms
{
	public partial class TypeSpecificationForm: Form
    {

        #region " Properties "
        protected bool IsFormComplete
        {
            get
            {
                foreach (Control ctl in Controls)
                {
                    ctl.Focus();
                    if (!Validate()) return false;
                }
                return true;
            }
        }
        #endregion

        #region " Methods "
        public TypeSpecificationForm()
        {
            InitializeComponent();
        }

        public virtual DialogResult ShowDialog(MethodDefinition mdef)
        {
            TypeSpecificationEditor.MethodDefinition = mdef;
            return base.ShowDialog();
        }
        #endregion

        #region " Events "
        private void ItemName_Validating(object sender, CancelEventArgs e)
        {
            if (ItemName.Text == string.Empty)
            {
                ErrorProvider.SetError(ItemName, "Name is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(ItemName, string.Empty);
            }
        }

        private void TypeSpecificationEditor_Validating(object sender, CancelEventArgs e)
        {
            bool validated;
            if (TypeSpecificationEditor.SelectedTypeReference is TypeSpecification)
            {
                TypeSpecification tspec = TypeSpecificationEditor.SelectedTypeReference as TypeSpecification;
                validated = tspec.ElementType != null;
            }
            else
            {
                validated = TypeSpecificationEditor.SelectedTypeReference != null;
            }

            if (!validated)
            {
                ErrorProvider.SetError(TypeSpecificationEditor, "Type is mandatory");
                e.Cancel = true;
            }
            else
            {
                ErrorProvider.SetError(TypeSpecificationEditor, string.Empty);
            }
        }
        #endregion

	}
}
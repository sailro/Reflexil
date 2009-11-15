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
using System.ComponentModel;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
    /// <summary>
    /// Method attributes editor (all object readable/writeable non indexed properties)
    /// </summary>
    public partial class MethodAttributesControl : BaseMethodAttributesControl 
    {

        #region " Consts "
        private const string MEMBER_ACCESS_MASK = "Member access";
        private const string VTABLE_LAYOUT_MASK = "VTable layout";
        private const string CODE_TYPE_MASK = "Code type";
        private const string MANAGED_MASK = "Managed";

        private readonly string[] MEMBER_ACCESS_PROPERTIES = { "IsCompilerControlled", "IsPrivate", "IsFamilyAndAssembly", "IsAssembly", "IsFamily", "IsFamilyOrAssembly", "IsPublic" };
        private readonly string[] VTABLE_LAYOUT_PROPERTIES = { "IsReuseSlot", "IsNewSlot" };
        private readonly string[] CODE_TYPE_PROPERTIES = { "IsIL", "IsNative", "IsRuntime" };
        private readonly string[] MANAGED_PROPERTIES = { "IsUnmanaged", "IsManaged" };
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public MethodAttributesControl()
        {
            InitializeComponent();
            FillPrefixes(m_prefixes, MEMBER_ACCESS_MASK, MEMBER_ACCESS_PROPERTIES);
            FillPrefixes(m_prefixes, VTABLE_LAYOUT_MASK, VTABLE_LAYOUT_PROPERTIES);
            FillPrefixes(m_prefixes, CODE_TYPE_MASK, CODE_TYPE_PROPERTIES);
            FillPrefixes(m_prefixes, MANAGED_MASK, MANAGED_PROPERTIES);
            CallingConvention.DataSource = System.Enum.GetValues(typeof(Mono.Cecil.MethodCallingConvention));
        }

        /// <summary>
        /// Bind a method definition to this control
        /// </summary>
        /// <param name="mdef">Method definition to bind</param>
        public override void Bind(MethodDefinition mdef)
        {
            base.Bind(mdef);
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
        /// <summary>
        /// Handle combobox change event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        private void CallingConvention_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Item != null)
            {
                Item.CallingConvention = (Mono.Cecil.MethodCallingConvention)CallingConvention.SelectedItem;
            }
        }

        /// <summary>
        /// Handle text box validation
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
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
                if (Item != null)
                {
                    Item.ReturnType.ReturnType = ReturnType.SelectedTypeReference;
                }
            }
        }
        #endregion

    }

    #region " VS Designer generic support "
    public class BaseMethodAttributesControl : SplitAttributesControl<MethodDefinition>
    {
    }
    #endregion
}

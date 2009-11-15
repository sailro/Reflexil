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
	public partial class AssemblyDefinitionControl: UserControl
	{

        #region " Fields "
        private bool m_readonly;
        private AssemblyDefinition m_item;
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

        public AssemblyDefinition Item
        {
            get
            {
                return m_item;
            }
            set
            {
                m_item = value;
            }
        }
        #endregion

        #region " Events "
        private void ResetEntryPoint_Click(object sender, EventArgs e)
        {
            MethodDefinitionEditor.SelectedOperand = null;
            Item.EntryPoint = null;
        }

        private void MethodDefinitionEditor_Validated(object sender, EventArgs e)
        {
            Item.EntryPoint = MethodDefinitionEditor.SelectedOperand;
        }

        private void TargetRuntime_Validated(object sender, EventArgs e)
        {
            Item.Runtime = (TargetRuntime)TargetRuntime.SelectedItem;
        }

        private void Kind_Validated(object sender, EventArgs e)
        {
            Item.Kind = (AssemblyKind)Kind.SelectedItem;
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public AssemblyDefinitionControl()
        {
            InitializeComponent();
            MethodDefinitionEditor.Dock = DockStyle.None;
            Kind.DataSource = System.Enum.GetValues(typeof(AssemblyKind));
            TargetRuntime.DataSource = System.Enum.GetValues(typeof(TargetRuntime));
        }

        /// <summary>
        /// Bind an AssemblyDefinition to this control
        /// </summary>
        /// <param name="item">AssemblyDefinition to bind</param>
        public virtual void Bind(AssemblyDefinition item)
        {
            Item = item;

            if (item != null)
            {
                MainModule.DataSource = item.Modules;
                MainModule.SelectedItem = item.MainModule;
                Kind.SelectedItem = item.Kind;
                TargetRuntime.SelectedItem = item.Runtime;
                MethodDefinitionEditor.SelectedOperand = item.EntryPoint;
                MethodDefinitionEditor.AssemblyRestriction = item;
            }
            else
            {
                Kind.SelectedIndex = -1;
                TargetRuntime.SelectedIndex = -1;
                MainModule.SelectedIndex = -1;
                MethodDefinitionEditor.SelectedOperand = null;
            }

            if (!ReadOnly)
            {
                Enabled = (item != null);
            }
        }
        #endregion

	}
}

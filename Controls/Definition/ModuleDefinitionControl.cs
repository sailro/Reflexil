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
using System;
using System.Windows.Forms;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
	public partial class ModuleDefinitionControl: UserControl
	{

        #region " Fields "
        private bool m_readonly;
        private ModuleDefinition m_item;
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

        public ModuleDefinition Item
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
        private void TargetRuntime_Validated(object sender, EventArgs e)
        {
            Item.Runtime = (TargetRuntime)TargetRuntime.SelectedItem;
        }

        private void Kind_Validated(object sender, EventArgs e)
        {
            Item.Kind = (ModuleKind)Kind.SelectedItem;
        }
        #endregion

        #region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
        public ModuleDefinitionControl()
        {
            InitializeComponent();
            Kind.DataSource = System.Enum.GetValues(typeof(ModuleKind));
            TargetRuntime.DataSource = System.Enum.GetValues(typeof(TargetRuntime));
        }

        /// <summary>
        /// Bind an ModuleDefinition to this control
        /// </summary>
        /// <param name="item">ModuleDefinition to bind</param>
        public virtual void Bind(ModuleDefinition item)
        {
            Item = item;

            if (item != null)
            {
                Kind.SelectedItem = item.Kind;
                TargetRuntime.SelectedItem = item.Runtime;
            }
            else
            {
                Kind.SelectedIndex = -1;
                TargetRuntime.SelectedIndex = -1;
            }

            if (!ReadOnly)
            {
                Enabled = (item != null);
            }
        }
        #endregion

	}
}

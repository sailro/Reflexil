/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

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

        private void Architecture_Validated(object sender, EventArgs e)
        {
            Item.Architecture = (TargetArchitecture)Architecture.SelectedItem;
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
            Architecture.DataSource = System.Enum.GetValues(typeof(TargetArchitecture));
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
                Architecture.SelectedItem = item.Architecture;
            }
            else
            {
                Kind.SelectedIndex = -1;
                TargetRuntime.SelectedIndex = -1;
                Architecture.SelectedIndex = -1;
            }

            if (!ReadOnly)
            {
                Enabled = (item != null);
            }
        }
        #endregion

	}
}

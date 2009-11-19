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
using System.Collections.Generic;
using System.Windows.Forms;
using Reflexil.Handlers;
#endregion

namespace Reflexil.Forms
{
	/// <summary>
	/// Main reflexil window
	/// </summary>
	public partial class ReflexilWindow
	{
		
		#region " Fields "
		private List<IHandler> m_handlers = new List<IHandler>();
		#endregion
		
		#region " Methods "
        /// <summary>
        /// Constructor
        /// </summary>
		public ReflexilWindow() : base()
		{
			InitializeComponent();
            DoubleBuffered = true;

            NotSupportedHandler nsh = new NotSupportedHandler();

            m_handlers.Add(new AssemblyDefinitionHandler());
            m_handlers.Add(new AssemblyNameReferenceHandler());
            m_handlers.Add(new ModuleDefinitionHandler());
            m_handlers.Add(new TypeDefinitionHandler());
			m_handlers.Add(new MethodDefinitionHandler());
            m_handlers.Add(new PropertyDefinitionHandler());
            m_handlers.Add(nsh);

            foreach (IHandler handler in m_handlers)
            {
                (handler as Control).Dock = DockStyle.Fill;
                if (handler != nsh)
                {
                    nsh.LabInfo.Text += " - " + handler.Label + "\n";
                }
            }
		}
		
        /// <summary>
        /// Handle browser tree item
        /// </summary>
        /// <param name="item">Item to handle</param>
        public IHandler HandleItem(object item)
		{
            foreach (IHandler handler in m_handlers)
            {
                if (handler.IsItemHandled(item))
                {
                    handler.HandleItem(item);
                    if (!(GroupBox.Controls.Count > 0 && GroupBox.Controls[0].Equals(handler)))
                    {
                        GroupBox.Controls.Clear();
                        GroupBox.Controls.Add(handler as Control);
                        GroupBox.Text = handler.Label;
                    }
                    return handler;
                }
            }
            return null;
		}

        /// <summary>
        /// Handle configure button click 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">attributes</param>
        private void Configure_Click(object sender, EventArgs e)
        {
            using (ConfigureForm frm = new ConfigureForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    foreach (IHandler Handler in m_handlers)
                    {
                        Handler.OnConfigurationChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Handle Strong Name button click 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">attributes</param>
        private void SNRemover_Click(object sender, EventArgs e)
        {
            using (StrongNameRemoverForm frm = new StrongNameRemoverForm())
            {
                frm.ShowDialog();
            }
        }
        #endregion

	}
}


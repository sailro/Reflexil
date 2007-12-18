/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
			
			m_handlers.Add(new ModuleDefinitionHandler());
            m_handlers.Add(new TypeDefinitionHandler());
			m_handlers.Add(new MethodDefinitionHandler());
			m_handlers.Add(new NotSupportedHandler());
		}
		
        /// <summary>
        /// Handle reflector tree item
        /// </summary>
        /// <param name="item">Item to handle</param>
		public void HandleItem(object item)
		{
			TabControl.TabPages.Clear();
			foreach (IHandler Handler in m_handlers)
			{
				if (Handler.IsItemHandled(item))
				{
					TabPage tabpage = new TabPage(Handler.Label);
					TabControl.TabPages.Add(tabpage);
					if ((Handler) is Control)
					{
						Control ctl = (Control) Handler;
						tabpage.Controls.Add(ctl);
						ctl.Dock = DockStyle.Fill;
					}
					tabpage.Tag = Handler;
					Handler.HandleItem(item);
                    break;
				}
			}
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
        #endregion

	}
}


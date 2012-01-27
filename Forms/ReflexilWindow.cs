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
using System.Collections.Generic;
using System.Windows.Forms;
using Reflexil.Handlers;
using System.Text;
using System.Diagnostics;
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
            m_handlers.Add(new FieldDefinitionHandler());
            m_handlers.Add(new EventDefinitionHandler());
            m_handlers.Add(new EmbeddedResourceHandler());
            m_handlers.Add(new LinkedResourceHandler());
            m_handlers.Add(new AssemblyLinkedResourceHandler());
            m_handlers.Add(nsh);

            foreach (IHandler handler in m_handlers)
            {
                (handler as Control).Dock = DockStyle.Fill;
                if (handler != nsh)
                {
                    nsh.LabInfo.Text += " - " + handler.Label + "\n";
                }
            }

#if DEBUG
            PGrid.Visible = true;
#endif
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
                    }

                    StringBuilder builder = new StringBuilder(handler.Label);
                    GroupBox.Text = builder.ToString();

                    if (handler.TargetObject != null)
                    {
                        builder.Append(" - ");
                        builder.Append(handler.TargetObject.ToString());
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

        /// <summary>
        /// Debug PGrid button click 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">attributes</param>
        private void PGrid_Click(object sender, EventArgs e)
        {
            using (PropertyGridForm frm = new PropertyGridForm())
            {
                frm.ShowDialog();
            }
        }


        #endregion

	}
}


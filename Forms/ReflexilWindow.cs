
#region " Imports "
using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Windows.Forms;
using Reflexil.Editors;
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


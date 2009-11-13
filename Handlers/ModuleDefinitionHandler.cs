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
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Forms;
using Reflexil.Utils;
using Reflexil.Plugins;
#endregion

namespace Reflexil.Handlers
{
	
	public partial class ModuleDefinitionHandler : IHandler
	{
		
		#region " Fields "
		private AssemblyDefinition m_adef;
		private string m_originallocation;
		#endregion
		
		#region " Properties "
		public AssemblyDefinition AssemblyDefinition
		{
			get
			{
				return m_adef;
			}
		}
		
		public string OriginalLocation
		{
			get
			{
				return m_originallocation;
			}
		}
		
		public bool IsItemHandled(object item)
		{
            return PluginFactory.GetInstance().IsModuleDefinitionHandled(item);
		}
		
		public string Label
		{
			get
			{
				return "Module definition";
			}
		}
		#endregion
		
		#region " Events "
        public void OnConfigurationChanged(object sender, EventArgs e)
        {
        }

		private void ButSaveAs_Click(Object sender, EventArgs e)
		{
            if (AssemblyDefinition != null)
            {
                SaveFileDialog.InitialDirectory = Path.GetDirectoryName(OriginalLocation);
                SaveFileDialog.FileName = Path.GetFileNameWithoutExtension(OriginalLocation) + ".Patched" + Path.GetExtension(OriginalLocation);
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        AssemblyFactory.SaveAssembly(AssemblyDefinition, SaveFileDialog.FileName);
                        if ((AssemblyDefinition.Name.Flags & AssemblyFlags.PublicKey) != 0)
                        {
                            using (StrongNameForm snform = new StrongNameForm())
                            {
                                snform.AssemblyDefinition = AssemblyDefinition;
                                snform.ShowDialog();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Reflexil is unable to save this assembly: {0}", ex.Message));
                    }
                }
            }
            else
            {
                MessageBox.Show("Assembly definition is not loaded (not a CLI image?)");
            }
		}
		
		private void ButReload_Click(Object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure to reload assembly, discarding all changes?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
                IAssemblyContext context = PluginFactory.GetInstance().ReloadAssemblyContext(OriginalLocation);
                if (context != null)
                {
                    m_adef = context.AssemblyDefinition;
                }
                else
                {
                    m_adef = null;
                }
			}
		}
		#endregion
		
		#region " Methods "
        public ModuleDefinitionHandler() : base()
        {
            InitializeComponent();
        }

		public void HandleItem(object item)
		{
            m_originallocation = PluginFactory.GetInstance().GetModuleLocation(item);
            IAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(m_originallocation);
            if (context != null)
            {
                m_adef = context.AssemblyDefinition;
            }
            else
            {
                m_adef = null;
            }
		}
		#endregion
		
	}
	
}



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
using System.IO;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Forms;
using Reflexil.Utils;
using Reflexil.Plugins;
using Reflexil.Verifier;
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

        object IHandler.TargetObject
        {
            get { return m_adef.MainModule; }
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
            AssemblyHelper.SaveAssembly(AssemblyDefinition, OriginalLocation);
		}
		
		private void ButReload_Click(Object sender, EventArgs e)
		{
            m_adef = AssemblyHelper.ReloadAssembly(OriginalLocation);
		}

        private void ButVerify_Click(object sender, EventArgs e)
        {
            AssemblyHelper.VerifyAssembly(AssemblyDefinition, OriginalLocation);
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



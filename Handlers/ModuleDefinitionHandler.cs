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
		private ModuleDefinition m_mdef;
		#endregion
		
		#region " Properties "
        public ModuleDefinition ModuleDefinition
		{
			get
			{
				return m_mdef;
			}
		}
				
		public bool IsItemHandled(object item)
		{
            return PluginFactory.GetInstance().IsModuleDefinitionHandled(item);
		}

        object IHandler.TargetObject
        {
            get { return m_mdef; }
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
        #endregion
		
		#region " Methods "
        public ModuleDefinitionHandler() : base()
        {
            InitializeComponent();
        }

		public void HandleItem(object item)
		{
            string location = PluginFactory.GetInstance().GetModuleLocation(item);
            IAssemblyContext context = PluginFactory.GetInstance().GetAssemblyContext(location);
            if (context != null)
            {
                m_mdef = context.AssemblyDefinition.MainModule;
            }
            else
            {
                m_mdef = null;
            }
            Definition.Bind(m_mdef);
		}
		#endregion
	}
	
}



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
            CustomAttributes.Rehash();
        }

        private void CustomAttributes_GridUpdated(object sender, EventArgs e)
        {
            CustomAttributes.Rehash();
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
            CustomAttributes.Bind(m_mdef);
		}
		#endregion
	}
	
}



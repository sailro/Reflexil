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
using Mono.Cecil;
using Reflexil.Utils;
using Reflexil.Plugins;
#endregion

namespace Reflexil.Handlers
{
	
	public partial class MethodDefinitionHandler : IHandler
    {

        #region " Fields "
        private MethodDefinition m_mdef;
        private bool m_readonly;
		#endregion
		
		#region " Properties "
        public bool ReadOnly
        {
            get {
                return m_readonly;
            }
            set
            {
                Instructions.ReadOnly = value;
                ExceptionHandlers.ReadOnly = value;
                Variables.ReadOnly = value;
                Parameters.ReadOnly = value;
                Overrides.ReadOnly = value;
                Attributes.ReadOnly = value;
                m_readonly = value;
            }
        }

        object IHandler.TargetObject
        {
            get { return m_mdef; }
        }

		public string Label
		{
			get
			{
				return "Method definition";
			}
		}
        		
		public MethodDefinition MethodDefinition
		{
			get
			{
				return m_mdef;
			}
		}
		#endregion
       
        #region " Events "
        private void Instructions_GridUpdated(object sender, EventArgs e)
        {
            if (m_mdef.Body != null)
            {
                CecilHelper.UpdateInstructionsOffsets(m_mdef.Body);
            }
            Instructions.Rehash();
            ExceptionHandlers.Rehash();
        }

        private void ExceptionHandlers_GridUpdated(object sender, EventArgs e)
        {
            ExceptionHandlers.Rehash();
        }

        private void Variables_GridUpdated(object sender, EventArgs e)
        {
            Variables.Rehash();
            Instructions.Rehash();
        }

        private void Parameters_GridUpdated(object sender, EventArgs e)
        {
            Parameters.Rehash();
            Instructions.Rehash();
        }

        private void Overrides_GridUpdated(object sender, EventArgs e)
        {
            Overrides.Rehash();
        }

        private void CustomAttributes_GridUpdated(object sender, EventArgs e)
        {
            CustomAttributes.Rehash();
        }

        public void OnConfigurationChanged(object sender, EventArgs e)
        {
            Instructions.Rehash();
            ExceptionHandlers.Rehash();
            Variables.Rehash();
            Parameters.Rehash();
            CustomAttributes.Rehash();
        }

        private void Instructions_BodyReplaced(object sender, EventArgs e)
        {
            HandleItem(MethodDefinition);
        }
        #endregion
	
		#region " Methods "
        public MethodDefinitionHandler() : base()
        {
            InitializeComponent();
            m_readonly = false;
        }

        public bool IsItemHandled(object item)
        {
            return PluginFactory.GetInstance().IsMethodDefinitionHandled(item);
        }

        public void HandleItem(MethodDefinition mdef)
        {
            m_mdef = mdef;
            Instructions.Bind(mdef);
            Variables.Bind(mdef);
            ExceptionHandlers.Bind(mdef);
            Parameters.Bind(mdef);
            Overrides.Bind(mdef);
            Attributes.Bind(mdef);
            CustomAttributes.Bind(mdef);
        }

		public void HandleItem(object item)
		{
            HandleItem(PluginFactory.GetInstance().GetMethodDefinition(item));
		}
        #endregion

    }
}



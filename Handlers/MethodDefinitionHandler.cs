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

        public void OnConfigurationChanged(object sender, EventArgs e)
        {
            Instructions.Rehash();
            ExceptionHandlers.Rehash();
            Variables.Rehash();
            Parameters.Rehash();
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
            Instructions.Bind(m_mdef);
            Variables.Bind(m_mdef);
            ExceptionHandlers.Bind(m_mdef);
            Parameters.Bind(m_mdef);
            Overrides.Bind(m_mdef);
            Attributes.Bind(mdef);
        }

		public void HandleItem(object item)
		{
            HandleItem(PluginFactory.GetInstance().GetMethodDefinition(item));
		}
        #endregion

    }
}



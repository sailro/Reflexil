
#region " Imports "
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflector.CodeModel;
using Reflexil.Utils;
using Reflexil.Forms;
using Reflexil.Wrappers;
using Reflexil.Properties;
using Reflexil.Editors;
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
            return (item) is IMethodDeclaration;
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
			IMethodDeclaration mdec = (IMethodDeclaration) item;
            HandleItem(CecilHelper.ReflectorMethodToCecilMethod(mdec));
		}
        #endregion

    }
}




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

        #region " Consts "
        private const string MEMBER_ACCESS_MASK = "Member access";
        private const string VTABLE_LAYOUT_MASK = "VTable layout";
        private const string CODE_TYPE_MASK = "Code type";
        private const string MANAGED_MASK = "Managed";

        private readonly string[] MEMBER_ACCESS_PROPERTIES = { "IsCompilerControlled", "IsPrivate", "IsFamilyAndAssembly", "IsAssembly", "IsFamily", "IsFamilyOrAssembly", "IsPublic" };
        private readonly string[] VTABLE_LAYOUT_PROPERTIES = { "IsReuseSlot", "IsNewSlot" };
        private readonly string[] CODE_TYPE_PROPERTIES = { "IsIL", "IsNative", "IsRuntime" };
        private readonly string[] MANAGED_PROPERTIES = { "IsUnmanaged", "IsManaged" };
        #endregion

        #region " Fields "
        private MethodDefinition m_mdef;
        private bool m_readonly;
        Dictionary<string, string> m_prefixes = new Dictionary<string, string>();
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
                Attributes.ReadOnly = value;
                m_readonly = value;
            }
        }

		public bool IsItemHandled(object item)
		{
			return (item) is IMethodDeclaration;
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
        public void FillPrefixes(Dictionary<string, string> prefixes, string prefix, string[] items) {
            foreach (string item in items)
            {
                prefixes.Add(item, prefix);
            }
        }

        public MethodDefinitionHandler() : base()
        {
            InitializeComponent();
            m_readonly = false;

            FillPrefixes(m_prefixes, MEMBER_ACCESS_MASK, MEMBER_ACCESS_PROPERTIES);
            FillPrefixes(m_prefixes, VTABLE_LAYOUT_MASK, VTABLE_LAYOUT_PROPERTIES);
            FillPrefixes(m_prefixes, CODE_TYPE_MASK, CODE_TYPE_PROPERTIES);
            FillPrefixes(m_prefixes, MANAGED_MASK, MANAGED_PROPERTIES);
        }

        public void HandleItem(MethodDefinition mdef)
        {
            m_mdef = mdef;
            Instructions.Bind(m_mdef);
            Variables.Bind(m_mdef);
            ExceptionHandlers.Bind(m_mdef);
            Parameters.Bind(m_mdef);
            Attributes.Bind(mdef, m_prefixes);
        }

		public void HandleItem(object item)
		{
			IMethodDeclaration mdec = (IMethodDeclaration) item;
            HandleItem(CecilHelper.ReflectorMethodToCecilMethod(mdec));
		}
        #endregion

    }
}



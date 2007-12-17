
#region " Imports "
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Mono.Cecil;
using Reflector.CodeModel;
using Reflexil.Utils;
#endregion

namespace Reflexil.Handlers
{
	public partial class TypeDefinitionHandler: UserControl, IHandler
    {

        #region " Methods "
        public TypeDefinitionHandler()
		{
			InitializeComponent();
		}

        bool IHandler.IsItemHandled(object item)
        {
            return (item) is ITypeDeclaration;
        }

        string IHandler.Label
        {
            get {
                return "Type definition";
            }
        }

        void IHandler.HandleItem(object item)
        {
            ITypeDeclaration tdec = item as ITypeDeclaration;
            HandleItem(CecilHelper.ReflectorTypeToCecilType(tdec));
        }

        void HandleItem(TypeDefinition tdef)
        {
            Attributes.Bind(tdef);
        }

        void IHandler.OnConfigurationChanged(object sender, EventArgs e)
        {
        }
        #endregion
        
    }
}

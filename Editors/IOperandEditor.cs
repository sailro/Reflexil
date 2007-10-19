
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{

    public interface IOperandEditor<T> : IGlobalOperandEditor
	{
		
		#region " Properties "
        T SelectedOperand
        {
            get;
            set;
        }
		#endregion

	}
	
}


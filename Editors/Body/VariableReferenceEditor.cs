
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Wrappers;
using System.Collections;
#endregion

namespace Reflexil.Editors
{
    class VariableReferenceEditor : GenericOperandReferenceEditor<VariableDefinition, VariableWrapper>
	{
        public VariableReferenceEditor() : base()
        {
        }

        public VariableReferenceEditor(ICollection referenceditems) : base(referenceditems)
		{
		}
	}
}


#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Wrappers;
using System.Collections;
#endregion

namespace Reflexil.Editors
{
    class ParameterReferenceEditor : GenericOperandReferenceEditor<ParameterDefinition, Wrappers.ParameterWrapper>
	{
        public ParameterReferenceEditor() : base()
        {
        }

        public ParameterReferenceEditor(ICollection referenceditems) : base(referenceditems)
		{
		}
	}
}

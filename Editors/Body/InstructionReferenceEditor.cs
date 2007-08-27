
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Wrappers;
using System.Collections;
#endregion

namespace Reflexil.Editors
{
    class InstructionReferenceEditor : GenericOperandReferenceEditor<Instruction, InstructionWrapper>
	{
        public InstructionReferenceEditor() : base()
        {
        }

        public InstructionReferenceEditor(ICollection referenceditems) : base(referenceditems)
		{
		}
	}
}

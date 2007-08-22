
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{
	
	public partial class TypeReferenceEditor : GenericMemberReferenceEditor<TypeReference>
	{
		
		#region " Methods "
		public override Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
            return worker.Create(opcode, MethodDefinition.DeclaringType.Module.Import(SelectedOperand));
		}
		#endregion
		
	}
	
}

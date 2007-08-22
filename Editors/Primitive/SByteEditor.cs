
#region " Imports "
using System;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{
	
	public partial class SByteEditor : GenericOperandEditor<SByte>
	{
		
		#region " Methods "
		public override Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, SelectedOperand);
		}
		#endregion
		
	}
	
}


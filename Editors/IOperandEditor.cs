
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{
	
	public interface IOperandEditor
	{
		
		#region " Properties "
		string Label{
			get;
		}
		bool IsOperandHandled(object operand);
		#endregion
		
		#region " Methods "
		void Initialize(MethodDefinition mdef);
		void SelectOperand(object operand);
		Instruction CreateInstruction(CilWorker worker, OpCode opcode);
		#endregion
		
	}
	
}


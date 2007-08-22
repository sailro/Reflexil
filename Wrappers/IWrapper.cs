
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Wrappers
{
	
	public interface IWrapper<T>
	{
		
		#region " Properties "
		T Item {
			get;
			set;
		}
		MethodDefinition MethodDefinition{
			get;
			set;
		}
		#endregion
		
		#region " Methods "
		Instruction CreateInstruction(CilWorker worker, OpCode opcode);
		#endregion
		
	}
	
}


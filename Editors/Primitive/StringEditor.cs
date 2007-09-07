
#region " Imports "
using System;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Editors
{
	
	public partial class StringEditor : GenericOperandEditor<string>
	{
		
		#region " Methods "
        public StringEditor() : base()
        {
            UseBaseSelector = false;
        }

		public override Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, SelectedOperand);
		}
		#endregion
		
	}
	
}


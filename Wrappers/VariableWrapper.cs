
#region " Imports "
using System;
using Mono.Cecil.Cil;
using Mono.Cecil;
#endregion

namespace Reflexil.Wrappers
{
	
	public partial class VariableWrapper : IWrapper<VariableDefinition>
	{
		
		#region " Fields "
		private MethodDefinition m_mdef;
		private VariableDefinition m_variable;
		#endregion
		
		#region " Properties "
        public VariableDefinition Item
		{
			get
			{
				return m_variable;
			}
			set
			{
				m_variable = value;
			}
		}
		
		public MethodDefinition MethodDefinition
		{
			get
			{
				return m_mdef;
			}
			set
			{
				m_mdef = value;
			}
		}
		#endregion
		
		#region " Methods "
		public VariableWrapper()
		{
		}
		
		public VariableWrapper(VariableDefinition variable, MethodDefinition mdef)
		{
			m_variable = variable;
			m_mdef = mdef;
		}
		
		public override string ToString()
		{
			return OperandDisplayHelper.ToString(m_variable);
		}
		
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, Item);
		}
		#endregion
		
	}
	
}


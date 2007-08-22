
#region " Imports "
using System;
using Mono.Cecil.Cil;
using Mono.Cecil;
#endregion

namespace Reflexil.Wrappers
{
	
	public partial class InstructionWrapper : IWrapper<Instruction>, ICloneable
	{
			
		#region " Fields "
		private MethodDefinition m_mdef;
		private Instruction m_instruction;
		#endregion
		
		#region " Properties "
        public Instruction Item
		{
			get
			{
				return m_instruction;
			}
			set
			{
				m_instruction = value;
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
		public InstructionWrapper()
		{
		}
		
		public InstructionWrapper(Instruction instruction, MethodDefinition mdef)
		{
			m_instruction = instruction;
			m_mdef = mdef;
		}
		
		public override string ToString()
		{
			if (m_mdef != null)
			{
				return OperandDisplayHelper.ToString(m_mdef, m_instruction, true);
			}
			return string.Empty;
		}
		
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, Item);
		}
		
		public object Clone()
		{
			return new InstructionWrapper(m_instruction, m_mdef);
		}
		#endregion
		
	}
	
}


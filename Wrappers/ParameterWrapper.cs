
#region " Imports "
using System;
using Mono.Cecil.Cil;
using Mono.Cecil;
#endregion

namespace Reflexil.Wrappers
{
	
	public partial class ParameterWrapper : IWrapper<ParameterDefinition>
	{
		
		#region " Fields "
		private MethodDefinition m_mdef;
		private ParameterDefinition m_parameter;
		#endregion
		
		#region " Properties "
        public ParameterDefinition Item
		{
			get
			{
				return m_parameter;
			}
			set
			{
				m_parameter = value;
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
		public ParameterWrapper()
		{
		}
		
		public ParameterWrapper(ParameterDefinition parameter, MethodDefinition mdef)
		{
			m_parameter = parameter;
			m_mdef = mdef;
		}
		
		public override string ToString()
		{
			return OperandDisplayHelper.ToString(m_parameter);
		}
		
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, Item);
		}
		#endregion
		
	}
	
}


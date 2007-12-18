/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Wrappers
{
	/// <summary>
	/// Instruction wrapper
	/// </summary>
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
        /// <summary>
        /// Default constructor
        /// </summary>
		public InstructionWrapper()
		{
		}
		
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="instruction">Instruction to wrap</param>
        /// <param name="mdef">Method definition</param>
		public InstructionWrapper(Instruction instruction, MethodDefinition mdef)
		{
			m_instruction = instruction;
			m_mdef = mdef;
		}
		
        /// <summary>
        /// Returns a String that represents the wrapped instruction
        /// </summary>
        /// <returns>See OperandDisplayHelper.ToString</returns>
		public override string ToString()
		{
			if (m_mdef != null)
			{
				return OperandDisplayHelper.ToString(m_mdef, m_instruction, true);
			}
			return string.Empty;
		}

        /// <summary>
        /// Create an instruction, using the wrapped item as an operand
        /// </summary>
        /// <param name="worker">Cil worker</param>
        /// <param name="opcode">Instruction opcode</param>
        /// <returns></returns>
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, Item);
		}
		
        /// <summary>
        /// Clone the current wrapper
        /// </summary>
        /// <returns>A new InstructionWrapper instance</returns>
		public object Clone()
		{
			return new InstructionWrapper(m_instruction, m_mdef);
		}
		#endregion
		
	}
}


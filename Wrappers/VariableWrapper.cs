/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
using Mono.Cecil;
using Mono.Cecil.Cil;
#endregion

namespace Reflexil.Wrappers
{
	/// <summary>
	/// Variable wrapper
	/// </summary>
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
        /// <summary>
        /// Default constructor
        /// </summary>
		public VariableWrapper()
		{
		}
		
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="variable">Variable to wrap</param>
        /// <param name="mdef">Method definition</param>
		public VariableWrapper(VariableDefinition variable, MethodDefinition mdef)
		{
			m_variable = variable;
			m_mdef = mdef;
		}

        /// <summary>
        /// Returns a String that represents the wrapped instruction
        /// </summary>
        /// <returns>See OperandDisplayHelper.ToString</returns>
		public override string ToString()
		{
			return OperandDisplayHelper.ToString(m_variable);
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
		#endregion
		
	}
}


/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
    /// Generic item wrapping interface 
	/// </summary>
	/// <typeparam name="T">Wrapped item type</typeparam>
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
        /// <summary>
        /// Create an instruction, using the wrapped item as an operand
        /// </summary>
        /// <param name="worker">Cil worker</param>
        /// <param name="opcode">Instruction opcode</param>
        /// <returns></returns>
		Instruction CreateInstruction(CilWorker worker, OpCode opcode);
		#endregion
		
	}	
}


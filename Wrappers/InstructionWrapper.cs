/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region Imports

using System;
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Wrappers
{
	/// <summary>
	/// Instruction wrapper
	/// </summary>
	public class InstructionWrapper : IWrapper<Instruction>, ICloneable
	{
		#region Properties

		public Instruction Item { get; set; }

		public MethodDefinition MethodDefinition { get; set; }

		#endregion

		#region Methods

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
			Item = instruction;
			MethodDefinition = mdef;
		}

		/// <summary>
		/// Returns a String that represents the wrapped instruction
		/// </summary>
		/// <returns>See OperandDisplayHelper.ToString</returns>
		public override string ToString()
		{
			return MethodDefinition != null ? OperandDisplayHelper.ToString(MethodDefinition, Item, true) : string.Empty;
		}

		/// <summary>
		/// Create an instruction, using the wrapped item as an operand
		/// </summary>
		/// <param name="worker">Cil worker</param>
		/// <param name="opcode">Instruction opcode</param>
		/// <returns></returns>
		public Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return worker.Create(opcode, Item);
		}

		/// <summary>
		/// Clone the current wrapper
		/// </summary>
		/// <returns>A new InstructionWrapper instance</returns>
		public object Clone()
		{
			return new InstructionWrapper(Item, MethodDefinition);
		}

		#endregion
	}
}
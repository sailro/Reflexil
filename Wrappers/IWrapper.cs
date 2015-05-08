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
		#region Properties

		T Item { get; set; }
		MethodDefinition MethodDefinition { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Create an instruction, using the wrapped item as an operand
		/// </summary>
		/// <param name="worker">Cil worker</param>
		/// <param name="opcode">Instruction opcode</param>
		/// <returns></returns>
		Instruction CreateInstruction(ILProcessor worker, OpCode opcode);

		#endregion
	}
}
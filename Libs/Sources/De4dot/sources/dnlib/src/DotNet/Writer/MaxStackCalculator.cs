/*
    Copyright (C) 2012-2014 de4dot@gmail.com

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
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
    CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

﻿using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;

namespace dnlib.DotNet.Writer {
	/// <summary>
	/// Calculates max stack usage by using a simple pass over all instructions. This value
	/// can be placed in the fat method header's MaxStack field.
	/// </summary>
	public struct MaxStackCalculator {
		readonly IList<Instruction> instructions;
		readonly IList<ExceptionHandler> exceptionHandlers;
		readonly Dictionary<Instruction, int> stackHeights;
		int errors;

		/// <summary>
		/// Gets max stack value
		/// </summary>
		/// <param name="instructions">All instructions</param>
		/// <param name="exceptionHandlers">All exception handlers</param>
		/// <returns>Max stack value</returns>
		public static uint GetMaxStack(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers) {
			uint maxStack;
			new MaxStackCalculator(instructions, exceptionHandlers).Calculate(out maxStack);
			return maxStack;
		}

		/// <summary>
		/// Gets max stack value
		/// </summary>
		/// <param name="instructions">All instructions</param>
		/// <param name="exceptionHandlers">All exception handlers</param>
		/// <param name="maxStack">Updated with max stack value</param>
		/// <returns><c>true</c> if no errors were detected, <c>false</c> otherwise</returns>
		public static bool GetMaxStack(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers, out uint maxStack) {
			return new MaxStackCalculator(instructions, exceptionHandlers).Calculate(out maxStack);
		}

		MaxStackCalculator(IList<Instruction> instructions, IList<ExceptionHandler> exceptionHandlers) {
			this.instructions = instructions;
			this.exceptionHandlers = exceptionHandlers;
			this.stackHeights = new Dictionary<Instruction, int>();
			this.errors = 0;
		}

		bool Calculate(out uint maxStack) {
			foreach (var eh in exceptionHandlers) {
				if (eh == null)
					continue;
				if (eh.TryStart != null)
					stackHeights[eh.TryStart] = 0;
				if (eh.FilterStart != null)
					stackHeights[eh.FilterStart] = 1;
				if (eh.HandlerStart != null) {
					bool pushed = eh.HandlerType == ExceptionHandlerType.Catch || eh.HandlerType == ExceptionHandlerType.Filter;
					stackHeights[eh.HandlerStart] = pushed ? 1 : 0;
				}
			}

			int stack = 0;
			bool resetStack = false;
			foreach (var instr in instructions) {
				if (instr == null)
					continue;

				if (resetStack) {
					stackHeights.TryGetValue(instr, out stack);
					resetStack = false;
				}
				stack = WriteStack(instr, stack);

				if (instr.OpCode.Code == Code.Jmp) {
					if (stack != 0)
						errors++;
				}
				else {
					int pushes, pops;
					instr.CalculateStackUsage(out pushes, out pops);
					if (pops == -1)
						stack = 0;
					else {
						stack -= pops;
						if (stack < 0) {
							errors++;
							stack = 0;
						}
						stack += pushes;
					}
				}
				if (stack < 0) {
					errors++;
					stack = 0;
				}

				switch (instr.OpCode.FlowControl) {
				case FlowControl.Branch:
					WriteStack(instr.Operand as Instruction, stack);
					resetStack = true;
					break;

				case FlowControl.Call:
					if (instr.OpCode.Code == Code.Jmp)
						resetStack = true;
					break;

				case FlowControl.Cond_Branch:
					if (instr.OpCode.Code == Code.Switch) {
						var targets = instr.Operand as IList<Instruction>;
						if (targets != null) {
							foreach (var target in targets)
								WriteStack(target, stack);
						}
					}
					else
						WriteStack(instr.Operand as Instruction, stack);
					break;

				case FlowControl.Return:
				case FlowControl.Throw:
					resetStack = true;
					break;
				}
			}

			stack = 0;
			foreach (var v in stackHeights.Values)
				stack = Math.Max(stack, v);
			maxStack = (uint)stack;
			return errors == 0;
		}

		int WriteStack(Instruction instr, int stack) {
			if (instr == null) {
				errors++;
				return stack;
			}
			int stack2;
			if (stackHeights.TryGetValue(instr, out stack2)) {
				if (stack != stack2)
					errors++;
				return stack2;
			}
			stackHeights[instr] = stack;
			return stack;
		}
	}
}

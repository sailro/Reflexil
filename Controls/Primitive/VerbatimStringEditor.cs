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

using System.Text.RegularExpressions;
using Mono.Cecil.Cil;
using System;

#endregion

namespace Reflexil.Editors
{
	public class VerbatimStringEditor : BaseVerbatimStringEditor
	{
		public override string Label
		{
			get { return "Verbatim String"; }
		}

		public override string SelectedOperand
		{
			get { return Regex.Unescape(base.SelectedOperand); }
			set { base.SelectedOperand = Regex.Escape(value); }
		}

		#region Methods

		public VerbatimStringEditor()
		{
			UseBaseSelector = false;
		}

		public override Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return worker.Create(opcode, Regex.Unescape(SelectedOperand));
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseVerbatimStringEditor : GenericOperandEditor<string>
	{
		public override Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			throw new NotImplementedException();
		}
	}

	#endregion
}
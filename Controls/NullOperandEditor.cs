/* Reflexil Copyright (c) 2007-2021 Sebastien Lebreton

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

using System.Windows.Forms;
using Mono.Cecil.Cil;

namespace Reflexil.Editors
{
	public sealed class NullOperandEditor : TextBox, IOperandEditor<object>, IInstructionOperandEditor
	{
		public string Label
		{
			get { return "[Null]"; }
		}

		public string ShortLabel
		{
			get { return Label; }
		}

		public object SelectedOperand
		{
			get { return null; }
			set { Text = value.ToString(); }
		}

		public NullOperandEditor()
		{
			Dock = DockStyle.Fill;
			ReadOnly = true;
		}

		public bool IsOperandHandled(object operand)
		{
			return false;
		}

		public Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return worker.Create(opcode);
		}

		public void Refresh(object context)
		{
		}
	}
}

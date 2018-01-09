/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System.Collections;
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Reflexil.Editors
{
	public class OperandReferenceEditor<T, TW> : ComboBox, IOperandEditor<T>, IInstructionOperandEditor where TW : class, Wrappers.IWrapper<T>, new()
	{
		object IOperandEditor.SelectedOperand
		{
			get { return SelectedOperand; }
			set { SelectedOperand = (T) value; }
		}

		public T SelectedOperand
		{
			get
			{
				var wrapper = (TW) SelectedItem;
				return wrapper != null ? wrapper.Item : default(T);
			}
			set
			{
				foreach (var wrapper in Items.Cast<TW>().Where(wrapper => (object) wrapper.Item == (object) value))
				{
					SelectedItem = wrapper;
				}
			}
		}

		public string Label
		{
			get { return string.Format("-> {0} reference", ShortLabel); }
		}

		public string ShortLabel
		{
			get { return typeof(TW).Name.Replace("Wrapper", string.Empty); }
		}

		public ICollection ReferencedItems { get; set; }

		public OperandReferenceEditor()
		{
			DropDownStyle = ComboBoxStyle.DropDownList;
		}

		public bool IsOperandHandled(object operand)
		{
			return (operand) is T;
		}

		public OperandReferenceEditor(ICollection referenceditems) : this()
		{
			// ReSharper disable once DoNotCallOverridableMethodsInConstructor
			Dock = DockStyle.Fill;
			ReferencedItems = referenceditems;
		}

		public void Refresh(object context)
		{
			Items.Clear();

			var mdef = context as MethodDefinition;
			if (mdef == null)
				return;

			if (!mdef.HasBody)
				return;

			foreach (var item in from T refItem in ReferencedItems select new TW {Item = refItem, MethodDefinition = mdef})
			{
				Items.Add(item);
			}
		}

		public Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return ((TW) SelectedItem).CreateInstruction(worker, opcode);
		}
	}
}
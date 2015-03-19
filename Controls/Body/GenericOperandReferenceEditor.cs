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

using System.Collections;
using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Editors
{
	public class GenericOperandReferenceEditor<T, TW> : ComboBox, IOperandEditor<T>
		where TW : class, Wrappers.IWrapper<T>, new()
	{
		#region Fields

		private ICollection _referencedItems;

		#endregion

		#region Properties

		object IOperandEditor.SelectedOperand
		{
			get { return SelectedOperand; }
			set { SelectedOperand = (T) value; }
		}

		public T SelectedOperand
		{
			get
			{
				var wrapper = ((TW) SelectedItem);
				return wrapper != null ? wrapper.Item : default(T);
			}
			set
			{
				foreach (var wrapper in Items.Cast<TW>().Where(wrapper => ((object) wrapper.Item) == (object) value))
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
			get { return typeof (TW).Name.Replace("Wrapper", string.Empty); }
		}

		public ICollection ReferencedItems
		{
			get { return _referencedItems; }
			set { _referencedItems = value; }
		}

		#endregion

		#region Methods

		public GenericOperandReferenceEditor()
		{
			DropDownStyle = ComboBoxStyle.DropDownList;
		}

		public bool IsOperandHandled(object operand)
		{
			return (operand) is T;
		}

		public GenericOperandReferenceEditor(ICollection referenceditems)
			: this()
		{
			// ReSharper disable once DoNotCallOverridableMethodsInConstructor
			Dock = DockStyle.Fill;
			_referencedItems = referenceditems;
		}

		public void Initialize(MethodDefinition mdef)
		{
			Items.Clear();
			if (!mdef.HasBody)
				return;

			foreach (var item in from T refItem in _referencedItems select new TW {Item = refItem, MethodDefinition = mdef})
			{
				Items.Add(item);
			}
		}

		public Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return ((TW) SelectedItem).CreateInstruction(worker, opcode);
		}

		#endregion
	}
}
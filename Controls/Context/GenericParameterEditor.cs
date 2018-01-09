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

using System.Linq;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Reflexil.Editors
{
	public class GenericParameterEditor : ComboBox, IOperandEditor<TypeReference>, IInstructionOperandEditor
	{
		public string Label
		{
			get { return "-> Generic parameter"; }
		}

		public string ShortLabel
		{
			get { return Label; }
		}

		object IOperandEditor.SelectedOperand
		{
			get { return SelectedOperand; }
			set { SelectedOperand = (TypeReference) value; }
		}

		public TypeReference SelectedOperand
		{
			get { return (TypeReference) SelectedItem; }
			set { SelectedItem = value; }
		}

		public GenericParameterEditor()
		{
			// ReSharper disable once DoNotCallOverridableMethodsInConstructor
			Dock = DockStyle.Fill;
			DropDownStyle = ComboBoxStyle.DropDownList;
		}

		public bool IsOperandHandled(object operand)
		{
			return (operand) is GenericParameter;
		}

		private void AppendGenericParameters(IGenericParameterProvider provider)
		{
			if (provider == null)
				return;

			foreach (var item in provider.GenericParameters)
				AddGenericParameter(item);

			var mref = provider as MemberReference;
			if (mref != null)
				AppendGenericParameters(mref.DeclaringType);
		}

		private void AddGenericParameter(GenericParameter item)
		{
			if (Items.OfType<GenericParameter>().Any(i => i.ToString() == item.ToString()))
				return;

			Items.Add(item);
		}

		public void Refresh(object context)
		{
			Items.Clear();

			var genericContext = context as IGenericParameterProvider;
			if (genericContext == null)
				return;

			AppendGenericParameters(genericContext);
			Sorted = true;
		}

		public Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return worker.Create(opcode, ((GenericParameter) SelectedItem));
		}
	}
}
 
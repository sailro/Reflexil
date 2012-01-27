/* Reflexil Copyright (c) 2007-2012 Sebastien LEBRETON

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

#region " Imports "
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Editors
{
	
	public partial class GenericTypeReferenceEditor : ComboBox, IOperandEditor<TypeReference>
	{
		
		#region " Properties "
		public string Label
		{
			get
			{
				return "-> Generic type reference";
			}
		}

        public string ShortLabel
        {
            get
            {
                return Label;
            }
        }

        object IOperandEditor.SelectedOperand
        {
            get
            {
                return SelectedOperand;
            }
            set
            {
                SelectedOperand = (TypeReference)value;
            }
        }

        public TypeReference SelectedOperand
        {
            get
            {
                return (TypeReference)this.SelectedItem;
            }
            set
            {
                this.SelectedItem = value;
            }
        }
		#endregion
		
		#region " Methods "
		public GenericTypeReferenceEditor()
		{
			this.Dock = DockStyle.Fill;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
		}

        public bool IsOperandHandled(object operand)
        {
            return (operand) is GenericParameter;
        }

        private void AppendGenericParameters(Mono.Collections.Generic.Collection<GenericParameter> collection)
		{
			foreach (GenericParameter item in collection)
			{
				Items.Add(item);
			}
		}
		
		public void Initialize(MethodDefinition mdef)
		{
			Items.Clear();
			AppendGenericParameters(mdef.GenericParameters);
			AppendGenericParameters(mdef.DeclaringType.GenericParameters);
			this.Sorted = true;
		}
		
		public Instruction CreateInstruction(ILProcessor worker, OpCode opcode)
		{
			return worker.Create(opcode, ((GenericParameter) SelectedItem));
		}
		#endregion
		
	}
	
}



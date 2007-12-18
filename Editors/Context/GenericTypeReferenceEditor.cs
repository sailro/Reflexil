/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

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
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Editors
{
	
	public partial class GenericTypeReferenceEditor : ComboBox, IOperandEditor<TypeReference>
	{
		
		#region " Properties "
		public bool IsOperandHandled(object operand)
		{
			return (operand) is GenericParameter;
		}
		
		public string Label
		{
			get
			{
				return "-> Generic type reference";
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
		
		private void AppendGenericParameters(GenericParameterCollection collection)
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
		
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode, ((GenericParameter) SelectedItem));
		}

        public void SelectOperand(object operand)
        {
            SelectedOperand = (TypeReference)operand;
        }

        public object CreateObject()
        {
            return SelectedOperand;
        }
		#endregion
		
	}
	
}



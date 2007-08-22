
#region " Imports "
using System;
using System.Windows.Forms;
using Mono.Cecil.Cil;
using Mono.Cecil;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Editors
{
	
	public partial class GenericTypeReferenceEditor : ComboBox, IOperandEditor
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
			this.SelectedItem = operand;
		}
		#endregion
		
	}
	
}



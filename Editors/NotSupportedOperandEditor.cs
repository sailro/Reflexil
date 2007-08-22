
#region " Imports "
using System;
using System.Windows.Forms;
using Mono.Cecil.Cil;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
	
	public partial class NotSupportedOperandEditor : TextBox, IOperandEditor
	{
		
		#region " Properties "
		public bool IsOperandHandled(object operand)
		{
			return true;
		}
		
		public string Label
		{
			get
			{
				return "[Not supported]";
			}
		}
		#endregion
		
		#region " Methods "
		public NotSupportedOperandEditor()
		{
			this.Dock = DockStyle.Fill;
			this.ReadOnly = true;
		}
		
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			throw (new NotImplementedException());
		}
		
		public void Initialize(MethodDefinition mdef)
		{
		}
		
		public void SelectOperand(object operand)
		{
			Text = operand.ToString();
		}
		#endregion
		
	}
	
}



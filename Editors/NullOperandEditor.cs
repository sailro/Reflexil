
#region " Imports "
using System;
using System.Windows.Forms;
using Mono.Cecil.Cil;
using Mono.Cecil;
#endregion

namespace Reflexil.Editors
{
	
	public partial class NullOperandEditor : TextBox, IOperandEditor
	{
		
		#region " Properties "
		public bool IsOperandHandled(object operand)
		{
			return false;
		}
		
		public string Label
		{
			get
			{
				return "[None]";
			}
		}
		#endregion
		
		#region " Methods "
		public NullOperandEditor() : base()
		{
			this.Dock = DockStyle.Fill;
			this.ReadOnly = true;
		}
		
		public Instruction CreateInstruction(CilWorker worker, OpCode opcode)
		{
			return worker.Create(opcode);
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


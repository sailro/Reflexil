
#region " Imports "
using System;
using Mono.Cecil.Cil;
using Mono.Cecil;
using System.Windows.Forms;
#endregion

namespace Reflexil.Editors
{
	
	public abstract partial class GenericOperandEditor<T> : TextComboUserControl, IOperandEditor<T>
	{
		
		#region " Properties "
		public bool IsOperandHandled(object operand)
		{
			return (operand) is T;
		}
		
		public string Label
		{
			get
			{
				return typeof(T).Name;
			}
		}

		public T SelectedOperand
		{
			get
			{
                return ((T)(Convert.ChangeType(Value, typeof(T))));
			}
            set
            {
                Value = value.ToString();
            }
		}
		#endregion
		
		#region " Methods "
		public GenericOperandEditor()
		{
			this.Dock = DockStyle.Fill;
		}
		
		public abstract Instruction CreateInstruction(CilWorker worker, OpCode opcode);
		
		public void Initialize(MethodDefinition mdef)
		{
		}
        public void SelectOperand(object operand)
        {
            SelectedOperand = (T) operand;
        }

        public object CreateObject()
        {
            return SelectedOperand;
        }
		#endregion
		
	}
	
}


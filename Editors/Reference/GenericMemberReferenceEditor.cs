
#region " Imports "
using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Windows.Forms;
using Reflexil.Forms;
#endregion

namespace Reflexil.Editors
{
	
	public abstract partial class GenericMemberReferenceEditor<T> : BasePopupEditor, IOperandEditor<T> where T :  MemberReference 
	{
		
		#region " Fields "
		private MethodDefinition m_mdef;
		private T m_operand;
		#endregion
		
		#region " Properties "
		public bool IsOperandHandled(object operand)
		{
			return (operand) is T;
		}
		
		public string Label
		{
			get
			{
				return "-> " + typeof(T).Name.Replace("Reference", string.Empty) + " reference";
			}
		}
		
		public MethodDefinition MethodDefinition
		{
			get
			{
				return m_mdef;
			}
		}
		
		public T SelectedOperand
		{
			get
			{
				return m_operand;
			}
			set
			{
				m_operand = value;
				if (m_operand != null)
				{
					LabCaption.Text = ((MemberReference) value).Name;
				}
				else
				{
					LabCaption.Text = string.Empty;
				}
			}
		}
		#endregion
		
		#region " Events "
		protected override void OnSelectClick(System.Object sender, System.EventArgs e)
		{
			using (GenericMemberReferenceForm<T> refselectform = new GenericMemberReferenceForm<T>(m_operand))
			{
				if (refselectform.ShowDialog() == DialogResult.OK)
				{
					SelectedOperand = (T) refselectform.SelectedItem;
				}
			}
			
		}
		#endregion
		
		#region " Methods "
		public GenericMemberReferenceEditor() : base()
		{
			this.Dock = DockStyle.Fill;
		}
		
		public abstract Instruction CreateInstruction(CilWorker worker, OpCode opcode);
		
		public void Initialize(MethodDefinition mdef)
		{
			m_mdef = mdef;
		}
		
		public void SelectOperand(object operand)
		{
			SelectedOperand = (T) operand;
		}
		#endregion
		
	}
	
}


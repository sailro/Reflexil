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

        public object CreateObject()
        {
            return SelectedOperand;
        }
		#endregion
		
	}
	
}


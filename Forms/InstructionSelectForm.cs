
#region " Imports "
using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Wrappers;
#endregion

namespace Reflexil.Forms
{
	
	public partial class InstructionSelectForm
	{

		#region " Properties "
		public List<Instruction> SelectedInstructions
		{
			get
			{
				List<Instruction> result = new List<Instruction>();
				foreach (InstructionWrapper wrapper in LbxSelection.Items)
				{
					result.Add(wrapper.Item);
				}
				return result;
			}
		}
		#endregion
		
		#region " Events "
		private void LbxInstructions_DoubleClick(object sender, EventArgs e)
		{
			if (LbxInstructions.SelectedItem != null)
			{
				LbxSelection.Items.Add(((ICloneable) LbxInstructions.SelectedItem).Clone());
			}
		}
		
		private void LbxSelection_DoubleClick(object sender, EventArgs e)
		{
			if (LbxSelection.SelectedItem != null)
			{
				LbxSelection.Items.Remove(LbxSelection.SelectedItem);
			}
		}
		
		private void ButTop_Click(Object sender, EventArgs e)
		{
			MoveSelection(0);
		}
		
		private void ButUp_Click(Object sender, EventArgs e)
		{
			MoveSelection(LbxSelection.SelectedIndex - 1);
		}
		
		private void ButDown_Click(Object sender, EventArgs e)
		{
			MoveSelection(LbxSelection.SelectedIndex + 1);
		}
		
		private void ButBottom_Click(Object sender, EventArgs e)
		{
			MoveSelection(LbxSelection.Items.Count - 1);
		}
		#endregion
		
		#region " Methods "
        public InstructionSelectForm() : base()
        {
            InitializeComponent();
        }

		public InstructionSelectForm(MethodDefinition mdef, List<Instruction> instructions, List<Instruction> selectedinstructions)
		{
			InitializeComponent();
			
			foreach (Instruction ins in instructions)
			{
				LbxInstructions.Items.Add(new InstructionWrapper(ins, mdef));
			}
			
			foreach (Instruction ins in selectedinstructions)
			{
				LbxSelection.Items.Add(new InstructionWrapper(ins, mdef));
			}
		}
		
		private void MoveSelection(int newindex)
		{
			object selection = LbxSelection.SelectedItem;
			if ( (selection != null) && (newindex >= 0) && (newindex < LbxSelection.Items.Count) )
			{
				LbxSelection.Items.Remove(selection);
				LbxSelection.Items.Insert(newindex, selection);
				LbxSelection.SelectedIndex = newindex;
			}
		}
		
		#endregion
		
	}
	
}


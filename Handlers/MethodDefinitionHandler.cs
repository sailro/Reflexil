
#region " Imports "
using System;
using Reflector.CodeModel;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflexil.Utils;
using Reflexil.Forms;
using Reflexil.Wrappers;
using System.Drawing;
#endregion

namespace Reflexil.Handlers
{
	
	public partial class MethodDefinitionHandler : IHandler
	{
		
		#region " Fields "
		private MethodDefinition m_mdef;
        private Rectangle m_dragbox;
        private int m_dragindex;
        private bool m_readonly;
		#endregion
		
		#region " Properties "
        public bool ReadOnly
        {
            get {
                return m_readonly;
            }
            set
            {
                m_readonly = value;
            }
        }

		public bool IsItemHandled(object item)
		{
			return (item) is IMethodDeclaration;
		}
		
		public string Label
		{
			get
			{
				return "Method definition";
			}
		}
		
		public Instruction SelectedInstruction
		{
			get
			{
				if (Instructions.SelectedRows.Count > 0)
				{
					return ((Instruction) (Instructions.SelectedRows[0].DataBoundItem));
				}
				return null;
			}
		}
		
		public MethodDefinition MethodDefinition
		{
			get
			{
				return m_mdef;
			}
		}
		#endregion
		
		#region " Events "
		private void MenCreateInstruction_Click(object sender, EventArgs e)
		{
			using (CreateInstructionForm createForm = new CreateInstructionForm())
			{
				if (createForm.ShowDialog(this) == DialogResult.OK)
				{
					InstructionBindingSource.ResetBindings(false);
                    ExceptionHandlerBindingSource.ResetBindings(false);
				}
			}
			
		}
		
		private void MenDeleteInstruction_Click(object sender, EventArgs e)
		{
			MethodDefinition.Body.CilWorker.Remove(SelectedInstruction);
			InstructionBindingSource.ResetBindings(false);
            ExceptionHandlerBindingSource.ResetBindings(false);
		}
		
		private void MenEditInstruction_Click(object sender, EventArgs e)
		{
			using (EditInstructionForm editForm = new EditInstructionForm())
			{
				if (editForm.ShowDialog(this) == DialogResult.OK)
				{
					InstructionBindingSource.ResetBindings(false);
                    ExceptionHandlerBindingSource.ResetBindings(false);
                }
			}
		}

        private void MenReplaceBody_Click(object sender, EventArgs e)
        {
            using (CodeForm codeForm = new CodeForm(MethodDefinition))
            {
                if (codeForm.ShowDialog(this) == DialogResult.OK)
                {
                    CecilHelper.ImportMethodBody(codeForm.MethodDefinition, MethodDefinition);
                    HandleItem(MethodDefinition);
                }
            }
        }

        private void MenDeleteAllInstructions_Click(object sender, EventArgs e)
        {
            MethodDefinition.Body.Instructions.Clear();
            InstructionBindingSource.ResetBindings(false);
            ExceptionHandlerBindingSource.ResetBindings(false);
        }
		
		private void InstructionsContextMenu_Opened(object sender, EventArgs e)
		{
            MenCreateInstruction.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenEditInstruction.Enabled = (!ReadOnly) && (SelectedInstruction != null);
            MenReplaceBody.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenDeleteInstruction.Enabled = (!ReadOnly) && (SelectedInstruction != null);
            MenDeleteAllInstructions.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
        }
		
		private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if ((e.Value) is OpCode)
			{
				Instructions.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = DataManager.GetInstance().GetOpcodeDesc((OpCode) e.Value);
			}
			else
			{
				e.Value = Wrappers.OperandDisplayHelper.ToString(m_mdef, e.Value);
			}
		}

        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            string strRowNumber = e.RowIndex.ToString();

            while (strRowNumber.Length < grid.RowCount.ToString().Length)
            {
                strRowNumber = "0" + strRowNumber;
            }

            SizeF size = e.Graphics.MeasureString(strRowNumber, grid.Font);

            if (grid.RowHeadersWidth < (size.Width + 20))
            {
                grid.RowHeadersWidth = Convert.ToInt32(size.Width + 20);
            }

            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, grid.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }
    	#endregion

        #region " Drag&Drop "
        private void Instructions_MouseDown(object sender, MouseEventArgs e)
        {
            m_dragindex = Instructions.HitTest(e.X, e.Y).RowIndex;
            if (m_dragindex != -1)
            {
                Size dragSize = SystemInformation.DragSize;
                m_dragbox = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
            {
                m_dragbox = Rectangle.Empty;
            }
        }

        private void Instructions_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (m_dragbox != Rectangle.Empty &&
                !m_dragbox.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = Instructions.DoDragDrop(Instructions.Rows[m_dragindex], DragDropEffects.Move);
                }
            }
        }

        private void Instructions_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = Instructions.PointToClient(new Point(e.X, e.Y));
            int rowindex = Instructions.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (e.Effect == DragDropEffects.Move)
            {

                DataGridViewRow sourceRow = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                DataGridViewRow targetRow = Instructions.Rows[rowindex];

                Instruction sourceIns = sourceRow.DataBoundItem as Instruction;
                Instruction targetIns = targetRow.DataBoundItem as Instruction;

                if (sourceIns != targetIns)
                {
                    MethodDefinition.Body.CilWorker.Remove(sourceIns);
                    if (sourceRow.Index > targetRow.Index)
                    {
                        MethodDefinition.Body.CilWorker.InsertBefore(targetIns, sourceIns);
                    }
                    else
                    {
                        MethodDefinition.Body.CilWorker.InsertAfter(targetIns, sourceIns);
                    }
                    InstructionBindingSource.ResetBindings(false);
                }
            }
        }

        private void Instructions_DragOver(object sender, DragEventArgs e)
        {
            if (ReadOnly)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;
            }
        }
        #endregion
		
		#region " Methods "
        public MethodDefinitionHandler() : base()
        {
            InitializeComponent();
            m_readonly = false;
        }

        public void HandleItem(MethodDefinition mdef)
        {
            m_mdef = mdef;
            if (m_mdef != null)
            {
                if (m_mdef.HasBody)
                {
                    InstructionBindingSource.DataSource = m_mdef.Body.Instructions;
                    VariableDefinitionBindingSource.DataSource = m_mdef.Body.Variables;
                    ExceptionHandlers.DataSource = m_mdef.Body.ExceptionHandlers;
                }
                else
                {
                    InstructionBindingSource.DataSource = null;
                    VariableDefinitionBindingSource.DataSource = null;
                    ExceptionHandlers.DataSource = null;
                }
                ParameterDefinitionBindingSource.DataSource = m_mdef.Parameters;
            }
            else
            {
                InstructionBindingSource.DataSource = null;
                VariableDefinitionBindingSource.DataSource = null;
                ParameterDefinitionBindingSource.DataSource = null;
                ExceptionHandlers.DataSource = null;
            }
        }

		public void HandleItem(object item)
		{
			IMethodDeclaration mdec = (IMethodDeclaration) item;
            HandleItem(CecilHelper.ReflectorMethodToCecilMethod(mdec));
		}
		#endregion

	}
	
}



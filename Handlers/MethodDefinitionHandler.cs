
#region " Imports "
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Reflector.CodeModel;
using Reflexil.Utils;
using Reflexil.Forms;
using Reflexil.Wrappers;
using Reflexil.Properties;
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
        private int m_firstrowindex;
        private int m_hscrolloffset;
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

        public T GetFirstSelectedItem<T>(DataGridView grid)
        {
            T[] selections = GetSelectedItems<T>(grid);
            if (selections.Length > 0)
            {
                return selections[0];
            }
            return default(T);
        }

        public T[] GetSelectedItems<T>(DataGridView grid)
        {
            List<T> result = new List<T>();
            if (grid.SelectedRows.Count > 0)
            {
                for (int i = 0; i < grid.SelectedRows.Count; i++)
                {
                    result.Add((T)(grid.SelectedRows[i].DataBoundItem));
                }
            }
            return result.ToArray();
        }
	
		public Instruction SelectedInstruction
		{
			get
			{
                return GetFirstSelectedItem<Instruction>(Instructions);
			}
		}

        public Instruction[] SelectedInstructions
        {
            get
            {
                return GetSelectedItems<Instruction>(Instructions);
            }
        }

        public ExceptionHandler SelectedExceptionHandler
        {
            get
            {
                return GetFirstSelectedItem<ExceptionHandler>(ExceptionHandlers);
            }
        }

        public ExceptionHandler[] SelectedExceptionHandlers
        {
            get
            {
                return GetSelectedItems<ExceptionHandler>(ExceptionHandlers);
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
		
		#region " Instruction events "
        private void RefreshInstructionsAndDependencies()
        {
            SaveScrollBarPositions(Instructions);
            InstructionBindingSource.ResetBindings(false);
            ExceptionHandlerBindingSource.ResetBindings(false);
            RestoreScrollBarPositions(Instructions);
        }

		private void MenCreateInstruction_Click(object sender, EventArgs e)
		{
			using (CreateInstructionForm createForm = new CreateInstructionForm())
			{
				if (createForm.ShowDialog(this) == DialogResult.OK)
				{
                    RefreshInstructionsAndDependencies();
                }
			}
		}

        private void MenDeleteInstruction_Click(object sender, EventArgs e)
        {
            foreach (Instruction ins in SelectedInstructions)
            {
                MethodDefinition.Body.CilWorker.Remove(ins);
            }
            RefreshInstructionsAndDependencies();
        }

        private void MenEditInstruction_Click(object sender, EventArgs e)
        {
            using (EditInstructionForm editForm = new EditInstructionForm())
            {
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshInstructionsAndDependencies();
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
            RefreshInstructionsAndDependencies();
        }

        private void InstructionsContextMenu_Opened(object sender, EventArgs e)
        {
            MenCreateInstruction.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenEditInstruction.Enabled = (!ReadOnly) && (SelectedInstruction != null);
            MenReplaceBody.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenDeleteInstruction.Enabled = (!ReadOnly) && (SelectedInstructions.Length > 0);
            MenDeleteAllInstructions.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
        }
        #endregion

        #region " Exception handler events "
        private void RefreshExceptionHandlersAndDependencies()
        {
            SaveScrollBarPositions(ExceptionHandlers);
            ExceptionHandlerBindingSource.ResetBindings(false);
            RestoreScrollBarPositions(ExceptionHandlers);
        }


        private void ExceptionHandlersContextMenu_Opened(object sender, EventArgs e)
        {
            MenCreateExceptionHandler.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
            MenEditExceptionHandler.Enabled = (!ReadOnly) && (SelectedExceptionHandler != null);
            MenDeleteExceptionHandler.Enabled = (!ReadOnly) && (SelectedExceptionHandlers.Length > 0);
            MenDeleteAllExceptionHandlers.Enabled = (!ReadOnly) && (MethodDefinition != null) && (MethodDefinition.Body != null);
        }

        private void MenCreateExceptionHandler_Click(object sender, EventArgs e)
        {
            using (CreateExceptionHandlerForm createForm = new CreateExceptionHandlerForm())
            {
                if (createForm.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshExceptionHandlersAndDependencies();
                }
            }
        }

        private void MenEditExceptionHandler_Click(object sender, EventArgs e)
        {
            using (EditExceptionHandlerForm editForm = new EditExceptionHandlerForm())
            {
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshExceptionHandlersAndDependencies();
                }
            }
        }

        private void MenDeleteExceptionHandler_Click(object sender, EventArgs e)
        {
            foreach (ExceptionHandler handler in SelectedExceptionHandlers)
            {
                MethodDefinition.Body.ExceptionHandlers.Remove(handler);
            }
            RefreshExceptionHandlersAndDependencies();
        }

        private void MenDeleteAllExceptionHandlers_Click(object sender, EventArgs e)
        {
            MethodDefinition.Body.ExceptionHandlers.Clear();
            RefreshExceptionHandlersAndDependencies();
        }
        #endregion

        #region " Grid events "
        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if ((e.Value) is OpCode)
			{
				Instructions.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = DataManager.GetInstance().GetOpcodeDesc((OpCode) e.Value);
			}
			else
			{
                if ((e.Value is Int16 || e.Value is Int32 || e.Value is Int64 || e.Value is SByte)
                    || (e.Value is UInt16 || e.Value is UInt32 || e.Value is UInt64 || e.Value is Byte))
                {
                    StringBuilder tipbuilder = new StringBuilder();
                    Array values = System.Enum.GetValues(typeof(ENumericBase));
                    for (int i =0; i<values.Length; i++)
                    {
                        if (i > 0)
                        {
                            tipbuilder.AppendLine();
                        }
                        ENumericBase item = (ENumericBase)values.GetValue(i);
                        tipbuilder.Append(item.ToString());
                        tipbuilder.Append(": ");
                        tipbuilder.Append(OperandDisplayHelper.Changebase(e.Value.ToString(), ENumericBase.Dec, item));
                    }
                    Instructions.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tipbuilder.ToString();
                }
                e.Value = Wrappers.OperandDisplayHelper.ToString(m_mdef, e.Value);
			}
		}

        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            string strRowNumber = OperandDisplayHelper.Changebase(e.RowIndex.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase);
            string strRowCount = OperandDisplayHelper.Changebase(grid.RowCount.ToString(), ENumericBase.Dec, Settings.Default.RowIndexDisplayBase);

            while (strRowNumber.Length < strRowCount.Length)
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

        #region " Other events "
        public void OnConfigurationChanged(object sender, EventArgs e)
        {
            DataGridView[] grids = {Instructions, Variables, Parameters, ExceptionHandlers};
            BindingSource[] sources = { InstructionBindingSource, VariableDefinitionBindingSource, ParameterDefinitionBindingSource, ExceptionHandlerBindingSource };

            for (int i = 0; i < grids.Length; i++)
            {
                SaveScrollBarPositions(grids[i]);
                sources[i].ResetBindings(false);
                RestoreScrollBarPositions(grids[i]);
            }
        }
        #endregion

        #region " Drag&Drop "
        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            m_dragindex = (sender as DataGridView).HitTest(e.X, e.Y).RowIndex;
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

        private void DataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (m_dragbox != Rectangle.Empty &&
                !m_dragbox.Contains(e.X, e.Y))
                {
                    DragDropEffects dropEffect = grid.DoDragDrop(grid.Rows[m_dragindex], DragDropEffects.Move);
                }
            }
        }

        private void DataGridView_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            Point clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            int rowindex = grid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (e.Effect == DragDropEffects.Move)
            {

                DataGridViewRow sourceRow = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                DataGridViewRow targetRow = grid.Rows[rowindex];

                if (sourceRow.DataBoundItem is Instruction)
                {
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
                        RefreshInstructionsAndDependencies();
                    }
                }
                else if (sourceRow.DataBoundItem is ExceptionHandler)
                {
                    ExceptionHandler sourceExc = sourceRow.DataBoundItem as ExceptionHandler;
                    ExceptionHandler targetExc = targetRow.DataBoundItem as ExceptionHandler;

                    if (sourceExc != targetExc)
                    {
                        MethodDefinition.Body.ExceptionHandlers.Remove(sourceExc);
                        MethodDefinition.Body.ExceptionHandlers.Insert(targetRow.Index, sourceExc);
                        RefreshExceptionHandlersAndDependencies();
                    }
                }
            }
        }

        private void DataGridView_DragOver(object sender, DragEventArgs e)
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
        public void SaveScrollBarPositions(DataGridView grid) {
            m_firstrowindex = grid.FirstDisplayedScrollingRowIndex;
            m_hscrolloffset = grid.HorizontalScrollingOffset;
        }

        public void RestoreScrollBarPositions(DataGridView grid)
        {
            if (m_firstrowindex < grid.RowCount && m_firstrowindex >= 0)
            {
                grid.FirstDisplayedScrollingRowIndex = m_firstrowindex;
            }
            grid.HorizontalScrollingOffset = m_hscrolloffset;
        }

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
                    ExceptionHandlerBindingSource.DataSource = m_mdef.Body.ExceptionHandlers;
                }
                else
                {
                    InstructionBindingSource.DataSource = null;
                    VariableDefinitionBindingSource.DataSource = null;
                    ExceptionHandlerBindingSource.DataSource = null;
                }
                ParameterDefinitionBindingSource.DataSource = m_mdef.Parameters;
            }
            else
            {
                InstructionBindingSource.DataSource = null;
                VariableDefinitionBindingSource.DataSource = null;
                ParameterDefinitionBindingSource.DataSource = null;
                ExceptionHandlerBindingSource.DataSource = null;
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



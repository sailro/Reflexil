namespace Reflexil.Editors
{
    partial class InstructionGridControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.OffsetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MenReplaceNop = new System.Windows.Forms.ToolStripMenuItem();
			this.MenReplaceBody = new System.Windows.Forms.ToolStripMenuItem();
            this.MenCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenPaste = new System.Windows.Forms.ToolStripMenuItem();

            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OffsetDataGridViewTextBoxColumn,
            this.OpCodeDataGridViewTextBoxColumn,
            this.OperandDataGridViewTextBoxColumn});
            // 
            // GridContextMenuStrip
            // 
            GridContextMenuStrip.Items.Insert(GridContextMenuStrip.Items.IndexOf(MenSeparator), MenReplaceBody);
			GridContextMenuStrip.Items.Insert(GridContextMenuStrip.Items.IndexOf(MenReplaceBody), MenReplaceNop);
            GridContextMenuStrip.Items.Insert(GridContextMenuStrip.Items.IndexOf(MenReplaceNop), MenPaste);
            GridContextMenuStrip.Items.Insert(GridContextMenuStrip.Items.IndexOf(MenPaste), MenCopy);
            // 
            // MenReplaceBody
            // 
            this.MenReplaceBody.Name = "MenReplaceBody";
            this.MenReplaceBody.Size = new System.Drawing.Size(197, 22);
            this.MenReplaceBody.Text = "Replace all with code...";
            this.MenReplaceBody.Click += new System.EventHandler(this.MenReplaceBody_Click);
			// 
			// MenReplaceNop
			// 
			this.MenReplaceNop.Name = "MenReplaceNop";
			this.MenReplaceNop.Size = new System.Drawing.Size(197, 22);
			this.MenReplaceNop.Text = "Replace with NOP";
			this.MenReplaceNop.Click += new System.EventHandler(this.MenReplaceNop_Click);
            ///
            /// MenCopy
            /// 
            this.MenCopy.Name = "MenCopy";
            this.MenCopy.Size = new System.Drawing.Size(197, 22);
            this.MenCopy.Text = "Copy";
            this.MenCopy.Click += new System.EventHandler(this.MenCopy_Click);
            ///
            /// MenPaste
            /// 
            this.MenPaste.Name = "MenPaste";
            this.MenPaste.Size = new System.Drawing.Size(197, 22);
            this.MenPaste.Text = "Paste";
            this.MenPaste.Click += new System.EventHandler(this.MenPaste_Click);
            // 
            // OffsetDataGridViewTextBoxColumn
            // 
            this.OffsetDataGridViewTextBoxColumn.DataPropertyName = "Offset";
            this.OffsetDataGridViewTextBoxColumn.HeaderText = "Offset";
            this.OffsetDataGridViewTextBoxColumn.Name = "OffsetDataGridViewTextBoxColumn";
            this.OffsetDataGridViewTextBoxColumn.ReadOnly = true;
            this.OffsetDataGridViewTextBoxColumn.DefaultCellStyle.Format = "X04";
            // 
            // OpCodeDataGridViewTextBoxColumn
            // 
            this.OpCodeDataGridViewTextBoxColumn.DataPropertyName = "OpCode";
            this.OpCodeDataGridViewTextBoxColumn.HeaderText = "OpCode";
            this.OpCodeDataGridViewTextBoxColumn.Name = "OpCodeDataGridViewTextBoxColumn";
            this.OpCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // OperandDataGridViewTextBoxColumn
            // 
            this.OperandDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OperandDataGridViewTextBoxColumn.DataPropertyName = "Operand";
            this.OperandDataGridViewTextBoxColumn.HeaderText = "Operand";
            this.OperandDataGridViewTextBoxColumn.Name = "OperandDataGridViewTextBoxColumn";
            this.OperandDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // InstructionBindingSource
            // 
            this.BindingSource.AllowNew = false;
            this.BindingSource.DataSource = typeof(Mono.Cecil.Cil.Instruction);
        }

        internal System.Windows.Forms.DataGridViewTextBoxColumn OffsetDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn OpCodeDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn OperandDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStripMenuItem MenReplaceBody;
		private System.Windows.Forms.ToolStripMenuItem MenReplaceNop;
        private System.Windows.Forms.ToolStripMenuItem MenCopy;
        private System.Windows.Forms.ToolStripMenuItem MenPaste;

        #endregion
    }
}

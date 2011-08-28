namespace Reflexil.Editors
{
	partial class GridControl<T, TD>
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.GridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.MenDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.MenDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.GridContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.Grid);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(532, 320);
            this.MainPanel.TabIndex = 0;
            // 
            // Grid
            // 
            this.Grid.AllowDrop = true;
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToDeleteRows = false;
            this.Grid.AllowUserToResizeRows = false;
            this.Grid.AutoGenerateColumns = false;
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.ContextMenuStrip = this.GridContextMenuStrip;
            this.Grid.DataSource = this.BindingSource;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid.Location = new System.Drawing.Point(0, 0);
            this.Grid.Name = "Grid";
            this.Grid.ReadOnly = true;
            this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid.Size = new System.Drawing.Size(532, 320);
            this.Grid.TabIndex = 1;
            this.Grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDown);
            this.Grid.DragOver += new System.Windows.Forms.DragEventHandler(this.Grid_DragOver);
            this.Grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseMove);
            this.Grid.DragDrop += new System.Windows.Forms.DragEventHandler(this.Grid_DragDrop);
            this.Grid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Grid_RowPostPaint);
            this.Grid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            // 
            // GridContextMenuStrip
            // 
            this.GridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenCreate,
            this.MenEdit,
            this.MenSeparator,
            this.MenDelete,
            this.MenDeleteAll});
            this.GridContextMenuStrip.Name = "ContextMenuStrip";
            this.GridContextMenuStrip.Size = new System.Drawing.Size(154, 120);
            this.GridContextMenuStrip.Opened += new System.EventHandler(this.GridContextMenuStrip_Opened);
            // 
            // MenCreate
            // 
            this.MenCreate.Name = "MenCreate";
            this.MenCreate.Size = new System.Drawing.Size(197, 22);
            this.MenCreate.Text = "Create new...";
            this.MenCreate.Click += new System.EventHandler(this.MenCreate_Click);
            // 
            // MenEdit
            // 
            this.MenEdit.Name = "MenEdit";
            this.MenEdit.Size = new System.Drawing.Size(197, 22);
            this.MenEdit.Text = "Edit...";
            this.MenEdit.Click += new System.EventHandler(this.MenEdit_Click);
            // 
            // MenSeparator
            // 
            this.MenSeparator.Name = "MenSeparator";
            this.MenSeparator.Size = new System.Drawing.Size(194, 6);
            // 
            // MenDelete
            // 
            this.MenDelete.Name = "MenDelete";
            this.MenDelete.Size = new System.Drawing.Size(153, 22);
            this.MenDelete.Text = "Delete";
            this.MenDelete.Click += new System.EventHandler(this.MenDelete_Click);
            // 
            // MenDeleteAll
            // 
            this.MenDeleteAll.Name = "MenDeleteAll";
            this.MenDeleteAll.Size = new System.Drawing.Size(153, 22);
            this.MenDeleteAll.Text = "Delete all";
            this.MenDeleteAll.Click += new System.EventHandler(this.MenDeleteAll_Click);
            // 
            // GridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "GridControl";
            this.Size = new System.Drawing.Size(532, 320);
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.GridContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel MainPanel;
        internal System.Windows.Forms.ToolStripMenuItem MenCreate;
        internal System.Windows.Forms.ToolStripMenuItem MenEdit;
        internal System.Windows.Forms.ToolStripSeparator MenSeparator;
        internal System.Windows.Forms.ToolStripMenuItem MenDelete;
        internal System.Windows.Forms.ToolStripMenuItem MenDeleteAll;
        internal System.Windows.Forms.DataGridView Grid;
        internal System.Windows.Forms.ContextMenuStrip GridContextMenuStrip;
        internal System.Windows.Forms.BindingSource BindingSource;
	}
}

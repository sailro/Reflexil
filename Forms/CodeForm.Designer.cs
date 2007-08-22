namespace Reflexil.Forms
{
	partial class CodeForm
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
            Fireball.Windows.Forms.LineMarginRender lineMarginRender1 = new Fireball.Windows.Forms.LineMarginRender();
            this.CodeEditor = new Fireball.Windows.Forms.CodeEditorControl();
            this.SyntaxDocument = new Fireball.Syntax.SyntaxDocument(this.components);
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.ButPreview = new System.Windows.Forms.Button();
            this.ButOk = new System.Windows.Forms.Button();
            this.ButCancel = new System.Windows.Forms.Button();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.VerticalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.HorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MethodHandler = new Reflexil.Handlers.MethodDefinitionHandler();
            this.GbxErrors = new System.Windows.Forms.GroupBox();
            this.ErrorGridView = new System.Windows.Forms.DataGridView();
            this.ErrorLineColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorColumnColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isWarningDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CompilerErrorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BottomPanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.VerticalSplitContainer.Panel1.SuspendLayout();
            this.VerticalSplitContainer.Panel2.SuspendLayout();
            this.VerticalSplitContainer.SuspendLayout();
            this.HorizontalSplitContainer.Panel1.SuspendLayout();
            this.HorizontalSplitContainer.Panel2.SuspendLayout();
            this.HorizontalSplitContainer.SuspendLayout();
            this.GbxErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompilerErrorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // CodeEditor
            // 
            this.CodeEditor.ActiveView = Fireball.Windows.Forms.CodeEditor.ActiveView.BottomRight;
            this.CodeEditor.AutoListPosition = null;
            this.CodeEditor.AutoListSelectedText = "a123";
            this.CodeEditor.AutoListVisible = false;
            this.CodeEditor.CopyAsRTF = false;
            this.CodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeEditor.Document = this.SyntaxDocument;
            this.CodeEditor.InfoTipCount = 1;
            this.CodeEditor.InfoTipPosition = null;
            this.CodeEditor.InfoTipSelectedIndex = 1;
            this.CodeEditor.InfoTipVisible = false;
            lineMarginRender1.Bounds = new System.Drawing.Rectangle(19, 0, 19, 16);
            this.CodeEditor.LineMarginRender = lineMarginRender1;
            this.CodeEditor.Location = new System.Drawing.Point(0, 0);
            this.CodeEditor.LockCursorUpdate = false;
            this.CodeEditor.Name = "CodeEditor";
            this.CodeEditor.Saved = false;
            this.CodeEditor.ShowScopeIndicator = false;
            this.CodeEditor.Size = new System.Drawing.Size(367, 415);
            this.CodeEditor.SmoothScroll = false;
            this.CodeEditor.SplitviewH = -4;
            this.CodeEditor.SplitviewV = -4;
            this.CodeEditor.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(243)))), ((int)(((byte)(234)))));
            this.CodeEditor.TabIndex = 0;
            this.CodeEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
            // 
            // SyntaxDocument
            // 
            this.SyntaxDocument.Lines = new string[] {
        "//"};
            this.SyntaxDocument.MaxUndoBufferSize = 1000;
            this.SyntaxDocument.Modified = false;
            this.SyntaxDocument.UndoStep = 0;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.ButPreview);
            this.BottomPanel.Controls.Add(this.ButOk);
            this.BottomPanel.Controls.Add(this.ButCancel);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 415);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(721, 39);
            this.BottomPanel.TabIndex = 1;
            // 
            // ButPreview
            // 
            this.ButPreview.Location = new System.Drawing.Point(12, 7);
            this.ButPreview.Name = "ButPreview";
            this.ButPreview.Size = new System.Drawing.Size(75, 23);
            this.ButPreview.TabIndex = 2;
            this.ButPreview.Text = "Preview IL";
            this.ButPreview.UseVisualStyleBackColor = true;
            this.ButPreview.Click += new System.EventHandler(this.ButPreview_Click);
            // 
            // ButOk
            // 
            this.ButOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButOk.Enabled = false;
            this.ButOk.Location = new System.Drawing.Point(553, 7);
            this.ButOk.Name = "ButOk";
            this.ButOk.Size = new System.Drawing.Size(75, 23);
            this.ButOk.TabIndex = 1;
            this.ButOk.Text = "Ok";
            this.ButOk.UseVisualStyleBackColor = true;
            // 
            // ButCancel
            // 
            this.ButCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButCancel.Location = new System.Drawing.Point(634, 7);
            this.ButCancel.Name = "ButCancel";
            this.ButCancel.Size = new System.Drawing.Size(75, 23);
            this.ButCancel.TabIndex = 0;
            this.ButCancel.Text = "Cancel";
            this.ButCancel.UseVisualStyleBackColor = true;
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.VerticalSplitContainer);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(721, 415);
            this.TopPanel.TabIndex = 2;
            // 
            // VerticalSplitContainer
            // 
            this.VerticalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VerticalSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.VerticalSplitContainer.Name = "VerticalSplitContainer";
            this.VerticalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // VerticalSplitContainer.Panel1
            // 
            this.VerticalSplitContainer.Panel1.Controls.Add(this.HorizontalSplitContainer);
            // 
            // VerticalSplitContainer.Panel2
            // 
            this.VerticalSplitContainer.Panel2.Controls.Add(this.GbxErrors);
            this.VerticalSplitContainer.Panel2Collapsed = true;
            this.VerticalSplitContainer.Size = new System.Drawing.Size(721, 415);
            this.VerticalSplitContainer.SplitterDistance = 282;
            this.VerticalSplitContainer.TabIndex = 2;
            // 
            // HorizontalSplitContainer
            // 
            this.HorizontalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HorizontalSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.HorizontalSplitContainer.Name = "HorizontalSplitContainer";
            // 
            // HorizontalSplitContainer.Panel1
            // 
            this.HorizontalSplitContainer.Panel1.Controls.Add(this.CodeEditor);
            // 
            // HorizontalSplitContainer.Panel2
            // 
            this.HorizontalSplitContainer.Panel2.Controls.Add(this.MethodHandler);
            this.HorizontalSplitContainer.Size = new System.Drawing.Size(721, 415);
            this.HorizontalSplitContainer.SplitterDistance = 367;
            this.HorizontalSplitContainer.TabIndex = 0;
            // 
            // MethodHandler
            // 
            this.MethodHandler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MethodHandler.Location = new System.Drawing.Point(0, 0);
            this.MethodHandler.Name = "MethodHandler";
            this.MethodHandler.ReadOnly = true;
            this.MethodHandler.Size = new System.Drawing.Size(350, 415);
            this.MethodHandler.TabIndex = 0;
            // 
            // GbxErrors
            // 
            this.GbxErrors.Controls.Add(this.ErrorGridView);
            this.GbxErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GbxErrors.Location = new System.Drawing.Point(0, 0);
            this.GbxErrors.Name = "GbxErrors";
            this.GbxErrors.Size = new System.Drawing.Size(150, 46);
            this.GbxErrors.TabIndex = 1;
            this.GbxErrors.TabStop = false;
            this.GbxErrors.Text = "Errors";
            // 
            // ErrorGridView
            // 
            this.ErrorGridView.AllowUserToAddRows = false;
            this.ErrorGridView.AllowUserToDeleteRows = false;
            this.ErrorGridView.AutoGenerateColumns = false;
            this.ErrorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ErrorGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ErrorLineColumn,
            this.ErrorColumnColumn,
            this.errorNumberDataGridViewTextBoxColumn,
            this.errorTextDataGridViewTextBoxColumn,
            this.isWarningDataGridViewCheckBoxColumn});
            this.ErrorGridView.DataSource = this.CompilerErrorBindingSource;
            this.ErrorGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorGridView.Location = new System.Drawing.Point(3, 16);
            this.ErrorGridView.Name = "ErrorGridView";
            this.ErrorGridView.ReadOnly = true;
            this.ErrorGridView.Size = new System.Drawing.Size(144, 27);
            this.ErrorGridView.TabIndex = 0;
            this.ErrorGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ErrorGridView_CellDoubleClick);
            // 
            // ErrorLineColumn
            // 
            this.ErrorLineColumn.DataPropertyName = "Line";
            this.ErrorLineColumn.HeaderText = "Line";
            this.ErrorLineColumn.Name = "ErrorLineColumn";
            this.ErrorLineColumn.ReadOnly = true;
            this.ErrorLineColumn.Width = 50;
            // 
            // ErrorColumnColumn
            // 
            this.ErrorColumnColumn.DataPropertyName = "Column";
            this.ErrorColumnColumn.HeaderText = "Column";
            this.ErrorColumnColumn.Name = "ErrorColumnColumn";
            this.ErrorColumnColumn.ReadOnly = true;
            this.ErrorColumnColumn.Width = 50;
            // 
            // errorNumberDataGridViewTextBoxColumn
            // 
            this.errorNumberDataGridViewTextBoxColumn.DataPropertyName = "ErrorNumber";
            this.errorNumberDataGridViewTextBoxColumn.HeaderText = "Error number";
            this.errorNumberDataGridViewTextBoxColumn.Name = "errorNumberDataGridViewTextBoxColumn";
            this.errorNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // errorTextDataGridViewTextBoxColumn
            // 
            this.errorTextDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.errorTextDataGridViewTextBoxColumn.DataPropertyName = "ErrorText";
            this.errorTextDataGridViewTextBoxColumn.HeaderText = "Error message";
            this.errorTextDataGridViewTextBoxColumn.Name = "errorTextDataGridViewTextBoxColumn";
            this.errorTextDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isWarningDataGridViewCheckBoxColumn
            // 
            this.isWarningDataGridViewCheckBoxColumn.DataPropertyName = "IsWarning";
            this.isWarningDataGridViewCheckBoxColumn.HeaderText = "Warning";
            this.isWarningDataGridViewCheckBoxColumn.Name = "isWarningDataGridViewCheckBoxColumn";
            this.isWarningDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isWarningDataGridViewCheckBoxColumn.Width = 50;
            // 
            // CompilerErrorBindingSource
            // 
            this.CompilerErrorBindingSource.DataSource = typeof(System.CodeDom.Compiler.CompilerError);
            // 
            // CodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 454);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CodeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compile";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CodeForm_FormClosed);
            this.BottomPanel.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.VerticalSplitContainer.Panel1.ResumeLayout(false);
            this.VerticalSplitContainer.Panel2.ResumeLayout(false);
            this.VerticalSplitContainer.ResumeLayout(false);
            this.HorizontalSplitContainer.Panel1.ResumeLayout(false);
            this.HorizontalSplitContainer.Panel2.ResumeLayout(false);
            this.HorizontalSplitContainer.ResumeLayout(false);
            this.GbxErrors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CompilerErrorBindingSource)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private Fireball.Windows.Forms.CodeEditorControl CodeEditor;
        private Fireball.Syntax.SyntaxDocument SyntaxDocument;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button ButPreview;
        private System.Windows.Forms.Button ButOk;
        private System.Windows.Forms.Button ButCancel;
        private Reflexil.Handlers.MethodDefinitionHandler MethodHandler;
        private System.Windows.Forms.SplitContainer HorizontalSplitContainer;
        private System.Windows.Forms.GroupBox GbxErrors;
        private System.Windows.Forms.SplitContainer VerticalSplitContainer;
        private System.Windows.Forms.DataGridView ErrorGridView;
        private System.Windows.Forms.BindingSource CompilerErrorBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorLineColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorColumnColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn errorNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn errorTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isWarningDataGridViewCheckBoxColumn;
	}
}
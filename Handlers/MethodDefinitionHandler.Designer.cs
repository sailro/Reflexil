using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Handlers
{
	public partial class MethodDefinitionHandler : System.Windows.Forms.UserControl
	{
		
		
		//UserControl remplace la mÃ©thode Dispose pour nettoyer la liste des composants.
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && (components != null))
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

        //Requise par le Concepteur Windows Form
		
		//REMARQUEÂ : la procÃ©dure suivante est requise par le Concepteur Windows Form
		//Elle peut Ãªtre modifiÃ©e Ã  l'aide du Concepteur Windows Form.
		//Ne la modifiez pas Ã  l'aide de l'Ã©diteur de code.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.Instructions = new System.Windows.Forms.DataGridView();
            this.OpCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstructionsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenCreateInstruction = new System.Windows.Forms.ToolStripMenuItem();
            this.MenEditInstruction = new System.Windows.Forms.ToolStripMenuItem();
            this.MenReplaceBody = new System.Windows.Forms.ToolStripMenuItem();
            this.MenSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.MenDeleteInstruction = new System.Windows.Forms.ToolStripMenuItem();
            this.MenDeleteAllInstructions = new System.Windows.Forms.ToolStripMenuItem();
            this.InstructionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.VariableDefinitionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Variables = new System.Windows.Forms.DataGridView();
            this.NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabInstructions = new System.Windows.Forms.TabPage();
            this.TabVariables = new System.Windows.Forms.TabPage();
            this.TabParameters = new System.Windows.Forms.TabPage();
            this.Parameters = new System.Windows.Forms.DataGridView();
            this.NameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MetadataTokenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterDefinitionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TabExceptionHandlers = new System.Windows.Forms.TabPage();
            this.ExceptionHandlers = new System.Windows.Forms.DataGridView();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catchTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tryStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tryEndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handlerStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handlerEndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filterStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filterEndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExceptionHandlersContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenCreateExceptionHandler = new System.Windows.Forms.ToolStripMenuItem();
            this.MenEditExceptionHandler = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenDeleteExceptionHandler = new System.Windows.Forms.ToolStripMenuItem();
            this.MenDeleteAllExceptionHandlers = new System.Windows.Forms.ToolStripMenuItem();
            this.ExceptionHandlerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConstantDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Instructions)).BeginInit();
            this.InstructionsContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InstructionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableDefinitionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Variables)).BeginInit();
            this.TabControl.SuspendLayout();
            this.TabInstructions.SuspendLayout();
            this.TabVariables.SuspendLayout();
            this.TabParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Parameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParameterDefinitionBindingSource)).BeginInit();
            this.TabExceptionHandlers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionHandlers)).BeginInit();
            this.ExceptionHandlersContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionHandlerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Instructions
            // 
            this.Instructions.AllowDrop = true;
            this.Instructions.AllowUserToAddRows = false;
            this.Instructions.AllowUserToDeleteRows = false;
            this.Instructions.AutoGenerateColumns = false;
            this.Instructions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Instructions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OpCodeDataGridViewTextBoxColumn,
            this.OperandDataGridViewTextBoxColumn});
            this.Instructions.ContextMenuStrip = this.InstructionsContextMenu;
            this.Instructions.DataSource = this.InstructionBindingSource;
            this.Instructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Instructions.Location = new System.Drawing.Point(3, 3);
            this.Instructions.MultiSelect = false;
            this.Instructions.Name = "Instructions";
            this.Instructions.ReadOnly = true;
            this.Instructions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Instructions.Size = new System.Drawing.Size(671, 442);
            this.Instructions.TabIndex = 2;
            this.Instructions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseDown);
            this.Instructions.DragOver += new System.Windows.Forms.DragEventHandler(this.DataGridView_DragOver);
            this.Instructions.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseMove);
            this.Instructions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView_CellFormatting);
            this.Instructions.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGridView_RowPostPaint);
            this.Instructions.DragDrop += new System.Windows.Forms.DragEventHandler(this.DataGridView_DragDrop);
            // 
            // OpCodeDataGridViewTextBoxColumn
            // 
            this.OpCodeDataGridViewTextBoxColumn.DataPropertyName = "OpCode";
            this.OpCodeDataGridViewTextBoxColumn.HeaderText = "OpCode";
            this.OpCodeDataGridViewTextBoxColumn.Name = "OpCodeDataGridViewTextBoxColumn";
            this.OpCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // InstructionsContextMenu
            // 
            this.InstructionsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenCreateInstruction,
            this.MenEditInstruction,
            this.MenReplaceBody,
            this.MenSeparator,
            this.MenDeleteInstruction,
            this.MenDeleteAllInstructions});
            this.InstructionsContextMenu.Name = "ContextMenuStrip";
            this.InstructionsContextMenu.Size = new System.Drawing.Size(198, 120);
            this.InstructionsContextMenu.Opened += new System.EventHandler(this.InstructionsContextMenu_Opened);
            // 
            // MenCreateInstruction
            // 
            this.MenCreateInstruction.Name = "MenCreateInstruction";
            this.MenCreateInstruction.Size = new System.Drawing.Size(197, 22);
            this.MenCreateInstruction.Text = "Create new...";
            this.MenCreateInstruction.Click += new System.EventHandler(this.MenCreateInstruction_Click);
            // 
            // MenEditInstruction
            // 
            this.MenEditInstruction.Name = "MenEditInstruction";
            this.MenEditInstruction.Size = new System.Drawing.Size(197, 22);
            this.MenEditInstruction.Text = "Edit...";
            this.MenEditInstruction.Click += new System.EventHandler(this.MenEditInstruction_Click);
            // 
            // MenReplaceBody
            // 
            this.MenReplaceBody.Name = "MenReplaceBody";
            this.MenReplaceBody.Size = new System.Drawing.Size(197, 22);
            this.MenReplaceBody.Text = "Replace all with code...";
            this.MenReplaceBody.Click += new System.EventHandler(this.MenReplaceBody_Click);
            // 
            // MenSeparator
            // 
            this.MenSeparator.Name = "MenSeparator";
            this.MenSeparator.Size = new System.Drawing.Size(194, 6);
            // 
            // MenDeleteInstruction
            // 
            this.MenDeleteInstruction.Name = "MenDeleteInstruction";
            this.MenDeleteInstruction.Size = new System.Drawing.Size(197, 22);
            this.MenDeleteInstruction.Text = "Delete";
            this.MenDeleteInstruction.Click += new System.EventHandler(this.MenDeleteInstruction_Click);
            // 
            // MenDeleteAllInstructions
            // 
            this.MenDeleteAllInstructions.Name = "MenDeleteAllInstructions";
            this.MenDeleteAllInstructions.Size = new System.Drawing.Size(197, 22);
            this.MenDeleteAllInstructions.Text = "Delete all";
            this.MenDeleteAllInstructions.Click += new System.EventHandler(this.MenDeleteAllInstructions_Click);
            // 
            // InstructionBindingSource
            // 
            this.InstructionBindingSource.AllowNew = false;
            this.InstructionBindingSource.DataSource = typeof(Mono.Cecil.Cil.Instruction);
            // 
            // VariableDefinitionBindingSource
            // 
            this.VariableDefinitionBindingSource.DataSource = typeof(Mono.Cecil.Cil.VariableDefinition);
            // 
            // Variables
            // 
            this.Variables.AllowUserToAddRows = false;
            this.Variables.AllowUserToDeleteRows = false;
            this.Variables.AutoGenerateColumns = false;
            this.Variables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Variables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameDataGridViewTextBoxColumn,
            this.VariableTypeDataGridViewTextBoxColumn});
            this.Variables.DataSource = this.VariableDefinitionBindingSource;
            this.Variables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Variables.Location = new System.Drawing.Point(3, 3);
            this.Variables.MultiSelect = false;
            this.Variables.Name = "Variables";
            this.Variables.ReadOnly = true;
            this.Variables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Variables.Size = new System.Drawing.Size(671, 442);
            this.Variables.TabIndex = 3;
            this.Variables.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGridView_RowPostPaint);
            // 
            // NameDataGridViewTextBoxColumn
            // 
            this.NameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.NameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn";
            this.NameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // VariableTypeDataGridViewTextBoxColumn
            // 
            this.VariableTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VariableTypeDataGridViewTextBoxColumn.DataPropertyName = "VariableType";
            this.VariableTypeDataGridViewTextBoxColumn.HeaderText = "Variable Type";
            this.VariableTypeDataGridViewTextBoxColumn.Name = "VariableTypeDataGridViewTextBoxColumn";
            this.VariableTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // DataGridViewTextBoxColumn1
            // 
            this.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn1.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn1.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
            this.DataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabInstructions);
            this.TabControl.Controls.Add(this.TabVariables);
            this.TabControl.Controls.Add(this.TabParameters);
            this.TabControl.Controls.Add(this.TabExceptionHandlers);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Multiline = true;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(685, 474);
            this.TabControl.TabIndex = 4;
            // 
            // TabInstructions
            // 
            this.TabInstructions.Controls.Add(this.Instructions);
            this.TabInstructions.Location = new System.Drawing.Point(4, 22);
            this.TabInstructions.Name = "TabInstructions";
            this.TabInstructions.Padding = new System.Windows.Forms.Padding(3);
            this.TabInstructions.Size = new System.Drawing.Size(677, 448);
            this.TabInstructions.TabIndex = 0;
            this.TabInstructions.Text = "Instructions";
            this.TabInstructions.UseVisualStyleBackColor = true;
            // 
            // TabVariables
            // 
            this.TabVariables.Controls.Add(this.Variables);
            this.TabVariables.Location = new System.Drawing.Point(4, 22);
            this.TabVariables.Name = "TabVariables";
            this.TabVariables.Padding = new System.Windows.Forms.Padding(3);
            this.TabVariables.Size = new System.Drawing.Size(677, 448);
            this.TabVariables.TabIndex = 1;
            this.TabVariables.Text = "Variables";
            this.TabVariables.UseVisualStyleBackColor = true;
            // 
            // TabParameters
            // 
            this.TabParameters.Controls.Add(this.Parameters);
            this.TabParameters.Location = new System.Drawing.Point(4, 22);
            this.TabParameters.Name = "TabParameters";
            this.TabParameters.Padding = new System.Windows.Forms.Padding(3);
            this.TabParameters.Size = new System.Drawing.Size(677, 448);
            this.TabParameters.TabIndex = 2;
            this.TabParameters.Text = "Parameters";
            this.TabParameters.UseVisualStyleBackColor = true;
            // 
            // Parameters
            // 
            this.Parameters.AllowUserToAddRows = false;
            this.Parameters.AllowUserToDeleteRows = false;
            this.Parameters.AutoGenerateColumns = false;
            this.Parameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Parameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameDataGridViewTextBoxColumn1,
            this.ParameterTypeDataGridViewTextBoxColumn,
            this.MetadataTokenDataGridViewTextBoxColumn,
            this.ConstantDataGridViewTextBoxColumn});
            this.Parameters.DataSource = this.ParameterDefinitionBindingSource;
            this.Parameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Parameters.Location = new System.Drawing.Point(3, 3);
            this.Parameters.MultiSelect = false;
            this.Parameters.Name = "Parameters";
            this.Parameters.ReadOnly = true;
            this.Parameters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Parameters.Size = new System.Drawing.Size(671, 442);
            this.Parameters.TabIndex = 0;
            this.Parameters.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGridView_RowPostPaint);
            // 
            // NameDataGridViewTextBoxColumn1
            // 
            this.NameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.NameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.NameDataGridViewTextBoxColumn1.Name = "NameDataGridViewTextBoxColumn1";
            this.NameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // ParameterTypeDataGridViewTextBoxColumn
            // 
            this.ParameterTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ParameterTypeDataGridViewTextBoxColumn.DataPropertyName = "ParameterType";
            this.ParameterTypeDataGridViewTextBoxColumn.HeaderText = "Parameter Type";
            this.ParameterTypeDataGridViewTextBoxColumn.MinimumWidth = 128;
            this.ParameterTypeDataGridViewTextBoxColumn.Name = "ParameterTypeDataGridViewTextBoxColumn";
            this.ParameterTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.ParameterTypeDataGridViewTextBoxColumn.Width = 128;
            // 
            // MetadataTokenDataGridViewTextBoxColumn
            // 
            this.MetadataTokenDataGridViewTextBoxColumn.DataPropertyName = "MetadataToken";
            this.MetadataTokenDataGridViewTextBoxColumn.HeaderText = "Metadata Token";
            this.MetadataTokenDataGridViewTextBoxColumn.Name = "MetadataTokenDataGridViewTextBoxColumn";
            this.MetadataTokenDataGridViewTextBoxColumn.ReadOnly = true;
            this.MetadataTokenDataGridViewTextBoxColumn.Width = 128;
            // 
            // ParameterDefinitionBindingSource
            // 
            this.ParameterDefinitionBindingSource.DataSource = typeof(Mono.Cecil.ParameterDefinition);
            // 
            // TabExceptionHandlers
            // 
            this.TabExceptionHandlers.Controls.Add(this.ExceptionHandlers);
            this.TabExceptionHandlers.Location = new System.Drawing.Point(4, 22);
            this.TabExceptionHandlers.Name = "TabExceptionHandlers";
            this.TabExceptionHandlers.Padding = new System.Windows.Forms.Padding(3);
            this.TabExceptionHandlers.Size = new System.Drawing.Size(677, 448);
            this.TabExceptionHandlers.TabIndex = 3;
            this.TabExceptionHandlers.Text = "Exception Handlers";
            this.TabExceptionHandlers.UseVisualStyleBackColor = true;
            // 
            // ExceptionHandlers
            // 
            this.ExceptionHandlers.AllowDrop = true;
            this.ExceptionHandlers.AllowUserToAddRows = false;
            this.ExceptionHandlers.AllowUserToDeleteRows = false;
            this.ExceptionHandlers.AutoGenerateColumns = false;
            this.ExceptionHandlers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExceptionHandlers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeDataGridViewTextBoxColumn,
            this.catchTypeDataGridViewTextBoxColumn,
            this.tryStartDataGridViewTextBoxColumn,
            this.tryEndDataGridViewTextBoxColumn,
            this.handlerStartDataGridViewTextBoxColumn,
            this.handlerEndDataGridViewTextBoxColumn,
            this.filterStartDataGridViewTextBoxColumn,
            this.filterEndDataGridViewTextBoxColumn});
            this.ExceptionHandlers.ContextMenuStrip = this.ExceptionHandlersContextMenu;
            this.ExceptionHandlers.DataSource = this.ExceptionHandlerBindingSource;
            this.ExceptionHandlers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExceptionHandlers.Location = new System.Drawing.Point(3, 3);
            this.ExceptionHandlers.MultiSelect = false;
            this.ExceptionHandlers.Name = "ExceptionHandlers";
            this.ExceptionHandlers.ReadOnly = true;
            this.ExceptionHandlers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ExceptionHandlers.Size = new System.Drawing.Size(671, 442);
            this.ExceptionHandlers.TabIndex = 1;
            this.ExceptionHandlers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseDown);
            this.ExceptionHandlers.DragOver += new System.Windows.Forms.DragEventHandler(this.DataGridView_DragOver);
            this.ExceptionHandlers.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseMove);
            this.ExceptionHandlers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView_CellFormatting);
            this.ExceptionHandlers.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGridView_RowPostPaint);
            this.ExceptionHandlers.DragDrop += new System.Windows.Forms.DragEventHandler(this.DataGridView_DragDrop);
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // catchTypeDataGridViewTextBoxColumn
            // 
            this.catchTypeDataGridViewTextBoxColumn.DataPropertyName = "CatchType";
            this.catchTypeDataGridViewTextBoxColumn.HeaderText = "Catch Type";
            this.catchTypeDataGridViewTextBoxColumn.Name = "catchTypeDataGridViewTextBoxColumn";
            this.catchTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tryStartDataGridViewTextBoxColumn
            // 
            this.tryStartDataGridViewTextBoxColumn.DataPropertyName = "TryStart";
            this.tryStartDataGridViewTextBoxColumn.HeaderText = "Try Start";
            this.tryStartDataGridViewTextBoxColumn.Name = "tryStartDataGridViewTextBoxColumn";
            this.tryStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tryEndDataGridViewTextBoxColumn
            // 
            this.tryEndDataGridViewTextBoxColumn.DataPropertyName = "TryEnd";
            this.tryEndDataGridViewTextBoxColumn.HeaderText = "Try End";
            this.tryEndDataGridViewTextBoxColumn.Name = "tryEndDataGridViewTextBoxColumn";
            this.tryEndDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // handlerStartDataGridViewTextBoxColumn
            // 
            this.handlerStartDataGridViewTextBoxColumn.DataPropertyName = "HandlerStart";
            this.handlerStartDataGridViewTextBoxColumn.HeaderText = "Handler Start";
            this.handlerStartDataGridViewTextBoxColumn.Name = "handlerStartDataGridViewTextBoxColumn";
            this.handlerStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // handlerEndDataGridViewTextBoxColumn
            // 
            this.handlerEndDataGridViewTextBoxColumn.DataPropertyName = "HandlerEnd";
            this.handlerEndDataGridViewTextBoxColumn.HeaderText = "Handler End";
            this.handlerEndDataGridViewTextBoxColumn.Name = "handlerEndDataGridViewTextBoxColumn";
            this.handlerEndDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filterStartDataGridViewTextBoxColumn
            // 
            this.filterStartDataGridViewTextBoxColumn.DataPropertyName = "FilterStart";
            this.filterStartDataGridViewTextBoxColumn.HeaderText = "Filter Start";
            this.filterStartDataGridViewTextBoxColumn.Name = "filterStartDataGridViewTextBoxColumn";
            this.filterStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filterEndDataGridViewTextBoxColumn
            // 
            this.filterEndDataGridViewTextBoxColumn.DataPropertyName = "FilterEnd";
            this.filterEndDataGridViewTextBoxColumn.HeaderText = "Filter End";
            this.filterEndDataGridViewTextBoxColumn.Name = "filterEndDataGridViewTextBoxColumn";
            this.filterEndDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ExceptionHandlersContextMenu
            // 
            this.ExceptionHandlersContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenCreateExceptionHandler,
            this.MenEditExceptionHandler,
            this.toolStripSeparator1,
            this.MenDeleteExceptionHandler,
            this.MenDeleteAllExceptionHandlers});
            this.ExceptionHandlersContextMenu.Name = "ContextMenuStrip";
            this.ExceptionHandlersContextMenu.Size = new System.Drawing.Size(154, 98);
            this.ExceptionHandlersContextMenu.Opened += new System.EventHandler(this.ExceptionHandlersContextMenu_Opened);
            // 
            // MenCreateExceptionHandler
            // 
            this.MenCreateExceptionHandler.Name = "MenCreateExceptionHandler";
            this.MenCreateExceptionHandler.Size = new System.Drawing.Size(153, 22);
            this.MenCreateExceptionHandler.Text = "Create new...";
            this.MenCreateExceptionHandler.Click += new System.EventHandler(this.MenCreateExceptionHandler_Click);
            // 
            // MenEditExceptionHandler
            // 
            this.MenEditExceptionHandler.Name = "MenEditExceptionHandler";
            this.MenEditExceptionHandler.Size = new System.Drawing.Size(153, 22);
            this.MenEditExceptionHandler.Text = "Edit...";
            this.MenEditExceptionHandler.Click += new System.EventHandler(this.MenEditExceptionHandler_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // MenDeleteExceptionHandler
            // 
            this.MenDeleteExceptionHandler.Name = "MenDeleteExceptionHandler";
            this.MenDeleteExceptionHandler.Size = new System.Drawing.Size(153, 22);
            this.MenDeleteExceptionHandler.Text = "Delete";
            this.MenDeleteExceptionHandler.Click += new System.EventHandler(this.MenDeleteExceptionHandler_Click);
            // 
            // MenDeleteAllExceptionHandlers
            // 
            this.MenDeleteAllExceptionHandlers.Name = "MenDeleteAllExceptionHandlers";
            this.MenDeleteAllExceptionHandlers.Size = new System.Drawing.Size(153, 22);
            this.MenDeleteAllExceptionHandlers.Text = "Delete all";
            this.MenDeleteAllExceptionHandlers.Click += new System.EventHandler(this.MenDeleteAllExceptionHandlers_Click);
            // 
            // ExceptionHandlerBindingSource
            // 
            this.ExceptionHandlerBindingSource.DataSource = typeof(Mono.Cecil.Cil.ExceptionHandler);
            // 
            // DataGridViewTextBoxColumn2
            // 
            this.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn2.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn2.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
            this.DataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // DataGridViewTextBoxColumn3
            // 
            this.DataGridViewTextBoxColumn3.DataPropertyName = "Constant";
            this.DataGridViewTextBoxColumn3.HeaderText = "Constant";
            this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
            // 
            // DataGridViewTextBoxColumn4
            // 
            this.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DataGridViewTextBoxColumn4.DataPropertyName = "Operand";
            this.DataGridViewTextBoxColumn4.HeaderText = "Operand";
            this.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
            // 
            // DataGridViewTextBoxColumn5
            // 
            this.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataGridViewTextBoxColumn5.DataPropertyName = "Constant";
            this.DataGridViewTextBoxColumn5.HeaderText = "Constant";
            this.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Operand";
            this.dataGridViewTextBoxColumn6.HeaderText = "Operand";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Constant";
            this.dataGridViewTextBoxColumn7.HeaderText = "Constant";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Operand";
            this.dataGridViewTextBoxColumn8.HeaderText = "Operand";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Constant";
            this.dataGridViewTextBoxColumn9.HeaderText = "Constant";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Operand";
            this.dataGridViewTextBoxColumn10.HeaderText = "Operand";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Constant";
            this.dataGridViewTextBoxColumn11.HeaderText = "Constant";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Operand";
            this.dataGridViewTextBoxColumn12.HeaderText = "Operand";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Constant";
            this.dataGridViewTextBoxColumn13.HeaderText = "Constant";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Operand";
            this.dataGridViewTextBoxColumn14.HeaderText = "Operand";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Constant";
            this.dataGridViewTextBoxColumn15.HeaderText = "Constant";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Operand";
            this.dataGridViewTextBoxColumn16.HeaderText = "Operand";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn17.DataPropertyName = "Constant";
            this.dataGridViewTextBoxColumn17.HeaderText = "Constant";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Width = 74;
            // 
            // OperandDataGridViewTextBoxColumn
            // 
            this.OperandDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OperandDataGridViewTextBoxColumn.DataPropertyName = "Operand";
            this.OperandDataGridViewTextBoxColumn.HeaderText = "Operand";
            this.OperandDataGridViewTextBoxColumn.Name = "OperandDataGridViewTextBoxColumn";
            this.OperandDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ConstantDataGridViewTextBoxColumn
            // 
            this.ConstantDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ConstantDataGridViewTextBoxColumn.DataPropertyName = "Constant";
            this.ConstantDataGridViewTextBoxColumn.HeaderText = "Constant";
            this.ConstantDataGridViewTextBoxColumn.Name = "ConstantDataGridViewTextBoxColumn";
            this.ConstantDataGridViewTextBoxColumn.ReadOnly = true;
            this.ConstantDataGridViewTextBoxColumn.Width = 74;
            // 
            // MethodDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "MethodDefinitionHandler";
            this.Size = new System.Drawing.Size(685, 474);
            ((System.ComponentModel.ISupportInitialize)(this.Instructions)).EndInit();
            this.InstructionsContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InstructionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VariableDefinitionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Variables)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.TabInstructions.ResumeLayout(false);
            this.TabVariables.ResumeLayout(false);
            this.TabParameters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Parameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParameterDefinitionBindingSource)).EndInit();
            this.TabExceptionHandlers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionHandlers)).EndInit();
            this.ExceptionHandlersContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExceptionHandlerBindingSource)).EndInit();
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.DataGridView Instructions;
		internal System.Windows.Forms.BindingSource InstructionBindingSource;
		internal System.Windows.Forms.ContextMenuStrip InstructionsContextMenu;
		internal System.Windows.Forms.ToolStripMenuItem MenCreateInstruction;
		internal System.Windows.Forms.ToolStripMenuItem MenEditInstruction;
		internal System.Windows.Forms.ToolStripMenuItem MenDeleteInstruction;
		internal System.Windows.Forms.DataGridViewTextBoxColumn OpCodeDataGridViewTextBoxColumn;
		internal System.Windows.Forms.DataGridViewTextBoxColumn OperandDataGridViewTextBoxColumn;
		internal System.Windows.Forms.BindingSource VariableDefinitionBindingSource;
		internal System.Windows.Forms.DataGridView Variables;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
		internal System.Windows.Forms.DataGridViewTextBoxColumn NameDataGridViewTextBoxColumn;
		internal System.Windows.Forms.DataGridViewTextBoxColumn VariableTypeDataGridViewTextBoxColumn;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn2;
		internal System.Windows.Forms.TabControl TabControl;
		internal System.Windows.Forms.TabPage TabInstructions;
		internal System.Windows.Forms.TabPage TabVariables;
		internal System.Windows.Forms.TabPage TabParameters;
		internal System.Windows.Forms.DataGridView Parameters;
		internal System.Windows.Forms.BindingSource ParameterDefinitionBindingSource;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn3;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn4;
		internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn5;
		internal System.Windows.Forms.DataGridViewTextBoxColumn NameDataGridViewTextBoxColumn1;
		internal System.Windows.Forms.DataGridViewTextBoxColumn ParameterTypeDataGridViewTextBoxColumn;
		internal System.Windows.Forms.DataGridViewTextBoxColumn MetadataTokenDataGridViewTextBoxColumn;
		internal System.Windows.Forms.DataGridViewTextBoxColumn ConstantDataGridViewTextBoxColumn;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem MenDeleteAllInstructions;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.ToolStripMenuItem MenReplaceBody;
        private System.Windows.Forms.ToolStripSeparator MenSeparator;
        private System.Windows.Forms.TabPage TabExceptionHandlers;
        internal System.Windows.Forms.DataGridView ExceptionHandlers;
        private System.Windows.Forms.BindingSource ExceptionHandlerBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn catchTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tryStartDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tryEndDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn handlerStartDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn handlerEndDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filterStartDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filterEndDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        internal System.Windows.Forms.ContextMenuStrip ExceptionHandlersContextMenu;
        internal System.Windows.Forms.ToolStripMenuItem MenCreateExceptionHandler;
        internal System.Windows.Forms.ToolStripMenuItem MenEditExceptionHandler;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem MenDeleteExceptionHandler;
        private System.Windows.Forms.ToolStripMenuItem MenDeleteAllExceptionHandlers;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
		
	}
}

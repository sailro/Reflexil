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
            this.OperandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstructionsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.MenSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.MenDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.MenDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ConstantDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterDefinitionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.Instructions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Instructions_MouseDown);
            this.Instructions.DragOver += new System.Windows.Forms.DragEventHandler(this.Instructions_DragOver);
            this.Instructions.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Instructions_MouseMove);
            this.Instructions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Instructions_CellFormatting);
            this.Instructions.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Instructions_RowPostPaint);
            this.Instructions.DragDrop += new System.Windows.Forms.DragEventHandler(this.Instructions_DragDrop);
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
            // InstructionsContextMenu
            // 
            this.InstructionsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenCreate,
            this.MenEdit,
            this.MenReplace,
            this.MenSeparator,
            this.MenDelete,
            this.MenDeleteAll});
            this.InstructionsContextMenu.Name = "ContextMenuStrip";
            this.InstructionsContextMenu.Size = new System.Drawing.Size(198, 120);
            this.InstructionsContextMenu.Opened += new System.EventHandler(this.ContextMenu_Opened);
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
            // MenReplace
            // 
            this.MenReplace.Name = "MenReplace";
            this.MenReplace.Size = new System.Drawing.Size(197, 22);
            this.MenReplace.Text = "Replace all with code...";
            this.MenReplace.Click += new System.EventHandler(this.MenReplace_Click);
            // 
            // MenSeparator
            // 
            this.MenSeparator.Name = "MenSeparator";
            this.MenSeparator.Size = new System.Drawing.Size(194, 6);
            // 
            // MenDelete
            // 
            this.MenDelete.Name = "MenDelete";
            this.MenDelete.Size = new System.Drawing.Size(197, 22);
            this.MenDelete.Text = "Delete";
            this.MenDelete.Click += new System.EventHandler(this.MenDelete_Click);
            // 
            // MenDeleteAll
            // 
            this.MenDeleteAll.Name = "MenDeleteAll";
            this.MenDeleteAll.Size = new System.Drawing.Size(197, 22);
            this.MenDeleteAll.Text = "Delete all";
            this.MenDeleteAll.Click += new System.EventHandler(this.MenDeleteAll_Click);
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
            this.Variables.Name = "Variables";
            this.Variables.ReadOnly = true;
            this.Variables.Size = new System.Drawing.Size(671, 442);
            this.Variables.TabIndex = 3;
            this.Variables.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Instructions_RowPostPaint);
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
            this.Parameters.Name = "Parameters";
            this.Parameters.ReadOnly = true;
            this.Parameters.Size = new System.Drawing.Size(671, 442);
            this.Parameters.TabIndex = 0;
            this.Parameters.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.Instructions_RowPostPaint);
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
            // ConstantDataGridViewTextBoxColumn
            // 
            this.ConstantDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ConstantDataGridViewTextBoxColumn.DataPropertyName = "Constant";
            this.ConstantDataGridViewTextBoxColumn.HeaderText = "Constant";
            this.ConstantDataGridViewTextBoxColumn.Name = "ConstantDataGridViewTextBoxColumn";
            this.ConstantDataGridViewTextBoxColumn.ReadOnly = true;
            this.ConstantDataGridViewTextBoxColumn.Width = 74;
            // 
            // ParameterDefinitionBindingSource
            // 
            this.ParameterDefinitionBindingSource.DataSource = typeof(Mono.Cecil.ParameterDefinition);
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
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.DataGridView Instructions;
		internal System.Windows.Forms.BindingSource InstructionBindingSource;
		internal System.Windows.Forms.ContextMenuStrip InstructionsContextMenu;
		internal System.Windows.Forms.ToolStripMenuItem MenCreate;
		internal System.Windows.Forms.ToolStripMenuItem MenEdit;
		internal System.Windows.Forms.ToolStripMenuItem MenDelete;
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
        private System.Windows.Forms.ToolStripMenuItem MenDeleteAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.ToolStripMenuItem MenReplace;
        private System.Windows.Forms.ToolStripSeparator MenSeparator;
		
	}
}

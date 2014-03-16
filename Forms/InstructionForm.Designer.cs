using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Forms
{
	
	public partial class InstructionForm : System.Windows.Forms.Form
	{
		
		//Form overrides dispose to clean up the component list.
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

        //Required by the Windows Form Designer
		
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.OperandPanel = new System.Windows.Forms.Panel();
			this.LabOperand = new System.Windows.Forms.Label();
			this.OpCodes = new System.Windows.Forms.ComboBox();
			this.OpCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.LabOpCode = new System.Windows.Forms.Label();
			this.Operands = new System.Windows.Forms.ComboBox();
			this.LabOperandType = new System.Windows.Forms.Label();
			this.LabOpCodeDesc = new System.Windows.Forms.Label();
			this.RtbOpCodeDesc = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.OpCodeBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// OperandPanel
			// 
			this.OperandPanel.Location = new System.Drawing.Point(113, 146);
			this.OperandPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.OperandPanel.Name = "OperandPanel";
			this.OperandPanel.Size = new System.Drawing.Size(413, 26);
			this.OperandPanel.TabIndex = 3;
			// 
			// LabOperand
			// 
			this.LabOperand.AutoSize = true;
			this.LabOperand.Location = new System.Drawing.Point(11, 146);
			this.LabOperand.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LabOperand.Name = "LabOperand";
			this.LabOperand.Size = new System.Drawing.Size(64, 17);
			this.LabOperand.TabIndex = 10;
			this.LabOperand.Text = "Operand";
			// 
			// OpCodes
			// 
			this.OpCodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.OpCodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.OpCodes.DataSource = this.OpCodeBindingSource;
			this.OpCodes.FormattingEnabled = true;
			this.OpCodes.Location = new System.Drawing.Point(113, 17);
			this.OpCodes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.OpCodes.Name = "OpCodes";
			this.OpCodes.Size = new System.Drawing.Size(412, 24);
			this.OpCodes.TabIndex = 0;
			this.OpCodes.SelectedIndexChanged += new System.EventHandler(this.OpCodes_SelectedIndexChanged);
			this.OpCodes.TextChanged += new System.EventHandler(this.OpCodes_TextChanged);
			// 
			// OpCodeBindingSource
			// 
			this.OpCodeBindingSource.DataSource = typeof(Mono.Cecil.Cil.OpCode);
			// 
			// LabOpCode
			// 
			this.LabOpCode.AutoSize = true;
			this.LabOpCode.Location = new System.Drawing.Point(11, 21);
			this.LabOpCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LabOpCode.Name = "LabOpCode";
			this.LabOpCode.Size = new System.Drawing.Size(60, 17);
			this.LabOpCode.TabIndex = 7;
			this.LabOpCode.Text = "OpCode";
			// 
			// Operands
			// 
			this.Operands.DisplayMember = "Label";
			this.Operands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Operands.FormattingEnabled = true;
			this.Operands.Location = new System.Drawing.Point(113, 111);
			this.Operands.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Operands.Name = "Operands";
			this.Operands.Size = new System.Drawing.Size(412, 24);
			this.Operands.TabIndex = 2;
			this.Operands.ValueMember = "Label";
			this.Operands.SelectedIndexChanged += new System.EventHandler(this.Operands_SelectedIndexChanged);
			// 
			// LabOperandType
			// 
			this.LabOperandType.AutoSize = true;
			this.LabOperandType.Location = new System.Drawing.Point(11, 114);
			this.LabOperandType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LabOperandType.Name = "LabOperandType";
			this.LabOperandType.Size = new System.Drawing.Size(95, 17);
			this.LabOperandType.TabIndex = 6;
			this.LabOperandType.Text = "Operand type";
			// 
			// LabOpCodeDesc
			// 
			this.LabOpCodeDesc.AutoSize = true;
			this.LabOpCodeDesc.Location = new System.Drawing.Point(11, 58);
			this.LabOpCodeDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LabOpCodeDesc.Name = "LabOpCodeDesc";
			this.LabOpCodeDesc.Size = new System.Drawing.Size(79, 17);
			this.LabOpCodeDesc.TabIndex = 12;
			this.LabOpCodeDesc.Text = "Description";
			// 
			// RtbOpCodeDesc
			// 
			this.RtbOpCodeDesc.BackColor = System.Drawing.SystemColors.Control;
			this.RtbOpCodeDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.RtbOpCodeDesc.Location = new System.Drawing.Point(113, 50);
			this.RtbOpCodeDesc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.RtbOpCodeDesc.Name = "RtbOpCodeDesc";
			this.RtbOpCodeDesc.ReadOnly = true;
			this.RtbOpCodeDesc.Size = new System.Drawing.Size(413, 52);
			this.RtbOpCodeDesc.TabIndex = 1;
			this.RtbOpCodeDesc.Text = "";
			// 
			// InstructionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(715, 181);
			this.Controls.Add(this.RtbOpCodeDesc);
			this.Controls.Add(this.LabOpCodeDesc);
			this.Controls.Add(this.OperandPanel);
			this.Controls.Add(this.LabOperand);
			this.Controls.Add(this.LabOperandType);
			this.Controls.Add(this.Operands);
			this.Controls.Add(this.OpCodes);
			this.Controls.Add(this.LabOpCode);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "InstructionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "InstructionForm";
			((System.ComponentModel.ISupportInitialize)(this.OpCodeBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		internal System.Windows.Forms.Panel OperandPanel;
		internal System.Windows.Forms.Label LabOperand;
        internal System.Windows.Forms.ComboBox OpCodes;
		internal System.Windows.Forms.Label LabOpCode;
		internal System.Windows.Forms.ComboBox Operands;
		internal System.Windows.Forms.Label LabOperandType;
		internal System.Windows.Forms.BindingSource OpCodeBindingSource;
		internal System.Windows.Forms.Label LabOpCodeDesc;
		internal System.Windows.Forms.RichTextBox RtbOpCodeDesc;
        private System.ComponentModel.IContainer components;
	}
}



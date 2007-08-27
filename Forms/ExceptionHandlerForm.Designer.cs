using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Reflexil.Forms
{
	
	public partial class ExceptionHandlerForm : System.Windows.Forms.Form
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
            this.Types = new System.Windows.Forms.ComboBox();
            this.OpCodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LabType = new System.Windows.Forms.Label();
            this.LabCatchType = new System.Windows.Forms.Label();
            this.typeReferenceEditor1 = new Reflexil.Editors.TypeReferenceEditor();
            this.TryStart = new Reflexil.Editors.InstructionReferenceEditor();
            this.TryEnd = new Reflexil.Editors.InstructionReferenceEditor();
            this.HandleStart = new Reflexil.Editors.InstructionReferenceEditor();
            this.HandleEnd = new Reflexil.Editors.InstructionReferenceEditor();
            this.LabTryStart = new System.Windows.Forms.Label();
            this.LabTryEnd = new System.Windows.Forms.Label();
            this.LabHandleStart = new System.Windows.Forms.Label();
            this.LabHandleEnd = new System.Windows.Forms.Label();
            this.LabFilterEnd = new System.Windows.Forms.Label();
            this.LabFilterStart = new System.Windows.Forms.Label();
            this.FilterEnd = new Reflexil.Editors.InstructionReferenceEditor();
            this.FilterStart = new Reflexil.Editors.InstructionReferenceEditor();
            ((System.ComponentModel.ISupportInitialize)(this.OpCodeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Types
            // 
            this.Types.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Types.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Types.DataSource = this.OpCodeBindingSource;
            this.Types.FormattingEnabled = true;
            this.Types.Location = new System.Drawing.Point(85, 14);
            this.Types.Name = "Types";
            this.Types.Size = new System.Drawing.Size(310, 21);
            this.Types.TabIndex = 2;
            this.Types.SelectedIndexChanged += new System.EventHandler(this.OpCodes_SelectedIndexChanged);
            this.Types.TextChanged += new System.EventHandler(this.OpCodes_TextChanged);
            // 
            // OpCodeBindingSource
            // 
            this.OpCodeBindingSource.DataSource = typeof(Mono.Cecil.Cil.OpCode);
            // 
            // LabType
            // 
            this.LabType.AutoSize = true;
            this.LabType.Location = new System.Drawing.Point(8, 17);
            this.LabType.Name = "LabType";
            this.LabType.Size = new System.Drawing.Size(31, 13);
            this.LabType.TabIndex = 7;
            this.LabType.Text = "Type";
            // 
            // LabCatchType
            // 
            this.LabCatchType.AutoSize = true;
            this.LabCatchType.Location = new System.Drawing.Point(8, 44);
            this.LabCatchType.Name = "LabCatchType";
            this.LabCatchType.Size = new System.Drawing.Size(58, 13);
            this.LabCatchType.TabIndex = 9;
            this.LabCatchType.Text = "Catch type";
            // 
            // typeReferenceEditor1
            // 
            this.typeReferenceEditor1.BackColor = System.Drawing.SystemColors.Window;
            this.typeReferenceEditor1.Location = new System.Drawing.Point(85, 41);
            this.typeReferenceEditor1.Name = "typeReferenceEditor1";
            this.typeReferenceEditor1.SelectedOperand = null;
            this.typeReferenceEditor1.Size = new System.Drawing.Size(310, 21);
            this.typeReferenceEditor1.TabIndex = 10;
            // 
            // TryStart
            // 
            this.TryStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TryStart.FormattingEnabled = true;
            this.TryStart.Location = new System.Drawing.Point(85, 68);
            this.TryStart.Name = "TryStart";
            this.TryStart.Size = new System.Drawing.Size(310, 21);
            this.TryStart.TabIndex = 11;
            // 
            // TryEnd
            // 
            this.TryEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TryEnd.FormattingEnabled = true;
            this.TryEnd.Location = new System.Drawing.Point(85, 95);
            this.TryEnd.Name = "TryEnd";
            this.TryEnd.Size = new System.Drawing.Size(310, 21);
            this.TryEnd.TabIndex = 13;
            // 
            // HandleStart
            // 
            this.HandleStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HandleStart.FormattingEnabled = true;
            this.HandleStart.Location = new System.Drawing.Point(85, 122);
            this.HandleStart.Name = "HandleStart";
            this.HandleStart.Size = new System.Drawing.Size(310, 21);
            this.HandleStart.TabIndex = 14;
            // 
            // HandleEnd
            // 
            this.HandleEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HandleEnd.FormattingEnabled = true;
            this.HandleEnd.Location = new System.Drawing.Point(85, 149);
            this.HandleEnd.Name = "HandleEnd";
            this.HandleEnd.Size = new System.Drawing.Size(310, 21);
            this.HandleEnd.TabIndex = 15;
            // 
            // LabTryStart
            // 
            this.LabTryStart.AutoSize = true;
            this.LabTryStart.Location = new System.Drawing.Point(8, 71);
            this.LabTryStart.Name = "LabTryStart";
            this.LabTryStart.Size = new System.Drawing.Size(45, 13);
            this.LabTryStart.TabIndex = 16;
            this.LabTryStart.Text = "Try start";
            // 
            // LabTryEnd
            // 
            this.LabTryEnd.AutoSize = true;
            this.LabTryEnd.Location = new System.Drawing.Point(8, 98);
            this.LabTryEnd.Name = "LabTryEnd";
            this.LabTryEnd.Size = new System.Drawing.Size(43, 13);
            this.LabTryEnd.TabIndex = 17;
            this.LabTryEnd.Text = "Try end";
            // 
            // LabHandleStart
            // 
            this.LabHandleStart.AutoSize = true;
            this.LabHandleStart.Location = new System.Drawing.Point(8, 125);
            this.LabHandleStart.Name = "LabHandleStart";
            this.LabHandleStart.Size = new System.Drawing.Size(64, 13);
            this.LabHandleStart.TabIndex = 18;
            this.LabHandleStart.Text = "Handle start";
            // 
            // LabHandleEnd
            // 
            this.LabHandleEnd.AutoSize = true;
            this.LabHandleEnd.Location = new System.Drawing.Point(8, 152);
            this.LabHandleEnd.Name = "LabHandleEnd";
            this.LabHandleEnd.Size = new System.Drawing.Size(62, 13);
            this.LabHandleEnd.TabIndex = 19;
            this.LabHandleEnd.Text = "Handle end";
            // 
            // LabFilterEnd
            // 
            this.LabFilterEnd.AutoSize = true;
            this.LabFilterEnd.Location = new System.Drawing.Point(8, 206);
            this.LabFilterEnd.Name = "LabFilterEnd";
            this.LabFilterEnd.Size = new System.Drawing.Size(51, 13);
            this.LabFilterEnd.TabIndex = 23;
            this.LabFilterEnd.Text = "Filter End";
            // 
            // LabFilterStart
            // 
            this.LabFilterStart.AutoSize = true;
            this.LabFilterStart.Location = new System.Drawing.Point(8, 179);
            this.LabFilterStart.Name = "LabFilterStart";
            this.LabFilterStart.Size = new System.Drawing.Size(52, 13);
            this.LabFilterStart.TabIndex = 22;
            this.LabFilterStart.Text = "Filter start";
            // 
            // FilterEnd
            // 
            this.FilterEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterEnd.FormattingEnabled = true;
            this.FilterEnd.Location = new System.Drawing.Point(85, 203);
            this.FilterEnd.Name = "FilterEnd";
            this.FilterEnd.Size = new System.Drawing.Size(310, 21);
            this.FilterEnd.TabIndex = 21;
            // 
            // FilterStart
            // 
            this.FilterStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterStart.FormattingEnabled = true;
            this.FilterStart.Location = new System.Drawing.Point(85, 176);
            this.FilterStart.Name = "FilterStart";
            this.FilterStart.Size = new System.Drawing.Size(310, 21);
            this.FilterStart.TabIndex = 20;
            // 
            // ExceptionHandlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 232);
            this.Controls.Add(this.LabFilterEnd);
            this.Controls.Add(this.LabFilterStart);
            this.Controls.Add(this.FilterEnd);
            this.Controls.Add(this.FilterStart);
            this.Controls.Add(this.LabHandleEnd);
            this.Controls.Add(this.LabHandleStart);
            this.Controls.Add(this.LabTryEnd);
            this.Controls.Add(this.LabTryStart);
            this.Controls.Add(this.HandleEnd);
            this.Controls.Add(this.HandleStart);
            this.Controls.Add(this.TryEnd);
            this.Controls.Add(this.TryStart);
            this.Controls.Add(this.typeReferenceEditor1);
            this.Controls.Add(this.LabCatchType);
            this.Controls.Add(this.Types);
            this.Controls.Add(this.LabType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExceptionHandlerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExceptionHandlerForm";
            ((System.ComponentModel.ISupportInitialize)(this.OpCodeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.ComboBox Types;
        internal System.Windows.Forms.Label LabType;
        internal System.Windows.Forms.BindingSource OpCodeBindingSource;
        private System.ComponentModel.IContainer components;
        internal System.Windows.Forms.Label LabCatchType;
        private Reflexil.Editors.TypeReferenceEditor typeReferenceEditor1;
        private Reflexil.Editors.InstructionReferenceEditor TryStart;
        private Reflexil.Editors.InstructionReferenceEditor TryEnd;
        private Reflexil.Editors.InstructionReferenceEditor HandleStart;
        private Reflexil.Editors.InstructionReferenceEditor HandleEnd;
        internal System.Windows.Forms.Label LabTryStart;
        internal System.Windows.Forms.Label LabTryEnd;
        internal System.Windows.Forms.Label LabHandleStart;
        internal System.Windows.Forms.Label LabHandleEnd;
        internal System.Windows.Forms.Label LabFilterEnd;
        internal System.Windows.Forms.Label LabFilterStart;
        private Reflexil.Editors.InstructionReferenceEditor FilterEnd;
        private Reflexil.Editors.InstructionReferenceEditor FilterStart;
	}
}



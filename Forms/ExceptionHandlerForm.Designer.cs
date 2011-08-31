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
            this.Types = new System.Windows.Forms.ComboBox();
            this.LabHandlerType = new System.Windows.Forms.Label();
            this.LabCatchType = new System.Windows.Forms.Label();
            this.LabTryStart = new System.Windows.Forms.Label();
            this.LabTryEnd = new System.Windows.Forms.Label();
            this.LabHandleStart = new System.Windows.Forms.Label();
            this.LabHandleEnd = new System.Windows.Forms.Label();
            this.LabFilterStart = new System.Windows.Forms.Label();
            this.FilterEnd = new Reflexil.Editors.InstructionReferenceEditor();
            this.FilterStart = new Reflexil.Editors.InstructionReferenceEditor();
            this.HandlerEnd = new Reflexil.Editors.InstructionReferenceEditor();
            this.HandlerStart = new Reflexil.Editors.InstructionReferenceEditor();
            this.TryEnd = new Reflexil.Editors.InstructionReferenceEditor();
            this.TryStart = new Reflexil.Editors.InstructionReferenceEditor();
            this.CatchType = new Reflexil.Editors.TypeReferenceEditor();
            this.SuspendLayout();
            // 
            // Types
            // 
            this.Types.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Types.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Types.FormattingEnabled = true;
            this.Types.Location = new System.Drawing.Point(85, 14);
            this.Types.Name = "Types";
            this.Types.Size = new System.Drawing.Size(310, 21);
            this.Types.TabIndex = 0;
            this.Types.SelectedIndexChanged += new System.EventHandler(this.Types_SelectedIndexChanged);
            // 
            // LabHandlerType
            // 
            this.LabHandlerType.AutoSize = true;
            this.LabHandlerType.Location = new System.Drawing.Point(8, 17);
            this.LabHandlerType.Name = "LabHandlerType";
            this.LabHandlerType.Size = new System.Drawing.Size(71, 13);
            this.LabHandlerType.TabIndex = 7;
            this.LabHandlerType.Text = "Handler Type";
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
            this.LabHandleStart.Size = new System.Drawing.Size(67, 13);
            this.LabHandleStart.TabIndex = 18;
            this.LabHandleStart.Text = "Handler start";
            // 
            // LabHandleEnd
            // 
            this.LabHandleEnd.AutoSize = true;
            this.LabHandleEnd.Location = new System.Drawing.Point(8, 152);
            this.LabHandleEnd.Name = "LabHandleEnd";
            this.LabHandleEnd.Size = new System.Drawing.Size(65, 13);
            this.LabHandleEnd.TabIndex = 19;
            this.LabHandleEnd.Text = "Handler end";
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
            this.FilterEnd.ReferencedItems = null;
            this.FilterEnd.SelectedOperand = null;
            this.FilterEnd.Size = new System.Drawing.Size(310, 21);
            this.FilterEnd.TabIndex = 7;
            this.FilterEnd.Visible = false;
            // 
            // FilterStart
            // 
            this.FilterStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterStart.FormattingEnabled = true;
            this.FilterStart.Location = new System.Drawing.Point(85, 176);
            this.FilterStart.Name = "FilterStart";
            this.FilterStart.ReferencedItems = null;
            this.FilterStart.SelectedOperand = null;
            this.FilterStart.Size = new System.Drawing.Size(310, 21);
            this.FilterStart.TabIndex = 6;
            // 
            // HandlerEnd
            // 
            this.HandlerEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HandlerEnd.FormattingEnabled = true;
            this.HandlerEnd.Location = new System.Drawing.Point(85, 149);
            this.HandlerEnd.Name = "HandlerEnd";
            this.HandlerEnd.ReferencedItems = null;
            this.HandlerEnd.SelectedOperand = null;
            this.HandlerEnd.Size = new System.Drawing.Size(310, 21);
            this.HandlerEnd.TabIndex = 5;
            // 
            // HandlerStart
            // 
            this.HandlerStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.HandlerStart.FormattingEnabled = true;
            this.HandlerStart.Location = new System.Drawing.Point(85, 122);
            this.HandlerStart.Name = "HandlerStart";
            this.HandlerStart.ReferencedItems = null;
            this.HandlerStart.SelectedOperand = null;
            this.HandlerStart.Size = new System.Drawing.Size(310, 21);
            this.HandlerStart.TabIndex = 4;
            // 
            // TryEnd
            // 
            this.TryEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TryEnd.FormattingEnabled = true;
            this.TryEnd.Location = new System.Drawing.Point(85, 95);
            this.TryEnd.Name = "TryEnd";
            this.TryEnd.ReferencedItems = null;
            this.TryEnd.SelectedOperand = null;
            this.TryEnd.Size = new System.Drawing.Size(310, 21);
            this.TryEnd.TabIndex = 3;
            // 
            // TryStart
            // 
            this.TryStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TryStart.FormattingEnabled = true;
            this.TryStart.Location = new System.Drawing.Point(85, 68);
            this.TryStart.Name = "TryStart";
            this.TryStart.ReferencedItems = null;
            this.TryStart.SelectedOperand = null;
            this.TryStart.Size = new System.Drawing.Size(310, 21);
            this.TryStart.TabIndex = 2;
            // 
            // CatchType
            // 
            this.CatchType.AssemblyRestriction = null;
            this.CatchType.BackColor = System.Drawing.SystemColors.Window;
            this.CatchType.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.CatchType.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.CatchType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CatchType.Location = new System.Drawing.Point(85, 41);
            this.CatchType.Name = "CatchType";
            this.CatchType.SelectedOperand = null;
            this.CatchType.Size = new System.Drawing.Size(310, 21);
            this.CatchType.TabIndex = 1;
            this.CatchType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CatchType.UseVisualStyleBackColor = false;
            // 
            // ExceptionHandlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 203);
            this.Controls.Add(this.LabFilterStart);
            this.Controls.Add(this.FilterEnd);
            this.Controls.Add(this.FilterStart);
            this.Controls.Add(this.LabHandleEnd);
            this.Controls.Add(this.LabHandleStart);
            this.Controls.Add(this.LabTryEnd);
            this.Controls.Add(this.LabTryStart);
            this.Controls.Add(this.HandlerEnd);
            this.Controls.Add(this.HandlerStart);
            this.Controls.Add(this.TryEnd);
            this.Controls.Add(this.TryStart);
            this.Controls.Add(this.CatchType);
            this.Controls.Add(this.LabCatchType);
            this.Controls.Add(this.Types);
            this.Controls.Add(this.LabHandlerType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExceptionHandlerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExceptionHandlerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.ComboBox Types;
        internal System.Windows.Forms.Label LabHandlerType;
        private System.ComponentModel.IContainer components = null;
        internal System.Windows.Forms.Label LabCatchType;
        internal System.Windows.Forms.Label LabTryStart;
        internal System.Windows.Forms.Label LabTryEnd;
        internal System.Windows.Forms.Label LabHandleStart;
        internal System.Windows.Forms.Label LabHandleEnd;
        internal System.Windows.Forms.Label LabFilterStart;
        internal Reflexil.Editors.TypeReferenceEditor CatchType;
        internal Reflexil.Editors.InstructionReferenceEditor TryStart;
        internal Reflexil.Editors.InstructionReferenceEditor TryEnd;
        internal Reflexil.Editors.InstructionReferenceEditor HandlerStart;
        internal Reflexil.Editors.InstructionReferenceEditor HandlerEnd;
        internal Reflexil.Editors.InstructionReferenceEditor FilterEnd;
        internal Reflexil.Editors.InstructionReferenceEditor FilterStart;
	}
}



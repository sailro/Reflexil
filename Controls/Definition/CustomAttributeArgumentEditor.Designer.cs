namespace Reflexil.Editors
{
    partial class CustomAttributeArgumentEditor
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
            this.TypeSpecification = new System.Windows.Forms.ComboBox();
            this.TypPanel = new System.Windows.Forms.Panel();
            this.LabOperand = new System.Windows.Forms.Label();
            this.LabSpecification = new System.Windows.Forms.Label();
            this.ArgumentPanel = new System.Windows.Forms.Panel();
            this.LabArgument = new System.Windows.Forms.Label();
            this.LabArgumentType = new System.Windows.Forms.Label();
            this.ArgumentTypes = new System.Windows.Forms.ComboBox();
            this.TypeReferenceEditor = new Reflexil.Editors.TypeReferenceEditor();
            this.TypPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TypeSpecification
            // 
            this.TypeSpecification.CausesValidation = false;
            this.TypeSpecification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypeSpecification.FormattingEnabled = true;
            this.TypeSpecification.Location = new System.Drawing.Point(75, 29);
            this.TypeSpecification.Name = "TypeSpecification";
            this.TypeSpecification.Size = new System.Drawing.Size(310, 21);
            this.TypeSpecification.TabIndex = 2;
            // 
            // TypPanel
            // 
            this.TypPanel.BackColor = System.Drawing.SystemColors.Info;
            this.TypPanel.Controls.Add(this.TypeReferenceEditor);
            this.TypPanel.Location = new System.Drawing.Point(75, 1);
            this.TypPanel.Name = "TypPanel";
            this.TypPanel.Size = new System.Drawing.Size(310, 22);
            this.TypPanel.TabIndex = 1;
            // 
            // LabOperand
            // 
            this.LabOperand.AutoSize = true;
            this.LabOperand.Location = new System.Drawing.Point(0, 5);
            this.LabOperand.Name = "LabOperand";
            this.LabOperand.Size = new System.Drawing.Size(31, 13);
            this.LabOperand.TabIndex = 14;
            this.LabOperand.Text = "Type";
            // 
            // LabSpecification
            // 
            this.LabSpecification.AutoSize = true;
            this.LabSpecification.Location = new System.Drawing.Point(0, 32);
            this.LabSpecification.Name = "LabSpecification";
            this.LabSpecification.Size = new System.Drawing.Size(68, 13);
            this.LabSpecification.TabIndex = 16;
            this.LabSpecification.Text = "Specification";
            // 
            // ArgumentPanel
            // 
            this.ArgumentPanel.BackColor = System.Drawing.SystemColors.Info;
            this.ArgumentPanel.Location = new System.Drawing.Point(75, 83);
            this.ArgumentPanel.Name = "ArgumentPanel";
            this.ArgumentPanel.Size = new System.Drawing.Size(310, 21);
            this.ArgumentPanel.TabIndex = 24;
            // 
            // LabArgument
            // 
            this.LabArgument.AutoSize = true;
            this.LabArgument.Location = new System.Drawing.Point(0, 86);
            this.LabArgument.Name = "LabArgument";
            this.LabArgument.Size = new System.Drawing.Size(52, 13);
            this.LabArgument.TabIndex = 23;
            this.LabArgument.Text = "Argument";
            // 
            // LabArgumentType
            // 
            this.LabArgumentType.AutoSize = true;
            this.LabArgumentType.Location = new System.Drawing.Point(0, 59);
            this.LabArgumentType.Name = "LabArgumentType";
            this.LabArgumentType.Size = new System.Drawing.Size(49, 13);
            this.LabArgumentType.TabIndex = 22;
            this.LabArgumentType.Text = "Arg. type";
            // 
            // ArgumentTypes
            // 
            this.ArgumentTypes.DisplayMember = "Label";
            this.ArgumentTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ArgumentTypes.FormattingEnabled = true;
            this.ArgumentTypes.Location = new System.Drawing.Point(75, 56);
            this.ArgumentTypes.Name = "ArgumentTypes";
            this.ArgumentTypes.Size = new System.Drawing.Size(310, 21);
            this.ArgumentTypes.TabIndex = 21;
            this.ArgumentTypes.ValueMember = "Label";
            this.ArgumentTypes.SelectedIndexChanged += new System.EventHandler(this.ArgumentTypes_SelectedIndexChanged);
            // 
            // TypeReferenceEditor
            // 
            this.TypeReferenceEditor.AssemblyRestriction = null;
            this.TypeReferenceEditor.BackColor = System.Drawing.SystemColors.Window;
            this.TypeReferenceEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TypeReferenceEditor.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.TypeReferenceEditor.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.TypeReferenceEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TypeReferenceEditor.Location = new System.Drawing.Point(0, 0);
            this.TypeReferenceEditor.Name = "TypeReferenceEditor";
            this.TypeReferenceEditor.SelectedOperand = null;
            this.TypeReferenceEditor.Size = new System.Drawing.Size(310, 22);
            this.TypeReferenceEditor.TabIndex = 0;
            this.TypeReferenceEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TypeReferenceEditor.UseVisualStyleBackColor = false;
            // 
            // CustomAttributeArgumentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ArgumentPanel);
            this.Controls.Add(this.LabArgument);
            this.Controls.Add(this.LabArgumentType);
            this.Controls.Add(this.ArgumentTypes);
            this.Controls.Add(this.LabSpecification);
            this.Controls.Add(this.TypPanel);
            this.Controls.Add(this.LabOperand);
            this.Controls.Add(this.TypeSpecification);
            this.Name = "CustomAttributeArgumentEditor";
            this.Size = new System.Drawing.Size(391, 110);
            this.TypPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        internal System.Windows.Forms.Panel TypPanel;
        internal System.Windows.Forms.Label LabOperand;
        internal System.Windows.Forms.Label LabSpecification;
        internal System.Windows.Forms.Panel ArgumentPanel;
        internal System.Windows.Forms.Label LabArgument;
        internal System.Windows.Forms.Label LabArgumentType;
        internal System.Windows.Forms.ComboBox ArgumentTypes;
        internal TypeReferenceEditor TypeReferenceEditor;
        internal System.Windows.Forms.ComboBox TypeSpecification;
	}
}

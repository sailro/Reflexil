namespace Reflexil.Handlers
{
	partial class TypeDefinitionHandler
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabAttributes = new System.Windows.Forms.TabPage();
            this.Attributes = new Reflexil.Editors.TypeAttributesControl();
            this.TabInterfaces = new System.Windows.Forms.TabPage();
            this.Interfaces = new Reflexil.Editors.InterfaceGridControl();
            this.TabControl.SuspendLayout();
            this.TabAttributes.SuspendLayout();
            this.TabInterfaces.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabAttributes);
            this.TabControl.Controls.Add(this.TabInterfaces);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(526, 400);
            this.TabControl.TabIndex = 0;
            // 
            // TabAttributes
            // 
            this.TabAttributes.Controls.Add(this.Attributes);
            this.TabAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabAttributes.Name = "TabAttributes";
            this.TabAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabAttributes.Size = new System.Drawing.Size(518, 374);
            this.TabAttributes.TabIndex = 0;
            this.TabAttributes.Text = "Attributes";
            this.TabAttributes.UseVisualStyleBackColor = true;
            // 
            // Attributes
            // 
            this.Attributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Attributes.Item = null;
            this.Attributes.Location = new System.Drawing.Point(3, 3);
            this.Attributes.Name = "Attributes";
            this.Attributes.ReadOnly = false;
            this.Attributes.Size = new System.Drawing.Size(512, 368);
            this.Attributes.TabIndex = 0;
            // 
            // TabInterfaces
            // 
            this.TabInterfaces.Controls.Add(this.Interfaces);
            this.TabInterfaces.Location = new System.Drawing.Point(4, 22);
            this.TabInterfaces.Name = "TabInterfaces";
            this.TabInterfaces.Padding = new System.Windows.Forms.Padding(3);
            this.TabInterfaces.Size = new System.Drawing.Size(518, 374);
            this.TabInterfaces.TabIndex = 1;
            this.TabInterfaces.Text = "Interfaces";
            this.TabInterfaces.UseVisualStyleBackColor = true;
            // 
            // Interfaces
            // 
            this.Interfaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Interfaces.Location = new System.Drawing.Point(3, 3);
            this.Interfaces.Name = "Interfaces";
            this.Interfaces.ReadOnly = false;
            this.Interfaces.Size = new System.Drawing.Size(512, 368);
            this.Interfaces.TabIndex = 0;
            this.Interfaces.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.TypeReference, Mono.Cecil.TypeDefinition>.GridUpdatedEventHandler(this.Interfaces_GridUpdated);
            // 
            // TypeDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "TypeDefinitionHandler";
            this.Size = new System.Drawing.Size(526, 400);
            this.TabControl.ResumeLayout(false);
            this.TabAttributes.ResumeLayout(false);
            this.TabInterfaces.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabAttributes;
        private Reflexil.Editors.TypeAttributesControl Attributes;
        private System.Windows.Forms.TabPage TabInterfaces;
        private Reflexil.Editors.InterfaceGridControl Interfaces;

    }
}

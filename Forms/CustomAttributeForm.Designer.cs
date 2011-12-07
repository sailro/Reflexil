namespace Reflexil.Forms
{
	partial class CustomAttributeForm
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
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ConstructorArguments = new Reflexil.Editors.CustomAttributeArgumentGridControl();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabCtorArgs = new System.Windows.Forms.TabPage();
            this.TabFields = new System.Windows.Forms.TabPage();
            this.TabProperties = new System.Windows.Forms.TabPage();
            this.TabAttributes = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.TabControl.SuspendLayout();
            this.TabCtorArgs.SuspendLayout();
            this.SuspendLayout();
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // ConstructorArguments
            // 
            this.ConstructorArguments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConstructorArguments.Location = new System.Drawing.Point(3, 3);
            this.ConstructorArguments.Name = "ConstructorArguments";
            this.ConstructorArguments.ReadOnly = false;
            this.ConstructorArguments.Size = new System.Drawing.Size(381, 189);
            this.ConstructorArguments.TabIndex = 0;
            this.ConstructorArguments.GridUpdated +=new Editors.GridControl<Mono.Cecil.CustomAttributeArgument?,Mono.Cecil.CustomAttribute>.GridUpdatedEventHandler(ConstructorArguments_GridUpdated);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabCtorArgs);
            this.TabControl.Controls.Add(this.TabFields);
            this.TabControl.Controls.Add(this.TabProperties);
            this.TabControl.Controls.Add(this.TabAttributes);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(395, 221);
            this.TabControl.TabIndex = 16;
            // 
            // TabCtorArgs
            // 
            this.TabCtorArgs.Controls.Add(this.ConstructorArguments);
            this.TabCtorArgs.Location = new System.Drawing.Point(4, 22);
            this.TabCtorArgs.Name = "TabCtorArgs";
            this.TabCtorArgs.Padding = new System.Windows.Forms.Padding(3);
            this.TabCtorArgs.Size = new System.Drawing.Size(387, 195);
            this.TabCtorArgs.TabIndex = 0;
            this.TabCtorArgs.Text = "Constructor arguments";
            this.TabCtorArgs.UseVisualStyleBackColor = true;
            // 
            // TabFields
            // 
            this.TabFields.Location = new System.Drawing.Point(4, 22);
            this.TabFields.Name = "TabFields";
            this.TabFields.Padding = new System.Windows.Forms.Padding(3);
            this.TabFields.Size = new System.Drawing.Size(387, 195);
            this.TabFields.TabIndex = 1;
            this.TabFields.Text = "Fields";
            this.TabFields.UseVisualStyleBackColor = true;
            // 
            // TabProperties
            // 
            this.TabProperties.Location = new System.Drawing.Point(4, 22);
            this.TabProperties.Name = "TabProperties";
            this.TabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.TabProperties.Size = new System.Drawing.Size(387, 195);
            this.TabProperties.TabIndex = 2;
            this.TabProperties.Text = "Properties";
            this.TabProperties.UseVisualStyleBackColor = true;
            // 
            // TabAttributes
            // 
            this.TabAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabAttributes.Name = "TabAttributes";
            this.TabAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabAttributes.Size = new System.Drawing.Size(387, 195);
            this.TabAttributes.TabIndex = 3;
            this.TabAttributes.Text = "Attributes";
            this.TabAttributes.UseVisualStyleBackColor = true;
            // 
            // CustomAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 221);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomAttributeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomAttributeForm";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.TabCtorArgs.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ErrorProvider ErrorProvider;
        protected Editors.CustomAttributeArgumentGridControl ConstructorArguments;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabCtorArgs;
        private System.Windows.Forms.TabPage TabFields;
        private System.Windows.Forms.TabPage TabProperties;
        private System.Windows.Forms.TabPage TabAttributes;
	}
}
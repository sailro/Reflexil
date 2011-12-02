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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabInstructions = new System.Windows.Forms.TabPage();
            this.Instructions = new Reflexil.Editors.InstructionGridControl();
            this.TabVariables = new System.Windows.Forms.TabPage();
            this.Variables = new Reflexil.Editors.VariableGridControl();
            this.TabParameters = new System.Windows.Forms.TabPage();
            this.Parameters = new Reflexil.Editors.ParameterGridControl();
            this.TabExceptionHandlers = new System.Windows.Forms.TabPage();
            this.ExceptionHandlers = new Reflexil.Editors.ExceptionHandlerGridControl();
            this.TabOverrides = new System.Windows.Forms.TabPage();
            this.Overrides = new Reflexil.Editors.OverrideGridControl();
            this.TabAttributes = new System.Windows.Forms.TabPage();
            this.Attributes = new Reflexil.Editors.MethodAttributesControl();
            this.TagCustomAttributes = new System.Windows.Forms.TabPage();
            this.CustomAttributes = new Reflexil.Editors.CustomAttributeGridControl();
            this.TabControl.SuspendLayout();
            this.TabInstructions.SuspendLayout();
            this.TabVariables.SuspendLayout();
            this.TabParameters.SuspendLayout();
            this.TabExceptionHandlers.SuspendLayout();
            this.TabOverrides.SuspendLayout();
            this.TabAttributes.SuspendLayout();
            this.TagCustomAttributes.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabInstructions);
            this.TabControl.Controls.Add(this.TabVariables);
            this.TabControl.Controls.Add(this.TabParameters);
            this.TabControl.Controls.Add(this.TabExceptionHandlers);
            this.TabControl.Controls.Add(this.TabOverrides);
            this.TabControl.Controls.Add(this.TabAttributes);
            this.TabControl.Controls.Add(this.TagCustomAttributes);
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
            // Instructions
            // 
            this.Instructions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Instructions.Location = new System.Drawing.Point(3, 3);
            this.Instructions.Name = "Instructions";
            this.Instructions.ReadOnly = false;
            this.Instructions.Size = new System.Drawing.Size(671, 442);
            this.Instructions.TabIndex = 0;
            this.Instructions.BodyReplaced += new Reflexil.Editors.InstructionGridControl.BodyReplacedEventHandler(this.Instructions_BodyReplaced);
            this.Instructions.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.Cil.Instruction, Mono.Cecil.MethodDefinition>.GridUpdatedEventHandler(this.Instructions_GridUpdated);
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
            // Variables
            // 
            this.Variables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Variables.Location = new System.Drawing.Point(3, 3);
            this.Variables.Name = "Variables";
            this.Variables.ReadOnly = false;
            this.Variables.Size = new System.Drawing.Size(671, 442);
            this.Variables.TabIndex = 0;
            this.Variables.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.Cil.VariableDefinition, Mono.Cecil.MethodDefinition>.GridUpdatedEventHandler(this.Variables_GridUpdated);
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
            this.Parameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Parameters.Location = new System.Drawing.Point(3, 3);
            this.Parameters.Name = "Parameters";
            this.Parameters.ReadOnly = false;
            this.Parameters.Size = new System.Drawing.Size(671, 442);
            this.Parameters.TabIndex = 0;
            this.Parameters.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.ParameterDefinition, Mono.Cecil.MethodDefinition>.GridUpdatedEventHandler(this.Parameters_GridUpdated);
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
            this.ExceptionHandlers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExceptionHandlers.Location = new System.Drawing.Point(3, 3);
            this.ExceptionHandlers.Name = "ExceptionHandlers";
            this.ExceptionHandlers.ReadOnly = false;
            this.ExceptionHandlers.Size = new System.Drawing.Size(671, 442);
            this.ExceptionHandlers.TabIndex = 0;
            this.ExceptionHandlers.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.Cil.ExceptionHandler, Mono.Cecil.MethodDefinition>.GridUpdatedEventHandler(this.ExceptionHandlers_GridUpdated);
            // 
            // TabOverrides
            // 
            this.TabOverrides.Controls.Add(this.Overrides);
            this.TabOverrides.Location = new System.Drawing.Point(4, 22);
            this.TabOverrides.Name = "TabOverrides";
            this.TabOverrides.Padding = new System.Windows.Forms.Padding(3);
            this.TabOverrides.Size = new System.Drawing.Size(677, 448);
            this.TabOverrides.TabIndex = 5;
            this.TabOverrides.Text = "Overrides";
            this.TabOverrides.UseVisualStyleBackColor = true;
            // 
            // Overrides
            // 
            this.Overrides.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Overrides.Location = new System.Drawing.Point(3, 3);
            this.Overrides.Name = "Overrides";
            this.Overrides.ReadOnly = false;
            this.Overrides.Size = new System.Drawing.Size(671, 442);
            this.Overrides.TabIndex = 0;
            this.Overrides.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.MethodReference, Mono.Cecil.MethodDefinition>.GridUpdatedEventHandler(this.Overrides_GridUpdated);
            // 
            // TabAttributes
            // 
            this.TabAttributes.Controls.Add(this.Attributes);
            this.TabAttributes.Location = new System.Drawing.Point(4, 22);
            this.TabAttributes.Name = "TabAttributes";
            this.TabAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TabAttributes.Size = new System.Drawing.Size(677, 448);
            this.TabAttributes.TabIndex = 4;
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
            this.Attributes.Size = new System.Drawing.Size(671, 442);
            this.Attributes.TabIndex = 0;
            // 
            // TagCustomAttributes
            // 
            this.TagCustomAttributes.Controls.Add(this.CustomAttributes);
            this.TagCustomAttributes.Location = new System.Drawing.Point(4, 22);
            this.TagCustomAttributes.Name = "TagCustomAttributes";
            this.TagCustomAttributes.Padding = new System.Windows.Forms.Padding(3);
            this.TagCustomAttributes.Size = new System.Drawing.Size(677, 448);
            this.TagCustomAttributes.TabIndex = 6;
            this.TagCustomAttributes.Text = "Custom attributes";
            this.TagCustomAttributes.UseVisualStyleBackColor = true;
            // 
            // CustomAttributes
            // 
            this.CustomAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CustomAttributes.Location = new System.Drawing.Point(3, 3);
            this.CustomAttributes.Name = "CustomAttributes";
            this.CustomAttributes.ReadOnly = false;
            this.CustomAttributes.Size = new System.Drawing.Size(671, 442);
            this.CustomAttributes.TabIndex = 0;
            this.CustomAttributes.GridUpdated += new Reflexil.Editors.GridControl<Mono.Cecil.CustomAttribute, Mono.Cecil.ICustomAttributeProvider>.GridUpdatedEventHandler(this.CustomAttributes_GridUpdated);
            // 
            // MethodDefinitionHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Name = "MethodDefinitionHandler";
            this.Size = new System.Drawing.Size(685, 474);
            this.TabControl.ResumeLayout(false);
            this.TabInstructions.ResumeLayout(false);
            this.TabVariables.ResumeLayout(false);
            this.TabParameters.ResumeLayout(false);
            this.TabExceptionHandlers.ResumeLayout(false);
            this.TabOverrides.ResumeLayout(false);
            this.TabAttributes.ResumeLayout(false);
            this.TagCustomAttributes.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		internal System.Windows.Forms.TabControl TabControl;
		internal System.Windows.Forms.TabPage TabInstructions;
		internal System.Windows.Forms.TabPage TabVariables;
        internal System.Windows.Forms.TabPage TabParameters;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabPage TabExceptionHandlers;
        private System.Windows.Forms.TabPage TabAttributes;
        private Reflexil.Editors.InstructionGridControl Instructions;
        private Reflexil.Editors.ExceptionHandlerGridControl ExceptionHandlers;
        private Reflexil.Editors.VariableGridControl Variables;
        private Reflexil.Editors.ParameterGridControl Parameters;
        private Reflexil.Editors.MethodAttributesControl Attributes;
        private System.Windows.Forms.TabPage TabOverrides;
        private Reflexil.Editors.OverrideGridControl Overrides;
        private System.Windows.Forms.TabPage TagCustomAttributes;
        private Editors.CustomAttributeGridControl CustomAttributes;
	}
}

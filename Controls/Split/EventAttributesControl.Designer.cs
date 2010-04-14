namespace Reflexil.Editors
{
    partial class EventAttributesControl
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
            this.EventType = new Reflexil.Editors.TypeSpecificationEditor();
            this.GbxEventType = new System.Windows.Forms.GroupBox();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.GbxEventType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.GbxEventType);
            // 
            // EventType
            // 
            this.EventType.AllowArray = true;
            this.EventType.AllowPointer = true;
            this.EventType.AllowReference = false;
            this.EventType.Location = new System.Drawing.Point(6, 19);
            this.EventType.MethodDefinition = null;
            this.EventType.Name = "EventType";
            this.EventType.SelectedTypeReference = null;
            this.EventType.Size = new System.Drawing.Size(383, 77);
            this.EventType.TabIndex = 5;
            this.EventType.Validating += new System.ComponentModel.CancelEventHandler(this.EventType_Validating);
            // 
            // GbxEventType
            // 
            this.GbxEventType.Controls.Add(this.EventType);
            this.GbxEventType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GbxEventType.Location = new System.Drawing.Point(0, 0);
            this.GbxEventType.Name = "GbxEventType";
            this.GbxEventType.Size = new System.Drawing.Size(491, 452);
            this.GbxEventType.TabIndex = 7;
            this.GbxEventType.TabStop = false;
            this.GbxEventType.Text = "Event type";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // EventAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "EventAttributesControl";
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.GbxEventType.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbxEventType;
        private TypeSpecificationEditor EventType;
        private System.Windows.Forms.ErrorProvider ErrorProvider;
    }
}

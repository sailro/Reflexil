namespace Reflexil.Editors
{
    partial class InterfaceGridControl
    {
        /// <summary>
        /// Required designer Parameter.
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
            this.SignatureDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            // 
            // Grid
            // 
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SignatureDataGridViewTextBoxColumn,
            });
            // 
            // SignatureDataGridViewTextBoxColumn
            // 
            this.SignatureDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SignatureDataGridViewTextBoxColumn.HeaderText = "Interface Type";
            this.SignatureDataGridViewTextBoxColumn.Name = "SignatureDataGridViewTextBoxColumn";
	        this.SignatureDataGridViewTextBoxColumn.DataPropertyName = "InterfaceType";
            this.SignatureDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ParameterBindingSource
            // 
            this.BindingSource.AllowNew = false;
            this.BindingSource.DataSource = typeof(Mono.Cecil.InterfaceImplementation);
        }

        #endregion

        internal System.Windows.Forms.DataGridViewTextBoxColumn SignatureDataGridViewTextBoxColumn;
    }
}

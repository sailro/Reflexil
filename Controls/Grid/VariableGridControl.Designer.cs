namespace Reflexil.Editors
{
    partial class VariableGridControl
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
            this.NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            // 
            // Grid
            // 
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameDataGridViewTextBoxColumn,
            this.VariableTypeDataGridViewTextBoxColumn});
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
            // VariableBindingSource
            // 
            this.BindingSource.AllowNew = false;
            this.BindingSource.DataSource = typeof(Mono.Cecil.Cil.VariableDefinition);
        }
        #endregion

        internal System.Windows.Forms.DataGridViewTextBoxColumn NameDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn VariableTypeDataGridViewTextBoxColumn;
    }
}

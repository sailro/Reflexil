namespace Reflexil.Editors
{
    partial class ParameterGridControl
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
            this.NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConstantDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            // 
            // Grid
            // 
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameDataGridViewTextBoxColumn,
            this.ParameterTypeDataGridViewTextBoxColumn,
            this.ConstantDataGridViewTextBoxColumn
            });
            // 
            // NameDataGridViewTextBoxColumn
            // 
            this.NameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.NameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn1";
            this.NameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ParameterTypeDataGridViewTextBoxColumn
            // 
            this.ParameterTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParameterTypeDataGridViewTextBoxColumn.DataPropertyName = "ParameterType";
            this.ParameterTypeDataGridViewTextBoxColumn.HeaderText = "Parameter Type";
            this.ParameterTypeDataGridViewTextBoxColumn.MinimumWidth = 128;
            this.ParameterTypeDataGridViewTextBoxColumn.Name = "ParameterTypeDataGridViewTextBoxColumn";
            this.ParameterTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.ParameterTypeDataGridViewTextBoxColumn.Width = 128;
            // 
            // ConstantDataGridViewTextBoxColumn
            // 
            this.ConstantDataGridViewTextBoxColumn.DataPropertyName = "Constant";
            this.ConstantDataGridViewTextBoxColumn.HeaderText = "Constant";
            this.ConstantDataGridViewTextBoxColumn.Name = "ConstantDataGridViewTextBoxColumn";
            this.ConstantDataGridViewTextBoxColumn.ReadOnly = true;
            this.ConstantDataGridViewTextBoxColumn.Width = 128;
            // 
            // ParameterBindingSource
            // 
            this.BindingSource.AllowNew = false;
            this.BindingSource.DataSource = typeof(Mono.Cecil.ParameterDefinition);
        }

        #endregion

        internal System.Windows.Forms.DataGridViewTextBoxColumn NameDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ParameterTypeDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ConstantDataGridViewTextBoxColumn;
    }
}

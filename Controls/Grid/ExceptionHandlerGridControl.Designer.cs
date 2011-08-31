namespace Reflexil.Editors
{
    partial class ExceptionHandlerGridControl
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
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catchTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tryStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tryEndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handlerStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.handlerEndDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filterStartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeDataGridViewTextBoxColumn,
            this.catchTypeDataGridViewTextBoxColumn,
            this.tryStartDataGridViewTextBoxColumn,
            this.tryEndDataGridViewTextBoxColumn,
            this.handlerStartDataGridViewTextBoxColumn,
            this.handlerEndDataGridViewTextBoxColumn,
            this.filterStartDataGridViewTextBoxColumn});
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "HandlerType";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Handler Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // catchTypeDataGridViewTextBoxColumn
            // 
            this.catchTypeDataGridViewTextBoxColumn.DataPropertyName = "CatchType";
            this.catchTypeDataGridViewTextBoxColumn.HeaderText = "Catch Type";
            this.catchTypeDataGridViewTextBoxColumn.Name = "catchTypeDataGridViewTextBoxColumn";
            this.catchTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.catchTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            // 
            // tryStartDataGridViewTextBoxColumn
            // 
            this.tryStartDataGridViewTextBoxColumn.DataPropertyName = "TryStart";
            this.tryStartDataGridViewTextBoxColumn.HeaderText = "Try Start";
            this.tryStartDataGridViewTextBoxColumn.Name = "tryStartDataGridViewTextBoxColumn";
            this.tryStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tryEndDataGridViewTextBoxColumn
            // 
            this.tryEndDataGridViewTextBoxColumn.DataPropertyName = "TryEnd";
            this.tryEndDataGridViewTextBoxColumn.HeaderText = "Try End";
            this.tryEndDataGridViewTextBoxColumn.Name = "tryEndDataGridViewTextBoxColumn";
            this.tryEndDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // handlerStartDataGridViewTextBoxColumn
            // 
            this.handlerStartDataGridViewTextBoxColumn.DataPropertyName = "HandlerStart";
            this.handlerStartDataGridViewTextBoxColumn.HeaderText = "Handler Start";
            this.handlerStartDataGridViewTextBoxColumn.Name = "handlerStartDataGridViewTextBoxColumn";
            this.handlerStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // handlerEndDataGridViewTextBoxColumn
            // 
            this.handlerEndDataGridViewTextBoxColumn.DataPropertyName = "HandlerEnd";
            this.handlerEndDataGridViewTextBoxColumn.HeaderText = "Handler End";
            this.handlerEndDataGridViewTextBoxColumn.Name = "handlerEndDataGridViewTextBoxColumn";
            this.handlerEndDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filterStartDataGridViewTextBoxColumn
            // 
            this.filterStartDataGridViewTextBoxColumn.DataPropertyName = "FilterStart";
            this.filterStartDataGridViewTextBoxColumn.HeaderText = "Filter Start";
            this.filterStartDataGridViewTextBoxColumn.Name = "filterStartDataGridViewTextBoxColumn";
            this.filterStartDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ExceptionHandlerBindingSource
            // 
            this.BindingSource.AllowNew = false;
            this.BindingSource.DataSource = typeof(Mono.Cecil.Cil.ExceptionHandler);
        }

        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn catchTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tryStartDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tryEndDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn handlerStartDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn handlerEndDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filterStartDataGridViewTextBoxColumn;

        #endregion
    }
}

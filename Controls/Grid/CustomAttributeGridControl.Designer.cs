namespace Reflexil.Editors
{
    partial class CustomAttributeGridControl
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
            this.HasConstructorArgumentsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HasFieldsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HasPropertiesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttributeTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            // 
            // Grid
            // 
            this.Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AttributeTypeColumn,
            this.HasConstructorArgumentsColumn,
            this.HasFieldsColumn,
            this.HasPropertiesColumn});
            // 
            // AttributeTypeColumn
            // 
            this.AttributeTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AttributeTypeColumn.DataPropertyName = "AttributeType";
            this.AttributeTypeColumn.HeaderText = "Attribute Type";
            this.AttributeTypeColumn.Name = "AttributeTypeColumn";
            this.AttributeTypeColumn.ReadOnly = true;
            // 
            // HasConstructorArgumentsColumn
            // 
            this.HasConstructorArgumentsColumn.DataPropertyName = "HasConstructorArguments";
            this.HasConstructorArgumentsColumn.HeaderText = "Constructor Arguments";
            this.HasConstructorArgumentsColumn.Name = "HasConstructorArgumentsColumn";
            this.HasConstructorArgumentsColumn.ReadOnly = true;
            // 
            // HasFieldsColumn
            // 
            this.HasFieldsColumn.DataPropertyName = "HasFields";
            this.HasFieldsColumn.HeaderText = "Fields";
            this.HasFieldsColumn.Name = "HasFieldsColumn";
            this.HasFieldsColumn.ReadOnly = true;
            // 
            // HasPropertiesColumn
            // 
            this.HasPropertiesColumn.DataPropertyName = "HasProperties";
            this.HasPropertiesColumn.HeaderText = "Properties";
            this.HasPropertiesColumn.Name = "HasPropertiesColumn";
            this.HasPropertiesColumn.ReadOnly = true;
            // 
            // CustomAttributeBindingSource
            // 
            this.BindingSource.AllowNew = false;
            this.BindingSource.DataSource = typeof(Mono.Cecil.CustomAttribute);
        }
        #endregion

        internal System.Windows.Forms.DataGridViewTextBoxColumn HasConstructorArgumentsColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn HasFieldsColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn HasPropertiesColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn AttributeTypeColumn;
    }
}

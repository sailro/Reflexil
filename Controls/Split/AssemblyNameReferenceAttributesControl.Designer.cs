namespace Reflexil.Editors
{
	partial class AssemblyNameReferenceAttributesControl
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
            this.components = new System.ComponentModel.Container();
            this.LabAssemblyName = new System.Windows.Forms.Label();
            this.LabAlgorithm = new System.Windows.Forms.Label();
            this.LabHash = new System.Windows.Forms.Label();
            this.LabAssemblyVersion = new System.Windows.Forms.Label();
            this.LabAssemblyCulture = new System.Windows.Forms.Label();
            this.AssemblyName = new System.Windows.Forms.TextBox();
            this.AssemblyCulture = new System.Windows.Forms.TextBox();
            this.LabPublicKey = new System.Windows.Forms.Label();
            this.PublicKey = new System.Windows.Forms.TextBox();
            this.LabPublicKeyToken = new System.Windows.Forms.Label();
            this.PublicKeyToken = new System.Windows.Forms.TextBox();
            this.Hash = new System.Windows.Forms.TextBox();
            this.Algorithm = new System.Windows.Forms.ComboBox();
            this.Major = new System.Windows.Forms.NumericUpDown();
            this.Minor = new System.Windows.Forms.NumericUpDown();
            this.Build = new System.Windows.Forms.NumericUpDown();
            this.Revision = new System.Windows.Forms.NumericUpDown();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Major)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Build)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Revision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Panel1MinSize = 150;
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.Revision);
            this.SplitContainer.Panel2.Controls.Add(this.Build);
            this.SplitContainer.Panel2.Controls.Add(this.Minor);
            this.SplitContainer.Panel2.Controls.Add(this.Major);
            this.SplitContainer.Panel2.Controls.Add(this.Algorithm);
            this.SplitContainer.Panel2.Controls.Add(this.Hash);
            this.SplitContainer.Panel2.Controls.Add(this.PublicKeyToken);
            this.SplitContainer.Panel2.Controls.Add(this.LabPublicKeyToken);
            this.SplitContainer.Panel2.Controls.Add(this.PublicKey);
            this.SplitContainer.Panel2.Controls.Add(this.LabPublicKey);
            this.SplitContainer.Panel2.Controls.Add(this.AssemblyCulture);
            this.SplitContainer.Panel2.Controls.Add(this.AssemblyName);
            this.SplitContainer.Panel2.Controls.Add(this.LabAssemblyCulture);
            this.SplitContainer.Panel2.Controls.Add(this.LabAssemblyVersion);
            this.SplitContainer.Panel2.Controls.Add(this.LabHash);
            this.SplitContainer.Panel2.Controls.Add(this.LabAlgorithm);
            this.SplitContainer.Panel2.Controls.Add(this.LabAssemblyName);
            this.SplitContainer.SplitterDistance = 150;
            // 
            // LabAssemblyName
            // 
            this.LabAssemblyName.AutoSize = true;
            this.LabAssemblyName.Location = new System.Drawing.Point(12, 12);
            this.LabAssemblyName.Name = "LabAssemblyName";
            this.LabAssemblyName.Size = new System.Drawing.Size(35, 13);
            this.LabAssemblyName.TabIndex = 0;
            this.LabAssemblyName.Text = "Name";
            // 
            // LabAlgorithm
            // 
            this.LabAlgorithm.AutoSize = true;
            this.LabAlgorithm.Location = new System.Drawing.Point(11, 142);
            this.LabAlgorithm.Name = "LabAlgorithm";
            this.LabAlgorithm.Size = new System.Drawing.Size(50, 13);
            this.LabAlgorithm.TabIndex = 2;
            this.LabAlgorithm.Text = "Algorithm";
            // 
            // LabHash
            // 
            this.LabHash.AutoSize = true;
            this.LabHash.Location = new System.Drawing.Point(12, 168);
            this.LabHash.Name = "LabHash";
            this.LabHash.Size = new System.Drawing.Size(32, 13);
            this.LabHash.TabIndex = 3;
            this.LabHash.Text = "Hash";
            // 
            // LabAssemblyVersion
            // 
            this.LabAssemblyVersion.AutoSize = true;
            this.LabAssemblyVersion.Location = new System.Drawing.Point(12, 64);
            this.LabAssemblyVersion.Name = "LabAssemblyVersion";
            this.LabAssemblyVersion.Size = new System.Drawing.Size(42, 13);
            this.LabAssemblyVersion.TabIndex = 4;
            this.LabAssemblyVersion.Text = "Version";
            // 
            // LabAssemblyCulture
            // 
            this.LabAssemblyCulture.AutoSize = true;
            this.LabAssemblyCulture.Location = new System.Drawing.Point(12, 38);
            this.LabAssemblyCulture.Name = "LabAssemblyCulture";
            this.LabAssemblyCulture.Size = new System.Drawing.Size(40, 13);
            this.LabAssemblyCulture.TabIndex = 5;
            this.LabAssemblyCulture.Text = "Culture";
            // 
            // AssemblyName
            // 
            this.AssemblyName.Location = new System.Drawing.Point(102, 9);
            this.AssemblyName.Name = "AssemblyName";
            this.AssemblyName.Size = new System.Drawing.Size(258, 20);
            this.AssemblyName.TabIndex = 0;
            this.AssemblyName.Validated += new System.EventHandler(this.AssemblyName_Validated);
            this.AssemblyName.Validating += new System.ComponentModel.CancelEventHandler(this.AssemblyName_Validating);
            // 
            // AssemblyCulture
            // 
            this.AssemblyCulture.Location = new System.Drawing.Point(102, 35);
            this.AssemblyCulture.Name = "AssemblyCulture";
            this.AssemblyCulture.Size = new System.Drawing.Size(121, 20);
            this.AssemblyCulture.TabIndex = 1;
            this.AssemblyCulture.Validated += new System.EventHandler(this.AssemblyCulture_Validated);
            // 
            // LabPublicKey
            // 
            this.LabPublicKey.AutoSize = true;
            this.LabPublicKey.Location = new System.Drawing.Point(12, 90);
            this.LabPublicKey.Name = "LabPublicKey";
            this.LabPublicKey.Size = new System.Drawing.Size(54, 13);
            this.LabPublicKey.TabIndex = 9;
            this.LabPublicKey.Text = "PublicKey";
            // 
            // PublicKey
            // 
            this.PublicKey.Location = new System.Drawing.Point(102, 87);
            this.PublicKey.Name = "PublicKey";
            this.PublicKey.Size = new System.Drawing.Size(258, 20);
            this.PublicKey.TabIndex = 6;
            this.PublicKey.Validated += new System.EventHandler(this.PublicKey_Validated);
            this.PublicKey.Validating += new System.ComponentModel.CancelEventHandler(this.StringToByte_Validating);
            // 
            // LabPublicKeyToken
            // 
            this.LabPublicKeyToken.AutoSize = true;
            this.LabPublicKeyToken.Location = new System.Drawing.Point(11, 116);
            this.LabPublicKeyToken.Name = "LabPublicKeyToken";
            this.LabPublicKeyToken.Size = new System.Drawing.Size(85, 13);
            this.LabPublicKeyToken.TabIndex = 11;
            this.LabPublicKeyToken.Text = "PublicKeyToken";
            // 
            // PublicKeyToken
            // 
            this.PublicKeyToken.Location = new System.Drawing.Point(102, 113);
            this.PublicKeyToken.Name = "PublicKeyToken";
            this.PublicKeyToken.Size = new System.Drawing.Size(258, 20);
            this.PublicKeyToken.TabIndex = 7;
            this.PublicKeyToken.Validated += new System.EventHandler(this.PublicKeyToken_Validated);
            this.PublicKeyToken.Validating += new System.ComponentModel.CancelEventHandler(this.StringToByte_Validating);
            // 
            // Hash
            // 
            this.Hash.Location = new System.Drawing.Point(102, 165);
            this.Hash.Name = "Hash";
            this.Hash.Size = new System.Drawing.Size(258, 20);
            this.Hash.TabIndex = 9;
            this.Hash.Validated += new System.EventHandler(this.Hash_Validated);
            this.Hash.Validating += new System.ComponentModel.CancelEventHandler(this.StringToByte_Validating);
            // 
            // Algorithm
            // 
            this.Algorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Algorithm.FormattingEnabled = true;
            this.Algorithm.Location = new System.Drawing.Point(102, 139);
            this.Algorithm.Name = "Algorithm";
            this.Algorithm.Size = new System.Drawing.Size(121, 21);
            this.Algorithm.TabIndex = 8;
            this.Algorithm.Validated += new System.EventHandler(this.Algorithm_Validated);
            // 
            // Major
            // 
            this.Major.Location = new System.Drawing.Point(102, 62);
            this.Major.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.Major.Name = "Major";
            this.Major.Size = new System.Drawing.Size(60, 20);
            this.Major.TabIndex = 2;
            this.Major.Validated += new System.EventHandler(this.Version_Validated);
            // 
            // Minor
            // 
            this.Minor.Location = new System.Drawing.Point(168, 61);
            this.Minor.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.Minor.Name = "Minor";
            this.Minor.Size = new System.Drawing.Size(60, 20);
            this.Minor.TabIndex = 3;
            this.Minor.Validated += new System.EventHandler(this.Version_Validated);
            // 
            // Build
            // 
            this.Build.Location = new System.Drawing.Point(234, 61);
            this.Build.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.Build.Name = "Build";
            this.Build.Size = new System.Drawing.Size(60, 20);
            this.Build.TabIndex = 4;
            this.Build.Validated += new System.EventHandler(this.Version_Validated);
            // 
            // Revision
            // 
            this.Revision.Location = new System.Drawing.Point(300, 61);
            this.Revision.Maximum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            0});
            this.Revision.Name = "Revision";
            this.Revision.Size = new System.Drawing.Size(60, 20);
            this.Revision.TabIndex = 5;
            this.Revision.Validated += new System.EventHandler(this.Version_Validated);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // AssemblyNameReferenceAttributesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AssemblyNameReferenceAttributesControl";
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            this.SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Major)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Minor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Build)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Revision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        protected System.Windows.Forms.Label LabAssemblyName;
        protected System.Windows.Forms.TextBox AssemblyCulture;
        protected System.Windows.Forms.TextBox AssemblyName;
        protected System.Windows.Forms.Label LabAssemblyCulture;
        protected System.Windows.Forms.Label LabAssemblyVersion;
        protected System.Windows.Forms.Label LabHash;
        protected System.Windows.Forms.Label LabAlgorithm;
        protected System.Windows.Forms.ComboBox Algorithm;
        protected System.Windows.Forms.TextBox Hash;
        protected System.Windows.Forms.TextBox PublicKeyToken;
        protected System.Windows.Forms.Label LabPublicKeyToken;
        protected System.Windows.Forms.TextBox PublicKey;
        protected System.Windows.Forms.Label LabPublicKey;
        protected System.Windows.Forms.NumericUpDown Revision;
        protected System.Windows.Forms.NumericUpDown Build;
        protected System.Windows.Forms.NumericUpDown Minor;
        protected System.Windows.Forms.NumericUpDown Major;
        protected System.Windows.Forms.ErrorProvider ErrorProvider;
	}
}

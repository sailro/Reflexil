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
			this.LabAlgorithm = new System.Windows.Forms.Label();
			this.LabHash = new System.Windows.Forms.Label();
			this.LabAssemblyVersion = new System.Windows.Forms.Label();
			this.LabAssemblyCulture = new System.Windows.Forms.Label();
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
			this.SplitContainer.Panel2.Controls.Add(this.LabAssemblyCulture);
			this.SplitContainer.Panel2.Controls.Add(this.LabAssemblyVersion);
			this.SplitContainer.Panel2.Controls.Add(this.LabHash);
			this.SplitContainer.Panel2.Controls.Add(this.LabAlgorithm);
			// 
			// LabAlgorithm
			// 
			this.LabAlgorithm.AutoSize = true;
			this.LabAlgorithm.Location = new System.Drawing.Point(4, 109);
			this.LabAlgorithm.Name = "LabAlgorithm";
			this.LabAlgorithm.Size = new System.Drawing.Size(50, 13);
			this.LabAlgorithm.TabIndex = 2;
			this.LabAlgorithm.Text = "Algorithm";
			// 
			// LabHash
			// 
			this.LabHash.AutoSize = true;
			this.LabHash.Location = new System.Drawing.Point(5, 135);
			this.LabHash.Name = "LabHash";
			this.LabHash.Size = new System.Drawing.Size(32, 13);
			this.LabHash.TabIndex = 3;
			this.LabHash.Text = "Hash";
			// 
			// LabAssemblyVersion
			// 
			this.LabAssemblyVersion.AutoSize = true;
			this.LabAssemblyVersion.Location = new System.Drawing.Point(5, 31);
			this.LabAssemblyVersion.Name = "LabAssemblyVersion";
			this.LabAssemblyVersion.Size = new System.Drawing.Size(42, 13);
			this.LabAssemblyVersion.TabIndex = 4;
			this.LabAssemblyVersion.Text = "Version";
			// 
			// LabAssemblyCulture
			// 
			this.LabAssemblyCulture.AutoSize = true;
			this.LabAssemblyCulture.Location = new System.Drawing.Point(5, 5);
			this.LabAssemblyCulture.Name = "LabAssemblyCulture";
			this.LabAssemblyCulture.Size = new System.Drawing.Size(40, 13);
			this.LabAssemblyCulture.TabIndex = 5;
			this.LabAssemblyCulture.Text = "Culture";
			// 
			// AssemblyCulture
			// 
			this.AssemblyCulture.Location = new System.Drawing.Point(95, 2);
			this.AssemblyCulture.Name = "AssemblyCulture";
			this.AssemblyCulture.Size = new System.Drawing.Size(121, 20);
			this.AssemblyCulture.TabIndex = 1;
			this.AssemblyCulture.Validated += new System.EventHandler(this.AssemblyCulture_Validated);
			// 
			// LabPublicKey
			// 
			this.LabPublicKey.AutoSize = true;
			this.LabPublicKey.Location = new System.Drawing.Point(5, 57);
			this.LabPublicKey.Name = "LabPublicKey";
			this.LabPublicKey.Size = new System.Drawing.Size(54, 13);
			this.LabPublicKey.TabIndex = 9;
			this.LabPublicKey.Text = "PublicKey";
			// 
			// PublicKey
			// 
			this.PublicKey.Location = new System.Drawing.Point(95, 54);
			this.PublicKey.Name = "PublicKey";
			this.PublicKey.Size = new System.Drawing.Size(258, 20);
			this.PublicKey.TabIndex = 6;
			this.PublicKey.Validating += new System.ComponentModel.CancelEventHandler(this.StringToByte_Validating);
			this.PublicKey.Validated += new System.EventHandler(this.PublicKey_Validated);
			// 
			// LabPublicKeyToken
			// 
			this.LabPublicKeyToken.AutoSize = true;
			this.LabPublicKeyToken.Location = new System.Drawing.Point(4, 83);
			this.LabPublicKeyToken.Name = "LabPublicKeyToken";
			this.LabPublicKeyToken.Size = new System.Drawing.Size(85, 13);
			this.LabPublicKeyToken.TabIndex = 11;
			this.LabPublicKeyToken.Text = "PublicKeyToken";
			// 
			// PublicKeyToken
			// 
			this.PublicKeyToken.Location = new System.Drawing.Point(95, 80);
			this.PublicKeyToken.Name = "PublicKeyToken";
			this.PublicKeyToken.Size = new System.Drawing.Size(258, 20);
			this.PublicKeyToken.TabIndex = 7;
			this.PublicKeyToken.Validating += new System.ComponentModel.CancelEventHandler(this.StringToByte_Validating);
			this.PublicKeyToken.Validated += new System.EventHandler(this.PublicKeyToken_Validated);
			// 
			// Hash
			// 
			this.Hash.Location = new System.Drawing.Point(95, 132);
			this.Hash.Name = "Hash";
			this.Hash.Size = new System.Drawing.Size(258, 20);
			this.Hash.TabIndex = 9;
			this.Hash.Validating += new System.ComponentModel.CancelEventHandler(this.StringToByte_Validating);
			this.Hash.Validated += new System.EventHandler(this.Hash_Validated);
			// 
			// Algorithm
			// 
			this.Algorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Algorithm.FormattingEnabled = true;
			this.Algorithm.Location = new System.Drawing.Point(95, 106);
			this.Algorithm.Name = "Algorithm";
			this.Algorithm.Size = new System.Drawing.Size(121, 21);
			this.Algorithm.TabIndex = 8;
			this.Algorithm.Validated += new System.EventHandler(this.Algorithm_Validated);
			// 
			// Major
			// 
			this.Major.Location = new System.Drawing.Point(95, 29);
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
			this.Minor.Location = new System.Drawing.Point(161, 28);
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
			this.Build.Location = new System.Drawing.Point(227, 28);
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
			this.Revision.Location = new System.Drawing.Point(293, 28);
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

		protected System.Windows.Forms.TextBox AssemblyCulture;
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

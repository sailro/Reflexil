/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System.Windows.Forms;

namespace Reflexil.Editors
{
	public class HexGotoForm : Form
	{
		private Label _label;
		private Button _btnCancel;
		private Button _btnOk;
		private Panel _panel;
		private LongEditor _longEditor;

		private readonly System.ComponentModel.Container _components = null;

		public HexGotoForm()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_components != null)
				{
					_components.Dispose();
				}
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
			this._label = new System.Windows.Forms.Label();
			this._btnCancel = new System.Windows.Forms.Button();
			this._btnOk = new System.Windows.Forms.Button();
			this._panel = new System.Windows.Forms.Panel();
			this._longEditor = new Reflexil.Editors.LongEditor();
			this._panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this._label.Location = new System.Drawing.Point(1, 14);
			this._label.Name = "_label";
			this._label.Size = new System.Drawing.Size(72, 16);
			this._label.TabIndex = 0;
			this._label.Text = "Byte offset:";
			// 
			// btnCancel
			// 
			this._btnCancel.Anchor =
			((System.Windows.Forms.AnchorStyles)
				((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(153, 42);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(75, 23);
			this._btnCancel.TabIndex = 2;
			this._btnCancel.Text = "Cancel";
			this._btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this._btnOk.Anchor =
			((System.Windows.Forms.AnchorStyles)
				((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._btnOk.Location = new System.Drawing.Point(72, 42);
			this._btnOk.Name = "_btnOk";
			this._btnOk.Size = new System.Drawing.Size(75, 23);
			this._btnOk.TabIndex = 1;
			this._btnOk.Text = "OK";
			this._btnOk.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// panel1
			// 
			this._panel.Controls.Add(this._longEditor);
			this._panel.Location = new System.Drawing.Point(72, 14);
			this._panel.Name = "_panel";
			this._panel.Size = new System.Drawing.Size(155, 22);
			this._panel.TabIndex = 3;
			// 
			// LongEditor
			// 
			this._longEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this._longEditor.Location = new System.Drawing.Point(0, 0);
			this._longEditor.Name = "LongEditor";
			this._longEditor.SelectedOperand = ((long) (0));
			this._longEditor.Size = new System.Drawing.Size(155, 22);
			this._longEditor.TabIndex = 0;
			this._longEditor.UseBaseSelector = true;
			this._longEditor.Value = "";
			// 
			// HexGotoForm
			// 
			this.AcceptButton = this._btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(240, 72);
			this.Controls.Add(this._panel);
			this.Controls.Add(this._btnOk);
			this.Controls.Add(this._btnCancel);
			this.Controls.Add(this._label);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HexGotoForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Goto byte";
			this.Activated += new System.EventHandler(this.FormGoTo_Activated);
			this._panel.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		public void SetDefaultValue(long byteIndex)
		{
			_longEditor.SelectedOperand = byteIndex;
		}

		public long GetByteIndex()
		{
			return _longEditor.SelectedOperand;
		}

		private void FormGoTo_Activated(object sender, System.EventArgs e)
		{
			_longEditor.Focus();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}
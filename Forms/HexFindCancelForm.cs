/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System.Windows.Forms;
using Be.Windows.Forms;

namespace Reflexil.Editors
{
	public class HexFindCancelForm : Form
	{
		private HexBox _hexBox;

		private Button _btnCancel;
		private Label _lblFindingHead;
		private Timer _timer;
		private Label _lblFinding;
		private Label _lblPercent;
		private Timer _timerPercent;
		private System.ComponentModel.IContainer components;

		public HexFindCancelForm()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
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
			this.components = new System.ComponentModel.Container();
			this._btnCancel = new System.Windows.Forms.Button();
			this._lblFindingHead = new System.Windows.Forms.Label();
			this._timer = new System.Windows.Forms.Timer(this.components);
			this._lblFinding = new System.Windows.Forms.Label();
			this._lblPercent = new System.Windows.Forms.Label();
			this._timerPercent = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(230, 12);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(72, 23);
			this._btnCancel.TabIndex = 0;
			this._btnCancel.Text = "Cancel";
			this._btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblFinding
			// 
			this._lblFindingHead.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
			this._lblFindingHead.ForeColor = System.Drawing.Color.Blue;
			this._lblFindingHead.Location = new System.Drawing.Point(141, 11);
			this._lblFindingHead.Name = "_lblFindingHead";
			this._lblFindingHead.Size = new System.Drawing.Size(80, 23);
			this._lblFindingHead.TabIndex = 1;
			// 
			// timer
			// 
			this._timer.Interval = 50;
			this._timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// _lblFinding
			// 
			this._lblFinding.ForeColor = System.Drawing.Color.Black;
			this._lblFinding.Location = new System.Drawing.Point(16, 16);
			this._lblFinding.Name = "_lblFinding";
			this._lblFinding.Size = new System.Drawing.Size(56, 16);
			this._lblFinding.TabIndex = 10;
			this._lblFinding.Text = "Finding...";
			// 
			// lblPercent
			// 
			this._lblPercent.Location = new System.Drawing.Point(78, 11);
			this._lblPercent.Name = "_lblPercent";
			this._lblPercent.Size = new System.Drawing.Size(48, 23);
			this._lblPercent.TabIndex = 12;
			this._lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// timerPercent
			// 
			this._timerPercent.Tick += new System.EventHandler(this.timerPercent_Tick);
			// 
			// HexFindCancelForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this._btnCancel;
			this.ClientSize = new System.Drawing.Size(314, 48);
			this.ControlBox = false;
			this.Controls.Add(this._lblPercent);
			this.Controls.Add(this._lblFinding);
			this.Controls.Add(this._lblFindingHead);
			this.Controls.Add(this._btnCancel);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HexFindCancelForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Finding...";
			this.Activated += new System.EventHandler(this.FormFindCancel_Activated);
			this.Deactivate += new System.EventHandler(this.FormFindCancel_Deactivate);
			this.ResumeLayout(false);
		}

		#endregion

		public void SetHexBox(HexBox hexBox)
		{
			_hexBox = hexBox;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			if (_lblFindingHead.Text.Length == 13)
				_lblFindingHead.Text = string.Empty;

			_lblFindingHead.Text += @".";
		}

		private void FormFindCancel_Activated(object sender, System.EventArgs e)
		{
			_timer.Enabled = true;
			_timerPercent.Enabled = true;
		}

		private void FormFindCancel_Deactivate(object sender, System.EventArgs e)
		{
			_timer.Enabled = false;
		}

		private void timerPercent_Tick(object sender, System.EventArgs e)
		{
			var pos = _hexBox.CurrentFindingPosition;
			var length = _hexBox.ByteProvider.Length;
			var percent = pos/(double) length*100;

			System.Globalization.NumberFormatInfo nfi =
				new System.Globalization.CultureInfo("en-US").NumberFormat;

			var text = percent.ToString("0.00", nfi) + " %";
			_lblPercent.Text = text;
		}
	}
}
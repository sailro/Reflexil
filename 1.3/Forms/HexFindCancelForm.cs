/*
 * Taken and adapted from Be.HexEditor
 * sourceforge.net/projects/hexbox
 */

using System.Windows.Forms;
using Be.Windows.Forms;

namespace Be.HexEditor
{
	/// <summary>
	/// Summary description for FormFindCancel.
	/// </summary>
	public class HexFindCancelForm : System.Windows.Forms.Form
	{
		HexBox _hexBox;

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblFinding;
        private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblPercent;
		private System.Windows.Forms.Timer timerPercent;
		private System.ComponentModel.IContainer components;

		public HexFindCancelForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFinding = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.timerPercent = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(230, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFinding
            // 
            this.lblFinding.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblFinding.ForeColor = System.Drawing.Color.Blue;
            this.lblFinding.Location = new System.Drawing.Point(141, 11);
            this.lblFinding.Name = "lblFinding";
            this.lblFinding.Size = new System.Drawing.Size(80, 23);
            this.lblFinding.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Finding...";
            // 
            // lblPercent
            // 
            this.lblPercent.Location = new System.Drawing.Point(78, 11);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(48, 23);
            this.lblPercent.TabIndex = 12;
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerPercent
            // 
            this.timerPercent.Tick += new System.EventHandler(this.timerPercent_Tick);
            // 
            // HexFindCancelForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(314, 48);
            this.ControlBox = false;
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFinding);
            this.Controls.Add(this.btnCancel);
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
			if(lblFinding.Text.Length == 13)
				lblFinding.Text = "";

			lblFinding.Text += ".";
		}

		private void FormFindCancel_Activated(object sender, System.EventArgs e)
		{
			timer.Enabled = true;
			timerPercent.Enabled = true;
		}

		private void FormFindCancel_Deactivate(object sender, System.EventArgs e)
		{
			timer.Enabled = false;
		}

		private void timerPercent_Tick(object sender, System.EventArgs e)
		{
			long pos = _hexBox.CurrentFindingPosition;
			long length = _hexBox.ByteProvider.Length;
			double percent = (double)pos / (double)length * (double)100;
			
			System.Globalization.NumberFormatInfo nfi = 
				new System.Globalization.CultureInfo("en-US").NumberFormat;

			string text = percent.ToString("0.00", nfi) + " %";
			lblPercent.Text = text;
		}
	}
}

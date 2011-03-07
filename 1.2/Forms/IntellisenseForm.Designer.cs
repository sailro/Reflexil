namespace Reflexil.Forms
{
	partial class IntellisenseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntellisenseForm));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "Icons.16x16.Class.png");
            this.ImageList.Images.SetKeyName(1, "Icons.16x16.Method.png");
            this.ImageList.Images.SetKeyName(2, "Icons.16x16.Property.png");
            this.ImageList.Images.SetKeyName(3, "Icons.16x16.Field.png");
            this.ImageList.Images.SetKeyName(4, "Icons.16x16.Enum.png");
            this.ImageList.Images.SetKeyName(5, "Icons.16x16.NameSpace.png");
            this.ImageList.Images.SetKeyName(6, "Icons.16x16.Event.png");
            // 
            // IntellisenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "IntellisenseForm";
            this.Text = "IntellisenseForm";
            this.ResumeLayout(false);

		}

		#endregion

        internal System.Windows.Forms.ImageList ImageList;
	}
}
/* Reflexil Copyright (c) 2007-2015 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

#region Imports

using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#endregion

namespace Reflexil.Editors
{
	public class BasePopupControl : Button
	{
		#region Fields

		private readonly PropertyInfo _mouseIsDown;
		private readonly PropertyInfo _mouseIsOver;

		#endregion

		#region Properties

		public ComboBoxState State
		{
			get
			{
				var result = ComboBoxState.Disabled;
				if (!Enabled)
					return result;

				if ((_mouseIsOver != null) && ((bool) _mouseIsOver.GetValue(this, null)))
				{
					if ((_mouseIsDown != null) && ((bool) _mouseIsDown.GetValue(this, null)))
					{
						result = ComboBoxState.Pressed;
					}
					else
					{
						result = ComboBoxState.Hot;
					}
				}
				else
				{
					result = ComboBoxState.Normal;
				}
				return result;
			}
		}

		#endregion

		#region Methods

		private void InitializeComponent()
		{
			SuspendLayout();
			FlatAppearance.BorderColor = SystemColors.ButtonShadow;
			FlatAppearance.MouseOverBackColor = SystemColors.Window;
			FlatStyle = FlatStyle.Flat;
			BackColor = SystemColors.Window;
			TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			ResumeLayout(false);
		}

		public BasePopupControl()
		{
			InitializeComponent();
			_mouseIsDown = GetType().GetProperty("MouseIsDown", BindingFlags.Instance | BindingFlags.NonPublic);
			_mouseIsOver = GetType().GetProperty("MouseIsOver", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		protected override void OnPaint(PaintEventArgs pevent)
		{
			ComboBoxState state = State;
			base.OnPaint(pevent);
			const int xsize = 17;
			//const int ysize = 19;
			int ysize = Height - 2;

			if (ComboBoxRenderer.IsSupported)
			{
				ComboBoxRenderer.DrawDropDownButton(pevent.Graphics,
					new Rectangle(Width - xsize - 1, Height - ysize - 1, xsize, ysize), state);
			}
			else
			{
				ControlPaint.DrawComboButton(pevent.Graphics, new Rectangle(Width - xsize - 1, Height - ysize - 1, xsize, ysize),
					(Enabled) ? ButtonState.Normal : ButtonState.Inactive);
			}
		}

		#endregion
	}
}
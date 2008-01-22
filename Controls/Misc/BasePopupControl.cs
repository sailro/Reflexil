/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007 Sebastien LEBRETON

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#region " Imports "
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
#endregion

namespace Reflexil.Editors
{
	
	public partial class BasePopupControl : Button
    {

        #region " Fields "
        private PropertyInfo mid;
        private PropertyInfo mio;
        #endregion

        #region " Properties "
        public ComboBoxState State
        {
            get
            {
                ComboBoxState result = ComboBoxState.Disabled;
                if (Enabled)
                {
                    if ((mio != null) && ((bool)mio.GetValue(this, null)))
                    {
                        if ((mid != null) && ((bool)mid.GetValue(this, null)))
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
                }
                return result;
            }
        }
        #endregion

        #region " Methods "
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackColor = SystemColors.Window;
            this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ResumeLayout(false);
        }

        public BasePopupControl() : base()
        {
            InitializeComponent();
            mid = this.GetType().GetProperty("MouseIsDown", BindingFlags.Instance | BindingFlags.NonPublic);
            mio = this.GetType().GetProperty("MouseIsOver", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            ComboBoxState state = State;
            base.OnPaint(pevent);
            const int xsize = 17;
            const int ysize = 19;

            ComboBoxRenderer.DrawDropDownButton(pevent.Graphics, new Rectangle(this.Width - xsize - 1, this.Height - ysize - 1, xsize, ysize), state);
        }
        #endregion

	}
}


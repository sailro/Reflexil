/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2009 Sebastien LEBRETON

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
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mono.Cecil;
using Reflexil.Editors;
using Reflexil.Handlers;
using Reflexil.Plugins;
using Reflexil.Utils;
#endregion

namespace Reflexil.Forms
{
	public partial class RenameForm: Form
    {
        #region " Properties "
        public object Item
        {
            get;
            set;
        }
        #endregion

        #region " Methods "
        public RenameForm()
		{
			InitializeComponent();
        }

        public void ShowDialog(object item)
        {
            Item = item;
            ItemName.Text = RenameHelper.GetName(item);
            ShowDialog();
        }
        #endregion

        #region " Events "
        private void Ok_Click(object sender, EventArgs e)
        {
            RenameHelper.Rename(Item, ItemName.Text);
        }
        #endregion
    }
}

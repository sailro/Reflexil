/*
    Reflexil .NET assembly editor.
    Copyright (C) 2007-2010 Sebastien LEBRETON

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
using System.Windows.Forms;
using Reflexil.Plugins;
using Reflexil.Properties;
#endregion

namespace Reflexil.Forms
{
	public partial class SyncWarningForm: Form
    {

        #region " Events "
        private void SyncWarningForm_Load(object sender, System.EventArgs e)
        {
            LabWarning.Text = string.Format(LabWarning.Text, PluginFactory.GetInstance().HostApplication);
        }

        private void BtOk_Click(object sender, System.EventArgs e)
        {
            Settings.Default.Save();
        }
        #endregion
        
        #region " Methods "
        public SyncWarningForm()
		{
			InitializeComponent();
        }
        #endregion
    
    }
}

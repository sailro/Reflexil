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

using System;
using System.Windows.Forms;
using Reflexil.Utils;
using Reflexil.Wrappers;

#endregion

namespace Reflexil.Editors
{
	public partial class TextComboUserControl : UserControl
	{
		#region Fields

		private ENumericBase _previousbase = ENumericBase.Dec;

		#endregion

		#region Properties

		public ENumericBase PreviousBase
		{
			get { return _previousbase; }
			private set { _previousbase = value; }
		}

		public ENumericBase CurrentBase
		{
			get { return (ENumericBase) BaseCombo.SelectedItem; }
		}

		public bool UseBaseSelector
		{
			get { return !ItemSplitContainer.Panel2Collapsed; }
			set { ItemSplitContainer.Panel2Collapsed = !value; }
		}

		public string Value
		{
			get
			{
				return (UseBaseSelector)
					? OperandDisplayHelper.Changebase(TextBox.Text, CurrentBase, ENumericBase.Dec)
					: TextBox.Text;
			}
			set
			{
				TextBox.Text = (UseBaseSelector) ? OperandDisplayHelper.Changebase(value, ENumericBase.Dec, CurrentBase) : value;
			}
		}

		#endregion

		#region Events

		private void BaseCombo_SelectionChangeCommitted(object sender, EventArgs e)
		{
			TextBox.Text = OperandDisplayHelper.Changebase(TextBox.Text, PreviousBase, CurrentBase);
			PreviousBase = CurrentBase;
		}

		#endregion

		#region Methods

		public TextComboUserControl()
		{
			InitializeComponent();
			foreach (ENumericBase item in Enum.GetValues(typeof (ENumericBase)))
			{
				BaseCombo.Items.Add(item);
			}
			BaseCombo.SelectedItem = Properties.Settings.Default.InputBase;
			PreviousBase = CurrentBase;
		}

		#endregion
	}
}
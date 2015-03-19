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

#endregion

namespace Reflexil.Forms
{
	public partial class CreateVariableForm : VariableForm
	{
		#region Methods

		public CreateVariableForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Events

		private void ButAppend_Click(object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				var newvar = CreateVariable();
				if (newvar != null)
					MethodDefinition.Body.Variables.Add(newvar);

				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void ButInsertBefore_Click(object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				var newvar = CreateVariable();
				if (newvar != null)
				{
					var vars = MethodDefinition.Body.Variables;
					vars.Insert(vars.IndexOf(SelectedVariable), newvar);
				}
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void ButInsertAfter_Click(object sender, EventArgs e)
		{
			if (IsFormComplete)
			{
				var newvar = CreateVariable();
				if (newvar != null)
				{
					var vars = MethodDefinition.Body.Variables;
					vars.Insert(vars.IndexOf(SelectedVariable) + 1, newvar);
				}
				DialogResult = DialogResult.OK;
			}
			else
			{
				DialogResult = DialogResult.None;
			}
		}

		private void CreateVariableForm_Load(object sender, EventArgs e)
		{
			ButInsertBefore.Enabled = (SelectedVariable != null);
			ButInsertAfter.Enabled = (SelectedVariable != null);
		}

		#endregion
	}
}
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
using System.Collections;
using System.Windows.Forms;
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Forms
{
	public partial class ExceptionHandlerForm
	{
		#region Properties

		public MethodDefinition MethodDefinition { get; private set; }

		public ExceptionHandler SelectedExceptionHandler { get; private set; }

		#endregion

		#region Events

		private void Types_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Types.SelectedItem == null)
				return;

			var ehtype = (ExceptionHandlerType) Types.SelectedItem;
			if (ehtype == ExceptionHandlerType.Filter)
			{
				FilterStart.Enabled = FilterEnd.Enabled = true;
			}
			else
			{
				FilterStart.Enabled = FilterEnd.Enabled = false;
				FilterStart.Text = FilterEnd.Text = string.Empty;
			}
		}

		#endregion

		#region Methods

		public ExceptionHandlerForm()
		{
			InitializeComponent();
			CatchType.Dock = DockStyle.None;
		}

		public void FillControls(MethodDefinition mdef)
		{
			foreach (var ire in new[] {TryStart, TryEnd, HandlerStart, HandlerEnd, FilterStart, FilterEnd})
			{
				ire.ReferencedItems = mdef.Body.Instructions;
				ire.Initialize(mdef);
			}

			Types.Items.AddRange(new ArrayList(Enum.GetValues(typeof (ExceptionHandlerType))).ToArray());
			Types.SelectedIndex = 0;
		}

		public virtual DialogResult ShowDialog(MethodDefinition mdef, ExceptionHandler selected)
		{
			MethodDefinition = mdef;
			SelectedExceptionHandler = selected;
			return ShowDialog();
		}

		protected ExceptionHandler CreateExceptionHandler()
		{
			try
			{
				var eh = new ExceptionHandler((ExceptionHandlerType) Types.SelectedItem);
				if (eh.HandlerType == ExceptionHandlerType.Filter)
					eh.FilterStart = FilterStart.SelectedOperand;

				eh.TryStart = TryStart.SelectedOperand;
				eh.TryEnd = TryEnd.SelectedOperand;
				eh.HandlerStart = HandlerStart.SelectedOperand;
				eh.HandlerEnd = HandlerEnd.SelectedOperand;

				if (CatchType.SelectedOperand != null)
					eh.CatchType = MethodDefinition.DeclaringType.Module.Import(CatchType.SelectedOperand);

				return eh;
			}
			catch (Exception)
			{
				MessageBox.Show(@"Reflexil is unable to create this exception handler");
				return null;
			}
		}

		#endregion
	}
}
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
using Mono.Cecil;
using Mono.Cecil.Cil;

#endregion

namespace Reflexil.Forms
{
	public partial class CreateExceptionHandlerForm
	{
		#region Events

		private void ButInsertBefore_Click(Object sender, EventArgs e)
		{
			ExceptionHandler neweh = CreateExceptionHandler();
			if (neweh != null)
			{
				var handlers = MethodDefinition.Body.ExceptionHandlers;
				handlers.Insert(handlers.IndexOf(SelectedExceptionHandler), neweh);
			}
		}

		private void ButInsertAfter_Click(Object sender, EventArgs e)
		{
			ExceptionHandler neweh = CreateExceptionHandler();
			if (neweh != null)
			{
				var handlers = MethodDefinition.Body.ExceptionHandlers;
				handlers.Insert(handlers.IndexOf(SelectedExceptionHandler) + 1, neweh);
			}
		}

		private void ButAppend_Click(Object sender, EventArgs e)
		{
			ExceptionHandler neweh = CreateExceptionHandler();
			if (neweh != null)
			{
				var handlers = MethodDefinition.Body.ExceptionHandlers;
				handlers.Add(neweh);
			}
		}

		private void CreateExceptionHandlerForm_Load(object sender, EventArgs e)
		{
			ButInsertBefore.Enabled = (SelectedExceptionHandler != null);
			ButInsertAfter.Enabled = (SelectedExceptionHandler != null);
		}

		#endregion

		#region Methods

		public CreateExceptionHandlerForm()
		{
			InitializeComponent();
		}

		public override DialogResult ShowDialog(MethodDefinition mdef, ExceptionHandler selected)
		{
			FillControls(mdef);
			return base.ShowDialog(mdef, selected);
		}

		#endregion
	}
}
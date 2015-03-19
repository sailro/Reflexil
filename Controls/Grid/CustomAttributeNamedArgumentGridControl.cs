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
using Mono.Collections.Generic;
using Reflexil.Forms;

#endregion

namespace Reflexil.Editors
{
	public partial class CustomAttributeNamedArgumentGridControl : BaseCustomAttributeNamedArgumentGridControl
	{
		#region Properties

		public bool UseFields { get; set; }

		private Collection<CustomAttributeNamedArgument> ArgumentContainer
		{
			get { return UseFields ? OwnerDefinition.Fields : OwnerDefinition.Properties; }
		}

		#endregion

		#region Methods

		public CustomAttributeNamedArgumentGridControl()
		{
			InitializeComponent();
		}

		protected override void GridContextMenuStrip_Opened(object sender, EventArgs e)
		{
			MenCreate.Enabled = (!ReadOnly) && (OwnerDefinition != null);
			MenEdit.Enabled = (!ReadOnly) && (FirstSelectedItem.HasValue);
			MenDelete.Enabled = (!ReadOnly) && (SelectedItems.Length > 0);
			MenDeleteAll.Enabled = (!ReadOnly) && (OwnerDefinition != null);
		}

		protected override void MenCreate_Click(object sender, EventArgs e)
		{
			using (var createForm = new CreateCustomAttributeNamedArgumentForm())
			{
				if (createForm.ShowDialog(OwnerDefinition, FirstSelectedItem, UseFields) == DialogResult.OK)
				{
					RaiseGridUpdated();
				}
			}
		}

		protected override void MenEdit_Click(object sender, EventArgs e)
		{
			using (var editForm = new EditCustomAttributeNamedArgumentForm())
			{
				if (editForm.ShowDialog(OwnerDefinition, FirstSelectedItem, UseFields) == DialogResult.OK)
				{
					RaiseGridUpdated();
				}
			}
		}

		protected override void MenDelete_Click(object sender, EventArgs e)
		{
			foreach (var cattrna in SelectedItems)
			{
				ArgumentContainer.Remove(cattrna.Value);
			}
			RaiseGridUpdated();
		}

		protected override void MenDeleteAll_Click(object sender, EventArgs e)
		{
			ArgumentContainer.Clear();
			RaiseGridUpdated();
		}

		protected override void DoDragDrop(object sender, DataGridViewRow sourceRow, DataGridViewRow targetRow,
			DragEventArgs e)
		{
			var sourceCattra = sourceRow.DataBoundItem as CustomAttributeNamedArgument?;
			var targetCattra = targetRow.DataBoundItem as CustomAttributeNamedArgument?;

			if (sourceCattra.HasValue && targetCattra.HasValue && !sourceCattra.Value.Equals(targetCattra.Value))
			{
				ArgumentContainer.Remove(sourceCattra.Value);
				ArgumentContainer.Insert(targetRow.Index, sourceCattra.Value);
				RaiseGridUpdated();
			}
		}

		public override void Bind(CustomAttribute cattr)
		{
			base.Bind(cattr);
			if ((cattr != null) && (ArgumentContainer != null))
			{
				BindingSource.DataSource = ArgumentContainer;
			}
			else
			{
				BindingSource.DataSource = null;
			}
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseCustomAttributeNamedArgumentGridControl : GridControl<CustomAttributeNamedArgument?, CustomAttribute>
	{
	}

	#endregion
}
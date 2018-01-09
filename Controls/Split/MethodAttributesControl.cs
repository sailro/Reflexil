/* Reflexil Copyright (c) 2007-2018 Sebastien Lebreton

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

using System;
using System.ComponentModel;
using System.Globalization;
using Mono.Cecil;
using Reflexil.Utils;

namespace Reflexil.Editors
{
	public partial class MethodAttributesControl : BaseMethodAttributesControl
	{
		public MethodAttributesControl()
		{
			InitializeComponent();
			CallingConvention.DataSource = Enum.GetValues(typeof(MethodCallingConvention));
		}

		public override void Bind(MethodDefinition mdef)
		{
			base.Bind(mdef);
			if (mdef != null)
			{
				CallingConvention.SelectedItem = mdef.CallingConvention;
				RVA.Text = mdef.RVA.ToString(CultureInfo.InvariantCulture);
				ReturnType.Context = mdef;
				ReturnType.SelectedTypeReference = mdef.ReturnType;
			}
			else
			{
				CallingConvention.SelectedIndex = -1;
				RVA.Text = string.Empty;
				ReturnType.Context = null;
				ReturnType.SelectedTypeReference = null;
			}
		}

		private void CallingConvention_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (Item != null)
			{
				Item.CallingConvention = (MethodCallingConvention) CallingConvention.SelectedItem;
			}
		}

		private void ReturnType_Validating(object sender, CancelEventArgs e)
		{
			bool validated;
			var typeSpecification = ReturnType.SelectedTypeReference as Mono.Cecil.TypeSpecification;
			if (typeSpecification != null)
			{
				var tspec = typeSpecification;
				validated = tspec.ElementType != null;
			}
			else
			{
				validated = ReturnType.SelectedTypeReference != null;
			}

			if (!validated)
			{
				ErrorProvider.SetError(ReturnType, "Type is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(ReturnType, string.Empty);
				if (Item != null && Item.Module != null)
				{
					Item.ReturnType = CecilImporter.Import(Item.Module, ReturnType.SelectedTypeReference, ReturnType.Context);
				}
			}
		}
	}

	public class BaseMethodAttributesControl : SplitAttributesControl<MethodDefinition>
	{
	}
}
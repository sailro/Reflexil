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
using System.ComponentModel;
using System.Globalization;
using Mono.Cecil;

#endregion

namespace Reflexil.Editors
{
	/// <summary>
	/// Method attributes editor (all object readable/writeable non indexed properties)
	/// </summary>
	public partial class MethodAttributesControl : BaseMethodAttributesControl
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public MethodAttributesControl()
		{
			InitializeComponent();
			CallingConvention.DataSource = Enum.GetValues(typeof (MethodCallingConvention));
		}

		/// <summary>
		/// Bind a method definition to this control
		/// </summary>
		/// <param name="mdef">Method definition to bind</param>
		public override void Bind(MethodDefinition mdef)
		{
			base.Bind(mdef);
			if (mdef != null)
			{
				CallingConvention.SelectedItem = mdef.CallingConvention;
				RVA.Text = mdef.RVA.ToString(CultureInfo.InvariantCulture);
				ReturnType.SelectedTypeReference = mdef.ReturnType;
			}
			else
			{
				CallingConvention.SelectedIndex = -1;
				RVA.Text = string.Empty;
				ReturnType.SelectedTypeReference = null;
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Handle combobox change event
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		private void CallingConvention_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (Item != null)
			{
				Item.CallingConvention = (MethodCallingConvention) CallingConvention.SelectedItem;
			}
		}

		/// <summary>
		/// Handle text box validation
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		private void ReturnType_Validating(object sender, CancelEventArgs e)
		{
			bool validated;
			if (ReturnType.SelectedTypeReference is Mono.Cecil.TypeSpecification)
			{
				var tspec = ReturnType.SelectedTypeReference as Mono.Cecil.TypeSpecification;
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
					Item.ReturnType = Item.Module.Import(ReturnType.SelectedTypeReference);
				}
			}
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseMethodAttributesControl : SplitAttributesControl<MethodDefinition>
	{
	}

	#endregion
}
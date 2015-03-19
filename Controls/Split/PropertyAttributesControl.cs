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

using Mono.Cecil;
using System.ComponentModel;
using System;

#endregion

namespace Reflexil.Editors
{
	/// <summary>
	/// Property attributes editor (all object readable/writeable non indexed properties)
	/// </summary>
	public partial class PropertyAttributesControl : BasePropertyAttributesControl
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public PropertyAttributesControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Bind a property definition to this control
		/// </summary>
		/// <param name="pdef">Property definition to bind</param>
		public override void Bind(PropertyDefinition pdef)
		{
			base.Bind(pdef);
			if (pdef != null)
			{
				PropertyType.SelectedTypeReference = pdef.PropertyType;
				Constant.ReadStateFrom(pdef);
			}
			else
			{
				PropertyType.SelectedTypeReference = null;
				Constant.Reset();
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Handle text box validation
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		private void PropertyType_Validating(object sender, CancelEventArgs e)
		{
			bool validated;
			if (PropertyType.SelectedTypeReference is Mono.Cecil.TypeSpecification)
			{
				var tspec = PropertyType.SelectedTypeReference as Mono.Cecil.TypeSpecification;
				validated = tspec.ElementType != null;
			}
			else
			{
				validated = PropertyType.SelectedTypeReference != null;
			}

			if (!validated)
			{
				ErrorProvider.SetError(PropertyType, "Type is mandatory");
				e.Cancel = true;
			}
			else
			{
				ErrorProvider.SetError(PropertyType, string.Empty);
				if (Item != null && Item.Module != null)
				{
					Item.PropertyType = Item.Module.Import(PropertyType.SelectedTypeReference);
				}
			}
		}

		private void Constant_Validating(object sender, CancelEventArgs e)
		{
			try
			{
				ErrorProvider.SetError(Constant, string.Empty);
				Constant.CopyStateTo(Item);
			}
			catch (Exception)
			{
				ErrorProvider.SetError(Constant, "Unable to convert input");
				e.Cancel = true;
			}
		}

		#endregion
	}

	#region VS Designer generic support

	public class BasePropertyAttributesControl : SplitAttributesControl<PropertyDefinition>
	{
	}

	#endregion
}
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

#endregion

namespace Reflexil.Editors
{
	/// <summary>
	/// Type attributes editor (all object readable/writeable non indexed properties)
	/// </summary>
	public partial class TypeAttributesControl : BaseTypeAttributesControl
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public TypeAttributesControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Bind a type definition to this control
		/// </summary>
		/// <param name="tdef">Type definition to bind</param>
		public override void Bind(TypeDefinition tdef)
		{
			base.Bind(tdef);
			BaseType.SelectedOperand = tdef != null ? tdef.BaseType : null;
		}

		#endregion

		#region Events

		/// <summary>
		/// Commit changes to the TypeDefinition
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		private void BaseType_Validated(object sender, System.EventArgs e)
		{
			if (Item == null)
				return;

			var tref = BaseType.SelectedOperand;
			Item.BaseType = tref != null && Item.Module != null ? Item.Module.Import(tref) : null;
		}

		#endregion
	}

	#region VS Designer generic support

	public class BaseTypeAttributesControl : SplitAttributesControl<TypeDefinition>
	{
	}

	#endregion
}
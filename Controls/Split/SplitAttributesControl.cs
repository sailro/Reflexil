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

using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace Reflexil.Editors
{
	/// <summary>
	/// Split view control: attributes editor on the left, custom controls on the right
	/// </summary>
	/// <typeparam name="T">Target object type</typeparam>
	public partial class SplitAttributesControl<T> : UserControl where T : class
	{
		#region Consts

		private const string MemberAccessMask = "@Member access";
		private const string LayoutMask = "@Layout";
		private const string ClassSemanticMask = "@Class Semantic";
		private const string MethodSemanticMask = "@Method Semantic";
		private const string StringFormatMask = "@String Format";
		private const string VtableLayoutMask = "@VTable layout";
		private const string CodeTypeMask = "@Code type";
		private const string ManagedMask = "@Managed";
		private const string MethodImplMask = "@Method Impl";

		private readonly string[] _vtableLayoutProperties = {"IsReuseSlot", "IsNewSlot"};
		private readonly string[] _codeTypeProperties = {"IsIL", "IsNative", "IsRuntime"};
		private readonly string[] _managedProperties = {"IsUnmanaged", "IsManaged"};

		private readonly string[] _memberAccessProperties =
		{
			"IsCompilerControlled", "IsPrivate", "IsFamilyAndAssembly",
			"IsAssembly", "IsFamily", "IsFamilyOrAssembly", "IsNotPublic", "IsPublic", "IsNestedPublic", "IsNestedPrivate",
			"IsNestedFamily", "IsNestedAssembly", "IsNestedFamilyAndAssembly", "IsNestedFamilyOrAssembly"
		};

		private readonly string[] _layoutProperties = {"IsAutoLayout", "IsSequentialLayout", "IsExplicitLayout"};
		private readonly string[] _classSemanticProperties = {"IsClass", "IsInterface"};

		private readonly string[] _methodSemanticProperties =
		{
			"IsSetter", "IsGetter", "IsOther", "IsAddOn", "IsRemoveOn",
			"IsFire"
		};

		private readonly string[] _stringFormatProperties = {"IsAnsiClass", "IsUnicodeClass", "IsAutoClass"};

		private readonly string[] _methodImplProperties =
		{
			"IsForwardRef", "IsPreserveSig", "IsInternalCall", "IsSynchronized",
			"NoInlining", "NoOptimization"
		};

		#endregion

		#region Fields

		private bool _readonly;
		private readonly Dictionary<string, string> _prefixes = new Dictionary<string, string>();

		#endregion

		#region Properties

		public bool ReadOnly
		{
			get { return _readonly; }
			set
			{
				_readonly = value;
				Enabled = !value;
			}
		}

		public T Item
		{
			get { return Attributes.Item as T; }
			set { Attributes.Item = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		protected SplitAttributesControl()
		{
			InitializeComponent();
			FillPrefixes(_prefixes, MemberAccessMask, _memberAccessProperties);
			FillPrefixes(_prefixes, LayoutMask, _layoutProperties);
			FillPrefixes(_prefixes, ClassSemanticMask, _classSemanticProperties);
			FillPrefixes(_prefixes, StringFormatMask, _stringFormatProperties);
			FillPrefixes(_prefixes, VtableLayoutMask, _vtableLayoutProperties);
			FillPrefixes(_prefixes, CodeTypeMask, _codeTypeProperties);
			FillPrefixes(_prefixes, ManagedMask, _managedProperties);
			FillPrefixes(_prefixes, MethodSemanticMask, _methodSemanticProperties);
			FillPrefixes(_prefixes, MethodImplMask, _methodImplProperties);
		}

		/// <summary>
		/// Fills a dictionary 
		/// </summary>
		/// <param name="prefixes">Work dictionary</param>
		/// <param name="prefix">value</param>
		/// <param name="items">keys</param>
		private static void FillPrefixes(IDictionary<string, string> prefixes, string prefix, IEnumerable<string> items)
		{
			foreach (var item in items)
			{
				prefixes.Add(item, prefix);
			}
		}

		/// <summary>
		/// Bind an item to this control
		/// </summary>
		/// <param name="item">Control to bind</param>
		public virtual void Bind(T item)
		{
			Attributes.Bind(item, _prefixes);
			if (!ReadOnly)
			{
				Enabled = (item != null);
			}
		}

		#endregion
	}
}